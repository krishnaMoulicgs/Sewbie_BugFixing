<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.ProductsAlsoPurchasedControl"
    CodeBehind="ProductsAlsoPurchased.ascx.cs" %>
    
   
<script language="javascript" type="text/javascript">
var also_sWidth = 220;
var also_visible = 4;
var Amr = 0; 
var also_bWidth ;
var also_listLength ;
var also_listWidth ;
var also_listLeft ;
var also_bId; 
var also_listId;
var also_trendLeft;
var maxAmr;
function also_init(){
also_bId = $("#also_bigDiv");
also_listId = $("#<%=dlAlsoPurchasedProducts.ClientID%>");
also_bWidth = also_bId.width();
also_listLength = also_listId.find(".item-box").size();
also_listWidth = also_listLength*also_sWidth;
also_listLeft =parseInt(also_listId.css('left'));
} 
function also_picList(Vfx){
also_init();
maxAmr = also_listLength - also_visible ;
if(also_listWidth > also_bWidth){
if(Vfx == 'also_next'){
if(-Amr < maxAmr){
Amr--;
also_trendLeft = Amr*also_sWidth;
also_listId.animate({
left:also_trendLeft + "px"
},200);
}
}
else if(Vfx == 'also_pre'){
if( Amr < 0){
Amr++;
also_trendLeft = Amr*also_sWidth;
also_listId.animate({
left:also_trendLeft + "px"
},200);
}
}
}

if(-Amr == maxAmr){
$("input#btnalso_next").attr("disabled","disabled");
$("input#btnalso_next").css("background","#fff");
$("input#btnalso_pre").css("background","url(../App_Themes/Sewbie/images/related_left.gif) no-repeat center center");
$("input#btnalso_pre").attr("disabled","");
}else if(Amr==0){
$("input#btnalso_pre").attr("disabled","disabled");
$("input#btnalso_pre").css("background","#fff");
$("input#btnalso_next").css("background","url(../App_Themes/Sewbie/images/related_right.gif) no-repeat center center");
$("input#btnalso_next").attr("disabled","");
}else{
$("input#btnalso_next").attr("disabled","");
$("input#btnalso_next").css("background","url(../App_Themes/Sewbie/images/related_right.gif) no-repeat center center");
$("input#btnalso_pre").css("background","url(../App_Themes/Sewbie/images/related_left.gif) no-repeat center center");
$("input#btnalso_pre").attr("disabled","");
}
var pnum = $("#<%=dlAlsoPurchasedProducts.ClientID%> > span").size();
if( pnum < 5){
	$("#btnalso_pre").css("background","none");
	$("#btnalso_next").css("background","none");
	}
}

$(function(){
also_picList();   
})
</script>


<div class="also-purchased-products-grid">
<%--    <div class="title">
        <%=GetLocaleResourceString("Products.AlsoPurchased")%>
    </div>
    <div class="clear">
    </div>--%>
     <input type="button" class="also_lf btn" onclick="also_picList('also_pre')" value="" id="btnalso_pre"  />
    <div class="also_mainDiv also_lf" id="also_bigDiv">
    
    <asp:DataList ID="dlAlsoPurchasedProducts" runat="server" RepeatColumns="0" RepeatDirection="Horizontal"
        RepeatLayout="Flow" OnItemDataBound="dlAlsoPurchasedProducts_ItemDataBound"
        ItemStyle-CssClass="item-box">
        <ItemTemplate>
            <div class="item">
                <div class="picture">
                    <asp:HyperLink ID="hlImageLink" runat="server" />
                </div>
                 <div class="product-title">
                    <asp:HyperLink ID="hlProduct" runat="server" />
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
    
     </div>
    <input type="button" class="also_lf btn" onclick="also_picList('also_next')" value="" id="btnalso_next" />
</div>
