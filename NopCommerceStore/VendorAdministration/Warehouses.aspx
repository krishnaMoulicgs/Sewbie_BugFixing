<%@ Page Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.VendorAdministration_Warehouses"
    CodeBehind="Warehouses.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="Warehouses" Src="Modules/Warehouses.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:Warehouses runat="server" ID="ctrlWarehouses" />
</asp:Content>