//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Payment.Methods.PayPal;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web
{
    public partial class PaypalExpressReturnPage : BaseNopFrontendPage
    {

         string token;
         PaypalPayer payer;


        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                try
                {
                    PaymentInfo paymentInfo = new PaymentInfo();

                    PaymentMethod paypalExpressPaymentMethod = this.PaymentService.GetPaymentMethodBySystemKeyword("PayPalExpress");
            
                    paymentInfo.PaymentMethodId = paypalExpressPaymentMethod.PaymentMethodId;
                    paymentInfo.BillingAddress = NopContext.Current.User.BillingAddress;
                    paymentInfo.ShippingAddress = NopContext.Current.User.ShippingAddress;
                    paymentInfo.PaypalPayerId = payer.PayerID;
                    paymentInfo.PaypalToken = token;
                    paymentInfo.CustomerLanguage = NopContext.Current.WorkingLanguage;
                    paymentInfo.CustomerCurrency = NopContext.Current.WorkingCurrency;

                    int orderId = 0;
                    string result = this.OrderService.PlaceOrder(paymentInfo, NopContext.Current.User,int.Parse(payer.VendorId), payer.Note, out orderId);

                    Order order = this.OrderService.GetOrderById(orderId);
                    if (!String.IsNullOrEmpty(result))
                    {
                        lConfirmOrderError.Text = Server.HtmlEncode(result);
                        btnNextStep.Visible = false;
                        return;
                    }
                    else
                        this.PaymentService.PostProcessPayment(order);
                    Response.Redirect("~/checkoutcompleted.aspx");
                }
                catch (Exception exc)
                {
                    this.LogService.InsertLog(LogTypeEnum.OrderError, exc.Message, exc);
                    lConfirmOrderError.Text = Server.HtmlEncode(exc.ToString());
                    btnNextStep.Visible = false;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            PayPalExpressPaymentProcessor payPalExpress = new PayPalExpressPaymentProcessor();
            try
            {
                token = CommonHelper.QueryString("token");
                payer = payPalExpress.GetExpressCheckout(token);
                if (string.IsNullOrEmpty(payer.PayerID))
                    throw new NopException("Payer ID is not set");

                OrderSummaryControl.VendorId = int.Parse(payer.VendorId);
                OrderSummaryControl.BindData();
            }
            catch (Exception Ex)
            {
                LogService.InsertLog(LogTypeEnum.OrderError, Ex.Message, String.Empty);
            }
            finally
            {
                base.OnInit(e);
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CommonHelper.SetResponseNoCache(Response);
            ShoppingCart cart;

            if ((NopContext.Current.User == null))
            {
                //never redirect unless shopping cart not found.
                NopContext.Current.User = this.CustomerService.GetCustomerById(Convert.ToInt32(payer.CustomerId));
                if (NopContext.Current.User == null)
                {
                    //if the user is still null, check the shopping cart.
                    cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);
                    if (cart == null || cart.Count == 0)
                    {
                        //send error email no shopping cart was detected.
                        MessageService.SendEmail(@"Checkout Error: User does not have a shopping cart.",
                            @"A user attempted a checkout but did not have a valid shopping cart or a User.",
                            "site@sewbie.com", 
                            "info@sewbie.com", 
                            MessageService.GetEmailAccountById(1));
                        Response.Redirect(SEOHelper.GetShoppingCartUrl());
                    }

                }
            }

            //for now, check for a cart anyway, in any case a cart is missing it should be an immediate exit.
            cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);
            if (cart == null || cart.Count == 0)
            {
                MessageService.SendEmail(@"Checkout Error: User does not have a shopping cart.",
                            @"A user attempted a checkout but did not have a valid shopping cart.  Customer ID is " +
                            NopContext.Current.User.CustomerId.ToString() + "", "", "", MessageService.GetEmailAccountById(1));
                Response.Redirect(SEOHelper.GetShoppingCartUrl());
            }

            this.btnNextStep.Attributes.Add("onclick", "this.disabled = true;" + Page.ClientScript.GetPostBackEventReference(this.btnNextStep, ""));
        }

        public override bool AllowGuestNavigation
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this page is tracked by 'Online Customers' module
        /// </summary>
        public override bool TrackedByOnlineCustomersModule
        {
            get
            {
                return false;
            }
        }
    }
}