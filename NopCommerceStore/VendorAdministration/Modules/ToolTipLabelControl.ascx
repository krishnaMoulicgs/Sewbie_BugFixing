<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolTipLabelControl.ascx.cs"Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ToolTipLabelControl" %>
<span class="nop-tooltip">
    <asp:Image runat="server" ID="imgToolTip" AlternateText="?" />
    <asp:Label runat="server" ID="lblValue" />
</span>