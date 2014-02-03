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
    public partial class SewbieProductCategoryControl : BaseNopVendorAdministrationUserControl
    {
        private void BindData()
        {
            List<ProductCategoryMappingHelperClass> productCategoryMappings = null;
            
            Product product = this.ProductService.GetProductById(this.ProductId);

            var existingProductCategoryCollection = product.ProductCategories;

            PopulateMensCatagoryDDL(existingProductCategoryCollection);
            PopulateWomensCatagoryDDL(existingProductCategoryCollection);
            PopulateKidsCatagoryDDL(existingProductCategoryCollection);

        }

        private void PopulateMensCatagoryDDL(List<ProductCategory> existingProducts){

            ddlMensCategory.DataSource = GetProductCategoryMappings(existingProducts, "Mens");
            ddlMensCategory.DataTextField = "CategoryInfo";
            ddlMensCategory.DataValueField = "CategoryId";
            ddlMensCategory.DataBind();

            //set the selected values if any.
            for (int ctrCategory = 0; ctrCategory <= existingProducts.Count - 1; ctrCategory++)
            {
                if (existingProducts[ctrCategory] != null)
                {
                    for(int ctrDDLItems = 0; ctrDDLItems <= ddlMensCategory.Items.Count - 1; ctrDDLItems++){
                        if (ddlMensCategory.Items[ctrDDLItems].Value == existingProducts[ctrCategory].CategoryId.ToString()){
                            ddlMensCategory.Items[ctrDDLItems].Selected = true;
                        } 
                    }
                }
            }
        }

        private void PopulateWomensCatagoryDDL(List<ProductCategory> existingProducts)
        {
            ddlWomensCategory.DataSource = GetProductCategoryMappings(existingProducts, "Womens");
            ddlWomensCategory.DataTextField = "CategoryInfo";
            ddlWomensCategory.DataValueField = "CategoryId";
            ddlWomensCategory.DataBind();

            //set the selected values if any.
            for (int ctrCategory = 0; ctrCategory <= existingProducts.Count - 1; ctrCategory++)
            {
                if (existingProducts[ctrCategory] != null)
                {
                    for (int ctrDDLItems = 0; ctrDDLItems <= ddlWomensCategory.Items.Count - 1; ctrDDLItems++)
                    {
                        if (ddlWomensCategory.Items[ctrDDLItems].Value == existingProducts[ctrCategory].CategoryId.ToString())
                        {
                            ddlWomensCategory.Items[ctrDDLItems].Selected = true;
                        }
                    }
                }
            }
        }

        private void PopulateKidsCatagoryDDL(List<ProductCategory> existingProducts)
        {

            ddlKidsCategory.DataSource = GetProductCategoryMappings(existingProducts, "Kids");
            ddlKidsCategory.DataTextField = "CategoryInfo";
            ddlKidsCategory.DataValueField = "CategoryId";
            ddlKidsCategory.DataBind();

            //set the selected values if any.
            for (int ctrCategory = 0; ctrCategory <= existingProducts.Count - 1; ctrCategory++)
            {
                if (existingProducts[ctrCategory] != null)
                {
                    for (int ctrDDLItems = 0; ctrDDLItems <= ddlKidsCategory.Items.Count - 1; ctrDDLItems++)
                    {
                        if (ddlKidsCategory.Items[ctrDDLItems].Value == existingProducts[ctrCategory].CategoryId.ToString())
                        {
                            ddlKidsCategory.Items[ctrDDLItems].Selected = true;
                        }
                    }
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
            else
            {

            }
        }

        public void SaveInfo()
        {
            SaveInfo(this.ProductId);
        }

        public void SaveInfo(int prodId)
        {
            Product product = this.ProductService.GetProductById(prodId);
            if (product != null)
            {
                //go through every value in each text box.  Also
                //include every parent category inside the final array.
                List<int> formCategories = new List<int>();
                
                List<ProductCategory> productCategories = product.ProductCategories;
                
                foreach (System.Web.UI.Control ctrl in this.Controls)
                {
                    if (ctrl.GetType().ToString() == typeof(HtmlSelect).ToString())
                    {
                        HtmlSelect ddl = (HtmlSelect)ctrl;
                        var initialCount = formCategories == null ? 0 : formCategories.Count;
                        formCategories.AddRange(GetSelectedCategories((HtmlSelect)ctrl));
                        if (initialCount != formCategories.Count)
                        {
                            switch (ctrl.ID)
                            {
                                case "ddlMensCategory":
                                    formCategories.Add(55);
                                    break;
                                case "ddlWomensCategory": 
                                    formCategories.Add(53);
                                    break;
                                case "ddlKidsCategory": 
                                    formCategories.Add(57);
                                    break;
                                default: break;
                            }
                        }
                    }

                }

                //then loop through the array and ensure every value
                //exists inside of the productcategory object.
                foreach (int ctrFormCategories in formCategories)
                {
                    Boolean matchFound = false;
                    foreach (ProductCategory prodCat in productCategories)
                    {
                        if (prodCat.CategoryId == ctrFormCategories){
                            matchFound = true;
                        }
                    }

                    if (!matchFound) { 
                        
                        //add to product categories.
                        var productCategory = new ProductCategory()
                        {
                            ProductId = product.ProductId,
                            CategoryId = ctrFormCategories,
                            IsFeaturedProduct = false,
                            DisplayOrder = 1
                        };
                        this.CategoryService.InsertProductCategory(productCategory);
                    }
                    
                }

                //if there is a productcategory in the product but not in the selection, remove it.
                foreach (ProductCategory prodCat in productCategories)
                {
                    bool matchFound = false;
                    foreach (int ctrFormCategories in formCategories)
                    {
                        if (prodCat.CategoryId == ctrFormCategories){
                            matchFound = true;
                        }
                    }

                    if (!matchFound)
                    {
                        this.CategoryService.DeleteProductCategory(prodCat.ProductCategoryId);
                    }
                }
            }
        }

        private List<int> GetSelectedCategories(HtmlSelect ddl)
        {
            List<int> returnItems = new List<int>();
            foreach (ListItem item in ddl.Items)
            {
                if (item.Selected)
                {
                    returnItems.Add(int.Parse(item.Value));
                }

            }


            return returnItems;
        }

        protected void gvCategoryMappings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.gvCategoryMappings.PageIndex = e.NewPageIndex;
            //this.BindData();
        }
       
        //private List<ProductCategoryMappingHelperClass> GetProductCategoryMappings(List<ProductCategory> existingProductCategoryCollection)
        //{
        //    var categories = this.CategoryService.GetAllCategories();

        //    this.CategoryService.GetAllCategoriesByParentCategoryId(0);
            

        //    List<ProductCategoryMappingHelperClass> result = new List<ProductCategoryMappingHelperClass>();
        //    for (int i = 0; i < categories.Count; i++)
        //    {
        //        Category category = categories[i];
        //        ProductCategory existingProductCategory = null;
        //        if (existingProductCategoryCollection != null)
        //            existingProductCategory = existingProductCategoryCollection.FindProductCategory(this.ProductId, category.CategoryId);
        //        ProductCategoryMappingHelperClass pcm = new ProductCategoryMappingHelperClass();
        //        if (existingProductCategory != null)
        //        {
        //            pcm.ProductCategoryId = existingProductCategory.ProductCategoryId;
        //            pcm.IsMapped = true;
        //            pcm.IsFeatured = existingProductCategory.IsFeaturedProduct;
        //            pcm.DisplayOrder = existingProductCategory.DisplayOrder;
        //        }
        //        else
        //        {
        //            pcm.DisplayOrder = 1;
        //        }
        //        pcm.CategoryId = category.CategoryId;
        //        pcm.CategoryInfo = GetCategoryFullName(category);


        //        result.Add(pcm);
        //    }

        //    return result;
        //}

        private List<ProductCategoryMappingHelperClass> GetProductCategoryMappings(List<ProductCategory> existingProductCategoryCollection, string categoryTypeName)
        {
            List<Category> categories = null;

            switch (categoryTypeName) {
            
                case "Mens":
                        categories = this.CategoryService.GetAllCategoriesByParentCategoryId(55);
                        break;
                case "Womens":
                        categories = this.CategoryService.GetAllCategoriesByParentCategoryId(53);
                        break;
                case "Kids":
                        categories = this.CategoryService.GetAllCategoriesByParentCategoryId(57);
                        break;
                default:
                    categories = this.CategoryService.GetAllCategories();
                    break;
            }
            
            List<ProductCategoryMappingHelperClass> result = new List<ProductCategoryMappingHelperClass>();
            for (int i = 0; i < categories.Count; i++)
            {
                Category category = categories[i];
                //ProductCategory existingProductCategory = null;
                //if (existingProductCategoryCollection != null)
                //    existingProductCategory = existingProductCategoryCollection.FindProductCategory(this.ProductId, category.CategoryId);
                ProductCategoryMappingHelperClass pcm = new ProductCategoryMappingHelperClass();
                //if (existingProductCategory != null)
                //{
                //    pcm.ProductCategoryId = existingProductCategory.ProductCategoryId;
                //    pcm.IsMapped = true;
                //    pcm.IsFeatured = existingProductCategory.IsFeaturedProduct;
                //    pcm.DisplayOrder = existingProductCategory.DisplayOrder;
                //}
                //else
                //{
                //    pcm.DisplayOrder = 1;
                //}
                pcm.CategoryId = category.CategoryId;
                pcm.CategoryInfo = category.Name;// GetCategoryFullName(category);


                result.Add(pcm);
            }

            return result;
        }

        protected string GetCategoryFullName(Category category)
        {
            string result = string.Empty;

            while (category != null && !category.Deleted)
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = category.Name + " >> " + result;
                }
                category = category.ParentCategory;
            }
            return result;
        }


        public int ProductId
        {
            get
            {
                return CommonHelper.QueryStringInt("ProductId");
            }
        }

        [Serializable]
        private class ProductCategoryMappingHelperClass
        {
            public int ProductCategoryId { get; set; }
            public int CategoryId { get; set; }
            public string CategoryInfo { get; set; }
            public bool IsMapped { get; set; }
            public bool IsFeatured { get; set; }
            public int DisplayOrder { get; set; }
        }
    }
}