<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MproductList.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.MproductList" %>

<link type='text/css' href='/AddonsByOsShop/Styles/productshort.css' rel='stylesheet' media='screen' /> 
<%@ Register TagPrefix="nopCommerce" TagName="productShow" Src="/AddonsByOsShop/Modules/ProductShortList.ascx" %>
<nopCommerce:productShow ID="productShortShowList" runat="server"></nopCommerce:productShow>

<div class="category-description">
    <asp:Literal runat="server" ID="lDescription"></asp:Literal>
</div>


<div class="filter-sort floatLeft">
    <%=GetLocaleResourceString("OsShop.SortBy")%>
    <select id="sortSelect" name="sortSelect" onchange="AjaxClient.Sort(this)">
        <option value="">
            <%=GetLocaleResourceString("OsShop.Select")%>
        </option>
        <option value="new" <%if (order == "new") {%>selected<%} %>>
            <%=GetLocaleResourceString("OsShop.WhatIsNew") %>
        </option>
        <option value="lth" <%if (order == "lth") {%>selected<%} %>>
            <%=GetLocaleResourceString("OsShop.PriceLowToHigh")%> 
        </option>
        <option value="htl" <%if (order == "htl") {%>selected<%} %>>
            <%=GetLocaleResourceString("OsShop.PriceHighToLow")%>
        </option>
    </select>
</div>

<div class="product-pager floatRight" id="pageContainerTop" runat="server">        
</div>
<div class="clear">
</div>

<asp:DataList ID="dlProducts" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
    RepeatLayout="Flow" ItemStyle-CssClass="item-box" OnItemDataBound="dlProducts_OnItemDataBound">
    <ItemTemplate>
    <div onmouseover="ShowProductsshort_button(this)" onmouseout="HideProductsshort_button(this)">
        <asp:HyperLink ID="hlProductImage" runat="server" />
        <br />
        <asp:HyperLink ID="hlProductName" runat="server" />
        <br />
        <asp:Label ID="lblPrice" runat="server" />
      <div> 
            <input id="Productsshort_button" type='button' name='show' value='<%=GetLocaleResourceString("OsShop.QuickLook")%>' class='osx demo' onclick="sunTest(<%#Eval("ProductId") %>,'<%=productIdList%>');"/>
            </div>
      </div>
    </ItemTemplate>
</asp:DataList>

<div class="clear">
</div>
<div class="product-pager floatRight" id="pageContainerBottom" runat="server">      
</div>