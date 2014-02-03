/*
 * Copyright 2010 PayPal, Inc. All Rights Reserved.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalPlatformNVPSDK
{
    class BaseConstants
    {
        public const string PAYPALLOGFILE = "PAYPALLOGFILE";
        public const int DEFAULT_TIMEOUT = 3600000;
        public const string REQUESTMETHOD = "POST";
        public const string XPAYPALSOURCEValue = "DOTNET_NVP_SDK_V1.1";

        public const string XPAYPALREQUESTSOURCE="X-PAYPAL-REQUEST-SOURCE";

        public const string XPAYPALREQUESTDATAFORMAT = "X-PAYPAL-REQUEST-DATA-FORMAT";
        public const string XPAYPALRESPONSEDATAFORMAT = "X-PAYPAL-RESPONSE-DATA-FORMAT";
        public const string XPayFormat = "NV";


        //Error Messages
        public const string emptyRequest = "NVPRequest: Request Cannot be Empty.  " + "\n";
        public const string emptyHeader = "NVPHeaders: Headers Cannot be Empty.  " + "\n";
        public const string emptyEndpoint = "EndpointUrl: Endpoint Cannot be Empty.  " + "\n";

        public const string generalSDKException = "Error occurred in CallerServices method";

    }
}
