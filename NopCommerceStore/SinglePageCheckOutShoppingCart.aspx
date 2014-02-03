<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true" CodeBehind="SinglePageCheckOutShoppingCart.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.SinglePageCheckOutShoppingCart" %>
<%@ Register TagPrefix="nopCommerce" TagName="SinglePageCheckOutOrderSummary" Src="~/Modules/SinglePageCheckOutOrderSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <div class="shoppingcart-page">
        <div class="page-title">
            <h1><%=GetLocaleResourceString("Account.ShoppingCart")%></h1>
        </div>
        <div class="clear">
        </div>
        <div class="body">
            <nopCommerce:SinglePageCheckOutOrderSummary ID="SinglePageCheckOutOrderSummaryControl" runat="server" IsShoppingCart="true">
            </nopCommerce:SinglePageCheckOutOrderSummary>
        </div>
        <div class="common-buttons">
            <asp:Button ID="btnCheckOut" OnClick="btnCheckOut_Click" runat="server"
                Text="Check Out" CssClass="continueshoppingbutton" />
        </div>
    </div>
</asp:Content>
