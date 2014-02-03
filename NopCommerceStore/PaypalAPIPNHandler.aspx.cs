using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Payment.Methods.PayPal;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
namespace NopSolutions.NopCommerce.Web
{
    public partial class PaypalAPIPNHandlerPage : BaseNopFrontendPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonHelper.SetResponseNoCache(Response);

            if (!Page.IsPostBack)
            {
                byte[] param = Request.BinaryRead(Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                Dictionary<string, string> values;

                PayPalSewbiePaymentProcessor processor = new PayPalSewbiePaymentProcessor();
                if (processor.VerifyIPN(strRequest, out values))
                {
                    #region values
                    decimal total = decimal.Zero;
                    try
                    {
                        total = decimal.Parse(values[Server.UrlEncode("transaction[0].amount")].Split("+".ToCharArray())[1], new CultureInfo("en-US"));
                    }
                    catch { }//value could not be converted to an decimal.

                    string transaction_type = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction_type"), out transaction_type);
                    string status = string.Empty;
                    values.TryGetValue(Server.UrlEncode("status"), out status);
                    string sender_email = string.Empty;
                    values.TryGetValue(Server.UrlEncode("sender_email"), out sender_email);
                    string action_type = string.Empty;
                    values.TryGetValue(Server.UrlEncode("action_type"), out action_type);
                    string payment_request_date = string.Empty;
                    values.TryGetValue(Server.UrlEncode("payment_request_date"), out payment_request_date);
                    string reverse_all_parallel_payments_on_error = string.Empty;
                    values.TryGetValue(Server.UrlEncode("reverse_all_parallel_payments_on_error"), out reverse_all_parallel_payments_on_error);

                    //transaction level info.
                    string transaction_0_id = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].id"), out transaction_0_id);
                    string transaction_0_status = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].status"), out transaction_0_status);
                    string transaction_0_id_for_sender = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].id_for_sender"), out transaction_0_id_for_sender);
                    string transaction_0_status_for_sender_txn = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].status_for_sender_txn"), out transaction_0_status_for_sender_txn);
                    string transaction_0_receiver = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].receiver"), out transaction_0_receiver);
                    string transaction_0_invoiceId = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].invoiceId"), out transaction_0_invoiceId);
                    string transaction_0_amount = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].amount"), out transaction_0_amount);
                    string transaction_0_is_primary_receiver = string.Empty;
                    values.TryGetValue(Server.UrlEncode("transaction[0].is_primary_receiver"), out transaction_0_is_primary_receiver);

                    string return_url = string.Empty;
                    values.TryGetValue(Server.UrlEncode("return_url"), out return_url);
                    string cancel_url = string.Empty;
                    values.TryGetValue(Server.UrlEncode("cancel_url"), out cancel_url);
                    string ipn_notification_url = string.Empty;
                    values.TryGetValue(Server.UrlEncode("ipn_notification_url"), out ipn_notification_url);
                    string pay_key = string.Empty;
                    values.TryGetValue(Server.UrlEncode("pay_key"), out pay_key);
                    string memo = string.Empty;
                    values.TryGetValue(Server.UrlEncode("memo"), out memo);
                    string fees_payer = string.Empty;
                    values.TryGetValue(Server.UrlEncode("fees_payer"), out fees_payer);
                    string tracking_id = string.Empty;
                    values.TryGetValue(Server.UrlEncode("tracking_id"), out tracking_id);
                    string preapproval_key = string.Empty;
                    values.TryGetValue(Server.UrlEncode("preapproval_key"), out preapproval_key);
                    string reason_code = string.Empty;
                    values.TryGetValue(Server.UrlEncode("reason_code"), out reason_code);

                    #endregion

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Paypal IPN:");
                    foreach (KeyValuePair<string, string> kvp in values)
                    {
                        sb.AppendLine(kvp.Key + ": " + kvp.Value);
                    }

                    Guid orderNumberGuid = Guid.Empty;
                    Order order;
                    PaymentStatusEnum newPaymentStatus = PaypalAPHelper.GetPaymentStatus(status);
                    sb.AppendLine("New payment status: " + newPaymentStatus.GetPaymentStatusName());

                    switch (Server.UrlDecode(transaction_type))
                    {
                        case "Adaptive Payment PAY":
                            //should be the only case.
                            #region Adaptive Pay
                                try
                                {
                                    orderNumberGuid = new Guid(tracking_id);
                                }
                                catch
                                {
                                }

                                order = this.OrderService.GetOrderByGuid(orderNumberGuid);
                                if (order != null)
                                {
                                    this.OrderService.InsertOrderNote(order.OrderId, sb.ToString(), false, DateTime.UtcNow);

                                    switch (newPaymentStatus)
                                    {
                                        case PaymentStatusEnum.Pending:
                                            {
                                            }
                                            break;
                                        case PaymentStatusEnum.Authorized:
                                            {
                                                if (this.OrderService.CanMarkOrderAsAuthorized(order))
                                                {
                                                    this.OrderService.MarkAsAuthorized(order.OrderId);
                                                }
                                            }
                                            break;
                                        case PaymentStatusEnum.Paid:
                                            {
                                                if (this.OrderService.CanMarkOrderAsPaid(order))
                                                {
                                                    this.OrderService.MarkOrderAsPaid(order.OrderId);
                                                }
                                            }
                                            break;
                                        case PaymentStatusEnum.Refunded:
                                            {
                                                if (this.OrderService.CanRefundOffline(order))
                                                {
                                                    this.OrderService.RefundOffline(order.OrderId);
                                                }
                                            }
                                            break;
                                        case PaymentStatusEnum.Voided:
                                            {
                                                if (this.OrderService.CanVoidOffline(order))
                                                {
                                                    this.OrderService.VoidOffline(order.OrderId);
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    this.LogService.InsertLog(LogTypeEnum.OrderError, "PayPal IPN. Order is not found", new NopException(sb.ToString()));
                                }  
                            break;
                            #endregion
                        case "Adjustment":
                            //Do nothing for now.
                            //switch (Server.UrlDecode(reason_code).ToLowerInvariant())
                            //{           
                            //    case "chargeback settlement":
                            //        {
                            //            if (this.OrderService.CanRefundOffline(order))
                            //            {
                            //                this.OrderService.RefundOffline(order.OrderId);
                            //            }
                            //        }
                            //        break;
                            //    case "refund":
                            //        {
                            //            if (this.OrderService.CanRefundOffline(order))
                            //            {
                            //                this.OrderService.RefundOffline(order.OrderId);
                            //            }
                            //        }
                            //        break;
                            //    case "admin reversal":
                            //        {
                            //            if (this.OrderService.CanRefundOffline(order))
                            //            {
                            //                this.OrderService.RefundOffline(order.OrderId);
                            //            }
                            //        }
                            //        break;
                            //}
                            break;
                    }
                }
                else
                {
                    this.LogService.InsertLog(LogTypeEnum.OrderError, "PayPal IPN failed.", strRequest);
                }
            }
        }
        
        public override bool AllowGuestNavigation
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this page is tracked by 'Online Customers' module
        /// </summary>
        public override bool TrackedByOnlineCustomersModule
        {
            get
            {
                return false;
            }
        }
    }
}