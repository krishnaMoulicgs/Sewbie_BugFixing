using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Products;

namespace NopSolutions.NopCommerce.BusinessLogic.Orders
{
    /// <summary>
    /// Represents a best sellers report line for vendors
    /// </summary>
    public partial class VendorBestSellersReportLine : BaseEntity
    {
        #region Fields
        private ProductVariant _pv;
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the product variant identifier
        /// </summary>
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the total count
        /// </summary>
        public int SalesTotalCount { get; set; }

        /// <summary>
        /// Gets or sets the total amount
        /// </summary>
        public decimal SalesTotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the vendor id
        /// </summary>
        public int VendorID { get; set; }

        #endregion

        #region Custom Properties
        /// <summary>
        /// Gets a product variant
        /// </summary>
        public ProductVariant ProductVariant
        {
            get
            {
                if (_pv == null)
                    _pv = IoC.Resolve<IProductService>().GetProductVariantById(this.ProductVariantId);
                return _pv;
            }
        }
        #endregion
    }

}
