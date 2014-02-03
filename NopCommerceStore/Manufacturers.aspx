<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/SimpleColumn.master"
    CodeBehind="Manufacturers.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.ManufacturersPage"
     %>

<%@ Register TagPrefix="nopCommerce" TagName="MiniShoppingCartBox" Src="~/Modules/MiniShoppingCartBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="CategoryNavigation" Src="~/Modules/CategoryNavigation.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ManufacturerNavigation" Src="~/Modules/ManufacturerNavigation.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RecentlyViewedProducts" Src="~/Modules/RecentlyViewedProductsBox.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="cph1">
    <div class="manufacturerlist-page">
        <div class="page-title">
            <h1>
                <%=GetLocaleResourceString("Common.ManufacturerList")%></h1>
        </div>
        <div class="clear">
        </div>
        <div class="manufacturer-grid">
            <asp:DataList ID="dlManufacturers" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                RepeatLayout="Flow" OnItemDataBound="DlManufacturers_OnItemDataBound" ItemStyle-CssClass="item-box">
                <ItemTemplate>
                    <div class="manufacturer-item">
                        <div class="picture">
                            <asp:HyperLink ID="hlImageLink" runat="server" />
                        </div>
                           <h2 class="man-title">
                            <asp:HyperLink ID="hlManufacturer" runat="server" />
                        </h2>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
