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

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AjaxMenuPro
{
    public partial class HeadMenuPro : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

        public void createNewMenu()
        {
            try
            {
                //Three categories obtain the commodity ID under each category in the 
                //commodity ID commodity manufacturers and labels
                string strID = "", strManufacturer = "", strTags = "";
                //XML Start
                XmlDocument xmlDoc;
                xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/AddonsByOsShop/UserExperience/AjaxMenuPro/MenuList.xml"));
                XmlNode xmldocSelect = xmlDoc.SelectSingleNode("MenuRoot");
                xmldocSelect.RemoveAll();//When re-generated to delete navigation list
                foreach (Category category in categoryCollection)
                {
                    XmlElement menuValue = xmlDoc.CreateElement("MenuValue");//Create a root category
                    //The first layer to get the goods under this category ID
                    strID += getProductId(category);

                    //This category under the category name stored in xml
                    string categoryOneName = category.LocalizedName;
                    string categoryOneURL = SEOHelper.GetCategoryUrl(category.CategoryId);
                    XmlElement productCategoryOne = xmlDoc.CreateElement("ProductCategory");
                    productCategoryOne.InnerText = categoryOneName + "|" + categoryOneURL;
                    menuValue.AppendChild(productCategoryOne);

                    List<Category> subCategoryCollection = CategoryService.GetAllCategoriesByParentCategoryId(category.CategoryId);
                    if (subCategoryCollection.Count != 0)
                    {
                        //Second layer
                        foreach (Category category2 in subCategoryCollection)
                        {
                            strID += getProductId(category2);

                            //This category under the category name stored in xml
                            string categoryTwoName = category2.LocalizedName;
                            string categoryTwoURL = SEOHelper.GetCategoryUrl(category2.CategoryId);
                            XmlElement productCategoryTwo = xmlDoc.CreateElement("ProductCategory");
                            productCategoryTwo.InnerText = categoryTwoName + "|" + categoryTwoURL;
                            menuValue.AppendChild(productCategoryTwo);

                            List<Category> subCategoryCollection2 = CategoryService.GetAllCategoriesByParentCategoryId(category2.CategoryId);
                            if (subCategoryCollection2.Count != 0)
                            {
                                //第三层
                                foreach (Category category3 in subCategoryCollection2)
                                {
                                    strID += getProductId(category3);
                                    //将该类别下的 类别名称存入xml
                                    string categoryThreeName = category3.LocalizedName;
                                    string categoryThreeURL = SEOHelper.GetCategoryUrl(category3.CategoryId);
                                    XmlElement productCategoryThree = xmlDoc.CreateElement("ProductCategory");
                                    productCategoryThree.InnerText = categoryThreeName + "|" + categoryThreeURL;
                                    menuValue.AppendChild(productCategoryThree);

                                }
                            }
                        }

                    }

                    //根据商品的ID获得商品的 厂商
                    string[] productIdValue = strID.Split('|');

                    //获取厂商
                    for (int i = 0; i < productIdValue.Length - 1; i++)
                    {
                        var manufacturerCollection = ManufacturerService.GetProductManufacturersByProductId(int.Parse(productIdValue[i]));
                        foreach (ProductManufacturer manufacturer in manufacturerCollection)
                        {
                            //每个产品对应的 厂商名字
                            string manufacturerName = manufacturer.Manufacturer.LocalizedName;
                            string manufacturerURL = SEOHelper.GetManufacturerUrl(manufacturer.ManufacturerId) + "?manufacturerTransId=" + category.CategoryId;
                            //将得到的厂商名字和链接读到XML中 如果厂商已存在XML中则不再添加
                            XmlElement manufacturesNode = xmlDoc.CreateElement("Manufactures");
                            manufacturesNode.InnerText = manufacturerName + "|" + manufacturerURL;
                            menuValue.AppendChild(manufacturesNode);


                            strManufacturer += manufacturerName;
                        }
                    }

                    //获取标签
                    for (int j = 0; j < productIdValue.Length - 1; j++)
                    {
                        var productTags = ProductService.GetProductTagsByProductId(int.Parse(productIdValue[j]));
                        //var productTags = ProductManager.GetAllProductTags(int.Parse(productIdValue[j]), string.Empty);
                        for (int k = 0; k < productTags.Count; k++)
                        {
                            string productTagsName = productTags[k].Name;
                            string productTagsURL = CommonHelper.GetStoreLocation() + "producttag.aspx?tagid=" + productTags[k].ProductTagId + "&manufacturerTransId=" + category.CategoryId;
                            //将得到的标签读入XML中 如果已经存在则不再添加
                            XmlElement getInspiredNode = xmlDoc.CreateElement("GetInspired");
                            getInspiredNode.InnerText = productTagsName + "|" + productTagsURL;
                            menuValue.AppendChild(getInspiredNode);

                            strTags += productTagsName;
                        }
                    }

                    xmldocSelect.AppendChild(menuValue);
                    //下一个类别 重新清零
                    strID = "";
                    strManufacturer = "";
                    strTags = "";
                }
                //关闭xml
                //xmlDoc.Save(Server.MapPath("~/AddonsByOsShop/UserExperience/AjaxMenuPro/MenuList.xml"));
            }
            catch (Exception eee)
            {
                Response.Write(eee.Message);
            }
        }

        public string Menu_Value
        {
            get
            {
                StringBuilder sb = new StringBuilder();//存储
                StringBuilder menuTrans = new StringBuilder();//menuTrans存储母类别的信息
                StringBuilder ProductCategoryTrans = new StringBuilder();
                StringBuilder ManufacturesTrans = new StringBuilder();
                StringBuilder GetInspiredTrans = new StringBuilder();
                int MenuNumSign = 0;
                //引入XML
                XmlDocument xmlDoc;
                xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/AddonsByOsShop/UserExperience/AjaxMenuPro/MenuList.xml"));

                XmlNodeList nodeList = xmlDoc.SelectSingleNode("MenuRoot").ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    int categoryCount = 0;
                    int manufacturesCount = 0;
                    int getInspiredCount = 0;
                    XmlNodeList nodeList1 = node.ChildNodes;
                    foreach (XmlNode node1 in nodeList1)
                    {
                        MenuNumSign++;
                        string menuName = node1.Name;
                        if (menuName == "ProductCategory")
                        {
                            string[] menuNameValue = node1.InnerText.Split('|');
                            string transSignValue = ProductCategoryTrans.ToString();
                            if (menuTrans.ToString() == "")
                            {
                                menuTrans.Append("<li id='floor_" + MenuNumSign.ToString() + "'><a class='replace selected' href='" + menuNameValue[1] + "'>" + menuNameValue[0] + "</a>");

                                sb.Append(menuTrans.ToString());
                                sb.Append("<div id='sub_floor_" + MenuNumSign.ToString() + "' class='sub-floor-menus' style='display: none; left: 21px;' >");

                            }
                            else
                            {
                                //不存在
                                if (transSignValue.IndexOf(menuNameValue[0].ToString().Trim()) == -1 && transSignValue.IndexOf(menuNameValue[1].ToString().Trim()) == -1)
                                {
                                    ProductCategoryTrans.Append("<li><a href='" + menuNameValue[1] + "'>" + menuNameValue[0] + "</a></li>");
                                    categoryCount++;
                                    if (categoryCount >= 20)
                                    {
                                        categoryCount = 0;
                                        ProductCategoryTrans.Append("|");
                                    }
                                }
                            }

                        }
                        if (menuName == "Manufactures")
                        {
                            string[] ManufacturesValue = node1.InnerText.Split('|');
                            //看该种类是否已经存在了
                            string transSignValue = ManufacturesTrans.ToString();
                            //不存在
                            if (transSignValue.IndexOf(ManufacturesValue[0].ToString().Trim()) == -1 && transSignValue.IndexOf(ManufacturesValue[1].ToString().Trim()) == -1)
                            {
                                ManufacturesTrans.Append("<li><a href='" + ManufacturesValue[1] + "'>" + ManufacturesValue[0] + "</a></li>");
                                manufacturesCount++;
                                if (manufacturesCount >= 20)
                                {
                                    manufacturesCount = 0;
                                    ManufacturesTrans.Append("|");
                                }
                            }

                        }
                        if (menuName == "GetInspired")
                        {
                            string[] GetInspiredValue = node1.InnerText.Split('|');
                            //看该种类是否已经存在了
                            string transSignValue = GetInspiredTrans.ToString();
                            //不存在
                            if (transSignValue.IndexOf(GetInspiredValue[0].ToString().Trim()) == -1 && transSignValue.IndexOf(GetInspiredValue[1].ToString().Trim()) == -1)
                            {
                                GetInspiredTrans.Append("<li><a href='" + GetInspiredValue[1] + "'>" + GetInspiredValue[0] + "</a></li>");
                                getInspiredCount++;
                                if (getInspiredCount >= 20)
                                {
                                    getInspiredCount = 0;
                                    GetInspiredTrans.Append("|");
                                }
                            }
                        }
                    }

                    sb.Append("<div class='sub-menu-wrapper-left'>");
                    sb.Append("<div class='sub-menu-wrapper-right'>");
                    sb.Append("<div class='sub-menu-wrapper-bottom'>");
                    sb.Append("<div class='sub-menu-wrapper-top'>");
                    sb.Append("<div class='sub-menu-wrapper'>");
                    sb.Append("<dl class='section'>");
                    sb.Append("<dt>" + GetLocaleResourceString("OsShop.SHOPBYPRODUCT") + "</dt>");
                    sb.Append("<dd>");

                   // sb.Append("<ul class='items'>");
                   // sb.Append(ProductCategoryTrans.ToString());
                   // sb.Append("</ul>");

                    string[] categoryLen = ProductCategoryTrans.ToString().Split('|');
                    for (int i = 0; i < categoryLen.Length; i++)
                    {
                        if (categoryLen[i].ToString().Trim() != "")
                        {
                            sb.Append("<ul class='items'>");
                            sb.Append(categoryLen[i].ToString());
                            sb.Append("</ul>");
                        }
                        

                        //$("div.sub-floor-menus").attr("width",180);

                    }


                    sb.Append("</dd>");
                    sb.Append("</dl>");
                    // Alex Garcia - 8/17/2011 - remove the brands
                    sb.Append("<dl style='display:none' class='section'>");
                    sb.Append("<dt style='display:none' >" + GetLocaleResourceString("OsShop.BRANDS") + "</dt>");
                    sb.Append("<dd>");



                    string[] manufacturesLen = ManufacturesTrans.ToString().Split('|');
                    for (int j = 0; j < manufacturesLen.Length; j++)
                    {
                        if (manufacturesLen[j].ToString().Trim() != "")
                        {
                            sb.Append("<ul class='items'>");
                            sb.Append(manufacturesLen[j].ToString());
                            sb.Append("</ul>");
                        }
                        //$("div.sub-floor-menus").attr("width",180);

                    }


                    sb.Append("</dd>");
                    sb.Append("</dl>");
                    // Alex Garcia - 8/17/2011 - remove the inspired
                    sb.Append("<dl style='display:none' class='section'>");
                    sb.Append("<dt  style='display:none'>" + GetLocaleResourceString("OsShop.GETINSPIRED") + "</dt>");
                    sb.Append("<dd>");


                
                    string[] getInspiredLen = GetInspiredTrans.ToString().Split('|');
                    for (int k = 0; k < getInspiredLen.Length; k++)
                    {
                        if (getInspiredLen[k].ToString().Trim() != "")
                        {
                            sb.Append("<ul class='items'>");
                            sb.Append(getInspiredLen[k].ToString());
                            sb.Append("</ul>");
                        }
                        //$("div.sub-floor-menus").attr("width",180);

                    }


                    sb.Append("</dd>");
                    sb.Append("</dl>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    sb.Append("</div></li>");//结束menuContent的div
                    //清空 以存储下一个类别
                    ProductCategoryTrans.Clear();
                    ManufacturesTrans.Clear();
                    GetInspiredTrans.Clear();
                    menuTrans.Clear();
                }



                //关闭xml
                //xmlDoc.Save(Server.MapPath("~/AddonsByOsShop/UserExperience/AjaxMenuPro/MenuList.xml"));

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