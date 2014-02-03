/*
 * Copyright 2010 PayPal, Inc. All Rights Reserved.
 */
using System;
using System.IO;
using System.Net;
using System.Collections;
using log4net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalPlatformNVPSDK
{
    public class CallerServices_NVP
    {
        

            private static readonly ILog log = LogManager.GetLogger(BaseConstants.PAYPALLOGFILE);
        /*This method makes the API call, endpoint, header values and nvp request string is passed
        as input */
        public string Call(string NVPRequest, Hashtable NVPHeaders,string endpointUrl)
        {

            validateRequest(NVPRequest, NVPHeaders, endpointUrl);

             System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslError)
                {
                    bool validationResult = true;
                    return validationResult;
                }; 
            try
            {
                HttpWebRequest paypalRequest = (HttpWebRequest)WebRequest.Create(endpointUrl);
                paypalRequest.Method = BaseConstants.REQUESTMETHOD;
                paypalRequest.ContentLength = NVPRequest.Length;
                //paypalRequest.ContentType = "application/x-www-form-urlencoded";
                paypalRequest.Timeout = BaseConstants.DEFAULT_TIMEOUT;

                //
                    if (NVPHeaders[BaseConstants.XPAYPALREQUESTSOURCE] != null && NVPHeaders[BaseConstants.XPAYPALREQUESTSOURCE].ToString().Length > 0)
                    {
                        NVPHeaders[BaseConstants.XPAYPALREQUESTSOURCE] = BaseConstants.XPAYPALSOURCEValue + "-" + NVPHeaders[BaseConstants.XPAYPALREQUESTSOURCE];
                    }
                    else
                    {
                        NVPHeaders[BaseConstants.XPAYPALREQUESTSOURCE] = BaseConstants.XPAYPALSOURCEValue;
                    }
                //

                //Defaulting Request and Response format as NVP
                    if (NVPHeaders[BaseConstants.XPAYPALREQUESTDATAFORMAT] == null)
                    {
                        NVPHeaders[BaseConstants.XPAYPALREQUESTDATAFORMAT] = BaseConstants.XPayFormat;
                    }
                    if (NVPHeaders[BaseConstants.XPAYPALRESPONSEDATAFORMAT] == null)
                    {
                        NVPHeaders[BaseConstants.XPAYPALRESPONSEDATAFORMAT] = BaseConstants.XPayFormat;
                    }
                //
                foreach (DictionaryEntry de in NVPHeaders)
                {
                    paypalRequest.Headers.Add(de.Key.ToString(), de.Value.ToString());
                }



                StreamWriter paypalstreamWriter = new StreamWriter(paypalRequest.GetRequestStream());
                paypalstreamWriter.Write(NVPRequest);
                ///added for logging
                if (log.IsInfoEnabled)
                {
                    log.Info(DateTime.Now.ToString());                   
                    log.Info("###Request Starts###");                   
                    log.Info(NVPRequest);
                    log.Info("###Request Ends###");
                    
                }

                ///
                paypalstreamWriter.Close();

                //API call is made and the response is stored in string "NVPRespons"
                HttpWebResponse paypalResponse = (HttpWebResponse)paypalRequest.GetResponse();
                StreamReader paypalstreamReader = new StreamReader(paypalResponse.GetResponseStream());
                string NVPResponse = paypalstreamReader.ReadToEnd();

                if (log.IsInfoEnabled)
                {
                    log.Info("###Response Starts###");
                    log.Info(NVPResponse);
                    log.Info("#########Response Ends#########");                   
                }
                paypalstreamReader.Close();


                return NVPResponse;
            }
            catch (FATALException FATALEx)
            {
                throw FATALEx;
            }
            catch (Exception ex)
            {
                throw new FATALException(BaseConstants.generalSDKException, ex);
            }

        }

        private void validateRequest(string NVPRequest, Hashtable NVPHeaders, string endpointUrl)
        {
            Exception e = new Exception();
            String Fex = "";
            Boolean isError = false;
            if (NVPRequest == null || NVPRequest.Length < 1)
            {
                Fex = BaseConstants.emptyRequest;
                isError = true;
            }
            if (NVPHeaders == null || NVPHeaders.Count < 1)
            {
                Fex = Fex + BaseConstants.emptyHeader;
                isError = true;
            }
            if (endpointUrl == null || endpointUrl.Length < 1)
            {
                Fex = Fex + BaseConstants.emptyEndpoint;
                isError = true;
            }
            if(isError==true)
                throw new FATALException(Fex, e);
        }
    }

}
