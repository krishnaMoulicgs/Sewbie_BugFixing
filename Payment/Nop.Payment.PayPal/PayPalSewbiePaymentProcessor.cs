using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalPlatformNVPSDK;
using System.Collections;
using System.Collections.Generic;

//using NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalSvc;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal
{
    /// <summary>
    /// Paypal Direct payment processor
    /// </summary>
    public class PayPalSewbiePaymentProcessor : IPaymentMethod
    {
        #region Fields
        //private bool useSandBox = true;
        string url = CommonHelper.GetStoreLocation(false);
        string returnURL = CommonHelper.GetStoreLocation(false) + "PaypalAPHandler.aspx";
        string cancelURL = CommonHelper.GetStoreLocation(false) + "PaypalCancel.aspx";
        string ipnURL = CommonHelper.GetStoreLocation(false) + "PaypalAPIPNHandler.aspx";
        //Setting endpoint for adaptive payment and adaptive account.
        private string endpoint;

        private Hashtable headers()
        {
            Hashtable NVPHeaders = new Hashtable();
            if (UseSandbox)
            {
                NVPHeaders["X-PAYPAL-SECURITY-USERID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.UserID");
                NVPHeaders["X-PAYPAL-SECURITY-PASSWORD"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.APIAccountPassword");
                NVPHeaders["X-PAYPAL-SECURITY-SIGNATURE"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.Signature");
                NVPHeaders["X-PAYPAL-APPLICATION-ID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.ApplicationID"); //"APP-80W284485P519543T";
                NVPHeaders["X-PAYPAL-DEVICE-IPADDRESS"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.IPAddress");
            }
            else
            {
                NVPHeaders["X-PAYPAL-SECURITY-USERID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.UserID");
                NVPHeaders["X-PAYPAL-SECURITY-PASSWORD"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.APIAccountPassword");
                NVPHeaders["X-PAYPAL-SECURITY-SIGNATURE"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.Signature");
                NVPHeaders["X-PAYPAL-APPLICATION-ID"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.ApplicationID");
                NVPHeaders["X-PAYPAL-DEVICE-IPADDRESS"] = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.IPAddress");
            }


            NVPHeaders["X-PAYPAL-REQUEST-DATA-FORMAT"] = "NV";
            NVPHeaders["X-PAYPAL-RESPONSE-DATA-FORMAT"] = "NV";

            return NVPHeaders;
        }

        #endregion

        /// <summary>
        /// Gets Paypal URL
        /// </summary>
        /// <returns></returns>
        private string GetPaypalUrl()
        {
            return UseSandbox ? "https://www.sandbox.paypal.com/us/cgi-bin/webscr" :
                "https://www.paypal.com/us/cgi-bin/webscr";
        }



        #region Methods



        /// <summary>
        /// Initializes the PayPalSewbiePaymentProcessor
        /// </summary>
        private void InitSettings()
        {
            if (!UseSandbox)
            {
                returnURL = CommonHelper.GetStoreLocation(false) + IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.ReturnPage");
                cancelURL = CommonHelper.GetStoreLocation(false) + IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.CancelPage");
                ipnURL = CommonHelper.GetStoreLocation(false) + IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Production.IpnPage");
            }
            else
            {
                returnURL = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.ReturnPage");
                cancelURL = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.CancelPage");
                ipnURL = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.IpnPage");
            }
        }

        /// <summary>
        /// Verifies IPN
        /// </summary>
        /// <param name="formString">Form string</param>
        /// <param name="values">Values</param>
        /// <returns>Result</returns>
        public bool VerifyIPN(string formString, out Dictionary<string, string> values)
        {
            InitSettings();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GetPaypalUrl());
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            string formContent = string.Format("{0}&cmd=_notify-validate", formString);
            req.ContentLength = formContent.Length;

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream(), Encoding.ASCII))
            {
                sw.Write(formContent);
            }

            string response = null;
            using (StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                response = HttpUtility.UrlDecode(sr.ReadToEnd());
            }
            bool success = response.Trim().Equals("VERIFIED", StringComparison.OrdinalIgnoreCase);

            values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string l in formString.Split('&'))
            {
                string line = l.Trim();
                int equalPox = line.IndexOf('=');
                if (equalPox >= 0)
                    values.Add(line.Substring(0, equalPox), line.Substring(equalPox + 1));
            }

            return success;
        }
        
        /// <summary>
        /// Process payment
        /// </summary>
        /// <param name="paymentInfo">Payment info required for an order processing</param>
        /// <param name="customer">Customer</param>
        /// <param name="orderGuid">Unique order identifier</param>
        /// <param name="processPaymentResult">Process payment result</param>
        public void ProcessPayment(PaymentInfo paymentInfo, Customer customer, Guid orderGuid, ref ProcessPaymentResult processPaymentResult)
        {
            processPaymentResult.PaymentStatus = PaymentStatusEnum.Pending;
        }

        /// <summary>
        /// Post process payment (payment gateways that require redirecting)
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>The error status, or String.Empty if no errors</returns>
        public string PostProcessPayment(Order order)
        {
            try
            {
                InitSettings();

                string vendorEmail = order.OrderProductVariants[0].NpProductVariant.Vendor.PaypalEmailAddress;

                string endpoint = PaypalEndpoint + "Pay";
                NVPHelper NVPRequest = new NVPHelper();
                //requestEnvelope.errorLanguage is common for all the request
                NVPRequest[NVPConstant.requestEnvelopeerrorLanguage] = "en_US";

                NVPRequest[NVPConstant.Pay.actionType] = "PAY";
                NVPRequest[NVPConstant.Pay.currencyCode] = "USD";
                NVPRequest[NVPConstant.Pay.feesPayer] = "EACHRECEIVER";
                NVPRequest[NVPConstant.Pay.memo] = "Order reference number: " + order.OrderGuid;
                NVPRequest[NVPConstant.Pay.receiverListreceiveramount_0] = order.OrderTotal.ToString();
                NVPRequest[NVPConstant.Pay.receiverListreceiveremail_0] = vendorEmail;
                NVPRequest[NVPConstant.Pay.receiverListreceiverprimary_0] = "false";
                //do not pass this.  It is not necessary to complete payment and
                //may not actually be the customers paypal account.
                //NVPRequest[NVPConstant.Pay.senderEmail] = order.Customer.Email;
                NVPRequest[NVPConstant.Pay.trackingId] = order.OrderGuid.ToString();

                NVPRequest[NVPConstant.Pay.cancelUrl] = cancelURL;
                NVPRequest[NVPConstant.Pay.returnUrl] = returnURL;
                NVPRequest[NVPConstant.Pay.ipnUrl] = ipnURL;

                //
                //Needed for pre approval.  Not doing pre approvals at the moment.
                //
                //if (txtPreapprovalkey.Text.Trim() != "")
                //{
                //    NVPRequest[NVPConstant.Pay.preapprovalKey] = txtPreapprovalkey.Text;
                //}

                string strrequestforNvp = NVPRequest.Encode();
                //calling Call method where actuall API call is made, NVP string, header value and end point are passed as the input.
                CallerServices_NVP CallerServices = new CallerServices_NVP();
                string stresponsenvp = CallerServices.Call(strrequestforNvp, headers(), endpoint);

                //Response is send to Decoder method where it is decoded to readable hash table
                NVPHelper decoder = new NVPHelper();
                decoder.Decode(stresponsenvp);

                //Response obtained after the API call is stored in print string to display all the response
                //string print = Utils.BuildResponse(decoder, "Pay", "");
                ////Storing response string in session
                HttpContext.Current.Session["PaypalAPResponse"] = decoder; //
                ////COMPLETED, CREATED, Success

                string redirectURL = string.Empty;

                //this is a complete success, ready to allow the user to redirect and pay.
                if (decoder != null && decoder["responseEnvelope.ack"].Equals("Success") && NVPRequest[NVPConstant.Pay.actionType] == "PAY" && decoder["paymentExecStatus"].Equals("CREATED"))
                {
                    if (UseLightBox) {
                        //HttpContext.Current.Session["PayRedirect"] = redirectURLStarter + "paykey=" + decoder["payKey"];
                        HttpContext.Current.Session["PayRedirect"] = PaypalLightboxURL + "expType=light&paykey=" + decoder["payKey"];
                        redirectURL = PaypalLightboxURL + "expType=light&paykey=" + decoder["payKey"];
                    }
                    else { 
                        HttpContext.Current.Session["PayRedirect"] = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];
                        //redirectURL = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];
                    }
                }
                //this is assuming we did a preapproval. this will execute the payment and charge the user.
                else if (decoder != null && decoder["responseEnvelope.ack"].Equals("Success") && (String.Empty != "") && NVPRequest[NVPConstant.Pay.actionType] == "CREATE" && decoder["paymentExecStatus"].Equals("CREATED"))
                {
                    HttpContext.Current.Session["PayRedirect"] = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];

                    redirectURL = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];

                    HttpContext.Current.Session["setPayKey"] = decoder["payKey"];
                    HttpContext.Current.Session["executePayKey"] = decoder["payKey"];
                }
                //this assumes that if we executed a payment using above preapproval this would be our return message.
                else if (decoder != null && decoder["responseEnvelope.ack"].Equals("Success") && NVPRequest[NVPConstant.Pay.actionType] == "CREATE" && decoder["paymentExecStatus"].Equals("CREATED"))
                {
                    string senderApiEmail = "admin_1320635048_biz_api1.sewbie.com";
                    string senderEmail = NVPRequest[NVPConstant.Pay.senderEmail];
                    string senderTruncatedEmail = senderEmail.Substring(0, senderEmail.IndexOf("@"));
                    string senderApiTruncatedEmail = senderApiEmail.Substring(0, senderApiEmail.IndexOf("_api"));
                    if (senderTruncatedEmail == senderApiTruncatedEmail)
                    {
                        HttpContext.Current.Session["PayRedirect"] = PaypalRedirectURL+ "cmd=_ap-payment&paykey=" + decoder["payKey"];
                        redirectURL = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];
                        HttpContext.Current.Session["setPayKey"] = decoder["payKey"];
                        HttpContext.Current.Session["executePayKey"] = decoder["payKey"];
                    }
                    else
                    {
                        HttpContext.Current.Session["PayRedirect"] = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];
                        redirectURL = PaypalRedirectURL + "cmd=_ap-payment&paykey=" + decoder["payKey"];
                    }
                } //if it's not a success of any type, check if it's a success with warning otherwise process the error.
                else if (decoder != null &&
                            (!decoder["responseEnvolope.ack"].Equals("Success") ||
                                !decoder["responseEnvolope.ack"].Equals("SuccessWithWarning")))
                {
                    //paypal has not approved the request.  Do we need to undo everything to this point and set the customer straight?
                    //order has been processed.  Cannot cancel.
                    HttpContext.Current.Response.Redirect(CommonHelper.GetStoreLocation(false) + "PaypalAPPaymentRequestFailure.aspx");
                    //logService.InsertLog(LogTypeEnum.OrderError, decoder[""].ToString(), "error logging");
                }

                if (UseLightBox)
                {
                    return redirectURL;
                }
                else
                {
                    //According to paypal redirects need to be performed via javascript.
                    //At this point we redirect to our redirector page, redirects from server will fail.
                    HttpContext.Current.Response.Redirect(CommonHelper.GetStoreLocation(false) + "PaypalAPRedirect.aspx");
                    return String.Empty;
                }
            }
            catch (FATALException fx)
            {
                NVPHelper decoder = new NVPHelper();
                decoder.Add("fx.FATALExceptionMessage", fx.FATALExceptionMessage);
                decoder.Add("fx.FATALExceptionLongMessage", fx.FATALExceptionLongMessage);
                string printerror = Utils.BuildResponse(decoder, "SDK Error Page", "");

                //logService.InsertLog(LogTypeEnum.CommonError, "Post Process FATAL Exception" + printerror, "logging");
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets additional handling fee
        /// </summary>
        /// <returns>Additional handling fee</returns>
        public decimal GetAdditionalHandlingFee()
        {
            return IoC.Resolve<ISettingManager>().GetSettingValueDecimalNative("PaymentMethod.PaypalDirect.AdditionalFee");
        }

        /// <summary>
        /// Captures payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="processPaymentResult">Process payment result</param>
        public void Capture(Order order, ref ProcessPaymentResult processPaymentResult)
        {

        }

        /// <summary>
        /// Authorizes the payment
        /// </summary>
        /// <param name="paymentInfo">Payment info required for an order processing</param>
        /// <param name="customer">Customer</param>
        /// <param name="orderGuid">Unique order identifier</param>
        /// <param name="processPaymentResult">Process payment result</param>
        /// <param name="authorizeOnly">A value indicating whether to authorize only; true - authorize; false - sale</param>
        protected void AuthorizeOrSale(PaymentInfo paymentInfo, Customer customer,
            Guid orderGuid, ProcessPaymentResult processPaymentResult, bool authorizeOnly)
        {

        }

        /// <summary>
        /// Refunds payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="cancelPaymentResult">Cancel payment result</param>        
        public void Refund(Order order, ref CancelPaymentResult cancelPaymentResult)
        {

        }

        /// <summary>
        /// Voids payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="cancelPaymentResult">Cancel payment result</param>        
        public void Void(Order order, ref CancelPaymentResult cancelPaymentResult)
        {

        }

        /// <summary>
        /// Process recurring payment
        /// </summary>
        /// <param name="paymentInfo">Payment info required for an order processing</param>
        /// <param name="customer">Customer</param>
        /// <param name="orderGuid">Unique order identifier</param>
        /// <param name="processPaymentResult">Process payment result</param>
        public void ProcessRecurringPayment(PaymentInfo paymentInfo, Customer customer, Guid orderGuid, ref ProcessPaymentResult processPaymentResult)
        {

        }

        /// <summary>
        /// Cancels recurring payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="cancelPaymentResult">Cancel payment result</param>        
        public void CancelRecurringPayment(Order order, ref CancelPaymentResult cancelPaymentResult)
        {
        }

        #endregion

        #region Properties

        ///<summary>
        /// Gets Paypal API version
        ///</summary>
        public string APIVersion
        {
            get
            {
                return "1.1.0";
            }
        }

        /// <summary>
        /// Gets a value indicating whether capture is supported
        /// </summary>
        public bool CanCapture
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether partial refund is supported
        /// </summary>
        public bool CanPartiallyRefund
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether refund is supported
        /// </summary>
        public bool CanRefund
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether void is supported
        /// </summary>
        public bool CanVoid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a recurring payment type of payment method
        /// </summary>
        /// <returns>A recurring payment type of payment method</returns>
        public RecurringPaymentTypeEnum SupportRecurringPayments
        {
            get
            {
                return RecurringPaymentTypeEnum.Automatic;
            }
        }

        /// <summary>
        /// Gets a payment method type
        /// </summary>
        /// <returns>A payment method type</returns>
        public PaymentMethodTypeEnum PaymentMethodType
        {
            get
            {
                return PaymentMethodTypeEnum.Standard;
            }
        }


        public bool UseLightBox
        {
            get
            {
                return SettingManager.GetSettingValueBoolean("PaymentMethod.PaypalAdaptive.UseLightBox");
            }
        }

        public bool UseSandbox
        {
            get
            {
                return SettingManager.GetSettingValueBoolean("PaymentMethod.PaypalAdaptive.UseSandbox");
            }
        }

        public string PaypalEndpoint
        {
            get
            {
                if (UseSandbox) { return SettingManager.GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.Endpoint"); }
                else { return SettingManager.GetSettingValue("PaymentMethod.PaypalAdaptive.Production.Endpoint"); }
            }
        }

        public string PaypalRedirectURL
        {
            get
            {
                if (UseSandbox) { return SettingManager.GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.RedirectURL"); }
                else { return SettingManager.GetSettingValue("PaymentMethod.PaypalAdaptive.Production.RedirectURL"); }
            }
        }

        public string PaypalLightboxURL
        {
            get
            {
                if (UseSandbox) { return SettingManager.GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.LightBoxUrl"); }
                else { return SettingManager.GetSettingValue("PaymentMethod.PaypalAdaptive.Sandbox.LightBoxUrl"); }
            }
        }
        #endregion


        public ILogService LogService
        {
            get { return IoC.Resolve<ILogService>(); }
        }

        public ISettingManager SettingManager
        {
            get { return IoC.Resolve<ISettingManager>(); }
        }

    }
}