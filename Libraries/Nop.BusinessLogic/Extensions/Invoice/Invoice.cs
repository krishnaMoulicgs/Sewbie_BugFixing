using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement
{
    public class Invoice:BaseEntity
    {
        public int InvoiceID { get; set; }
        public int VendorID { get; set; }
        public DateTime InvoiceFromDate { get; set; }
        public DateTime InvoiceToDate { get; set; }
        public decimal Amount { get; set; }
        public int InvoiceStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        #region Navigation Properties
        public virtual Vendor Vendor { get; set; }
        //public virtual InvoiceCharges Invoice_Charges { get; set; }
        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the invoice charges.
        /// </summary>
        /// <value>
        /// The invoice charges.
        /// </value>
        public List<Invoice_Charges> InvoiceCharges
        {
            get
            {
                return IoC.Resolve<IInvoiceChargesService>().GetInvoiceChargesByID(this.InvoiceID);
            }
        }
        #endregion
    }
}
