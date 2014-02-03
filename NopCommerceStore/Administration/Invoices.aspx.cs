using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement;

namespace NopSolutions.NopCommerce.Web.Administration
{
    public partial class Invoices : BaseNopAdministrationPage
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
                FillDropDowns();
                BindGrid();
            }
        }

        /// <summary>
        /// Fills the drop downs.
        /// </summary>
        protected void FillDropDowns()
        {
            this.ddlVendors.Items.Clear();
            ListItem itemEmptyVendor = new ListItem(GetLocaleResourceString("Admin.Common.Unknown"), "0");
            this.ddlVendors.Items.Add(itemEmptyVendor);
            var vendors = VendorService.GetAllVendors();
            foreach (Vendor vendor in vendors)
            {
                ListItem itemVendor = new ListItem(vendor.CompanyName, vendor.CustomerId.ToString());
                this.ddlVendors.Items.Add(itemVendor);
            }

        }

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <returns></returns>
        protected List<Invoice> GetInvoice()
        {
            var invoice = this.InvoiceService.GetAllInvoice();
            return invoice;
        }
        /// <summary>
        /// Handles the Click event of the btnExportXLS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnExportXLS_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string fileName = string.Format("invoice_{0}_{1}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
                    string filePath = string.Format("{0}files\\ExportImport\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);
                    var invoices = GetInvoice();

                    this.ExportManager.ExportInvoiceToXls(filePath, invoices);
                    CommonHelper.WriteResponseXls(filePath, fileName);
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    BindGrid();
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
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
            int vendorID=0;
            int invoiceStatus=0;
            string invoiceNumber=null;
            DateTime? startDate=null;
            DateTime? invoiceDate=null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
            {
                invoiceNumber = txtInvoiceNumber.Text.Trim();
            }
            if (ddlVendors.SelectedIndex > 0 && ddlVendors.SelectedValue != null)
            {
                vendorID = Convert.ToInt16(ddlVendors.SelectedValue);
            }
            if (ddlInvoiceStatus.SelectedIndex > 0 && ddlInvoiceStatus.SelectedValue != null)
            {
                invoiceStatus= Convert.ToInt16(ddlInvoiceStatus.SelectedIndex);
            }
            if (ctrlStartDatePicker.SelectedDate != null)
                startDate = ctrlStartDatePicker.SelectedDate;
            if (ctrlInvoiceDatePicker.SelectedDate != null)
                invoiceDate= ctrlInvoiceDatePicker.SelectedDate;
            if (ctrlEndDatePicker.SelectedDate != null)
                endDate = ctrlEndDatePicker.SelectedDate;
            var invoices = this.InvoiceService.GetInvoiceBySearch(vendorID, invoiceStatus, invoiceNumber, startDate, invoiceDate, endDate);
            gvInvoice.DataSource = invoices;
            gvInvoice.DataBind();
        }
    }
}