<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true"
	Inherits="NopSolutions.NopCommerce.Web.PayPalAPPaymentRequestFailurePage" CodeBehind="PaypalAPPaymentRequestFailure.aspx.cs"
	 %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
	<div class="checkout-page">
		<div class="page-title">
			<h1>There is an error processing your payment.</h1>
		</div>
		<div class="clear">
		</div>
        Your payment was not approved by Paypal.  This could be an issue with the Seller's account or with Paypal's system.  Please contact the seller directly to complete the order.
	</div>
	<div id="wrapper">
	<div id="header">
	</div>
</div>    

</asp:Content>
