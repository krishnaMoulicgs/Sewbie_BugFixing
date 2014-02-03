using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using System.Xml;

namespace NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AdminManager
{
    public partial class functionMenu : BaseNopAdministrationPage
    {
        protected override void OnInit(EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                int CheckedValue = 0;
                rblMenuVariant.Checked = false;
                rblMenuVariant.Checked = false;


                //Read the menu options in XML
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
                xmlDoc.Save(Server.MapPath("~/Administration/OsShopAdmin/MenuChoice.xml"));


                if (CheckedValue == 1)
                {
                    rblMenuVariant.Checked = true;
                }
                if (CheckedValue == 2)
                {
                    rblMenuOne.Checked = true;
                }

            }

            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (rblMenuVariant.Checked)
            {
                string a = rblMenuVariant.Value;
            }
            if (rblMenuOne.Checked)
            {
                string a = rblMenuOne.Value;
            }
        }

        protected void CreateMenu_Click(object sender, EventArgs e)
        {
            ctrlProMenu.createNewMenu();
        }

        protected void ChangeMenu_Click(object sender, EventArgs e)
        {
            int CheckedValue = 0;
            if (rblMenuVariant.Checked == true)
            {
                CheckedValue = 1;
            }
            if (rblMenuOne.Checked == true)
            {
                CheckedValue = 2;
            }

            //Read the menu options in XML
            XmlDocument xmlDoc;
            xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/Administration/OsShopAdmin/MenuChoice.xml"));

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("MenuRoot").ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                string menuName = node.Name;
                if (menuName == "MenuChoice")
                {
                    node.InnerText = CheckedValue.ToString();//Is modified
                }
            }
            xmlDoc.Save(Server.MapPath("~/Administration/OsShopAdmin/MenuChoice.xml"));

        }

        protected string GetLocaleResourceString(string ResourceName)
        {
            Language language = NopContext.Current.WorkingLanguage;
            return LocalizationManager.GetLocaleResourceString(ResourceName, language.LanguageId);
        }

    }
}