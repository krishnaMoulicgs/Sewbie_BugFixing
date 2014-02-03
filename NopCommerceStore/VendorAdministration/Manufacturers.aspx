<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VendorAdministration/main.master"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.VendorAdministration_Manufacturers"
    CodeBehind="Manufacturers.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="Manufacturers" Src="~/VendorAdministration/Modules/Manufacturers.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:Manufacturers runat="server" ID="ctrlManufacturers" />
</asp:Content>
