<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VendorAdministration/main.master"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Administration_SellerDetails"
    CodeBehind="SellerDetails.aspx.cs"  %>

<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerDetails" Src="Modules/ManufacturerDetails.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:ManufacturerDetails runat="server" ID="ctrlManufacturerDetails" />
</asp:Content>