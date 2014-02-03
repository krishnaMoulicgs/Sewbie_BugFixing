<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"
	Inherits="NopSolutions.NopCommerce.Web.CheckoutCompletedPage" CodeBehind="CheckoutCompleted.aspx.cs"
	 %>
<%@ Register TagPrefix="nopCommerce" TagName="CheckoutCompleted" Src="~/Modules/CheckoutCompleted.ascx" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <script type="text/javascript" charset="utf-8">
        function handleEmbeddedFlow() {
            if (top && top.opener && top.opener.top) {
                top.opener.top.myEmbeddedPaymentFlow.paymentSuccess();
                window.close();
            } else if (top.myEmbeddedPaymentFlow) {
                top.myEmbeddedPaymentFlow.paymentSuccess();
            } else {
                alert('Please close the window and reload to continue');
            }
        }
    </script>
	<div class="checkout-page" style="text-align:center;">
        <img alt="progress confirm" src="images/completeHeader.jpg"/>
		<div class="page-title">
			<h1><%=GetLocaleResourceString("Checkout.ThankYou")%></h1>
		</div>
		<div class="clear">
		</div>
		<nopCommerce:CheckoutCompleted ID="ctrlCheckoutCompleted" runat="server" />
	</div>
</asp:Content>
