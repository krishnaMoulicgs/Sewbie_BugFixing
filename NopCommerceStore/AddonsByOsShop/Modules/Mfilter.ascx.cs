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
    public partial class Mfilter : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            txtManufacturerId.Text = this.ManufacturerId.ToString();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateSubcategories();
            CreateAttributes();
            CreateSpec();
            base.CreateChildControls();
        }

        protected void CreateSubcategories()
        {
            if (CategoryIds.Count == 0)
            {
                pnlSubCategories.Attributes.Add("style", "display:none");
                return;
            }
            else
            {
                //如果要是从 列表导航跳转过来的(只有一个ID)
                string manuTransId = Request.QueryString["manufacturerTransId"];

                foreach (int Id in CategoryIds)
                {
                    bool choiceSign = false;
                    NopcommerceLi li = new NopcommerceLi();
                    if (manuTransId != "" && manuTransId != null)
                    {
                        foreach (int motId in getCategoryByCatecoryId(int.Parse(manuTransId)))
                        {
                            //如果要是有相等的话就 打钩
                            if (Id == motId)
                            {
                                li.HyperLink.Attributes.Add("class", "selected");
                                li.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnSubcategoryClick(this)");
                                li.HyperLink.Attributes.Add("href", "#" + Id);
                                li.HyperLink.Text = Server.HtmlEncode(CategoryService.GetCategoryById(Id).Name);
                                choiceSign = true;
                                break;
                            }
                        }

                    }

                    if (choiceSign == false)
                    {
                        li.HyperLink.Attributes.Add("class", "unSelected");
                        li.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnSubcategoryClick(this)");
                        li.HyperLink.Attributes.Add("href", "#" + Id);
                        li.HyperLink.Text = Server.HtmlEncode(CategoryService.GetCategoryById(Id).Name);
                    }

                    phCategories.Controls.Add(li);
                }
            }
        }


        //获取母类别下的所有类别
        public List<int> getCategoryByCatecoryId(int productCategoryId)
        {
            List<Category> categoryCollection = CategoryService.GetAllCategoriesByParentCategoryId(productCategoryId);
            List<int> Ids = new List<int>();
            //加入主类别
            Ids.Add(productCategoryId);
            foreach (Category category in categoryCollection)
            {
                //将该类别下的 类别名称存入xml
                Ids.Add(category.CategoryId);
                List<Category> subCategoryCollection = CategoryService.GetAllCategoriesByParentCategoryId(category.CategoryId);
                foreach (Category category1 in subCategoryCollection)
                {
                    Ids.Add(category1.CategoryId);
                   
                }
            }
            return Ids;

        }

        public void CreateAttributes()
        {
            int i = 0;
            List<ProductAttribute> pac = ProductAttributeService.GetAllProductAttributes();
            foreach (ProductAttribute pa in pac)
            {
                DataTable dtAttributes = GetAttributeValueList(ManufacturerId, pa.ProductAttributeId);
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
                DataTable dt = GetSpecList(ManufacturerId, sa.SpecificationAttributeId);
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
            sBulider.Append("SELECT DISTINCT Name FROM Nop_ProductVariantAttributeValue");
            sBulider.Append(" WHERE ProductVariantAttributeID ");
            sBulider.Append(" IN ( SELECT pvp.ProductVariantAttributeID FROM Nop_ProductVariant_ProductAttribute_Mapping pvp ");
            sBulider.Append(" WHERE pvp.ProductAttributeID =" + attrId.ToString() + " AND pvp.ProductVariantID ");
            sBulider.Append(" IN( SELECT ProductVariantID FROM Nop_ProductVariant WHERE ProductID ");
            sBulider.Append(" IN( SELECT ProductId from Nop_Product where ProductId ");
            sBulider.Append(" in (select ProductId from Nop_Product_Manufacturer_Mapping ");
            sBulider.Append(" where ManufacturerID =" + Id.ToString() + "))" + " and deleted = 0" + "))");

            string connectionStr = NopSolutions.NopCommerce.BusinessLogic.Configuration.NopConfig.ConnectionString;
            SqlDatabase db = new SqlDatabase(connectionStr);
            DbCommand dbCommand = db.GetSqlStringCommand(sBulider.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }

        public DataTable GetSpecList(int Id, int SpecificationAttributeID)
        {
            StringBuilder sBulider = new StringBuilder();
            sBulider.Append("select SpecificationAttributeOptionId, Name from Nop_SpecificationAttributeOption");
            sBulider.Append(" where SpecificationAttributeID =" + SpecificationAttributeID.ToString());
            sBulider.Append(" and SpecificationAttributeOptionID");
            sBulider.Append(" in ( select distinct SpecificationAttributeOptionID from Nop_Product_SpecificationAttribute_Mapping where AllowFiltering = 1 and ProductId");
            sBulider.Append(" in (select ProductId from Nop_Product_Manufacturer_Mapping ");
            sBulider.Append(" where ManufacturerID =" + Id.ToString() + "))");

            string connectionStr = NopSolutions.NopCommerce.BusinessLogic.Configuration.NopConfig.ConnectionString;
            SqlDatabase db = new SqlDatabase(connectionStr);
            DbCommand dbCommand = db.GetSqlStringCommand(sBulider.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }

        #region [properties]
        public int ManufacturerId
        {
            get
            {
                return CommonHelper.QueryStringInt("ManufacturerId");
            }
        }

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

        public List<int> CategoryIds
        {
            get
            {
                List<Product> products = new List<Product>();
                List<int> Ids = new List<int>();
                int totalRecords;
                products.AddRange(ProductService.GetAllProducts(0, this.ManufacturerId, 0, 0, false, 0, 0, out totalRecords));

                foreach (Product p in products)
                {
                    foreach (ProductCategory pc in p.ProductCategories)
                    {
                        if (Ids.Contains(pc.CategoryId))
                        {
                            continue;
                        }
                        else
                        {
                            Ids.Add(pc.CategoryId);
                        }
                    }
                }

                return Ids;
            }
        }

        protected decimal _minPrice = Decimal.Zero;
        protected decimal _maxPrice = Decimal.Zero;

        protected decimal minPrice
        {
            get
            {
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

        protected List<Product> products
        {
            get
            {
                List<Product> list = new List<Product>();
                int totalRecords;
                list.AddRange(ProductService.GetAllProducts(0, this.ManufacturerId, 0, 0, false, 1000, 0, out totalRecords));

                return list;
            }
        }

        public int PageSize
        {
            get
            {
                Manufacturer m = ManufacturerService.GetManufacturerById(ManufacturerId);

                if (m.PageSize != 0)
                    return m.PageSize;
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
                    DataTable dt = GetAttributeValueList(ManufacturerId, pa.ProductAttributeId);
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
                    DataTable dt = GetSpecList(ManufacturerId, SA.SpecificationAttributeId);
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