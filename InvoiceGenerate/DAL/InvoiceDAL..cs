using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using InvoiceGenerate.BAL;
using InvoiceGenerate.Domain;

namespace InvoiceGenerate.DAL
{
    public class InvoiceDAL
    {
        static string connString = ConfigurationManager.ConnectionStrings["NopSqlConnection"].ConnectionString;
       
        public InvoiceDAL()
        {
        }

        /// <summary>
        /// Emails the account list.
        /// </summary>
        /// <returns></returns>
        public static List<DataRow> EmailAccountList()
        {
           
            using (SqlConnection Con = new SqlConnection(connString))
            {
                using (SqlDataAdapter dAd = new SqlDataAdapter("Select * from Nop_EmailAccount where EmailAccountId=1", Con))
                {                    
                    DataTable dTable = new DataTable();
                    dAd.Fill(dTable);
                    return dTable.AsEnumerable().ToList();
                }
            }
        }

        /// <summary>
        /// Retruns vendors list.
        /// </summary>
        /// <returns></returns>
        public static List<DataRow> VendorList()
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = "SELECT Vendor.CustomerId as VendorID,Vendor.CompanyName, Nop_Customer.Email FROM Nop_Customer INNER JOIN Vendor ON Nop_Customer.CustomerID = Vendor.CustomerId where deleted=0";
                using (SqlDataAdapter dAd = new SqlDataAdapter(sqlStmt, Con))
                {
                    DataTable dTable = new DataTable();
                    dAd.Fill(dTable);
                    return dTable.AsEnumerable().ToList();
                }
            }
        }

        /// <summary>
        /// Returns Sewbie admin ID.
        /// </summary>
        /// <returns></returns>
        public static int SewbieAdminID()
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                Con.Open();
                string sqlStmt = "SELECT TOP(1) CustomerId as SewbieAdminID from Nop_Customer where ISAdmin=1 and deleted=0";
                SqlCommand cmd=new SqlCommand(sqlStmt,Con);
                object obj = cmd.ExecuteScalar();
                return (int)obj;               
            }
        }

        /// <summary>
        /// Gets Invoicedata.
        /// </summary>
        /// <returns></returns>
        public static InvoiceData InvoiceDataList()
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                InvoiceData InvoiceDataObj = new InvoiceData();
                string sqlStmt = "Select VALUE AS StartDate,(Select VALUE AS EndDate from Nop_Setting where name ='Invoice.EndDate')AS EndDate,(SELECT CAST( cast(YEAR(GETDATE()) AS VARCHAR)+'-'+ cast(MONTH(GETDATE())AS VARCHAR)+'-'+(Select VALUE AS GenerateDate from Nop_Setting where name ='Invoice.GenerateDate') AS DATE))AS GenerateDate from Nop_Setting where name ='Invoice.StartDate'";
                using (SqlDataAdapter dAd = new SqlDataAdapter(sqlStmt, Con))
                {
                    DataTable dTable = new DataTable();
                    dAd.Fill(dTable);
                    foreach (DataRow row in dTable.Rows)
                    {
                        InvoiceDataObj.StartDate = Convert.ToDateTime(row["StartDate"].ToString());
                        InvoiceDataObj.EndDate = Convert.ToDateTime(row["EndDate"].ToString());
                        InvoiceDataObj.GenerateDate = Convert.ToDateTime(row["GenerateDate"].ToString());
                        InvoiceDataObj.DueDate = InvoiceDataObj.GenerateDate.AddDays(15);
                    }

                    return InvoiceDataObj;
                }
            }
        }


        /// <summary>
        /// Gets the Invoice Amount.
        /// </summary>
        /// <param name="VendorID">The vendor ID.</param>
        /// <param name="SewbieAdminID">The sewbie admin ID.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <returns></returns>
        public static decimal InvoiceAmount(int VendorID, int SewbieAdminID, DateTime StartDate, DateTime EndDate)
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = "SELECT Receivables-Payables AS Amount FROM ("
                    + "SELECT isnull(SUM(convert(decimal(18,2),isnull(Amount,0))),0) as Receivables,(select isnull(SUM(convert(decimal(18,2),isnull(Amount,0))),0) AS Payables FROM Charge where PayFrom=" + SewbieAdminID + " and PayTo=" + VendorID + " and "
                    + "cast(effectivedate AS date) >='" + StartDate + "' and cast(effectivedate AS date)<='" + EndDate + "' and isinvoicecharge=1) as Payables from Charge where PayFrom=" + VendorID + " and PayTo=" + SewbieAdminID + " and "
                    + "cast(effectivedate AS date) >='" + StartDate + "' and cast(effectivedate AS date)<='" + EndDate + "' and isinvoicecharge=1"
                    + ")AS Temp";
                Con.Open();
                SqlCommand cmd = new SqlCommand(sqlStmt, Con);
                object obj = cmd.ExecuteScalar();
                return (decimal)obj;
            }
        }

        /// <summary>
        /// Get the receivables.
        /// </summary>
        /// <param name="VendorID">The vendor ID.</param>
        /// <param name="SewbieAdminID">The sewbie admin ID.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <returns></returns>
        public static List<DataRow> ReceivablesChargeList(int VendorID, int SewbieAdminID, DateTime StartDate, DateTime EndDate)
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = " SELECT   * FROM Charge "
                 + "WHERE (PayFrom = " + VendorID + ") AND (PayTo = " + SewbieAdminID + ") AND (CAST(EffectiveDate AS date) >= '" + StartDate + "') AND (CAST(EffectiveDate AS date) <= '" + EndDate + "') AND (IsInvoiceCharge = 1)";
                   
                Con.Open();
                using (SqlDataAdapter dAd = new SqlDataAdapter(sqlStmt, Con))
                {
                    DataTable dTable = new DataTable();
                    dAd.Fill(dTable);
                    return dTable.AsEnumerable().ToList();
                }

            }
        }

        /// <summary>
        /// Get the payables.
        /// </summary>
        /// <param name="VendorID">The vendor ID.</param>
        /// <param name="SewbieAdminID">The sewbie admin ID.</param>
        /// <param name="StartDate">The start date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <returns></returns>
        public static List<DataRow> PayablesChargeList(int VendorID, int SewbieAdminID, DateTime StartDate, DateTime EndDate)
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = " SELECT   * FROM Charge "
             + "WHERE (PayFrom = " + SewbieAdminID + ") AND (PayTo = " + VendorID + ") AND (CAST(EffectiveDate AS date) >= '" + StartDate + "') AND (CAST(EffectiveDate AS date) <= '" + EndDate + "') AND (IsInvoiceCharge = 1)";

                Con.Open();
                using (SqlDataAdapter dAd = new SqlDataAdapter(sqlStmt, Con))
                {
                    DataTable dTable = new DataTable();
                    dAd.Fill(dTable);
                    return dTable.AsEnumerable().ToList();
                }

            }
        }

        /// <summary>
        /// Adds the invoice.
        /// </summary>
        /// <param name="VendorID">The vendor ID.</param>
        /// <param name="InvoiceFromDate">The invoice from date.</param>
        /// <param name="InvoiceToDate">The invoice to date.</param>
        /// <param name="Amount">The amount.</param>
        /// <returns></returns>
        public static int AddInvoice(int VendorID, DateTime InvoiceFromDate, DateTime InvoiceToDate, decimal Amount)
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = "INSERT INTO [Invoice]([VendorID],[InvoiceFromDate],[InvoiceToDate],[Amount],[InvoiceStatus],[CreatedOn],[UpdatedOn]) " +
                    "VALUES (@VendorID,@InvoiceFromDate,@InvoiceToDate,@Amount,@InvoiceStatus,@CreatedOn,@UpdatedOn)";
                Con.Open();
                using (SqlCommand dCmd = new SqlCommand(sqlStmt, Con))
                {
                    dCmd.Parameters.AddWithValue("@VendorID", VendorID);
                    dCmd.Parameters.AddWithValue("@InvoiceFromDate", InvoiceFromDate);
                    dCmd.Parameters.AddWithValue("@InvoiceToDate", InvoiceToDate);
                    dCmd.Parameters.AddWithValue("@Amount", Amount);
                    dCmd.Parameters.AddWithValue("@InvoiceStatus", 1);
                    dCmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    dCmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                    dCmd.ExecuteNonQuery();

                    string sqlStmtInc = "SELECT @@IDENTITY";
                    SqlCommand cmd = new SqlCommand(sqlStmtInc, Con);
                    object obj = cmd.ExecuteScalar();
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// Adds the invoicecharges.
        /// </summary>
        /// <param name="InvoiceID">The invoice ID.</param>
        /// <param name="ChargeID">The charge ID.</param>
        /// <param name="Amount">The amount.</param>
        /// <returns></returns>
        public static int AddInvoiceCharges(int InvoiceID, int ChargeID, decimal Amount)
        {
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = "INSERT INTO [Invoice_Charges]([InvoiceID],[ChargeID],[Amount]) " +
                    "VALUES (@InvoiceID,@ChargeID,@Amount)";
                Con.Open();
                using (SqlCommand dCmd = new SqlCommand(sqlStmt, Con))
                {
                    dCmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    dCmd.Parameters.AddWithValue("@ChargeID", ChargeID);
                    dCmd.Parameters.AddWithValue("@Amount", Amount);
                    return dCmd.ExecuteNonQuery();                   
                }
            }
        }

        public static InvoiceTemplate InvoiceTemplateList()
        {
            InvoiceTemplate InvoiceTemplateObj = new InvoiceTemplate();
            using (SqlConnection Con = new SqlConnection(connString))
            {
                string sqlStmt = "SELECT TOP (1)*  FROM Nop_MessageTemplate INNER JOIN "
                        + "Nop_MessageTemplateLocalized ON Nop_MessageTemplate.MessageTemplateID = Nop_MessageTemplateLocalized.MessageTemplateID "
                        + "WHERE Nop_MessageTemplate.Name = 'Vendor.InvoiceMail'";
                using (SqlDataAdapter dAd = new SqlDataAdapter(sqlStmt, Con))
                {
                    DataTable dTable = new DataTable();
                    dAd.Fill(dTable);
                    foreach (DataRow row in dTable.Rows)
                    {
                        InvoiceTemplateObj.BCCEmailAddresses = row["BCCEmailAddresses"].ToString();
                        InvoiceTemplateObj.Body = row["Body"].ToString();
                        InvoiceTemplateObj.EmailAccountId = Convert.ToInt16(row["EmailAccountId"].ToString());
                        InvoiceTemplateObj.IsActive = Convert.ToBoolean(row["IsActive"].ToString());
                        InvoiceTemplateObj.LanguageID = Convert.ToInt16(row["LanguageID"].ToString());
                        InvoiceTemplateObj.MessageTemplateID = Convert.ToInt16(row["MessageTemplateID"].ToString());
                        InvoiceTemplateObj.MessageTemplateLocalizedID = Convert.ToInt16(row["MessageTemplateLocalizedID"].ToString());
                        InvoiceTemplateObj.Name = row["Name"].ToString();
                        InvoiceTemplateObj.Subject = row["Subject"].ToString();
                    }
                    return InvoiceTemplateObj;
                }
            }
        }
    }

}
