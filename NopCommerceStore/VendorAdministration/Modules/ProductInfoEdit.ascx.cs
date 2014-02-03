using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FredCK.FCKeditorV2;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ProductInfoControl : BaseNopVendorAdministrationUserControl
    {
        private void BindData()
        {
            Product product = this.ProductService.GetProductById(this.ProductId);

            if (this.HasLocalizableContent)
            {
                var languages = this.GetLocalizableLanguagesSupported();
                rptrLanguageTabs.DataSource = languages;
                rptrLanguageTabs.DataBind();
                rptrLanguageDivs.DataSource = languages;
                rptrLanguageDivs.DataBind();
            }

            if (product != null)
            {
                this.txtName.Text = product.Name;
                this.txtShortDescription.Text = product.ShortDescription;
                this.txtFullDescription.Value = product.FullDescription;
                this.txtStockQuantity.Value = product.ProductVariants[0].StockQuantity;
                this.cbPublished.Checked = product.Published;

                this.txtPrice.Value = product.ProductVariants[0].Price;

                this.txtAdditionalShippingCharge.Value = product.ProductVariants[0].AdditionalShippingCharge;
                this.cbDisplayStockQuantity.Checked = product.ProductVariants[0].DisplayStockQuantity;

                var productReviews = product.ProductReviews;
                if (productReviews.Count > 0)
                {
                    hlViewReviews.Visible = true;
                    hlViewReviews.Text = string.Format(GetLocaleResourceString("Admin.ProductInfo.AllowCustomerReviewsView"), productReviews.Count);
                    hlViewReviews.NavigateUrl = CommonHelper.GetStoreAdminLocation() + "ProductReviews.aspx?ProductID=" + product.ProductId.ToString();
                }
                else
                    hlViewReviews.Visible = false;

                this.txtProductTags.Text = GenerateListOfProductTagss(this.ProductService.GetProductTagsByProductId(product.ProductId));
            }
        }

        private void FillDropDowns()
        {
            //this.ddlTemplate.Items.Clear();
            //var productTemplateCollection = this.TemplateService.GetAllProductTemplates();
            //foreach (ProductTemplate productTemplate in productTemplateCollection)
            //{
            //    ListItem item2 = new ListItem(productTemplate.Name, productTemplate.ProductTemplateId.ToString());
            //    this.ddlTemplate.Items.Add(item2);
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            mainMaster m = (mainMaster)Page.Master;
            
            if (!Page.IsPostBack)
            {
                this.FillDropDowns();
                this.BindData();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            BindJQuery();
            BindJQueryIdTabs();

            base.OnPreRender(e);
        }

        private string GenerateListOfProductTagss(List<ProductTag> productTags)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < productTags.Count; i++)
            {
                ProductTag pt = productTags[i];
                result.Append(pt.Name.ToString());
                if (i != productTags.Count - 1)
                {
                    result.Append(", ");
                }
            }
            return result.ToString();
        }

        private string[] ParseProductTags(string productTags)
        {
            List<string> result = new List<string>();
            string[] values = productTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string val1 in values)
            {
                if (!String.IsNullOrEmpty(val1.Trim()))
                {
                    result.Add(val1);
                }
            }
            return result.ToArray();
        }

        public Product SaveInfo()
        {
            
            Product product = this.ProductService.GetProductById(this.ProductId);
            if (Page.IsValid)
            { 
                if (product != null)
                {
                    product.Name = txtName.Text;
                    product.ShortDescription = txtShortDescription.Text;
                    product.FullDescription = txtFullDescription.Value;
                    product.AdminComment = String.Empty; // txtAdminComment.Text;
                    product.TemplateId = 5; // int.Parse(this.ddlTemplate.SelectedItem.Value);
                    product.ShowOnHomePage = false; // cbShowOnHomePage.Checked;
                    product.AllowCustomerReviews = true; // cbAllowCustomerReviews.Checked;
                    product.AllowCustomerRatings = true; // cbAllowCustomerRatings.Checked;
                    product.Published = cbPublished.Checked;
                    product.MetaKeywords = txtProductTags.Text;
                    product.MetaDescription = txtShortDescription.Text;
                    product.MetaTitle = txtName.Text;
                    //use this flag to determine whether or not
                    //product is an add or an update.
                    product.Activated = true;
                    product.UpdatedOn = DateTime.UtcNow;
                    this.ProductService.UpdateProduct(product);

               
                    //NFF - 12.19.2011 - There is only one product variant in Sewbie.
                    //                   we will not use anything else so it can be hard coded
                    //                   as if it is an extension table / object.
                    //NFF - 01/11/2012 - Updated so publish saves as well.
                    //NFF - 01/20/2012 - Updated so the disable buy button is automatically enabled.
                    product.ProductVariants[0].StockQuantity = txtStockQuantity.Value;
                    //product.ProductVariants[0].VendorId = Convert.ToInt32(ddlVendor.SelectedItem.Value);
                    product.ProductVariants[0].VendorId = NopCommerce.BusinessLogic.NopContext.Current.User.CustomerId;
                    product.ProductVariants[0].Price = txtPrice.Value;
                    product.ProductVariants[0].Published = cbPublished.Checked;
                    product.ProductVariants[0].DisableBuyButton = false;

                    product.ProductVariants[0].IsShipEnabled = true;
                    product.ProductVariants[0].IsFreeShipping = false;
                    product.ProductVariants[0].AdditionalShippingCharge = txtAdditionalShippingCharge.Value;
                    product.ProductVariants[0].DisplayStockAvailability = true;
                    product.ProductVariants[0].DisplayStockQuantity = cbDisplayStockQuantity.Checked;
                    product.ProductVariants[0].ManageInventory = 1;

                    this.ProductService.UpdateProductVariant(product.ProductVariants[0]);

                    SaveLocalizableContent(product);

                    //product tags
                    var existingProductTags = this.ProductService.GetProductTagsByProductId(product.ProductId);
                    string[] newProductTags = ParseProductTags(txtProductTags.Text);
                    var productTagsToDelete = new List<ProductTag>();
                    foreach (var existingProductTag in existingProductTags)
                    {
                        bool found = false;
                        foreach (string newProductTag in newProductTags)
                        {
                            if (existingProductTag.Name.Equals(newProductTag, StringComparison.InvariantCultureIgnoreCase))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            productTagsToDelete.Add(existingProductTag);
                        }
                    }
                    foreach (var productTag in productTagsToDelete)
                    {
                        this.ProductService.RemoveProductTagMapping(product.ProductId, productTag.ProductTagId);
                    }
                    foreach (string productTagName in newProductTags)
                    {
                        ProductTag productTag = null;
                        var productTag2 = this.ProductService.GetProductTagByName(productTagName);
                        if (productTag2 == null)
                        {
                            //add new product tag
                            productTag = new ProductTag()
                            {
                                Name = productTagName,
                                ProductCount = 0
                            };
                            this.ProductService.InsertProductTag(productTag);
                        }
                        else
                        {
                            productTag = productTag2;
                        }
                        if (!this.ProductService.DoesProductTagMappingExist(product.ProductId, productTag.ProductTagId))
                        {
                            this.ProductService.AddProductTagMapping(product.ProductId, productTag.ProductTagId);
                        }
                    }
                }

                
            }
            else { 
                //page is invalid.  no good data on the form.
                base.ShowError("There is an error on the form.  Please check your entries and try again.");
            }
            return product;
        }

        protected void SaveLocalizableContent(Product product)
        {
            if (product == null)
                return;

            if (!this.HasLocalizableContent)
                return;

            foreach (RepeaterItem item in rptrLanguageDivs.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var txtLocalizedName = (TextBox)item.FindControl("txtLocalizedName");
                    var txtLocalizedShortDescription = (TextBox)item.FindControl("txtLocalizedShortDescription");
                    var txtLocalizedFullDescription = (FCKeditor)item.FindControl("txtLocalizedFullDescription");
                    var lblLanguageId = (Label)item.FindControl("lblLanguageId");

                    int languageId = int.Parse(lblLanguageId.Text);
                    string name = txtLocalizedName.Text;
                    string shortDescription = txtLocalizedShortDescription.Text;
                    string fullDescription = txtLocalizedFullDescription.Value;

                    bool allFieldsAreEmpty = (string.IsNullOrEmpty(name) &&
                        string.IsNullOrEmpty(shortDescription) &&
                        string.IsNullOrEmpty(fullDescription));

                    var content = this.ProductService.GetProductLocalizedByProductIdAndLanguageId(product.ProductId, languageId);
                    if (content == null)
                    {
                        if (!allFieldsAreEmpty && languageId > 0)
                        {
                            //only insert if one of the fields are filled out (avoid too many empty records in db...)
                            content = new ProductLocalized()
                            {
                                ProductId = product.ProductId,
                                LanguageId = languageId,
                                Name = name,
                                ShortDescription = shortDescription,
                                FullDescription = fullDescription
                            };
                            this.ProductService.InsertProductLocalized(content);
                        }
                    }
                    else
                    {
                        if (languageId > 0)
                        {
                            content.LanguageId = languageId;
                            content.Name = name;
                            content.ShortDescription = shortDescription;
                            content.FullDescription = fullDescription;
                            this.ProductService.UpdateProductLocalized(content);
                        }
                    }
                }
            }
        }

        protected void rptrLanguageDivs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtLocalizedName = (TextBox)e.Item.FindControl("txtLocalizedName");
                var txtLocalizedShortDescription = (TextBox)e.Item.FindControl("txtLocalizedShortDescription");
                var txtLocalizedFullDescription = (FCKeditor)e.Item.FindControl("txtLocalizedFullDescription");
                var lblLanguageId = (Label)e.Item.FindControl("lblLanguageId");

                int languageId = int.Parse(lblLanguageId.Text);

                var content = this.ProductService.GetProductLocalizedByProductIdAndLanguageId(this.ProductId, languageId);
                if (content != null)
                {
                    txtLocalizedName.Text = content.Name;
                    txtLocalizedShortDescription.Text = content.ShortDescription;
                    txtLocalizedFullDescription.Value = content.FullDescription;
                }
            }
        }

        public int ProductId
        {
            get
            {
                return CommonHelper.QueryStringInt("ProductId");
            }
        }

        protected void FullDescription_Validate(object source, ServerValidateEventArgs args)
        {
            if (txtFullDescription.Value == String.Empty)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}