<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.CustomerInfoControl"
    CodeBehind="CustomerInfo.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="EmailTextBox" Src="EmailTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DatePicker" Src="DatePicker.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
<table class="adminContent">
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblCustomerEmailTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.Email %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Email.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:EmailTextBox runat="server" ID="txtEmail" CssClass="adminInput" />
        </td>
    </tr>    
    <tr runat="server" id="pnlUsername">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblCustomerUsernameTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.Username %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Username.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:SimpleTextBox runat="server" ID="txtUsername" CssClass="adminInput"
                ErrorMessage="<% $NopResources:Admin.CustomerInfo.Username.ErrorMessage %>">
            </nopCommerce:SimpleTextBox>
            <asp:Label runat="server" ID="lblUsername" />
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblPasswordTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.Password %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Password.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox runat="server" ID="txtPassword" CssClass="adminInput" TextMode="Password" />
            <asp:Button runat="server" ID="btnChangePassword" CssClass="adminButtonBlue" Text="<% $NopResources:Admin.CustomerInfo.BtnChangePassword.Text %>"
                OnClick="BtnChangePassword_OnClick" />
        </td>
    </tr>
    <% if (this.CustomerService.FormFieldGenderEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblGenderTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.Gender %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Gender.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:RadioButton runat="server" ID="rbGenderM" GroupName="Gender" Text="<% $NopResources:VendorAdmin.CustomerInfo.Gender.Male %>"
                Checked="true" />
            <asp:RadioButton runat="server" ID="rbGenderF" GroupName="Gender" Text="<% $NopResources:VendorAdmin.CustomerInfo.Gender.Female %>" />
        </td>
    </tr>
    <% } %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblFirstNameTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.FirstName %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.FirstName.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox runat="server" ID="txtFirstName" CssClass="adminInput" />
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblLastNameTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.LastName %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.LastName.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox runat="server" ID="txtLastName" CssClass="adminInput" />
        </td>
    </tr>
    <% if (this.CustomerService.FormFieldDateOfBirthEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblDateOfBirthTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.DateOfBirth %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.DateOfBirth.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:DatePicker runat="server" ID="ctrlDateOfBirthDatePicker" />
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldCompanyEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblCompanyTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.Company %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Company.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtCompany" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>    
    <% if (this.TaxService.EUVatEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblVatNumberTitle" Text="<% $NopResources:VendorAdmin.CustomerInfo.VatNumber %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.VatNumber.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtVatNumber" runat="server" CssClass="adminInput" />&nbsp;&nbsp;&nbsp;<asp:Label
                ID="lblVatNumberStatus" runat="server" />
            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnMarkVatNumberAsValid" CssClass="adminButton"
                CausesValidation="false" Text="<% $NopResources:Admin.CustomerInfo.BtnMarkVatNumberAsValid.Text %>"
                OnClick="BtnMarkVatNumberAsValid_OnClick" />&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnMarkVatNumberAsInvalid" CssClass="adminButton"
                CausesValidation="false" Text="<% $NopResources:Admin.CustomerInfo.BtnMarkVatNumberAsInvalid.Text %>"
                OnClick="BtnMarkVatNumberAsInvalid_OnClick" />
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldStreetAddressEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblStreetAddressTitle" Text="<% $NopResources:Admin.CustomerInfo.Address %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Address.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtStreetAddress" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldStreetAddress2Enabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblStreetAddress2Title" Text="<% $NopResources:Admin.CustomerInfo.Address2 %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Address2.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtStreetAddress2" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldPostCodeEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblZipPostalCodeTitle" Text="<% $NopResources:Admin.CustomerInfo.Zip %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Zip.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtZipPostalCode" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldCityEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblCityTitle" Text="<% $NopResources:Admin.CustomerInfo.City %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.City.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtCity" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldCountryEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblCountryTitle" Text="<% $NopResources:Admin.CustomerInfo.Country %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Country.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlCountry" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldCountryEnabled && this.CustomerService.FormFieldStateEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblStateProvinceTitle" Text="<% $NopResources:Admin.CustomerInfo.State %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.State.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlStateProvince" AutoPostBack="False" runat="server" Width="137px">
            </asp:DropDownList>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldPhoneEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTelephoneNumberTitle" Text="<% $NopResources:Admin.CustomerInfo.Phone %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Phone.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldFaxEnabled)
       { %>
    <tr runat="server" id="pnlFaxNumber">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblFaxNumberTitle" Text="<% $NopResources:Admin.CustomerInfo.Fax %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Fax.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <% } %>
    <% if (this.CustomerService.FormFieldNewsletterEnabled)
       { %>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblNewsletterTitle" Text="<% $NopResources:Admin.CustomerInfo.Newsletter %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Newsletter.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbNewsletter" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <% } %>
    <tr runat="server" id="pnlTimeZone">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTimeZoneTitle" Text="<% $NopResources:Admin.CustomerInfo.TimeZone %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.TimeZone.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlTimeZone" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblAffiliateTitle" Text="<% $NopResources:Admin.CustomerInfo.Affiliate %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Affiliate.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlAffiliate" AutoPostBack="False" CssClass="adminInput" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblIsTaxExempt" Text="<% $NopResources:Admin.CustomerInfo.TaxExempt %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.TaxExempt.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbIsTaxExempt" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblIsAdminTitle" Text="<% $NopResources:Admin.CustomerInfo.Admin %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Admin.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbIsAdmin" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblIsForumModerator" Text="<% $NopResources:Admin.CustomerInfo.ForumModerator %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.ForumModerator.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbIsForumModerator" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblAdminComment" Text="<% $NopResources:Admin.CustomerInfo.AdminComment %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.AdminComment.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtAdminComment" runat="server" TextMode="MultiLine" CssClass="adminInput"
                Height="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblActiveTitle" Text="<% $NopResources:Admin.CustomerInfo.Active %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.Active.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbActive" runat="server" Checked="true"></asp:CheckBox>
        </td>
    </tr>
    <tr runat="server" id="pnlRegistrationDate">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblRegistrationDateTitle" Text="<% $NopResources:Admin.CustomerInfo.RegistrationDate %>"
                ToolTip="<% $NopResources:Admin.CustomerInfo.RegistrationDate.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:Label ID="lblRegistrationDate" runat="server"></asp:Label>
        </td>
    </tr>
</table>
