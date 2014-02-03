<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductShortList.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.ProductShortList" %>

<script type="text/javascript">
    function shortShowBuyNow() {
        return '<%=GetLocaleResourceString("OsShop.BuyNow")%>';
    }
</script>

<script type='text/javascript' src='/AddonsByOsShop/CustomScripts/jquery.simplemodal.js'></script>
<script type='text/javascript' src="/AddonsByOsShop/CustomScripts/osx.js"></script>
<script type='text/javascript' src='/AddonsByOsShop/CustomScripts/productShortList.js'></script>


<div id="productShortShowContent">
	<div id="osx-modal-content">
			<div class="close"><a href="#" class="simplemodal-close"><%=GetLocaleResourceString("OsShop.Close")%></a></div>
			<div id="osx-modal-data">
             <div id="ProductShortShow_LeftRight">
             <input type="button" class="productShortShow_prev" onclick="picList('pre')" value="" id="btnPre" />
             <input type="button" class="productShortShow_next"  onclick="picList('next')" value="" id="btnNext" />
             </div>
             <div class="ProductShortShow_move">
             <input id="moveLeft" type="button" value='<%=GetLocaleResourceString("Pager.Previous")%>' onclick="productLeft();"/>
             <input id="moveRight" type="button" value='<%=GetLocaleResourceString("Pager.Next")%>' onclick="productRight();"/>
             </div>
            <div id="ProductListMessage"></div>
			</div>
		</div>
</div>


