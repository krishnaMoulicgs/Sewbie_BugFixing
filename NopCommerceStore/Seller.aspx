<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.SellerPage" CodeBehind="Seller.aspx.cs"
     %>
<%@ Register TagPrefix="OsShop" TagName="ProductFilter" Src="~/AddonsByOsShop/Modules/Mfilter.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cph2" runat="server">
    <OsShop:ProductFilter runat="server" ID="ctrlProductFilter" />
    <div class="clear">
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <asp:PlaceHolder runat="server" ID="ManufacturerPlaceHolder"></asp:PlaceHolder>
</asp:Content>
