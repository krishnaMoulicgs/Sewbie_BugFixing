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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.Common.Utils;
using System.Xml;

namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class HeaderMenuControl : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.Contains("/RecentlyAddedProducts.aspx"))
            {
                Menu_RecentlyAddedProducts.CssClass = "menuActive";
            }
            else if (Request.Url.AbsolutePath.Contains("/Manufacturers.aspx"))
            {
                //Menu_AllBrands.CssClass = "menuActive";
            }
            else if (Request.Url.AbsolutePath.Contains("/Blog.aspx"))
            {
                Menu_Blog.CssClass = "menuActive";
            }
            else if (Request.Url.AbsolutePath.Contains("/Boards"))
            {
                Menu_Forum.CssClass = "menuActive";
            }
        }
        private string getProductId(Category category)
        {
            string productIdStrValue = "";
            var productCategoryCollention = category.ProductCategories;
            foreach (ProductCategory productCategory in productCategoryCollention)
            {
                productIdStrValue += productCategory.Product.ProductId;
                productIdStrValue += "|";
            }
            return productIdStrValue;
        }


        public int MenuSelect
        {
            get
            {
                int CheckedValue = 0;
                
                XmlDocument xmlDoc;
                xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/Administration/OsShopAdmin/MenuChoice.xml"));

                XmlNodeList nodeList = xmlDoc.SelectSingleNode("MenuRoot").ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    string menuName = node.Name;
                    if (menuName == "MenuChoice")
                    {
                        CheckedValue = int.Parse(node.InnerText);
                    }
                }
                //xmlDoc.Save(Server.MapPath("~/Administration/OsShopAdmin/MenuChoice.xml"));
                
                return CheckedValue;
            }
        }

        public string Menu_Category
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Category category in categoryCollection)
                {
                    sb.Append("<li class='_nav MenuElite'>");
                    sb.Append("<a id='menu_" + category.CategoryId + "_a' href='" + SEOHelper.GetCategoryUrl(category) + "'>" + category.Name + "</a>");
                    List<Category> subCategoryCollection = CategoryService.GetAllCategoriesByParentCategoryId(category.CategoryId);
                    if (subCategoryCollection.Count != 0)
                    {
                        sb.Append("<ul class='_nav1'>");
                        foreach (Category subCategory in subCategoryCollection)
                        {
                            sb.Append("<li class='_nav1'><a  href='" + SEOHelper.GetCategoryUrl(subCategory) + "'>" + subCategory.Name + "</a>");
                            //.............................................................
                            List<Category> subCategoryCollection1 = CategoryService.GetAllCategoriesByParentCategoryId(subCategory.CategoryId);
                            if (subCategoryCollection1.Count != 0)
                            {
                                sb.Append("<ul class='_nav2'>");
                                foreach (Category subCategory1 in subCategoryCollection1)
                                {
                                    sb.Append("<li class='_nav2'><a href='" + SEOHelper.GetCategoryUrl(subCategory1) + "'>" + subCategory1.Name + "</a>"); sb.Append("</li>");
                                }
                                sb.Append("</ul>");
                            }
                            sb.Append("</li>");
                            //.............................................................
                        }

                        sb.Append("</ul>");
                    }
                    sb.Append("</li>");
                }
                return sb.ToString();
            }
        }


        public string AllBrands
        {
            get
            {
                var manufacturers = ManufacturerService.GetAllManufacturers();
                StringBuilder sb = new StringBuilder();

                sb.Append("<a  href='/Manufacturers.aspx'>" + GetLocaleResourceString("OsShop.AllBrands") + "</a>");
                sb.Append("<ul class='ManufacturersNav'>");
                foreach (Manufacturer manufacturer in manufacturers)
                {

                    sb.Append("<li>");
                    sb.Append("<a id='menu_" + manufacturer.ManufacturerId + "_a' href='" + SEOHelper.GetManufacturerUrl(manufacturer) + "'>" + manufacturer.Name + "</a>");
                    sb.Append("</li>");

                }
                sb.Append("</ul>");

                return sb.ToString();
            }
        }



        public List<Category> categoryCollection
        {
            get
            {
                return CategoryService.GetAllCategoriesByParentCategoryId(0);
            }
        }
    }
}