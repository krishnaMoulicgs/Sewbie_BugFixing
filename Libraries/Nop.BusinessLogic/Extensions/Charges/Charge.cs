using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.ChargeManagement
{
    public class Charge: BaseEntity
    {
        /// <summary>
        /// Gets or sets the charge ID.
        /// </summary>
        /// <value>The charge ID.</value>
        public int ChargeID { get; set; }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        /// <value>The order ID.</value>
        public int OrderID { get; set; }

        /// <summary>
        /// Gets or sets the charge type ID.
        /// </summary>
        /// <value>The charge type ID.</value>
        public int ChargeTypeID { get; set; }

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        /// <value>The customer ID.</value>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is invoice charge.
        /// </summary>
        /// <value><c>true</c> if this instance is invoice charge; otherwise, <c>false</c>.</value>
        public bool IsInvoiceCharge { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public int Type { get; set; }
        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>The remarks.</value>
        public string Remarks { get; set; }
        /// <summary>
        /// Gets or sets the pay to.
        /// </summary>
        /// <value>The pay to.</value>
        public int PayTo { get; set; }
        /// <summary>
        /// Gets or sets the pay from.
        /// </summary>
        /// <value>The pay from.</value>
        public int PayFrom { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>The created on.</value>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>The updated on.</value>
        public DateTime UpdatedOn { get; set; }
        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>The effective date.</value>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>The updated by.</value>
        public int UpdatedBy { get; set; }

        #region Navigation Properties

        public virtual ChargeType ChargeType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Order Nop_Order { get; set; }
        public virtual Customer Customer1 { get; set; }
        public virtual Customer Customer2 { get; set; }
        public virtual Customer Customer3 { get; set; }
        public virtual Invoice_Charges Invoice_Charges { get; set; }
        
        #endregion
    }
}
