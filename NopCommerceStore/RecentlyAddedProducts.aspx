<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.RecentlyAddedProductsPage" Codebehind="RecentlyAddedProducts.aspx.cs" %>
<%@ Register TagPrefix="OsShop" TagName="ProductFilter" Src="~/AddonsByOsShop/Modules/Rfilter.ascx"%>
<%@ Register TagPrefix="OsShop" TagName="RecentlyAddedProductsList" Src="~/AddonsByOsShop/Modules/RproductList.ascx" %>
<%@ Register TagPrefix="OsShop" TagName="RecentlyAddedProductsTitle" Src="~/AddonsByOsShop/Modules/RproductListTitle.ascx"%>

<asp:Content ID="Content2" ContentPlaceHolderID="cph2" runat="server">
    <OsShop:ProductFilter Id="ctrlProductFilter" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
<div class="recently-added-products">
    <OsShop:RecentlyAddedProductsTitle ID="ctrlRecentlyAddedProductsTitle" runat="server" />
    <div class="clear">
    </div>
    <OsShop:RecentlyAddedProductsList ID="ctrlRecentlyAddedProductsList" runat="server" />
</div>
</asp:Content>
