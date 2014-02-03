using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using System.Net;
using System.IO;
using System.Text;
namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class SinglePageCheckoutConfirm : BaseNopFrontendUserControl
    {
        protected CheckoutStepChangedEventHandler handler;
        protected ShoppingCart cart = null;
        protected int _vendorId;
        private string _vendorIds = "";
        private string amount = "0";
        private string strPreapprovalKey = "";
        private string strmaxAmount = "0";
        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                     var paymentInfo = this.PaymentInfo;

                    if (paymentInfo == null)
                    {
                        var args1 = new CheckoutStepEventArgs() { OrderConfirmed = false };
                        OnCheckoutStepChanged(args1);
                        if (!this.OnePageCheckout)
                        {
                            Response.Redirect("~/checkoutpaymentinfo.aspx");
                           
                        }
                       
                    }
                  
                    if (!this.OnePageCheckout)
                        Response.Redirect("~/checkoutcompleted.aspx");
                    else
                    {
                        if (txtNoteToSeller.Value.Trim().Length > 0)
                        {
                            Session["txtNoteToSeller"]= txtNoteToSeller.Value.Trim();
                        }
                        if (Session["Vendor_Based_amt_List"] != null)
                        {
                            List<ShoppingCartItem> vendorList = (List<ShoppingCartItem>)Session["Vendor_Based_amt_List"];
                            decimal tempamount = 0;
                            foreach (ShoppingCartItem item in vendorList)
                            {
                                tempamount += item.CustomerEnteredPrice;
                            }
                            if (paymentInfo == null)                                
                            paymentInfo = new BusinessLogic.Payment.PaymentInfo();

                            paymentInfo.BillingAddress = NopContext.Current.User.BillingAddress;
                            paymentInfo.ShippingAddress = NopContext.Current.User.ShippingAddress;
                            paymentInfo.CustomerLanguage = NopContext.Current.WorkingLanguage;
                            paymentInfo.CustomerCurrency = NopContext.Current.WorkingCurrency;
                            paymentInfo.PaymentMethodId = 43;
                            Order result = this.OrderService.TotalAmountForPayment(paymentInfo, NopContext.Current.User);
                            foreach (ShoppingCartItem item in vendorList)
                            {
                                Order result1 = this.OrderService.VendorBasedTotalAmountForPayment(paymentInfo, NopContext.Current.User, item.VendorID);
                                if (result1 != null)
                                    item.CustomerEnteredPrice = result1.OrderTotalInCustomerCurrency;
                            }
                            Session["Vendor_Based_amt_List"] = vendorList;
                            amount =result.OrderTotalInCustomerCurrency.ToString();
                            PreApproved(amount.ToString());
                        }
                        else
                            Response.Redirect("~/shoppingcart.aspx");

                    }
                }
                catch (Exception exc)
                {
                    this.LogService.InsertLog(LogTypeEnum.OrderError, exc.Message, exc);
                    lConfirmOrderError.Text = Server.HtmlEncode(exc.ToString());
                }
            }
            else
            {
                foreach (System.Web.UI.IValidator poo in Page.Validators)
                {
                    if (!poo.IsValid)
                    {
                        if (lConfirmOrderError.Text == String.Empty)
                        {
                            lConfirmOrderError.Text = poo.ErrorMessage;
                        }
                        else
                        {
                            lConfirmOrderError.Text += poo.ErrorMessage;
                        }
                    }
                }
            }
        }

        protected void PreApproved(string amount)
        {
            string strmaxAmount = amount;
            StringBuilder dataToSend = new StringBuilder();
            string strRequest = null;
            strRequest = "cancelUrl=" + ConfigurationManager.AppSettings["cancelUrl"];
            strRequest = strRequest + "&clientDetails.applicationId=" + ConfigurationManager.AppSettings["applicationId"];
            // strRequest = strRequest + "&clientDetails.deviceId=" + ConfigurationManager.AppSettings["deviceId"];// "127.0.0.1";
            strRequest = strRequest + "&currencyCode=" + NopContext.Current.WorkingCurrency.CurrencyCode;
            strRequest = strRequest + "&startingDate=" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.000zzz");// +TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("yyyy-MM-ddTHH:mm:ss.000zzz");// +TimeZoneInfo.ConvertTime(DateTimeOffset.Now.AddMinutes(10), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("yyyy-MM-ddTHH:mm:ss.FFF 000");//  System.DateTime.Now.ToString("O");//.ToString("yyyy-MM-ddThh:mm:ssZ");
            strRequest = strRequest + "&endingDate=" + DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss.000zzz");// +TimeZoneInfo.ConvertTime(DateTimeOffset.Now.AddHours(1), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")).ToString("yyyy-MM-ddTHH:mm:ss.000zzz");
            //strRequest = strRequest + "&startingDate=2013-02-25T10:20:16.269-08:00";// +System.DateTime.Now.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss.FFF zzz");// System.DateTime.Now.ToString("O");//.ToString("yyyy-MM-ddThh:mm:ssZ");
            //strRequest = strRequest + "&endingDate=2013-02-27T01:32:16.269-08:00";// +System.DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss.FFF zzz");//.ToString("yyyy-MM-ddThh:mm:ssZ");     
            strRequest = strRequest + "&maxTotalAmountOfAllPayments=" + strmaxAmount;
            strRequest = strRequest + "&requestEnvelope.errorLanguage=" + NopContext.Current.WorkingLanguage.LanguageCulture;
            strRequest = strRequest + "&requestEnvelope.detailLevel=" + "ReturnAll";
            strRequest = strRequest + "&returnUrl=" + ConfigurationManager.AppSettings["returnUrl"] + "?strmaxAmount=" + strmaxAmount;
            string strEndpointURL = ConfigurationManager.AppSettings["strEndpointURL"];
            string strRedirectURL = ConfigurationManager.AppSettings["strRedirectURL"] + "&preapprovalkey=";

            dataToSend.Append(strRequest);

            try
            {

                string strResponse = this.GetResponce(dataToSend, strEndpointURL);
                string[] strSplited = strResponse.Split(new char[] { '&' });
                string strPreapprovalKey = null;
                string strTmp = strSplited[1];
                strTmp = strTmp.Substring(21, 7);
                if (strTmp == "Success")
                {
                    strPreapprovalKey = strSplited[4];
                    strPreapprovalKey = strPreapprovalKey.Substring(15, 20);
                    strRedirectURL = strRedirectURL + strPreapprovalKey;
                    Session["strPreapprovalKey"] = strPreapprovalKey;
                    Response.Redirect(strRedirectURL);

                }
            }
            catch (Exception ex)
            {

            }
        }
        string GetResponce(StringBuilder strRequest, string strEndpointURL)
        {
            HttpWebRequest objectWebRequest = (HttpWebRequest)WebRequest.Create(strEndpointURL);
            objectWebRequest.Method = "POST";
            string USERID = ConfigurationManager.AppSettings["USERID"];//"ilsmut_1357886581_biz_api1.gmail.com";
            string PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];//"1357886601";
            string SIGNATURE = ConfigurationManager.AppSettings["SIGNATURE"];//"AOIwOKO7.AyfJVZAidZVKyKI-oS9A2Ik0WoxUysmRO4idMrSsVDaf0Hz";
            objectWebRequest.Headers.Set("X-PAYPAL-SECURITY-USERID", USERID);
            objectWebRequest.Headers.Set("X-PAYPAL-SECURITY-PASSWORD", PASSWORD);
            objectWebRequest.Headers.Set("X-PAYPAL-SECURITY-SIGNATURE", SIGNATURE);
            objectWebRequest.Headers.Set("X-PAYPAL-REQUEST-DATA-FORMAT", "NV");
            objectWebRequest.Headers.Set("X-PAYPAL-RESPONSE-DATA-FORMAT", "NV");
            objectWebRequest.Headers.Set("X-PAYPAL-APPLICATION-ID", "APP-80W284485P519543T");
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(strRequest.ToString());
            objectWebRequest.ContentLength = byteData.Length;
            Stream postStream = objectWebRequest.GetRequestStream();
            postStream.Write(byteData, 0, byteData.Length);
            HttpWebResponse response = default(HttpWebResponse);
            response = (HttpWebResponse)objectWebRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string strResponse = reader.ReadToEnd();
            return strResponse;
        }
        protected void PreApprovedDetails(string strmaxAmount, string strPreapprovalKey)
        {
            StringBuilder dataToSend = new StringBuilder();
            string strRequest = null;
            strRequest = strRequest + "requestEnvelope.errorLanguage=" + NopContext.Current.WorkingLanguage.LanguageCulture;
            strRequest = strRequest + "&requestEnvelope.detailLevel=ReturnAll";
            strRequest = strRequest + "&preapprovalKey=" + strPreapprovalKey;
            string strEndpointURL = ConfigurationManager.AppSettings["PreapprovalDetails_strEndpointURL"];

            dataToSend.Append(strRequest);

            try
            {
                string strResponse = this.GetResponce(dataToSend, strEndpointURL);
                string[] strSplited = strResponse.Split(new char[] { '&' });
                string strTmp = strSplited[1];
                strTmp = strTmp.Substring(21, 7);
                if (strTmp == "Success")
                {
                    strPreapprovalKey = strSplited[4];

                    string strsenderEmail = "";
                    for (int i = 0; i < strSplited.Length; i++)
                    {
                        if (strSplited[i].Contains("senderEmail="))
                            strsenderEmail = strSplited[i].Split('=')[1].ToString();
                    }
                    PaypalCreatePay(strmaxAmount, strsenderEmail);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void PaypalCreatePayBK(string strmaxAmount, string strsenderEmail)
        {
            List<ShoppingCartItem> totalAmount = (List<ShoppingCartItem>)Session["Vendor_amt"];
            StringBuilder dataToSend = new StringBuilder();
            string strRequest = null;
            strRequest = "actionType=CREATE";
            strRequest = strRequest + "&cancelUrl=" + ConfigurationManager.AppSettings["cancelUrl"];
            strRequest = strRequest + "&currencyCode=" + NopContext.Current.WorkingCurrency.CurrencyCode;

            strRequest = strRequest + "&feesPayer=EACHRECEIVER&senderEmail=" + strsenderEmail;
            if (totalAmount.Capacity <= 6)
            {
                for (int i = 0; i < totalAmount.Count; i++)
                {
                    strRequest = strRequest + "&receiverList.receiver(" + i.ToString() + ").email=" + totalAmount[i].Vendor.PaypalEmailAddress;
                    strRequest = strRequest + "&receiverList.receiver(" + i.ToString() + ").amount=" + totalAmount[i].CustomerEnteredPrice.ToString();
                }
            }
            else
            {
                //not possible on parallel process payment @max of six account can process

            }
            
            strRequest = strRequest + "&requestEnvelope.errorLanguage=" + NopContext.Current.WorkingLanguage.LanguageCulture;
            strRequest = strRequest + "&requestEnvelope.detailLevel=ReturnAll";
            strRequest = strRequest + "&returnUrl=" + ConfigurationManager.AppSettings["returnUrl"];
            dataToSend.Append(strRequest);




            string strEndpointURL = ConfigurationManager.AppSettings["Pay_strEndpointURL"];
            string strRedirectURL = ConfigurationManager.AppSettings["Pay_strRedirectURL"] + "&preapprovalkey=" + strPreapprovalKey + "&paykey=";
            try
            {
                HttpWebRequest objectWebRequest = (HttpWebRequest)WebRequest.Create(strEndpointURL);
                objectWebRequest.Method = "POST";
                string USERID = ConfigurationManager.AppSettings["USERID"];//"ilsmut_1357886581_biz_api1.gmail.com";
                string PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];//"1357886601";
                string SIGNATURE = ConfigurationManager.AppSettings["SIGNATURE"];//"AOIwOKO7.AyfJVZAidZVKyKI-oS9A2Ik0WoxUysmRO4idMrSsVDaf0Hz";
                objectWebRequest.Headers.Set("X-PAYPAL-SECURITY-USERID", USERID);
                objectWebRequest.Headers.Set("X-PAYPAL-SECURITY-PASSWORD", PASSWORD);
                objectWebRequest.Headers.Set("X-PAYPAL-SECURITY-SIGNATURE", SIGNATURE);
                objectWebRequest.Headers.Set("X-PAYPAL-REQUEST-DATA-FORMAT", "NV");
                objectWebRequest.Headers.Set("X-PAYPAL-RESPONSE-DATA-FORMAT", "NV");
                objectWebRequest.Headers.Set("X-PAYPAL-APPLICATION-ID", "APP-80W284485P519543T");
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(dataToSend.ToString());
                objectWebRequest.ContentLength = byteData.Length;
                Stream postStream = objectWebRequest.GetRequestStream();
                postStream.Write(byteData, 0, byteData.Length);
                HttpWebResponse response = default(HttpWebResponse);
                response = (HttpWebResponse)objectWebRequest.GetResponse();
                StreamReader reader = default(StreamReader);
                reader = new StreamReader(response.GetResponseStream());
                string strResponse = reader.ReadToEnd();
                string[] strSplited = strResponse.Split('&');
                string strPayKey = null;
                string strTmp = strSplited[1];
                strTmp = strTmp.Substring(21, 7);
                if (strTmp == "Success")
                {
                    strPayKey = strSplited[4];
                    strPayKey = strPayKey.Substring(7, 20);
                    strRedirectURL = strRedirectURL + strPayKey;


                    ExecutePay(strPayKey, strmaxAmount);

                    // Response.Redirect(strRedirectURL);
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void PaypalCreatePay(string strmaxAmount, string strsenderEmail)
        {
            List<ShoppingCartItem> Vendor_Based_amt_List = (List<ShoppingCartItem>)Session["Vendor_Based_amt_List"];
            StringBuilder dataToSend = new StringBuilder();
            string strRequest = null;
            strRequest = "requestEnvelope.errorLanguage=" + NopContext.Current.WorkingLanguage.LanguageCulture;
            strRequest = strRequest + "&actionType=CREATE";
            strRequest = strRequest + "&currencyCode=" + NopContext.Current.WorkingCurrency.CurrencyCode;
            strRequest = strRequest + "&feesPayer=EACHRECEIVER";
            strRequest = strRequest + "&memo=CgsTest";
            if (Vendor_Based_amt_List.Capacity <= 6)
            {

                for (int i = 0; i < Vendor_Based_amt_List.Count; i++)
                {
                    strRequest = strRequest + "&receiverList.receiver(" + i.ToString() + ").email=" + Vendor_Based_amt_List[i].Vendor.PaypalEmailAddress;// ConfigurationManager.AppSettings["receiver_email"];
                    strRequest = strRequest + "&receiverList.receiver(" + i.ToString() + ").amount=" + Vendor_Based_amt_List[i].CustomerEnteredPrice.ToString();
                    strRequest = strRequest + "&receiverList.receiver(" + i.ToString() + ").primary(" + i.ToString() + ")=false";
                }
            }
            else
            {
                //not possible on parallel process payment @max of six account can process
            }
            strRequest = strRequest + "&senderEmail="+strsenderEmail;
            strRequest = strRequest + "&cancelUrl=" + ConfigurationManager.AppSettings["cancelUrl"];
            //strRequest = strRequest + "&requestEnvelope.detailLevel=ReturnAll";
            strRequest = strRequest + "&returnUrl=" + ConfigurationManager.AppSettings["returnUrl"];
            strRequest = strRequest + "&preapprovalKey=" + strPreapprovalKey;
            dataToSend.Append(strRequest);
            string strEndpointURL = ConfigurationManager.AppSettings["Pay_strEndpointURL"];
            string strRedirectURL = ConfigurationManager.AppSettings["Pay_strRedirectURL"] + "&preapprovalkey=" + strPreapprovalKey + "&paykey=";
            try
            {

                string strResponse = this.GetResponce(dataToSend, strEndpointURL);               
                string[] strSplited = strResponse.Split('&');
                string strPayKey = null;
                string strTmp = strSplited[1];
                strTmp = strTmp.Substring(21, 7);
                if (strTmp == "Success")
                {
                    strPayKey = strSplited[4];
                    strPayKey = strPayKey.Substring(7, 20);
                    strRedirectURL = strRedirectURL + strPayKey;
                    ExecutePay(strPayKey, strmaxAmount);
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void ExecutePay(string paykey, string strmaxAmount)
        {
            // paykey = "AP-2M702517CG537834K";
            StringBuilder dataToSend = new StringBuilder();
            string strRequest = null;
            strRequest = strRequest + "requestEnvelope.errorLanguage=" + NopContext.Current.WorkingLanguage.LanguageCulture;
            strRequest = strRequest + "&requestEnvelope.detailLevel=ReturnAll";
            strRequest = strRequest + "&payKey=" + paykey;
            strRequest = strRequest + "&returnUrl=" + ConfigurationManager.AppSettings["returnUrl"];
            dataToSend.Append(strRequest);
            string Execute_strEndpointURL = ConfigurationManager.AppSettings["Execute_strEndpointURL"];
            try
            {
                string strResponse = this.GetResponce(dataToSend, Execute_strEndpointURL);  
                string[] strSplited = strResponse.Split('&');
                string strPayKey = null;
                string strTmp = strSplited[1];
                strTmp = strTmp.Substring(21, 7);
                if (strTmp == "Success")
                {
                    strPayKey = strSplited[4];
                    strPayKey = strPayKey.Substring(7, 20);
                    
                    // Response.Redirect( ConfigurationManager.AppSettings["returnUrl"]);

                }
                ExecutePayStatus(paykey, strmaxAmount);
            }
            catch (Exception ex)
            {

            }

        }
        public void ExecutePayStatus(string paykey, string strmaxAmount)
        {
            StringBuilder dataToSend = new StringBuilder();
            string strRequest = null;
            strRequest = strRequest + "requestEnvelope.errorLanguage=" + NopContext.Current.WorkingLanguage.LanguageCulture;
            strRequest = strRequest + "&requestEnvelope.detailLevel=ReturnAll";
            strRequest = strRequest + "&payKey=" + paykey;
            strRequest = strRequest + "&returnUrl=" + ConfigurationManager.AppSettings["returnUrl"];// "https://localhost:8091/SewbieDemo/PaypalAPRedirect.aspx";
            dataToSend.Append(strRequest);
            string Execute_strEndpointURL = ConfigurationManager.AppSettings["ExecuteDetails_strEndpointURL"];
            try
            {
                string strResponse = this.GetResponce(dataToSend, Execute_strEndpointURL);                
                string[] strSplited = strResponse.Split('&');
                string strPayKey = null;
                string strTmp = strSplited[1];
                strTmp = strTmp.Substring(21, 7);
                if (strTmp == "Success")
                {
                    strPayKey = strSplited[4];
                    strPayKey = strPayKey.Substring(7, 20);
                    int j = 0;
                    string strTransactionIds = "";
                    string strStatus = "";
                    for (int i = 0; i < strSplited.Length; i++)
                    {
                        if (strSplited[i].Contains("paymentInfoList.paymentInfo(" + j.ToString() + ").senderTransactionId="))
                        {
                            if (string.IsNullOrEmpty(strTransactionIds))
                                strTransactionIds = strSplited[i].Split('=')[1].ToString();
                            else
                                strTransactionIds += "," + strSplited[i].Split('=')[1].ToString();
                            j++;
                        }
                        if (strSplited[i].Contains("status="))
                            strStatus = strSplited[i].Split('=')[1].ToString();
                    }
                    if (strStatus == "CREATED")
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["PayPalSuccessReturnUrl"] + strmaxAmount + "&strTransactionIds=" + strTransactionIds + "&strStatus=" + strStatus);
                    }
                    var paymentInfo = this.PaymentInfo;
                    paymentInfo = new BusinessLogic.Payment.PaymentInfo();
                    paymentInfo.BillingAddress = NopContext.Current.User.BillingAddress;
                    paymentInfo.ShippingAddress = NopContext.Current.User.ShippingAddress;
                    paymentInfo.CustomerLanguage = NopContext.Current.WorkingLanguage;
                    paymentInfo.CustomerCurrency = NopContext.Current.WorkingCurrency;
                    paymentInfo.PaymentMethodId = 43;
                    int orderId = 0;

                    string result = this.OrderService.PlaceOrder(paymentInfo, NopContext.Current.User, out orderId);

                    this.PaymentInfo = null;
                    var order = this.OrderService.GetOrderById(orderId);

                    if (txtNoteToSeller.Value.Trim().Length > 0)
                    {
                        OrderService.InsertOrderNote(orderId, txtNoteToSeller.Value.Trim(), DateTime.Now);
                    }

                    if (!String.IsNullOrEmpty(result))
                    {
                        lConfirmOrderError.Text = Server.HtmlEncode(result);
                        return;
                    }
                    else
                    {
                       // hidPaypalURL.Value = this.PaymentService.PostProcessPayment(order);
                    }

                    var args2 = new CheckoutStepEventArgs() { OrderConfirmed = true };
                    if (hidPaypalURL.Value != String.Empty)
                    {
                        //redirect via javascript, directly into the frame.
                        //Response.Redirect(hidPaypalURL.Value, false);
                    }
                    else
                    {
                        OnCheckoutStepChanged(args2);
                    }

                    Response.Redirect(ConfigurationManager.AppSettings["PayPalSuccessReturnUrl"] + strmaxAmount + "&strTransactionIds=" + strTransactionIds + "&strStatus=" + strStatus);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(ConfigurationManager.AppSettings["PayPalSuccessReturnUrl"] + strmaxAmount + "&strStatus=Failed" + "&strTransactionIds=");
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //for latest change 
                string TransactionID = Request.QueryString.Get("strTransactionIds");
                string Status = Request.QueryString.Get("strStatus");
                string Amount = Request.QueryString.Get("strmaxAmount");
                string Currency = Request.QueryString.Get("cc");
                string OrderID = Request.QueryString.Get("ID");
               


                if (Session["strPreapprovalKey"] != null)
                {
                    strPreapprovalKey = Session["strPreapprovalKey"].ToString();
                    // strmaxAmount = Session["strmaxAmount"].ToString();
                    Session["strPreapprovalKey"] = null;
                    if (Request.QueryString["strmaxAmount"] != null)
                    {
                        strmaxAmount = Request.QueryString["strmaxAmount"].ToString();
                        PreApprovedDetails(strmaxAmount, strPreapprovalKey);

                    }
                }
                if (this.Cart.Count == 0)
                {
                    Response.Redirect(SEOHelper.GetShoppingCartUrl());
                }
            }
                   
            if ((NopContext.Current.User == null) || (NopContext.Current.User.IsGuest && !this.CustomerService.AnonymousCheckoutAllowed))
            {
                string loginURL = SEOHelper.GetLoginPageUrl(true);
                Response.Redirect(loginURL);
            }

            //if (Request.QueryString["vendorids"] == null)
            //{

            //    _vendorIds = "";
            //}
            //else
            //{
            //    _vendorIds = Request.QueryString["vendorids"].ToString();
            //}
           
            //if (this.Cart.Count == 0)
            //    Response.Redirect(SEOHelper.GetSinglePageCheckOutShoppingCartUrl());
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.btnNextStep.Attributes.Add("onclick", "this.disabled = true;" + Page.ClientScript.GetPostBackEventReference(this.btnNextStep, ""));

            string checkoutScript = CommonHelper.GetStoreLocation() + "Scripts/checkout.js";
            //Page.ClientScript.RegisterClientScriptInclude(checkoutScript, checkoutScript);

            //string paypalJsLibrary = this.SettingManager.GetSettingValue("Paypal.AdaptivePayments.JavascriptLocation");
            //Page.ClientScript.RegisterClientScriptInclude(paypalJsLibrary, paypalJsLibrary);

            //string paypalScript = CommonHelper.GetStoreLocation() + "Scripts/paypalDebugger.js";
            //Page.ClientScript.RegisterClientScriptInclude(paypalScript, paypalScript);

            //use postback if we're on one-page checkout page
            //we need it to properly process redirects (hosted payment methods)
            if (this.SettingManager.GetSettingValueBoolean("Checkout.UseOnePageCheckout"))
            {
                var sm = ScriptManager.GetCurrent(this.Page);
                if (sm != null)
                {
                    sm.RegisterPostBackControl(btnNextStep);
                }
            }

            base.OnPreRender(e);
        }

        protected virtual void OnCheckoutStepChanged(CheckoutStepEventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void BindData()
        {
            //min order amount validation
            bool minOrderTotalAmountOK = this.OrderService.ValidateMinOrderTotalAmount(this.Cart, NopContext.Current.User);
            if (minOrderTotalAmountOK)
            {
                lMinOrderTotalAmount.Visible = false;
                btnNextStep.Visible = true;
            }
            else
            {
                decimal minOrderTotalAmount = this.CurrencyService.ConvertCurrency(this.OrderService.MinOrderTotalAmount, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                lMinOrderTotalAmount.Text = string.Format(GetLocaleResourceString("Checkout.MinOrderTotalAmount"), PriceHelper.FormatPrice(minOrderTotalAmount, true, false));
                lMinOrderTotalAmount.Visible = true;
                btnNextStep.Visible = false;
            }
        }

        public event CheckoutStepChangedEventHandler CheckoutStepChanged
        {
            add
            {
                handler += value;
            }
            remove
            {
                handler -= value;
            }
        }

        protected PaymentInfo PaymentInfo
        {
            get
            {
                if (this.Session["OrderPaymentInfo"] != null)
                    return (PaymentInfo)(this.Session["OrderPaymentInfo"]);
                return null;
            }
            set
            {
                this.Session["OrderPaymentInfo"] = value;
            }
        }

        public ShoppingCart Cart
        {
            get
            {
                if (cart == null)
                {
                    cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);
                }
                return cart;
            }
        }

        public bool OnePageCheckout
        {
            get
            {
                if (ViewState["OnePageCheckout"] != null)
                    return (bool)ViewState["OnePageCheckout"];
                return false;
            }
            set
            {
                ViewState["OnePageCheckout"] = value;
            }
        }
    }
}