<%@ Page Language="C#" MasterPageFile="~/VendorAdministration/main.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Administration_Products"
    CodeBehind="Products.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="Products" Src="Modules/Products.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:Products runat="server" ID="ctrlProducts" />
</asp:Content>
