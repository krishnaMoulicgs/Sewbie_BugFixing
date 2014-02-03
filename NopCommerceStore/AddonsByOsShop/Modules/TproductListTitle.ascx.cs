using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Products;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules
{
    public partial class TproductListTitle : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductTag tag = ProductService.GetProductTagById(this.ProductTagId);
            lTitle.Text = string.Format(GetLocaleResourceString("ProductTags.Title", Server.HtmlEncode(tag.Name)));
        }

        public int ProductTagId
        {
            get
            {
                return CommonHelper.QueryStringInt("tagid");
            }
        }
    }
}