<%@ Page Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.VendorAdministration_Orders"
    CodeBehind="Orders.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="Orders" Src="~/VendorAdministration/Modules/Orders.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:Orders runat="server" ID="ctrlOrders" />
</asp:Content>