<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.EmailTextBox"
    CodeBehind="EmailTextBox.ascx.cs" %>
<asp:TextBox ID="txtValue" runat="server"></asp:TextBox><font color="red">*</font>
<asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue"
    Display="Dynamic"><div class="onepageCheckOut_error"><%=GetLocaleResourceString("OsShop.RequiredField")%></div></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revValue" runat="server" ControlToValidate="txtValue"
    ValidationExpression=".+@.+\..+" ErrorMessage="<% $NopResources:Account.WrongEmailFormat %>"
    Display="Dynamic" />
