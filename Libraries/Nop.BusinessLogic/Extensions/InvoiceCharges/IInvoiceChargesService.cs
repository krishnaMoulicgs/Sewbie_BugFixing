using System;
using System.Collections.Generic;
using System.Web.Security;
using NopSolutions.NopCommerce.Common;

namespace NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement
{
    public partial interface IInvoiceChargesService
    {
        /// <summary>
        /// Gets all invoice.
        /// </summary>
        /// <returns></returns>
        List<Invoice_Charges> GetInvoiceChargesByID(int invoiceID);


    }
}
