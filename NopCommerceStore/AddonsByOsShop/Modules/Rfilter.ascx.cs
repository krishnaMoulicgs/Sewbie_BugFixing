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
    public partial class Rfilter : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void CreateChildControls()
        {
            CreateSubcategories();
            CreateManufacturers();
            CreateAttributes();
            CreateSpec();
            base.CreateChildControls();
        }

        protected void CreateSubcategories()
        {
            if (categories.Count == 0)
            {
                pnlSubCategories.Attributes.Add("style", "display:none");
            }
            else
            {
                foreach (Category c in categories)
                {
                    NopcommerceLi li = new NopcommerceLi();
                    li.HyperLink.Attributes.Add("class", "unSelected");
                    li.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnSubcategoryClick(this)");
                    li.HyperLink.Attributes.Add("href", "#" + c.CategoryId);
                    li.HyperLink.Text = Server.HtmlEncode(c.LocalizedName);
                    phCategories.Controls.Add(li);
                }
            }
        }

        protected void CreateManufacturers()
        {
            if (manufacturers.Count == 0)
            {
                pnlManufacturers.Attributes.Add("style", "display:none");
            }
            else
            {
                foreach (Manufacturer m in manufacturers)
                {
                    NopcommerceLi li = new NopcommerceLi();
                    li.HyperLink.Attributes.Add("class", "unSelected");
                    li.HyperLink.Attributes.Add("onclick", "return AjaxClient.OnManufacturerClick(this)");
                    li.HyperLink.Attributes.Add("href", "#" + m.ManufacturerId);
                    li.HyperLink.Text = Server.HtmlEncode(m.LocalizedName);
                    phManufacturers.Controls.Add(li);
                }
            }
        }

        public void CreateAttributes()
        {
            int i = 0;
            List<ProductAttribute> pac = ProductAttributeService.GetAllProductAttributes();
            foreach (ProductAttribute pa in pac)
            {
                DataTable dtAttributes = GetAttributeValueList(pa.ProductAttributeId);
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
                DataTable dt = GetSpecList(sa.SpecificationAttributeId);
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

        public DataTable GetAttributeValueList(int attrId)
        {
            StringBuilder sBulider = new StringBuilder();

            sBulider.Append("select distinct Name from Nop_ProductVariantAttributeValue ");
            sBulider.Append("where ProductVariantAttributeID ");
            sBulider.Append("in (");
            sBulider.Append("select ProductVariantAttributeID from Nop_ProductVariant_ProductAttribute_Mapping ");
            sBulider.Append("where ProductAttributeID = " + attrId.ToString() + " and ProductVariantID ");
            sBulider.Append("in (");
            sBulider.Append("select ProductVariantId from Nop_ProductVariant where ProductId "); 
            sBulider.Append("in (");
            sBulider.Append("SELECT TOP 30 ProductId from Nop_Product where deleted = 0 order by CreatedOn desc )))");

            string connectionStr = NopSolutions.NopCommerce.BusinessLogic.Configuration.NopConfig.ConnectionString;
            SqlDatabase db = new SqlDatabase(connectionStr);
            DbCommand dbCommand = db.GetSqlStringCommand(sBulider.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }

        public DataTable GetSpecList(int SpecificationAttributeID)
        {
            StringBuilder sBulider = new StringBuilder();
            sBulider.Append("select SpecificationAttributeOptionID, Name from Nop_SpecificationAttributeOption");
            sBulider.Append(" where SpecificationAttributeID =" + SpecificationAttributeID.ToString());
            sBulider.Append(" and SpecificationAttributeOptionID");
            sBulider.Append(" in (select distinct SpecificationAttributeOptionID from Nop_Product_SpecificationAttribute_Mapping where AllowFiltering=1 AND ProductID ");
            sBulider.Append(" in (select top 30 productId from Nop_Product where Deleted = 0 order by CreatedOn desc))");

            string connectionStr = NopSolutions.NopCommerce.BusinessLogic.Configuration.NopConfig.ConnectionString;
            SqlDatabase db = new SqlDatabase(connectionStr);
            DbCommand dbCommand = db.GetSqlStringCommand(sBulider.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }

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

        public List<Manufacturer> manufacturers
        {
            get
            {
                List<Manufacturer> list = new List<Manufacturer>();
                foreach (Product p in products)
                {
                    foreach (ProductManufacturer pm in p.ProductManufacturers)
                    {
                        if (list.Contains(pm.Manufacturer))
                        {
                            continue;
                        }
                        else
                        {
                            list.Add(pm.Manufacturer);
                        }
                    }
                }

                return list;
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

        public int PageSize
        {
            get
            {
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
                    DataTable dt = GetAttributeValueList(pa.ProductAttributeId);
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
                    DataTable dt = GetSpecList(SA.SpecificationAttributeId);
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