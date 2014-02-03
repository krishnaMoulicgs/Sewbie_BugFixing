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
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using System.Net;
using System.IO;
using System.ComponentModel;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.Web.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
namespace NopSolutions.NopCommerce.Web
{
    public partial class SinglePageGuestCheckOut : BaseNopFrontendPage
    {
        #region Declaration
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
        protected int _vendorId;
        private string _vendorIds = "";
        private string amount = "0";
        private string strPreapprovalKey = "";
        private string strmaxAmount = "0";
        protected CheckoutStepChangedEventHandler handler;
        protected ShoppingCart cart = null; 
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //user validation
            if (NopContext.Current.User == null && this.CustomerService.AnonymousCheckoutAllowed)
            {
                //create anonymous record
                this.CustomerService.CreateAnonymousUser();
            }

            if (((NopContext.Current.User == null) || NopContext.Current.User.IsGuest) && !this.CustomerService.AnonymousCheckoutAllowed)
            {
                string loginURL = SEOHelper.GetLoginPageUrl(true);
                Response.Redirect(loginURL);
            }

            if (Cart.Count == 0)
            {
                Response.Redirect(SEOHelper.GetShoppingCartUrl());
            }

             var shippingAddress = NopContext.Current.User.ShippingAddress;
             if (shippingAddress != null)
             {
                 pnlEditShippingAddress.Visible = false;
                 
             }
            if(Page.FindControl("adAddress")!=null)
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(Page.FindControl("adAddress"));
            if ((NopContext.Current.User == null) || (NopContext.Current.User.IsGuest && !this.CustomerService.AnonymousCheckoutAllowed))
            {
                //string loginURL = SEOHelper.GetLoginPageUrl(true);
                //Response.Redirect(loginURL);               
                if (!string.IsNullOrEmpty(Request.QueryString["vendorids"]))
                {
                    _vendorIds = Request.QueryString["vendorids"].ToString();                   
                   // Response.Redirect("~/GuestRegister.aspx");
                }
            }
            if (Request.QueryString["strTransactionIds"] != null && Request.QueryString["strStatus"] != null)
            {
                string strStatus = Request.QueryString["strStatus"].ToString();
                string strTransactionIds = Request.QueryString["strTransactionIds"].ToString();
                // payment status showing popup to be implement
                if (!string.IsNullOrEmpty(strTransactionIds))
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Your Transaction status :" + strStatus + " Transaction Ids are : " + strTransactionIds + "');", true);
                else
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Your Transaction status :" + strStatus + ".');", true);

            }
            else
            {
                if (this.Cart.Count == 0)
                {
                    // Response.Redirect(SEOHelper.GetShoppingCartUrl());
                    Response.Redirect(SEOHelper.GetSinglePageCheckOutShoppingCartUrl());
                }
            }

            if (!IsPostBack)
            {               
                BindData();
                BindBillingData();
                BindShippingInfoData();
                SummaryBindData();
                OrderTotalCalculation();
                FillCountryDropDownsForShipping();
                FillCountryDropDownsForBilling();
                FillStateProvinceDropDowns();
                FillBillCountryDropDownsForShipping();
                FillBillCountryDropDownsForBilling();
                FillBillStateProvinceDropDowns();
                var cresult = from c in cart group c by new { c.VendorID, c.Vendor, c.ProductVariant.Price } into g
                              let amt = g.Sum(x => x.Quantity)select new ShoppingCartItem
                              {
                                  Vendor = g.Key.Vendor,
                                  VendorID = g.Key.VendorID,
                                  CustomerEnteredPrice = amt * g.Key.Price
                              };
                List<ShoppingCartItem> list = cresult.ToList();
                Session["Vendor_Based_amt_List"] = list;// totalAmount;
                if (Session["strPreapprovalKey"] != null)
                {
                    strPreapprovalKey = Session["strPreapprovalKey"].ToString();                   
                    Session["strPreapprovalKey"] = null;
                    if (Request.QueryString["strmaxAmount"] != null)
                    {
                        strmaxAmount = Request.QueryString["strmaxAmount"].ToString();
                        PreApprovedDetails(strmaxAmount, strPreapprovalKey);
                    }
                }
            }
        }  
        protected void Page_Render(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterForEventValidation("dlShippingAddresses");
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
        protected virtual void OnCheckoutStepChanged(CheckoutStepEventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }       
        

        #region Address Ship Display
        public void BindData()
        {
            bool shoppingCartRequiresShipping = this.ShippingService.ShoppingCartRequiresShipping(Cart);
            if (!shoppingCartRequiresShipping)
            {
                SelectAddress(null);
            }
            else
            {
                var addresses = GetAllowedShippingAddresses(NopContext.Current.User);
                if (addresses.Count > 0)
                {
                    dlShippingAddresses.DataSource = addresses;
                    dlShippingAddresses.DataBind();
                    lEnterShippingAddress.Text = GetLocaleResourceString("Checkout.OrEnterNewAddress");
                }
                else
                {
                    pnlSelectShippingAddress.Visible = false;
                    lEnterShippingAddress.Text = GetLocaleResourceString("Checkout.EnterShippingAddress");
                }
            }
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
        protected List<Address> GetAllowedShippingAddresses(Customer customer)
        {
            var addresses = new List<Address>();
            if (customer == null)
                return addresses;

            foreach (var address in customer.ShippingAddresses)
            {
                var country = address.Country;
                if (country != null && country.AllowsShipping)
                {
                    if (customer.ShippingAddressId == address.AddressId)
                    {
                        addresses.Add(address);
                        break;
                    }
                }
            }

            return addresses;
        }
        public Address ShippingDisplayAddress
        {
            set
            {
                Address address = value;
               
            }
        }
        public string getStateProvinceIfnull(Address address)
        {
            var stateProvince = address.StateProvince;
            if (stateProvince != null)
                return Server.HtmlEncode(stateProvince.Name).ToString();
            else
                return "";
        }
        public string getCountryIfnull(Address address)
        {
            var Country = address.Country;
            if (Country != null)
                return Server.HtmlEncode(Country.Name).ToString();
            else
                return "";
        }
        public string getFullname(Address address)
        {
            return address.FirstName + " " + address.LastName;
        }

       
        
        protected void btnEditAddress_Click(object sender, CommandEventArgs e)
        {
            //pnladdressEdit.Visible = true;
            int addressId = Convert.ToInt32(e.CommandArgument);
            //Response.Redirect(string.Format("~/addressedit.aspx?addressid={0}", addressId));
            Response.Redirect(string.Format("~/AddressEditOnCheckOut.aspx?addressid={0}", addressId));
        }

        protected void btnDeleteAddress_Click(object sender, CommandEventArgs e)
        {
           // pnladdressEdit.Visible = true;
            int addressId = Convert.ToInt32(e.CommandArgument);
            //Response.Redirect(string.Format("~/addressedit.aspx?addressid={0}&delete={1}", addressId, true));
            Response.Redirect(string.Format("~/AddressEditOnCheckOut.aspx?addressid={0}&delete={1}", addressId, true));
        }

        [DefaultValue(true)]
        public bool ShowDeleteButton
        {
            get
            {
                return true;
                //object obj2 = this.ViewState["ShowDeleteButton"];
                //return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["ShowDeleteButton"] = value;
            }
        }

        [DefaultValue(true)]
        public bool ShowEditButton
        {
            get
            {
                return true;
                //object obj2 = this.ViewState["ShowEditButton"];
                //return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["ShowEditButton"] = value;
            }
        }
        #endregion
        #region Address Bill Display
        public void BindBillingData()
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
                    if (address.AddressId == customer.BillingAddressId)
                    {
                        addresses.Add(address);
                        break;
                    }
                }
            }

            return addresses;
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

                this.BillAddress = billingAddress;

