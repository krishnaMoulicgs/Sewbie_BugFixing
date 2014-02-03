using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Security;
using NopSolutions.NopCommerce.BusinessLogic.Templates;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.VendorAdministration.Modules;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class Administration_SellerDetails : BaseNopVendorAdministrationPage
    {
        protected override bool ValidatePageSecurity()
        {
            return this.ACLService.IsActionAllowed("ManageCatalog");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //check the current user
            if (NopCommerce.BusinessLogic.NopContext.Current.User != null)
            {
                // is this a vendor.  we assume they have a manufacturer record.
                if (!NopCommerce.BusinessLogic.NopContext.Current.User.IsVendor)
                {
                    HttpContext.Current.Response.Redirect(CommonHelper.GetStoreLocation(false));
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(CommonHelper.GetStoreLocation(false));
            }
        }
    }
}