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

namespace NopSolutions.NopCommerce.BusinessLogic.ChargeManagement
{
    public class ChargeService : IChargeService
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
        public ChargeService(NopObjectContext context)
        {
            this._context = context;
            this._cacheManager = new NopRequestCache();
        }
        #endregion

        /// <summary>
        /// Adds the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="createdOn">The created on.</param>
        /// <param name="updatedOn">The updated on.</param>
        /// <returns>Charge.</returns>
        public Charge AddCharge(int orderID, int chargeTypeID, int customerID, decimal amount, bool isInvoiceCharge, int type, string remarks, int payTo, int payFrom, DateTime createdOn, DateTime updatedOn, DateTime effectiveDate, int createdBy, int updatedBy)
        {
            createdOn = DateTime.UtcNow;
            updatedOn = DateTime.UtcNow;

            var charge = _context.Charges.CreateObject();
            charge.OrderID = orderID;
            charge.ChargeTypeID = chargeTypeID;
            charge.CustomerID = customerID;
            charge.Amount = amount;
            charge.IsInvoiceCharge = isInvoiceCharge;
            charge.Type = type;
            charge.Remarks = remarks;
            charge.PayTo = payTo;
            charge.PayFrom = payFrom;
            charge.CreatedOn = createdOn;
            charge.UpdatedOn = updatedOn;
            charge.EffectiveDate = effectiveDate;
            charge.CreatedBy = createdBy;
            charge.UpdatedBy = updatedBy;
            _context.Charges.AddObject(charge);
            _context.SaveChanges();

            return charge;
        }

        /// <summary>
        /// Updates the Charge
        /// </summary>
        /// <param name="charge">Charge</param>
        public void UpdateCharge(Charge charge)
        {
            if (charge == null)
                throw new ArgumentNullException("charge");
            
            charge.UpdatedOn = DateTime.UtcNow;
            if (!_context.IsAttached(charge))
                _context.Charges.Attach(charge);
            _context.SaveChanges();

        }

        /// <summary>
        /// Deletes the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        public void DeleteCharge(int chargeID)
        {
            var charge = GetCharge(chargeID);
            if (charge == null)
                return;


            if (!_context.IsAttached(charge))
                _context.Charges.Attach(charge);
            _context.DeleteObject(charge);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all Charges
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Charge collection</returns>
        public List<Charge> GetAllCharges()
        {
            var query = from c in _context.Charges
                        orderby c.EffectiveDate
                        select c;
            var charges = query.ToList();

            return charges;


        }

        /// <summary>
        /// Gets the type of the charges by vendor charge.
        /// </summary>
        /// <param name="vendorID">The vendor ID.</param>
        /// <param name="chargeTypeID">The charge type ID.</param>
        /// <returns></returns>
        public List<Charge> GetChargesByVendorChargeType(int vendorID, int chargeTypeID)
        {
            var query = from c in _context.Charges
                        where c.CustomerID == vendorID & c.ChargeTypeID == chargeTypeID
                        orderby c.EffectiveDate
                        select c;
            var charges = query.ToList();

            return charges;
        }
        /// <summary>
        /// Gets all charges by vendor.
        /// </summary>
        /// <param name="vendorID">The vendor ID.</param>
        /// <returns></returns>
        public List<Charge> GetAllChargesByVendor(int vendorID)
        {
            var query = from c in _context.Charges
                        where c.CustomerID == vendorID
                        orderby c.EffectiveDate
                        select c;
            var charges = query.ToList();

            return charges;
        }
        /// <summary>
        /// Gets the type of all charges by charge.
        /// </summary>
        /// <param name="chargeTypeID">The charge type ID.</param>
        /// <returns></returns>
        public List<Charge> GetAllChargesByChargeType(int chargeTypeID)
        {
            var query = from c in _context.Charges
                        where c.ChargeTypeID == chargeTypeID
                        orderby c.EffectiveDate
                        select c;
            var charges = query.ToList();

            return charges;
        }
        /// <summary>
        /// Gets the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        /// <returns>Charge.</returns>
        public Charge GetCharge(int chargeID)
        {
            if (chargeID == 0)
                return null;

            var query = from c in _context.Charges
                        where c.ChargeID == chargeID
                        select c;
            var charge = query.SingleOrDefault();
            return charge;
        }


    }
}
