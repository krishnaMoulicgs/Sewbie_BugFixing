using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.Web.Services
{
    public static class Mapper
    {

        public static AddressDTO ConvertToDTO(Address input){
            AddressDTO result = new AddressDTO();

            result.addressId = input.AddressId.ToString();
            result.firstName = input.FirstName;
            result.lastName = input.LastName;
            result.address1 = input.Address1;
            result.address2 = input.Address2;
            result.city = input.City;
            result.stateId = input.StateProvinceId;
            result.stateText = input.StateProvinceId == 0 || input.StateProvince == null ? String.Empty : input.StateProvince.Abbreviation;
            result.zip = input.ZipPostalCode;
            result.email = input.Email;

            return result;
        }

        public static Address ConvertFromDTO(AddressDTO input){
            Address result = new Address();

            result.AddressId = Convert.ToInt32(input.addressId);
            result.FirstName = input.firstName;
            result.LastName = input.lastName;
            result.Address1 = input.address1;
            result.Address2 = input.address2;
            result.City = input.city;
            result.CountryId = 1;
            result.StateProvinceId = Convert.ToInt32(input.stateId);
            result.ZipPostalCode = input.zip;
            result.Email = input.email;

            return result;
        }
    }
}