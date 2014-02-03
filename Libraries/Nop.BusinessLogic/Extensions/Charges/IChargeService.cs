using System;
using System.Collections.Generic;
using System.Web.Security;
using NopSolutions.NopCommerce.Common;

namespace NopSolutions.NopCommerce.BusinessLogic.ChargeManagement
{
    public partial interface IChargeService
    {

        /// <summary>
        /// Adds the charge.
        /// </summary>
        /// <param name="orderID">The order ID.</param>
        /// <param name="chargeTypeID">The charge type ID.</param>
        /// <param name="CustomerID">The customer ID.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="isInvoiceCharge">if set to <c>true</c> [is invoice charge].</param>
        /// <param name="type">The type.</param>
        /// <param name="remarks">The remarks.</param>
        /// <param name="payTo">The pay to.</param>
        /// <param name="payFrom">The pay from.</param>
        /// <param name="createdOn">The created on.</param>
        /// <param name="updatedOn">The updated on.</param>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="createdBy">The created by.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns>Charge.</returns>
        Charge AddCharge(int orderID, int chargeTypeID, int CustomerID, decimal amount, bool isInvoiceCharge, int type, string remarks, int payTo, int payFrom, DateTime createdOn, DateTime updatedOn, DateTime effectiveDate, int createdBy, int updatedBy);

        /// <summary>
        /// Updates the charge.
        /// </summary>
        /// <param name="charge">The charge.</param>
        void UpdateCharge(Charge charge);

        /// <summary>
        /// Gets all charges.
        /// </summary>
        /// <returns>List{Charge}.</returns>
        List<Charge> GetAllCharges();

        /// <summary>
        /// Gets the type of the charges by vendor charge.
        /// </summary>
        /// <param name="vendorID">The vendor ID.</param>
        /// <param name="chargeTypeID">The charge type ID.</param>
        /// <returns></returns>
        List<Charge> GetChargesByVendorChargeType(int vendorID,int chargeTypeID);

        /// <summary>
        /// Gets all charges by vendor.
        /// </summary>
        /// <param name="vendorID">The vendor ID.</param>
        /// <returns></returns>
        List<Charge> GetAllChargesByVendor(int vendorID);

        /// <summary>
        /// Gets the type of all charges by charge.
        /// </summary>
        /// <param name="chargeTypeID">The charge type ID.</param>
        /// <returns></returns>
        List<Charge> GetAllChargesByChargeType(int chargeTypeID);

        /// <summary>
        /// Gets the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        /// <returns>Charge.</returns>
        Charge GetCharge(int chargeID);

        /// <summary>
        /// Deletes the charge.
        /// </summary>
        /// <param name="chargeID">The charge ID.</param>
        void DeleteCharge(int chargeID);
    }
}
