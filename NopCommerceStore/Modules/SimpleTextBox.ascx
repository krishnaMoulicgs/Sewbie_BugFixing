﻿<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.Modules.SimpleTextBox" Codebehind="SimpleTextBox.ascx.cs" %>
<asp:TextBox ID="txtValue"  runat="server" AutoCompleteType="Disabled"></asp:TextBox><font color="red">*</font>
<div>
<asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtValue" Font-Name="verdana"
    Font-Size="9pt" runat="server" Display="Dynamic"><div class="onepageCheckOut_error"><%=GetLocaleResourceString("OsShop.RequiredField")%></div>
</asp:RequiredFieldValidator>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
</div>



