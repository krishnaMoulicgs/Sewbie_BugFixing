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
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Security;
using NopSolutions.NopCommerce.BusinessLogic.Warehouses;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
 
namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class VEndorAdministration_WarehouseDetails : BaseNopVendorAdministrationPage
    {
        protected override bool ValidatePageSecurity()
        {
            return this.ACLService.IsActionAllowed("ManageWarehouses");
        }
    }
}
