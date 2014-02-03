<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/SimpleColumn.master"
    CodeBehind="Sitemap.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.Sitemap"
     %>
     
<asp:Content runat="server" ContentPlaceHolderID="cph1">
<!-- Alex Garcia 8/17/2011 sitemap formatting -->
    <div class="sitemap-page">
        <div class="page-title">
            <h1>
                <%=GetLocaleResourceString("Sitemap.Title")%></h1>
        </div>
        <div class="entity">
            <asp:DataList ID="dlTopics" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                RepeatLayout="Table" OnItemDataBound="dlTopics_ItemDataBound" EnableViewState="false"
                ItemStyle-CssClass="topic-box" Width="100%" ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                   |&nbsp;<asp:HyperLink ID="hlLink" runat="server" />&nbsp;
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="clear">
        </div>
        <div class="entity">
            <asp:DataList ID="dlCategories" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnItemDataBound="dlCategories_ItemDataBound" EnableViewState="false"
                ItemStyle-CssClass="category-box" Width="100%" ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Left">
                <HeaderTemplate>
                    <h2>
                        <%=GetLocaleResourceString("Sitemap.Categories")%></h2>
                </HeaderTemplate>
                <ItemTemplate>
                     |&nbsp;<asp:HyperLink ID="hlLink" runat="server" />&nbsp;
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="clear">
        </div>
        <div class="entity">
            <asp:DataList ID="dlManufacturers" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnItemDataBound="dlManufacturers_ItemDataBound" EnableViewState="false"
                ItemStyle-CssClass="manufacturer-box" Width="100%" ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Left">
                <HeaderTemplate>
                    <h2>
                        <%=GetLocaleResourceString("Sitemap.Manufacturers")%></h2>
                </HeaderTemplate>
                <ItemTemplate>
                    |&nbsp;<asp:HyperLink ID="hlLink" runat="server" />&nbsp;
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="clear">
        </div>
        <div class="entity">
            <asp:DataList ID="dlProducts" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnItemDataBound="dlProducts_ItemDataBound" EnableViewState="false"
                ItemStyle-CssClass="product-box" Width="100%" ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Left">
                <HeaderTemplate>
                    <h2>
                        <%=GetLocaleResourceString("Sitemap.Products")%></h2>
                </HeaderTemplate>
                <ItemTemplate>
                    |&nbsp;<asp:HyperLink ID="hlLink" runat="server" />&nbsp;
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
