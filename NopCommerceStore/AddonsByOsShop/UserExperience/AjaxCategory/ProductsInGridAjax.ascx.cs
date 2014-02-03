using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AjaxCategory
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

            var category = this.CategoryService.GetCategoryById(CategoryId);

            rptrCategoryBreadcrumb.DataSource = CategoryService.GetBreadCrumb(CategoryId);
            rptrCategoryBreadcrumb.DataBind();

            //lDescription.Text = category.Description;
        }

        public int CategoryId
        {
            get
            {
                return CommonHelper.QueryStringInt("CategoryId");
            }
        }
    }
}