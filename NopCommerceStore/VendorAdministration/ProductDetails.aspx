<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VendorAdministration/main.master"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Administration_ProductDetails"
    CodeBehind="ProductDetails.aspx.cs"  %>

<%@ Register TagPrefix="nopCommerce" TagName="ProductDetails" Src="Modules/ProductDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:ProductDetails runat="server" ID="ctrlProductDetails" />
</asp:Content>
