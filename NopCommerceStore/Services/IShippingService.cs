using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;

namespace NopSolutions.NopCommerce.Web.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShippingService" in both code and config file together.
    [ServiceContract]
    public interface IShippingService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json)]
        ShippingOption GetShippingOptions(int vendorId);
    }
}
