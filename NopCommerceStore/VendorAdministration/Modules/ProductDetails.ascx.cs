using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class ProductDetailsControl : BaseNopVendorAdministrationUserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Product product = this.ProductService.GetProductById(this.ProductId);

                if (product != null)
                {
                    lblTitle.Text = Server.HtmlEncode(product.Name);
                    if (product.ProductVariants[0].Vendor.CustomerId != 
                            NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.CustomerId)
                    {
                        //this user is not allowed to edit other items.  Redirect to their
                        //products page.
                        Response.Redirect("Products.aspx");
                    }
                }
                else
                {
                    DateTime nowDT = DateTime.UtcNow;

                    

                    //create new product without activated set.
                    product = new Product()
                    {
                        Name = "New " + NopCommerce.BusinessLogic.NopContext.Current.User.Vendor.CompanyName + " Product ",
                        ShortDescription = String.Empty,
                        FullDescription = String.Empty,
                        AdminComment = "Created",
                        TemplateId = 5, //default for sewbie.
                        ShowOnHomePage = false,
                        AllowCustomerReviews = true,
                        AllowCustomerRatings = true,
                        Published = false,
                        Activated = false,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };


                    this.ProductService.InsertProduct(product);

                    ProductVariant prv = new ProductVariant()
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        SKU = String.Empty,
                        Description = String.Empty,
                        AdminComment = "Created",
                        ManufacturerPartNumber = String.Empty,
                        IsGiftCard = false,
                        GiftCardType = 0,
                        IsDownload = false,
                        DownloadId = 0,
                        UnlimitedDownloads = false,
                        MaxNumberOfDownloads = 0,
                        DownloadExpirationDays = 0,
                        DownloadActivationType = 0,
                        HasSampleDownload = false,
                        SampleDownloadId = 0,
                        HasUserAgreement = false,
                        UserAgreementText = String.Empty,
                        IsRecurring = false,
                        CycleLength = 0,
                        CyclePeriod = 0,
                        TotalCycles = 0,
                        IsShipEnabled = false,
                        IsFreeShipping = false,
                        AdditionalShippingCharge = 0,
                        IsTaxExempt = false,
                        TaxCategoryId = 0,
                        ManageInventory = 0,
                        StockQuantity = 1,
                        DisplayStockAvailability = false,
                        DisplayStockQuantity = true,
                        MinStockQuantity = 1,
                        LowStockActivityId = 0,
                        NotifyAdminForQuantityBelow = 1,
                        Backorders = 0,
                        OrderMinimumQuantity = 1,
                        OrderMaximumQuantity = 10000,
                        WarehouseId = 0,
                        DisableBuyButton = false,
                        CallForPrice = false,
                        Price = 0.01M,
                        OldPrice = 0,
                        ProductCost = 0,
                        CustomerEntersPrice = false,
                        MinimumCustomerEnteredPrice = 0,
                        MaximumCustomerEnteredPrice = 0,
                        Weight = 0,
                        Length = 0,
                        Width = 0,
                        Height = 0,
                        PictureId = 0,
                        AvailableStartDateTime = null,
                        AvailableEndDateTime = null,
                        Published = false,
                        Deleted = false,
                        DisplayOrder = 1,
                        VendorId = NopCommerce.BusinessLogic.NopContext.Current.User.CustomerId,
                        CreatedOn = nowDT,
                        UpdatedOn = nowDT
                    };

                    this.ProductService.InsertProductVariant(prv);

                    //we're going to do a redirect here so that the product id makes it into the
                    //query strnig and this page should load as if it's an edit.
                    Response.Redirect("ProductDetails.aspx?productid=" + product.ProductId);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            SimpleTextBox txtName = ctrlProductInfoEdit.FindControl("txtName") as SimpleTextBox;
            //if(txtName != null)
            //{
            //    txtProductCopyName.Text = "Copy of " + txtName.Text;
            //}

            //var validateScriptLocation = CommonHelper.GetStoreLocation() + "Scripts/jquery.validate.min.js";
            //Page.ClientScript.RegisterClientScriptInclude(validateScriptLocation, validateScriptLocation);


            Product product = this.ProductService.GetProductById(this.ProductId);
            if (product != null)
            {
                PreviewButton.OnClientClick = string.Format("javascript:OpenWindow('{0}', 800, 600, true); return false;", SEOHelper.GetProductUrl(product.ProductId));
            }

            base.OnPreRender(e);
        }

        protected Product Save()
        {
            Product product = null;
            //uncomment this line to support transactions
            using (var scope = new System.Transactions.TransactionScope())
            {
                product = ctrlProductInfoEdit.SaveInfo();
                //ctrlProductSEO.SaveInfo();
                //ctrlProductVariants.SaveInfo();
                ctrlProductCategory.SaveInfo();
                ctrlProductManufacturer.SaveInfo();
                //ctrlRelatedProducts.SaveInfo();
                //ctrlCrossSellProducts.SaveInfo();
                ctrlProductPictures.SaveInfo();
                //ctrlProductSpecifications.SaveInfo();

                this.CustomerActivityService.InsertActivity(
                    "EditProduct",
                    GetLocaleResourceString("ActivityLog.EditProduct"),
                    product.Name);

                //uncomment this line to support transactions
                scope.Complete();
            }

            return product;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    Product product = Save();
                    Response.Redirect("Products.aspx");
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void SaveAndStayButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    Product product = Save();
                    //Response.Redirect(string.Format("ProductDetails.aspx?ProductID={0}", ProductId.ToString()), false);
                    base.ShowMessage("Product saved successfully.");
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = this.ProductService.GetProductById(this.ProductId);
                if (product != null)
                {
                    this.ProductService.MarkProductAsDeleted(this.ProductId);

                    this.CustomerActivityService.InsertActivity(
                        "DeleteProduct",
                        GetLocaleResourceString("ActivityLog.DeleteProduct"),
                        product.Name);

                }
                Response.Redirect("Products.aspx");
            }
            catch (Exception exc)
            {
                ProcessException(exc);
            }
        }

        //protected void BtnDuplicate_OnClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Product productCopy = this.ProductService.DuplicateProduct(ProductId, txtProductCopyName.Text, cbIsProductCopyPublished.Checked, cbCopyImages.Checked);
        //        if(productCopy != null)
        //        {
        //            this.CustomerActivityService.InsertActivity(
        //                "AddNewProduct",
        //                GetLocaleResourceString("ActivityLog.AddNewProduct"),
        //                productCopy.Name);

        //            Response.Redirect(String.Format("ProductDetails.aspx?ProductID={0}", productCopy.ProductId));
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ProcessException(ex);
        //    }
        //}

        public int ProductId
        {
            get
            {
                return CommonHelper.QueryStringInt("ProductId");
            }
        }

        protected string TabId
        {
            get
            {
                return CommonHelper.QueryString("TabId");
            }
        }
    }
}