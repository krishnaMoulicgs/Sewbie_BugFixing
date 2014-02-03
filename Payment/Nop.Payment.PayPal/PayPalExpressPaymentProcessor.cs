//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Globalization;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.Payment.Methods.PayPal.PayPalSvc;
using NopSolutions.NopCommerce.BusinessLogic.Audit;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal
{
    /// <summary>
    /// Paypal Express payment processor
    /// </summary>
    public class PayPalExpressPaymentProcessor : IPaymentMethod
    {
        #region Fields
        private bool useSandBox = true;
        private string APIAccountName;
        private string APIAccountPassword;
        private string Signature;
        private PayPalAPISoapBinding service1;
        private PayPalAPIAASoapBinding service2;
        #endregion

        #region Methods
        /// <summary>
        /// Gets transaction mode configured by store owner
        /// </summary>
        /// <returns></returns>
        private TransactMode GetCurrentTransactionMode()
        {
            TransactMode transactionModeEnum = TransactMode.Authorize;
            string transactionMode = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalExpress.TransactionMode");
            if (!String.IsNullOrEmpty(transactionMode))
                transactionModeEnum = (TransactMode)Enum.Parse(typeof(TransactMode), transactionMode);
            return transactionModeEnum;
        }

        /// <summary>
        /// Initializes the PayPalExpressPaymentProcessor
        /// </summary>
        private void InitSettings()
        {
            useSandBox = IoC.Resolve<ISettingManager>().GetSettingValueBoolean("PaymentMethod.PaypalExpress.UseSandbox");
            APIAccountName = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalExpress.APIAccountName");
            APIAccountPassword = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalExpress.APIAccountPassword");
            Signature = IoC.Resolve<ISettingManager>().GetSettingValue("PaymentMethod.PaypalExpress.Signature");

            if (string.IsNullOrEmpty(APIAccountName))
                throw new NopException("Paypal Express API Account Name is empty");

            if (string.IsNullOrEmpty(Signature))
                throw new NopException("Paypal Express API Account Password is empty");

            if (string.IsNullOrEmpty(APIAccountPassword))
                throw new NopException("Paypal Express Signature is empty");

            service1 = new PayPalAPISoapBinding();
            service2 = new PayPalAPIAASoapBinding();


            if (!useSandBox)
            {
                service2.Url = service1.Url = "https://api-3t.paypal.com/2.0/";
            }
            else
            {
                service2.Url = service1.Url = "https://api-3t.sandbox.paypal.com/2.0/";
            }

            service1.RequesterCredentials = new CustomSecurityHeaderType();
            service1.RequesterCredentials.Credentials = new UserIdPasswordType();
            service1.RequesterCredentials.Credentials.Username = APIAccountName;
            service1.RequesterCredentials.Credentials.Password = APIAccountPassword;
            service1.RequesterCredentials.Credentials.Signature = Signature;
            service1.RequesterCredentials.Credentials.Subject = "";

            service2.RequesterCredentials = new CustomSecurityHeaderType();
            service2.RequesterCredentials.Credentials = new UserIdPasswordType();
            service2.RequesterCredentials.Credentials.Username = APIAccountName;
            service2.RequesterCredentials.Credentials.Password = APIAccountPassword;
            service2.RequesterCredentials.Credentials.Signature = Signature;
            service2.RequesterCredentials.Credentials.Subject = "";
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
            DoExpressCheckout(paymentInfo, orderGuid, processPaymentResult);
        }

        /// <summary>
        /// Post process payment (payment gateways that require redirecting)
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>The error status, or String.Empty if no errors</returns>
        public string PostProcessPayment(Order order)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets additional handling fee
        /// </summary>
        /// <returns>Additional handling fee</returns>
        public decimal GetAdditionalHandlingFee()
        {
            return decimal.Zero;
        }

        /// <summary>
        /// Captures payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="processPaymentResult">Process payment result</param>
        public void Capture(Order order, ref ProcessPaymentResult processPaymentResult)
        {
            InitSettings();

            string authorizationID = processPaymentResult.AuthorizationTransactionId;
            DoCaptureReq req = new DoCaptureReq();
            req.DoCaptureRequest = new DoCaptureRequestType();
            req.DoCaptureRequest.Version = this.APIVersion;
            req.DoCaptureRequest.AuthorizationID = authorizationID;
            req.DoCaptureRequest.Amount = new BasicAmountType();
            req.DoCaptureRequest.Amount.Value = order.OrderTotal.ToString("N", new CultureInfo("en-us"));
            req.DoCaptureRequest.Amount.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);
            req.DoCaptureRequest.CompleteType = CompleteCodeType.Complete;
            DoCaptureResponseType response = service2.DoCapture(req);

            string error = string.Empty;
            bool Success = PaypalHelper.CheckSuccess(response, out error);
            if (Success)
            {
                processPaymentResult.PaymentStatus = PaymentStatusEnum.Paid;
                processPaymentResult.CaptureTransactionId = response.DoCaptureResponseDetails.PaymentInfo.TransactionID;
                processPaymentResult.CaptureTransactionResult = response.Ack.ToString();
            }
            else
            {
                processPaymentResult.Error = error;
            }
        }

        /// <summary>
        /// Sets paypal express checkout
        /// </summary>
        /// <param name="OrderTotal">Order total</param>
        /// <param name="ReturnURL">Return URL</param>
        /// <param name="CancelURL">Cancel URL</param>
        /// <returns>Express checkout URL</returns>
        public string SetExpressCheckout(decimal OrderTotal, 
            string ReturnURL, string CancelURL)
        {
            InitSettings();
            TransactMode transactionMode = GetCurrentTransactionMode();

            SetExpressCheckoutReq req = new SetExpressCheckoutReq();
            req.SetExpressCheckoutRequest = new SetExpressCheckoutRequestType();
            req.SetExpressCheckoutRequest.Version = this.APIVersion;
            
            SetExpressCheckoutRequestDetailsType details = new SetExpressCheckoutRequestDetailsType();
            req.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails = details;
            
            if (transactionMode == TransactMode.Authorize)
                details.PaymentAction = PaymentActionCodeType.Authorization;
            else
                details.PaymentAction = PaymentActionCodeType.Sale;
            details.PaymentActionSpecified = true;
            details.OrderTotal = new BasicAmountType();
            details.OrderTotal.Value = OrderTotal.ToString("N", new CultureInfo("en-us"));
            details.OrderTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);
            details.ReturnURL = ReturnURL;
            details.CancelURL = CancelURL;
            SetExpressCheckoutResponseType response = service2.SetExpressCheckout(req);
            string error;
            if (PaypalHelper.CheckSuccess(response, out error))
                return GetPaypalUrl(response.Token);
            throw new NopException(error);
        }

        /// <summary>
        /// Special method which Sets paypal express checkout
        /// using the cart.  This method also allows for the items to be included
        /// which will appear in the paypal checkout page.
        /// </summary>
        /// <param name="OrderTotal">Order total</param>
        /// <param name="ReturnURL">Return URL</param>
        /// <param name="CancelURL">Cancel URL</param>
        /// <returns>Express checkout URL</returns>
        public string SetExpressCheckout(ShoppingCart Cart, decimal OrderTotal,
            string ReturnURL, string CancelURL, string NotifyURL, string noteToSeller)
        {
            InitSettings();
            TransactMode transactionMode = GetCurrentTransactionMode();

            SetExpressCheckoutReq req = new SetExpressCheckoutReq();
            req.SetExpressCheckoutRequest = new SetExpressCheckoutRequestType();
            req.SetExpressCheckoutRequest.Version = this.APIVersion;
            
            SetExpressCheckoutRequestDetailsType details = new SetExpressCheckoutRequestDetailsType();
            
            

            details.PaymentDetails = new PaymentDetailsType[1];

            details.AllowNote = "1";

            PaymentDetailsType payDetails = new PaymentDetailsType();
            details.BrandName = "Sewbie";
            details.OrderTotal = new BasicAmountType();
            details.OrderTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);
            details.OrderTotal.Value = OrderTotal.ToString("N", new CultureInfo("en-us"));

            details.Custom = Cart[0].VendorID.ToString() + ":" + Cart[0].CustomerSession.CustomerId;
            
            decimal discountAmount = 0, subTotalWithoutDiscount = 0, subTotalWithDiscount = 0;
            NopCommerce.BusinessLogic.Promo.Discounts.Discount appliedDiscount = new NopCommerce.BusinessLogic.Promo.Discounts.Discount();
            string scError = IoC.Resolve<IShoppingCartService>().GetShoppingCartSubTotal(Cart, NopCommerce.BusinessLogic.NopContext.Current.User, out discountAmount, out appliedDiscount, out subTotalWithoutDiscount, out subTotalWithDiscount);
            //TODO: We may need to put an error check in here.
            payDetails.ItemTotal = new BasicAmountType();
            payDetails.ItemTotal.Value = subTotalWithoutDiscount.ToString("N", new CultureInfo("en-us"));
            payDetails.ItemTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

            payDetails.TaxTotal = new BasicAmountType();
            payDetails.TaxTotal.Value = 0.ToString("N", new CultureInfo("en-us"));
            payDetails.TaxTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

            decimal shippingAmount = 0;
            payDetails.ShippingTotal = new BasicAmountType();
            foreach (ShoppingCartItem sci in Cart)
            {
                shippingAmount = shippingAmount + sci.AdditionalShippingCharge;
            }
            payDetails.ShippingTotal.Value = shippingAmount.ToString("N", new CultureInfo("en-us"));
            payDetails.ShippingTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

            payDetails.HandlingTotal = new BasicAmountType();
            payDetails.HandlingTotal.Value = 0.ToString("N", new CultureInfo("en-us"));
            payDetails.HandlingTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

            payDetails.InsuranceTotal = new BasicAmountType();
            payDetails.InsuranceTotal.Value = 0.ToString("N", new CultureInfo("en-us"));
            payDetails.InsuranceTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

            //payDetails.ShipToAddress.Name = "";
            //payDetails.ShipToAddress.Street1 = "";
            //payDetails.ShipToAddress.Street2 = "";
            //payDetails.ShipToAddress.CityName = "";
            //payDetails.ShipToAddress.PostalCode = "";
            //payDetails.ShipToAddress.StateOrProvince = "";
            
            payDetails.NoteText = noteToSeller.Length > 0? noteToSeller:String.Empty;

            payDetails.SellerDetails = new SellerDetailsType();
            payDetails.SellerDetails.PayPalAccountID = Cart[0].Vendor.PaypalEmailAddress;

            payDetails.PaymentDetailsItem = new PaymentDetailsItemType[Cart.TotalProducts];

            payDetails.NotifyURL = NotifyURL;

            int itemCounter = 0;
            foreach (ShoppingCartItem sci in Cart)
            {
               PaymentDetailsItemType item = new PaymentDetailsItemType();
               item.Name = sci.ProductVariant.Product.Name;
               item.Number = sci.ProductVariant.Product.ProductId.ToString();
               item.Description = sci.ProductVariant.Product.ShortDescription;

               item.Amount = new BasicAmountType();
               item.Amount.Value = sci.ProductVariant.Price.ToString("N", new CultureInfo("en-us"));
               item.Amount.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

               item.Quantity = sci.Quantity.ToString();

               item.Tax = new BasicAmountType();
               item.Tax.Value = 0.ToString("N", new CultureInfo("en-us"));;
               item.Tax.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);

               payDetails.PaymentDetailsItem[itemCounter] = item;
               
               itemCounter++;
            }

            details.PaymentDetails[0] = payDetails;

            details.PaymentAction = PaymentActionCodeType.Sale;
            details.PaymentActionSpecified = true;
            details.ReturnURL = ReturnURL;
            details.CancelURL = CancelURL;
            req.SetExpressCheckoutRequest.SetExpressCheckoutRequestDetails = details;

            SetExpressCheckoutResponseType response = service2.SetExpressCheckout(req);
            string error;
            if (PaypalHelper.CheckSuccess(response, out error))
                return GetPaypalUrl(response.Token);
            throw new NopException(error);
        }

        /// <summary>
        /// Gets a paypal express checkout result
        /// </summary>
        /// <param name="token">paypal express checkout token</param>
        /// <returns>Paypal payer</returns>
        public PaypalPayer GetExpressCheckout(string token)
        {
            InitSettings();
            GetExpressCheckoutDetailsReq req = new GetExpressCheckoutDetailsReq();
            GetExpressCheckoutDetailsRequestType request = new GetExpressCheckoutDetailsRequestType();
            req.GetExpressCheckoutDetailsRequest = request;

            request.Token = token;
            request.Version = this.APIVersion;

            GetExpressCheckoutDetailsResponseType response = service2.GetExpressCheckoutDetails(req);

            string error;
            ILogService logService = IoC.Resolve<ILogService>();

            if (!PaypalHelper.CheckSuccess(response, out error)) {
                //first we need to log the error for our own good.  We can trouble shoot if
                //we determine the issue is occuring here.  So ... first thing's first.
                foreach(ErrorType errorses in response.Errors){
                    System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();

                    sbErrors.AppendLine(errorses.ErrorCode);
                    sbErrors.AppendLine(errorses.LongMessage);
                    sbErrors.AppendLine(response.ToString());

                    logService.InsertLog(LogTypeEnum.OrderError, sbErrors.ToString(), "Paypal GetExpresCheckout Failure");
                }

                logService.InsertLog(LogTypeEnum.OrderError, response.Ack.ToString(), "Paypal GetExpressCheckout Failure");

                //we throw the user to the custom error page with this.
                throw new NopException(error);
            }
            
            PaypalPayer payer = new PaypalPayer();
            payer.PayerEmail = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Payer;
            payer.FirstName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerName.FirstName;
            payer.LastName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerName.LastName;
            payer.CompanyName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerBusiness;
            payer.Address1 = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.Street1;
            payer.Address2 = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.Street2;
            payer.City = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.CityName;
            payer.State = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.StateOrProvince;
            payer.Zipcode = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.PostalCode;
            payer.Phone = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.ContactPhone;
            payer.PaypalCountryName = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.CountryName;
            payer.CountryCode = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Address.Country.ToString();
            payer.PayerID = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
            payer.VendorId = response.GetExpressCheckoutDetailsResponseDetails.Custom.Split(Convert.ToChar(":"))[0];
            payer.CustomerId = response.GetExpressCheckoutDetailsResponseDetails.Custom.Split(Convert.ToChar(":"))[1];
            payer.Note = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].NoteText == null ? String.Empty : response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].NoteText;
            payer.Token = response.GetExpressCheckoutDetailsResponseDetails.Token;
            payer.PaypalVendorAccount = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0].SellerDetails.PayPalAccountID;
            
            return payer;
        }

        /// <summary>
        /// Do paypal express checkout
        /// </summary>
        /// <param name="paymentInfo">Payment info required for an order processing</param>
        /// <param name="orderGuid">Unique order identifier</param>
        /// <param name="processPaymentResult">Process payment result</param>
        public void DoExpressCheckout(PaymentInfo paymentInfo, 
            Guid orderGuid,  ProcessPaymentResult processPaymentResult)
        {
            PaypalPayer payer = GetExpressCheckout(paymentInfo.PaypalToken);
            
            InitSettings();
            TransactMode transactionMode = GetCurrentTransactionMode();

            DoExpressCheckoutPaymentReq req = new DoExpressCheckoutPaymentReq();
            DoExpressCheckoutPaymentRequestType request = new DoExpressCheckoutPaymentRequestType();
            req.DoExpressCheckoutPaymentRequest = request;
            request.Version = this.APIVersion;
            DoExpressCheckoutPaymentRequestDetailsType details = new DoExpressCheckoutPaymentRequestDetailsType();
            request.DoExpressCheckoutPaymentRequestDetails = details;
            details.PaymentAction = PaymentActionCodeType.Sale;
            
            details.PaymentActionSpecified = true;
            details.Token = paymentInfo.PaypalToken;
            details.PayerID = paymentInfo.PaypalPayerId;
            details.PaymentDetails = new PaymentDetailsType[1];
            PaymentDetailsType paymentDetails1 = new PaymentDetailsType();
            details.PaymentDetails[0] = paymentDetails1;
            paymentDetails1.SellerDetails = new SellerDetailsType();
            //Order order = IoC.Resolve<OrderService>().GetOrderByGuid(orderGuid);
            paymentDetails1.SellerDetails.PayPalAccountID = payer.PaypalVendorAccount;// order.OrderProductVariants[0].ProductVariant.Vendor.PaypalEmailAddress;
            paymentDetails1.OrderTotal = new BasicAmountType();
            paymentDetails1.OrderTotal.Value = paymentInfo.OrderTotal.ToString("N", new CultureInfo("en-us"));
            paymentDetails1.OrderTotal.currencyID = PaypalHelper.GetPaypalCurrency(IoC.Resolve<ICurrencyService>().PrimaryStoreCurrency);
            paymentDetails1.Custom = orderGuid.ToString();
            paymentDetails1.ButtonSource = "nopCommerceCart";
            
            DoExpressCheckoutPaymentResponseType response = service2.DoExpressCheckoutPayment(req);
            string error;
            if (!PaypalHelper.CheckSuccess(response, out error))
                throw new NopException(error);

            if (response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo != null &&
                response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0] != null)
            {
                processPaymentResult.AuthorizationTransactionId = response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].TransactionID;
                processPaymentResult.AuthorizationTransactionResult = response.Ack.ToString();

                if (transactionMode == TransactMode.Authorize)
                    processPaymentResult.PaymentStatus = PaymentStatusEnum.Authorized;
                else
                    processPaymentResult.PaymentStatus = PaymentStatusEnum.Paid;
            }
            else
            {
                throw new NopException("response.DoExpressCheckoutPaymentResponseDetails.PaymentInfo is null");
            }
        }

        /// <summary>
        /// Gets Paypal URL
        /// </summary>
        /// <param name="token">Paypal express token</param>
        /// <returns>URL</returns>
        private string GetPaypalUrl(string token)
        {
            //return useSandBox ? "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + token  + "&useraction=commit":
            //    "https://www.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + token + "&useraction=commit";
            return useSandBox ? "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + token :
                "https://www.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + token;
        }

        /// <summary>
        /// Refunds payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="cancelPaymentResult">Cancel payment result</param>        
        public void Refund(Order order, ref CancelPaymentResult cancelPaymentResult)
        {
            throw new NopException("Refund method not supported");
        }

        /// <summary>
        /// Voids payment
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="cancelPaymentResult">Cancel payment result</param>        
        public void Void(Order order, ref CancelPaymentResult cancelPaymentResult)
        {
            throw new NopException("Void method not supported");
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
            throw new NopException("Recurring payments not supported");
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

        #region Properies

        /// <summary>
        /// Gets Paypal API version
        /// </summary>
        public string APIVersion
        {
            get
            {
                return "104";
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
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether void is supported
        /// </summary>
        public bool CanVoid
        {
            get
            {
                return false;
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
                return RecurringPaymentTypeEnum.NotSupported;
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
                return PaymentMethodTypeEnum.Button;
            }
        }
        
        #endregion
    }
}
