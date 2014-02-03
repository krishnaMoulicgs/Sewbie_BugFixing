using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Utils;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class popupMaster : BaseNopVendorAdministrationMasterPage
    {
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