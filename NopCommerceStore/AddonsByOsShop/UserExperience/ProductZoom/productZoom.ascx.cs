using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;

namespace NopSolutions.NopCommerce.Web.Templates.Products
{
    public partial class WebUserControl1 : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        public int ProductId
        {
            get
            {
                return CommonHelper.QueryStringInt("ProductId");
            }
        }
        protected void BindData()
        {
            var product = ProductService.GetProductById(this.ProductId);
            if (product == null || product.ProductVariants.Count == 0)
            {
                Response.Redirect(CommonHelper.GetStoreLocation());
            }
           
            BindProductInfo(product);
        }

        protected void BindProductInfo(Product product)
        {
            
            //pictures
            var pictures = PictureService.GetPicturesByProductId(product.ProductId);
            if (pictures.Count > 0)
            {
                defaultImage.ImageUrl = PictureService.GetPictureUrl(pictures[0]);
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                lvProductPictures.DataSource = pictures;
                lvProductPictures.DataBind();
            }
            else if (pictures.Count == 1)
            {
                defaultImage.ImageUrl = PictureService.GetPictureUrl(pictures[0]);
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                lvProductPictures.Visible = false;
            }
            else
            {
                defaultImage.ImageUrl = PictureService.GetDefaultPictureUrl(SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 1000));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                lvProductPictures.Visible = false;
            }
            if (SettingManager.GetSettingValueBoolean("Media.Product.DefaultPictureZoomEnabled", false))
            {
                var picture = product.DefaultPicture;
                if (picture != null)
                {
                    lnkMainLightbox.Attributes["href"] = PictureService.GetPictureUrl(picture);
                    lnkMainLightbox.Attributes["rel"] = "lightbox-pd";
                }
            }
        }
        
    }
}