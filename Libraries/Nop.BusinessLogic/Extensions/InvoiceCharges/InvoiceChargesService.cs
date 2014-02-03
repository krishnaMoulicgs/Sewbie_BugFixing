using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.Common.Utils;
using System.Data;

namespace NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement
{
    public class InvoiceChargesService : IInvoiceChargesService
    {
         #region Fields

        /// <summary>
        /// Object context
        /// </summary>
        private readonly NopObjectContext _context;
        /// <summary>
        /// Cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public InvoiceChargesService(NopObjectContext context)
        {
            this._context = context;
            this._cacheManager = new NopRequestCache();
        }
        #endregion
        /// <summary>
        /// Gets all invoice.
        /// </summary>
        /// <returns></returns>
        public List<Invoice_Charges> GetInvoiceChargesByID(int invoiceID)
        {
            var query = from c in _context.InvoiceCharges
                        where c.InvoiceID == invoiceID
                        select c;
            var invoiceCharges = query.ToList();
            return invoiceCharges;
        }
    }
}
