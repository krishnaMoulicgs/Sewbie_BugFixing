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
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ManufacturerAddControl : BaseNopVendorAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SelectTab(this.ManufacturerTabs, this.TabId);
            }
        }

        protected Manufacturer Save()
        {
            Manufacturer manufacturer = ctrlManufacturerInfo.SaveInfo();
            ctrlManufacturerSEO.SaveInfo(manufacturer.ManufacturerId);

            this.CustomerActivityService.InsertActivity(
                "AddNewManufacturer",
                GetLocaleResourceString("ActivityLog.AddNewManufacturer"),
                manufacturer.Name);

            return manufacturer;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Manufacturer manufacturer = Save();
                    Response.Redirect("manufacturers.aspx");
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
                    Manufacturer manufacturer = Save();
                    Response.Redirect(string.Format("ManufacturerDetails.aspx?ManufacturerID={0}&TabID={1}", manufacturer.ManufacturerId, this.GetActiveTabId(this.ManufacturerTabs)));
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
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

        protected string TabId
        {
            get
            {
                return CommonHelper.QueryString("TabId");
            }
        }
    }
}