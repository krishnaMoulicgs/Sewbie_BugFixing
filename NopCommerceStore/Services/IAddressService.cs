using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace NopSolutions.NopCommerce.Web.Services
{
    [ServiceContract]
    public interface IAddressService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json)]
        AddressResponse GetAddresses(AddressRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json)]
        AddressResponse SaveAddress(AddressRequest request);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    RequestFormat = WebMessageFormat.Json)]
        //StateProvinceResponse GetStates(StateProvinceRequest request);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    RequestFormat = WebMessageFormat.Json)]
        //CountryResponse GetCountries(CountryRequest request);
    }

    [DataContract]
    public class StateProvinceRequest
    {
        
    }

    [DataContract]
    public class StateProvinceResponse
    {
        List<StateProvinceDTO> states;
    }
    
    [DataContract]
    public class StateProvinceDTO
    {
        [DataMember]
        public int stateProvinceID;
        [DataMember]
        public string stateProvinceName;
    }

    [DataContract]
    public class CountryRequest
    {

    }

    [DataContract]
    public class CountryResponse
    {

    }

    [DataContract]
    public class CountryDTO
    {
        [DataMember]
        public int countryID;
        public int countryName;
    }

    [DataContract]
    public class AddressRequest
    {
        [DataMember]
        public int CustomerId;

        [DataMember]
        public AddressDTO address;

        [DataMember]
        public string Billing;

        [DataMember]
        public string UseShippingAsBilling;
    }

    [DataContract]
    public class AddressResponse
    {
        [DataMember]
        public int success;

        [DataMember]
        public int customerId;

        [DataMember]
        public List<AddressDTO> addresses;

        [DataMember]
        public int billing;
    }

    [DataContract]
    public class AddressDTO
    {
        [DataMember]
        public string addressId;

        [DataMember]
        public string firstName;

        [DataMember]
        public string lastName;

        [DataMember]
        public string address1;

        [DataMember]
        public string address2;

        [DataMember]
        public string city;

        [DataMember]
        public string stateText;

        [DataMember]
        public int stateId;

        [DataMember]
        public string zip;

        [DataMember]
        public string country;

        [DataMember]
        public string email;
    }
}
