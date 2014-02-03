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

namespace NopSolutions.NopCommerce.BusinessLogic.VendorManagement
{
    public class VendorService : IVendorService
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
        public VendorService(NopObjectContext context)
        {
            this._context = context;
            this._cacheManager = new NopRequestCache();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a vendor
        /// </summary>
        /// <param name="vendorid">The vendor id</param>
        /// <param name="paypalemail">the email address that is registered with paypal.</param>
        /// <returns>A vendor</returns>
        public Vendor AddVendor(int vendorID, string paypalemail, string companyName, string paypalFirstName, string paypalLastName, bool paypalVerified)
        {
            paypalemail = CommonHelper.EnsureNotNull(paypalemail);
            paypalemail = paypalemail.Trim();
            paypalemail = CommonHelper.EnsureMaximumLength(paypalemail, 255);

            companyName = CommonHelper.EnsureNotNull(companyName);
            companyName = CommonHelper.EnsureMaximumLength(companyName, 400);

            var vendor = _context.Vendors.CreateObject();
            vendor.CustomerId = NopContext.Current.User.CustomerId;
            vendor.PaypalEmailAddress = paypalemail;
            vendor.CompanyName = companyName;
            vendor.PaypalFirstName = paypalFirstName;
            vendor.PaypalLastName = paypalLastName;
            vendor.PaypalVerified = paypalVerified;

            var customer = _context.Customers.CreateObject();
            customer = IoC.Resolve<ICustomerService>().GetCustomerById(NopContext.Current.User.CustomerId);

            customer.IsVendor = true;

            customer.Vendor = vendor;
            
            //automatically add manufacturer to the manufacturer table.
            //and by that the manufacturer list.

            //TODO: Verify it doesn't exist first.
            //IoC.Resolve<IManufacturerService>().GetManufacturerByName(companyName);

            var manufacturer = _context.Manufacturers.CreateObject();

            


            manufacturer.Name = companyName;
            manufacturer.Description = "";
            manufacturer.MetaKeywords = "";
            manufacturer.MetaDescription = "";
            manufacturer.MetaTitle = "";
            manufacturer.SEName = "";
            manufacturer.PriceRanges = "";
            manufacturer.TemplateId = 1;
            manufacturer.PageSize = 12;
            manufacturer.Published = true;
            manufacturer.CreatedOn = DateTime.Now;
            manufacturer.UpdatedOn = DateTime.Now;

            if (!_context.IsAttached(customer))
                _context.Customers.AddObject(customer);

            if (!_context.IsAttached(manufacturer))
                _context.Manufacturers.AddObject(manufacturer);

            _context.SaveChanges();

            return vendor;
        }

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="customer">Vendor</param>
        public void UpdateVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            vendor.CompanyName = CommonHelper.EnsureNotNull(vendor.CompanyName);
            vendor.CompanyName = vendor.CompanyName.Trim();
            vendor.CompanyName = CommonHelper.EnsureMaximumLength(vendor.CompanyName, 255);


            vendor.PaypalEmailAddress = CommonHelper.EnsureNotNull(vendor.PaypalEmailAddress);
            vendor.PaypalEmailAddress = vendor.PaypalEmailAddress.Trim();
            vendor.PaypalEmailAddress = CommonHelper.EnsureMaximumLength(vendor.PaypalEmailAddress, 255);

            vendor.PaypalFirstName = CommonHelper.EnsureNotNull(vendor.PaypalFirstName);
            vendor.PaypalFirstName = vendor.PaypalFirstName.Trim();
            vendor.PaypalFirstName = CommonHelper.EnsureMaximumLength(vendor.PaypalFirstName, 255);

            vendor.PaypalLastName = CommonHelper.EnsureNotNull(vendor.PaypalLastName);
            vendor.PaypalLastName = vendor.PaypalLastName.Trim();
            vendor.PaypalLastName = CommonHelper.EnsureMaximumLength(vendor.PaypalLastName, 255);

            vendor.PaypalVerified = vendor.PaypalVerified;

            if (!_context.IsAttached(vendor))
                _context.Vendors.Attach(vendor);
            _context.SaveChanges();

            //raise event             
            //EventContext.Current.OnCustomerUpdated(null,
            //    new CustomerEventArgs() { Customer = customer });
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Vendor collection</returns>
        public PagedList<Vendor> GetAllVendors(int pageSize, int pageIndex)
        {
            return GetAllVendors(null, null, pageSize, pageIndex);
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Customer collection</returns>
        public PagedList<Vendor> GetAllVendors(string companyName,
            string paypalEmailAddress, int pageSize, int pageIndex)
        {
            if (companyName == null)
                companyName = string.Empty;

            if (paypalEmailAddress == null)
                paypalEmailAddress = string.Empty;

            var query = from v in _context.Vendors
                        from c in _context.Customers
                        where
                        (v.CustomerId == c.CustomerId) && (!c.Deleted) &&
                        (String.IsNullOrEmpty(companyName) || v.CompanyName.Contains(companyName)) &&
                        (String.IsNullOrEmpty(paypalEmailAddress) || v.PaypalEmailAddress.Contains(paypalEmailAddress))
                        orderby v.CompanyName
                        select v;
            var vendors = new PagedList<Vendor>(query, pageIndex, pageSize);

            return vendors;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public Vendor GetVendor(int vendorID)
        {
            var query = from v in _context.Vendors
                        from c in _context.Customers
                        where
                        (v.CustomerId == vendorID) && (!c.Deleted)
                        orderby v.CompanyName
                        select v;
           
            return query.First();
        }
        
        /// <summary>
        /// Gets all Vendors
        /// </summary>
        /// <returns>Vendor collection</returns>
        public List<Vendor> GetAllVendors()
        {
            var query = from c in _context.Vendors
                        orderby c.CompanyName
                        select c;
            var vendors = query.ToList();

            return vendors;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vendor GetSiteAdminVendor()
        {
            var query = from v in _context.Vendors
                        from c in _context.Customers
                        where (!c.Deleted) && (c.IsAdmin) && (v.CustomerId == c.CustomerId)
                        select v;

            return query.First();
        }
    }

    #endregion
}
