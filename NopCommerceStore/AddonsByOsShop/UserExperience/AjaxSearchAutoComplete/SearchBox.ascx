<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.SearchBoxControl" 
    CodeBehind="SearchBox.ascx.cs" %>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/SearchBox.js"></script>

<script type="text/javascript">
    function searchPage() {
        window.location.href = '<%=CommonHelper.GetStoreLocation()%>' + "Search.aspx?searchterms=" + $("#txtSearchTerms").val();
    }
</script>
<li style="float: right; margin-right: 26px;">
     <input type="button" id="btnSearch" class="searchboxbutton" onclick="searchPage();" value="<%=GetLocaleResourceString("Search.SearchButton")%>"/>
</li>
<li style="float: right;">
    <div class="searchbox_text"><%=GetLocaleResourceString("Search.SearchStoreTooltip")%></div>
    <input type="text" id="txtSearchTerms" class="searchboxtext" onblur="onblurValue();" onfocus="onfocusValue();" onkeyup="findProducts();" onkeydown="txtSearch_onKeyDown();" value="&nbsp;"/>&nbsp;
</li>
<div class="showSearchResult_loading"></div>
<div id="showSearchResult">
</div>
