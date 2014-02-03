using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NopSolutions.NopCommerce.BusinessLogic.VendorManagement
{
    public class SellerCode : BaseEntity
    {
        #region Properties

        public int SellerCodeID { get; set; }
        public string Code { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        #endregion
    }
}
