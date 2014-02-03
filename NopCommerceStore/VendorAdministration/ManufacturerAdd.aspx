<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VendorAdministration/main.master"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.VendorAdministration_ManufacturerAdd"
    CodeBehind="ManufacturerAdd.aspx.cs"  %>

<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerAdd" Src="Modules/ManufacturerAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:ManufacturerAdd runat="server" ID="ctrlManufacturerAdd" />
</asp:Content>