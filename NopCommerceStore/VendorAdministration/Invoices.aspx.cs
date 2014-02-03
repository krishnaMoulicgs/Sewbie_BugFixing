using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement;
using NopSolutions.NopCommerce.BusinessLogic;
using System.Configuration;
using System.Text;
namespace NopSolutions.NopCommerce.Web.VendorAdministration
{
    public partial class Invoices : BaseNopVendorAdministrationPage
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
        /// Gets the invoice.
        /// </summary>
        /// <returns></returns>
        protected List<Invoice> GetInvoice()
        {
            int vendorID = NopContext.Current.User.CustomerId;
            int invoiceStatus = 0;
            string invoiceNumber = null;
            DateTime? startDate = null;
            DateTime? invoiceDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
            {
                invoiceNumber = txtInvoiceNumber.Text.Trim();
            }
            if (ddlInvoiceStatus.SelectedIndex > 0 && ddlInvoiceStatus.SelectedValue != null)
            {
                invoiceStatus = Convert.ToInt16(ddlInvoiceStatus.SelectedIndex);
            }
            if (ctrlStartDatePicker.SelectedDate != null)
                startDate = ctrlStartDatePicker.SelectedDate;
            if (ctrlInvoiceDatePicker.SelectedDate != null)
                invoiceDate = ctrlInvoiceDatePicker.SelectedDate;
            if (ctrlEndDatePicker.SelectedDate != null)
                endDate = ctrlEndDatePicker.SelectedDate;
            var invoice = this.InvoiceService.GetInvoiceBySearch(vendorID, invoiceStatus, invoiceNumber, startDate, invoiceDate, endDate);
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
        protected void btnPay_Click(object sender, EventArgs e)
        {

        }

        protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "paynow")
            {
                int vendorID = NopContext.Current.User.CustomerId;
                //Vendor vendor =this.VendorService.GetVendor(vendorID);
                Vendor vendor = this.VendorService.GetSiteAdminVendor();
                if (vendor != null)
                {
                    string invoiceID = "0";   
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    var hfInvoiceID = gvr.FindControl("hfInvoiceID") as HiddenField;
                   
                    if (hfInvoiceID != null && hfInvoiceID.Value != null)
                    {
                        invoiceID = hfInvoiceID.Value.ToString();
                    }

                    string vendorPayPalEmail = vendor.PaypalEmailAddress;
                    string redirecturl = string.Empty;
                    //string redirecturl = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + "ilsmut_1357886581_biz@gmail.com";
                  //  redirecturl = ConfigurationManager.AppSettings["PapalURL"].ToString() + "&business=" + "ilsmut_1357886581_biz@gmail.com";
                    redirecturl = ConfigurationManager.AppSettings["PaypalURL"].ToString() + "&business=" +vendorPayPalEmail;
                    redirecturl += "&item_name=" + "Invoice";
                    redirecturl += "&amount=" + e.CommandArgument;
                    redirecturl += "&currency=" + "USD";
                    //redirecturl += "&itemName=" + invoiceID;
                    //Success return page url
                    redirecturl += "&return=" + ConfigurationManager.AppSettings["SuccessURL"].ToString() + "?item_number=" + invoiceID;
                    //Failed return page url
                    redirecturl += "&cancel_return=" + ConfigurationManager.AppSettings["FailedURL"].ToString();
                    //double cost = Convert.ToDouble(e.CommandArgument);
                    //string url = GetPayPalPaymentUrl(invoiceID, "Invoice", cost, 0, "USD");
                    Response.Redirect(redirecturl);
                }
            }
        }

        public  string GetPayPalPaymentUrl(string OrderId,string itemNames, double ItemCost, double shippingcost, string currency)
        {

            string serverUrl = ("https://www.sandbox.paypal.com/us/cgi-bin/webscr");

            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(
               HttpContext.Current.Request.Url.PathAndQuery, "") + HttpContext.Current.Request.ApplicationPath;
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";

            string notifyUrl = HttpUtility.UrlEncode(baseUrl + "Notify.aspx");
            //string returnUrl = HttpUtility.UrlEncode(baseUrl + "OrderCompleted.aspx?ID=" + OrderId);
            string returnUrl = ConfigurationManager.AppSettings["SuccessURL"].ToString();
           // string cancelUrl = HttpUtility.UrlEncode(baseUrl + "OrderCancelled.aspx");
            string cancelUrl = ConfigurationManager.AppSettings["FailedURL"].ToString();
            string business = HttpUtility.UrlEncode("ilsmut_1357886581_biz@gmail.com");
            string businessApi = HttpUtility.UrlEncode("ilsmut_1357886581_biz_api1.gmail.com");
            string businessApipwd = HttpUtility.UrlEncode("1357886601");
            string businessApisgn = HttpUtility.UrlEncode("AOIwOKO7.AyfJVZAidZVKyKI-oS9A2Ik0WoxUysmRO4idMrSsVDaf0Hz");
            string itemName = HttpUtility.UrlEncode( OrderId);

            StringBuilder url = new StringBuilder();
            url.AppendFormat(
               "{0}?cmd=_xclick&upload=1&rm=2&no_shipping=1&no_note=1&currency_code={1}&business={2}&item_number={3}&custom={3}&item_name={4}&amount={5}&shipping={6}&notify_url={7}&return={8}&cancel_return={9}&USER={10}&PWD={11}&SIGNATURE{12}",
               serverUrl, currency, business, OrderId, itemName,
               ItemCost, shippingcost, notifyUrl, returnUrl, cancelUrl, businessApi, businessApipwd, businessApisgn);

            return url.ToString();
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
            if (NopContext.Current != null && NopContext.Current.User != null)
            {
                var invoices = GetInvoice();
                gvInvoice.DataSource = invoices;
                gvInvoice.DataBind();
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the gvInvoice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnDisplay = (Button)e.Row.FindControl("btnPayNow");
                string invoiceStatus = gvInvoice.DataKeys[e.Row.RowIndex].Value.ToString();
                if (invoiceStatus == "2")
                    btnDisplay.Enabled = false;
            }
        }

    }
}