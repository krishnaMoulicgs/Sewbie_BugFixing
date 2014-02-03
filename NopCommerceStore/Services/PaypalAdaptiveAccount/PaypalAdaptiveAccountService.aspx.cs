using System;
using System.Web.Services;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.Payment.Methods.PayPal;
using NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalPlatformNVPSDK;

namespace NopSolutions.NopCommerce.Web.Services.PaypalAdaptiveAccount
{
    public partial class PaypalAdaptiveAccountService1 : System.Web.UI.Page
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [WebMethod]
        public string VerifyPaypalEmail(string email, string firstName, string lastName)
        {
            string returnResponse = "false";

            try
            {
                ISettingManager settingManager = IoC.Resolve<ISettingManager>();
                bool useSandbox = settingManager.GetSettingValueBoolean("PaymentMethod.PaypalAdaptive.UseSandbox");
                string sandboxEndpoint = settingManager.GetSettingValue("Paypal.AdaptiveAccounts.Sandbox.Endpoint");
                string productionEndpoint = settingManager.GetSettingValue("Paypal.AdaptiveAccounts.Production.Endpoint");
                string aa_endpoint = useSandbox ? sandboxEndpoint : productionEndpoint;
                string endpoint = aa_endpoint + "GetVerifiedStatus";
                System.Collections.Hashtable headers = new System.Collections.Hashtable();

                headers = settingManager.GetSettingValueBoolean("PaymentMethod.PaypalAdaptive.UseSandbox") ? Constants_Common.sandboxHeaders() : Constants_Common.productionHeaders();

                NVPHelper NVPRequest = new NVPHelper();
                //requestEnvelope.errorLanguage is common for all the request
                NVPRequest[NVPConstant.requestEnvelopeerrorLanguage] = "en_US";
                NVPRequest[NVPConstant.GetVerifiedStatus.emailAddress] = email;
                NVPRequest[NVPConstant.GetVerifiedStatus.matchCriteria] = "NAME";

                NVPRequest[NVPConstant.GetVerifiedStatus.firstName] = firstName;
                NVPRequest[NVPConstant.GetVerifiedStatus.lastName] = lastName;

                string strrequestforNvp = NVPRequest.Encode();

                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, "pre-call", "Adaptive Accounts");
                //calling Call method where actuall API call is made, NVP string, header value adne end point are passed as the input.
                CallerServices_NVP CallerServices = new CallerServices_NVP();
                string stresponsenvp = CallerServices.Call(strrequestforNvp, headers, endpoint);
                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, stresponsenvp, "Adaptive Accounts");

                //Response is send to Decoder method where it is decoded to readable hash table
                NVPHelper decoder = new NVPHelper();
                decoder.Decode(stresponsenvp);

                if (decoder["responseEnvelope.ack"] != "Failure")
                {
                    //account exists, may or may not be verified.
                    //add code later to addres this issue.
                    //if (decoder["accountStatus"] == "VERIFIED") { }
                    //if (decoder["accountStatus"] == "UNVERIFIED") { }
                    IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, stresponsenvp, "Adaptive Accounts");
                    returnResponse = "true";
                }
                else
                {
                    IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, stresponsenvp, "Adaptive Accounts");
                    returnResponse = "false";
                }

            }
            catch (FATALException fx)
            {
                //NVPHelper decoder = new NVPHelper();
                //decoder.Add("fx.FATALExceptionMessage", fx.FATALExceptionMessage);
                //decoder.Add("fx.FATALExceptionLongMessage", fx.FATALExceptionLongMessage);
                //string printerror = Utils.BuildResponse(decoder, "SDK Error Page", "");
                //Session["AllResponse"] = printerror;
                //Response.Redirect("../Public/allResponse.aspx");
                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, fx.Message, fx);
                returnResponse = "false";
            }
            catch (Exception ex)
            {
                IoC.Resolve<ILogService>().InsertLog(LogTypeEnum.Unknown, ex.Message, ex);
                returnResponse = "false";
            }

            return returnResponse;
        }
    }
}