<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/main.master" AutoEventWireup="true" CodeBehind="ChargeDetails.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.Administration.ChargeDetails" %>
<%@ Register TagPrefix="nopCommerce" TagName="DecimalTextBox" Src="Modules/DecimalTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DatePicker" Src="Modules/DatePicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">

<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="Add New Charge" />
        Add New Charge
        <a href="Charges.aspx">
            Back To Charges list</a>
    </div>
    <div class="options">
        <asp:Button ID="SaveButton" OnClick="SaveButton_Click" runat="server" Text="<% $NopResources:Admin.ChargeDetails.SaveButton.Text %>"
            CssClass="adminButtonBlue" ToolTip="<% $NopResources:Admin.ChargeDetails.SaveButton.Tooltip %>" />
        <asp:Button ID="SaveAndStayButton" OnClick="SaveAndStayButton_Click" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:Admin.ChargeDetails.SaveAndStayButton.Text %>"
            />
    </div>
</div>
<table width="100%">
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblVendor" Text="<% $NopResources:Admin.ChargeDetails.lblVendor.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlVendors" CssClass="adminInput" runat="server">
            </asp:DropDownList>
        </td>
         <td class="adminTitle">
            <asp:Label runat="server" ID="lblChargeTypes" Text="<% $NopResources:Admin.ChargeDetails.lblChargeTypes.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlChargeTypes" CssClass="adminInput" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblAmount" Text="<% $NopResources:Admin.ChargeDetails.lblAmount.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
          <td class="adminData">
                        <asp:TextBox ID="txtChargeAmount" runat="server" />
        </td>
        <asp:RegularExpressionValidator ID="rvDecimal" ControlToValidate="txtChargeAmount" runat="server"
ErrorMessage="Please enter a valid amount.Accepts Integer or decimal with two digits." ValidationExpression="^(-)?\d+(\.\d\d)?$">
</asp:RegularExpressionValidator>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblDate" Text="<% $NopResources:Admin.ChargeDetails.lblDate.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
       <td class="adminData">
                        <nopCommerce:DatePicker runat="server" ID="ctrlDatePicker" />
                    </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblRemarks" Text="<% $NopResources:Admin.ChargeDetails.lblRemarks.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtRemarks" CssClass="adminInput" TextMode="MultiLine" runat="server"></asp:TextBox>
        </td>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblChargeType" Text="<% $NopResources:Admin.ChargeDetails.lblChargeType.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="adminInput">
            <asp:ListItem Text="Payment" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Charge"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblIsInvoiceCharge" Text="<% $NopResources:Admin.ChargeDetails.lblIsInvoiceCharge.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
           <asp:CheckBox ID="chkIsInvoiceCharge" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
