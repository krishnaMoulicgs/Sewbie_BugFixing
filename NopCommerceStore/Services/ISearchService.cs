using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using System.Text;

namespace NopSolutions.NopCommerce.Web.Services
{
    [ServiceContract]
    public interface ISearchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, 
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json)]
        SearchDTO SearchByCategory(string category, string page);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json)]
        SearchDTO SearchByCategoryPaging(string category, string page, string pagesize);
    }


    [DataContract]
    public class SearchDTO
    {
        [DataMember]
        public List<SearchProducts> products = new List<SearchProducts>();

        [DataMember]
        public int maxRecords;

        [DataMember]
        public int requestNumber;

        [DataMember]
        public int currentPage;
    }

    [DataContract]
    public class SearchProducts
    {
        [DataMember]
        public int productid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string shortName { get; set; }
        [DataMember]
        public decimal price { get; set; }
        [DataMember]
        public string strPrice { get; set; }
        [DataMember]
        public string thumbURL { get; set; }
        [DataMember]
        public string productURL { get; set; }
        [DataMember]
        public string sellerURL { get; set; }
        [DataMember]
        public string sellerName {get;set;}

        public SearchProducts()
        {
            productid = 0;
            name = "";
            price = 0;
            thumbURL = "";
            productURL = "";

        }
    }
}
