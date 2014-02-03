using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic.ExportImport;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ManufacturersControl : BaseNopVendorAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            var manufacturers = this.ManufacturerService.GetAllManufacturers();
            gvManufacturers.DataSource = manufacturers;
            gvManufacturers.DataBind();
        }

        protected void gvManufacturers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvManufacturers.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnExportXML_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var manufacturers = this.ManufacturerService.GetAllManufacturers();
                    string xml = this.ExportManager.ExportManufacturersToXml(manufacturers);
                    string fileName = string.Format("manufacturers_{0}.xml", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    CommonHelper.WriteResponseXml(xml, fileName);
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }
    }
}