using System;
using System.Collections.Generic;
using System.Text;
using InvoiceGenerate.DAL;
using System.Net.Mail;
using System.Net;
using System.Data;
using InvoiceGenerate.Domain;

namespace InvoiceGenerate.BAL
{
    class InvoiceBAL
    {
        public InvoiceBAL()
        {
        }
        InvoiceData InvoiceDataObj = new InvoiceData();
        InvoiceTemplate InvoiceTemplateObj = new InvoiceTemplate();
        int SewbieAdminID = 0;
        int InvoiceId = 0;
        decimal Amount = 0;

        /// <summary>
        /// Invoice reader.
        /// </summary>
        public void InvoiceReader()
        {           
            StringBuilder sb = new StringBuilder();
            InvoiceDataObj = InvoiceDAL.InvoiceDataList();

            if (InvoiceDataObj.GenerateDate.Day == DateTime.Today.Day)
            {
                List<DataRow> emailAccountList = InvoiceDAL.EmailAccountList();
                foreach (DataRow dr in emailAccountList)
                {
                    List<DataRow> vendorList = InvoiceDAL.VendorList();
                    SewbieAdminID = InvoiceDAL.SewbieAdminID();

                    foreach (DataRow drVendor in vendorList)
                    {
                        Amount = InvoiceDAL.InvoiceAmount(Convert.ToInt16(drVendor["VendorID"]), SewbieAdminID, InvoiceDataObj.StartDate, InvoiceDataObj.EndDate);
                        if (Amount > 0)
                        {

                            string strHeaderContent = "<html><body> <div style='color: #003399;'>Dear " + drVendor["CompanyName"].ToString() + ", <br /><br />"
                            + "Invoice <br /><br /><br /> "
                            + "</div>    </body></html> ";

                            sb.Append(strHeaderContent);
                            sb.Append("<table border='1' bordercolor='#2f2c63'>");
                            sb.Append("<tr>");
                            sb.Append("<th style='width: 200px; color: #000066'>Date </th><th style='width: 200px; color: #000066'> Due Date</th> <th style='width: 200px; color: #000066'> Amount </th> <th style='width: 200px; color: #000066'> Pay Now </th> ");
                            sb.Append("</tr>");



                            sb.Append("<tr>");
                            sb.Append("<td style='color: #000066; text-align:center '>" + InvoiceDataObj.GenerateDate.ToShortDateString() + "</td>");
                            sb.Append("<td style='width: 500px; color: #000066; text-align:center'>" + InvoiceDataObj.DueDate.ToShortDateString() + "</td>");
                            sb.Append("<td style='width: 200px; color: #000066; text-align:right'>" + Amount + "</td>");
                            sb.Append("<td style='width: 500px; color: #000066; text-align:center'>" + "<a href='http://www.sewbie.com/default.aspx'>Pay Now</a>" + "</td>");

                            sb.Append("</tr>");

                            sb.Append("</table>");
                            sb.Append("<br /><br /><br /><br /> *This is a system generated email, please do not reply to this message.");


                            SendMail(dr["Email"].ToString().Trim(), dr["DisplayName"].ToString().Trim(), drVendor["Email"].ToString().Trim(), "Invoice", sb.ToString(), Convert.ToBoolean(dr["UseDefaultCredentials"].ToString().Trim()), dr["Host"].ToString().Trim(), Convert.ToInt16(dr["Port"].ToString().Trim()), Convert.ToBoolean(dr["EnableSsl"].ToString().Trim()), dr["Username"].ToString().Trim(), dr["Password"].ToString().Trim());
                            InvoiceId = InvoiceDAL.AddInvoice(Convert.ToInt16(drVendor["VendorID"]), InvoiceDataObj.StartDate, InvoiceDataObj.EndDate, Amount);

                            List<DataRow> receivablesChargeList = InvoiceDAL.ReceivablesChargeList(Convert.ToInt16(drVendor["VendorID"]), SewbieAdminID, InvoiceDataObj.StartDate, InvoiceDataObj.EndDate);
                            foreach (DataRow drReceivablesCharge in receivablesChargeList)
                            {
                                InvoiceDAL.AddInvoiceCharges(InvoiceId, Convert.ToInt16(drReceivablesCharge["ChargeID"]), Convert.ToDecimal(drReceivablesCharge["Amount"]));
                            }

                            List<DataRow> payablesChargeList = InvoiceDAL.PayablesChargeList(Convert.ToInt16(drVendor["VendorID"]), SewbieAdminID, InvoiceDataObj.StartDate, InvoiceDataObj.EndDate);
                            foreach (DataRow drPayablesCharge in payablesChargeList)
                            {
                                InvoiceDAL.AddInvoiceCharges(InvoiceId, Convert.ToInt16(drPayablesCharge["ChargeID"]), -Convert.ToDecimal(drPayablesCharge["Amount"]));
                            }
                            sb = new StringBuilder();
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="FromAddress">From address.</param>
        /// <param name="DisplayName">The display name.</param>
        /// <param name="ToAddress">To address.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="Body">The body.</param>
        /// <param name="UseDefaultCredentials">if set to <c>true</c> [use default credentials].</param>
        /// <param name="Host">The host.</param>
        /// <param name="Port">The port.</param>
        /// <param name="EnableSsl">if set to <c>true</c> [enable SSL].</param>
        /// <param name="CredentialsMail">The credentials mail.</param>
        /// <param name="CredentialsPwd">The credentials PWD.</param>
        public void SendMail(string FromAddress, String DisplayName, String ToAddress, string Subject, string Body, bool UseDefaultCredentials, string Host, Int16 Port, bool EnableSsl, string CredentialsMail, string CredentialsPwd)
        {
            MailAddress fromAddress = new MailAddress(FromAddress, DisplayName);
            MailAddress toAddress = new MailAddress(ToAddress);
            var message = new MailMessage();
            message.From = fromAddress;
            message.To.Add(toAddress);

            message.Subject = Subject;
            message.IsBodyHtml = true;
           // string sbody=<p><a href="%Store.URL%">%Store.Name%</a> <br />   <br />   %ProductVariant.FullProductName% (ID: %ProductVariant.ID%) low quantity. <br />   <br />   Quantity: %ProductVariant.StockQuantity%<br /></p>
            message.Body = Body;

            var smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = UseDefaultCredentials;
            smtpClient.Host = Host;
            smtpClient.Port = Port;
            smtpClient.EnableSsl = EnableSsl;
            smtpClient.Credentials = new NetworkCredential(CredentialsMail, CredentialsPwd);
            smtpClient.Send(message);

        }


        public void InvoiceReaderMailSendTrowInvoiceTemplete()
        {

            StringBuilder sb = new StringBuilder();
            InvoiceTemplateObj = InvoiceDAL.InvoiceTemplateList();
            InvoiceDataObj = InvoiceDAL.InvoiceDataList();

            if (InvoiceDataObj.GenerateDate.Day == DateTime.Today.Day)
            {
            List<DataRow> emailAccountList = InvoiceDAL.EmailAccountList();
            foreach (DataRow dr in emailAccountList)
            {
                List<DataRow> vendorList = InvoiceDAL.VendorList();
                SewbieAdminID = InvoiceDAL.SewbieAdminID();

                foreach (DataRow drVendor in vendorList)
                {
                    Amount = InvoiceDAL.InvoiceAmount(Convert.ToInt16(drVendor["VendorID"]), SewbieAdminID, InvoiceDataObj.StartDate, InvoiceDataObj.EndDate);
                    if (Amount > 0)
                    {  
                    string body = "<b>Dear " + drVendor["CompanyName"].ToString() + ",<b><br><br>";
                    body = body+"<b>Invoice<b><br>";
                    body = body+InvoiceTemplateObj.Body;
                    body = body.Replace("%Date%", InvoiceDataObj.GenerateDate.ToShortDateString());
                    body = body.Replace("%Duedate%", InvoiceDataObj.DueDate.ToShortDateString());
                    body = body.Replace("%Amount%", Convert.ToString(Amount));
                    body = body.Replace("%PayNow%", "<a>PayNow</a>");
                    SendMail(dr["Email"].ToString().Trim(), dr["DisplayName"].ToString().Trim(), drVendor["Email"].ToString().Trim(), InvoiceTemplateObj.Subject, body, Convert.ToBoolean(dr["UseDefaultCredentials"].ToString().Trim()), dr["Host"].ToString().Trim(), Convert.ToInt16(dr["Port"].ToString().Trim()), Convert.ToBoolean(dr["EnableSsl"].ToString().Trim()), dr["Username"].ToString().Trim(), dr["Password"].ToString().Trim());
                    InvoiceId = InvoiceDAL.AddInvoice(Convert.ToInt16(drVendor["VendorID"]), InvoiceDataObj.StartDate, InvoiceDataObj.EndDate, Amount);

                    List<DataRow> receivablesChargeList = InvoiceDAL.ReceivablesChargeList(Convert.ToInt16(drVendor["VendorID"]), SewbieAdminID, InvoiceDataObj.StartDate, InvoiceDataObj.EndDate);
                    foreach (DataRow drReceivablesCharge in receivablesChargeList)
                    {
                        InvoiceDAL.AddInvoiceCharges(InvoiceId, Convert.ToInt16(drReceivablesCharge["ChargeID"]), Convert.ToDecimal(drReceivablesCharge["Amount"]));
                    }

                    List<DataRow> payablesChargeList = InvoiceDAL.PayablesChargeList(Convert.ToInt16(drVendor["VendorID"]), SewbieAdminID, InvoiceDataObj.StartDate, InvoiceDataObj.EndDate);
                    foreach (DataRow drPayablesCharge in payablesChargeList)
                    {
                        InvoiceDAL.AddInvoiceCharges(InvoiceId, Convert.ToInt16(drPayablesCharge["ChargeID"]), -Convert.ToDecimal(drPayablesCharge["Amount"]));
                    }
                    sb = new StringBuilder();
                    }

                }
                }
            }

        }
    }
}
