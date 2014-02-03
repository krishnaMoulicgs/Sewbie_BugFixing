using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web.Administration
{
    public partial class InvoiceDetails : BaseNopAdministrationPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        /// <summary>
        /// Gets the invoice ID.
        /// </summary>
        /// <value>
        /// The invoice ID.
        /// </value>
        public int InvoiceID
        {
            get
            {
                return CommonHelper.QueryStringInt("InvoiceID");
            }
        }


        /// <summary>
        /// Handles the PageIndexChanging event of the gvCharges control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoice.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            var invoiceCharges = this.InvoiceChargesService.GetInvoiceChargesByID(InvoiceID);
            gvInvoice.DataSource = invoiceCharges;
            gvInvoice.DataBind();
        }
    }
}