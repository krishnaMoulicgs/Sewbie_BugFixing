using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Utils;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class mainMaster : BaseNopVendorAdministrationMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            //string headerText = string.Format("nopCommerce {0}", this.SettingManager.CurrentVersion);
            //lblHeader.Text = headerText;
        }

        //protected void lbClearCache_Click(object sender, EventArgs e)
        //{
        //    new NopStaticCache().Clear();
        //}

        public override void ShowMessage(string message)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = "messageBox messageBoxSuccess";
            lMessage.Text = message;
        }

        public override void ShowError(string message, string completeMessage)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = "messageBox messageBoxError";
            lMessage.Text = message;
            lMessageComplete.Text = completeMessage;
        }
    }
}