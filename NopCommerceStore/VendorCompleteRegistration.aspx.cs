using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
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
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Common.Xml;
using NopSolutions.NopCommerce.Controls;

namespace NopSolutions.NopCommerce.Web
{
    public partial class VendorCompleteRegistration : BaseNopFrontendPage
    {
        private void ApplyLocalization()
        {
            //var EmailRequired = CreateUserWizardStep1.ContentTemplateContainer.FindControl("EmailRequired") as RequiredFieldValidator;
            //if (EmailRequired != null)
            //{
            //    if (this.CustomerService.UsernamesEnabled)
            //    {
            //        EmailRequired.ErrorMessage = GetLocaleResourceString("Account.E-MailRequired");
            //        EmailRequired.ToolTip = GetLocaleResourceString("Account.E-MailRequired");
            //    }
            //    else
            //    {
            //        //EmailRequired is not enabled
            //    }
            //}

            //var revEmail = CreateUserWizardStep1.ContentTemplateContainer.FindControl("revEmail") as RegularExpressionValidator;
            //if (revEmail != null)
            //{
            //    if (this.CustomerService.UsernamesEnabled)
            //    {
            //        revEmail.ErrorMessage = GetLocaleResourceString("Account.InvalidEmail");
            //        revEmail.ToolTip = GetLocaleResourceString("Account.InvalidEmail");
            //    }
            //    else
            //    {
            //        //revEmail is not enabled
            //    }
            //}


            //var lUsernameOrEmail = CreateUserWizardStep1.ContentTemplateContainer.FindControl("lUsernameOrEmail") as Literal;
            //if (lUsernameOrEmail != null)
            //{
            //    if (this.CustomerService.UsernamesEnabled)
            //    {
            //        lUsernameOrEmail.Text = GetLocaleResourceString("Account.Username");
            //    }
            //    else
            //    {
            //        lUsernameOrEmail.Text = GetLocaleResourceString("Account.E-Mail");
            //    }
            //}

            //var UserNameOrEmailRequired = CreateUserWizardStep1.ContentTemplateContainer.FindControl("UserNameOrEmailRequired") as RequiredFieldValidator;
            //if (UserNameOrEmailRequired != null)
            //{
            //    if (this.CustomerService.UsernamesEnabled)
            //    {
            //        UserNameOrEmailRequired.ErrorMessage = GetLocaleResourceString("Account.UserNameRequired");
            //        UserNameOrEmailRequired.ToolTip = GetLocaleResourceString("Account.UserNameRequired");
            //    }
            //    else
            //    {
            //        UserNameOrEmailRequired.ErrorMessage = GetLocaleResourceString("Account.E-MailRequired");
            //        UserNameOrEmailRequired.ToolTip = GetLocaleResourceString("Account.E-MailRequired");
            //    }
            //}

            //var refUserNameOrEmail = CreateUserWizardStep1.ContentTemplateContainer.FindControl("refUserNameOrEmail") as RegularExpressionValidator;
            //if (refUserNameOrEmail != null)
            //{
            //    if (this.CustomerService.UsernamesEnabled)
            //    {
            //        //refUserNameOrEmail is not enabled
            //    }
            //    else
            //    {
            //        refUserNameOrEmail.ErrorMessage = GetLocaleResourceString("Account.InvalidEmail");
            //        refUserNameOrEmail.ToolTip = GetLocaleResourceString("Account.InvalidEmail");
            //    }
            //}

            //var lblCompleteStep = CompleteWizardStep1.ContentTemplateContainer.FindControl("lblCompleteStep") as Label;
            //if (lblCompleteStep != null)
            //{
            //    switch (this.CustomerService.CustomerRegistrationType)
            //    {
            //        case CustomerRegistrationTypeEnum.Standard:
            //            {
            //                lblCompleteStep.Text = GetLocaleResourceString("Account.RegistrationCompleted");
            //            }
            //            break;
            //        case CustomerRegistrationTypeEnum.EmailValidation:
            //            {
            //                lblCompleteStep.Text = GetLocaleResourceString("Account.ActivationEmailHasBeenSent");
            //            }
            //            break;
            //        case CustomerRegistrationTypeEnum.AdminApproval:
            //            {
            //                lblCompleteStep.Text = GetLocaleResourceString("Account.AdminApprovalRequired");
            //            }
            //            break;
            //        case CustomerRegistrationTypeEnum.Disabled:
            //            {
            //                lblCompleteStep.Text = "Registration method error";
            //            }
            //            break;
            //        default:
            //            {
            //                lblCompleteStep.Text = "Registration method error";
            //            }
            //            break;
            //    }
            //}


            //vendor registration
            var VendorEmailRequired = this.FindControl("VendorEmailRequired") as RequiredFieldValidator;
            if (VendorEmailRequired != null)
            {
                if (this.CustomerService.UsernamesEnabled)
                {
                    VendorEmailRequired.ErrorMessage = GetLocaleResourceString("Account.E-MailRequired");
                    VendorEmailRequired.ToolTip = GetLocaleResourceString("Account.E-MailRequired");
                }
                else
                {
                    //EmailRequired is not enabled
                }
            }

            var revVendorEmail = this.FindControl("revEmail") as RegularExpressionValidator;
            if (revVendorEmail != null)
            {
                if (this.CustomerService.UsernamesEnabled)
                {
                    revVendorEmail.ErrorMessage = GetLocaleResourceString("Account.InvalidEmail");
                    revVendorEmail.ToolTip = GetLocaleResourceString("Account.InvalidEmail");
                }
                else
                {
                    //revEmail is not enabled
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ApplyLocalization();

            if (this.CustomerService.CustomerRegistrationType == CustomerRegistrationTypeEnum.Disabled)
            {
                //CreateUserForm.Visible = false;
                //topicRegistrationNotAllowed.Visible = true;
            }
            else
            {
                //CreateUserForm.Visible = true;
                //topicRegistrationNotAllowed.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                if (NopContext.Current.User != null && !NopContext.Current.User.IsGuest)
                {
                    this.CustomerService.Logout();
                    Response.Redirect("~/register.aspx");
                }

                #region Username/emails hack
                //var pnlEmail;// = CreateUserWizardStep1.ContentTemplateContainer.FindControl("pnlEmail") as HtmlTableRow;
                //if (pnlEmail != null)
                //{
                //    pnlEmail.Visible = this.CustomerService.UsernamesEnabled;
                //}
                //var refUserNameOrEmail = CreateUserWizardStep1.ContentTemplateContainer.FindControl("refUserNameOrEmail") as RegularExpressionValidator;
                //if (refUserNameOrEmail != null)
                //{
                //    refUserNameOrEmail.Enabled = !this.CustomerService.UsernamesEnabled;
                //}
                #endregion

                this.FillCountryDropDowns();
                this.FillStateProvinceDropDowns();
                this.FillTimeZones();
                this.DataBind();
            }

            //var CaptchaCtrl = CreateUserWizardStep1.ContentTemplateContainer.FindControl("CaptchaCtrl") as CaptchaControl;
            //if (CaptchaCtrl != null)
            //{
            //    CaptchaCtrl.Visible = this.SettingManager.GetSettingValueBoolean("Common.RegisterCaptchaImageEnabled");
            //}

            string title = GetLocaleResourceString("PageTitle.RegisterVendor");
            SEOHelper.RenderTitle(this, title, true);

            //vendor application

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

        public override PageSslProtectionEnum SslProtected
        {
            get
            {
                return PageSslProtectionEnum.Yes;
            }
        }

        public override bool AllowGuestNavigation
        {
            get
            {
                return true;
            }
        }

        private void FillCountryDropDowns()
        {
            //var ddlCountry = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlCountry");
            //ddlCountry.Items.Clear();
            //var countryCollection = this.CountryService.GetAllCountriesForRegistration();
            //foreach (var country in countryCollection)
            //{
            //    var ddlCountryItem2 = new ListItem(country.Name, country.CountryId.ToString());
            //    ddlCountry.Items.Add(ddlCountryItem2);
            //}
        }

        private void FillStateProvinceDropDowns()
        {
            //var ddlCountry = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlCountry");
            //var ddlStateProvince = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlStateProvince");
            //ddlStateProvince.Items.Clear();
            //int countryId = 0;
            //if (ddlCountry.SelectedItem != null)
            //    countryId = int.Parse(ddlCountry.SelectedItem.Value);

            //var stateProvinceCollection = this.StateProvinceService.GetStateProvincesByCountryId(countryId);
            //foreach (var stateProvince in stateProvinceCollection)
            //{
            //    var ddlStateProviceItem2 = new ListItem(stateProvince.Name, stateProvince.StateProvinceId.ToString());
            //    ddlStateProvince.Items.Add(ddlStateProviceItem2);
            //}
            //if (stateProvinceCollection.Count == 0)
            //{
            //    var ddlStateProvinceItem = new ListItem(GetLocaleResourceString("Address.StateProvinceNonUS"), "0");
            //    ddlStateProvince.Items.Add(ddlStateProvinceItem);
            //}
        }

        private void FillTimeZones()
        {
            if (DateTimeHelper.AllowCustomersToSetTimeZone && this.CustomerService.FormFieldTimeZoneEnabled)
            {
                //var ddlTimeZone = (DropDownList)CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlTimeZone");

                //ddlTimeZone.Items.Clear();
                //var timeZones = DateTimeHelper.GetSystemTimeZones();
                //foreach (var timeZone in timeZones)
                //{
                //    string timeZoneName = timeZone.DisplayName;
                //    var ddlTimeZoneItem2 = new ListItem(timeZoneName, timeZone.Id);
                //    ddlTimeZone.Items.Add(ddlTimeZoneItem2);
                //}
                //CommonHelper.SelectListItem(ddlTimeZone, DateTimeHelper.CurrentTimeZone.Id);
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateProvinceDropDowns();
        }

    }
}