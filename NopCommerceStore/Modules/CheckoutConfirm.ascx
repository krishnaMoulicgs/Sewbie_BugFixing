<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.CheckoutConfirmControl"
    CodeBehind="CheckoutConfirm.ascx.cs" %>
    <script type="text/javascript" >
        function showPaypalAUP() {
            window.open("https://cms.paypal.com/us/cgi-bin/marketingweb?cmd=_render-content&content_ID=ua/AcceptableUse_full&locale.x=en_US");
        }
    </script>
<div class="checkout-data">
    <div class="confirm-order">
    </div>
    <div class="error-block">
        <div class="message-error">
            <asp:Literal runat="server" ID="lConfirmOrderError" EnableViewState="false" />
        </div>
    </div>
    <div class="confirm-order">
        Note to Seller:
        <textarea id="txtNoteToSeller" runat="server" resize="none" rows="3" cols="4"></textarea>
    </div>
    <div class="confirm-order">
        You will be redirected to paypal to complete your order.  If you do not complete your order, you can see this order and all of your past orders in the my account link above.  Please follow paypal acceptable use policies.
        <a href="#" onclick="showPaypalAUP()">Paypal Acceptable Use Policy</a>
    </div>
    <div class="confirm-order select-button">
        <asp:Label runat="server" ID="lMinOrderTotalAmount" />
        <asp:Button runat="server" ID="btnNextStep" Text="<% $NopResources:Checkout.ConfirmButton %>"
            OnClick="btnNextStep_Click" CssClass="confirmordernextstepbutton" ValidationGroup="CheckoutConfirm" />
        <input type="hidden" name="expType" value="light"/>
    </div>
</div>
<input type="hidden" runat="server" id="hidPaypalURL" value=""/>
