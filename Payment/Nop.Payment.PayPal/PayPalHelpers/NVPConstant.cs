using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NopSolutions.NopCommerce.Payment.Methods.PayPal

{
    
    public class NVPConstant
    {
        public static string requestEnvelopeerrorLanguage = "requestEnvelope.errorLanguage";

        //AdaptivePayments

        //CancelPreapproval
        public class CancelPreapproval
        {
            public static string preapprovalKey = "preapprovalKey";
        }
        public class SetConvertCurrency
        {
            public static string baseAmountListcurrencyamount_0 = "baseAmountList.currency(0).amount";
            public static string baseAmountListcurrencycode_0 = "baseAmountList.currency(0).code";
            public static string convertToCurrencyListcurrencyCode_0 = "convertToCurrencyList.currencyCode(0)";
            public static string convertToCurrencyListcurrencyCode_1 = "convertToCurrencyList.currencyCode(1)";
            public static string convertToCurrencyListcurrencyCode_2 = "convertToCurrencyList.currencyCode(2)";
        }
        public class ExecutePayment
        {
            public static string payKey = "payKey";
        }
        public class GetPaymentOptions
        {
            public static string payKey = "payKey";
        }
        public class Pay
        {
            public static string payKey = "payKey";
            public static string actionType = "actionType";
            public static string currencyCode = "currencyCode";
            public static string feesPayer = "feesPayer";
            public static string memo = "memo";
            public static string receiverListreceiveramount_0 = "receiverList.receiver[0].amount";
            public static string receiverListreceiveremail_0 = "receiverList.receiver[0].email";
            public static string receiverListreceiverprimary_0 = "receiverList.receiver[0].primary[0]";
            public static string receiverListreceiveramount_1 = "receiverList.receiver[1].amount";
            public static string receiverListreceiveremail_1 = "receiverList.receiver[1].email";
            public static string receiverListreceiverprimary = "receiverList.receiver[1].primary[1]";
            public static string senderEmail = "senderEmail";
            public static string cancelUrl = "cancelUrl";
            public static string returnUrl = "returnUrl";
            public static string ipnUrl = "ipnNotificationUrl";
            public static string preapprovalKey = "preapprovalKey";
            public static string trackingId = "trackingId";
        }
        public class PaymentDetails
        {
            public static string payKey = "payKey";
        }
        public class Preapproval
        {
            public static string currencyCode = "currencyCode";
            public static string startingDate = "startingDate";
            public static string endingDate = "endingDate";
            public static string maxNumberOfPayments = "maxNumberOfPayments";
            public static string maxTotalAmountOfAllPayments = "maxTotalAmountOfAllPayments";
            public static string requestEnvelopesenderEmail = "requestEnvelope.senderEmail";
            public static string cancelUrl = "cancelUrl";
            public static string returnUrl = "returnUrl";
        }
        public class PreapprovalDetail
        {
            public static string preapprovalKey = "preapprovalKey";
        }
        public class Refund
        {
            public static string payKey = "payKey";
            public static string currencyCode = "currencyCode";
            public static string receiverListreceiveremail_0 = "receiverList.receiver[0].email";
            public static string receiverListreceiveramount_0 = "receiverList.receiver[0].amount";
        }
        public class SetPaymentOption
        {
            public static string displayOptionsemailHeaderImageUrl = "displayOptions.emailHeaderImageUrl";
            public static string displayOptionsemailMarketingImageUrl = "displayOptions.emailMarketingImageUrl";
            public static string initiatingEntityinstitutionCustomercountryCode = "initiatingEntity.institutionCustomer.countryCode";
            public static string initiatingEntityinstitutionCustomerdisplayName = "initiatingEntity.institutionCustomer.displayName";
            public static string initiatingEntityinstitutionCustomeremail = "initiatingEntity.institutionCustomer.email";
            public static string initiatingEntityinstitutionCustomerfirstName = "initiatingEntity.institutionCustomer.firstName";
            public static string initiatingEntityinstitutionCustomerlastName = "initiatingEntity.institutionCustomer.lastName";
            public static string initiatingEntityinstitutionCustomerinstitutionCustomerId = "initiatingEntity.institutionCustomer.institutionCustomerId";
            public static string initiatingEntityinstitutionCustomerinstitutionId = "initiatingEntity.institutionCustomer.institutionId";
            public static string payKey = "payKey";
        }
        //AdaptiveAccounts
        public class AddBankAccount
        {
            public static string bankAccountNumber = "bankAccountNumber";
            public static string bankAccountType = "bankAccountType";
            public static string bankCountryCode = "bankCountryCode";
            public static string bankName = "bankName";
            public static string confirmationType = "confirmationType";
            public static string emailAddress = "emailAddress";
            public static string routingNumber = "routingNumber";
            public static string webOptionscancelUrl = "webOptions.cancelUrl";
            public static string webOptionscancelUrlDescription = "webOptions.cancelUrlDescription";
            public static string webOptionsreturnUrl = "webOptions.returnUrl";
            public static string webOptionsreturnUrlDescription = "webOptions.returnUrlDescription";
            public static string createAccountKey = "createAccountKey";

        }
        public class AddPaymentCard
        {
            public static string cardNumber = "cardNumber";
            public static string cardType = "cardType";
            public static string confirmationType = "confirmationType";
            public static string emailAddress = "emailAddress";
            public static string expirationDatemonth = "expirationDate.month";
            public static string expirationDateyear = "expirationDate.year";
            public static string billingAddressline1 = "billingAddress.line1";
            public static string billingAddressline2 = "billingAddress.line2";
            public static string billingAddresscity = "billingAddress.city";
            public static string billingAddressstate = "billingAddress.state";
            public static string billingAddresspostalCode = "billingAddress.postalCode";
            public static string billingAddresscountryCode = "billingAddress.countryCode";
            public static string nameOnCardfirstName = "nameOnCard.firstName";
            public static string nameOnCardlastName = "nameOnCard.lastName";
            public static string webOptionscancelUrl = "webOptions.cancelUrl";
            public static string webOptionscancelUrlDescription = "webOptions.cancelUrlDescription";
            public static string webOptionsreturnUrl = "webOptions.returnUrl";
            public static string webOptionsreturnUrlDescription = "webOptions.returnUrlDescription";
            public static string createAccountKey = "createAccountKey";
            public static string cardVerificationNumber = "cardVerificationNumber";
        }
        public class CreateAccount
        {
            public static string accountType="accountType";
            public static string addresscity="address.city";
            public static string addresscountryCode="address.countryCode";
            public static string addressline1="address.line1";
            public static string addressline2="address.line2";
            public static string addresspostalCode="address.postalCode";
            public static string addressstate="address.state";
            public static string citizenshipCountryCode="citizenshipCountryCode";
            public static string contactPhoneNumber="contactPhoneNumber";
            public static string currencyCode="currencyCode";
            public static string dateOfBirth="dateOfBirth";
            public static string namefirstName="name.firstName";
            public static string namelastName="name.lastName";
            public static string namemiddleName="name.middleName";
            public static string namesalutation="name.salutation";
            public static string notificationURL="notificationURL";
            public static string partnerField1="partnerField1";
            public static string partnerField2="partnerField2";
            public static string partnerField3="partnerField3";
            public static string partnerField4="partnerField4";
            public static string partnerField5="partnerField5";
            public static string preferredLanguageCode="preferredLanguageCode";
            public static string createAccountWebOptionsreturnUrl="createAccountWebOptions.returnUrl";
            public static string registrationType="registrationType";
            public static string sandboxEmailAddress="sandboxEmailAddress";
            public static string emailAddress="emailAddress";
            //For Business Account
            public static string businessInfoaverageMonthlyVolume="businessInfo.averageMonthlyVolume";
            public static string businessInfoaveragePrice = "businessInfo.averagePrice";
            public static string businessInfobusinessAddresscity = "businessInfo.businessAddress.city";
            public static string businessInfobusinessAddresscountryCode = "businessInfo.businessAddress.countryCode";
            public static string businessInfobusinessAddressline1 = "businessInfo.businessAddress.line1";
            public static string businessInfobusinessAddressline2 = "businessInfo.businessAddress.line2";
            public static string businessInfobusinessAddresspostalCode = "businessInfo.businessAddress.postalCode";
            public static string businessInfobusinessAddressstate = "businessInfo.businessAddress.state";
            public static string businessInfobusinessName = "businessInfo.businessName";
            public static string businessInfobusinessType = "businessInfo.businessType";
            public static string businessInfocustomerServiceEmail = "businessInfo.customerServiceEmail";
            public static string businessInfocustomerServicePhone = "businessInfo.customerServicePhone";
            public static string businessInfodateOfEstablishment = "businessInfo.dateOfEstablishment";
            public static string businessInfopercentageRevenueFromOnline = "businessInfo.percentageRevenueFromOnline";
            public static string businessInfosalesVenue = "businessInfo.salesVenue";
            public static string businessInfocategory = "businessInfo.category";
            public static string businessInfosubCategory = "businessInfo.subCategory";
            public static string businessInfowebSite = "businessInfo.webSite";
            public static string businessInfoworkPhone = "businessInfo.workPhone";
            
        }
        public class GetVerifiedStatus
        {
            public static string emailAddress="emailAddress";
            public static string matchCriteria="matchCriteria";
            public static string firstName="firstName";
            public static string lastName = "lastName";
            
        }
        public class SetFundingSource
        {
            public static string emailAddress="emailAddress";
            public static string fundingSourceKey = "fundingSourceKey";
        }

        //Permissions

        public class RequestPermissions
        {
            public static string callback = "callback";
            public static string scope = "scope";
 

        }
        public class GetAccessToken
        {
            public static string token = "token";
            public static string verifier = "verifier";
            public static string subjectAlias = "subjectAlias";
        }
        public class GetPermissions
        {
            public static string token = "token";            
        }
        public class CancelPermissions
        {
            public static string token = "token";            
        }
    }    
}

