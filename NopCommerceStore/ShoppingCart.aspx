<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.ShoppingCartPage" CodeBehind="ShoppingCart.aspx.cs"
     %>
<%@ Register TagPrefix="nopCommerce" TagName="OrderSummary" Src="~/Modules/OrderSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <div class="shoppingcart-page">
        <div class="page-title">
            <h1><%=GetLocaleResourceString("Account.ShoppingCart")%></h1>
        </div>
        <div class="clear">
        </div>
        <div class="body">
            <nopCommerce:OrderSummary ID="OrderSummaryControl" runat="server" IsShoppingCart="true">
            </nopCommerce:OrderSummary>
        </div>
    </div>
</asp:Content>
