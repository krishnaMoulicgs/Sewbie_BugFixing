using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Orders;

namespace NopSolutions.NopCommerce.BusinessLogic.VendorManagement
{
    public class Vendor : BaseEntity
    {

        #region Properties
        
        public string PaypalEmailAddress { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string PaypalFirstName { get; set; }
        public string PaypalLastName { get; set; }
        public bool PaypalVerified { get; set; }
        public bool Approved { get; set; }
        public bool Denied { get; set; }
            
        #endregion


        #region navigation properties

        public virtual Customer Nop_Customer { get; set; }

        public virtual ICollection<Products.ProductVariant> Nop_ProductVariant { get; set; }

        public virtual ICollection<ShoppingCartItem> Nop_ShoppingCartItem { get; set; }

        #endregion
    }
}
