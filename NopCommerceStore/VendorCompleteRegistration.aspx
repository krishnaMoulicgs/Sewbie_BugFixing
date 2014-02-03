<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SimpleColumn.master" AutoEventWireup="true" CodeBehind="VendorCompleteRegistration.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorRegistration" %>
<%@ Register TagPrefix="nopCommerce" TagName="VendorRegister" Src="~/Modules/VendorRegister.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="Captcha" Src="~/Modules/Captcha.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="Topic" Src="~/Modules/Topic.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/jquery.ui.draggable.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/jquery.alerts.js"></script>

<script type="text/javascript">
    $(document).ready(
    function () {
        $("#ctl00_ctl00_cph1_cph1_ctrlCustomerRegister_CreateUserForm_CreateUserStepContainer_UserName").blur(function () {
            var EmailAddress = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlCustomerRegister_CreateUserForm_CreateUserStepContainer_UserName");
            var a = $("#ctl00_ctl00_cph1_cph1_ctrlCustomerRegister_CreateUserForm_CreateUserStepContainer_UserNameOrEmailRequired").css("display");
            var b = $("#ctl00_ctl00_cph1_cph1_ctrlCustomerRegister_CreateUserForm_CreateUserStepContainer_refUserNameOrEmail").css("display");
            if (a == "none" && b == "none") {
                dealRegister(EmailAddress.value);
            }
        });
    }
    );


    function dealRegister(email) {
        //var emailTrans = "emailAddress:email";
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/dealRegisterLogin.aspx?" + new Date().getTime(), //¼ÓÈëÊ±¼ä urlÆÛÆ­ ÖØÐÂÔËÐÐajax
            type: "Get",
            data: 'emailAddress=' + email,
            success: function (data, statue) {
                dataEmail = data + "";
                var strItem = new Array();
                strItem = dataEmail.split("<!>"); //Ã¿¸öÉÌÆ·Ö®¼äÓÃ <!> ·Ö¸î
                if (parseInt(strItem) == 1) {
                    var EmailAddress = document.getElementById("ctl00_ctl00_cph1_cph1_ctrlCustomerRegister_CreateUserForm_CreateUserStepContainer_UserName");
                    jAlert('The e-mail address that you entered is already in use. Please enter a different e-mail address.', 'TIPS');
                    EmailAddress.value = "";
                    $("#ctl00_ctl00_cph1_cph1_ctrlCustomerRegister_CreateUserForm_CreateUserStepContainer_UserNameOrEmailRequired").css("display", "inline");
                }
                else {
                    //Èç¹û²»´æÔÚ Õý³£Í¨¹ý
                }

            }
        });
    }
