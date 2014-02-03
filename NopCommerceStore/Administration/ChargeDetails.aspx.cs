using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails;
using NopSolutions.NopCommerce.BusinessLogic.ChargeManagement;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic;
namespace NopSolutions.NopCommerce.Web.Administration
{
    public partial class ChargeDetails : BaseNopAdministrationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillDropDowns();
                BindData();
            }
        }
        /// <summary>
        /// Gets the charge ID.
        /// </summary>
        /// <value>The charge ID.</value>
        public int ChargeID
        {
            get
            {
                return CommonHelper.QueryStringInt("ChargeID");
            }
        }


        /// <summary>
        /// Fills the drop downs.
        /// </summary>
        protected void FillDropDowns()
        {

            this.ddlVendors.Items.Clear();
            ListItem itemEmptyVendor = new ListItem(GetLocaleResourceString("Admin.Common.Unknown"), "0");
            this.ddlVendors.Items.Add(itemEmptyVendor);
            var vendors = VendorService.GetAllVendors();
            foreach (Vendor vendor in vendors)
            {
                ListItem itemVendor = new ListItem(vendor.CompanyName, vendor.CustomerId.ToString());
                this.ddlVendors.Items.Add(itemVendor);
            }

            this.ddlChargeTypes.Items.Clear();
            ListItem itemEmptyChargeTypes = new ListItem(GetLocaleResourceString("Admin.Common.Unknown"), "0");
            this.ddlChargeTypes.Items.Add(itemEmptyChargeTypes);
            var chargeTypes = ChargeTypeService.GetAllChargeTypes();
            foreach (ChargeType chargeType in chargeTypes)
            {
                ListItem itemChargeType = new ListItem(chargeType.Name, chargeType.ChargeTypeID.ToString());
                this.ddlChargeTypes.Items.Add(itemChargeType);
            }
        }
        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            Charge charge = this.ChargeService.GetCharge(ChargeID);
            if (charge != null)
            {
                if (charge.Amount != null)
                    this.txtChargeAmount.Text = charge.Amount.ToString();
                else
                    this.txtChargeAmount.Text = "0";
                this.txtRemarks.Text = charge.Remarks;
                this.chkIsInvoiceCharge.Checked = charge.IsInvoiceCharge;
                this.ddlChargeTypes.SelectedValue = charge.ChargeTypeID.ToString();
                this.ddlChargeTypes.Enabled = false;
                this.ddlVendors.SelectedValue = charge.CustomerID.ToString();
                this.ddlVendors.Enabled = false;
                this.ddlType.SelectedIndex = charge.Type - 1;
                this.ddlType.Enabled = false;
                this.ctrlDatePicker.SelectedDate = charge.EffectiveDate;
            }
        }
        /// <summary>
        /// Validates page security for current user
        /// </summary>
        /// <returns>true if action is allow; otherwise false</returns>
        protected override bool ValidatePageSecurity()
        {
            return this.ACLService.IsActionAllowed("ManageCharges");
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
                    if (ctrlDatePicker.SelectedDate != null)
                    {
                        if (Convert.ToDateTime(ctrlDatePicker.SelectedDate) < DateTime.Today)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + "Effective date accepts only current and future date" + "');", true);
                        }
                        else
                        {
                            Save();
                            Response.Redirect("ChargeDetails.aspx?ChargeID=" + ChargeID.ToString());
                        }
                    }
                    else
                    {
                        Save();
                        Response.Redirect("ChargeDetails.aspx?ChargeID=" + ChargeID.ToString());
                    }
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
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
                    if (ctrlDatePicker.SelectedDate != null)
                    {
                        if (Convert.ToDateTime(ctrlDatePicker.SelectedDate) < DateTime.Today)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + "Effective date accepts only current and future date" + "');", true);
                        }
                        else
                        {
                            Save();
                            Response.Redirect(string.Format("Charges.aspx?ChargeID={0}", ChargeID));
                        }
                    }
                    else
                    {
                        Save();
                        Response.Redirect(string.Format("Charges.aspx?ChargeID={0}", ChargeID));
                    }
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
        /// <returns>Charge</returns>
        protected void Save()
        {
            var charge = this.ChargeService.GetCharge(ChargeID);
            if (charge != null)
            {
                charge.CreatedOn = charge.CreatedOn;
                charge.UpdatedOn = DateTime.UtcNow;

                if (ddlVendors.SelectedValue != null)
                    charge.CustomerID = Convert.ToInt32(ddlVendors.SelectedValue);
                if (ddlChargeTypes.SelectedValue != null)
                    charge.ChargeTypeID = Convert.ToInt32(ddlChargeTypes.SelectedValue);
                if (!string.IsNullOrEmpty(txtRemarks.Text))
                    charge.Remarks = txtRemarks.Text.Trim();
                else
                    charge.Remarks = "";
                if (!string.IsNullOrEmpty(txtChargeAmount.Text))
                    charge.Amount = Convert.ToDecimal(txtChargeAmount.Text.Trim());
                else
                    charge.Amount = 0M;
                charge.IsInvoiceCharge = chkIsInvoiceCharge.Checked;
                charge.Type = ddlType.SelectedIndex + 1;
                if (charge.Type == 1)
                {
                    charge.PayFrom = NopContext.Current.User.CustomerId;
                    charge.PayTo = 0;
                }
                else
                {
                    charge.PayFrom = 0;
                    charge.PayTo = NopContext.Current.User.CustomerId;
                }
                charge.CreatedBy = charge.CreatedBy;
                charge.UpdatedBy = NopContext.Current.User.CustomerId;
                charge.OrderID = 0;
                if(ctrlDatePicker.SelectedDate!=null)
                charge.EffectiveDate = Convert.ToDateTime(ctrlDatePicker.SelectedDate);
                else
                    charge.EffectiveDate = Convert.ToDateTime(DateTime.UtcNow);
                this.ChargeService.UpdateCharge(charge);
            }
           
        }

    }
}