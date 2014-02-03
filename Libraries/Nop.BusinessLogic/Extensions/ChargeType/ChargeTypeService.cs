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
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Profile;

namespace NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails
{
    public class ChargeTypeService : IChargeTypeService
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
        public ChargeTypeService(NopObjectContext context)
        {
            this._context = context;
            this._cacheManager = new NopRequestCache();
        }
        #endregion

        #region Methods

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
        public ChargeType AddChargeType(string name, string description, bool isActive, DateTime createdOn, DateTime updatedOn)
        {
            name = CommonHelper.EnsureNotNull(name);
            createdOn = DateTime.UtcNow;
            updatedOn = DateTime.UtcNow;

            var charge = _context.ChargeTypes.CreateObject();
            charge.Name = name;
            charge.Description = description;
            charge.CreatedOn = createdOn;
            charge.UpdatedOn = updatedOn;
            charge.IsActive = isActive;
            _context.ChargeTypes.AddObject(charge);
            _context.SaveChanges();

            return charge;
        }

        /// <summary>
        /// Updates the Charge
        /// </summary>
        /// <param name="charge">Charge</param>
        public void UpdateChargeType(ChargeType charge)
        {
            if (charge == null)
                throw new ArgumentNullException("charge");

            charge.Name =  CommonHelper.EnsureNotNull(charge.Name);
            charge.Name = CommonHelper.EnsureMaximumLength(charge.Name, 50);
            charge.Description = charge.Description;
            charge.CreatedOn = charge.CreatedOn;
            charge.UpdatedOn = DateTime.UtcNow;
            charge.IsActive = charge.IsActive;
            if (!_context.IsAttached(charge))
                _context.ChargeTypes.Attach(charge);
            _context.SaveChanges();

        }

        public void DeleteChargeType(int chargeTypeID)
        {
            var charge = GetChargeType(chargeTypeID);
            if (charge == null)
                return;


            if (!_context.IsAttached(charge))
                _context.ChargeTypes.Attach(charge);
            _context.DeleteObject(charge);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all Charges
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Charge collection</returns>
        public List<ChargeType> GetAllChargeTypes()
        {
            var query = from c in _context.ChargeTypes
                        orderby c.Name
                        select c;
            var chargeTypes = query.ToList();

            return chargeTypes;

           
        }

        /// <summary>
        /// Gets the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        /// <returns>Charge.</returns>
        public ChargeType GetChargeType(int chargeTypeID)
        {
            if (chargeTypeID == 0)
                return null;

            var query = from c in _context.ChargeTypes
                        where c.ChargeTypeID == chargeTypeID
                        select c;
            var chargeType = query.SingleOrDefault();
            return chargeType;
        }


        #endregion
    }

}

