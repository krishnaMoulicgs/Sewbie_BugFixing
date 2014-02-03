<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageProductsControl"
    CodeBehind="HomePageProducts.ascx.cs" %>
<div class="home-page-product-grid">
    <%--<div class="boxtitle">
        <%=GetLocaleResourceString("HomePage.FeaturedProducts")%>
    </div>
    <div class="clear">
    </div>--%>
<div class="blk_18">
<div id="ISL_Cont_1" class="pcont">
<div class="ScrCont">
<div id="List1_1">
    <asp:DataList ID="dlCatalog" runat="server" RepeatColumns="0" RepeatDirection="Horizontal"
        RepeatLayout="Flow" OnItemDataBound="dlCatalog_ItemDataBound" ItemStyle-CssClass="item-box" EnableViewState="false">
        <ItemTemplate>
            <div class="product-item">
                <div class="picture">
                    <asp:HyperLink ID="hlImageLink" runat="server" />
                </div>
                <h2 class="product-title">
                    <asp:HyperLink ID="hlProduct" runat="server" />
                </h2>
            </div>
        </ItemTemplate>
    </asp:DataList>           
</div>
<div id="List2_1"></div>
<div id="List2_12"></div>

</div>
</div>
</div>
<div class="clear"></div>
<div class="homepage_buttons">
<a onmouseup="ISL_StopUp_1()" class="LeftBotton" 
onmouseout="ISL_StopUp_1()" onmousedown="ISL_GoUp_1()" href="javascript:void(0);" 
target="_self"></a>
<a onmouseup="ISL_StopDown_1()" 
class="RightBotto"n onmouseout="ISL_StopDown_1()" onmousedown="ISL_GoDown_1()" 
href="javascript:void(0);" target="_self"></a>
</div>
<div class="clear"></div>
<script type="text/javascript">
<!--
picrun_ini()
//-->
</script>
  
</div>                                 


