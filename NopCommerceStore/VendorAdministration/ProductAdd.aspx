<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/VendorAdministration/main.master"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Administration_ProductAdd"
    CodeBehind="ProductAdd.aspx.cs"  %>

<%@ Register TagPrefix="nopCommerce" TagName="ProductAdd" Src="Modules/ProductAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:ProductAdd runat="server" ID="ctrlProductAdd" />
</asp:Content>
