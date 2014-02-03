using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Directory;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules
{
    public partial class CproductList : BaseNopUserControl
    {
        string ProMessage = "";
        #region Page_Load()&BindData()
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            lDescription.Text = CategoryService.GetCategoryById(CategoryId).LocalizedDescription;

            #region [filter]

            #region [properties]
            decimal? minPriceConverted = urlMinPrice;
            decimal? maxPriceConverted = urlMaxPrice;

            List<Product> products = new List<Product>();
            List<Product> filterResult = new List<Product>();
            List<int> Ids = new List<int>();
            #endregion

            #region [CategoryFilter]
            if (this.SubcategoryIdsSelected.Count > 0)
            {
                foreach (int Id in SubcategoryIdsSelected)
                {
                    int totalRecords;
                    products.AddRange(ProductService.GetAllProducts(Id, 0, 0, 0, false, minPriceConverted - 1, maxPriceConverted+1, 1000, 0, null, out totalRecords));
                }
            }
            else
            {
                foreach (int Id in getSubcategoryIds)
                {
                    int totalRecords;
                    products.AddRange(ProductService.GetAllProducts(Id, 0, 0, 0, false, minPriceConverted-1, maxPriceConverted+1, 1000, 0, null, out totalRecords));
                }
            }
            int tr;
            products.AddRange(ProductService.GetAllProducts(CategoryId, 0, 0, 0, false, minPriceConverted - 1, maxPriceConverted + 1, 1000, 0, null, out tr));
            #endregion

            foreach (Product p in products)
            {
                //there are three filters which are Manufacturer filter, Product Attribute filter and Product Specification filter
                //product marked with pmOut=false && paOut = false && psOut = false will be added in the filterResult and foward into order and pager pharse.
                bool pmOut = true;
                bool paOut = true;
                bool psOut = true;

                #region [ManufacturerFilter]
                //Manufacturer selected
                if (ManufacturerIdsSelected.Count > 0)
                {
                    foreach (ProductManufacturer pm in p.ProductManufacturers)
                    {
                        if (ManufacturerIdsSelected.Contains(pm.ManufacturerId))
                        {
                            pmOut = false;
                        }
                    }
                }
                //Manufacturer not selected
                else
                {
                    pmOut = false;
                }
                #endregion

                #region [AttributeFilter]
                //Attribute selected
                if (AttrSelected.Count > 0)
                {
                    foreach (ProductVariant pv in p.ProductVariants)
                    {
                        List<ProductVariantAttribute> pvas = ProductAttributeService.GetProductVariantAttributesByProductVariantId(pv.ProductVariantId);
                        foreach (ProductVariantAttribute pva in pvas)
                        {
                            List<ProductVariantAttributeValue> pvavs = ProductAttributeService.GetProductVariantAttributeValues(pva.ProductVariantAttributeId);
                            foreach (ProductVariantAttributeValue pvav in pvavs)
                            {
                                if (AttrSelected.Contains(pvav.Name))
                                {
                                    paOut = false;
                                    break;
                                }
                                else
                                {
                                    paOut = true;
                                }
                            }
                            if (!paOut)
                            {
                                break;
                            }
                        }
                        if (!paOut)
                        {
                            break;
                        }
                    }
                }
                //Attribute not selected
                else
                {
                    paOut = false;
                }
                #endregion 

                #region [SpecificationFilter]
                if (SpecSelected.Count > 0)
                {
                    foreach (ProductSpecificationAttribute psa in p.NpProductSpecificationAttributes)
                    {
                        if (SpecSelected.Contains(psa.SpecificationAttributeOptionId.ToString()))
                        {
                            psOut = false;
                            break;
                        }
                    }
                }
                else
                {
                    psOut = false;
                }

                if (!pmOut && !paOut && !psOut)
                {
                    if (!filterResult.Contains(p))
                    {
                        filterResult.Add(p);
                        pmOut = true;
                        paOut = true;
                        psOut = true;
                    }
                }
                #endregion
            }
            #endregion

            #region [order]
            switch (order)
            {
                case "new": filterResult.Sort(CompareByNew);
                    break;
                case "lth": filterResult.Sort(CompareByLowToHigh);
                    break;
                case "htl": filterResult.Sort(CompareByHighToLow);
                    break;
            }
            #endregion

            #region [pager]
            if (filterResult.Count > 0)
            {
                List<Product> showList = getPageData(filterResult, pageIndex, pageSize);
                string pageStr = " ";
                int productCount = filterResult.Count;
                int pageCount = this.pageCount(productCount);
                if (pageCount < 1)
                {
                    pageCount = 1;
                }
                //if (showList.Count != productCount)
                //{
                //    pageStr += "<a href ='javascript:;' onclick='AjaxClient.OnPageSizeClick(" + productCount.ToString() + ")'>" + GetLocaleResourceString("OsShop.ViewAll") + " " + productCount.ToString() + " " + GetLocaleResourceString("OsShop.Styles") + "</a>";
                //}
                //else
                //{
                //    pageStr += "<a href ='javascript:;' onclick='AjaxClient.OnPageSizeClick(" + this.getStartedPageSize().ToString() + ")'>" + GetLocaleResourceString("OsShop.View") + " " + getStartedPageSize().ToString() + "/" + GetLocaleResourceString("OsShop.page") + " " +productCount.ToString() + GetLocaleResourceString("OsShop.Styles") + "</a>";
                //}
                //pageStr += "<a href ='javascript:;' onclick='AjaxClient.OnPageSizeClick(" + this.getStartedPageSize().ToString() + ")'>" + GetLocaleResourceString("OsShop.View") + " " + getStartedPageSize().ToString() + "/" + GetLocaleResourceString("OsShop.page") + " " + productCount.ToString() + GetLocaleResourceString("OsShop.Styles") + "</a>";
                //Alex Garcia 8/16/2011  Change to "Page # of #"
                pageStr += "Page " + pageIndex.ToString() + " of " + pageCount.ToString() + " ";
                if (pageCount > 1 || pageIndex > 1)
                {
                    if (pageIndex - 1 >= 1)
                    {
                        pageStr += "<a href ='javascript:;' class='pager_a' onclick='AjaxClient.OnPageIndexClick(" + (pageIndex - 1).ToString() + ")'>«&nbsp;" + GetLocaleResourceString("OsShop.Previous") + "</a>";
                    }
                    else
                    {
                        if (pageIndex != 1)
                            pageStr += "<a href ='javascript:;'class='pager_a' onclick='AjaxClient.OnPageIndexClick(1)'>«&nbsp;" + GetLocaleResourceString("OsShop.Previous") + "</a>";
                    }

                }
                int pageStart = pageIndex - 3 > 1 ? pageIndex - 3 : 1;
                for (int i = 0; i < 5; i++)
                {
                    if (pageIndex == pageStart)
                    {
                        pageStr += "<a href ='javascript:;' class='pager_a_select' onclick='AjaxClient.OnPageIndexClick(" + pageStart + ")'>" + pageStart.ToString() + "</a>";
                    }
                    else
                    {
                        pageStr += "<a href ='javascript:;' class='pager_a' onclick='AjaxClient.OnPageIndexClick(" + pageStart + ")'>" + pageStart.ToString() + "</a>";
                    }

                    pageStart++;
                    if (pageStart > pageCount)
                    {
                        break;
                    }
                }
                if (pageIndex + 1 < pageCount)
                {
                    pageStr += "<a  href ='javascript:;' class='pager_a'  onclick='AjaxClient.OnPageIndexClick(" + (pageIndex + 1).ToString() + ")'>" + GetLocaleResourceString("OsShop.Next") + "&nbsp;»</a>";
                }
                else
                {
                    if (pageIndex != pageCount)
                        pageStr += "<a  href ='javascript:;' class='pager_a'  onclick='AjaxClient.OnPageIndexClick(" + pageCount.ToString() + ")'>" + GetLocaleResourceString("OsShop.Next") + "&nbsp;»</a>";
                }

                pageContainerTop.InnerHtml = pageStr;
                pageContainerBottom.InnerHtml = pageStr;
                this.dlProducts.DataSource = showList;
                this.dlProducts.DataBind();
            }
            else
            {
                dlProducts.Visible = false;
                pageContainerTop.InnerHtml = GetLocaleResourceString("OsShop.NoPage");
                pageContainerBottom.InnerHtml = GetLocaleResourceString("OsShop.NoPage");
            }
            #endregion
        }
        #endregion

        #region [order parameters]
        public int CompareByNew(Product a, Product b)
        {
            if (a.CreatedOn > b.CreatedOn)
            {
                return -1;
            }
            if (a.CreatedOn == b.CreatedOn)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int CompareByHighToLow(Product a, Product b)
        {
            if (a.ProductVariants.Count > 0 && b.ProductVariants.Count > 0)
            {
                if (a.ProductVariants[0].Price > b.ProductVariants[0].Price)
                {
                    return -1;
                }
                else
                {
                    if (a.ProductVariants[0].Price == b.ProductVariants[0].Price)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (a.ProductVariants.Count > 0)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int CompareByLowToHigh(Product a, Product b)
        {
            if (a.ProductVariants.Count > 0 && b.ProductVariants.Count > 0)
            {
                if (a.ProductVariants[0].Price > b.ProductVariants[0].Price)
                {
                    return 1;
                }
                else
                {
                    if (a.ProductVariants[0].Price == b.ProductVariants[0].Price)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else
            {
                if (a.ProductVariants.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        #endregion

        #region [Datalist databind]
        protected void dlProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var product = e.Item.DataItem as Product;
                string productUrl = SEOHelper.GetProductUrl(product);

                var hlProductImage = e.Item.FindControl("hlProductImage") as HyperLink;
                if (hlProductImage != null)
                {
                    var picture = product.DefaultPicture;
                    if (picture != null)
                    {
                        hlProductImage.ImageUrl = PictureService.GetPictureUrl(product.DefaultPicture.PictureId, SettingManager.GetSettingValueInteger("Media.Product.ThumbnailImageSize", 125), true);
                        hlProductImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageLinkTitleFormat"), product.LocalizedName);
                        hlProductImage.Text = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                    }
                    else
                    {
                        hlProductImage.ImageUrl = PictureService.GetDefaultPictureUrl(230);
                        hlProductImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageLinkTitleFormat"), product.LocalizedName);
                        hlProductImage.Text = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                    }
                    hlProductImage.NavigateUrl = productUrl;
                }

                var hlProductName = e.Item.FindControl("hlProductName") as HyperLink;
                if (hlProductName != null)
                {
                    hlProductName.NavigateUrl = productUrl;
                    hlProductName.Text = Server.HtmlEncode(product.Name);
                }

                var lblPrice = e.Item.FindControl("lblPrice") as Label;
                if (lblPrice != null)
                {
                    decimal price = CurrencyService.ConvertCurrency(product.ProductVariants[0].Price, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                    lblPrice.Text = PriceHelper.FormatPrice(price);
                }

                ProMessage += product.ProductId;
                ProMessage += "|";

            }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            productIdList = ProMessage;
            base.OnPreRender(e);
        }

        string productIdListMess = "";
        public string productIdList
        {
            get
            {
                return productIdListMess;
            }
            set
            {
                productIdListMess = value;
            }

        }




        #region [properties]
        public int CategoryId
        {
            get 
            {
                return CommonHelper.QueryStringInt("CategoryId");
            }
        }

        public int ManufacturerId
        {
            get
            {
                return CommonHelper.QueryStringInt("ManufacturerId");
            }
        }

        //get Subcategories of current Product Category
        public List<int> getSubcategoryIds
        {
            get
            {
                List<int> Ids = new List<int>();
                List<Category> categories = CategoryService.GetAllCategoriesByParentCategoryId(this.CategoryId);

                foreach (Category c in categories)
                {
                    Ids.Add(c.CategoryId);
                }

                return Ids;
            }
        }

        public List<int> SubcategoryIdsSelected
        {
            get
            {
                List<int> list = new List<int>();
                string urlProductSubcategories = CommonHelper.QueryString("urlSubcategoriesSelected");

                string[] Ids = urlProductSubcategories.Split('|');
                foreach (string strId in Ids)
                {
                    int result;
                    Int32.TryParse(strId, out result);
                    if (result != 0)
                    {
                        list.Add(result);
                    }
                }
                return list;
            }
        }

        public List<int> ManufacturerIdsSelected
        {
            get
            {
                List<int> list = new List<int>();
                string urlManufacturersSelected = CommonHelper.QueryString("urlManufacturersSelected");

                string[] Ids = urlManufacturersSelected.Split('|');
                foreach (string strId in Ids)
                {
                    int result;
                    Int32.TryParse(strId, out result);
                    if (result != 0)
                    {
                        list.Add(result);
                    }
                }

                return list;
            }
        }

        public decimal urlMinPrice
        {
            get
            {
                if (String.IsNullOrEmpty(CommonHelper.QueryString("urlPriceRange")))
                {
                    return 0;
                }
                else
                {
                    decimal value;
                    Decimal.TryParse(CommonHelper.QueryString("urlPriceRange").Split('|')[0], out value);
                    value = CurrencyService.ConvertCurrency(value, NopContext.Current.WorkingCurrency, CurrencyService.PrimaryStoreCurrency);
                    return value;
                }
            }
        }

        public decimal urlMaxPrice
        {
            get
            {
                if (String.IsNullOrEmpty(CommonHelper.QueryString("urlPriceRange")))
                {
                    return 1000000;
                }
                else
                {
                    decimal value;
                    Decimal.TryParse(CommonHelper.QueryString("urlPriceRange").Split('|')[1], out value);
                    value = CurrencyService.ConvertCurrency(value, NopContext.Current.WorkingCurrency, CurrencyService.PrimaryStoreCurrency);
                    return value;
                }
            }
        }

        public string order
        {
            get
            {
                return CommonHelper.QueryString("urlSort");
            }
        }

        public int pageIndex
        {
            get
            {
                if (CommonHelper.QueryStringInt("urlpageIndex") == 0)
                {
                    return 1;
                }
                else
                {
                    return CommonHelper.QueryStringInt("urlpageIndex");
                }
            }

            set
            {
                pageIndex = value;
            }
        }

        public int pageSize
        {
            get
            {

                Category c = CategoryService.GetCategoryById(this.CategoryId);
                if (CommonHelper.QueryStringInt("urlpageSize") == 0)
                {
                    return c.PageSize != 0 ? c.PageSize : 12;
                }
                return CommonHelper.QueryStringInt("urlpageSize");
            }
        }

        public int pageCount(int Count)
        {
            return Count % pageSize == 0 ? Count / pageSize : (int)Math.Ceiling((double)Count / pageSize);
        }

        public List<Product> getPageData(List<Product> p, int pageIndex, int pageSize)
        {
            int startIndex = pageSize * (pageIndex - 1);
            
            int productCount = p.Count;
            if (productCount >= startIndex)
            {
                //view all products
                if (pageIndex == 1 && pageIndex == pageCount(productCount))
                {
                    return p.GetRange(startIndex, productCount);
                }
                //display products in the last page
                else if (pageIndex == pageCount(productCount))
                {
                    if ((productCount % pageSize) != 0)
                    {
                        return p.GetRange(startIndex, productCount % pageSize);
                    }
                    else
                    {
                        return p.GetRange(startIndex, pageSize);
                    }
                }
                else
                {
                    return p.GetRange(startIndex, pageSize);
                }
            }
            else
            {
                if (productCount == 0)
                {
                    return null;
                }
                else
                {
                    return p.GetRange(startIndex, productCount);
                }
            }
        }

        public int getStartedPageSize()
        {
            Category c = CategoryService.GetCategoryById(this.CategoryId);
            if (c.PageSize != 0)
                return c.PageSize;
            else
                return 12;
        }

        public List<string> AttrSelected
        {
            get
            {
                List<string>Attrs = new List<string>();
                string str = CommonHelper.QueryString("urlAttr");
                string[] s = str.Split('|');
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = s[i].TrimStart();
                    if(!string.IsNullOrEmpty(s[i].TrimEnd()))
                    {
                        Attrs.Add(s[i]);
                    }
                }
                return Attrs;
            }
        }

        public List<string> SpecSelected
        {
            get
            {
                List<string> Specs = new List<string>();
                string str = CommonHelper.QueryString("urlSpec");
                string[] s = str.Split('|');
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = s[i].TrimStart();
                    if (!string.IsNullOrEmpty(s[i].TrimEnd()))
                    {
                        Specs.Add(s[i]);
                    }
                }
                return Specs;
            }
        }
        #endregion
    }
}