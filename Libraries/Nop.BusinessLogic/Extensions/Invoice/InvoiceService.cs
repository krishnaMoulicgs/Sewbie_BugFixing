using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.Common.Utils;
using System.Data;
using System.Net.Mail;
using System.Globalization;

namespace NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement
{
    public class InvoiceService : IInvoiceService
    {
        #region Fields

        /// <summary>
        /// Object context
        /// </summary>
        private readonly NopObjectContext _context;
        /// <summary>
        /// Cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public InvoiceService(NopObjectContext context)
        {
            this._context = context;
            this._cacheManager = new NopRequestCache();
        }
        #endregion

        /// <summary>
        /// Gets all invoice.
        /// </summary>
        /// <returns></returns>
        public List<Invoice> GetAllInvoice()
        {
            var query = from c in _context.Invoices
                        select c;
            var invoices = query.ToList();
            return invoices;

            //return GetInvoiceBySearch(0,0,null,null,null,null);
        }

        public List<Invoice> GetInvoiceBySearch(int vendorID, int invoiceStatus, string invoiceNumber, DateTime? startDate, DateTime? invoiceDate, DateTime? endDate)
        {
            int invoiceID = 0;
            if (!string.IsNullOrEmpty(invoiceNumber))
                invoiceID = Convert.ToInt16(invoiceNumber);

            var query = from c in _context.Invoices
                        select c;
            if (vendorID>0)
                query = from c in query
                        where c.VendorID == vendorID
                          select c;
            if (invoiceStatus > 0)
                query = from c in query
                        where c.InvoiceStatus == invoiceStatus
                        select c;
            if (invoiceID > 0)
                query = from c in query
                        where c.InvoiceID == invoiceID
                        select c;
            if (startDate != null && startDate!=DateTime.MinValue)
                query = from c in query
                        where c.CreatedOn >= startDate
                        select c;
            if (endDate != null && endDate != DateTime.MinValue)
                query = from c in query
                        where c.CreatedOn <= endDate
                        select c;
            if (invoiceDate != null && invoiceDate != DateTime.MinValue)
                query = from c in query
                        where c.CreatedOn == invoiceDate
                        select c;
            var invoices = query.ToList();
            return invoices;
        }

        DataTable Search(int vendorID, int chargeTypeID, int invoiceStatus, string invoiceNumber, DateTime? startDate, DateTime? invoiceDate, DateTime? endDate)
        {
            IQueryable<Invoice> query = _context.Invoices;
          
            //if(SearchParameterIsValid(firstName)) {
            //    query = SearchByFirstName(query, firstName);
            //}
            //if(SearchParameterIsValid(middleName)) {
            //    query = SearchByMiddleName(query, middleName);
            //}
            //if(SearchParameterIsValid(lastName)) {
            //    query = SearchByLastName(query, lastName);
            //}
            //if(SearchParameterIsValid(ssn)) {
            //    query = SearchBySSN(query, ssn);
            //}
            //if(birthDate != null) {
            //    query = SearchByBirthDate(query, birthDate);
            //}

            // fill up and return DataTable from query
            return new DataTable();
        }

        bool SearchParameterIsValid(string s)
        {
            return !String.IsNullOrEmpty(s);
        }

        public void GenerateInvoice()
        {
            var emailAccount = IoC.Resolve<IMessageService>().GetEmailAccountById(1);
            if (emailAccount == null)
                throw new NopException("Email account could not be loaded");

            MailAddress from = new MailAddress(emailAccount.Email, emailAccount.DisplayName);
            MailAddress to = new MailAddress("muthukumar@constient.com");
            string subject = IoC.Resolve<ISettingManager>().StoreName + ". Testing email functionaly.";
            string body = "Email works fine from windows service.";
            Setting invoiceSettingStartDate =IoC.Resolve<SettingManager>().GetSettingByName("Invoice.StartDate");
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "yyyy-MM-dd";
            dtfi.DateSeparator = "-";
            DateTime invoiceStartDate = Convert.ToDateTime(invoiceSettingStartDate.Value, dtfi);
            Setting invoiceSettingEndDate = IoC.Resolve<SettingManager>().GetSettingByName("Invoice.EndDate");
            DateTime invoiceEndDate = Convert.ToDateTime(invoiceSettingEndDate.Value, dtfi);
            Setting invoiceSettingDate = IoC.Resolve<SettingManager>().GetSettingByName("Invoice.GenerateDate");
            int invoiceDay= Convert.ToInt16(invoiceSettingDate.Value);
            body = body + " Invoice start date " + invoiceStartDate + " Invoice end date " + invoiceEndDate + " Invoice date " + invoiceDay;
            IoC.Resolve<IMessageService>().SendEmail(subject, body, from, to, emailAccount);
        }

        public Invoice GetInvoice(int invoiceID)
        {
            if (invoiceID == 0)
                return null;

            var query = from c in _context.Invoices
                        where c.InvoiceID == invoiceID
                        select c;
            var invoice = query.SingleOrDefault();
            return invoice;
        }

        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException("invoice");

            invoice.UpdatedOn = DateTime.UtcNow;
            if (!_context.IsAttached(invoice))
                _context.Invoices.Attach(invoice);
            _context.SaveChanges();

        }
    }
}
