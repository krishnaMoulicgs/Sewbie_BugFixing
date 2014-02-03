using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic.ChargeManagement;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails;
using NopSolutions.NopCommerce.BusinessLogic.VendorManagement;
using NopSolutions.NopCommerce.BusinessLogic.InvoiceManagement;

namespace NopSolutions.NopCommerce.Web.Administration
{
    public partial class Charges : BaseNopAdministrationPage
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
                FillDropDowns();
                BindGrid();
            }
        }
        /// <summary>
        /// Fills the drop downs.
        /// </summary>
        protected void FillDropDowns()
        {
            this.ddlVendors.Items.Clear();
            ListItem itemEmptyVendor = new ListItem("All", "0");
            this.ddlVendors.Items.Add(itemEmptyVendor);
            var vendors = VendorService.GetAllVendors();
            foreach (Vendor vendor in vendors)
            {
                ListItem itemVendor = new ListItem(vendor.CompanyName, vendor.CustomerId.ToString());
                this.ddlVendors.Items.Add(itemVendor);
            }

            this.ddlChargeTypes.Items.Clear();
            ListItem itemEmptyChargeTypes = new ListItem("All", "0");
            this.ddlChargeTypes.Items.Add(itemEmptyChargeTypes);
            var chargeTypes = ChargeTypeService.GetAllChargeTypes();
            foreach (ChargeType chargeType in chargeTypes)
            {
                ListItem itemChargeType = new ListItem(chargeType.Name, chargeType.ChargeTypeID.ToString());
                this.ddlChargeTypes.Items.Add(itemChargeType);
            }
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            if (ddlVendors.SelectedIndex <= 0 && ddlChargeTypes.SelectedIndex <= 0)
            {
                var charges = this.ChargeService.GetAllCharges();
                gvCharges.DataSource = charges;
                gvCharges.DataBind();
            }
            else if (ddlVendors.SelectedIndex > 0 && ddlChargeTypes.SelectedIndex > 0)
            {
                int vendorID=0;
                int chargeTypeID=0;
                 if(ddlVendors.SelectedValue!=null)
                    vendorID=Convert.ToInt32(ddlVendors.SelectedValue);
                if (ddlChargeTypes.SelectedValue != null)
                chargeTypeID = Convert.ToInt32(ddlChargeTypes.SelectedValue);
                var chargesByVendorChargeType = this.ChargeService.GetChargesByVendorChargeType(vendorID, chargeTypeID);
                gvCharges.DataSource = chargesByVendorChargeType;
                gvCharges.DataBind();
            }
            else if (ddlVendors.SelectedIndex > 0 && ddlChargeTypes.SelectedIndex <= 0)
            {
                int vendorID = 0;
                if (ddlVendors.SelectedValue != null)
                    vendorID = Convert.ToInt32(ddlVendors.SelectedValue);
                var chargesByVendor = this.ChargeService.GetAllChargesByVendor(vendorID);
                gvCharges.DataSource = chargesByVendor;
                gvCharges.DataBind();
            }
            else if (ddlVendors.SelectedIndex <= 0 && ddlChargeTypes.SelectedIndex > 0)
            {
                int chargeTypeID = 0;
                if (ddlChargeTypes.SelectedValue != null)
                    chargeTypeID = Convert.ToInt32(ddlChargeTypes.SelectedValue);
                var chargesByChargeType = this.ChargeService.GetAllChargesByChargeType(chargeTypeID);
                gvCharges.DataSource = chargesByChargeType;
                gvCharges.DataBind();
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
        /// Handles the PageIndexChanging event of the gvCharges control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gvCharges_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCharges.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Gets the charges.
        /// </summary>
        /// <returns>List{ChargeType}.</returns>
        protected List<Charge> GetCharges()
        {
            var charges = this.ChargeService.GetAllCharges();
            return charges;
        }


        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    BindGrid();
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExportXLS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnExportXLS_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string fileName = string.Format("charge_{0}_{1}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
                    string filePath = string.Format("{0}files\\ExportImport\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);
                    var charges = GetCharges();

                    this.ExportManager.ExportChargeToXls(filePath, charges);
                    CommonHelper.WriteResponseXls(filePath, fileName);
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }
        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvCharges.Rows)
                {
                    var cbCharge = row.FindControl("cbCharge") as CheckBox;
                    var hfChargeID = row.FindControl("hfChargeID") as HiddenField;

                    bool isChecked = cbCharge.Checked;
                    int chargeID = int.Parse(hfChargeID.Value);
                    if (isChecked)
                    {
                        List<Invoice_Charges> invCharges = this.InvoiceChargesService.GetInvoiceChargesByID(chargeID);
                        if (invCharges != null && invCharges.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + "You cannot delete this record, related record exists" + "');", true);
                        }
                        else
                        {
                            this.ChargeService.DeleteCharge(chargeID);
                        }
                    }
                }

                BindGrid();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}