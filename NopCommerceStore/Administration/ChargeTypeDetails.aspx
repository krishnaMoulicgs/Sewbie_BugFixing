<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/main.master" AutoEventWireup="true" CodeBehind="ChargeTypeDetails.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.Administration.ChargeTypeDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt=" <%=GetLocaleResourceString("Admin.ChargeTypeDetails.EditChargeType")%>" /><%=GetLocaleResourceString("Admin.ChargeTypeDetails.EditChargeType")%>
        <a href="ChargeTypes.aspx">
            Back To Charge Type list</a>
    </div>
    <div class="options">
        <asp:Button ID="SaveButton" runat="server" Text="<% $NopResources:Admin.ChargeTypeDetails.SaveButton.Text %>"
            CssClass="adminButtonBlue" OnClick="SaveButton_Click" ToolTip="<% $NopResources:Admin.ChargeTypeDetails.SaveButton.Tooltip %>" />
        <asp:Button ID="SaveAndStayButton" runat="server" CssClass="adminButtonBlue" OnClick="SaveAndStayButton_Click" Text="<% $NopResources:Admin.ChargeTypeDetails.SaveAndStayButton.Text %>"
            />
    </div>
</div>
<table width="100%">
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblName" Text="<% $NopResources:Admin.ChargeTypeDetails.lblName.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtChargeName" CssClass="adminInput" runat="server"></asp:TextBox>
        </td>
        <asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtChargeName" ErrorMessage="Name is required" Font-Name="verdana"
    Font-Size="9pt" runat="server" Display="None"></asp:RequiredFieldValidator>
    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
    HighlightCssClass="validatorCalloutHighlight" />
    </tr>
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblDescription" Text="<% $NopResources:Admin.ChargeTypeDetails.lblDescription.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtDescription" CssClass="adminInput" TextMode="MultiLine" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <asp:Label runat="server" ID="lblStatus" Text="<% $NopResources:Admin.ChargeTypeDetails.lblStatus.Text %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
           <asp:CheckBox ID="chkStatus" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>