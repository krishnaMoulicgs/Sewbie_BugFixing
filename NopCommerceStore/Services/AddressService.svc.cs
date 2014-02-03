using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Newtonsoft.Json;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Manufacturers;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;

namespace NopSolutions.NopCommerce.Web.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AddressService : IAddressService
    {
        public AddressResponse GetAddresses(AddressRequest request)
        {   
            AddressResponse response = new AddressResponse();

            int Billing = request.Billing.ToLower() == "billing" ? 1 : 0 ;

            List<Address> addresses = IoC.Resolve<CustomerService>().GetAddressesByCustomerId(request.CustomerId, Convert.ToBoolean(Billing));
            List<AddressDTO> results = new List<AddressDTO>();

            foreach (Address addy in addresses)
            {
                results.Add(Mapper.ConvertToDTO(addy));
            }
                
            response.addresses = results;
            response.success = 1;
            response.billing = Billing;

            return response;
        }

        public AddressResponse SaveAddress(AddressRequest request)
        {
            AddressResponse response = new AddressResponse();
            CustomerService cs = IoC.Resolve<NopSolutions.NopCommerce.BusinessLogic.CustomerManagement.CustomerService>();
            Address addy = Mapper.ConvertFromDTO(request.address);
            response.billing = request.Billing.ToLower() == "billing" ? 1 : 0;

            if (request == null){
                // no request object is probably an error.
                return response;
            }
            if (request.address == null){
                // no address object is probably an error.
                return response;
            }

            Customer cust = cs.GetCustomerById(request.CustomerId);

            //if address already has an id, be sure to update instead of insert.
            if (request.address.addressId == string.Empty || request.address.addressId == "0" || request.address.addressId == null)
            {
                //No ID found.  It must be an insert.

                //assign values for insert.
                
                addy.CustomerId = request.CustomerId;
                addy.CreatedOn = System.DateTime.Now;
                addy.UpdatedOn = System.DateTime.Now;
                addy.Email = cust.Email;
                
                addy.IsBillingAddress = request.Billing.ToLower() == "billing" ? true : false;

                //perform insert.
                cs.InsertAddress(addy);
            }
            else
            {
                //addressid was found. So we need to retreive it in order to get it.
                addy = cs.GetAddressById(Convert.ToInt32(request.address.addressId));
                if (addy.Email == "anonymous@anonymous.com")
                {
                    addy.Email = cust.Email;
                    cs.UpdateAddress(addy);
                }
            }

            //then update the user.
            if (cust != null)
            {
                if (request.Billing.ToLower() == "billing")
                {
                    //billing
                    if (request.UseShippingAsBilling == "1")
                    {
                        //yes, then save the addy as a new address.

                        Address ad = new Address();
                        ad.FirstName = addy.FirstName;
                        ad.LastName = addy.LastName;
                        
                        ad.Address1 = addy.Address1;
                        ad.Address2 = addy.Address2;
                        ad.City = addy.City;
                        ad.StateProvinceId = addy.StateProvinceId;
                        ad.ZipPostalCode = addy.ZipPostalCode;
                        ad.CountryId = addy.CountryId;
                        ad.Email = addy.Email;

                        ad.CustomerId = request.CustomerId;
                        ad.CreatedOn = System.DateTime.Now;
                        ad.UpdatedOn = System.DateTime.Now;
                        ad.IsBillingAddress = true;

                        cs.InsertAddress(ad);


                        cust.BillingAddressId = ad.AddressId;
                        // do this because down below we'll add it to the 
                        // return collection for update on the client.
                        addy = ad;
                    }
                    else
                    {
                        //no do not do shipping as billing.
                        //so do regular billing address update.
                        addy.IsBillingAddress = true;
                        cust.BillingAddressId = addy.AddressId;
                    }
                    
                }
                else
                {
                    //shipping
                    cust.ShippingAddressId = addy.AddressId;
                }
                
                cs.UpdateCustomer(cust);
                response.success = 1;
                if (response.addresses == null)
                {
                    response.addresses = new List<AddressDTO>();
                }
                response.addresses.Add(Mapper.ConvertToDTO(addy));
            }
            else
            {
                //customer's address was not updated for the user.
                response.success = 0;
            }
            return response;
        }
    }
}
