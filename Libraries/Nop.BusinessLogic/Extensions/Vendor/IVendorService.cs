using System;
using System.Collections.Generic;
using System.Web.Security;
using NopSolutions.NopCommerce.Common;

namespace NopSolutions.NopCommerce.BusinessLogic.VendorManagement
{
    public partial interface IVendorService
    {
        Vendor AddVendor(int vendorID, string paypalemail, string companyName, string paypalFirstName, string paypalLastName, bool paypalVerified);

        void UpdateVendor(Vendor vendor);

        PagedList<Vendor> GetAllVendors(int pageSize, int pageIndex);

        PagedList<Vendor> GetAllVendors(string companyName, string paypalEmailAddress, int pageSize, int pageIndex);

        Vendor GetVendor(int vendorID);

        List<Vendor> GetAllVendors();

        Vendor GetSiteAdminVendor();
    }
}
