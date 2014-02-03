<%@ Page Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true"
    CodeBehind="HelpHome.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.HelpHome" %>

<%@ Register Src="~/VendorAdministration/Modules/HelpHome.ascx" TagName="HelpHome" TagPrefix="nopCommerce" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cph1">
    <nopCommerce:HelpHome ID="ctrlHelpHome" runat="server" />
</asp:Content>
