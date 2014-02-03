using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class AddressAddControl : BaseNopVendorAdministrationUserControl
    {
        protected void BindData()
        {
            Customer customer = this.CustomerService.GetCustomerById(this.CustomerId);
            if (customer != null)
            {
                this.lblCustomer.Text = Server.HtmlEncode(customer.Email);
                lnkBack.NavigateUrl = CommonHelper.GetStoreAdminLocation() + "CustomerDetails.aspx?CustomerID=" + customer.CustomerId.ToString();
            }
            else
                Response.Redirect("Customers.aspx");
        }

        protected void FillCountryDropDowns()
        {
            this.ddlCountry.Items.Clear();
            List<Country> countryCollection = null;
            if (IsBillingAddress)
                countryCollection = this.CountryService.GetAllCountriesForBilling();
            else
                countryCollection = this.CountryService.GetAllCountriesForShipping();
            foreach (Country country in countryCollection)
            {
                ListItem ddlCountryItem2 = new ListItem(country.Name, country.CountryId.ToString());
                this.ddlCountry.Items.Add(ddlCountryItem2);
            }
        }

        protected void FillStateProvinceDropDowns()
        {
            this.ddlStateProvince.Items.Clear();
            int countryId = int.Parse(this.ddlCountry.SelectedItem.Value);

            var stateProvinceCollection = this.StateProvinceService.GetStateProvincesByCountryId(countryId);
            foreach (StateProvince stateProvince in stateProvinceCollection)
            {
                ListItem ddlStateProviceItem2 = new ListItem(stateProvince.Name, stateProvince.StateProvinceId.ToString());
                this.ddlStateProvince.Items.Add(ddlStateProviceItem2);
            }
            if (stateProvinceCollection.Count == 0)
            {
                ListItem ddlStateProvinceItem = new ListItem(GetLocaleResourceString("VendorAdmin.Common.State.Other"), "0");
                this.ddlStateProvince.Items.Add(ddlStateProvinceItem);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.FillCountryDropDowns();
                this.FillStateProvinceDropDowns();
                this.BindData();
            }
        }

        protected Address Save()
        {
            Address address = new Address()
            {
                CustomerId = this.CustomerId,
                IsBillingAddress = this.IsBillingAddress,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Email = txtEmail.Text,
                FaxNumber = txtFaxNumber.Text,
                Company = txtCompany.Text,
                Address1 = txtAddress1.Text,
                Address2 = txtAddress2.Text,
                City = txtCity.Text,
                StateProvinceId = int.Parse(this.ddlStateProvince.SelectedItem.Value),
                ZipPostalCode = txtZipPostalCode.Text,
                CountryId = int.Parse(this.ddlCountry.SelectedItem.Value),
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            this.CustomerService.InsertAddress(address);

            return address;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Address address = Save();
                    Response.Redirect(string.Format("CustomerDetails.aspx?CustomerID={0}", address.CustomerId));
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void SaveAndStayButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Address address = Save();
                    Response.Redirect("AddressDetails.aspx?AddressID=" + address.AddressId.ToString());
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateProvinceDropDowns();
        }

        public int CustomerId
        {
            get
            {
                return CommonHelper.QueryStringInt("CustomerId");
            }
        }

        public bool IsBillingAddress
        {
            get
            {
                return CommonHelper.QueryStringBool("IsBillingAddress");
            }
        }
    }
}