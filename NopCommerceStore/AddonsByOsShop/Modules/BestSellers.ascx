<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.BestSellersControl"
    CodeBehind="BestSellers.ascx.cs" %>
 
<div class="bestsellers">
<%--    <div class="boxtitle">
        <%=GetLocaleResourceString("Reports.BestSellingProducts")%>
    </div>
    <div class="clear">
    </div>--%>
<div class="blk_18">
<div id="ISL_Cont_21" class="pcont">
<div class="ScrCont">
<div id="List1_21">
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
<div id="List2_21"></div>
<div id="List2_212"></div>
</div>
</div>
</div>
<div class="clear"></div>
<div class="homepage_buttons">
<A onmouseup=ISL_StopUp_21() class=LeftBotton 
onmouseout=ISL_StopUp_21() onmousedown=ISL_GoUp_21() href="javascript:void(0);" 
target=_self></A>
<A onmouseup=ISL_StopDown_21() 
class=RightBotton onmouseout=ISL_StopDown_21() onmousedown=ISL_GoDown_21() 
href="javascript:void(0);" target=_self></A>
</div>
<div class="clear"></div>
<SCRIPT type=text/javascript>
<!--
picrun_ini2()
//-->
</SCRIPT>
  
</div>                                 


