using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
namespace NopSolutions.NopCommerce.Web
{
    public partial class PaypalAPRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PayRedirect"] != null)
            {
                hidRedirectURL.Value = Session["PayRedirect"].ToString();
                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.CommonError, "Redirect to: " + hidRedirectURL.Value, "Logging");              
            }
            else
            {
                hidRedirectURL.Value = CommonHelper.GetStoreLocation(false);
                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.CommonError, "Redirect Faiulure to: " + hidRedirectURL.Value, "Logging");                
            }
        }
    }
}