<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.VendorOrderSummaryControl"
    CodeBehind="VendorOrderSummary.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="OrderTotals" Src="~/Modules/OrderTotals.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="CheckoutAttributes" Src="~/Modules/CheckoutAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductBox1" Src="~/Modules/ProductBox1.ascx" %>
<asp:Panel class="order-summary-content" runat="server" ID="pnlEmptyCart">
    <%=GetLocaleResourceString("ShoppingCart.CartIsEmpty")%>
</asp:Panel>
<asp:Panel class="order-summary-content" runat="server" ID="pnlCart">
    <asp:Panel runat="server" ID="pnlCommonWarnings" CssClass="warning-box" EnableViewState="false"
        Visible="false">
        <asp:Label runat="server" ID="lblCommonWarning" CssClass="warning-text" EnableViewState="false"
            Visible="false"></asp:Label>
    </asp:Panel>
    <%--We want to repeat this table.--%>
    <asp:Repeater ID="rptShopHeader" runat="server" 
        onitemdatabound="rptShopHeader_ItemDataBound" 
        onitemcommand="rptShopHeader_ItemCommand">
        <ItemTemplate>
            <table class="cart">
                <%if (IsShoppingCart)
                    { %>
                <col width="1" />
                <%} %>
                <%if (this.SettingManager.GetSettingValueBoolean("Display.Products.ShowSKU"))
                    {%>
                <col width="1" />
                <%} %>
                <%if (this.SettingManager.GetSettingValueBoolean("Display.ShowProductImagesOnShoppingCart"))
                    {%>
                <col width="1" />
                <%} %>
                <col/>
                <col width="1" />
                <col width="2" />
                <col width="1" />
                <tr>
                    <%--This should be the header information of the Vendor - Company--%> 
                    <td colspan="4" style="text-align:left;"><%# Eval("VendorName") %></td>
                </tr>
                <tr>                                                                                                                                                                                                                            <tr class="cart-header-row">
                    <%if (IsShoppingCart)
                        { %>
                    <td>
                        <%=GetLocaleResourceString("ShoppingCart.Remove")%>
                    </td>
                    <%} %>
                    <%if (this.SettingManager.GetSettingValueBoolean("Display.Products.ShowSKU"))
                        {%>
                    <td>
                        <%=GetLocaleResourceString("ShoppingCart.SKU")%>
                    </td>
                    <%} %>
                    <%if (this.SettingManager.GetSettingValueBoolean("Display.ShowProductImagesOnShoppingCart"))
                        {%>
                    <td class="picture">
                    </td>
                    <%} %>
                    <td>
                        <%=GetLocaleResourceString("ShoppingCart.Product(s)")%>
                    </td>
                    <td>
                        <%=GetLocaleResourceString("ShoppingCart.UnitPrice")%>
                    </td>
                    <td>
                        <%=GetLocaleResourceString("ShoppingCart.Quantity")%>
                    </td>
                    <td class="end">
                        <%=GetLocaleResourceString("ShoppingCart.ItemTotal")%>
                    </td>
                </tr>
                <tbody>
                    <asp:Repeater ID="rptShoppingCart" runat="server">
                        <ItemTemplate>
                            <tr class="cart-item-row">
                                <%if (IsShoppingCart)
                                    { %>
                                <td>
                                    <asp:CheckBox runat="server" ID="cbRemoveFromCart" />
                                </td>
                                <%} %>
                                <%if (this.SettingManager.GetSettingValueBoolean("Display.Products.ShowSKU"))
                                    {%>
                                <td style="white-space: nowrap;">
                                    <%#Server.HtmlEncode(((ShoppingCartItem)Container.DataItem).ProductVariant.SKU)%>
                                </td>
                                <%} %>
                                <%if (this.SettingManager.GetSettingValueBoolean("Display.ShowProductImagesOnShoppingCart"))
                                    {%>
                                <td class="productpicture">
                                    <asp:Image ID="iProductVariantPicture" runat="server" ImageUrl='<%#GetProductVariantImageUrl((ShoppingCartItem)Container.DataItem)%>'
                                        AlternateText="Product picture" />
                                </td>
                                <%} %>
                                <td class="product">
                                    <a href='<%#GetProductUrl((ShoppingCartItem)Container.DataItem)%>' title="View details">
                                        <%#Server.HtmlEncode(GetProductVariantName((ShoppingCartItem)Container.DataItem))%></a>
                                    <%#GetAttributeDescription((ShoppingCartItem)Container.DataItem)%>
                                    <%#GetRecurringDescription((ShoppingCartItem)Container.DataItem)%>
                                    <asp:Panel runat="server" ID="pnlWarnings" CssClass="warning-box" EnableViewState="false"
                                        Visible="false">
                                        <asp:Label runat="server" ID="lblWarning" CssClass="warning-text" EnableViewState="false"
                                            Visible="false"></asp:Label>
                                    </asp:Panel>
                                </td>
                                <td style="white-space: nowrap;">
                                    <%#GetShoppingCartItemUnitPriceString((ShoppingCartItem)Container.DataItem)%>
                                </td>
                                <td style="white-space: nowrap;">
                                    <%if (IsShoppingCart)
                                        { %>
                                    <asp:TextBox ID="txtQuantity" size="4" runat="server" Text='<%# Eval("Quantity") %>'
                                        SkinID="ShoppingCartQuantityText" />
                                    <%}
                                        else
                                        { %>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' CssClass="Label" />
                                    <%} %>
                                </td>
                                <td style="white-space: nowrap;" class="end">
                                    <%#GetShoppingCartItemSubTotalString((ShoppingCartItem)Container.DataItem)%>
                                    <asp:Label ID="lblShoppingCartItemId" runat="server" Visible="false" Text='<%# Eval("ShoppingCartItemId") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <%--
            <div>
                <Add the shopping cart controls here.  They will be repeated once per vendor.
                    <div class="checkout-buttons">
                        <asp:Button ID="btnUpdate" runat="server" Text="<% $NopResources:ShoppingCart.UpdateCart %>" CssClass="checkoutbutton" CommandName="UpdateCart" CommandArgument="" />
                        <asp:Button ID="btnCheckout" runat="server" Text="<% $NopResources:ShoppingCart.Checkout %>" CssClass="checkoutbutton" CommandName="CheckoutCart" CommandArgument=""/>
                        <div class="addon-buttons"><nopCommerce:GoogleCheckoutButton runat="server" ID="btnGoogleCheckoutButton"></nopCommerce:GoogleCheckoutButton></div>
                    </div> 
            </div>
            --%>
            <div id="ShowDetialFood">
                <nopCommerce:OrderTotals runat="server" ID="ctrlOrderTotals" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="clear">
    </div>
    <div class="selected-checkout-attributes">
        <%=GetCheckoutAttributeDescription()%>
    </div>
    <div class="clear">
    </div>
    <div class="cart-footer">
        <%--<%if (this.IsShoppingCart)
          { %>
        <div class="common-buttons">
            <asp:Button ID="btnContinueShopping" OnClick="btnContinueShopping_Click" runat="server"
                Text="<% $NopResources:ShoppingCart.ContinueShopping %>" CssClass="continueshoppingbutton" />
        </div>
        <div class="clear">
        </div>
        <asp:PlaceHolder runat="server" ID="phMinOrderSubtotalAmount">
            <div class="min-amount-warning">
                <asp:Label runat="server" ID="lMinOrderSubtotalAmount" />
            </div>
        </asp:PlaceHolder>
        <%} %>--%>
        <div class="clear">
        </div>
        <%if (this.IsShoppingCart)
          { %>
        <nopCommerce:CheckoutAttributes ID="ctrlCheckoutAttributes" runat="server" />
        <div class="clear">
        </div>
        <%} %>
        <div class="totals">
            <%if (this.IsShoppingCart)
              { %>
            <div class="clear">
            </div>
            <%if (this.SettingManager.GetSettingValueBoolean("Checkout.TermsOfServiceEnabled"))
              { %>
            <script language="javascript" type="text/javascript">
                function accepttermsofservice(msg) {
                    if (!document.getElementById('<%=cbTermsOfService.ClientID%>').checked) {
                        alert(msg);
                        return false;
                    }
                    else
                        return true;
                }
            </script>
            <div class="terms-of-service" style="display:none;">
                <asp:CheckBox runat="server" ID="cbTermsOfService" Checked="true" />
                <asp:Literal runat="server" ID="lTermsOfService" />
            </div>
            <%} %>
            <%--<div class="clear">
            </div>
            <div class="checkout-buttons">
                <asp:Button ID="btnCheckout" OnClick="btnCheckout_Click" runat="server" Text="<% $NopResources:ShoppingCart.Checkout %>"
                    CssClass="checkoutbutton" />
            </div>
            <div class="addon-buttons">
                <nopCommerce:GoogleCheckoutButton runat="server" ID="btnGoogleCheckoutButton"></nopCommerce:GoogleCheckoutButton>
            </div>--%>
            <%} %>
        </div>
         <div class="clear">
            </div>
        <div class="cart-collaterals">
            <%if (this.IsShoppingCart)
              { %>
            <div class="deals">
                <%if (this.SettingManager.GetSettingValueBoolean("Display.Checkout.DiscountCouponBox"))
                  { %>
                <asp:Panel runat="server" ID="phCoupon" CssClass="coupon-box">
                    <b>
                        <%=GetLocaleResourceString("ShoppingCart.DiscountCouponCode")%></b>
                    <br />
                    <%=GetLocaleResourceString("ShoppingCart.DiscountCouponCode.Tooltip")%>
                    <br />
                    <asp:TextBox ID="txtDiscountCouponCode" runat="server" Width="125px" />&nbsp;
                    <asp:Button runat="server" ID="btnApplyDiscountCouponCode" OnClick="btnApplyDiscountCouponCode_Click"
                        Text="<% $NopResources:ShoppingCart.ApplyDiscountCouponCodeButton %>" CssClass="applycouponcodebutton"
                        CausesValidation="false" />
                    <asp:Panel runat="server" ID="pnlDiscountWarnings" CssClass="warning-box" EnableViewState="false"
                        Visible="false">
                        <br />
                        <asp:Label runat="server" ID="lblDiscountWarning" CssClass="warning-text" EnableViewState="false"
                            Visible="false"></asp:Label>
                    </asp:Panel>
                </asp:Panel>
                <%} %>
                <%if (this.SettingManager.GetSettingValueBoolean("Display.Checkout.GiftCardBox"))
                  { %>
                <asp:Panel runat="server" ID="phGiftCards" CssClass="coupon-box">
                    <b>
                        <%=GetLocaleResourceString("ShoppingCart.GiftCards")%></b>
                    <br />
                    <%=GetLocaleResourceString("ShoppingCart.GiftCards.Tooltip")%>
                    <br />
                    <asp:TextBox ID="txtGiftCardCouponCode" runat="server" Width="125px" />&nbsp;
                    <asp:Button runat="server" ID="btnApplyGiftCardsCouponCode" OnClick="btnApplyGiftCardCouponCode_Click"
                        Text="<% $NopResources:ShoppingCart.ApplyGiftCardCouponCodeButton %>" CssClass="applycouponcodebutton"
                        CausesValidation="false" />
                    <asp:Panel runat="server" ID="pnlGiftCardWarnings" CssClass="warning-box" EnableViewState="false"
                        Visible="false">
                        <br />
                        <asp:Label runat="server" ID="lblGiftCardWarning" CssClass="warning-text" EnableViewState="false"
                            Visible="false"></asp:Label>
                    </asp:Panel>
                </asp:Panel>
                <%} %>
            </div>
            <%} %>
        </div>
        <div class="clear">
        </div>
        <%if (this.IsShoppingCart)
          { %>
        <div class="product-grid">
            <asp:DataList ID="dlCrossSells" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                RepeatLayout="Table" ItemStyle-CssClass="item-box">
                <HeaderTemplate>
                    <span class="crosssells-title">
                        <%=GetLocaleResourceString("ShoppingCart.CrossSells")%></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <nopCommerce:ProductBox1 ID="ctrlProductBox" Product='<%# Container.DataItem %>'
                        runat="server" RedirectCartAfterAddingProduct="True" />
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="clear">
        </div>
        <%} %>
    </div>
</asp:Panel>
