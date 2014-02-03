using System;
//Added for PayPal
using NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalPlatformNVPSDK;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal

{
    public static class Utils
    {

        /// <summary>
        /// Build NVP response into a readable HTML response
        /// </summary>
        /// <param name="response"></param>
        /// <param name="header1"></param>
        /// <param name="header2"></param>
        /// <returns></returns>
        public static string BuildResponse(object response, string header1, string header2)
        {
            if (response != null)
            {
                NVPHelper decoder = new NVPHelper();
                decoder = (NVPHelper)response;


                string res = "<center>";
                if (header1 != null)
                    res = res + "<font size=4 color=black face=Verdana><b>" + header1 + "</b></font>";
                res = res + "<br>";
                res = res + "<br>";

                if (header2 != null)
                    res = res + "<b>" + header2 + "</b><br>";

                res = res + "<br>";

                res = res + "<table width=650 class=api>";


                for (int i = 0; i < decoder.Keys.Count; i++)
                {
                    res = res + "<tr><td align=left> " + decoder.Keys[i].ToString() + ":</td>";
                    res = res + "<td align=left>" + decoder.GetValues(i)[0] + "</td>";
                    res = res + "</tr>";
                    res = res + "<tr>";
                }

                res = res + "</table>";
                res = res + "</center>";
                return res;
            }
            else
            {
                return "Requested action not allowed";
            }

        }
    }
}
