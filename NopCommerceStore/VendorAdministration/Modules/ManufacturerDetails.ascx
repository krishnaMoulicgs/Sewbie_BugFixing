<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ManufacturerDetailsControl"
    CodeBehind="ManufacturerDetails.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerInfo" Src="ManufacturerInfo.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerSEO" Src="ManufacturerSEO.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ConfirmationBox" Src="ConfirmationBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SellerInfo" Src="SellerInfo.ascx" %>

<script type="text/javascript">
    var lblSuccess_id = "<%= GetLblSuccessClientID() %>";
    var btnSave_id = "<%=SaveButton.ClientID %>";
    var SaveAndStayButton_id = "<%=SaveAndStayButton.ClientID %>";

    var pnlMessage_id = "<%= GetpnlMessageClientID() %>";
    var lblMessage_id = "<%= GetlMessageClientID() %>";
    var lblCompleteMessage_id = "<%= GetlMessageCompleteClientID() %>";

    $(document).ready(function () {
        $('#' + btnSave_id).bind({'click': btnSave_Click});
        $('#' + SaveAndStayButton_id).bind({ 'click': btnSave_Click });
    });

    function btnSave_Click() {
        if ($('#' + lblSuccess_id).css('display') == 'none') {
            //unverified.
            $('#' + pnlMessage_id).show();
            $('#' + pnlMessage_id).addClass('messageBoxError');

            //messageBox messageBoxError
            $('#' + lblMessage_id).val('Your Paypal Account Information is not verified.  Please verify your Paypal Account Information before saving.');
            //$('#' + lblMessageComplete_id).val('');

            //do not allow save to continue.
            return false;
        } else {
            //verified.
            //allow save to continue
        }
    }
</script>

<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="<%=GetLocaleResourceString("VendorAdmin.ManufacturerDetails.Title")%>" />
        <%=GetLocaleResourceString("VendorAdmin.ManufacturerDetails.Title")%> - <asp:Label runat="server" ID="lblTitle" />
    </div>
    <div class="options">
        <asp:Button ID="PreviewButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ManufacturerDetails.PreviewButton.Text %>"
            ToolTip="<% $NopResources:VendorAdmin.ManufacturerDetails.PreviewButton.ToolTip %>" />
        <asp:Button ID="SaveButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ManufacturerDetails.SaveButton.Text %>"
            OnClick="SaveButton_Click" ToolTip="<% $NopResources:VendorAdmin.ManufacturerDetails.SaveButton.Tooltip %>" />
        <asp:Button ID="SaveAndStayButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ManufacturerDetails.SaveAndStayButton.Text %>"
            OnClick="SaveAndStayButton_Click" />
    </div>
</div>
<div>
    <nopCommerce:SellerInfo runat="server" ID="ctrlSellerInfo"/>
    <nopCommerce:ManufacturerInfo ID="ctrlManufacturerInfo" runat="server" />
    <nopCommerce:ManufacturerSEO ID="ctrlManufacturerSEO" runat="server"/>
</div>

