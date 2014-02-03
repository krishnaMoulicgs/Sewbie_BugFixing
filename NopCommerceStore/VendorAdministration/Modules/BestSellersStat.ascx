<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.BestSellersStatControl"
    CodeBehind="BestSellersStat.ascx.cs" %>
<div class="statisticsTitle">
    <%=GetLocaleResourceString("VendorAdmin.BestSellersStat.BestSellers")%>
</div>
<asp:GridView ID="gvBestSellers" runat="server" AutoGenerateColumns="False" Width="100%">
    <Columns>
        <asp:TemplateField HeaderText="<% $NopResources:VendorAdmin.BestSellersStat.Product%>" ItemStyle-Width="65%">
            <ItemTemplate>
                <div style="padding-left: 10px; padding-right: 10px; text-align: left;">
                    <%--<a href='<%#GetProductVariantUrl(Convert.ToInt32(Eval("ProductVariantId")))%>' title="View product details">
                        <%#Server.HtmlEncode(GetProductName(Convert.ToInt32(Eval("ProductVariantId"))))%></a>--%>
                        <span><%#Server.HtmlEncode(GetProductVariantName(Convert.ToInt32(Eval("ProductVariantId"))))%></span>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="SalesTotalCount" HeaderText="<% $NopResources:VendorAdmin.BestSellersStat.TotalCount %>" HeaderStyle-HorizontalAlign="Center"
            ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:TemplateField HeaderText="<% $NopResources:VendorAdmin.BestSellersStat.TotalAmount %>" ItemStyle-Width="20%">
            <ItemTemplate>
                <%#Server.HtmlEncode(PriceHelper.FormatPrice(Convert.ToDecimal(Eval("SalesTotalAmount")), true, false))%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
