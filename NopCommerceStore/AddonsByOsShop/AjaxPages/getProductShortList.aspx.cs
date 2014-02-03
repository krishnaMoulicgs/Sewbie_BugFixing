using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Audit.UsersOnline;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Content.Blog;
using NopSolutions.NopCommerce.BusinessLogic.Content.Forums;
using NopSolutions.NopCommerce.BusinessLogic.Content.NewsManagement;
using NopSolutions.NopCommerce.BusinessLogic.Content.Polls;
using NopSolutions.NopCommerce.BusinessLogic.Content.Topics;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.ExportImport;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Maintenance;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Measures;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Messages.SMS;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Affiliates;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Campaigns;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.QuickBooks;
using NopSolutions.NopCommerce.BusinessLogic.Security;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.BusinessLogic.Warehouses;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.SEO;


namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.AjaxPages
{
    public partial class getProductShortList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productListId = CommonHelper.QueryString("productListTransId");
            if (productListId != "")
            {
                int productId = int.Parse(productListId);
                getProductMessage(productId);
            }
            
        }

        public void getProductMessage(int productId)
        {
            Product product = ProductService.GetProductById(productId);
            BindProductInfo(product);
        }

        
        protected void BindProductInfo(Product product)
        {
            string str = "";
            //产品名字
            str += Server.HtmlEncode(product.LocalizedName);
            str += "<!>";
            //产品短描述
            str += product.LocalizedFullDescription;
            str += "<!>";
            //价格
            decimal taxRate = decimal.Zero;
            decimal finalPriceWithDiscountBase =this.TaxService.GetPrice(product.ProductVariants[0], PriceHelper.GetFinalPrice(product.ProductVariants[0], true), out taxRate);
            decimal finalPriceWithDiscount = CurrencyService.ConvertCurrency(finalPriceWithDiscountBase, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
            str += PriceHelper.FormatPrice(finalPriceWithDiscount);//价格
            str += "<!>"; 
            //产品对应的链接 
            str += SEOHelper.GetProductUrl(product);
            str += "<!>";
            //产品图片
            //..............................................................
            var pictures = PictureService.GetPicturesByProductId(product.ProductId);
            if (pictures.Count > 1)
            {
                for (int i = 0; i < pictures.Count; i++)
                {
                    str += PictureService.GetPictureUrl(pictures[i], SettingManager.GetSettingValueInteger("Media.MaximumImageSize"));
                    str += "|";
                }
                   
            }
            else if (pictures.Count == 1)
            {
                str += PictureService.GetPictureUrl(pictures[0], SettingManager.GetSettingValueInteger("Media.MaximumImageSize"));
                str += "|";
            }
            else
            {
                str += PictureService.GetDefaultPictureUrl(SettingManager.GetSettingValueInteger("Media.MaximumImageSize", 1000));
                str += "|";
            }

            //.................................................................................
            str += "<!>";
            Response.Write(str);
        }



        protected string GetLocaleResourceString(string ResourceName)
        {
            Language language = NopContext.Current.WorkingLanguage;
            return LocalizationManager.GetLocaleResourceString(ResourceName, language.LanguageId);
        }

        #region Services, managers

        public IOnlineUserService OnlineUserService
        {
            get { return IoC.Resolve<IOnlineUserService>(); }
        }
        public ICustomerActivityService CustomerActivityService
        {
            get { return IoC.Resolve<ICustomerActivityService>(); }
        }
        public ILogService LogService
        {
            get { return IoC.Resolve<ILogService>(); }
        }
        public ISearchLogService SearchLogService
        {
            get { return IoC.Resolve<ISearchLogService>(); }
        }
        public ICategoryService CategoryService
        {
            get { return IoC.Resolve<ICategoryService>(); }
        }
        public ISettingManager SettingManager
        {
            get { return IoC.Resolve<ISettingManager>(); }
        }
        public IBlogService BlogService
        {
            get { return IoC.Resolve<IBlogService>(); }
        }
        public IForumService ForumService
        {
            get { return IoC.Resolve<IForumService>(); }
        }
        public INewsService NewsService
        {
            get { return IoC.Resolve<INewsService>(); }
        }
        public IPollService PollService
        {
            get { return IoC.Resolve<IPollService>(); }
        }
        public ITopicService TopicService
        {
            get { return IoC.Resolve<ITopicService>(); }
        }
        public ICustomerService CustomerService
        {
            get { return IoC.Resolve<ICustomerService>(); }
        }
        public ICountryService CountryService
        {
            get { return IoC.Resolve<ICountryService>(); }
        }
        public ICurrencyService CurrencyService
        {
            get { return IoC.Resolve<ICurrencyService>(); }
        }
        public ILanguageService LanguageService
        {
            get { return IoC.Resolve<ILanguageService>(); }
        }
        public IStateProvinceService StateProvinceService
        {
            get { return IoC.Resolve<IStateProvinceService>(); }
        }
        public IExportManager ExportManager
        {
            get { return IoC.Resolve<IExportManager>(); }
        }
        public IImportManager ImportManager
        {
            get { return IoC.Resolve<IImportManager>(); }
        }
        public ILocalizationManager LocalizationManager
        {
            get { return IoC.Resolve<ILocalizationManager>(); }
        }
        public IMaintenanceService MaintenanceService
        {
            get { return IoC.Resolve<IMaintenanceService>(); }
        }
        public IManufacturerService ManufacturerService
        {
            get { return IoC.Resolve<IManufacturerService>(); }
        }
        public IMeasureService MeasureService
        {
            get { return IoC.Resolve<IMeasureService>(); }
        }
        public IDownloadService DownloadService
        {
            get { return IoC.Resolve<IDownloadService>(); }
        }
        public IPictureService PictureService
        {
            get { return IoC.Resolve<IPictureService>(); }
        }
        public ISMSService SMSService
        {
            get { return IoC.Resolve<ISMSService>(); }
        }
        public IMessageService MessageService
        {
            get { return IoC.Resolve<IMessageService>(); }
        }
        public IOrderService OrderService
        {
            get { return IoC.Resolve<IOrderService>(); }
        }
        public IShoppingCartService ShoppingCartService
        {
            get { return IoC.Resolve<IShoppingCartService>(); }
        }
        public IPaymentService PaymentService
        {
            get { return IoC.Resolve<IPaymentService>(); }
        }
        public ICheckoutAttributeService CheckoutAttributeService
        {
            get { return IoC.Resolve<ICheckoutAttributeService>(); }
        }
        public IProductAttributeService ProductAttributeService
        {
            get { return IoC.Resolve<IProductAttributeService>(); }
        }
        public ISpecificationAttributeService SpecificationAttributeService
        {
            get { return IoC.Resolve<ISpecificationAttributeService>(); }
        }
        public IProductService ProductService
        {
            get { return IoC.Resolve<IProductService>(); }
        }
        public IAffiliateService AffiliateService
        {
            get { return IoC.Resolve<IAffiliateService>(); }
        }
        public ICampaignService CampaignService
        {
            get { return IoC.Resolve<ICampaignService>(); }
        }
        public IDiscountService DiscountService
        {
            get { return IoC.Resolve<IDiscountService>(); }
        }
        public IQBService QBService
        {
            get { return IoC.Resolve<IQBService>(); }
        }
        public IACLService ACLService
        {
            get { return IoC.Resolve<IACLService>(); }
        }
        public IBlacklistService BlacklistService
        {
            get { return IoC.Resolve<IBlacklistService>(); }
        }
        public IShippingByTotalService ShippingByTotalService
        {
            get { return IoC.Resolve<IShippingByTotalService>(); }
        }
        public IShippingByWeightAndCountryService ShippingByWeightAndCountryService
        {
            get { return IoC.Resolve<IShippingByWeightAndCountryService>(); }
        }
        public IShippingByWeightService ShippingByWeightService
        {
            get { return IoC.Resolve<IShippingByWeightService>(); }
        }
        public IShippingRateComputationMethod ShippingRateComputationMethod
        {
            get { return IoC.Resolve<IShippingRateComputationMethod>(); }
        }
        public IShippingService ShippingService
        {
            get { return IoC.Resolve<IShippingService>(); }
        }
        public ITaxCategoryService TaxCategoryService
        {
            get { return IoC.Resolve<ITaxCategoryService>(); }
        }
        public ITaxProviderService TaxProviderService
        {
            get { return IoC.Resolve<ITaxProviderService>(); }
        }
        public ITaxRateService TaxRateService
        {
            get { return IoC.Resolve<ITaxRateService>(); }
        }
        public ITaxService TaxService
        {
            get { return IoC.Resolve<ITaxService>(); }
        }
        public ITemplateService TemplateService
        {
            get { return IoC.Resolve<ITemplateService>(); }
        }
        public IWarehouseService WarehouseService
        {
            get { return IoC.Resolve<IWarehouseService>(); }
        }
        #endregion
    }
}


/*

var pictures = PictureService.GetPicturesByProductId(product.ProductId);
            if (pictures.Count > 1)
            {
                defaultImage.ImageUrl = PictureService.GetPictureUrl(pictures[0], SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 300));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                lvProductPictures.DataSource = pictures;
                lvProductPictures.DataBind();
            }
            else if (pictures.Count == 1)
            {
                defaultImage.ImageUrl = PictureService.GetPictureUrl(pictures[0], SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 300));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                lvProductPictures.Visible = false;
            }
            else
            {
                defaultImage.ImageUrl = PictureService.GetDefaultPictureUrl(SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 300));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.LocalizedName);
                lvProductPictures.Visible = false;
            }

*/