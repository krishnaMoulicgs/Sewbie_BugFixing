<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mfilter.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.Mfilter" %>

<%--<script src="/Scripts/jquery-1.3.2.js" type="text/javascript"></script>--%>
<script src="/Scripts/ui.core.js" type="text/javascript"></script>
<script src="/Scripts/ui.slider.js" type="text/javascript"></script>
<script type="text/javascript">
    var urlManufacturerId = "<%=txtManufacturerId.ClientID %>";
    var menu_maxPrice=<%=maxPrice %>;
    var menu_minPrice=<%=minPrice %>; 

    var menu_getAttrValue = function() {
        return <%=AttJs %>;
    }

    var menu_getSpecValue=function(){
        return <%=SpeJs %>;
    }
</script>
<script src="/AddonsByOsShop/CustomScripts/MajaxClient.js" type="text/javascript"></script>


<asp:Panel ID="pnlSubCategories" runat="server">
<div class="block block-category-navigation">
    <div class="title">
        <%=GetLocaleResourceString("Category.Categories")%>
    </div>
    <div class="clear"></div>
    <div class="listbox">
        <ul id="ProductSubcategories">
            <asp:PlaceHolder runat="server" ID="phCategories"/>
        </ul>
    </div>
</div>
</asp:Panel>

<asp:panel ID="pnlPrice" runat="server">
<div class="block block-category-navigation">
        <div class="title">
           <%=GetLocaleResourceString("OsShop.PriceRange")%>
        </div>
        <div class="pricerangelistbox">
            <div class="pricerangetitleph">  
                <span class="fl"><%=GetLocaleResourceString("OsShop.Min")%>:<%=CurrencySymbol%><span id="productMin"><%=(int)minPrice %></span></span>
                <span class="fr"><%=GetLocaleResourceString("OsShop.Max")%>:<%=CurrencySymbol%><span id="productMax"><%=(int)maxPrice%></span></span>
            </div>
            <div id="slider" class="ui-slider">
            </div>
            <div class="pricerangebody">
                <span class="price-tip slider-price-user-min" style="float:left;"><%=CurrencySymbol%><span id="userMin"><%=(int)minPrice%></span></span>
                <span class="price-tip slider-price-user-max" style="float:right;"><%=CurrencySymbol%><span id="userMax"><%=(int)maxPrice%></span></span>
            </div>
        </div>
    </div>
</asp:panel>

<div id="filterByAttribute">
<asp:Panel runat="server" ID="pnlAttributes" >
</asp:Panel>
</div>

<div id="filterBySpec">
<asp:Panel runat="server" ID="pnlSpec" >
</asp:Panel>
</div>

<div style="display: none;">
    <input type="text" name="filterProductSubcategories" id="filterProductSubcategories" />
    <asp:TextBox runat="server" ID="txtManufacturerId"></asp:TextBox>
    <input type="text" name="filterPriceMaxTxt" id="filterPriceMaxTxt" value="<%=maxPrice %>" />
    <input type="text" name="filterPriceMinTxt" id="filterPriceMinTxt" value="<%=minPrice %>" />
    <input type="text" name="filterSortTxt" id="filterSortTxt" />
    <input type="text" name="filterPageIndex" id="filterPageIndex" value="1" />
    <input type="text" name="filterPageSize" id="filterPageSize" value="<%=PageSize %>"/>
    <asp:TextBox runat="server" Text ="" ID ="attr_num"></asp:TextBox>
    <input type="text" name="filterAttrs" id="filterAttrs" />
    <asp:TextBox runat="server" Text ="" ID ="spec_num"></asp:TextBox>
    <input type="text" name="filterSpec" id="filterSpec" />
</div>
