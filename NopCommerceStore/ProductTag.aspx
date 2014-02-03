<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.ProductTagPage" CodeBehind="ProductTag.aspx.cs"
     %>

<%@ Register TagPrefix="nopCommerce" TagName="ProductsByTag" Src="~/Modules/ProductsByTag.ascx" %>
<%@ Register TagPrefix="OsShop" TagName="ProductFilter" Src="~/AddonsByOsShop/Modules/Tfilter.ascx"%>
<%@ Register TagPrefix="OsShop" TagName="TecentlyAddedProductsList" Src="~/AddonsByOsShop/Modules/TproductList.ascx" %>
<%@ Register TagPrefix="OsShop" TagName="TecentlyAddedProductsTitle" Src="~/AddonsByOsShop/Modules/TproductListTitle.ascx"%>

<asp:Content ID="Content2" ContentPlaceHolderID="cph2" runat="server">
    <OsShop:ProductFilter Id="ctrlProductFilter" runat="server" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
<div class="recently-added-products">
    <OsShop:TecentlyAddedProductsTitle ID="ctrlRecentlyAddedProductsTitle" runat="server" />
    <div class="clear">
    </div>
    <OsShop:TecentlyAddedProductsList ID="ctrlRecentlyAddedProductsList" runat="server" />
</div>
</asp:Content>