/*
 * Copyright 2010 PayPal, Inc. All Rights Reserved.
 */
using System;


namespace NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalPlatformNVPSDK
{
    /// <summary>
    /// Custom FATALException holds Short and Long messages.
    /// </summary>
    public class FATALException : Exception
    {
        #region Priavte Members
                /// <summary>
        /// Short message
        /// </summary>
        private string FATALExMessage;
        /// <summary>
        /// Long message
        /// </summary>
        private string FATALExpLongMessage;

        #endregion

        #region Constructors

        public FATALException(string FATALExceptionMessage, Exception exception)
        {


            this.FATALExMessage = FATALExceptionMessage;
            this.FATALExpLongMessage = exception.Message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Short message.
        /// </summary>
        public string FATALExceptionMessage
        {
            get
            {
                return FATALExMessage;
            }
            set
            {
                FATALExMessage = value;
            }
        }

        /// <summary>
        /// Long message
        /// </summary>
        public string FATALExceptionLongMessage
        {
            get
            {
                return FATALExpLongMessage;
            }
            set
            {
                FATALExpLongMessage = value;
            }
        }

        #endregion
    }

}