                //automatically select the address for the user and move to next step. Rather than copying values then having to click next.
                //comment the line below to force a customer to click 'Next'
                SelectBillingAddress(this.BillingAddress);
            }
            else
            {
                pnlTheSameAsShippingAddress.Visible = false;
            }
        }
        public Address BillingDisplayAddress
        {
            set
            {
                Address address = value;

            }
        }       
        protected void btnBillEditAddress_Click(object sender, CommandEventArgs e)
        {
           // pnladdressEdit.Visible = true;
            int addressId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect(string.Format("~/AddressEditOnCheckOut.aspx?addressid={0}", addressId, true));
        }
        protected void btnBillDeleteAddress_Click(object sender, CommandEventArgs e)
        {
          //  pnladdressEdit.Visible = true;
            int addressId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect(string.Format("~/AddressEditOnCheckOut.aspx?addressid={0}&delete={1}", addressId, true));
        }
        [DefaultValue(true)]
        public bool ShowBillDeleteButton
        {
            get
            {
                return true;
                //object obj2 = this.ViewState["ShowBillDeleteButton"];
                //return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["ShowBillDeleteButton"] = value;
            }
        }
        [DefaultValue(true)]
        public bool ShowBillEditButton
        {
            get
            {
                return true;
                //object obj2 = this.ViewState["ShowBillEditButton"];
                //return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["ShowBillEditButton"] = value;
            }
        }
        #endregion

        #region AddressShipEdit
        protected Address ShippingAddress
        {
            get
            {
                var address = this.Address;
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
        protected void btnSaveShippingAddress_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var inputedAddress = this.Address;
                    inputedAddress.CustomerId = NopContext.Current.User.CustomerId;
                    inputedAddress.IsBillingAddress = false;
                    inputedAddress.CreatedOn = DateTime.UtcNow;
                    inputedAddress.UpdatedOn = DateTime.UtcNow;
                    this.CustomerService.InsertAddress(inputedAddress);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + "Saved Successfully" + "');", true);

                    pnlEditShippingAddress.Visible = false;
                    BindData();
                    BindBillingData();
                    BindShippingInfoData();
                    SummaryBindData();
                    OrderTotalCalculation();

                    if (inputedAddress != null && NopContext.Current.User != null)
                    {
                        var prevAddress = this.CustomerService.GetAddressById(inputedAddress.AddressId);
                        if (prevAddress.CustomerId != NopContext.Current.User.CustomerId)
                            return;
                    }

                    SelectAddress(inputedAddress);
                }
                catch (Exception exc)
                {
                }
                try
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

                        this.BillAddress = billingAddress;

                        //automatically select the address for the user and move to next step. Rather than copying values then having to click next.
                        //comment the line below to force a customer to click 'Next'
                        SelectBillingAddress(this.BillingAddress);
                    }
                    else
                    {
                        pnlTheSameAsShippingAddress.Visible = false;
                    }
                }
                catch (Exception ex)
                {

                }
                Response.Redirect("~/SinglePageGuestCheckOut.aspx");
            }
        }
        protected void SelectAddress(Address shippingAddress)
        {
            if (shippingAddress == null)
            {
                //set default shipping address
                NopContext.Current.User.ShippingAddressId = 0;
                this.CustomerService.UpdateCustomer(NopContext.Current.User);

                var args1 = new CheckoutStepEventArgs() { ShippingAddressSelected = true };
                OnCheckoutStepChanged(args1);

                return;
            }

            if (shippingAddress.AddressId == 0)
            {
                //check if address already exists
                var shippingAddress2 = NopContext.Current.User.ShippingAddresses.FindAddress(shippingAddress.FirstName,
                    shippingAddress.LastName, shippingAddress.PhoneNumber, shippingAddress.Email,
                    shippingAddress.FaxNumber, shippingAddress.Company,
                    shippingAddress.Address1, shippingAddress.Address2,
                    shippingAddress.City, shippingAddress.StateProvinceId, shippingAddress.ZipPostalCode,
                    shippingAddress.CountryId);

                if (shippingAddress2 != null)
                {
                    shippingAddress = shippingAddress2;
                }
                else
                {
                    shippingAddress.CustomerId = NopContext.Current.User.CustomerId;
                    shippingAddress.IsBillingAddress = false;
                    shippingAddress.CreatedOn = DateTime.UtcNow;
                    shippingAddress.UpdatedOn = DateTime.UtcNow;

                    this.CustomerService.InsertAddress(shippingAddress);
                }
            }

            //set default shipping address
            NopContext.Current.User.ShippingAddressId = shippingAddress.AddressId;
            this.CustomerService.UpdateCustomer(NopContext.Current.User);

            var args2 = new CheckoutStepEventArgs() { ShippingAddressSelected = true };
            OnCheckoutStepChanged(args2);

        }

        void BindShipAddressEdit()
        {
            if (!Page.IsPostBack)
            {
                if (IsNew)
                {
                    lblShippingAddressId.Text = string.Empty;
                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtPhoneNumber.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtFaxNumber.Text = string.Empty;
                    txtCompany.Text = string.Empty;
                    txtAddress1.Text = string.Empty;
                    txtAddress2.Text = string.Empty;

                    txtCity.Text = string.Empty;
                    txtZipPostalCode.Text = string.Empty;
                    if (IsBillingAddress)
                        this.FillCountryDropDownsForBilling();
                    else
                        this.FillCountryDropDownsForShipping();
                    this.FillStateProvinceDropDowns();
                }
            }
        }
        private void FillCountryDropDownsForShipping()
        {
            this.ddlCountry.Items.Clear();
            var countryCollection = this.CountryService.GetAllCountriesForShipping();
            foreach (var country in countryCollection)
            {
                var ddlCountryItem2 = new ListItem(country.Name, country.CountryId.ToString());
                this.ddlCountry.Items.Add(ddlCountryItem2);
            }
        }
        private void FillCountryDropDownsForBilling()
        {
            this.ddlCountry.Items.Clear();
            var countryCollection = this.CountryService.GetAllCountriesForBilling();
            foreach (var country in countryCollection)
            {
                var ddlCountryItem2 = new ListItem(country.Name, country.CountryId.ToString());
                this.ddlCountry.Items.Add(ddlCountryItem2);
            }
        }
        private void FillStateProvinceDropDowns()
        {
            this.ddlStateProvince.Items.Clear();
            int countryId = 0;
            if (this.ddlCountry.SelectedItem != null)
                countryId = int.Parse(this.ddlCountry.SelectedItem.Value);

            var stateProvinceCollection = this.StateProvinceService.GetStateProvincesByCountryId(countryId);
            foreach (var stateProvince in stateProvinceCollection)
            {
                var ddlStateProviceItem2 = new ListItem(stateProvince.Name, stateProvince.StateProvinceId.ToString());
                this.ddlStateProvince.Items.Add(ddlStateProviceItem2);
            }
            if (stateProvinceCollection.Count == 0)
            {
                var ddlStateProvinceItem = new ListItem(GetLocaleResourceString("Address.StateProvinceNonUS"), "0");
                this.ddlStateProvince.Items.Add(ddlStateProvinceItem);
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateProvinceDropDowns();
        }
        public Address Address
        {
            get
            {
                var address = new Address();
                if (!String.IsNullOrEmpty(lblShippingAddressId.Text))
                    address.AddressId = int.Parse(lblShippingAddressId.Text);
                address.FirstName = txtFirstName.Text.Trim();
                address.LastName = txtLastName.Text.Trim();
                address.PhoneNumber = txtPhoneNumber.Text.Trim();
                address.Email = txtEmail.Text.Trim();
                address.FaxNumber = txtFaxNumber.Text.Trim();
                address.Company = txtCompany.Text.Trim();
                address.Address1 = txtAddress1.Text.Trim();
                address.Address2 = txtAddress2.Text.Trim();
                address.City = txtCity.Text.Trim();

                if (this.ddlCountry.SelectedItem == null)
                    throw new NopException("Countries are not populated");
                address.CountryId = int.Parse(this.ddlCountry.SelectedItem.Value);

                if (this.ddlStateProvince.SelectedItem == null)
                    throw new NopException("State/Provinces are not populated");
                var stateProvince = this.StateProvinceService.GetStateProvinceById(int.Parse(this.ddlStateProvince.SelectedItem.Value));
                if (stateProvince != null && stateProvince.CountryId == address.CountryId)
                    address.StateProvinceId = stateProvince.StateProvinceId;

                address.ZipPostalCode = txtZipPostalCode.Text.Trim();
                return address;
            }
            set
            {
                Address address = value;
                if (address != null)
                {
                    this.lblShippingAddressId.Text = address.AddressId.ToString();
                    this.txtFirstName.Text = address.FirstName;
                    this.txtLastName.Text = address.LastName;
                    this.txtPhoneNumber.Text = address.PhoneNumber;
                    this.txtEmail.Text = address.Email;
                    this.txtFaxNumber.Text = address.FaxNumber;
                    this.txtCompany.Text = address.Company;
                    this.txtAddress1.Text = address.Address1;
                    this.txtAddress2.Text = address.Address2;
                    this.txtCity.Text = address.City;

                    if (address.IsBillingAddress)
                        this.FillCountryDropDownsForBilling();
                    else
                        this.FillCountryDropDownsForShipping();
                    CommonHelper.SelectListItem(this.ddlCountry, address.CountryId);

                    FillStateProvinceDropDowns();
                    CommonHelper.SelectListItem(this.ddlStateProvince, address.StateProvinceId);

                    this.txtZipPostalCode.Text = address.ZipPostalCode;
                }
            }
        }
        [DefaultValue(true)]
        public bool IsNew
        {
            get
            {
                object obj2 = this.ViewState["IsNew"];
                return ((obj2 != null) && ((bool)obj2));

            }
            set
            {
                this.ViewState["IsNew"] = value;
            }
        }
        [DefaultValue(true)]
        public bool IsBillingAddress
        {
            get
            {
                object obj2 = this.ViewState["IsBillingAddress"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["IsBillingAddress"] = value;
            }
        }
        public string ValidationGroup
        {
            get
            {
                return txtFirstName.ValidationGroup;
            }
            set
            {
                txtFirstName.ValidationGroup = value;
                txtLastName.ValidationGroup = value;
                txtPhoneNumber.ValidationGroup = value;
                txtEmail.ValidationGroup = value;
                txtFaxNumber.ValidationGroup = value;
                txtCompany.ValidationGroup = value;
                txtAddress1.ValidationGroup = value;
                txtAddress2.ValidationGroup = value;
                txtCity.ValidationGroup = value;
                txtZipPostalCode.ValidationGroup = value;
            }
        }        
        #endregion

        #region AddressBillEdit
        protected Address BillingAddress
        {
            get
            {
                var address = this.BillAddress;
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
        protected void btnSaveBillingAddress_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var inputedAddress = this.BillAddress;
                    inputedAddress.CustomerId = NopContext.Current.User.CustomerId;
                    inputedAddress.IsBillingAddress = true;
                    inputedAddress.CreatedOn = DateTime.UtcNow;
                    inputedAddress.UpdatedOn = DateTime.UtcNow;
                    this.CustomerService.InsertAddress(inputedAddress);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + "Saved Successfully" + "');", true);

                    SelectBillingAddress(inputedAddress);
                }
                catch (Exception exc)
                {
                }
               
            }
        }
        protected void SelectBillingAddress(Address billingAddress)
        {
            if (billingAddress == null)
            {
                //set default billing address
                NopContext.Current.User.BillingAddressId = 0;
                this.CustomerService.UpdateCustomer(NopContext.Current.User);
                var args1 = new CheckoutStepEventArgs() { BillingAddressSelected = true };
                OnCheckoutStepChanged(args1);

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
            //if (!this.OnePageCheckout)
            //    Response.Redirect("~/checkoutshippingmethod.aspx");
        }


        void BindBillShipAddressEdit()
        {
            if (!Page.IsPostBack)
            {
                if (BillIsNew )
                {
                    lblBillShippingAddressId.Text = string.Empty;
                    txtBillFirstName.Text = string.Empty;
                    txtBillLastName.Text = string.Empty;
                    txtBillPhoneNumber.Text = string.Empty;
                    txtBillEmail.Text = string.Empty;
                    txtBillFaxNumber.Text = string.Empty;
                    txtBillCompany.Text = string.Empty;
                    txtBillAddress1.Text = string.Empty;
                    txtBillAddress2.Text = string.Empty;

                    txtBillCity.Text = string.Empty;
                    txtBillZipPostalCode.Text = string.Empty;
                    if (BillIsBillingAddress)
                        this.FillBillCountryDropDownsForBilling();
                    else
                        this.FillBillCountryDropDownsForShipping();
                    this.FillBillStateProvinceDropDowns();
                }
            }
        }
        private void FillBillCountryDropDownsForShipping()
        {
            this.ddlBillCountry.Items.Clear();
            var countryCollection = this.CountryService.GetAllCountriesForShipping();
            foreach (var country in countryCollection)
            {
                var ddlCountryItem2 = new ListItem(country.Name, country.CountryId.ToString());
                this.ddlBillCountry.Items.Add(ddlCountryItem2);
            }
        }
        private void FillBillCountryDropDownsForBilling()
        {
            this.ddlBillCountry.Items.Clear();
            var countryCollection = this.CountryService.GetAllCountriesForBilling();
            foreach (var country in countryCollection)
            {
                var ddlCountryItem2 = new ListItem(country.Name, country.CountryId.ToString());
                this.ddlBillCountry.Items.Add(ddlCountryItem2);
            }
        }
        private void FillBillStateProvinceDropDowns()
        {
            this.ddlBillStateProvince.Items.Clear();
            int countryId = 0;
            if (this.ddlBillCountry.SelectedItem != null)
                countryId = int.Parse(this.ddlBillCountry.SelectedItem.Value);

            var stateProvinceCollection = this.StateProvinceService.GetStateProvincesByCountryId(countryId);
            foreach (var stateProvince in stateProvinceCollection)
            {
                var ddlStateProviceItem2 = new ListItem(stateProvince.Name, stateProvince.StateProvinceId.ToString());
                this.ddlBillStateProvince.Items.Add(ddlStateProviceItem2);
            }
            if (stateProvinceCollection.Count == 0)
            {
                var ddlStateProvinceItem = new ListItem(GetLocaleResourceString("Address.StateProvinceNonUS"), "0");
                this.ddlBillStateProvince.Items.Add(ddlStateProvinceItem);
            }
        }
        protected void ddlBillCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBillStateProvinceDropDowns();
        }
        public Address BillAddress
        {
            get
            {
                var address = new Address();
                if (!String.IsNullOrEmpty(lblBillShippingAddressId.Text))
                    address.AddressId = int.Parse(lblBillShippingAddressId.Text);
                address.FirstName = txtBillFirstName.Text.Trim();
                address.LastName = txtBillLastName.Text.Trim();
                address.PhoneNumber = txtBillPhoneNumber.Text.Trim();
                address.Email = txtBillEmail.Text.Trim();
                address.FaxNumber = txtBillFaxNumber.Text.Trim();
                address.Company = txtBillCompany.Text.Trim();
                address.Address1 = txtBillAddress1.Text.Trim();
                address.Address2 = txtBillAddress2.Text.Trim();
                address.City = txtBillCity.Text.Trim();

                if (this.ddlBillCountry.SelectedItem == null)
                    throw new NopException("Countries are not populated");
                address.CountryId = int.Parse(this.ddlBillCountry.SelectedItem.Value);

                if (this.ddlBillStateProvince.SelectedItem == null)
                    throw new NopException("State/Provinces are not populated");
                var stateProvince = this.StateProvinceService.GetStateProvinceById(int.Parse(this.ddlBillStateProvince.SelectedItem.Value));
                if (stateProvince != null && stateProvince.CountryId == address.CountryId)
                    address.StateProvinceId = stateProvince.StateProvinceId;

                address.ZipPostalCode = txtBillZipPostalCode.Text.Trim();
                return address;
            }
            set
            {
                Address address = value;
                if (address != null)
                {
                    this.lblBillShippingAddressId.Text = address.AddressId.ToString();
                    this.txtBillFirstName.Text = address.FirstName;
                    this.txtBillLastName.Text = address.LastName;
                    this.txtBillPhoneNumber.Text = address.PhoneNumber;
                    this.txtBillEmail.Text = address.Email;
                    this.txtBillFaxNumber.Text = address.FaxNumber;
                    this.txtBillCompany.Text = address.Company;
                    this.txtBillAddress1.Text = address.Address1;
                    this.txtBillAddress2.Text = address.Address2;
                    this.txtBillCity.Text = address.City;

                    if (address.IsBillingAddress)
                        this.FillBillCountryDropDownsForBilling();
                    else
                        this.FillBillCountryDropDownsForShipping();
                    CommonHelper.SelectListItem(this.ddlBillCountry, address.CountryId);

                    FillBillStateProvinceDropDowns();
                    CommonHelper.SelectListItem(this.ddlBillStateProvince, address.StateProvinceId);

                    this.txtBillZipPostalCode.Text = address.ZipPostalCode;
                }
            }
        }
        [DefaultValue(true)]
        public bool BillIsNew
        {
            get
            {
                object obj2 = this.ViewState["BillIsNew"];
                return ((obj2 != null) && ((bool)obj2));

            }
            set
            {
                this.ViewState["BillIsNew"] = value;
            }
        }
        [DefaultValue(true)]
        public bool BillIsBillingAddress
        {
            get
            {
                object obj2 = this.ViewState["BillIsBillingAddress"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["BillIsBillingAddress"] = value;
            }
        }
        public string BillValidationGroup
        {
            get
            {
                return txtBillFirstName.ValidationGroup;
            }
            set
            {
                txtBillFirstName.ValidationGroup = value;
                txtBillLastName.ValidationGroup = value;
                txtBillPhoneNumber.ValidationGroup = value;
                txtBillEmail.ValidationGroup = value;
                txtBillFaxNumber.ValidationGroup = value;
                txtBillCompany.ValidationGroup = value;
                txtBillAddress1.ValidationGroup = value;
                txtBillAddress2.ValidationGroup = value;
                txtBillCity.ValidationGroup = value;
                txtBillZipPostalCode.ValidationGroup = value;
            }
        }


        #endregion     

        #region Shipping Info
        protected void ctrlCheckoutShippingMethod_CheckoutStepChanged(object sender, CheckoutStepEventArgs e)
        {
            if (e.ShippingMethodSelected)
            {
                // ctrlCheckoutShippingMethod.BindData();
            }
        }
        protected string FormatShippingOption(ShippingOption shippingOption)
        {
            //calculate discounted and taxed rate
            Discount appliedDiscount = null;
            decimal shippingTotalWithoutDiscount = shippingOption.Rate;
            decimal discountAmount = this.ShippingService.GetShippingDiscount(NopContext.Current.User,
                shippingTotalWithoutDiscount, out appliedDiscount);
            decimal shippingTotalWithDiscount = shippingTotalWithoutDiscount - discountAmount;
            if (shippingTotalWithDiscount < decimal.Zero)
                shippingTotalWithDiscount = decimal.Zero;
            shippingTotalWithDiscount = Math.Round(shippingTotalWithDiscount, 2);

            decimal rateBase = this.TaxService.GetShippingPrice(shippingTotalWithDiscount, NopContext.Current.User);
            decimal rate = this.CurrencyService.ConvertCurrency(rateBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
            string rateStr = PriceHelper.FormatShippingPrice(rate, true);
            return string.Format("({0})", rateStr);
        }
        protected ShippingOption SelectedShippingOption
        {
            get
            {
                ShippingOption shippingOption = null;
                foreach (DataListItem item in this.dlShippingOptions.Items)
                {
                    var rdShippingOption = (RadioButton)item.FindControl("rdShippingOption");
                    var hfShippingRateComputationMethodId = (HiddenField)item.FindControl("hfShippingRateComputationMethodId");
                    var hfName = (HiddenField)item.FindControl("hfName");

                    if (rdShippingOption.Checked)
                    {
                        int shippingRateComputationMethodId = Convert.ToInt32(hfShippingRateComputationMethodId.Value);
                        string name = hfName.Value;

                        string error = string.Empty;
                        var shippingOptions = this.ShippingService.GetShippingOptions(Cart, NopContext.Current.User, NopContext.Current.User.ShippingAddress, shippingRateComputationMethodId, ref error);
                        shippingOption = shippingOptions.Find((so) => so.Name == name);
                        break;
                    }
                }
                return shippingOption;
            }
            set
            {
                foreach (DataListItem item in this.dlShippingOptions.Items)
                {
                    var rdShippingOption = (RadioButton)item.FindControl("rdShippingOption");
                    var hfName = (HiddenField)item.FindControl("hfName");

                    if (value == null)
                    {
                        rdShippingOption.Checked = false;
                    }
                    else
                    {
                        string name = hfName.Value;
                        if (name == value.Name)
                        {
                            rdShippingOption.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
        public void BindShippingInfoData()
        {
            bool shoppingCartRequiresShipping = this.ShippingService.ShoppingCartRequiresShipping(Cart);
            if (!shoppingCartRequiresShipping)
            {
                NopContext.Current.User.LastShippingOption = null;
                var args1 = new CheckoutStepEventArgs() { ShippingMethodSelected = true };
                OnCheckoutStepChanged(args1);
                //if (!this.OnePageCheckout)
                //    Response.Redirect("~/checkoutpaymentmethod.aspx");
            }
            else
            {
                string error = string.Empty;
                Address address = NopContext.Current.User.ShippingAddress;
                var shippingOptions = this.ShippingService.GetShippingOptions(Cart, NopContext.Current.User, address, ref error);
                if (!String.IsNullOrEmpty(error))
                {
                    this.LogService.InsertLog(LogTypeEnum.ShippingError, error, error);
                    phSelectShippingMethod.Visible = false;
                    lShippingMethodsError.Text = Server.HtmlEncode(error);
                }
                else
                {
                    if (shippingOptions.Count > 0)
                    {
                        phSelectShippingMethod.Visible = true;
                        dlShippingOptions.DataSource = shippingOptions;
                        dlShippingOptions.DataBind();

                        //select a default shipping option
                        if (dlShippingOptions.Items.Count > 0)
                        {
                            if (NopContext.Current.User != null &&
                                NopContext.Current.User.LastShippingOption != null)
                            {
                                //already selected shipping option
                                this.SelectedShippingOption = NopContext.Current.User.LastShippingOption;
                            }
                            else
                            {
                                //otherwise, the first shipping option
                                var tmp1 = dlShippingOptions.Items[0];
                                var rdShippingOption = tmp1.FindControl("rdShippingOption") as RadioButton;
                                if (rdShippingOption != null)
                                {
                                    rdShippingOption.Checked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        phSelectShippingMethod.Visible = false;
                        lShippingMethodsError.Text = GetLocaleResourceString("Checkout.ShippingIsNotAllowed");
                    }
                }
            }
        }
        protected void dlShippingOptions_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                var shippingOption = this.SelectedShippingOption;
                if (shippingOption != null)
                {
                    NopContext.Current.User.LastShippingOption = shippingOption;
                    var args1 = new CheckoutStepEventArgs() { ShippingMethodSelected = true };
                    OnCheckoutStepChanged(args1);

                }
            }
        }

        void rdShippingOption_CheckedChanged(object sender, EventArgs e)
        {
            var shippingOption = this.SelectedShippingOption;
            if (shippingOption != null)
            {
                NopContext.Current.User.LastShippingOption = shippingOption;
                var args1 = new CheckoutStepEventArgs() { ShippingMethodSelected = true };
                OnCheckoutStepChanged(args1);
                NopSolutions.NopCommerce.Controls.GlobalRadioButton rdShippingOption = new Controls.GlobalRadioButton();
                rdShippingOption.CheckedChanged += new EventHandler(rdShippingOption_CheckedChanged);
            }
        }
        #endregion

        #region Order Summary
        public void SummaryBindData()
        {
            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);

            var poo = (from p in cart
                       select new { VendorID = p.VendorID, VendorName = p.Vendor.CompanyName })
                        .Distinct();


            if (cart.Count > 0)
            {
                pnlEmptyCart.Visible = false;
                pnlCart.Visible = true;
                //shopping cart
                this.rptShopHeader.DataSource = poo;
                this.rptShopHeader.DataBind();              


                ValidateShoppingCart();

                foreach (RepeaterItem rptHeadItem in rptShopHeader.Items)
                {
                    ValidateCartItems(rptHeadItem);
                }
            }
            else
            {
                pnlEmptyCart.Visible = true;
                pnlCart.Visible = false;
            }

        }
        public void Checkout()
        {
            int vendorId;
            string strvendorIds = "";
            foreach (RepeaterItem rptHeadItem in rptShopHeader.Items)
            {
                System.Web.UI.WebControls.HiddenField hidVendorId = (System.Web.UI.WebControls.HiddenField)rptHeadItem.FindControl("hidVendorId");

                vendorId = int.Parse(hidVendorId.Value);
                if (string.IsNullOrEmpty(strvendorIds))
                    strvendorIds = hidVendorId.Value;
                else
                    strvendorIds +=","+ hidVendorId.Value;

                bool hasErrors = ValidateCartItems(rptHeadItem);
                if (!hasErrors)
                {                   
                    if (NopContext.Current.User == null || NopContext.Current.User.IsGuest)
                    {
                        string loginURL = SEOHelper.GetLoginPageUrl(true, this.CustomerService.AnonymousCheckoutAllowed);
                        Session["vendorid"] = vendorId;
                        Response.Redirect(loginURL);
                    }                   
                }
            }

            if (string.IsNullOrEmpty(strvendorIds))
            {
                string loginURL = SEOHelper.GetLoginPageUrl(true, this.CustomerService.AnonymousCheckoutAllowed);                
                Response.Redirect(loginURL);
            }
            else
            {
                Response.Redirect("~/SinglePageCheckOut.aspx?vendorids=" + strvendorIds);
                //  Response.Redirect("~/vendorcheckout.aspx?vendorid=" + vendorId.ToString());
            }
        }
        /// <summary>
        /// Validates shopping cart
        /// </summary>
        /// <returns>Indicates whether there're some warnings/errors</returns>
        protected bool ValidateShoppingCart()
        {
            bool hasErrors = false;

            //shopping cart
            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);

            var warnings = this.ShoppingCartService.GetShoppingCartWarnings(cart, string.Empty, false);
            if (warnings.Count > 0)
            {
                hasErrors = true;
                pnlCommonWarnings.Visible = true;
                lblCommonWarning.Visible = true;

                var scWarningsSb = new StringBuilder();
                for (int i = 0; i < warnings.Count; i++)
                {
                    scWarningsSb.Append(Server.HtmlEncode(warnings[i]));
                    if (i != warnings.Count - 1)
                    {
                        scWarningsSb.Append("<br />");
                    }
                }

                lblCommonWarning.Text = scWarningsSb.ToString();
            }
            else
            {
                pnlCommonWarnings.Visible = false;
                lblCommonWarning.Visible = false;
            }

            return hasErrors;
        }
        /// <summary>
        /// Validates shopping cart items
        /// </summary>
        /// <returns>Indicates whether there're some warnings/errors</returns>
        protected bool ValidateCartItems(RepeaterItem rptHeadItem)
        {
            bool hasErrors = false;


            Repeater rptDetail = (Repeater)rptHeadItem.FindControl("rptShoppingCart");
            //individual items
            foreach (RepeaterItem item in rptDetail.Items)
            {
                var txtQuantity = item.FindControl("txtQuantity") as TextBox;
                var lblShoppingCartItemId = item.FindControl("lblShoppingCartItemId") as Label;
                var cbRemoveFromCart = item.FindControl("cbRemoveFromCart") as CheckBox;
                var pnlWarnings = item.FindControl("pnlWarnings") as Panel;
                var lblWarning = item.FindControl("lblWarning") as Label;

                int shoppingCartItemId = 0;
                int quantity = 0;

                if (txtQuantity != null && lblShoppingCartItemId != null && cbRemoveFromCart != null)
                {
                    int.TryParse(lblShoppingCartItemId.Text, out shoppingCartItemId);

                    if (!cbRemoveFromCart.Checked)
                    {
                        int.TryParse(txtQuantity.Text, out quantity);
                        var sci = this.ShoppingCartService.GetShoppingCartItemById(shoppingCartItemId);

                        var warnings = this.ShoppingCartService.GetShoppingCartItemWarnings(
                            sci.ShoppingCartType,
                            sci.ProductVariantId,
                            sci.AttributesXml,
                            sci.CustomerEnteredPrice,
                            quantity);

                        if (warnings.Count > 0)
                        {
                            hasErrors = true;
                            if (pnlWarnings != null && lblWarning != null)
                            {
                                pnlWarnings.Visible = true;
                                lblWarning.Visible = true;

                                var addToCartWarningsSb = new StringBuilder();
                                for (int i = 0; i < warnings.Count; i++)
                                {
                                    addToCartWarningsSb.Append(Server.HtmlEncode(warnings[i]));
                                    if (i != warnings.Count - 1)
                                    {
                                        addToCartWarningsSb.Append("<br />");
                                    }
                                }

                                lblWarning.Text = addToCartWarningsSb.ToString();
                            }
                        }
                    }
                }
            }
            return hasErrors;
        }
        protected void UpdateShoppingCart(RepeaterItem rptHeadItem)
        {
            if (!IsShoppingCart)
                return;

          
            bool hasErrors = ValidateCartItems(rptHeadItem);

            Repeater rptShoppingCart = (Repeater)rptHeadItem.FindControl("rptShoppingCart");

            if (!hasErrors)
            {
                foreach (RepeaterItem item in rptShoppingCart.Items)
                {
                    var txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    var lblShoppingCartItemId = item.FindControl("lblShoppingCartItemId") as Label;
                    var cbRemoveFromCart = item.FindControl("cbRemoveFromCart") as CheckBox;

                    int shoppingCartItemId = 0;
                    int quantity = 0;

                    if (txtQuantity != null && lblShoppingCartItemId != null && cbRemoveFromCart != null)
                    {
                        int.TryParse(lblShoppingCartItemId.Text, out shoppingCartItemId);
                        if (cbRemoveFromCart.Checked)
                        {
                            this.ShoppingCartService.DeleteShoppingCartItem(shoppingCartItemId, true);
                        }
                        else
                        {
                            int.TryParse(txtQuantity.Text, out quantity);
                            List<string> addToCartWarning = this.ShoppingCartService.UpdateCart(shoppingCartItemId, quantity, true);
                        }
                    }
                }

                Response.Redirect(SEOHelper.GetShoppingCartUrl());
            }
        }
        public string GetProductVariantName(ShoppingCartItem shoppingCartItem)
        {
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
                return productVariant.LocalizedFullProductName;
            return "Not available";
        }
        public string GetProductVariantImageUrl(ShoppingCartItem shoppingCartItem)
        {
            string pictureUrl = String.Empty;
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
            {
                var productVariantPicture = productVariant.Picture;
                pictureUrl = this.PictureService.GetPictureUrl(productVariantPicture, this.SettingManager.GetSettingValueInteger("Media.ShoppingCart.ThumbnailImageSize", 80), false);
                if (String.IsNullOrEmpty(pictureUrl))
                {
                    var product = productVariant.Product;
                    var picture = product.DefaultPicture;
                    if (picture != null)
                    {
                        pictureUrl = this.PictureService.GetPictureUrl(picture, this.SettingManager.GetSettingValueInteger("Media.ShoppingCart.ThumbnailImageSize", 80));
                    }
                    else
                    {
                        pictureUrl = this.PictureService.GetDefaultPictureUrl(this.SettingManager.GetSettingValueInteger("Media.ShoppingCart.ThumbnailImageSize", 80));
                    }
                }
            }
            return pictureUrl;
        }
        public string GetProductUrl(ShoppingCartItem shoppingCartItem)
        {
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
                return SEOHelper.GetProductUrl(productVariant.ProductId);
            return string.Empty;
        }
        public string GetAttributeDescription(ShoppingCartItem shoppingCartItem)
        {
            string result = ProductAttributeHelper.FormatAttributes(shoppingCartItem.ProductVariant, shoppingCartItem.AttributesXml);
            if (!String.IsNullOrEmpty(result))
                result = "<br />" + result;
            return result;
        }
        public string GetRecurringDescription(ShoppingCartItem shoppingCartItem)
        {
            string result = string.Empty;
            if (shoppingCartItem.ProductVariant.IsRecurring)
            {
                result = string.Format(GetLocaleResourceString("ShoppingCart.RecurringPeriod"), shoppingCartItem.ProductVariant.CycleLength, ((RecurringProductCyclePeriodEnum)shoppingCartItem.ProductVariant.CyclePeriod).ToString());
                if (!String.IsNullOrEmpty(result))
                    result = "<br />" + result;
            }
            return result;
        }
        public string GetShoppingCartItemUnitPriceString(ShoppingCartItem shoppingCartItem)
        {
            var sb = new StringBuilder();
            if (shoppingCartItem.ProductVariant.CallForPrice)
            {
                sb.Append("<span class=\"productPrice\">");
                sb.Append(GetLocaleResourceString("Products.CallForPrice"));
                sb.Append("</span>");
            }
            else
            {
                decimal taxRate = decimal.Zero;
                decimal shoppingCartUnitPriceWithDiscountBase = this.TaxService.GetPrice(shoppingCartItem.ProductVariant, PriceHelper.GetUnitPrice(shoppingCartItem, true), out taxRate);
                decimal shoppingCartUnitPriceWithDiscount = this.CurrencyService.ConvertCurrency(shoppingCartUnitPriceWithDiscountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                string unitPriceString = PriceHelper.FormatPrice(shoppingCartUnitPriceWithDiscount);
                sb.Append("<span class=\"productPrice\">");
                sb.Append(unitPriceString);
                sb.Append("</span>");
            }
            return sb.ToString();
        }
        public string GetShoppingCartItemSubTotalString(ShoppingCartItem shoppingCartItem)
        {
            var sb = new StringBuilder();
            if (shoppingCartItem.ProductVariant.CallForPrice)
            {
                sb.Append("<span class=\"productPrice\">");
                sb.Append(GetLocaleResourceString("Products.CallForPrice"));
                sb.Append("</span>");
            }
            else
            {
                //sub total
                decimal taxRate = decimal.Zero;
                decimal shoppingCartItemSubTotalWithDiscountBase = this.TaxService.GetPrice(shoppingCartItem.ProductVariant, PriceHelper.GetSubTotal(shoppingCartItem, true), out taxRate);
                decimal shoppingCartItemSubTotalWithDiscount = this.CurrencyService.ConvertCurrency(shoppingCartItemSubTotalWithDiscountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                string subTotalString = PriceHelper.FormatPrice(shoppingCartItemSubTotalWithDiscount);

                sb.Append("<span class=\"productPrice\">");
                sb.Append(subTotalString);
                sb.Append("</span>");

                //display an applied discount amount
                decimal shoppingCartItemSubTotalWithoutDiscountBase = this.TaxService.GetPrice(shoppingCartItem.ProductVariant, PriceHelper.GetSubTotal(shoppingCartItem, false), out taxRate);
                decimal shoppingCartItemDiscountBase = shoppingCartItemSubTotalWithoutDiscountBase - shoppingCartItemSubTotalWithDiscountBase;
                if (shoppingCartItemDiscountBase > decimal.Zero)
                {
                    decimal shoppingCartItemDiscount = this.CurrencyService.ConvertCurrency(shoppingCartItemDiscountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                    string discountString = PriceHelper.FormatPrice(shoppingCartItemDiscount);

                    sb.Append("<br />");
                    sb.Append(GetLocaleResourceString("ShoppingCart.ItemYouSave"));
                    sb.Append("&nbsp;&nbsp;");
                    sb.Append(discountString);
                }
            }
            return sb.ToString();
        }
        public string GetCheckoutAttributeDescription()
        {
            string result = string.Empty;
            if (NopContext.Current.User != null)
            {
                string checkoutAttributes = NopContext.Current.User.CheckoutAttributes;
                result = CheckoutAttributeHelper.FormatAttributes(checkoutAttributes);
            }

            return result;
        }
        public string getTotalPriceSummary
        {
            get
            {
                return this.getTotalPriceOrder;
            }
        }
        [DefaultValue(false)]
        public bool IsShoppingCart
        {
            get
            {
                object obj2 = this.ViewState["IsShoppingCart"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["IsShoppingCart"] = value;
            }
        }
        protected void rptShopHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptShoppingCart;
            RepeaterItem item = e.Item;
            //OrderTotalsControl ctrlOrderTotals = (OrderTotalsControl)item.FindControl("ctrlOrderTotals");

            var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);

            var poo = (from p in cart
                       where DataBinder.Eval(e.Item.DataItem, "VendorID").ToString() == p.VendorID.ToString()
                       select p);

            if ((item.ItemType == ListItemType.Item) ||
                (item.ItemType == ListItemType.AlternatingItem))
            {
                rptShoppingCart = (Repeater)item.FindControl("rptShoppingCart");

                rptShoppingCart.DataSource = poo;
                rptShoppingCart.DataBind();
            }


            this.vendorId = (int)DataBinder.Eval(e.Item.DataItem, "VendorID");
            this.SummaryTotalBindData(this.IsShoppingCart, item);
        }
        protected void rptShopHeader_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            switch (e.CommandName)
            {

                //case "UpdateCart":
                //    UpdateShoppingCart(e.Item);
                //    break;
                //case "CheckoutCart":
                //    Checkout(e.Item);
                //    break;
            }
        }



        public void OrderTotalCalculation()
        {
            var paymentInfo = this.PaymentInfo;
            if (paymentInfo == null)
                paymentInfo = new BusinessLogic.Payment.PaymentInfo();

            paymentInfo.BillingAddress = NopContext.Current.User.BillingAddress;
            paymentInfo.ShippingAddress = NopContext.Current.User.ShippingAddress;
            paymentInfo.CustomerLanguage = NopContext.Current.WorkingLanguage;
            paymentInfo.CustomerCurrency = NopContext.Current.WorkingCurrency;
            paymentInfo.PaymentMethodId = 43;
            Order result = this.OrderService.TotalAmountForGuestPayment(paymentInfo, NopContext.Current.User);


            lblSubTotalAmount.Text = PriceHelper.FormatPrice(result.OrderSubtotalInclTaxInCustomerCurrency);
            lblSubTotalAmount.CssClass = "productPrice";
            lblShippingAmount.Text = PriceHelper.FormatShippingPrice(result.OrderShippingInclTaxInCustomerCurrency, true);
            lblShippingAmount.CssClass = "productPrice";
            lblTotalAmount.Text = PriceHelper.FormatPrice(result.OrderTotalInCustomerCurrency, true, false);
            lblTotalAmount.CssClass = "productPrice";
            if (result.OrderSubTotalDiscountInclTaxInCustomerCurrency > 0)
            {
                lblOrderSubTotalDiscountAmount.Text = PriceHelper.FormatPrice(-result.OrderSubTotalDiscountInclTaxInCustomerCurrency);
                phOrderSubTotalDiscount.Visible = true;
            }
            else
                phOrderSubTotalDiscount.Visible = false;

            if (result.PaymentMethodAdditionalFeeInclTaxInCustomerCurrency > 0)
            {
                lblPaymentMethodAdditionalFee.Text = PriceHelper.FormatPaymentMethodAdditionalFee(result.PaymentMethodAdditionalFeeInclTaxInCustomerCurrency, true);
            }
            else
                phPaymentMethodAdditionalFee.Visible = false;
            //if (result.OrderTaxInCustomerCurrency > 0)
            //{
                lblTaxAmount.Text = PriceHelper.FormatPrice(result.OrderTaxInCustomerCurrency, true, false);
            //}
            //else
            //    phTaxTotal.Visible = false;

            if (result.OrderDiscountInCustomerCurrency > 0)
            {
                lblOrderTotalDiscountAmount.Text = PriceHelper.FormatPrice(-result.OrderDiscountInCustomerCurrency, true, false);
                phOrderTotalDiscount.Visible = true;
            }
            else
                phOrderTotalDiscount.Visible = false;
            
            if (result.RedeemedRewardPoints != null)
            {
                phRewardPoints.Visible = true;
                lRewardPointsTitle.Text = string.Format(GetLocaleResourceString("ShoppingCart.Totals.RewardPoints"), result.RedeemedRewardPoints.Points.ToString());
                lblRewardPointsAmount.Text = PriceHelper.FormatPrice(-result.RedeemedRewardPoints.UsedAmountInCustomerCurrency, true, false);
            }
            else
            {
                phRewardPoints.Visible = false;
            }
        }
       

        public void SummaryTotalBindData(bool isShoppingCart, RepeaterItem item)
        {
            try
            {
                Label lblSubTotalAmount = (Label)item.FindControl("lblSubTotalAmount");
                Label lblOrderSubTotalDiscountAmount = (Label)item.FindControl("lblOrderSubTotalDiscountAmount");
                PlaceHolder phOrderSubTotalDiscount = (PlaceHolder)item.FindControl("phOrderSubTotalDiscount");
                LinkButton btnRemoveOrderSubTotalDiscount = (LinkButton)phOrderSubTotalDiscount.FindControl("btnRemoveOrderSubTotalDiscount");
                Label lblShippingAmount = (Label)item.FindControl("lblShippingAmount");

                Label lblPaymentMethodAdditionalFee = (Label)item.FindControl("lblPaymentMethodAdditionalFee");
                PlaceHolder phPaymentMethodAdditionalFee = (PlaceHolder)item.FindControl("phPaymentMethodAdditionalFee");
                PlaceHolder phTaxTotal = (PlaceHolder)item.FindControl("phTaxTotal");

                Label lblTaxAmount = (Label)phTaxTotal.FindControl("lblTaxAmount");
                Repeater rptrTaxRates = (Repeater)item.FindControl("rptrTaxRates");
                
                Label lblTotalAmount = (Label)item.FindControl("lblTotalAmount");

                PlaceHolder phOrderTotalDiscount = (PlaceHolder)item.FindControl("phOrderTotalDiscount");
                Label lblOrderTotalDiscountAmount = (Label)phOrderTotalDiscount.FindControl("lblOrderTotalDiscountAmount");


                LinkButton btnRemoveOrderTotalDiscount = (LinkButton)phOrderTotalDiscount.FindControl("btnRemoveOrderTotalDiscount");
                Repeater rptrGiftCards = (Repeater)item.FindControl("rptrGiftCards");
                PlaceHolder phRewardPoints = (PlaceHolder)item.FindControl("phRewardPoints");
                Literal lRewardPointsTitle = (Literal)phRewardPoints.FindControl("lRewardPointsTitle");
                Label lblRewardPointsAmount = (Label)phRewardPoints.FindControl("lblRewardPointsAmount");
               

                //int vendorId = int.Parse(HttpContext.Current.Request.QueryString["vendorid"]);
                this.IsShoppingCart = isShoppingCart;
                var cart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart, this.vendorId);

                if (cart.Count > 0)
                {
                    //payment method (if already selected)
                    int paymentMethodId = 0;
                    if (NopContext.Current.User != null)
                        paymentMethodId = NopContext.Current.User.LastPaymentMethodId;

                    //subtotal
                    decimal subtotalBase = decimal.Zero;
                    decimal orderSubTotalDiscountAmountBase = decimal.Zero;
                    Discount orderSubTotalAppliedDiscount = null;
                    decimal subTotalWithoutDiscountBase = decimal.Zero;
                    decimal subTotalWithDiscountBase = decimal.Zero;
                    string SubTotalError = this.ShoppingCartService.GetShoppingCartSubTotal(cart,
                        NopContext.Current.User, out orderSubTotalDiscountAmountBase, out orderSubTotalAppliedDiscount,
                    out subTotalWithoutDiscountBase, out subTotalWithDiscountBase);
                    subtotalBase = subTotalWithoutDiscountBase;
                    if (String.IsNullOrEmpty(SubTotalError))
                    {
                        decimal subtotal = this.CurrencyService.ConvertCurrency(subtotalBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                        lblSubTotalAmount.Text = PriceHelper.FormatPrice(subtotal);
                        lblSubTotalAmount.CssClass = "productPrice";

                        //order subtotal discount
                        if (orderSubTotalDiscountAmountBase > decimal.Zero)
                        {
                            decimal orderSubTotalDiscountAmount = this.CurrencyService.ConvertCurrency(orderSubTotalDiscountAmountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                            lblOrderSubTotalDiscountAmount.Text = PriceHelper.FormatPrice(-orderSubTotalDiscountAmount);
                            phOrderSubTotalDiscount.Visible = true;
                            btnRemoveOrderSubTotalDiscount.Visible = orderSubTotalAppliedDiscount != null &&
                                orderSubTotalAppliedDiscount.RequiresCouponCode &&
                                !String.IsNullOrEmpty(orderSubTotalAppliedDiscount.CouponCode) &&
                                this.IsShoppingCart;
                        }
                        else
                        {
                            phOrderSubTotalDiscount.Visible = false;
                            btnRemoveOrderSubTotalDiscount.Visible = false;
                        }
                    }
                    else
                    {
                        //impossible
                        lblSubTotalAmount.Text = GetLocaleResourceString("ShoppingCart.CalculatedDuringCheckout");
                        lblSubTotalAmount.CssClass = string.Empty;
                        phOrderSubTotalDiscount.Visible = false;
                    }

                    //shipping info
                    bool shoppingCartRequiresShipping = this.ShippingService.ShoppingCartRequiresShipping(cart);
                    if (shoppingCartRequiresShipping)
                    {
                        decimal? shoppingCartShippingBase = this.ShippingService.GetShoppingCartShippingTotal(cart, NopContext.Current.User);
                        if (shoppingCartShippingBase.HasValue)
                        {
                            decimal shoppingCartShipping = this.CurrencyService.ConvertCurrency(shoppingCartShippingBase.Value, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                            lblShippingAmount.Text = PriceHelper.FormatShippingPrice(shoppingCartShipping, true);
                            lblShippingAmount.CssClass = "productPrice";
                        }
                        else
                        {
                            lblShippingAmount.Text = GetLocaleResourceString("ShoppingCart.CalculatedDuringCheckout");
                            lblShippingAmount.CssClass = string.Empty;
                        }
                    }
                    else
                    {
                        lblShippingAmount.Text = GetLocaleResourceString("ShoppingCart.ShippingNotRequired");
                        lblShippingAmount.CssClass = string.Empty;
                    }

                    //payment method fee
                    bool displayPaymentMethodFee = true;
                    decimal paymentMethodAdditionalFee = this.PaymentService.GetAdditionalHandlingFee(paymentMethodId);
                    decimal paymentMethodAdditionalFeeWithTaxBase = this.TaxService.GetPaymentMethodAdditionalFee(paymentMethodAdditionalFee, NopContext.Current.User);
                    if (paymentMethodAdditionalFeeWithTaxBase > decimal.Zero)
                    {
                        decimal paymentMethodAdditionalFeeWithTax = this.CurrencyService.ConvertCurrency(paymentMethodAdditionalFeeWithTaxBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                        lblPaymentMethodAdditionalFee.Text = PriceHelper.FormatPaymentMethodAdditionalFee(paymentMethodAdditionalFeeWithTax, true);
                    }
                    else
                    {
                        displayPaymentMethodFee = false;
                    }
                    phPaymentMethodAdditionalFee.Visible = displayPaymentMethodFee;

                    //tax
                    bool displayTax = true;
                    bool displayTaxRates = true;
                    if (this.TaxService.HideTaxInOrderSummary && NopContext.Current.TaxDisplayType == TaxDisplayTypeEnum.IncludingTax)
                    {
                        displayTax = false;
                        displayTaxRates = false;
                    }
                    else
                    {
                        string taxError = string.Empty;
                        SortedDictionary<decimal, decimal> taxRates = null;
                        decimal shoppingCartTaxBase = this.TaxService.GetTaxTotal(cart, paymentMethodId, NopContext.Current.User, out taxRates, ref taxError);
                        decimal shoppingCartTax = this.CurrencyService.ConvertCurrency(shoppingCartTaxBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);

                        if (String.IsNullOrEmpty(taxError))
                        {
                            if (shoppingCartTaxBase == 0 && this.TaxService.HideZeroTax)
                            {
                                displayTax = false;
                                displayTaxRates = false;
                            }
                            else
                            {
                                displayTaxRates = this.TaxService.DisplayTaxRates && taxRates.Count > 0;
                                displayTax = !displayTaxRates;

                                lblTaxAmount.Text = PriceHelper.FormatPrice(shoppingCartTax, true, false);
                                lblTaxAmount.CssClass = "productPrice";
                                rptrTaxRates.DataSource = taxRates;
                                rptrTaxRates.DataBind();
                            }
                        }
                        else
                        {
                            lblTaxAmount.Text = GetLocaleResourceString("ShoppingCart.CalculatedDuringCheckout");
                            lblTaxAmount.CssClass = string.Empty;
                            displayTaxRates = false;
                        }
                    }
                    rptrTaxRates.Visible = displayTaxRates;
                    phTaxTotal.Visible = displayTax;

                    //total
                    decimal orderTotalDiscountAmountBase = decimal.Zero;
                    Discount orderTotalAppliedDiscount = null;
                    List<AppliedGiftCard> appliedGiftCards = null;
                    int redeemedRewardPoints = 0;
                    decimal redeemedRewardPointsAmount = decimal.Zero;
                    bool useRewardPoints = false;
                    if (NopContext.Current.User != null)
                        useRewardPoints = NopContext.Current.User.UseRewardPointsDuringCheckout;
                    decimal? shoppingCartTotalBase = this.ShoppingCartService.GetShoppingCartTotal(cart,
                        paymentMethodId, NopContext.Current.User,
                        out orderTotalDiscountAmountBase, out orderTotalAppliedDiscount,
                        out appliedGiftCards, useRewardPoints,
                        out redeemedRewardPoints, out redeemedRewardPointsAmount);
                    if (shoppingCartTotalBase.HasValue)
                    {
                        decimal shoppingCartTotal = this.CurrencyService.ConvertCurrency(shoppingCartTotalBase.Value, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                        getTotalPriceOrder = shoppingCartTotal.ToString().Trim();
                        lblTotalAmount.Text = PriceHelper.FormatPrice(shoppingCartTotal, true, false);
                        lblTotalAmount.CssClass = "productPrice";
                    }
                    else
                    {
                        getTotalPriceOrder = GetLocaleResourceString("ShoppingCart.CalculatedDuringCheckout").ToString().Trim();
                        lblTotalAmount.Text = GetLocaleResourceString("ShoppingCart.CalculatedDuringCheckout");
                        lblTotalAmount.CssClass = string.Empty;
                    }

                    //discount
                    if (orderTotalDiscountAmountBase > decimal.Zero)
                    {
                        decimal orderTotalDiscountAmount = this.CurrencyService.ConvertCurrency(orderTotalDiscountAmountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                        lblOrderTotalDiscountAmount.Text = PriceHelper.FormatPrice(-orderTotalDiscountAmount, true, false);
                        phOrderTotalDiscount.Visible = true;
                        btnRemoveOrderTotalDiscount.Visible = orderTotalAppliedDiscount != null &&
                            orderTotalAppliedDiscount.RequiresCouponCode &&
                            !String.IsNullOrEmpty(orderTotalAppliedDiscount.CouponCode) &&
                            this.IsShoppingCart;
                    }
                    else
                    {
                        phOrderTotalDiscount.Visible = false;
                        btnRemoveOrderTotalDiscount.Visible = false;
                    }

                    //gift cards
                    if (appliedGiftCards != null && appliedGiftCards.Count > 0)
                    {
                        rptrGiftCards.Visible = true;
                        rptrGiftCards.DataSource = appliedGiftCards;
                        rptrGiftCards.DataBind();
                    }
                    else
                    {
                        rptrGiftCards.Visible = false;
                    }

                    //reward points
                    if (redeemedRewardPointsAmount > decimal.Zero)
                    {
                        phRewardPoints.Visible = true;

                        decimal redeemedRewardPointsAmountInCustomerCurrency = this.CurrencyService.ConvertCurrency(redeemedRewardPointsAmount, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                        lRewardPointsTitle.Text = string.Format(GetLocaleResourceString("ShoppingCart.Totals.RewardPoints"), redeemedRewardPoints);
                        lblRewardPointsAmount.Text = PriceHelper.FormatPrice(-redeemedRewardPointsAmountInCustomerCurrency, true, false);
                    }
                    else
                    {
                        phRewardPoints.Visible = false;
                    }
                }
                else
                {
                    this.Visible = false;
                }
            }
            catch
            { }
        }

        protected void rptrTaxRates_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var item = (KeyValuePair<decimal, decimal>)e.Item.DataItem;

                var lTaxRateTitle = e.Item.FindControl("lTaxRateTitle") as Literal;
                lTaxRateTitle.Text = String.Format(GetLocaleResourceString("ShoppingCart.Totals.TaxRate"), PriceHelper.FormatTaxRate(item.Key));

                var lTaxRateValue = e.Item.FindControl("lTaxRateValue") as Literal;
                decimal taxValue = this.CurrencyService.ConvertCurrency(item.Value, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                lTaxRateValue.Text = PriceHelper.FormatPrice(taxValue, true, false);
            }
        }

        protected void rptrGiftCards_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var appliedGiftCard = e.Item.DataItem as AppliedGiftCard;

                var lGiftCard = e.Item.FindControl("lGiftCard") as Literal;
                lGiftCard.Text = String.Format(GetLocaleResourceString("ShoppingCart.Totals.GiftCardInfo"), Server.HtmlEncode(appliedGiftCard.GiftCard.GiftCardCouponCode));

                var lblGiftCardAmount = e.Item.FindControl("lblGiftCardAmount") as Label;
                decimal amountCanBeUsed = this.CurrencyService.ConvertCurrency(appliedGiftCard.AmountCanBeUsed, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                lblGiftCardAmount.Text = PriceHelper.FormatPrice(-amountCanBeUsed, true, false);

                var lGiftCardRemaining = e.Item.FindControl("lGiftCardRemaining") as Literal;
                decimal remainingAmountBase = GiftCardHelper.GetGiftCardRemainingAmount(appliedGiftCard.GiftCard) - appliedGiftCard.AmountCanBeUsed;
                decimal remainingAmount = this.CurrencyService.ConvertCurrency(remainingAmountBase, this.CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                lGiftCardRemaining.Text = string.Format(GetLocaleResourceString("ShoppingCart.Totals.GiftCardInfo.Remaining"), PriceHelper.FormatPrice(remainingAmount, true, false));

                var btnRemoveGC = e.Item.FindControl("btnRemoveGC") as LinkButton;
                btnRemoveGC.Visible = this.IsShoppingCart;
            }
        }

        protected void rptrGiftCards_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                if (this.IsShoppingCart)
                {
                    int giftCardId = Convert.ToInt32(e.CommandArgument);
                    GiftCard gc = this.OrderService.GetGiftCardById(giftCardId);
                    if (gc != null)
                    {
                        string couponCodesXML = string.Empty;
                        if (NopContext.Current.User != null)
                            couponCodesXML = NopContext.Current.User.GiftCardCouponCodes;
                        couponCodesXML = GiftCardHelper.RemoveCouponCode(couponCodesXML, gc.GiftCardCouponCode);
                        this.CustomerService.ApplyGiftCardCouponCode(couponCodesXML);
                    }

                   // this.SummaryTotalBindData(this.IsShoppingCart);
                }
            }
        }

        protected void btnRemoveOrderSubTotalDiscount_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                if (this.IsShoppingCart)
                {
                    //discount code (not used now)
                    //string discountCode = Convert.ToString(e.CommandArgument);
                    if (NopContext.Current.User != null)
                    {
                        //remove discount
                        this.CustomerService.ApplyDiscountCouponCode(NopContext.Current.User.CustomerId, string.Empty);
                    }
                    //this.SummaryTotalBindData(this.IsShoppingCart);
                }
            }
        }

        protected void btnRemoveOrderTotalDiscount_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                if (this.IsShoppingCart)
                {
                    //discount code (not used now)
                    //string discountCode = Convert.ToString(e.CommandArgument);
                    if (NopContext.Current.User != null)
                    {
                        //remove discount
                        this.CustomerService.ApplyDiscountCouponCode(NopContext.Current.User.CustomerId, string.Empty);
                    }
                    //this.SummaryTotalBindData(this.IsShoppingCart);
                }
            }
        }
        string totalPriceSun = "";

        public string getTotalPriceOrder
        {
            get
            {
                return totalPriceSun;
            }
            set
            {
                totalPriceSun = value;
            }

        }      

        public int vendorId
        {

            get { return _vendorId; }
            set { _vendorId = value; }

        }


        #endregion

        #region CheckOut
        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var paymentInfo = this.PaymentInfo;
                    if (txtNoteToSeller.Value.Trim().Length > 0)
                    {
                        Session["txtNoteToSeller"] = txtNoteToSeller.Value.Trim();
                    }
                    if (Session["Vendor_Based_amt_List"] != null)
                    {
                        List<ShoppingCartItem> vendorList = (List<ShoppingCartItem>)Session["Vendor_Based_amt_List"];
                        //decimal tempamount = 0;
                        //foreach (ShoppingCartItem item in vendorList)
                        //{
                        //    tempamount += item.CustomerEnteredPrice;
                        //}
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
                        amount = result.OrderTotalInCustomerCurrency.ToString();
                        PreApproved(amount.ToString());
                    }
                    else
                        Response.Redirect("~/shoppingcart.aspx");

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

        #region Paypal
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
                    strRequest = strRequest + "&receiverList.receiver(" + i.ToString() + ").email=" + totalAmount[i].Vendor.PaypalEmailAddress;// ConfigurationManager.AppSettings["receiver_email"];
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
            strRequest = strRequest + "&senderEmail=" + strsenderEmail;
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
                        Response.Redirect(ConfigurationManager.AppSettings["returnUrl"] + strmaxAmount + "&strTransactionIds=" + strTransactionIds + "&strStatus=" + strStatus);
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

                    Response.Redirect(ConfigurationManager.AppSettings["returnUrl"] + strmaxAmount + "&strTransactionIds=" + strTransactionIds + "&strStatus=" + strStatus);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(ConfigurationManager.AppSettings["returnUrl"] + strmaxAmount + "&strStatus=Failed" + "&strTransactionIds=");
            }

        }
        #endregion 
        #endregion
    }
}