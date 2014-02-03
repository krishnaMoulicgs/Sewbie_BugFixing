<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.LoginPage" Title="Login" Codebehind="Login.aspx.cs"  %>

<%@ Register TagPrefix="nopCommerce" TagName="CustomerLogin" Src="~/Modules/CustomerLogin.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript" src="Scripts/login.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <nopCommerce:CustomerLogin ID="ctrlCustomerLogin" runat="server" />
</asp:Content>
