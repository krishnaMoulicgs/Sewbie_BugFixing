<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.RecentlyAddedProductsControl" CodeBehind="RecentlyAddedProducts.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductBox1" Src="~/Modules/ProductBox1.ascx" %>
<div class="recently-added-products">
<%--<div class="page-title">
        <table width="100%">
            <tr>
                <td style="text-align: left;">
                    <h1>
                        <%=GetLocaleResourceString("Products.NewProducts")%></h1>
                </td>
                <td style="text-align: right;">
                    <a href="<%=Page.ResolveUrl("~/recentlyaddedproductsrss.aspx")%>">
                        <asp:Image ID="imgRSS" runat="server" ImageUrl="~/images/icon_rss.gif" ToolTip="<% $NopResources:RecentlyAddedProductsRSS.Tooltip %>"
                            AlternateText="RSS" /></a>
                </td>
            </tr>
        </table>
    </div>--%>
<div class="blk_18">
<div id="ISL_Cont_31" class="pcont">
<div class="ScrCont">
<div id="List1_31">
<div class="product-grid">
    <asp:DataList ID="dlCatalog" runat="server" RepeatColumns="0" RepeatDirection="Horizontal"
        RepeatLayout="Flow" ItemStyle-CssClass="item-box">    
        <ItemTemplate>
            <nopCommerce:ProductBox1 ID="ctrlProductBox" Product='<%# Container.DataItem %>' runat="server" />
        </ItemTemplate>
    </asp:DataList> 
</div>
</div>
<div id="List2_31"></div>
<div id="List2_312"></div>
</div>
</div>
</div>
<div class="clear"></div>
<div class="homepage_buttons">
<a onmouseup="ISL_StopUp_31()" class="LeftBotton" 
onmouseout="ISL_StopUp_31()" onmousedown="ISL_GoUp_31()" href="javascript:void(0);" 
target="_self"></a>
<a onmouseup="ISL_StopDown_31()" 
class="RightBotton" onmouseout="ISL_StopDown_31()" onmousedown="ISL_GoDown_31()" href="javascript:void(0);" target="_self"></a>
</div>
<div class="clear"></div>
<script type=text/javascript>
<!--
picrun_ini3()
//-->
</script>
  
</div>                                 
