using System;
using System.Collections;
using System.ComponentModel;
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
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Content.Forums;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Profile;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Common.Xml;
using NopSolutions.NopCommerce.Controls;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;


namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class VendorRegisterControl : BaseNopFrontendUserControl
    {
        private void ApplyLocalization()
        {
            var EmailRequired = this.FindControl("EmailRequired") as RequiredFieldValidator;
            if (EmailRequired != null)
            {
                if (this.CustomerService.UsernamesEnabled)
                {
                    EmailRequired.ErrorMessage = GetLocaleResourceString("Account.E-MailRequired");
                    EmailRequired.ToolTip = GetLocaleResourceString("Account.E-MailRequired");
                }
                else
                {
                    //EmailRequired is not enabled
                }
            }

            var revEmail = this.FindControl("revEmail") as RegularExpressionValidator;
            if (revEmail != null)
            {
                if (this.CustomerService.UsernamesEnabled)
                {
                    revEmail.ErrorMessage = GetLocaleResourceString("Account.InvalidEmail");
                    revEmail.ToolTip = GetLocaleResourceString("Account.InvalidEmail");
                }
                else
                {
                    //revEmail is not enabled
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            //form fields
            

            //user the customer service here to determine whether fields should be
            //enabled or disabled.

            base.OnInit(e);
        }

        public void CreatedVendor(object sender, EventArgs e)
        {
            
            
        }

        public void CreatingVendor(object sender, LoginCancelEventArgs e)
        {
            
        }

        protected void CreateVendorError(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ApplyLocalization();
            
            if (!Page.IsPostBack)
            {
                //only allow registration of a vendor if the current user exists
                //the user is not a guest
                //and the user is not already a vendor.
                if (NopContext.Current.User == null ||
                        NopContext.Current.User.IsGuest)
                {
                    this.CustomerService.Logout();
                    Response.Redirect("~/register.aspx");
                }
                else
                { //user exists but need to check if user is already
                  //a vendor.
                    if (NopContext.Current.User.IsVendor)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    
                }
                                
                this.DataBind();
            }
        }

        protected void StepNextButton_PreRender(object sender, EventArgs e)
        {
            //default button
            this.Page.Form.DefaultButton = (sender as Button).UniqueID;
        }

        protected override void OnPreRender(EventArgs e)
        {
            //string adminJs = CommonHelper.GetStoreLocation() + "Scripts/vendorRegister.js";
            //Page.ClientScript.RegisterClientScriptInclude(adminJs, adminJs);
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                this.VendorService.AddVendor(NopContext.Current.User.CustomerId, Email.Text, CompanyName.Text, FirstName.Text, LastName.Text, (hidPaypalVerified.Value != String.Empty));
                Response.Redirect("~/default.aspx");
            }
            else { }
        }

        protected void ValidatePaypalAccount(object source, ServerValidateEventArgs args)
        {
            NopSolutions.NopCommerce.Web.Services.PaypalAdaptiveAccount.PaypalAdaptiveAccountService svc = new Services.PaypalAdaptiveAccount.PaypalAdaptiveAccountService();
            string resp = svc.VerifyPaypalEmail(Email.Text, FirstName.Text, LastName.Text);

            if (resp == "true")
            {
                args.IsValid = true;
                hidPaypalVerified.Value = "1";
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ValidateSellerName(object source, ServerValidateEventArgs args)
        {
            NopCommerce.BusinessLogic.Manufacturers.Manufacturer seller =
                   IoC.Resolve<NopCommerce.BusinessLogic.Manufacturers.IManufacturerService>().GetManufacturerByName(CompanyName.Text);

            if (seller != null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

    }
}