<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RproductListTitle.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.RproductListTitle" %>

<div class="page-title">
    <table width="100%">
        <tr>
            <td style="text-align: left;">
                <h1>
                    <%=GetLocaleResourceString("Products.NewProducts")%></h1>
            </td>
            <td style="text-align: right;">
                <a href="<%=Page.ResolveUrl("~/recentlyaddedproductsrss.aspx")%>">
                    <asp:Image ID="imgRSS" runat="server" ImageUrl="~/images/icon_rss.gif" ToolTip="<% $NopResources:RecentlyAddedProductsRSS.Tooltip %>"
                        AlternateText="RSS" /></a>
            </td>
        </tr>
    </table>
</div>