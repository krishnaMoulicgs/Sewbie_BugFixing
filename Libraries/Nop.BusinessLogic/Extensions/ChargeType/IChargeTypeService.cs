using System;
using System.Collections.Generic;
using System.Web.Security;
using NopSolutions.NopCommerce.Common;

namespace NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails
{
    public partial interface IChargeTypeService
    {
        /// <summary>
        /// Adds the charge.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="createdOn">The created on.</param>
        /// <param name="updatedOn">The updated on.</param>
        /// <returns>Charge.</returns>
        ChargeType AddChargeType(string name, string description, bool isActive, DateTime createdOn, DateTime updatedOn);

        /// <summary>
        /// Updates the charge.
        /// </summary>
        /// <param name="charge">The charge.</param>
        void UpdateChargeType(ChargeType charge);

        /// <summary>
        /// Gets all charges.
        /// </summary>
        /// <returns>List{Charge}.</returns>
        List<ChargeType> GetAllChargeTypes();

        /// <summary>
        /// Gets the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        /// <returns>Charge.</returns>
        ChargeType GetChargeType(int chargeID);

        /// <summary>
        /// Deletes the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        void DeleteChargeType(int chargeID);
    }
}
