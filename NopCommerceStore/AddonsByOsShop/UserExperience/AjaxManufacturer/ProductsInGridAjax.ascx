<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsInGridAjax.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AjaxManufacturer.ProductsInGridAjax" %>
<%@ Register TagPrefix="OsShop" TagName="ProductList" Src="~/AddonsByOsShop/Modules/MproductList.ascx" %>

<div class="category-page">
    <div class="breadcrumb">
        <a href='<%=CommonHelper.GetStoreLocation()%>'>
            <%=GetLocaleResourceString("Breadcrumb.Top")%></a> /
        <asp:Repeater ID="rptrCategoryBreadcrumb" runat="server">
            <ItemTemplate>
                <a href='<%#SEOHelper.GetManufacturerUrl(Convert.ToInt32(Eval("ManufacturerId"))) %>'>
                        <%#Server.HtmlEncode(Eval("LocalizedName").ToString())%></a>
            </ItemTemplate>
            <SeparatorTemplate>
                /
            </SeparatorTemplate>
        </asp:Repeater>
        <br />
    </div>

    <div class="clear">
    </div>

    <div id="productBox">
        <OsShop:ProductList ID="ctrlProductList" runat="server" />
    </div>   
    <div id="overlay" class="overlay"></div>
</div>