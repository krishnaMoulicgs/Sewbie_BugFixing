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
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Messages.SMS;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.Infrastructure;

namespace NopSolutions.NopCommerce.Web.VendorAdministration.Modules
{
    public partial class SMSProviderInfoControl : BaseNopAdministrationUserControl
    {
        private void BindData()
        {
            SMSProvider smsProvider = this.SMSService.GetSMSProviderBySystemKeyword(SMSProviderSystemKeyword);
            if (smsProvider != null)
            {
                txtName.Text = smsProvider.Name;
                txtClassName.Text = smsProvider.ClassName;
                txtSystemKeyword.Text = smsProvider.SystemKeyword;
                cbActive.Checked = smsProvider.IsActive;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        public SMSProvider SaveInfo()
        {
            SMSProvider smsProvider = this.SMSService.GetSMSProviderBySystemKeyword(SMSProviderSystemKeyword);

            if (smsProvider != null)
            {
                smsProvider.Name = txtName.Text;
                smsProvider.ClassName = txtClassName.Text;
                smsProvider.SystemKeyword = txtSystemKeyword.Text;
                smsProvider.IsActive = cbActive.Checked;
                this.SMSService.UpdateSMSProvider(smsProvider);
            }
            else
            {
                smsProvider = new SMSProvider()
                {
                    Name = txtName.Text,
                    ClassName = txtClassName.Text,
                    SystemKeyword = txtSystemKeyword.Text,
                    IsActive = cbActive.Checked
                };
                this.SMSService.InsertSMSProvider(smsProvider);
            }

            return smsProvider;
        }

        public string SMSProviderSystemKeyword
        {
            get
            {
                return Convert.ToString(ViewState["SMSProviderSystemKeyword"]);
            }
            set
            {
                ViewState["SMSProviderSystemKeyword"] = value;
            }
        }
    }
}