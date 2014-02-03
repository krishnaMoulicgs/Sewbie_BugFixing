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
    public partial class RproductList : BaseNopUserControl
    {
        string ProMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            List<Product> result = new List<Product>();
            List<Product> filterResult = new List<Product>();
            bool In = false;
            bool In1 = false;
            bool paOut = true;
            bool psOut = true;

            decimal? minPriceConverted = urlMinPrice;
            decimal? maxPriceConverted = urlMaxPrice;

            //selected nothing
            if (categoryIdsSelected.Count == 0 && manufacturerIdsSelected.Count == 0)
            {
                foreach (Product p in products)
                {
                    foreach (ProductVariant pv in p.ProductVariants)
                    {
                        if (pv.Price >= minPriceConverted && pv.Price <= maxPriceConverted)
                        {
                            In = true;
                            break;
                        }
                    }
                    if (In && !result.Contains(p))
                    {
                        result.Add(p);
                        In = false;
                    }

                }
            }
            //manufacturer selected
            else if (categoryIdsSelected.Count == 0 && manufacturerIdsSelected.Count > 0)
            {
                foreach (Product p in products)
                {
                    foreach (ProductManufacturer pm in p.ProductManufacturers)
                    {
                        if (manufacturerIdsSelected.Contains(pm.ManufacturerId))
                        {
                            foreach (ProductVariant pv in p.ProductVariants)
                            {
                                if (pv.Price >= minPriceConverted && pv.Price <= maxPriceConverted)
                                {
                                    In = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            In = false;
                        }
                    }
                    if (In && !result.Contains(p))
                    {
                        result.Add(p);
                        In = false;
                    }
                }
            }

            //category selected
            else if (categoryIdsSelected.Count > 0 && manufacturerIdsSelected.Count == 0)
            {
                foreach (Product p in products)
                {
                    foreach (ProductCategory pc in p.ProductCategories)
                    {
                        if (categoryIdsSelected.Contains(pc.CategoryId))
                        {
                            foreach (ProductVariant pv in p.ProductVariants)
                            {
                                if (pv.Price >= minPriceConverted && pv.Price <= maxPriceConverted)
                                {
                                    In = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            In = false;
                        }
                    }
                    if (In && !result.Contains(p))
                    {
                        result.Add(p);
                        In = false;
                    }
                }
            }

            //both manufacturer and category selected
            else if (categoryIdsSelected.Count > 0 && manufacturerIdsSelected.Count > 0)
            {
                foreach (Product p in products)
                {
                    foreach (ProductCategory pc in p.ProductCategories)
                    {
                        if (categoryIdsSelected.Contains(pc.CategoryId))
                        {
                            foreach (ProductVariant pv in p.ProductVariants)
                            {
                                if (pv.Price >= minPriceConverted && pv.Price <= maxPriceConverted)
                                {
                                    In = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            In = false;
                        }
                    }

                    foreach (ProductManufacturer pm in p.ProductManufacturers)
                    {
                        if (manufacturerIdsSelected.Contains(pm.ManufacturerId))
                        {
                            foreach (ProductVariant pv in p.ProductVariants)
                            {
                                if (pv.Price >= minPriceConverted && pv.Price <= maxPriceConverted)
                                {
                                    In1 = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            In1 = false;
                        }
                    }

                    if (In && In1 && !result.Contains(p))
                    {
                        result.Add(p);
                        In = false;
                        In1 = false;
                    }
                }
            }

            foreach (Product p in result)
            {
                paOut = true;
                psOut = true;

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

                if (!paOut && !psOut)
                {
                    if (!filterResult.Contains(p))
                    {
                        filterResult.Add(p);
                        paOut = true;
                        psOut = true;
                    }
                }
                #endregion
            }

            switch (order)
            {
                case "new": filterResult.Sort(CompareByNew);
                    break;
                case "lth": filterResult.Sort(CompareByLowToHigh);
                    break;
                case "htl": filterResult.Sort(CompareByHighToLow);
                    break;
            }

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

            //dlProducts.DataSource = result;
            //dlProducts.DataBind();
        }

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



        #region [Properties]
        public List<Product> products
        {
            get
            {
                return ProductService.GetRecentlyAddedProducts(30);
            }
        }

        public List<Category> categories
        {
            get
            {
                List<Category> list = new List<Category>();
                foreach (Product p in products)
                {
                    foreach (ProductCategory pc in p.ProductCategories)
                    {
                        if (list.Contains(pc.Category))   
                        {
                            continue;
                        }
                        else
                        {
                            list.Add(pc.Category);
                        }
                    }
                }
                return list;
            }
        }

        public List<int> categoryIdsSelected
        {
            get
            {
                List<int> Ids = new List<int>();
                if (!String.IsNullOrWhiteSpace(CommonHelper.QueryString("urlSubcategoriesSelected")))
                {
                    string[] strIds = CommonHelper.QueryString("urlSubcategoriesSelected").Split('|');
                    if (strIds.Length > 0)
                    {
                        foreach (string Id in strIds)
                        {
                            int result;
                            Int32.TryParse(Id, out result);
                            Ids.Add(result);
                        }
                    }
                }
                return Ids;
            }
        }

        public List<Manufacturer> manufacturers
        {
            get
            {
                List<Manufacturer> list = new List<Manufacturer>();
                foreach (Product p in products)
                {
                    foreach (ProductManufacturer pc in p.ProductManufacturers)
                    {
                        if (list.Contains(pc.Manufacturer))
                        {
                            continue;
                        }
                        else
                        {
                            list.Add(pc.Manufacturer);
                        }
                    }
                }
                return list;
            }
        }

        public List<int> manufacturerIdsSelected
        {
            get
            {
                List<int> Ids = new List<int>();
                if (!String.IsNullOrWhiteSpace(CommonHelper.QueryString("urlManufacturersSelected")))
                {
                    string[] strIds = CommonHelper.QueryString("urlManufacturersSelected").Split('|');
                    if (strIds.Length > 0)
                    {
                        foreach (string Id in strIds)
                        {
                            int result;
                            Int32.TryParse(Id, out result);
                            Ids.Add(result);
                        }
                    }
                }
                return Ids;
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
                if (CommonHelper.QueryStringInt("urlpageSize") == 0)
                {
                    return 12;
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
            return 12;
        }

        public List<string> AttrSelected
        {
            get
            {
                List<string> Attrs = new List<string>();
                string str = CommonHelper.QueryString("urlAttr");
                string[] s = str.Split('|');
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = s[i].TrimStart();
                    if (!string.IsNullOrEmpty(s[i].TrimEnd()))
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