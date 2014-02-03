using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
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
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.Web.Templates.Payment;

namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class VendorCheckoutBillingAddressControl: BaseNopFrontendUserControl
    {
        protected CheckoutStepChangedEventHandler handler;
        protected ShoppingCart cart = null;

        protected Address BillingAddress
        {
            get
            {
                var address = ctrlBillingAddress.Address;
                if (address.AddressId != 0 && NopContext.Current.User != null)
                {
                    var prevAddress = this.CustomerService.GetAddressById(address.AddressId);
                    if (prevAddress.CustomerId != NopContext.Current.User.CustomerId)
                        return null;
                    address.CustomerId = prevAddress.CustomerId;
                    address.CreatedOn = prevAddress.CreatedOn;
                }
                else
                {
                    address.CreatedOn = DateTime.UtcNow;
                }

                return address;
            }
        }

        protected void SelectAddress(Address billingAddress)
        {
            if (billingAddress == null)
            {
                //set default billing address
                NopContext.Current.User.BillingAddressId = 0;
                this.CustomerService.UpdateCustomer(NopContext.Current.User);
                var args1 = new CheckoutStepEventArgs() { BillingAddressSelected = true };
                OnCheckoutStepChanged(args1);
                if (!this.OnePageCheckout) 
                    Response.Redirect("~/checkoutshippingmethod.aspx");
                return;
            }

            if (billingAddress.AddressId == 0)
            {
                //check if address already exists
                var billingAddress2 = NopContext.Current.User.BillingAddresses.FindAddress(billingAddress.FirstName,
                     billingAddress.LastName, billingAddress.PhoneNumber, billingAddress.Email,
                     billingAddress.FaxNumber, billingAddress.Company,
                     billingAddress.Address1, billingAddress.Address2,
                     billingAddress.City, billingAddress.StateProvinceId, billingAddress.ZipPostalCode,
                     billingAddress.CountryId);

                if (billingAddress2 != null)
                {
                    billingAddress = billingAddress2;
                }
                else
                {
                    billingAddress.CustomerId = NopContext.Current.User.CustomerId;
                    billingAddress.IsBillingAddress = true;
                    billingAddress.CreatedOn = DateTime.UtcNow;
                    billingAddress.UpdatedOn = DateTime.UtcNow;

                    this.CustomerService.InsertAddress(billingAddress);
                }
            }
            //set default billing address
            NopContext.Current.User.BillingAddressId = billingAddress.AddressId;
            this.CustomerService.UpdateCustomer(NopContext.Current.User);
            var args2 = new CheckoutStepEventArgs() { BillingAddressSelected = true };
            OnCheckoutStepChanged(args2);
            if (!this.OnePageCheckout)
                Response.Redirect("~/checkoutshippingmethod.aspx");
        }
        
        protected List<Address> GetAllowedBillingAddresses(Customer customer)
        {
            var addresses = new List<Address>();
            if (customer == null)
                return addresses;

            foreach (var address in customer.BillingAddresses)
            {
                var country = address.Country;
                if (country != null && country.AllowsBilling)
                {
                    addresses.Add(address);
                }
            }

            return addresses;
        }

        protected virtual void OnCheckoutStepChanged(CheckoutStepEventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected PaymentInfo PaymentInfo
        {
            set
            {
                this.Session["OrderPaymentInfo"] = value;
            }
        }

        public void BindData()
        {
            bool shoppingCartRequiresShipping = this.ShippingService.ShoppingCartRequiresShipping(Cart);
            if (shoppingCartRequiresShipping)
            {
                var shippingAddress = NopContext.Current.User.ShippingAddress;
                pnlTheSameAsShippingAddress.Visible = this.CustomerService.CanUseAddressAsBillingAddress(shippingAddress);
            }
            else
            {
                pnlTheSameAsShippingAddress.Visible = false;
            }

            var addresses = GetAllowedBillingAddresses(NopContext.Current.User);

            if (addresses.Count > 0)
            {
                //bind data
                dlBillingAddresses.DataSource = addresses;
                dlBillingAddresses.DataBind();
                lEnterBillingAddress.Text = GetLocaleResourceString("Checkout.OrEnterNewAddress");
            }
            else
            {
                pnlSelectBillingAddress.Visible = false;
                lEnterBillingAddress.Text = GetLocaleResourceString("Checkout.EnterBillingAddress");
            }
            }

        protected void btnSelect_Command(object sender, CommandEventArgs e)
        {
            if (Page.IsValid)
            {
                int addressId = int.Parse(e.CommandArgument.ToString());
                var billingAddress = this.CustomerService.GetAddressById(addressId);
                if (billingAddress != null && NopContext.Current.User != null)
                {
                    var prevAddress = this.CustomerService.GetAddressById(billingAddress.AddressId);
                    if (prevAddress.CustomerId != NopContext.Current.User.CustomerId)
                        return;
                }

                SelectAddress(billingAddress);

                //payment method stuff
                //reward points
                ApplyRewardPoints();

                //payment methods
                //don't bother searching for a payment method.
                //we know which one we're going to use Paypal Peer to Peer.

                var paymentMethod = this.PaymentService.GetPaymentMethodBySystemKeyword("PAYPALPEER");
                if (paymentMethod != null && paymentMethod.IsActive)
                {
                    //save selected payment methods
                    NopContext.Current.User.LastPaymentMethodId = paymentMethod.PaymentMethodId;
                    this.CustomerService.UpdateCustomer(NopContext.Current.User);
                    var args2 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                    OnCheckoutStepChanged(args2);
                    if (!this.OnePageCheckout)
                        Response.Redirect("~/checkoutpaymentinfo.aspx");
                }

                //paymentinfo stuff.
                this.PaymentInfo = this.GetPaymentInfo();
                var args1 = new CheckoutStepEventArgs() { PaymentInfoEntered = true };
                OnCheckoutStepChanged(args1);
                if (!this.OnePageCheckout)
                    Response.Redirect("~/checkoutconfirm.aspx");

                
            }
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SelectAddress(this.BillingAddress);

                //payment method stuff
                //reward points
                ApplyRewardPoints();

                //payment methods
                //don't bother searching for a payment method.
                //we know which one we're going to use Paypal Peer to Peer.

                var paymentMethod = this.PaymentService.GetPaymentMethodBySystemKeyword("PAYPALPEER");
                if (paymentMethod != null && paymentMethod.IsActive)
                {
                    //save selected payment methods
                    NopContext.Current.User.LastPaymentMethodId = paymentMethod.PaymentMethodId;
                    this.CustomerService.UpdateCustomer(NopContext.Current.User);
                    var args2 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                    OnCheckoutStepChanged(args2);
                    if (!this.OnePageCheckout)
                        Response.Redirect("~/checkoutpaymentinfo.aspx");
                }


                //paymentinfo stuff.
                this.PaymentInfo = this.GetPaymentInfo();
                var args1 = new CheckoutStepEventArgs() { PaymentInfoEntered = true };
                OnCheckoutStepChanged(args1);
                if (!this.OnePageCheckout)
                    Response.Redirect("~/checkoutconfirm.aspx");

                
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((NopContext.Current.User == null) || (NopContext.Current.User.IsGuest && !this.CustomerService.AnonymousCheckoutAllowed))
            {
                string loginURL = SEOHelper.GetLoginPageUrl(true);
                Response.Redirect(loginURL);
            }

            if (this.Cart.Count == 0)
                Response.Redirect(SEOHelper.GetShoppingCartUrl());
        }

        protected void btnTheSameAsShippingAddress_Click(object sender, EventArgs e)
        {
            var shippingAddress = NopContext.Current.User.ShippingAddress;
            if (shippingAddress != null && this.CustomerService.CanUseAddressAsBillingAddress(shippingAddress))
            {
                var billingAddress = new Address();
                billingAddress.AddressId = 0;
                billingAddress.CustomerId = shippingAddress.CustomerId;
                billingAddress.IsBillingAddress = true;
                billingAddress.FirstName = shippingAddress.FirstName;
                billingAddress.LastName = shippingAddress.LastName;
                billingAddress.PhoneNumber = shippingAddress.PhoneNumber;
                billingAddress.Email = shippingAddress.Email;
                billingAddress.FaxNumber = shippingAddress.FaxNumber;
                billingAddress.Company = shippingAddress.Company;
                billingAddress.Address1 = shippingAddress.Address1;
                billingAddress.Address2 = shippingAddress.Address2;
                billingAddress.City = shippingAddress.City;
                billingAddress.StateProvinceId = shippingAddress.StateProvinceId;
                billingAddress.ZipPostalCode = shippingAddress.ZipPostalCode;
                billingAddress.CountryId = shippingAddress.CountryId;
                billingAddress.CreatedOn = shippingAddress.CreatedOn;

                ctrlBillingAddress.Address = billingAddress;

                //automatically select the address for the user and move to next step. Rather than copying values then having to click next.
                //comment the line below to force a customer to click 'Next'
                SelectAddress(this.BillingAddress);


                //payment methods
                //don't bother searching for a payment method.
                //we know which one we're going to use Paypal Peer to Peer.

                var paymentMethod = this.PaymentService.GetPaymentMethodBySystemKeyword("PAYPALPEER");
                if (paymentMethod != null && paymentMethod.IsActive)
                {
                    //save selected payment methods
                    NopContext.Current.User.LastPaymentMethodId = paymentMethod.PaymentMethodId;
                    this.CustomerService.UpdateCustomer(NopContext.Current.User);
                    var args2 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                    OnCheckoutStepChanged(args2);
                    if (!this.OnePageCheckout)
                        Response.Redirect("~/checkoutpaymentinfo.aspx");
                }


                //paymentinfo stuff.
                this.PaymentInfo = this.GetPaymentInfo();
                var args1 = new CheckoutStepEventArgs() { PaymentInfoEntered = true };
                OnCheckoutStepChanged(args1);
                if (!this.OnePageCheckout)
                    Response.Redirect("~/checkoutconfirm.aspx");

            }
            else
            {
                pnlTheSameAsShippingAddress.Visible = false;
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

        #region payment info stuff
            public PaymentInfo GetPaymentInfo()
            {
                // grabbed this straight from the template.
                // captured this by 
                PaymentInfo paymentInfo = new PaymentInfo();
                paymentInfo.CreditCardType = string.Empty;
                paymentInfo.CreditCardName = string.Empty;
                paymentInfo.CreditCardNumber = string.Empty;
                paymentInfo.CreditCardExpireYear = 0;
                paymentInfo.CreditCardExpireMonth = 0;
                paymentInfo.CreditCardCvv2 = string.Empty;
                paymentInfo.PaymentMethodId = NopContext.Current.User.LastPaymentMethodId;
                return paymentInfo;
            }
        #endregion


        #region payment method stuff
            public void ApplyRewardPoints()
            {
                //reward points
                if (NopContext.Current.User != null)
                {
                    NopContext.Current.User.UseRewardPointsDuringCheckout = false;// cbUseRewardPoints.Checked;
                }

                //Check whether payment workflow is required
                bool isPaymentWorkflowRequired = IsPaymentWorkflowRequired();
                if (!isPaymentWorkflowRequired)
                {
                    //save selected payment methods
                    NopContext.Current.User.LastPaymentMethodId = 0;
                    this.CustomerService.UpdateCustomer(NopContext.Current.User);

                    var args1 = new CheckoutStepEventArgs() { PaymentMethodSelected = true };
                    OnCheckoutStepChanged(args1);
                    if (!this.OnePageCheckout)
                        Response.Redirect("~/checkoutpaymentinfo.aspx");
                }
            }

            public bool IsPaymentWorkflowRequired()
            {
                bool result = true;

                //check whether order total equals zero
                if (NopContext.Current.User != null)
                {
                    decimal? shoppingCartTotalBase = this.ShoppingCartService.GetShoppingCartTotal(this.Cart,
                    NopContext.Current.User.LastPaymentMethodId, NopContext.Current.User);

                    if (shoppingCartTotalBase.HasValue && shoppingCartTotalBase.Value == decimal.Zero)
                    {
                        result = false;
                    }
                }
                return result;
            }
        #endregion
    }
}