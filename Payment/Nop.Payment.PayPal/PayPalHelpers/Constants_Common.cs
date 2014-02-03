using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal
{
    public class Constants_Common
    {
        //Creating hash table for storing header values
        public static Hashtable headers()
        {

            Hashtable NVPHeaders = new Hashtable();

            if (IoC.Resolve<ISettingManager>().GetSettingValueBoolean("PaymentMethod.PaypalAdaptive.UseSandbox"))
            {
                NVPHeaders["X-PAYPAL-SECURITY-USERID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.UserID"); //"admin_1320635048_biz_api1.sewbie.com"; 
                NVPHeaders["X-PAYPAL-SECURITY-PASSWORD"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.APIAccountPassword"); //"1320635073";
                NVPHeaders["X-PAYPAL-SECURITY-SIGNATURE"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.Signature"); //"AVj0mNaVka-tVQNuy.W.Vh9b.EiCA7NhHUJP55FWsKJZWFSk.8ywgf7Z"; 
                NVPHeaders["X-PAYPAL-APPLICATION-ID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.ApplicationID"); //"APP-80W284485P519543T";
                NVPHeaders["X-PAYPAL-DEVICE-IPADDRESS"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.IPAddress"); //"127.0.0.1";
            }
            else
            {
                NVPHeaders["X-PAYPAL-SECURITY-USERID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.UserID"); //"admin_1320635048_biz_api1.sewbie.com"; 
                NVPHeaders["X-PAYPAL-SECURITY-PASSWORD"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.APIAccountPassword"); //"1320635073";
                NVPHeaders["X-PAYPAL-SECURITY-SIGNATURE"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.Signature"); //"AVj0mNaVka-tVQNuy.W.Vh9b.EiCA7NhHUJP55FWsKJZWFSk.8ywgf7Z"; 
                NVPHeaders["X-PAYPAL-APPLICATION-ID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.ApplicationID");  //"APP-80W284485P519543T";
                NVPHeaders["X-PAYPAL-DEVICE-IPADDRESS"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.IPAddress"); //"127.0.0.1";
            }

            NVPHeaders["X-PAYPAL-REQUEST-DATA-FORMAT"] = "NV";
            NVPHeaders["X-PAYPAL-RESPONSE-DATA-FORMAT"] = "NV";
            return NVPHeaders;
        }

        public static Hashtable sandboxHeaders(){
                Hashtable NVPHeaders = new Hashtable();
                NVPHeaders["X-PAYPAL-SECURITY-USERID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.UserID"); //"admin_1320635048_biz_api1.sewbie.com"; 
                NVPHeaders["X-PAYPAL-SECURITY-PASSWORD"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.APIAccountPassword"); //"1320635073";
                NVPHeaders["X-PAYPAL-SECURITY-SIGNATURE"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.Signature"); //"AVj0mNaVka-tVQNuy.W.Vh9b.EiCA7NhHUJP55FWsKJZWFSk.8ywgf7Z"; 
                NVPHeaders["X-PAYPAL-APPLICATION-ID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.ApplicationID"); //"APP-80W284485P519543T";
                NVPHeaders["X-PAYPAL-DEVICE-IPADDRESS"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.IPAddress"); //"127.0.0.1";
                NVPHeaders["X-PAYPAL-REQUEST-DATA-FORMAT"] = "NV";
                NVPHeaders["X-PAYPAL-RESPONSE-DATA-FORMAT"] = "NV";
                return NVPHeaders;
        }

        public static Hashtable productionHeaders(){
            Hashtable NVPHeaders = new Hashtable();
            NVPHeaders["X-PAYPAL-SECURITY-USERID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.UserID"); //"admin_1320635048_biz_api1.sewbie.com"; 
            NVPHeaders["X-PAYPAL-SECURITY-PASSWORD"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.APIAccountPassword"); //"1320635073";
            NVPHeaders["X-PAYPAL-SECURITY-SIGNATURE"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.Signature"); //"AVj0mNaVka-tVQNuy.W.Vh9b.EiCA7NhHUJP55FWsKJZWFSk.8ywgf7Z"; 
            NVPHeaders["X-PAYPAL-APPLICATION-ID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.ApplicationID");  //"APP-80W284485P519543T";
            NVPHeaders["X-PAYPAL-DEVICE-IPADDRESS"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.IPAddress"); //"127.0.0.1";
            NVPHeaders["X-PAYPAL-REQUEST-DATA-FORMAT"] = "NV";
            NVPHeaders["X-PAYPAL-RESPONSE-DATA-FORMAT"] = "NV";
            return NVPHeaders;
        }
        ////Setting endpoint for adaptive payment and adaptive account.
        //public static string endpoint = "https://svcs.sandbox.paypal.com/AdaptivePayments/";
        //public static string endpoint_AA = "https://svcs.sandbox.paypal.com/AdaptiveAccounts/";
        //public static string endpoint_PE = "https://svcs.sandbox.paypal.com/Permissions/";

    }
}
