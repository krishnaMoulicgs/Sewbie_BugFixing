//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class LocaleStringResourceDetailsControl : BaseNopVendorAdministrationUserControl
    {
        private void BindData()
        {
            var localeStringResource = this.LocalizationManager.GetLocaleStringResourceById(this.LocaleStringResourceId);
            if (localeStringResource != null)
            {
                Language language = this.LanguageService.GetLanguageById(localeStringResource.LanguageId);
                if (language != null)
                {
                    hlBackToResources.NavigateUrl = CommonHelper.GetStoreAdminLocation() + "LocaleStringResources.aspx?LanguageID=" + localeStringResource.LanguageId.ToString();
                }                
            }
            else
                Response.Redirect("LocaleStringResources.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected LocaleStringResource Save()
        {
            LocaleStringResource localeStringResource = ctrlLocaleStringResourceInfo.SaveInfo();
            return localeStringResource;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    LocaleStringResource localeStringResource = Save();
                    Response.Redirect("LocaleStringResources.aspx?LanguageID=" + localeStringResource.LanguageId.ToString());
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
                    LocaleStringResource localeStringResource = Save();
                    Response.Redirect("LocaleStringResourceDetails.aspx?LocaleStringResourceID=" + localeStringResource.LocaleStringResourceId.ToString());
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
                this.LocalizationManager.DeleteLocaleStringResource(this.LocaleStringResourceId);
                LocaleStringResource localeStringResource = this.LocalizationManager.GetLocaleStringResourceById(this.LocaleStringResourceId);
                if (localeStringResource != null)
                    Response.Redirect("LocaleStringResources.aspx?LanguageID=" + localeStringResource.LanguageId.ToString());
                else
                    Response.Redirect("LocaleStringResources.aspx");
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }
        }

        public int LocaleStringResourceId
        {
            get
            {
                return CommonHelper.QueryStringInt("LocaleStringResourceId");
            }
        }
    }
}