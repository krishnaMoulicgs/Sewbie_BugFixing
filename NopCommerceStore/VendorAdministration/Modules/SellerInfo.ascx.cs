using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class SellerInfo : BaseNopVendorAdministrationUserControl {

        protected override void OnPreRender(EventArgs e)
        {
            string adminJs = CommonHelper.GetStoreLocation() + "Scripts/SellerInfo.js";
            Page.ClientScript.RegisterClientScriptInclude(adminJs, adminJs);

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string firstName = String.Empty;
                string lastName = string.Empty;

                firstName = NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.PaypalFirstName;
                lastName = NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.PaypalLastName;

                if (!NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.PaypalVerified)
                {
                    lblSuccess.Style["display"] = "none";
                    lblUnverified.Style["display"] = "block";

                    if (firstName == string.Empty)
                    {
                        firstName = NopCommerce.BusinessLogic.NopContext.Current.User.FirstName;
                    }

                    if (lastName == String.Empty)
                    {
                        lastName = NopCommerce.BusinessLogic.NopContext.Current.User.LastName;
                    }
                }
                else
                {
                    lblUnverified.Style["display"] = "none";
                    lblSuccess.Style["display"] = "block";
                }

                txtFirstName.Text = firstName;
                txtLastName.Text = lastName;
                txtPaypalEmailAddress.Text = NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.PaypalEmailAddress;

                hidPaypalVerified.Value = NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.PaypalVerified.ToString().ToLower();
            }
        }


        public void SaveInfo(){
        
           Vendor vendor = NopContext.Current.User.Vendor;

           vendor.PaypalFirstName = txtFirstName.Text.Trim();
           vendor.PaypalLastName = txtLastName.Text.Trim();
           vendor.PaypalEmailAddress = txtPaypalEmailAddress.Text.Trim();
           vendor.PaypalVerified = hidPaypalVerified.Value.ToLower() == "true" ? true : false;

           IoC.Resolve<IVendorService>().UpdateVendor(vendor);

           ShowVerifiedStatus(hidPaypalVerified.Value.ToLower() == "true");
        }

        protected void ShowVerifiedStatus(bool bSetVerified)
        {
            if (bSetVerified)
            {
                lblUnverified.Style["display"] = "none";
                lblSuccess.Style["display"] = "block";
            }
            else
            {
                lblUnverified.Style["display"] = "block";
                lblSuccess.Style["display"] = "none";
            }
        }

    }
}