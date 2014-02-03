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
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
 
namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class PaymentMethodsControl : BaseNopAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BtnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                ctrlPaymentMethodsFilterControl.SaveInfo();

                Response.Redirect("PaymentMethods.aspx");
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        void BindGrid()
        {
            var paymentMethods = this.PaymentService.GetAllPaymentMethods();
            gvPaymentMethods.DataSource = paymentMethods;
            gvPaymentMethods.DataBind();
        }
    }
}