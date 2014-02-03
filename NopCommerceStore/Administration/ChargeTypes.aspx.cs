using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.BusinessLogic.ChargeTypeDetails;
using NopSolutions.NopCommerce.BusinessLogic.ChargeManagement;

namespace NopSolutions.NopCommerce.Web.Administration
{
    public partial class ChargeTypes : BaseNopAdministrationPage
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
                BindGrid();
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
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            var charges = this.ChargeTypeService.GetAllChargeTypes();
            gvCharges.DataSource = charges;
            gvCharges.DataBind();
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
        protected List<ChargeType> GetCharges()
        {
            var charges = this.ChargeTypeService.GetAllChargeTypes();
            return charges;
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
                    string fileName = string.Format("chargetype_{0}_{1}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
                    string filePath = string.Format("{0}files\\ExportImport\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);
                    var charges = GetCharges();

                    this.ExportManager.ExportChargeTypeToXls(filePath, charges);
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
                    var hfChargeTypeID = row.FindControl("hfChargeTypeID") as HiddenField;

                    bool isChecked = cbCharge.Checked;
                    int chargeTypeID = int.Parse(hfChargeTypeID.Value);
                    if (isChecked)
                    {
                        List<Charge> charges = this.ChargeService.GetAllChargesByChargeType(chargeTypeID);
                        if (charges != null && charges.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + "You cannot delete this record, related record exists" + "');", true);
                        }
                        else
                        {
                            this.ChargeTypeService.DeleteChargeType(chargeTypeID);
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