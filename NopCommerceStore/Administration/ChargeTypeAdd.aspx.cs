using System;
using System.Web.UI;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails;

namespace NopSolutions.NopCommerce.Web.Administration
{
    public partial class ChargeTypeAdd : BaseNopAdministrationPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// Gets the charge type ID.
        /// </summary>
        /// <value>The charge type ID.</value>
        public int ChargeTypeID
        {
            get
            {
                return CommonHelper.QueryStringInt("ChargeTypeID");
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            ChargeType chargeType = this.ChargeTypeService.GetChargeType(ChargeTypeID);
            if (chargeType != null)
            {
                this.txtChargeName.Text = chargeType.Name;
                this.txtDescription.Text = chargeType.Description;
            }
            
        }

        /// <summary>
        /// Validates page security for current user
        /// </summary>
        /// <returns>true if action is allow; otherwise false</returns>
        protected override bool ValidatePageSecurity()
        {
            return this.ACLService.IsActionAllowed("ManageChargeTypes");
        }
        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    ChargeType chargeType = Save();
                    Response.Redirect(string.Format("ChargeTypes.aspx?ChargeTypeID={0}", chargeType.ChargeTypeID));
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }
        /// <summary>
        /// Handles the Click event of the SaveAndStayButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void SaveAndStayButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    ChargeType chargeType= Save();
                    Response.Redirect("ChargeTypeDetails.aspx?ChargeTypeID=" + chargeType.ChargeTypeID.ToString());
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>ChargeType.</returns>
        protected ChargeType Save()
        {
            ChargeType chargeType = new ChargeType();
            chargeType.CreatedOn = DateTime.UtcNow;
            chargeType.UpdatedOn = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                chargeType.Description = txtDescription.Text.Trim();
            else
                chargeType.Description = "";
            chargeType.IsActive = chkStatus.Checked;
            if (!string.IsNullOrEmpty(txtChargeName.Text))
            {
                chargeType.Name = txtChargeName.Text.Trim();
                chargeType = this.ChargeTypeService.AddChargeType(chargeType.Name, chargeType.Description, chargeType.IsActive, chargeType.CreatedOn, chargeType.UpdatedOn);
            }
            return chargeType;
        }
    }
}