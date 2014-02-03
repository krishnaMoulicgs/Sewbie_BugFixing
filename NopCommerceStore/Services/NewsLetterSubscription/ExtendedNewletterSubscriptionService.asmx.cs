using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.Services.NewsLetterSubscription
{
    /// <summary>
    /// Summary description for ExtendedNewletterSubscriptionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ExtendedNewletterSubscriptionService : System.Web.Services.WebService
    {

        [WebMethod]
        public string subscribeEmail(string pEmail)
        {
            string strResult = string.Empty;
            
            try
            {
                var subscription = IoC.Resolve<IMessageService>().GetNewsLetterSubscriptionByEmail(pEmail);
                if (subscription != null)
                {
                    if (!subscription.Active)
                    {
                        IoC.Resolve<IMessageService>().SendNewsLetterSubscriptionActivationMessage(subscription.NewsLetterSubscriptionId, NopContext.Current.WorkingLanguage.LanguageId);
                    }
                    //lblResult.Text = GetLocaleResourceString("NewsLetterSubscriptionBox.SubscriptionCreated");
                    //strResult = GetLocaleResourceString("NewsLetterSubscriptionBox.SubscriptionCreated");
                }
                else
                    //if (rbSubscribe.Checked)
                    //{
                    subscription = new NopSolutions.NopCommerce.BusinessLogic.Messages.NewsLetterSubscription()
                    {
                        NewsLetterSubscriptionGuid = Guid.NewGuid(),
                        Email = pEmail,
                        Active = false,
                        CreatedOn = DateTime.UtcNow
                    };
                IoC.Resolve<IMessageService>().InsertNewsLetterSubscription(subscription);
                IoC.Resolve<IMessageService>().SendNewsLetterSubscriptionActivationMessage(subscription.NewsLetterSubscriptionId, NopContext.Current.WorkingLanguage.LanguageId);
                //lblResult.Text = GetLocaleResourceString("NewsLetterSubscriptionBox.SubscriptionCreated");
                //strResult =  GetLocaleResourceString("NewsLetterSubscriptionBox.SubscriptionCreated");
                //}
                //else
                //{
                //    //lblResult.Text = GetLocaleResourceString("NewsLetterSubscriptionBox.SubscriptionDeactivated");
                //}
            }
            catch (Exception ex)
            {
                //lblResult.Text = ex.Message;
                strResult = ex.Message;

            }

            //pnlResult.Visible = true;
            //pnlSubscribe.Visible = false;

            return strResult;
        }
        }
    }
