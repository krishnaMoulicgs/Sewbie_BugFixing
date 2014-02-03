using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web
{
    public partial class VendorRegistration : BaseNopFrontendPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = GetLocaleResourceString("PageTitle.RegisterVendor");
            SEOHelper.RenderTitle(this, title, true);
        }

        public override PageSslProtectionEnum SslProtected
        {
            get
            {
                return PageSslProtectionEnum.Yes;
            }
        }

        public override bool AllowGuestNavigation
        {
            get
            {
                return true;
            }
        }
    }
}