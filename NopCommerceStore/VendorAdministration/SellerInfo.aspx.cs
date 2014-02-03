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
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class SellerInfo : BaseNopVendorAdministrationPage
    {
        private void BindData()
        {
            AddressEditControl.IsBillingAddress = this.IsBillingAddress;
            if (this.AddressId > 0)
            {
                var address = this.CustomerService.GetAddressById(this.AddressId);
                if (address != null)
                {
                    lHeaderTitle.Text = GetLocaleResourceString("Address.UpdateAddressTitle");
                    btnSave.Text = GetLocaleResourceString("Address.UpdateAddress");
                    btnDelete.Visible = true;
                    AddressEditControl.IsNew = false;
                    AddressEditControl.IsBillingAddress = address.IsBillingAddress;
                    AddressEditControl.Address = address;
                }
                else
                    Response.Redirect(CommonHelper.GetStoreLocation());
            }
            else
            {
                lHeaderTitle.Text = GetLocaleResourceString("Address.NewAddressTitle");
                btnSave.Text = GetLocaleResourceString("Address.AddAddress");
                btnDelete.Visible = false;
                AddressEditControl.IsNew = true;
                AddressEditControl.IsBillingAddress = this.IsBillingAddress;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string title = GetLocaleResourceString("PageTitle.AddressEdit");
            SEOHelper.RenderTitle(this, title, true);

            CommonHelper.SetResponseNoCache(Response);

            if (NopContext.Current.User == null)
            {
                string loginURL = SEOHelper.GetLoginPageUrl(true);
                Response.Redirect(loginURL);
            }
            var address = this.CustomerService.GetAddressById(this.AddressId);
            if (address != null)
            {
                var addressCustomer = address.Customer;
                if (addressCustomer == null || addressCustomer.CustomerId != NopContext.Current.User.CustomerId)
                {
                    string loginURL = SEOHelper.GetLoginPageUrl(true);
                    Response.Redirect(loginURL);
                }

                if (DeleteAddress)
                {
                    this.CustomerService.DeleteAddress(address.AddressId);
                    Response.Redirect(SEOHelper.GetMyAccountUrl());
                }
            }

            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //TODO: Consider how to save this data: Option 1 is to save it with a new vendor flag, Option 2 is to save it
                //      with a newly developed address type, and Option 3 is to save it with the vendor and add the address
                //      field there.  Need to consider ups and downs of all approaches.  Also question whether or not there
                //      will be other types of addresses that need to be added.  Some products may have their own address as
                //      well depending on where shipping will come from.
                var oldAddress = this.CustomerService.GetAddressById(this.AddressId);
                var inputedAddress = AddressEditControl.Address;
                if (oldAddress != null)
                {
                    oldAddress.FirstName = inputedAddress.FirstName;
                    oldAddress.LastName = inputedAddress.LastName;
                    oldAddress.PhoneNumber = inputedAddress.PhoneNumber;
                    oldAddress.Email = inputedAddress.Email;
                    oldAddress.FaxNumber = inputedAddress.FaxNumber;
                    oldAddress.Company = inputedAddress.Company;
                    oldAddress.Address1 = inputedAddress.Address1;
                    oldAddress.Address2 = inputedAddress.Address2;
                    oldAddress.City = inputedAddress.City;
                    oldAddress.StateProvinceId = inputedAddress.StateProvinceId;
                    oldAddress.ZipPostalCode = inputedAddress.ZipPostalCode;
                    oldAddress.CountryId = inputedAddress.CountryId;
                    oldAddress.UpdatedOn = DateTime.UtcNow;

                    this.CustomerService.UpdateAddress(oldAddress);
                }
                else
                {
                    inputedAddress.CustomerId = NopContext.Current.User.CustomerId;
                    inputedAddress.IsBillingAddress = this.IsBillingAddress;
                    inputedAddress.CreatedOn = DateTime.UtcNow;
                    inputedAddress.UpdatedOn = DateTime.UtcNow;
                    this.CustomerService.InsertAddress(inputedAddress);
                }
                Response.Redirect(SEOHelper.GetMyAccountUrl());
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.CustomerService.DeleteAddress(this.AddressId);
            Response.Redirect(SEOHelper.GetMyAccountUrl());
        }

        public int AddressId
        {
            get
            {
                return CommonHelper.QueryStringInt("AddressId");
            }
        }

        public bool IsBillingAddress
        {
            get
            {
                return CommonHelper.QueryStringBool("IsBillingAddress");
            }
        }

        public bool DeleteAddress
        {
            get
            {
                return CommonHelper.QueryStringBool("Delete");
            }
        }

        public PageSslProtectionEnum SslProtected
        {
            get
            {
                return PageSslProtectionEnum.Yes;
            }
        }
    }
}