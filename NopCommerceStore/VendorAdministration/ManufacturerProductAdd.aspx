<%@ Page Language="C#" MasterPageFile="~/VendorAdministration/popup.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.VendorAdministration_ManufacturerProductAdd"
    CodeBehind="ManufacturerProductAdd.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerProductAdd" Src="Modules/ManufacturerProductAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:ManufacturerProductAdd runat="server" ID="ctrlManufacturerProductAdd" />
</asp:Content>
