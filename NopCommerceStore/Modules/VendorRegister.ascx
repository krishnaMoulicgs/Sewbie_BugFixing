<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.VendorRegisterControl" CodeBehind="VendorRegister.ascx.cs" %>

<script type="text/javascript">

</script>
<div class="registration-page">
    <div class="page-title">
        <h1><%=GetLocaleResourceString("Account.SellerRegistrationTitle")%></h1>
    </div>
    <div class="clear">
    </div>
    <div class="body">
        <div class="message-error">
            <div>
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
            </div>
            <div class="clear">
            </div>
            <div>
            <div style="display:none">
                <asp:ValidationSummary ID="valSum" runat="server" ShowSummary="true" DisplayMode="BulletList"
                    ValidationGroup="CreateVendorForm" />
            </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="section-title">
            Account Information
        </div>
        <div class="clear">
        </div>
        <div class="section-body">
            <table class="table-container">
                <tbody>
                    <tr class="row" runat="server" id="pnlFirstName">
                        <td class="item-name">
                            Paypal First Name:
                        </td>
                        <td class="item-value">
                            <asp:TextBox ID="FirstName"  CssClass="onepageCheckOut_error" runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                            <br/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FirstName"
                                ErrorMessage="The First Name registered to your Paypal account is required" ToolTip="You registered Paypal First Name is required" Display="Dynamic"
                                ValidationGroup="CreateVendorForm"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr class="row" runat="server" id="pnlLastName">
                        <td class="item-name">
                            Paypal Last Name:
                        </td>
                        <td class="item-value">
                            <asp:TextBox ID="LastName"  CssClass="onepageCheckOut_error" runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                            <br/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LastName"
                                ErrorMessage="The Last Name registered to your Paypal account is required" ToolTip="You registered Paypal Last Name is required" Display="Dynamic"
                                ValidationGroup="CreateVendorForm"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="row" runat="server" id="pnlEmail">
                        <td class="item-name">
                            Paypal Email Address:
                        </td>
                        <td class="item-value">
                            <asp:TextBox ID="Email" CssClass="onepageCheckOut_error" runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                            <br/>
                            <asp:RequiredFieldValidator ID="PaypalEmailRequired" runat="server" ControlToValidate="Email"
                                ErrorMessage="Paypal Email Address is required" ToolTip="Paypal Email Address is required" Display="Dynamic"
                                ValidationGroup="CreateVendorForm"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator  CssClass="onepageCheckOut_error" runat="server" ID="revEmail" Display="Dynamic" ControlToValidate="Email"
                                ErrorMessage="Invalid email" ToolTip="Invalid email" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"
                                ValidationGroup="CreateVendorForm"></asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="PaypalVerificationValidator" runat="server" ControlToValidate="Email"  ValidationGroup="CreateVendorForm"
                                ErrorMessage="<p style='width: 200px;'>*Cannot find a valid Paypal account with the First Name, Last Name, and Email Address entered.  Please check these fields and try again.</p>"
                             CssClass="onepageCheckOut_error" ToolTip="Unable to verify Paypal account" OnServerValidate="ValidatePaypalAccount" Display="Dynamic" ValidateEmptyText="true" ></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="row" runat="server" id="pnlComanyName">
                        <td class="item-name">
                            Company Name
                        </td>
                        <td>
                            <asp:TextBox ID="CompanyName" CssClass="onepageCheckout_error" runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                            <asp:RequiredFieldValidator ID="CompanyNameRequired" runat="server" ControlToValidate="CompanyName"
                                ErrorMessage="Company Name is required" ToolTip="Company Name is required" Display="Dynamic"
                                ValidationGroup="CreateVendorForm"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="SellerExistCheck" runat="server" ControlToValidate="Email"  ValidationGroup="CreateVendorForm"
                                ErrorMessage="<p style='width: 200px;'>*There is already a seller that exists with this name.  Please enter another company name or contact customer support to complete your registration.</p>"
                             CssClass="onepageCheckOut_error" ToolTip="Seller already exists." OnServerValidate="ValidateSellerName" Display="Dynamic" ValidateEmptyText="true" ></asp:CustomValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="clear">
        </div>
        <div class="section-body">
            <p>
                <asp:Label runat="server" ID="lblCompleteStep"></asp:Label>
            </p>
            <asp:Button ID="ContinueButton" runat="server" CausesValidation="True" CommandName="Continue" OnClick="ContinueButton_Click"
                Text="<% $NopResources:Account.RegisterContinueButton %>" ValidationGroup="CreateVendorForm"
                CssClass="completeregistrationbutton" />
        </div>
        <div class="clear">
        </div>
        <input runat="server" id="hidPaypalVerified" type="hidden" value=""/>
    </div>
</div>
