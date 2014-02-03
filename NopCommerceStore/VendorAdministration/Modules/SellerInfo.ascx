<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SellerInfo.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.SellerInfo" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>

<script type="text/javascript">
    var txtFirstName_id = "<%= txtFirstName.ClientID %>";
    var txtLastName_id = "<%= txtLastName.ClientID %>";
    var txtPaypalEmailAddress_id = "<%= txtPaypalEmailAddress.ClientID %>";

    var lblFirstName_id = "<%= lblFirstName.ClientID %>";
    var lblLastName_id = "<%= lblFirstName.ClientID %>";
    var lblPaypalEmailAddress_id = "<%= lblPaypalEmailAddress.ClientID %>";

    var lblUnverified_id = "<%= lblUnverified.ClientID %>";
    var lblSuccess_id = "<%=lblSuccess.ClientID %>";

    var hidPaypalVerified_id = "<%= hidPaypalVerified.ClientID %>";


    $(document).ready(function () {
        $('#' + txtFirstName_id).bind({ 'change': VerifyFields_onChange, 'blur': VerifyFields_onChange, 'keyup': VerifyFields_onChange });
        $('#' + txtLastName_id).bind({ 'change': VerifyFields_onChange, 'blur': VerifyFields_onChange, 'keyup': VerifyFields_onChange });
        $('#' + txtPaypalEmailAddress_id).bind({ 'change': VerifyFields_onChange, 'blur': VerifyFields_onChange, 'keyup': VerifyFields_onChange });
    });
</script>

<div>
    <table>
        <tr>
            <td class="adminTitle">
                <nopCommerce:ToolTipLabel runat="server" ID="lblFirstName" Text="First Name"
                    ToolTip="First Name as registered on Paypal." ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
            </td>
            <td class="adminData">
                <asp:TextBox runat="server" ID="txtFirstName" CssClass="adminInput"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                <nopCommerce:ToolTipLabel runat="server" ID="lblLastName" Text="Last Name"
                    ToolTip="Last Name as registered on Paypal" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
            </td>
            <td class="adminData">
                <asp:TextBox runat="server" ID="txtLastName" CssClass="adminInput"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                <nopCommerce:ToolTipLabel runat="server" ID="lblPaypalEmailAddress" Text="Paypal Email"
                    ToolTip="This is the Paypal email" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
            </td>
            <td class="adminData">
                <asp:TextBox runat="server" ID="txtPaypalEmailAddress" CssClass="adminInput"></asp:TextBox>
            </td>
            <td class="adminData">
                <input id="btnVerifyPaypalEmail" runat="server" class="adminButton" onclick="btnVerifyPaypalEmail_ClientClick()" type="button" value="Verify" />
            </td>
            <td class="adminData">
                <span id="lblInvalid" style="display:none; color:Red;">Please enter a properly formatted First Name, Last Name, and Email Address.</span>
                <span id="lblError" runat="server" style="display:none; color:Red;">There was an error contacting Paypal.  Please try again later.</span>
                <span id="lblFailure" style="display:none; color:Red;">Your Paypal account was not found.  Please ensure your email address and registered name are correct.</span>
                <span id="lblSuccess" runat="server" style="display:none; color:Green;">Verified</span>
                <span id="lblUnverified" runat="server" style="">Unverified</span>
                <span id="lblVerifying" style="display:none;">Verifying ... Please Wait ...</span>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hidPaypalVerified" value="false"/>
</div>
