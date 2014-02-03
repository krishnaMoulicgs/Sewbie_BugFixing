using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.Common.Utils;
using System.Collections.Generic;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class AddressDetailsControl : BaseNopVendorAdministrationUserControl
    {
        protected void BindData()
        {
            Address address = this.CustomerService.GetAddressById(this.AddressId);
            if (address != null)
            {
                Customer customer = address.Customer;
                if (customer != null)
                {
                    this.lblCustomer.Text = Server.HtmlEncode(customer.Email);
                    lnkBack.NavigateUrl = CommonHelper.GetStoreAdminLocation() + "CustomerDetails.aspx?CustomerID=" + customer.CustomerId.ToString();
                }
                else
                    Response.Redirect("Customers.aspx");

                this.FillCountryDropDowns(address);

                this.txtFirstName.Text = address.FirstName;
                this.txtLastName.Text = address.LastName;
                this.txtPhoneNumber.Text = address.PhoneNumber;
                this.txtEmail.Text = address.Email;
                this.txtFaxNumber.Text = address.FaxNumber;
                this.txtCompany.Text = address.Company;
                this.txtAddress1.Text = address.Address1;
                this.txtAddress2.Text = address.Address2;
                this.txtCity.Text = address.City;
                CommonHelper.SelectListItem(this.ddlCountry, address.CountryId);
                FillStateProvinceDropDowns();
                CommonHelper.SelectListItem(this.ddlStateProvince, address.StateProvinceId);
                this.txtZipPostalCode.Text = address.ZipPostalCode;
            }
            else
                Response.Redirect("Customers.aspx");
        }

        protected void FillCountryDropDowns(Address address)
        {
            this.ddlCountry.Items.Clear();
            List<Country> countryCollection = null;
            if (address.IsBillingAddress)
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
                this.BindData();
            }
        }

        protected Address Save()
        {
            var address = this.CustomerService.GetAddressById(this.AddressId);

            address.FirstName = txtFirstName.Text;
            address.LastName = txtLastName.Text;
            address.PhoneNumber = txtPhoneNumber.Text;
            address.Email = txtEmail.Text;
            address.FaxNumber = txtFaxNumber.Text;
            address.Company = txtCompany.Text;
            address.Address1 = txtAddress1.Text;
            address.Address2 = txtAddress2.Text;
            address.City = txtCity.Text;
            address.StateProvinceId = int.Parse(this.ddlStateProvince.SelectedItem.Value);
            address.ZipPostalCode = txtZipPostalCode.Text;
            address.CountryId = int.Parse(this.ddlCountry.SelectedItem.Value);
            address.UpdatedOn = DateTime.UtcNow;
            this.CustomerService.UpdateAddress(address);

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

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Address address = this.CustomerService.GetAddressById(this.AddressId);
                this.CustomerService.DeleteAddress(this.AddressId);
                if (address != null)
                    Response.Redirect("CustomerDetails.aspx?CustomerID=" + address.CustomerId.ToString());
                else
                    Response.Redirect("Customers.aspx");
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateProvinceDropDowns();
        }

        public int AddressId
        {
            get
            {
                return CommonHelper.QueryStringInt("AddressId");
            }
        }
    }
}