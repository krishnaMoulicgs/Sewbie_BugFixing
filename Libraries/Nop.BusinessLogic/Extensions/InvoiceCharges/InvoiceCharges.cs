using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.BusinessLogic.ChargeManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement
{
    public class Invoice_Charges:BaseEntity
    {
        public int InvoiceChargeID { get; set; }
        public int InvoiceID { get; set; }
        public int ChargeID { get; set; }
        public decimal Amount { get; set; }
        

        #region Navigation Properties
        public virtual Charge Charge { get; set; }
        public virtual Invoice Invoice { get; set; }
        #endregion
    }
}
