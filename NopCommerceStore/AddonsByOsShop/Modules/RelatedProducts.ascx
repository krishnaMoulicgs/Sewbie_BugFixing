<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.RelatedProductsControl"
    CodeBehind="RelatedProducts.ascx.cs" %>
    
<script language="javascript" type="text/javascript">
var top_sWidth = 120;
var top_visible = 3;
var Tmr = 0; 
var top_bWidth ;
var top_listLength ;
var top_listWidth ;
var top_listLeft ;
var top_bId; 
var top_listId;
var top_trendLeft;
var maxTmr;
function top_init(){
top_bId = $("#bigDiv");
top_listId = $("#<%=dlRelatedProducts.ClientID%>");
top_bWidth = top_bId.width();
top_listLength = top_listId.find("span").size();
top_listWidth = top_listLength*top_sWidth;
top_listLeft =parseInt(top_listId.css('top'));
} 
function top_picList(Tfx){
top_init();
maxTmr = top_listLength - top_visible ;
if(top_listWidth > top_bWidth){
if(Tfx == 'top_next'){
if(-Tmr < maxTmr){
Tmr--;
top_trendLeft = Tmr*top_sWidth;
top_listId.animate({
top:top_trendLeft + "px"
},200);
}
}
else if(Tfx == 'top_pre'){
if( Tmr < 0){
Tmr++;
top_trendLeft = Tmr*top_sWidth;
top_listId.animate({
top:top_trendLeft + "px"
},200);
}
}
}

if(-Tmr == maxTmr){
$("input#btntop_next").attr("disabled","disabled");
$("input#btntop_next").css("background","#fff");
$("input#btntop_pre").css("background","url(../App_Themes/darkOrange/images/related_prv.gif) no-repeat center center");
$("input#btntop_pre").attr("disabled","");
}else if(Tmr==0){
$("input#btntop_pre").attr("disabled","disabled");
$("input#btntop_pre").css("background","#fff");
$("input#btntop_next").css("background","url(../App_Themes/darkOrange/images/related_next.gif) no-repeat center center");
$("input#btntop_next").attr("disabled","");
}else{
$("input#btntop_next").attr("disabled","");
$("input#btntop_next").css("background","url(../App_Themes/darkOrange/images/related_next.gif) no-repeat center center");
$("input#btntop_pre").css("background","url(../App_Themes/darkOrange/images/related_prv.gif) no-repeat center center");
$("input#btntop_pre").attr("disabled","");
}
var pnum = $("#<%=dlRelatedProducts.ClientID%> > span").size();
if( pnum < 4){
	$("#btntop_pre").css("background","none");
	$("#btntop_next").css("background","none");
	}
}

$(function(){
top_picList();   
})
</script>


<div class="related-products-grid">
    <div class="title">
        <%=GetLocaleResourceString("Products.RelatedProducts")%>
    </div>
    <div class="clear">
    </div>
    <input type="button" class="top_lf btn" onclick="top_picList('top_pre')" value="" id="btntop_pre"  />
    <div class="top_mainDiv top_lf" id="bigDiv">

 
    <asp:DataList ID="dlRelatedProducts" runat="server" RepeatColumns="1" RepeatDirection="Vertical"
        RepeatLayout="Flow" OnItemDataBound="dlRelatedProducts_ItemDataBound" ItemStyle-CssClass="item-box">
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
    <input type="button" class="top_lf btn" onclick="top_picList('top_next')" value="" id="btntop_next" />
</div>
