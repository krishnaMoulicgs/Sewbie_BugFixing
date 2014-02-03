//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

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
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic.SEO;


namespace NopSolutions.NopCommerce.Web.OsShop.AjaxTemplateBySwn.AjaxPage
{
    public partial class getMiniShoppingInfo : System.Web.UI.Page
    {
        string str = "";
        static int errorSign;
        protected void lvCart_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var dataItem = e.Item as ListViewDataItem;
                if (dataItem != null)
                {
                    var sci = dataItem.DataItem as ShoppingCartItem;
                    if (sci != null)
                    {
                        //商品的名字
                        str += Server.HtmlEncode(sci.ProductVariant.LocalizedFullProductName);
                        str += "|";
                        //商品的图片   
                        GetProductVariantImageUrl(sci);
                        str += GetProductVariantImageUrl(sci);
                        str += "|";
                        //带税的价格
                        decimal taxRate = decimal.Zero;
                        decimal finalPriceWithDiscountBase = this.TaxService.GetPrice(sci.ProductVariant, PriceHelper.GetFinalPrice(sci.ProductVariant,true), out taxRate);
                        decimal finalPriceWithDiscount = CurrencyService.ConvertCurrency(finalPriceWithDiscountBase,

CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                        string selectAttr = "";//添加属性价格
                        string attributesXml = sci.AttributesXml;
                        string attributesValueTrans = "";
                        string attributeGiftCardMessage = "";
                        int GiftCardMessageSign = 0;

                        try
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(attributesXml);
                            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Attributes").ChildNodes;
                            foreach (XmlNode node in nodeList)
                            {
                                try
                                {
                                    string id = node.Attributes["ID"].InnerText.Trim();
                                    attributesValueTrans += id;
                                    attributesValueTrans += "|";

                                }
                                catch (Exception EE)
                                {
                                    GiftCardMessageSign = 1;
                                }
                                XmlNodeList nodeList1 = node.ChildNodes;//继续获取node子节点的所有子节
                                foreach (XmlNode node1 in nodeList1)
                                {
                                    string nodeNameSign = node1.Name.ToString();
                                    XmlNodeList nodeList2 = node1.ChildNodes;//继续获取node子节点的所有子节

                                    foreach (XmlNode node2 in nodeList2)
                                    {
                                        string b = node2.Name.ToString();
                                        if (GiftCardMessageSign == 1)
                                        {
                                            if (nodeNameSign == "RecipientName" || nodeNameSign == "SenderName")
                                            {
                                                attributeGiftCardMessage += node2.InnerText.ToString().Trim();
                                                attributeGiftCardMessage += "|";
                                            }

                                        }
                                        else
                                        {
                                            attributesValueTrans += node2.InnerText.ToString().Trim();
                                            attributesValueTrans += "|";
                                        }

                                    }

                                }

                                if (attributeGiftCardMessage != "")
                                {
                                    string[] getGiftAttr = attributeGiftCardMessage.Split('|');
                                    //Properties will be added into the shopping cart
                                    selectAttr += "For:" + getGiftAttr[0].ToString().Trim() + " ";
                                    selectAttr += "From:" + getGiftAttr[1].ToString().Trim();
                                    selectAttr += "<br/>";
                                    attributeGiftCardMessage = "";//Again attributeGiftCardMessageAssigned to empty properties in order to avoid two shopping card
                                    GiftCardMessageSign = 0;

                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            //If we can not read the form of gift cards will 
                            //throw an exception because the attribute of the 
                            //XML structure is different, and above all to find 
                            //the product attributes, exceptions
                        }


                        string[] attributesValue = attributesValueTrans.Split('|');

                        for (int j = 0; j < attributesValue.Count() - 1; j++)
                        {
                            if ((j + 1) % 2 == 0)
                            {
                                string contentText = attributesValue[j].ToString();
                                if (contentText.Count() != 0)
                                {
                                    int signLength = contentText.Count() - 1;
                                    if (contentText[signLength].ToString() == ".")
                                    {
                                        selectAttr += attributesValue[j].ToString();//Get property name
                                        selectAttr += "<br/>";
                                    }
                                    else
                                    {
                                        ProductVariantAttributeValue getAttr = ProductAttributeService.GetProductVariantAttributeValueById(int.Parse
                                            
(attributesValue[j]));
                                        decimal taxRate1 = decimal.Zero;
                                        decimal priceAdjustmentBase =this.TaxService.GetPrice(sci.ProductVariant, getAttr.PriceAdjustment, out taxRate1);
                                        decimal priceAdjustment = CurrencyService.ConvertCurrency(priceAdjustmentBase,

CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                                        finalPriceWithDiscount += priceAdjustment;
                                        selectAttr += getAttr.LocalizedName;//Get property name
                                        selectAttr += "<br/>";
                                    }
                                }

                            }
                        }

                        str += finalPriceWithDiscount;
                        str += "|";
                        //The number of goods
                        str += sci.Quantity.ToString();
                        str += "|";
                        //Shopping cart ID Delete event for processing
                        str += sci.ShoppingCartItemId.ToString();
                        str += "|";
                        //Goods corresponding to the link

                        str += SEOHelper.GetProductUrl(sci.ProductVariant.Product);
                        str += "|";
                        //Currency symbol
                        str += PriceHelper.FormatPrice(0).ToString()[0].ToString();
                        str += "|";
                        //Product properties
                        str += selectAttr;
                    }
                    str += "<!>";
                }
            }
        }

        public string GetProductVariantImageUrl(ShoppingCartItem shoppingCartItem)
        {
            string pictureUrl = String.Empty;
            var productVariant = shoppingCartItem.ProductVariant;
            if (productVariant != null)
            {
                var productVariantPicture = productVariant.Picture;
                pictureUrl = PictureService.GetPictureUrl(productVariantPicture, SettingManager.GetSettingValueInteger("Media.Product.VariantImageSize", 125), false);
                if (String.IsNullOrEmpty(pictureUrl))
                {
                    var product = productVariant.Product;
                    var picture = product.DefaultPicture;
                    if (picture != null)
                    {
                        pictureUrl = PictureService.GetPictureUrl(picture, SettingManager.GetSettingValueInteger("Media.Product.VariantImageSize", 125));
                    }
                    else
                    {
                        pictureUrl = PictureService.GetDefaultPictureUrl(SettingManager.GetSettingValueInteger("Media.Product.VariantImageSize", 125));
                    }
                }
            }
            return pictureUrl;
        }

        protected override void OnInit(EventArgs e)
        {
            //Click the items into the shopping cart
            decimal customerEnteredPrice = 0;
            errorSign = 1;

            //Add a shopping cart with attributes
            string productVariantIdAtrr = CommonHelper.QueryString("productVariantIdAtrr");
            string productAttributeMessage = CommonHelper.QueryString("productAttributeMessage");
            string quantity = CommonHelper.QueryString("quantity");
            string giftCardMessage = CommonHelper.QueryString("giftGardMessage");
            string[] giftCard = giftCardMessage.Split('|');

            if (productVariantIdAtrr != "")
            {
                string selectedAttributes = string.Empty;

                string[] attributeString = productAttributeMessage.Split('|');
                for (int i = 0; i < attributeString.Length - 1; i++)
                {
                    string[] attributeValue = attributeString[i].ToString().Split(',');
                    string attributeValueSign = "";
                    string contentText = attributeValue[2].ToString();
                    if (contentText.Count() != 0)
                    {
                        int signLength = contentText.Count() - 1;
                        if (contentText[signLength].ToString() == "@")
                        {
                            string[] dataValue = contentText.ToString().Trim().Split('@');

                            DateTime? DataSignString = new DateTime(Int32.Parse(dataValue[0]), Int32.Parse(dataValue[1]), Int32.Parse(dataValue[2]));
                            attributeValueSign = DataSignString.Value.ToString("D");
                            attributeValueSign += ".";
                        }
                        else
                        {
                            attributeValueSign = attributeValue[2].ToString();
                        }
                    }
                    attributeValueSign = attributeValueSign.ToString().Trim();
                    ProductVariantAttribute attribute = ProductAttributeService.GetProductVariantAttributeById(int.Parse(attributeValue[1].ToString()));
                    selectedAttributes = ProductAttributeHelper.AddProductAttribute(selectedAttributes,
                                            attribute, attributeValueSign);
                }
                //gift cards
                var pv = ProductService.GetProductVariantById(int.Parse(productVariantIdAtrr));
                if (pv.IsGiftCard)
                {
                    if (giftCard.Count() == 6)
                    {
                        string recipientName = giftCard[0].ToString().Trim();
                        string recipientEmail = giftCard[1].ToString().Trim();
                        string senderName = giftCard[2].ToString().Trim();
                        string senderEmail = giftCard[3].ToString().Trim();
                        string giftCardMessage1 = giftCard[4].ToString().Trim();

                        selectedAttributes = ProductAttributeHelper.AddGiftCardAttribute(selectedAttributes,
                            recipientName, recipientEmail, senderName, senderEmail, giftCardMessage1);
                    }

                }
                //Add to Shopping Cart
                try
                {

                   
                    var addToCartWarnings = this.ShoppingCartService.AddToCart(
                                        ShoppingCartTypeEnum.ShoppingCart, int.Parse(productVariantIdAtrr), selectedAttributes, customerEnteredPrice, int.Parse(quantity.ToString()));

                    //Add to Cart unsuccessful Returns an error message
                    if (addToCartWarnings.Count > 0)
                    {
                        string sep = "<br />";
                        var addToCartWarningsSb = new StringBuilder();
                        for (int i = 0; i < addToCartWarnings.Count; i++)
                        {
                            addToCartWarningsSb.Append(Server.HtmlEncode(addToCartWarnings[i]));
                            if (i != addToCartWarnings.Count - 1)
                            {
                                addToCartWarningsSb.Append(sep);
                            }
                        }
                        string errorFull = addToCartWarningsSb.ToString();
                        string errorMessage = errorFull.Replace(sep, "\n");
                        errorSign = 0;
                        errorMessage = "error" + "<!>" + errorMessage + "<!>";
                        Response.Write(errorMessage);
                    }
                }
                catch (Exception eee)
                {
                    //Fill the case to prevent malicious attacks.
                }

            }



            //Delete shopping cart items
            string productQueryId = CommonHelper.QueryString("productQueryId");
            if (productQueryId != "")
            {
                this.ShoppingCartService.DeleteShoppingCartItem(int.Parse(productQueryId), true);
            }
            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (errorSign == 1)
            {
                //Read the information inside the shopping cart 
                if (SettingManager.GetSettingValueBoolean("Common.ShowMiniShoppingCart"))
                {
                    var shoppingCart = this.ShoppingCartService.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart);

                    if (shoppingCart.Count > 0)
                    {
                        lvCart.Visible = true;
                        lvCart.DataSource = shoppingCart;
                        lvCart.DataBind();
                    }
                }

                //Returns the information to ajax
                if (str != "")
                    Response.Write(str);
            }
        }

        public string getSelectedAttribute(string message)
        {
            string selectedAttributes = string.Empty;

            string[] attributeString = message.Split('|'); ;
            for (int i = 0; i < attributeString.Length - 1; i++)
            {
                string[] attributeValue = attributeString[i].ToString().Split(',');

                ProductVariantAttribute attribute = ProductAttributeService.GetProductVariantAttributeById(int.Parse(attributeValue[0].ToString()));
                selectedAttributes = ProductAttributeHelper.AddProductAttribute(selectedAttributes,
                                        attribute, attributeValue[1].ToString());
            }

            return selectedAttributes;
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


