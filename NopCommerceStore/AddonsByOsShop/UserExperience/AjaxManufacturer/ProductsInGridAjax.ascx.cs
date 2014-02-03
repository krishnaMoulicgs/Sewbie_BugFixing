using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AjaxManufacturer
{
    public partial class ProductsInGridAjax : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            var manufacturer = ManufacturerService.GetManufacturerById(ManufacturerId);
            List<Manufacturer> m = new List<Manufacturer>();
            m.Add(manufacturer);

            rptrCategoryBreadcrumb.DataSource = m;
            rptrCategoryBreadcrumb.DataBind();

            //lDescription.Text = category.Description;
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