using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ManufacturerSEOControl : BaseNopVendorAdministrationUserControl
    {
        private void BindData()
        {
            Manufacturer manufacturer = this.ManufacturerService.GetManufacturerById(this.ManufacturerId);

            if (this.HasLocalizableContent)
            {
                var languages = this.GetLocalizableLanguagesSupported();
                rptrLanguageTabs.DataSource = languages;
                rptrLanguageTabs.DataBind();
                rptrLanguageDivs.DataSource = languages;
                rptrLanguageDivs.DataBind();
            }
            
            if (manufacturer != null)
            {
                this.txtMetaKeywords.Text = manufacturer.MetaKeywords;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            BindJQuery();
            BindJQueryIdTabs();

            base.OnPreRender(e);
        }
        
        public void SaveInfo()
        {
            SaveInfo(this.ManufacturerId);
        }

        public void SaveInfo(int manId)
        {
            Manufacturer manufacturer = this.ManufacturerService.GetManufacturerById(manId);

            if (manufacturer != null)
            {
                manufacturer.MetaKeywords = txtMetaKeywords.Text;
                manufacturer.PageSize = 12;// txtPageSize.Value;
                this.ManufacturerService.UpdateManufacturer(manufacturer);
            }

            SaveLocalizableContent(manufacturer);
        }

        protected void SaveLocalizableContent(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                return;

            if (!this.HasLocalizableContent)
                return;

            foreach (RepeaterItem item in rptrLanguageDivs.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtLocalizedMetaKeywords = (TextBox)item.FindControl("txtLocalizedMetaKeywords");
                    var lblLanguageId = (Label)item.FindControl("lblLanguageId");

                    int languageId = int.Parse(lblLanguageId.Text);
                    string metaKeywords = txtLocalizedMetaKeywords.Text;

                    bool allFieldsAreEmpty = (string.IsNullOrEmpty(metaKeywords));

                    var content = this.ManufacturerService.GetManufacturerLocalizedByManufacturerIdAndLanguageId(manufacturer.ManufacturerId, languageId);
                    if (content == null)
                    {
                        if (!allFieldsAreEmpty && languageId > 0)
                        {
                            //only insert if one of the fields are filled out (avoid too many empty records in db...)
                            content = new ManufacturerLocalized()
                            {
                                ManufacturerId = manufacturer.ManufacturerId,
                                LanguageId = languageId,
                                MetaKeywords = metaKeywords
                            };

                            this.ManufacturerService.InsertManufacturerLocalized(content);
                        }
                    }
                    else
                    {
                        if (languageId > 0)
                        {
                            content.LanguageId = languageId;
                            content.MetaKeywords = metaKeywords;
                            this.ManufacturerService.UpdateManufacturerLocalized(content);
                        }
                    }
                }
            }
        }

        protected void rptrLanguageDivs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtLocalizedMetaKeywords = (TextBox)e.Item.FindControl("txtLocalizedMetaKeywords");
                var lblLanguageId = (Label)e.Item.FindControl("lblLanguageId");

                int languageId = int.Parse(lblLanguageId.Text);

                var content = this.ManufacturerService.GetManufacturerLocalizedByManufacturerIdAndLanguageId(this.ManufacturerId, languageId);
                if (content != null)
                {
                    txtLocalizedMetaKeywords.Text = content.MetaKeywords;
                }
            }
        }
        
        public int ManufacturerId
        {
            get
            {
                return CommonHelper.QueryStringInt("ManufacturerId");
            }
        }
    }
}