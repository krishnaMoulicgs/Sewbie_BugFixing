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
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Measures;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement;
 
namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class Administration_Default : BaseNopVendorAdministrationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("Products.aspx");

            if (!Page.IsPostBack)
            {

                string TransactionID = Request.QueryString.Get("tx");
                string Status = Request.QueryString.Get("st");
                string Amount = Request.QueryString.Get("amt");
                string Currency = Request.QueryString.Get("cc");
                string item_number = Request.QueryString.Get("item_number");
                string NO = Request.QueryString.Get("cm");

                if (!string.IsNullOrEmpty(item_number))
                {
                    item_number=item_number.Replace(',', ' ').Trim();
                    int invoiceID = Convert.ToInt32(item_number);
                    Invoice invoice = InvoiceService.GetInvoice(invoiceID);
                    if (invoice != null)
                    {
                        invoice.InvoiceStatus = 2;
                        InvoiceService.UpdateInvoice(invoice);
                    }
                }

            }
        }
    }
}
