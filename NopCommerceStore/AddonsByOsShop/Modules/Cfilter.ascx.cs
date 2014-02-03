using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.Products.Specs;
using NopSolutions.NopCommerce.Web.AddonsByOsShop.Classes;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules
{
    public partial class Cfilter : BaseNopUserControl
    {


        #region [override]
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            txtCategoryId.Text = this.CategoryId.ToString();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateSubcategories();
            CreateManufacturers();
            CreateAttributes();
            CreateSpec();
            base.CreateChildControls();
        }
        #endregion

        #region [common methods]
        protected void CreateSubcategories()
        {
            Category currentCategory = CategoryService.GetCategoryById(CategoryId);

            //if current category doesn't have subcategories, hide the category filter
            if (CategoryService.GetAllCategoriesByParentCategoryId(CategoryId).Count == 0)
            {
                pnlSubCategories.Attributes.Add("style", "display: none");
                return;
            }
            else
            {
                List<Category> subCategories = new List<Category>();
                if (currentCategory != null)
                {
                    subCategories = CategoryService.GetAllCategoriesByParentCategoryId(CategoryId);
                }

                //Render subcategories
                foreach (Category subCategory in subCategories)
                {
                    NopcommerceLi li = new NopcommerceLi();
                    li.HyperLink.Attributes.Add("class", "unSelected");
                    li.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnSubcategoryClick(this)");
                    li.HyperLink.Attributes.Add("href", "#" + subCategory.CategoryId);
                    li.HyperLink.Text = Server.HtmlEncode(subCategory.LocalizedName);
                    phCategories.Controls.Add(li);
                }
            }
        }

        protected void CreateManufacturers()
        {
            int totalRecords;
            List<Product> ps = new List<Product>();
            List<Manufacturer> ms = new List<Manufacturer>();
            List<int> Ids = new List<int>();


            if (CategoryService.GetCategoryById(CategoryId) != null)
            {
                foreach (int Id in getSubcategoryIds(this.CategoryId))
                {
                    ps.AddRange(ProductService.GetAllProducts(Id, 0, 0, 0, false, 0, 0, out totalRecords));
                }
                ps.AddRange(ProductService.GetAllProducts(CategoryId, 0, 0, 0, false, 0, 0, out totalRecords));

                foreach (Product p in ps)
                {
                    foreach (ProductManufacturer pm in p.ProductManufacturers)
                    {
                        if (!ms.Contains(pm.Manufacturer))
                        {
                            ms.Add(pm.Manufacturer);
                        }
                    }
                }

                if (ms.Count == 0)
                {
                    pnlManufacturers.Attributes.Add("style", "display:none");
                }
                else
                {
                    foreach (Manufacturer m in ms)
                    {
                        NopcommerceLi li = new NopcommerceLi();
                        li.HyperLink.Attributes.Add("class", "unSelected");
                        li.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnManufacturerClick(this)");
                        li.HyperLink.Attributes.Add("href", "#" + m.ManufacturerId);
                        li.HyperLink.Text = Server.HtmlEncode(m.LocalizedName);
                        phManufacturers.Controls.Add(li);
                    }
                }
                return;
            }
        }

        public void CreateAttributes()
        {
            int i = 0;
            List<ProductAttribute> pac = ProductAttributeService.GetAllProductAttributes();
            foreach (ProductAttribute pa in pac)
            {
                DataTable dtAttributes = GetAttributeValueList(CategoryId, pa.ProductAttributeId);
                if (dtAttributes.Rows.Count == 0)
                {
                    continue;
                }
                Control controlExam = LoadControl("~/AddonsByOsShop/Modules/CproductAttribute.ascx");
                this.pnlAttributes.Controls.Add(controlExam);
                CproductAttribute psmi = (CproductAttribute)controlExam;
                psmi.pannelTitle = "refine";
                psmi.ID = pa.Name;
                psmi.title = pa.Name;
                psmi.titleIndex = i.ToString();
                psmi.titleId = pa.ProductAttributeId.ToString();
                psmi.dataSource = dtAttributes;
                psmi.DataBind();
                i++;
            }

            attr_num.Text = i.ToString();
        }

        public void CreateSpec()
        {
            List<SpecificationAttribute> sac = SpecificationAttributeService.GetSpecificationAttributes();
            int i = 0;
            foreach (SpecificationAttribute sa in sac)
            {
                DataTable dt = GetSpecList(CategoryId, sa.SpecificationAttributeId);
                if (dt.Rows.Count == 0)
                {
                    continue;
                }
                Control controlExam = LoadControl("~/AddonsByOsShop/Modules/CproductSpec.ascx");
                this.pnlSpec.Controls.Add(controlExam);
                CproductSpec psmi = (CproductSpec)controlExam;
                psmi.pannelTitle = "render";
                psmi.ID = sa.Name;
                psmi.title = sa.Name;
                psmi.titleIndex = i.ToString();
                psmi.titleId = sa.SpecificationAttributeId.ToString();
                psmi.dataSource = dt;
                psmi.DataBind();
                i++;
            }

            spec_num.Text = i.ToString();
        }

        public DataTable GetAttributeValueList(int Id, int attrId)
        {
            StringBuilder sBulider = new StringBuilder();
            sBulider.Append("SELECT DISTINCT pvav.Name FROM Nop_ProductVariantAttributeValue pvav");
            sBulider.Append(" WHERE pvav.ProductVariantAttributeID ");
            sBulider.Append(" IN ( SELECT pvp.ProductVariantAttributeID FROM Nop_ProductVariant_ProductAttribute_Mapping pvp ");
            sBulider.Append(" WHERE pvp.ProductAttributeID =" + attrId.ToString() + " AND pvp.ProductVariantID ");
            sBulider.Append(" IN( SELECT pv.ProductVariantID FROM Nop_ProductVariant pv WHERE pv.ProductID ");
            sBulider.Append(" IN( SELECT ProductId from Nop_Product where ProductId "); 
            sBulider.Append(" IN (SELECT pc.ProductID from Nop_Product_Category_Mapping pc ");
            sBulider.Append(" WHERE pc.CategoryId in (select cate.CategoryID from Nop_Category cate ");
            sBulider.Append(" WHERE cate.CategoryID=" + Id.ToString() + " or cate.ParentCategoryID=" + Id.ToString() + "))"+ " and deleted = 0" + ")))");





            string connectionStr = NopSolutions.NopCommerce.BusinessLogic.Configuration.NopConfig.ConnectionString;
            SqlDatabase db = new SqlDatabase(connectionStr);
            DbCommand dbCommand = db.GetSqlStringCommand(sBulider.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }

        public DataTable GetSpecList(int Id, int SpecificationAttributeID)
        {
            StringBuilder sBulider = new StringBuilder();
            sBulider.Append("SELECT NSAO.SpecificationAttributeOptionID,NSAO.Name FROM Nop_SpecificationAttributeOption NSAO");
            sBulider.Append(" WHERE NSAO.SpecificationAttributeID =" + SpecificationAttributeID.ToString());
            sBulider.Append(" AND NSAO.SpecificationAttributeOptionID");
            sBulider.Append(" IN( SELECT DISTINCT NPSAM.SpecificationAttributeOptionID FROM Nop_Product_SpecificationAttribute_Mapping NPSAM WHERE NPSAM.AllowFiltering=1 AND NPSAM.ProductID ");
            sBulider.Append(" IN (SELECT NPCM.ProductID from Nop_Product_Category_Mapping NPCM ");
            sBulider.Append(" WHERE NPCM.CategoryId in (select NC.CategoryID from Nop_Category NC ");
            sBulider.Append(" where NC.CategoryID=" + Id.ToString() + " or NC.ParentCategoryID=" + Id.ToString() + ")))");

            string connectionStr = NopSolutions.NopCommerce.BusinessLogic.Configuration.NopConfig.ConnectionString;
            SqlDatabase db = new SqlDatabase(connectionStr);
            DbCommand dbCommand = db.GetSqlStringCommand(sBulider.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }

        #endregion

        #region [properties]
        public string CurrencySymbol
        {
            get
            {
                if (NopContext.Current.WorkingCurrency.Name == "Euro")
                {
                    return "€";
                }
                else
                    return new CultureInfo(NopContext.Current.WorkingCurrency.DisplayLocale).NumberFormat.CurrencySymbol;
            }
        }

        public int CategoryId
        {
            get
            {
                return CommonHelper.QueryStringInt("CategoryId");
            }
        }

        public List<int> ManufacturerIds
        {
            get
            {
                List<int> ms = new List<int>();
                string ManufacturersSelected = CommonHelper.QueryString("urlManufacturersSelected");
                if (string.IsNullOrEmpty(ManufacturersSelected))
                {
                    string[] Ids = ManufacturersSelected.Split('|');
                    foreach (string Id in Ids)
                    {
                        int result;
                        Int32.TryParse(Id, out result);
                        ms.Add(result);
                    }
                    return ms;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<int> getSubcategoryIds(int CategoryId)
        {
            List<int> Ids = new List<int>();
            List<Category> categories = CategoryService.GetAllCategoriesByParentCategoryId(CategoryId);

            foreach (Category c in categories)
            {
                Ids.Add(c.CategoryId);
            }

            return Ids;
        }

        protected decimal _minPrice = Decimal.Zero;
        protected decimal _maxPrice = Decimal.Zero;

        protected decimal minPrice
        {
            get
            {
                List<Product> products = this.GetAllProductsByCategories(CategoryId);
                if (products.Count > 0)
                {
                    _minPrice = products[0].ProductVariants[0].Price;
                    foreach (Product p in products)
                    {
                        foreach (ProductVariant pv in p.ProductVariants)
                        {
                            if (_minPrice > pv.Price)
                            {
                                _minPrice = pv.Price;
                            }
                        }
                    }
                    _minPrice = CurrencyService.ConvertCurrency(_minPrice, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                    if (_minPrice == maxPrice)
                        _minPrice = 0;
                    return _minPrice;
                }
                else
                {
                    return Decimal.Zero;
                }
            }
        }

        protected decimal maxPrice
        {
            get
            {
                List<Product> products = GetAllProductsByCategories(CategoryId);
                if (products.Count > 0)
                {
                    _maxPrice = products[0].ProductVariants[0].Price;
                    foreach (Product p in products)
                    {
                        foreach (ProductVariant pv in p.ProductVariants)
                        {
                            if (_maxPrice < pv.Price)
                            {
                                _maxPrice = pv.Price;
                            }
                        }
                    }
                    _maxPrice = CurrencyService.ConvertCurrency(_maxPrice, CurrencyService.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                    return _maxPrice;
                }
                else
                {
                    return Decimal.Zero;
                }
            }
        }

        protected List<Product> GetAllProductsByCategories(int categoryId)
        {
            List<Category> pcc = CategoryService.GetAllCategoriesByParentCategoryId(CategoryId);
            List<Product> newProductCollection = new List<Product>();
            int totalRecords;
            foreach (Category c in pcc)
            {
                List<Product> pc = ProductService.GetAllProducts(c.CategoryId,
                    0, 0, 0, false, 1000, 0, out totalRecords);
                newProductCollection.AddRange(pc);
            }
            List<Product> pc1 = ProductService.GetAllProducts(categoryId,
                   0, 0, 0, false, 1000, 0, out totalRecords);
            newProductCollection.AddRange(pc1);
            return newProductCollection;
        }

        public int PageSize
        {
            get
            {
                Category c = CategoryService.GetCategoryById(CategoryId);

                if (c.PageSize != 0)
                    return c.PageSize;
                else
                    return 12;
            }
        }

        public string AttJs
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                List<ProductAttribute> pas = ProductAttributeService.GetAllProductAttributes();
                foreach (ProductAttribute pa in pas)
                {
                    DataTable dt = GetAttributeValueList(CategoryId, pa.ProductAttributeId);
                    if (dt.Rows.Count == 0)
                    {
                        continue;
                    }

                    sb.Append("AjaxClient.AttrSelected('refineBy" + pa.ProductAttributeId + "')");
                    sb.Append("+");
                }
                sb.Append("''");
                return sb.ToString();
            }
        }

        public string SpeJs
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                List<SpecificationAttribute> sac = SpecificationAttributeService.GetSpecificationAttributes();
                int i = 0;
                foreach (SpecificationAttribute SA in sac)
                {
                    DataTable dt = GetSpecList(CategoryId, SA.SpecificationAttributeId);
                    if (dt.Rows.Count == 0)
                    {
                        continue;
                    }
                    sb.Append("AjaxClient.AttrSelected('refineSpeBy" + SA.SpecificationAttributeId + "')");
                    sb.Append("+");
                }
                sb.Append("''");
                return sb.ToString();
            }
        }
        #endregion
    }
}