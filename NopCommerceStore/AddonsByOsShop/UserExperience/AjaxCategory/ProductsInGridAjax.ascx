<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsInGridAjax.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.UserExperience.AjaxCategory.ProductsInGridAjax" %>

<%@ Register TagPrefix="OsShop" TagName="ProductList" Src="~/AddonsByOsShop/Modules/CproductList.ascx" %>

<div class="category-page">
    <div class="breadcrumb">
        <a href='<%=CommonHelper.GetStoreLocation()%>'>
            <%=GetLocaleResourceString("Breadcrumb.Top")%></a> /
        <asp:Repeater ID="rptrCategoryBreadcrumb" runat="server">
            <ItemTemplate>
                <a href='<%#SEOHelper.GetCategoryUrl(Convert.ToInt32(Eval("CategoryId"))) %>'>
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

