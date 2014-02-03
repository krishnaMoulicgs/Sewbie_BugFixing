<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.Modules.RecentlyViewedProductsControl" Codebehind="RecentlyViewedProducts.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductBox1" Src="~/Modules/ProductBox1.ascx" %>
    
<script language="javascript" type="text/javascript">
var view_sWidth = 220;
var view_visible = 4;
var Vmr = 0; 
var view_bWidth ;
var view_listLength ;
var view_listWidth ;
var view_listLeft ;
var view_bId; 
var view_listId;
var view_trendLeft;
var maxVmr;
function view_init(){
view_bId = $("#view_bigDiv");
view_listId = $("#<%=dlCatalog.ClientID%>");
view_bWidth = view_bId.width();
view_listLength = view_listId.find(".item-box").size();
view_listWidth = view_listLength*view_sWidth;
view_listLeft =parseInt(view_listId.css('left'));
} 
function view_picList(Vfx){
view_init();
maxVmr = view_listLength - view_visible ;
if(view_listWidth > view_bWidth){
if(Vfx == 'view_next'){
if(-Vmr < maxVmr){
Vmr--;
view_trendLeft = Vmr*view_sWidth;
view_listId.animate({
left:view_trendLeft + "px"
},200);
}
}
else if(Vfx == 'view_pre'){
if( Vmr < 0){
Vmr++;
view_trendLeft = Vmr*view_sWidth;
view_listId.animate({
left:view_trendLeft + "px"
},200);
}
}
}

if(-Vmr == maxVmr){
$("input#btnview_next").attr("disabled","disabled");
$("input#btnview_next").css("background","#fff");
$("input#btnview_pre").css("background","url(../App_Themes/darkOrange/images/related_left.gif) no-repeat center center");
$("input#btnview_pre").attr("disabled","");
}else if(Vmr==0){
$("input#btnview_pre").attr("disabled","disabled");
$("input#btnview_pre").css("background","#fff");
$("input#btnview_next").css("background","url(../App_Themes/darkOrange/images/related_right.gif) no-repeat center center");
$("input#btnview_next").attr("disabled","");
}else{
$("input#btnview_next").attr("disabled","");
$("input#btnview_next").css("background","url(../App_Themes/darkOrange/images/related_right.gif) no-repeat center center");
$("input#btnview_pre").css("background","url(../App_Themes/darkOrange/images/related_left.gif) no-repeat center center");
$("input#btnview_pre").attr("disabled","");
}
var pnum = $("#<%=dlCatalog.ClientID%> > span").size();
if( pnum < 5){
	$("#btnview_pre").css("background","none");
	$("#btnview_next").css("background","none");
	}
}

$(function(){
view_picList();   
})
</script>


<div class="recently-viewed-products">
    <%--<div class="page-title">
        <h1><%=GetLocaleResourceString("Products.RecentlyViewedProducts")%></h1>
    </div>
    <div class="clear">
    </div>--%>
     <input type="button" class="view_lf btn" onclick="view_picList('view_pre')" value="" id="btnview_pre"  />
    <div class="view_mainDiv view_lf" id="view_bigDiv">
    
    <div class="product-grid">
        <asp:DataList ID="dlCatalog" runat="server" RepeatColumns="0" RepeatDirection="Horizontal"
            RepeatLayout="Flow" ItemStyle-CssClass="item-box">
            <ItemTemplate>
                <nopCommerce:ProductBox1 ID="ctrlProductBox" Product='<%# Container.DataItem %>' runat="server" />
            </ItemTemplate>
        </asp:DataList>
    </div>
    
    </div>
    <input type="button" class="view_lf btn" onclick="view_picList('view_next')" value="" id="btnview_next" />
</div>
