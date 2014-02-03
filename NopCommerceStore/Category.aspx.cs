using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
 
namespace NopSolutions.NopCommerce.Web
{
    public partial class CategoryPage : BaseNopFrontendPage
    {
        Category category = null;

        private void CreateChildControlsTree()
        {
            category = this.CategoryService.GetCategoryById(this.CategoryId);
            if (category != null)
            {
                //Control child = null;

                CategoryTemplate categoryTemplate = category.CategoryTemplate;
                if (categoryTemplate == null)
                    throw new NopException(string.Format("Category template path can not be empty. CategoryID={0}", category.CategoryId));

                //child = base.LoadControl(categoryTemplate.TemplatePath);
                //this.CategoryPlaceHolder.Controls.Add(child);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);            
            //this.CreateChildControlsTree();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            
            cs.RegisterStartupScript(this.GetType(), 
                "jsrender", 
                @"<script src=" + "\"" + ResolveClientUrl("~/Scripts/jsrender.js") + "\" type=\"text/javascript\"></script>");
            
            cs.RegisterStartupScript(this.GetType(),
                "isotope",
                @"<script src=" + "\"" + ResolveClientUrl("~/Scripts/jquery.isotope.min.js") + "\" type=\"text/javascript\"></script>");

            cs.RegisterStartupScript(this.GetType(),
                "category",
                @"<script src=" + "\"" + ResolveClientUrl("~/Scripts/category.js") + "\" type=\"text/javascript\"></script>");

            cs.RegisterStartupScript(this.GetType(),
                "productShortList",
                @"<script src=" + "\"" + ResolveClientUrl("~/Scripts/productShortList.js") + "\" type=\"text/javascript\"></script>");
            this.hidCategoryID.Value = CategoryId.ToString();


            if (category == null || category.Deleted || !category.Published)
            {
                //Response.Redirect(CommonHelper.GetStoreLocation());

                //IF there category is blank we're going to show the what's new records.  Not Redirect.
            }
            else { 
                //title, meta
                string title = string.Empty;
                if (!string.IsNullOrEmpty(category.LocalizedMetaTitle))
                    title = category.LocalizedMetaTitle;
                else
                    title = category.LocalizedName;
                SEOHelper.RenderTitle(this, title, true);
                SEOHelper.RenderMetaTag(this, "description", category.LocalizedMetaDescription, true);
                SEOHelper.RenderMetaTag(this, "keywords", category.LocalizedMetaKeywords, true);

                //canonical URL
                if (SEOHelper.EnableUrlRewriting &&
                    this.SettingManager.GetSettingValueBoolean("SEO.CanonicalURLs.Category.Enabled"))
                {
                    if (!this.SEName.Equals(SEOHelper.GetCategorySEName(category)))
                    {
                        string canonicalUrl = SEOHelper.GetCategoryUrl(category);
                        if (this.Request.QueryString != null)
                        {
                            for (int i = 0; i < this.Request.QueryString.Count; i++)
                            {
                                string key = Request.QueryString.GetKey(i);
                                if (!String.IsNullOrEmpty(key) &&
                                    (key.ToLowerInvariant() != "categoryid") && 
                                    (key.ToLowerInvariant() != "sename"))
                                {
                                    canonicalUrl = CommonHelper.ModifyQueryString(canonicalUrl, key + "=" + Request.QueryString[i], null);
                                }
                            }
                        }

                        SEOHelper.RenderCanonicalTag(Page, canonicalUrl);
                    }
                }
            }


            if (!Page.IsPostBack)
            {
                NopContext.Current.LastContinueShoppingPage = CommonHelper.GetThisPageUrl(true);
            }
        }

        public int CategoryId
        {
            get
            {
                return CommonHelper.QueryStringInt("CategoryId");
            }
        }

        public string SEName
        {
            get
            {
                return CommonHelper.QueryString("SEName");
            }
        }

        public override PageSslProtectionEnum SslProtected
        {
            get
            {
                return PageSslProtectionEnum.No;
            }
        }
    }
}