</script>
<div class="registration-page">
    <div class="page-title">
        <h1><%=GetLocaleResourceString("Account.Registration")%></h1>
    </div>
    <div class="clear">
    </div>
    <div class="body">
        <asp:CreateUserWizard ID="CreateUserForm" EmailRegularExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"
            RequireEmail="False" runat="server" OnCreatedUser="CreatedUser" OnCreatingUser="CreatingUser"
            OnCreateUserError="CreateUserError" FinishDestinationPageUrl="~/default.aspx"
            ContinueDestinationPageUrl="~/default.aspx" Width="100%" LoginCreatedUser="true"
            DuplicateEmailErrorMessage="<% $NopResources:Account.DuplicateEmail %>" 
            DuplicateUserNameErrorMessage="<% $NopResources:Account.DuplicateUserName %>">
            <WizardSteps> 
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" Title="">
                    <ContentTemplate>
                        <div class="message-error">
                            <div>
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </div>
                            <div class="clear">
                            </div>
                            <div>
                            <div style="display:none">
                                <asp:ValidationSummary ID="valSum" runat="server" ShowSummary="true" DisplayMode="BulletList"
                                    ValidationGroup="CreateUserForm" />
                           </div>
                           </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="section-title">
                            <%=GetLocaleResourceString("Account.YourPersonalDetails")%>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="section-body">
                            <table class="table-container">
                                <tbody>
                                <asp:PlaceHolder runat="server" ID="phGender">
                                    <tr class="row">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.Gender")%>:
                                        </td>
                                        <td class="item-value">
                                            <asp:RadioButton runat="server" ID="rbGenderM" GroupName="Gender" Text="<% $NopResources:Account.GenderMale %>"
                                                Checked="true" />
                                            <asp:RadioButton runat="server" ID="rbGenderF" GroupName="Gender" Text="<% $NopResources:Account.GenderFemale %>" />
                                        </td>
                                    </tr>
                                     </asp:PlaceHolder>
                                    <tr class="row">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.FirstName")%>:
                                        </td>
                                        <td class="item-value">
                                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="40"></asp:TextBox><font color="red">*</font>
                                            <br/>
                                            <asp:RequiredFieldValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                                ErrorMessage="<% $NopResources:Account.FirstNameIsRequired %>" ToolTip="<% $NopResources:Account.FirstNameIsRequired %>"
                                                ValidationGroup="CreateUserForm"><%=GetLocaleResourceString("OsShop.RequiredField")%>
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="row">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.LastName")%>:
                                        </td>
                                        <td class="item-value">
                                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="40" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                                            <br/>
                                            <asp:RequiredFieldValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                                ErrorMessage="<% $NopResources:Account.LastNameIsRequired %>" ToolTip="<% $NopResources:Account.LastNameIsRequired %>"
                                                ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="phDateOfBirth">
                                    <tr class="row">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.DateOfBirth")%>:
                                        </td>
                                        <td class="item-value">
                                            <nopCommerce:NopDatePicker runat="server" ID="dtDateOfBirth" DayText="<% $NopResources:DatePicker2.Day %>"
                                                MonthText="<% $NopResources:DatePicker2.Month %>" YearText="<% $NopResources:DatePicker2.Year %>" />
                                        </td>
                                    </tr>
                                     </asp:PlaceHolder>
                                    <%--pnlEmail is visible only when customers are authenticated by usernames and is used to get an email--%>
                                    <tr class="row" runat="server" id="pnlEmail">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.E-Mail")%>:
                                        </td>
                                        <td class="item-value">
                                            <asp:TextBox ID="Email"  CssClass="onepageCheckOut_error" runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                                            <br/>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                                ErrorMessage="Email is required" ToolTip="Email is required" Display="Dynamic"
                                                ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator  CssClass="onepageCheckOut_error" runat="server" ID="revEmail" Display="Dynamic" ControlToValidate="Email"
                                                ErrorMessage="Invalid email" ToolTip="Invalid email" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"
                                                ValidationGroup="CreateUserForm"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <%--this table row is used to get an username when customers are authenticated by usernames--%>
                                    <%--this table row is used to get an email when customers are authenticated by emails--%>
                                    <tr class="row">
                                        <td class="item-name">
                                            <asp:Literal runat="server" ID="lUsernameOrEmail" Text="E-Mail" />:
                                        </td>
                                        <td class="item-value">
                                            <asp:TextBox ID="UserName"  runat="server" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                                            <br/>
                                            <asp:RequiredFieldValidator  CssClass="onepageCheckOut_error" Display="Dynamic" ID="UserNameOrEmailRequired" runat="server" ControlToValidate="UserName"
                                                ErrorMessage="Username is required" ToolTip="Username is required"
                                                ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator  CssClass="onepageCheckOut_error" Display="Dynamic" runat="server" ID="refUserNameOrEmail"
                                                ControlToValidate="UserName" ErrorMessage="Invalid email" ToolTip="Invalid email"
                                                ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ValidationGroup="CreateUserForm"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="clear">
                        </div>
                        <asp:PlaceHolder runat="server" ID="phCompanyDetails">
                            <div class="section-title">
                                <%=GetLocaleResourceString("Account.CompanyDetails")%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="section-body">
                                <table class="table-container">
                                    <tbody>
                                        <asp:PlaceHolder runat="server" ID="phCompanyName">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.CompanyName")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                                                    <br/>
                                                    <asp:RequiredFieldValidator  CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvCompany" runat="server" ControlToValidate="txtCompany"
                                                        ErrorMessage="<% $NopResources:Account.CompanyIsRequired %>" ToolTip="<% $NopResources:Account.CompanyIsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phVatNumber">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.VATNumber")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtVatNumber" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="row">
                                                <td colspan="2">
                                                    <i>
                                                        <%=GetLocaleResourceString("VAT.EnteredWithoutCountryCode")%></i>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="phYourAddress">
                            <div class="section-title">
                                <%=GetLocaleResourceString("Account.YourAddress")%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="section-body">
                                <table class="table-container">
                                    <tbody>
                                        <asp:PlaceHolder runat="server" ID="phStreetAddress">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.StreetAddress")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtStreetAddress" runat="server"></asp:TextBox>
                                                    <br/>
                                                    <asp:RequiredFieldValidator  CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvStreetAddress" runat="server" ControlToValidate="txtStreetAddress"
                                                        ErrorMessage="<% $NopResources:Account.StreetAddressIsRequired %>" ToolTip="<% $NopResources:Account.StreetAddressIsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phStreetAddress2">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.StreetAddress2")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtStreetAddress2" runat="server"></asp:TextBox>
                                                    <br/>
                                                    <asp:RequiredFieldValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvStreetAddress2" runat="server" ControlToValidate="txtStreetAddress2"
                                                        ErrorMessage="<% $NopResources:Account.StreetAddress2IsRequired %>" ToolTip="<% $NopResources:Account.StreetAddress2IsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phPostCode">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.PostCode")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtZipPostalCode" runat="server"></asp:TextBox>
                                                   <br/>
                                                   <asp:RequiredFieldValidator  CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvZipPostalCode" runat="server" ControlToValidate="txtZipPostalCode"
                                                        ErrorMessage="<% $NopResources:Account.ZipPostalCodeIsRequired %>" ToolTip="<% $NopResources:Account.ZipPostalCodeIsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phCity">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.City")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="40"></asp:TextBox>
                                                    <br/>
                                                    <asp:RequiredFieldValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                                        ErrorMessage="<% $NopResources:Account.CityIsRequired %>" ToolTip="<% $NopResources:Account.CityIsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phCountry">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.Country")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:DropDownList ID="ddlCountry" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                        Width="137px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phStateProvince">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.StateProvince")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:DropDownList ID="ddlStateProvince" AutoPostBack="False" runat="server" Width="137px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="phYourContactInformation">
                            <div class="section-title">
                                <%=GetLocaleResourceString("Account.YourContactInformation")%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="section-body">
                                <table class="table-container">
                                    <tbody>
                                        <asp:PlaceHolder runat="server" ID="phTelephoneNumber">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.TelephoneNumber")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                                    <br/>
                                                    <asp:RequiredFieldValidator  CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber"
                                                        ErrorMessage="<% $NopResources:Account.PhoneNumberIsRequired %>" ToolTip="<% $NopResources:Account.PhoneNumberIsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder runat="server" ID="phFaxNumber">
                                            <tr class="row">
                                                <td class="item-name">
                                                    <%=GetLocaleResourceString("Account.FaxNumber")%>:
                                                </td>
                                                <td class="item-value">
                                                    <asp:TextBox ID="txtFaxNumber" runat="server"></asp:TextBox>
                                                    <br/>
                                                    <asp:RequiredFieldValidator  CssClass="onepageCheckOut_error" Display="Dynamic" ID="rfvFaxNumber" runat="server" ControlToValidate="txtFaxNumber"
                                                        ErrorMessage="<% $NopResources:Account.FaxIsRequired %>" ToolTip="<% $NopResources:Account.FaxIsRequired %>"
                                                        ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="phNewsletter">
                            <div class="section-title">
                                <%=GetLocaleResourceString("Account.Options")%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="section-body">
                                <table class="table-container">
                                    <tbody>
                                        <tr class="row">
                                            <td class="item-name">
                                                <%=GetLocaleResourceString("Account.Newsletter")%>:
                                            </td>
                                            <td class="item-value">
                                                <asp:CheckBox ID="cbNewsletter" runat="server" Checked="true"></asp:CheckBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="phPreferences">
                            <div class="section-title">
                                <%=GetLocaleResourceString("Account.Preferences")%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="section-body">
                                <table class="table-container">
                                    <tbody>
                                        <tr class="row" runat="server" id="trTimeZone">
                                            <td class="item-name">
                                                <%=GetLocaleResourceString("Account.TimeZone")%>:
                                            </td>
                                            <td class="item-value">
                                                <asp:DropDownList ID="ddlTimeZone" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="clear">
                            </div>
                        </asp:PlaceHolder>
                        <div class="section-title">
                            <%=GetLocaleResourceString("Account.YourPassword")%>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="section-body">
                            <table class="table-container">
                                <tbody>
                                    <tr class="row">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.Password")%>:
                                        </td>
                                        <td class="item-value">
                                            <asp:TextBox ID="Password" runat="server" MaxLength="50" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
                                            <br/>
                                            <asp:RequiredFieldValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                ErrorMessage="<% $NopResources:Account.PasswordIsRequired %>" ToolTip="<% $NopResources:Account.PasswordIsRequired %>"
                                                ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="row">
                                        <td class="item-name">
                                            <%=GetLocaleResourceString("Account.PasswordConfirmation")%>:
                                        </td>
                                        <td class="item-value">
                                            <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox><font color="red">*</font>
                                            <br/>
                                            <asp:RequiredFieldValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                                ErrorMessage="<% $NopResources:Account.ConfirmPasswordIsRequired %>" ToolTip="<% $NopResources:Account.ConfirmPasswordIsRequired %>"
                                                ValidationGroup="CreateUserForm"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator CssClass="onepageCheckOut_error" Display="Dynamic" ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                ControlToValidate="ConfirmPassword" ErrorMessage="<% $NopResources:Account.EnteredPasswordsDoNotMatch %>"
                                                ToolTip="<% $NopResources:Account.EnteredPasswordsDoNotMatch %>" ValidationGroup="CreateUserForm"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr class="row">
                                        <td colspan="2">
                                            <nopCommerce:Captcha ID="CaptchaCtrl" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="clear">
                        </div>
                    </ContentTemplate>
                    <CustomNavigationTemplate>
                        <div class="button">
                            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="<% $NopResources:Account.RegisterNextStepButton %>"
                                ValidationGroup="CreateUserForm" CssClass="registernextstepbutton" OnPreRender="StepNextButton_PreRender" />
                        </div>
                    </CustomNavigationTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                    <ContentTemplate>
                        <div class="section-body">
                            <p>
                                <asp:Label runat="server" ID="lblCompleteStep"></asp:Label>
                            </p>
                            <br />
                            <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue"
                                Text="<% $NopResources:Account.RegisterContinueButton %>" ValidationGroup="CreateUserForm"
                                CssClass="completeregistrationbutton" />
                        </div>
                        <div class="clear">
                        </div>
                        <div class="section-body">
                        </div>
                        <div class="clear">
                        </div>
                    </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps> 
        </asp:CreateUserWizard>
        <nopCommerce:Topic ID="topicRegistrationNotAllowed" runat="server" TopicName="RegistrationNotAllowed"
            OverrideSEO="false" Visible="false"></nopCommerce:Topic>
    </div>
</div>





<%--vendor registration below here--%>

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
</asp:Content>
