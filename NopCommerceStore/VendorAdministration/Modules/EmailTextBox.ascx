<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.EmailTextBox"
    CodeBehind="EmailTextBox.ascx.cs" %>
<asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue"
    ErrorMessage="<% $NopResources:VendorAdmin.Email.Required %>" Display="None" />
<asp:RegularExpressionValidator ID="revValue" runat="server" ControlToValidate="txtValue"
    ValidationExpression=".+@.+\..+" ErrorMessage="<% $NopResources:VendorAdmin.Email.WrongEmail %>"
    Display="None" />
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
    HighlightCssClass="validatorCalloutHighlight" />
<ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="revValueE" TargetControlID="revValue"
    HighlightCssClass="validatorCalloutHighlight" />
