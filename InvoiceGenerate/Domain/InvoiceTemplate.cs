using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceGenerate.Domain
{
    public class InvoiceTemplate
    {

        private int _MessageTemplateID;
        private string _Name;
        private int _MessageTemplateLocalizedID;
        private int _LanguageID;
        private string _BCCEmailAddresses;
        private string _Subject;
        private string _Body;
        private bool _IsActive;
        private int _EmailAccountId;

        public int MessageTemplateID
        {
            get { return _MessageTemplateID; }
            set { _MessageTemplateID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int MessageTemplateLocalizedID
        {
            get { return _MessageTemplateLocalizedID; }
            set { _MessageTemplateLocalizedID = value; }
        }
        public int LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }


        public string BCCEmailAddresses
        {
            get { return _BCCEmailAddresses; }
            set { _BCCEmailAddresses = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public int EmailAccountId
        {
            get { return _EmailAccountId; }
            set { _EmailAccountId = value; }
        }

    }
}
