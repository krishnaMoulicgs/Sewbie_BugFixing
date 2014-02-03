using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic;

namespace NopSolutions.NopCommerce.Web
{
    public partial class ShippingAddressSelect : BaseNopFrontendPage //BaseNopVendorAdministrationPage
    {
        bool IsBillings = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            IsBillings = Convert.ToBoolean(Request.QueryString["IsBilling"].ToString());
            if (!Page.IsPostBack)
            {
                if (IsBillings)
                    BindBillingData();
                else
                    BindGrid();
            }
        }
        protected void gvShippingAddressDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShippingAddressDetails.PageIndex = e.NewPageIndex;
            if (IsBillings)
                BindBillingData();
            else
                BindGrid();

        }
        private void BindGrid()
        {
            var addresses = GetAllowedShippingAddresses(NopContext.Current.User);
            if (addresses.Count > 0)
            {
                gvShippingAddressDetails.DataSource = addresses;
                gvShippingAddressDetails.DataBind();
            }
            else
            {
                gvShippingAddressDetails.DataSource = null;
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
                    addresses.Add(address);
                }
            }

            return addresses;
        }


        public void BindBillingData()
        {


            var addresses = GetAllowedBillingAddresses(NopContext.Current.User);

            if (addresses.Count > 0)
            {
                //bind data
                gvShippingAddressDetails.DataSource = addresses;
                gvShippingAddressDetails.DataBind();
            }
            else
            {
                gvShippingAddressDetails.DataSource = null; ;
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

                    addresses.Add(address);


                }
            }

            return addresses;
        }

        protected void gvShippingAddressDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                
            }

        }

        protected void gvShippingAddressDetails_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvShippingAddressDetails_DataBound(object sender, EventArgs e)
        {

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            // GridView gv = (GridView)sender;

            //string AddressId = ((NopSolutions.NopCommerce.BusinessLogic.CustomerManagement.Address)(.Row.DataItem)).AddressId.ToString();
        }

        protected void btnSelect_Command(object sender, CommandEventArgs e)
        {

        }

        protected void gvShippingAddressDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            var hdnAddressIds = gvr.FindControl("hdnAddressId") as HiddenField;
            if (!string.IsNullOrEmpty(hdnAddressIds.Value))
            {
                if (IsBillings)
                {
                    NopContext.Current.User.BillingAddressId = Convert.ToInt16(hdnAddressIds.Value);
                    this.CustomerService.UpdateCustomer(NopContext.Current.User);
                }
                else
                {
                    NopContext.Current.User.ShippingAddressId = Convert.ToInt16(hdnAddressIds.Value);
                    this.CustomerService.UpdateCustomer(NopContext.Current.User);


                }
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");

            }
        }
    }
}