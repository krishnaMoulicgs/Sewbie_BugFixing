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
using System.Web.Services;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ManufacturerDetailsControl : BaseNopVendorAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ManufacturerId == 0)
                {
                    Response.Redirect(CommonHelper.GetVendorAdminLocation(false));
                }
                Manufacturer manufacturer = this.ManufacturerService.GetManufacturerById(this.ManufacturerId);
                if (manufacturer != null)
                {
                    lblTitle.Text = Server.HtmlEncode(manufacturer.Name);
                }
            }
        }
        
        
        protected Manufacturer Save()
        {
            Manufacturer manufacturer = ctrlManufacturerInfo.SaveInfo();
            ctrlManufacturerSEO.SaveInfo();
            ctrlSellerInfo.SaveInfo();
            
            this.CustomerActivityService.InsertActivity(
                "EditManufacturer",
                GetLocaleResourceString("ActivityLog.EditManufacturer"),
                manufacturer.Name);

            return manufacturer;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //verified.
                try
                {
                    Manufacturer manufacturer = Save();
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
                    Response.Redirect(string.Format("SellerDetails.aspx?ManufacturerID={0}", manufacturer.ManufacturerId));
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            Manufacturer manufacturer = this.ManufacturerService.GetManufacturerById(this.ManufacturerId);
            if (manufacturer != null)
            {
                PreviewButton.OnClientClick = string.Format("javascript:OpenWindow('{0}', 800, 600, true); return false;", SEOHelper.GetManufacturerUrl(manufacturer.ManufacturerId));
            }

            base.OnPreRender(e);
        }

        public String GetLblSuccessClientID()
        {
            return ctrlSellerInfo.FindControl("lblSuccess").ClientID;
        }

        public String GetlMessageClientID()
        {
            return Page.Master.FindControl("lMessage").ClientID;
        }

        public String GetlMessageCompleteClientID()
        {
            return Page.Master.FindControl("lMessageComplete").ClientID;
        }

        public String GetpnlMessageClientID()
        {
            return Page.Master.FindControl("pnlMessage").ClientID;
        }


        public int ManufacturerId
        {
            get
            {
                Manufacturer mfu = IoC.Resolve<NopCommerce.BusinessLogic.Manufacturers.IManufacturerService>().GetManufacturerByName(NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.CompanyName);
                int mfuID;
                if (mfu != null)
                {
                    mfuID = mfu.ManufacturerId;
                }
                else
                {
                    mfuID = 0;
                }

                return mfuID;
            }
        }
    }
}