using System;
using System.Collections.Generic;
using System.Web.Security;
using NopSolutions.NopCommerce.Common;

namespace NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement
{
    public partial interface IInvoiceService
    {
        /// <summary>
        /// Gets all invoice.
        /// </summary>
        /// <returns></returns>
        List<Invoice> GetAllInvoice();

        /// <summary>
        /// Gets the invoice by search.
        /// </summary>
        /// <param name="vendorID">The vendor ID.</param>
        /// <param name="invoiceStatus">The invoice status.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="invoiceDate">The invoice date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        List<Invoice> GetInvoiceBySearch(int vendorID, int invoiceStatus, string invoiceNumber, DateTime? startDate, DateTime? invoiceDate, DateTime? endDate);

        /// <summary>
        /// Gets all invoice.
        /// </summary>
        /// <returns></returns>
        void GenerateInvoice();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        void UpdateInvoice(Invoice invoice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <returns></returns>
        Invoice GetInvoice(int invoiceID);
    }
}
