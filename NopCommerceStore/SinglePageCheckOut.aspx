<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThreeColumnCheckOut.master" AutoEventWireup="true" CodeBehind="SinglePageCheckOut.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.SinglePageCheckOut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="EmailTextBox" Src="~/Modules/EmailTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="OrderTotals" Src="~/Modules/OrderTotals.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="CheckoutAttributes" Src="~/Modules/CheckoutAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductBox1" Src="~/Modules/ProductBox1.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="cphHead" runat="server">
	<script type="text/javascript" src="Scripts/jsrender.js"></script>
    <script type="text/javascript" src="Scripts/libs/json2.js"></script>
	<script type="text/javascript" src="Scripts/SinglePageCheckout.js"></script>
	<link rel="stylesheet" href="css/dialog.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
	<!-- Shipping Address Display and Edit -->
	<div class="checkout-data">
		<table>
			<tr>
				<td>
					<div class="checkout-data">
						<asp:HiddenField ID="hdnShippingAddressId" runat="server" />
						<!-- shipping Address Display  -->
						<asp:Panel runat="server" ID="pnlSelectShippingAddress">
							<div class="checkout-data">
								<div class="select-address-title">
									Shipping Address <a href="#" class="linkButton" onclick="shippingAddressChange();">Change</a>
								</div>
								<div class="clear">
								</div>
								<div class="address-grid">
									<div class="address-item">
										<div class="address-box">
											<asp:DataList ID="dlShippingAddresses" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
												RepeatLayout="Table" ItemStyle-CssClass="item-box">
												<ItemTemplate>
													<table width="100%" cellspacing="0" cellpadding="2" border="0">
														<tbody>
															<tr>
																<td style="vertical-align: middle; padding-left: 10px;">
																	<b>
																		<asp:Literal ID="lFullName" Text='<%#(getFullname((Address)Container.DataItem)) %>'
																			runat="server"></asp:Literal></b>
																</td>
																<td align="right" style="padding: 5px 5px 5px 0px;">
																	&nbsp;
																</td>
															</tr>
															<tr>
																<td colspan="2">
																	<table cellspacing="0" cellpadding="2" border="0">
																		<tbody>
																			<tr>
																				<td width="10">
																					<img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>" />
																				</td>
																				<td>
																					<div>
																						<asp:Literal ID="lAddress1" Text='<%# ((Address)Container.DataItem).Address1 %>'
																							runat="server"></asp:Literal></div>
																					<asp:Panel ID="pnlAddress2" runat="server">
																						<asp:Literal ID="lAddress2" Text='<%# ((Address)Container.DataItem).Address2 %>'
																							runat="server"></asp:Literal></asp:Panel>
																					<div>
																						<asp:Literal ID="lCity" Text='<%# ((Address)Container.DataItem).City %>' runat="server"></asp:Literal>,
																						<asp:Literal ID="lStateProvince" Text='<%# (getStateProvinceIfnull( (Address)Container.DataItem)) %>'
																							runat="server"></asp:Literal>
																						<asp:Literal ID="lZipPostalCode" Text='<%# ((Address)Container.DataItem).ZipPostalCode %>'
																							runat="server"></asp:Literal>
																					</div>
																				</td>
																				<td width="10">
																					<img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>" />
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</ItemTemplate>
											</asp:DataList>
										</div>
									</div>
								</div>
							</div>
						</asp:Panel>
						<table width="100%" cellspacing="0" cellpadding="2" border="0">
							<tbody>
								<tr>
									<td style="vertical-align: middle; padding-left: 5px;">
										&nbsp;
									</td>
								</tr>
							</tbody>
						</table>
					</div>
				</td>
				<td valign="top" style="vertical-align: top;">
					<div class="checkout-data" style="vertical-align: top">
                        <asp:HiddenField ID="hdnBillingAddressId" runat="server" />
						<!-- Billing Address Display  -->
						<asp:Panel runat="server" ID="pnlSelectBillingAddress">
							<div class="checkout-data">
								<div class="select-address-title">
									Billing Address <a id="lnkBillingAddress" href="#" class="linkButton" onclick="billingAddressChange();">
										Change</a>
								</div>
								<div class="clear">
								</div>
								<div class="address-grid">
									<div class="address-item">
										<div class="address-box">
											<asp:DataList ID="dlBillingAddresses" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
												RepeatLayout="Table" ItemStyle-CssClass="item-box">
												<ItemTemplate>
													<table width="100%" cellspacing="0" cellpadding="2" border="0">
														<tbody>
															<tr>
																<td style="vertical-align: middle; padding-left: 10px;">
																	<b>
																		<asp:Literal ID="lFullName" Text='<%# (getFullname( (Address)Container.DataItem)) %> '
																			runat="server"></asp:Literal></b>
																</td>
																<td align="right" style="padding: 5px 5px 5px 0px;">
																	&nbsp;
																</td>
															</tr>
															<tr>
																<td colspan="2">
																	<table cellspacing="0" cellpadding="2" border="0">
																		<tbody>
																			<tr>
																				<td width="10">
																					<img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>"/>
																				</td>
																				<td>
																					<asp:Literal ID="lAddress1" Text='<%# ((Address)Container.DataItem).Address1 %>'
																						runat="server"></asp:Literal>
																					<asp:Panel ID="pnlAddress2" runat="server">
																						<asp:Literal ID="lAddress2" Text='<%# ((Address)Container.DataItem).Address2 %>'
																							runat="server"></asp:Literal>
																					</asp:Panel>
																					<div>
																						<asp:Literal ID="lCity" Text='<%# ((Address)Container.DataItem).City %>' runat="server"></asp:Literal>,
																						<asp:Literal ID="lStateProvince" Text='<%# (getStateProvinceIfnull( (Address)Container.DataItem)) %>'
																							runat="server"></asp:Literal>
																						<asp:Literal ID="lZipPostalCode" Text='<%# ((Address)Container.DataItem).ZipPostalCode %>'
																							runat="server"></asp:Literal>
																					</div>
																				</td>
																				<td width="10">
																					<img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>" />
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</td>
															</tr>
														</tbody>
													</table>
												</ItemTemplate>
											</asp:DataList>
										</div>
									</div>
								</div>
							</div>
						</asp:Panel>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBottom1" Visible="false" runat="server">
	<asp:Panel runat="server" ID="pnladdressEdit" Visible="false">
		<div class="checkout-data">
			<table width="100%">
				<tr>
					<td>
						<div class="checkout-data">
							<!-- Shipping Address  Edit -->
							<div class="enter-address-title">
								<asp:Label runat="server" ID="lEnterShippingAddress"></asp:Label>
							</div>
							<div class="clear">
							</div>
							<div class="enter-address">
								<div class="enter-address-body">
									<table>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.FirstName")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtFirstName" ErrorMessage="<% $NopResources:Address.FirstNameIsRequired %>">
												</nopCommerce:SimpleTextBox>
												<asp:Label ID="lblShippingAddressId" runat="server" Visible="false"></asp:Label>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.LastName")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtLastName" ErrorMessage="<% $NopResources:Address.LastNameIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.PhoneNumber")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtPhoneNumber" ErrorMessage="<% $NopResources:Address.PhoneNumberIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Email")%>:
											</td>
											<td>
												<nopCommerce:EmailTextBox runat="server" ID="txtEmail"></nopCommerce:EmailTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.FaxNumber")%>:
											</td>
											<td>
												<asp:TextBox ID="txtFaxNumber" runat="server"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Company")%>:
											</td>
											<td>
												<asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Address1")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtAddress1" ErrorMessage="<% $NopResources:Address.StreetAddressIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Address2")%>:
											</td>
											<td>
												<asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.City")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtCity" ErrorMessage="<% $NopResources:Address.CityIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Country")%>:
											</td>
											<td>
												<asp:DropDownList ID="ddlCountry" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
													Width="137px">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.StateProvince")%>:
											</td>
											<td>
												<asp:DropDownList ID="ddlStateProvince" AutoPostBack="False" runat="server" Width="137px">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.ZipPostalCode")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtZipPostalCode" ErrorMessage="<% $NopResources:Address.ZipPostalCodeIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
									</table>
								</div>
								<div class="clear">
								</div>
								<div class="button">
									<asp:Button runat="server" CausesValidation="false" ID="btnSaveShippingAddress" Text="Save and Ship to this address"
										CssClass="newaddressnextstepbutton" OnClick="btnSaveShippingAddress_Click" />
								</div>
							</div>
						</div>
					</td>
					<td>
						<div class="checkout-data">
							<!-- Billing Address Display and Edit -->
							<div class="enter-address-title">
								<asp:Label runat="server" ID="lEnterBillingAddress"></asp:Label>
							</div>
							<div class="clear">
							</div>
							<div class="enter-address">
								<div runat="server" id="pnlTheSameAsShippingAddress" class="the-same-address">
									<asp:Button runat="server" ID="btnTheSameAsShippingAddress" Text="Use the same as Shipping address"
										CausesValidation="false" OnClick="btnTheSameAsShippingAddress_Click" CssClass="sameasshippingaddressbutton" />
								</div>
								<div class="enter-address-body">
									<!-- Billing Address  Edit -->
									<table>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.FirstName")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtBillFirstName" ErrorMessage="<% $NopResources:Address.FirstNameIsRequired %>">
												</nopCommerce:SimpleTextBox>
												<asp:Label ID="lblBillShippingAddressId" runat="server" Visible="false"></asp:Label>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.LastName")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtBillLastName" ErrorMessage="<% $NopResources:Address.LastNameIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.PhoneNumber")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtBillPhoneNumber" ErrorMessage="<% $NopResources:Address.PhoneNumberIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Email")%>:
											</td>
											<td>
												<nopCommerce:EmailTextBox runat="server" ID="txtBillEmail"></nopCommerce:EmailTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.FaxNumber")%>:
											</td>
											<td>
												<asp:TextBox ID="txtBillFaxNumber" runat="server"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Company")%>:
											</td>
											<td>
												<asp:TextBox ID="txtBillCompany" runat="server"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Address1")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtBillAddress1" ErrorMessage="<% $NopResources:Address.StreetAddressIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Address2")%>:
											</td>
											<td>
												<asp:TextBox ID="txtBillAddress2" runat="server"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.City")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtBillCity" ErrorMessage="<% $NopResources:Address.CityIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.Country")%>:
											</td>
											<td>
												<asp:DropDownList ID="ddlBillCountry" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlBillCountry_SelectedIndexChanged"
													Width="137px">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.StateProvince")%>:
											</td>
											<td>
												<asp:DropDownList ID="ddlBillStateProvince" AutoPostBack="False" runat="server" Width="137px">
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td>
												<%=GetLocaleResourceString("Address.ZipPostalCode")%>:
											</td>
											<td>
												<nopCommerce:SimpleTextBox runat="server" ID="txtBillZipPostalCode" ErrorMessage="<% $NopResources:Address.ZipPostalCodeIsRequired %>">
												</nopCommerce:SimpleTextBox>
											</td>
										</tr>
									</table>
								</div>
								<div class="clear">
								</div>
								<div class="button">
									<asp:Button runat="server" CausesValidation="false" ID="btnSaveBillingAddress" Text="Save and Bill to this address"
										CssClass="newaddressnextstepbutton" OnClick="btnSaveBillingAddress_Click" />
								</div>
							</div>
						</div>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBottom" runat="server">
	<div class="checkout-data">
		<!-- Order Summary -->
		<asp:Panel runat="server" ID="pnlConfirmContent" class="stepcontent">
			<div class="order-summary-title">
				&nbsp;&nbsp;&nbsp;<%=GetLocaleResourceString("Checkout.OrderSummary")%></div>
			<div class="clear">
			</div>
			<div class="order-summary-body">
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
					<asp:Repeater ID="rptShopHeader" runat="server" OnItemDataBound="rptShopHeader_ItemDataBound"
						OnItemCommand="rptShopHeader_ItemCommand">
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
								<col />
								<col width="1" />
								<col width="2" />
								<col width="1" />
								<tr>
									<%--This should be the header information of the Vendor - Company--%>
									<td colspan="4" style="text-align: left;">
										<%# Eval("VendorName") %>
									</td>
								</tr>
								<tr>
									<tr class="cart-header-row">
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
							<div class="terms-of-service" style="display: none;">
								<asp:CheckBox runat="server" ID="cbTermsOfService" Checked="true" />
								<asp:Literal runat="server" ID="lTermsOfService" />
							</div>
							<%} %>
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
									<asp:Button runat="server" ID="btnApplyDiscountCouponCode" Text="<% $NopResources:ShoppingCart.ApplyDiscountCouponCodeButton %>"
										CssClass="applycouponcodebutton" CausesValidation="false" />
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
									<asp:Button runat="server" ID="btnApplyGiftCardsCouponCode" Text="<% $NopResources:ShoppingCart.ApplyGiftCardCouponCodeButton %>"
										CssClass="applycouponcodebutton" CausesValidation="false" />
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
			</div>
			<div class="clear">
			</div>
		</asp:Panel>
	</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="chp2" runat="server">
	<table style="margin-left: -70px">
		<tr>
			<td>
				<!-- Seller note and Confirm CheckOut -->
				<div class="checkout-data">
					<div class="error-block">
						<div class="message-error">
							<asp:Literal runat="server" ID="lConfirmOrderError" EnableViewState="false" />
						</div>
					</div>
					
                    <div class="select-address-title">Note to Seller:</div>
                    <div class="confirm-order">
						<textarea id="txtNoteToSeller" runat="server" resize="none" rows="6" cols="15"></textarea>
					</div>
					<input type="hidden" runat="server" id="hidPaypalURL" value="" />
				</div>
			</td>
			<td align="center" style="vertical-align: top;">
				<!---OrderTotal----->
				<div class="select-address-title">
					Order Total</div>
				<div class="clear">
				</div>
				<div class="checkout-data">
					<div class="checkout-data" style="border: 1px solid lightgray;">
						<table class="cart-total">
							<tbody>
								<tr>
									<td class="cart_total_left">
										<strong><span style="white-space: nowrap;">
											<%=GetLocaleResourceString("ShoppingCart.Sub-Total")%>:</span></strong>
									</td>
									<td class="cart_total_right">
										<span style="white-space: nowrap;">
											<asp:Label ID="lblSubTotalAmount" runat="server" CssClass="productPrice" />
										</span>
									</td>
								</tr>
								<asp:PlaceHolder runat="server" ID="phOrderSubTotalDiscount" Visible="false">
									<tr>
										<td class="cart_total_left">
											<strong><span style="white-space: nowrap;">
												<%=GetLocaleResourceString("ShoppingCart.OrderDiscount")%><asp:LinkButton runat="server"
													ID="btnRemoveOrderSubTotalDiscount" Text="" CommandName="remove" OnCommand="btnRemoveOrderSubTotalDiscount_Command"
													CssClass="removediscountbutton" />: </span></strong>
										</td>
										<td class="cart_total_right">
											<span style="white-space: nowrap;">
												<asp:Label ID="lblOrderSubTotalDiscountAmount" runat="server" CssClass="productPrice" />
											</span>
										</td>
									</tr>
								</asp:PlaceHolder>
								<tr>
									<td class="cart_total_left">
										<strong><span style="white-space: nowrap;">
											<%=GetLocaleResourceString("ShoppingCart.Shipping")%>: </span></strong>
									</td>
									<td class="cart_total_right">
										<span style="white-space: nowrap;">
											<asp:Label ID="lblShippingAmount" runat="server" CssClass="productPrice" />
										</span>
									</td>
								</tr>
								<asp:PlaceHolder runat="server" ID="phPaymentMethodAdditionalFee">
									<tr>
										<td class="cart_total_left">
											<strong><span style="white-space: nowrap;">
												<%=GetLocaleResourceString("ShoppingCart.PaymentMethodAdditionalFee")%>: </span>
											</strong>
										</td>
										<td class="cart_total_right">
											<span style="white-space: nowrap;">
												<asp:Label ID="lblPaymentMethodAdditionalFee" runat="server" CssClass="productPrice" />
											</span>
										</td>
									</tr>
								</asp:PlaceHolder>
								<asp:Repeater runat="server" ID="rptrTaxRates" OnItemDataBound="rptrTaxRates_ItemDataBound">
									<ItemTemplate>
										<tr>
											<td class="cart_total_left">
												<strong><span style="white-space: nowrap;">
													<asp:Literal runat="server" ID="lTaxRateTitle"></asp:Literal>: </span></strong>
											</td>
											<td class="cart_total_right">
												<span style="white-space: nowrap;">
													<asp:Literal runat="server" ID="lTaxRateValue"></asp:Literal>
												</span>
											</td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
								<asp:PlaceHolder runat="server" ID="phTaxTotal">
									<tr>
										<td class="cart_total_left">
											<strong><span style="white-space: nowrap;">
												<%=GetLocaleResourceString("ShoppingCart.Tax")%>: </span></strong>
										</td>
										<td class="cart_total_right">
											<span style="white-space: nowrap;">
												<asp:Label ID="lblTaxAmount" runat="server" CssClass="productPrice" />
											</span>
										</td>
									</tr>
								</asp:PlaceHolder>
								<asp:PlaceHolder runat="server" ID="phOrderTotalDiscount" Visible="false">
									<tr>
										<td class="cart_total_left">
											<strong><span style="white-space: nowrap;">
												<%=GetLocaleResourceString("ShoppingCart.OrderDiscount")%><asp:LinkButton runat="server"
													ID="btnRemoveOrderTotalDiscount" Text="" CommandName="remove" OnCommand="btnRemoveOrderTotalDiscount_Command"
													CssClass="removediscountbutton" />:</span></strong>
										</td>
										<td class="cart_total_right">
											<span style="white-space: nowrap;">
												<asp:Label ID="lblOrderTotalDiscountAmount" runat="server" CssClass="productPrice" />
											</span>
										</td>
									</tr>
								</asp:PlaceHolder>
								<asp:Repeater runat="server" ID="rptrGiftCards" OnItemDataBound="rptrGiftCards_ItemDataBound"
									Visible="false" OnItemCommand="rptrGiftCards_ItemCommand">
									<ItemTemplate>
										<tr>
											<td class="cart_total_left">
												<strong><span style="white-space: nowrap;">
													<asp:Literal runat="server" ID="lGiftCard"></asp:Literal><asp:LinkButton runat="server"
														ID="btnRemoveGC" Text="" CommandName="remove" CommandArgument='<%# Eval("GiftCardId")%>'
														CssClass="removegiftcardbutton" />:</span></strong>
											</td>
											<td class="cart_total_right">
												<span style="white-space: nowrap;">
													<asp:Label ID="lblGiftCardAmount" runat="server" CssClass="productPrice" />
												</span>
											</td>
										</tr>
										<tr>
											<td class="cart_total_left_below">
												<span style="white-space: nowrap;">
													<asp:Literal runat="server" ID="lGiftCardRemaining"></asp:Literal></span>
											</td>
											<td>
											</td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
								<asp:PlaceHolder runat="server" ID="phRewardPoints">
									<tr>
										<td class="cart_total_left">
											<strong><span style="white-space: nowrap;">
												<asp:Literal runat="server" ID="lRewardPointsTitle"></asp:Literal>:</span></strong>
										</td>
										<td class="cart_total_right">
											<span style="white-space: nowrap;">
												<asp:Label ID="lblRewardPointsAmount" runat="server" CssClass="productPrice" />
											</span>
										</td>
									</tr>
								</asp:PlaceHolder>
								<tr>
									<td class="cart_total_left">
										<strong><span style="white-space: nowrap;">
											<%=GetLocaleResourceString("ShoppingCart.OrderTotal")%>:</span></strong>
									</td>
									<td class="cart_total_right">
										<span style="white-space: nowrap;">
											<asp:Label ID="lblTotalAmount" runat="server" CssClass="productPrice" />
										</span>
									</td>
								</tr>
							</tbody>
						</table>
					</div>
					<div class="clear" style="height: 5px;">
					</div>
					<div style="vertical-align: middle;" class="checkoutstep">
						<asp:Label runat="server" ID="lMinOrderTotalAmount" /><%--
						<asp:Button runat="server" ID="btnNextStep" Text="<% $NopResources:Checkout.CheckoutWithPaypalButton %>"
							OnClick="btnNextStep_Click" CssClass="editaddressbutton" ValidationGroup="CheckoutConfirm" />--%>
                        <a href="#" runat="server" onserverclick="btnNextStep_Click">
                            <img src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" alt="paypal checkout logo"/>
                        </a>
						<input type="hidden" name="expType" value="light" />
					</div>
				</div>
			</td>
		</tr>
	</table>
	<input type="hidden" runat="server" id="hdnCustomerId" value="0" />
	<input type="hidden" runat="server" id="hdnShippingAddressFlag" value="0" />
	<input type="hidden" runat="server" id="hdnBillingAddressFlag" value="0" />
	<div id="shippingAddressDiv" class="" style="display: none;">
		<span>Select an address or Enter a new one in the form below.</span>
		<div id="divShippingSelect" class="address-select">
			<span><strong>Select an address:</strong></span>
			<br />
			<%--Shipping Template to be inserted here.--%>
			<span>No existing addresses found. Please enter a new address.</span>
		</div>
		<span id="newShipping"></span>
		<div id="newShippingAddressForm" style="text-align: left;" class="address-form">
			<table>
				<tr>
					<td>
						<span class="address-first-name-label">First Name:</span>
					</td>
					<td>
						<span class="address-last-name-label">Last Name:</span>
					</td>
				</tr>
				<tr>
					<td>
						<input type="text" class="address-first-name address-field" />
                        <input type="hidden" class="address-first-name-hidden" value="" />
					</td>
					<td>
						<input type="text" class="address-last-name address-field" />
                        <input type="hidden" class="address-last-name-hidden" value="" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<span class="address-one-label">Address 1:</span>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<input type="text" class="address-one address-field" />
                        <input type="hidden" class="address-one-hidden" value="" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<span class="address-two-label">Address 2:</span>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<input type="text" class="address-two address-field" />
                        <input type="hidden" class="address-two-hidden" value="" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<table>
							<tr>
								<td>
									<span class="address-city-label">City:</span>
								</td>
								<td>
									<span class="address-state-label">State:</span>
								</td>
								<td>
									<span class="address-zip-label">Zip:</span>
								</td>
							</tr>
							<tr>
								<td>
									<input type="text" class="address-city address-field" />
                                    <input type="hidden" class="address-city-hidden" value="" />
								</td>
								<td>
									<select class="address-state address-field">
										<option value="1">AL</option>
										<option value="2">AK</option>
										<option value="3">AS</option>
										<option value="4">AA</option>
										<option value="5">AE</option>
										<option value="6">AP</option>
										<option value="7">AZ</option>
										<option value="8">AR</option>
										<option value="9">CA</option>
										<option value="10">CO</option>
										<option value="11">CT</option>
										<option value="12">DE</option>
										<option value="13">DC</option>
										<option value="14">FM</option>
										<option value="15">FL</option>
										<option value="16">GA</option>
										<option value="17">GU</option>
										<option value="18">HI</option>
										<option value="19">ID</option>
										<option value="20">IL</option>
										<option value="21">IN</option>
										<option value="22">IA</option>
										<option value="23">KS</option>
										<option value="24">KY</option>
										<option value="25">LA</option>
										<option value="26">ME</option>
										<option value="27">MH</option>
										<option value="28">MD</option>
										<option value="29">MA</option>
										<option value="30">MI</option>
										<option value="31">MN</option>
										<option value="32">MS</option>
										<option value="33">MO</option>
										<option value="34">MT</option>
										<option value="35">MP</option>
										<option value="36">NE</option>
										<option value="42">NC</option>
										<option value="43">ND</option>
										<option value="38">NH</option>
										<option value="39">NJ</option>
										<option value="40">NM</option>
										<option value="37">NV</option>
										<option value="41">NY</option>
										<option value="44">OH</option>
										<option value="45">OK</option>
										<option value="46">OR</option>
										<option value="47">PW</option>
										<option value="48">PA</option>
										<option value="49">PR</option>
										<option value="50">RI</option>
										<option value="51">SC</option>
										<option value="52">SD</option>
										<option value="53">TN</option>
										<option value="54">TX</option>
										<option value="55">UT</option>
										<option value="56">VT</option>
										<option value="57">VI</option>
										<option value="58">VA</option>
										<option value="59">WA</option>
										<option value="60">WV</option>
										<option value="61">WI</option>
										<option value="62">WY</option>
									</select>
                                    <input type="hidden" class="address-state-hidden" value="" />
								</td>
								<td>
									<input type="text" class="address-zip address-field" maxlength="10" />
                                    <input type="hidden" class="address-zip-hidden" value="" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div id="billingAddressDiv">
        <div style="padding-bottom:5px;">
            <a href="#" onclick="UseShippingAsBilling();" style="text-decoration:underline;">Use the Shipping Address</a>
		    <span>, select a previously used address, or enter a new one in the form below.</span>
        </div>
		<div id="divBillingSelect" class="address-select">
			<%--Shipping Template to be inserted here.--%>
			<span>No existing addresses found. Please enter a new address.</span>
		</div>
		<span id="newBilling"></span>
		<div id="newBillingAddressForm" class="address-form" style="text-align: left;">
			<table>
				<tr>
					<td>
						<span class="address-first-name-label">First Name:</span>
					</td>
					<td>
						<span class="address-last-name-label">Last Name:</span>
					</td>
				</tr>
				<tr>
					<td>
						<input type="text" class="address-first-name address-field" />
					</td>
					<td>
						<input type="text" class="address-last-name address-field" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<span class="address-one-label">Address 1:</span>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<input type="text" class="address-one address-field" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<span class="address-two-label">Address 2:</span>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<input type="text" class="address-two address-field" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<table>
							<tr>
								<td>
									<span class="address-city-label">City:</span>
								</td>
								<td>
									<span class="address-state-label">State:</span>
								</td>
								<td>
									<span class="address-zip-label">Zip:</span>
								</td>
							</tr>
							<tr>
								<td>
									<input type="text" class="address-city address-field" />
								</td>
								<td>
									<select class="address-state address-field">
										<option value="1">AL</option>
										<option value="2">AK</option>
										<option value="3">AS</option>
										<option value="4">AA</option>
										<option value="5">AE</option>
										<option value="6">AP</option>
										<option value="7">AZ</option>
										<option value="8">AR</option>
										<option value="9">CA</option>
										<option value="10">CO</option>
										<option value="11">CT</option>
										<option value="12">DE</option>
										<option value="13">DC</option>
										<option value="14">FM</option>
										<option value="15">FL</option>
										<option value="16">GA</option>
										<option value="17">GU</option>
										<option value="18">HI</option>
										<option value="19">ID</option>
										<option value="20">IL</option>
										<option value="21">IN</option>
										<option value="22">IA</option>
										<option value="23">KS</option>
										<option value="24">KY</option>
										<option value="25">LA</option>
										<option value="26">ME</option>
										<option value="27">MH</option>
										<option value="28">MD</option>
										<option value="29">MA</option>
										<option value="30">MI</option>
										<option value="31">MN</option>
										<option value="32">MS</option>
										<option value="33">MO</option>
										<option value="34">MT</option>
										<option value="35">MP</option>
										<option value="36">NE</option>
										<option value="37">NV</option>
										<option value="38">NH</option>
										<option value="39">NJ</option>
										<option value="40">NM</option>
										<option value="41">NY</option>
										<option value="42">NC</option>
										<option value="43">ND</option>
										<option value="44">OH</option>
										<option value="45">OK</option>
										<option value="46">OR</option>
										<option value="47">PW</option>
										<option value="48">PA</option>
										<option value="49">PR</option>
										<option value="50">RI</option>
										<option value="51">SC</option>
										<option value="52">SD</option>
										<option value="53">TN</option>
										<option value="54">TX</option>
										<option value="55">UT</option>
										<option value="56">VT</option>
										<option value="57">VI</option>
										<option value="58">VA</option>
										<option value="59">WA</option>
										<option value="60">WV</option>
										<option value="61">WI</option>
										<option value="62">WY</option>
									</select>
								</td>
								<td>
									<input type="text" class="address-zip address-field" maxlength="10" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</div>
    <div class="confirm-order" style="display:none;">
		You will be redirected to paypal to complete your order. If you do not complete
		your order, you can see this order and all of your past orders in the my account
		link above. Please follow paypal acceptable use policies. <a href="#" onclick="showPaypalAUP()">
			Paypal Acceptable Use Policy</a>
	</div>
	<div id="address-dialog-succcess" title="Success" style="display: none;">
		<p>
			<span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
			Address Saved!</p>
	</div>
	<div id="address-dialog-error" title="Error" style="display: none;">
		<p>
			<span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
			There was an error saving your addresss. Please try again.</p>
	</div>
	<script id="itemTemplate" type="text/x-jsrender">
	 <div class="address-select-item" onclick="selectAddress(this);">
		<strong><span class="address-item-name">{{>firstName}} {{>lastName}}</span></strong><br/>
		<span class="address-item-address1">{{>address1}}</span><br/>
		{{if address2}}
		<span class="address-item-address2">{{>address2}}</span><br/>
		{{/if}}
		<span class="address-item-city-state-zip">{{>city}}, {{>stateText}} {{>zip}}</span><br/>
		<input type="hidden" class="address-item-id" value="{{>addressId}}"/>
	 </div>
	</script>
	<script id="currentAddressTemplate" type="text/x-jsrender">
	 <table width="100%" border="0" cellspacing="0" cellpadding="2">
	<tbody>
		<tr>
			<td style="padding-left: 10px; vertical-align: middle;">
				<b>{{>firstName}} {{>lastName}}</b>
			</td>

			 <td align="right" style="padding: 5px 5px 5px 0px;">
				&nbsp;
			</td>

		</tr>
		<tr>
			<td colspan="2">
				<table border="0" cellspacing="0" cellpadding="2">
					<tbody>
						<tr>
							<td width="10">
								<img width="10" height="1" src="/images/sp.gif" border="0">
							</td>
							<td>{{>address1}}
							{{if address2}}
							<div id="ctl00_ctl00_cph1_cph1_dlBillingAddresses_ctl00_pnlAddress2">
								{{>address2}}
							</div>
							{{/if}}
								<div>
									{{>city}},
									{{>stateText}}
									{{>zip}}
								</div>
								
							</td>
							<td width="10">
								<img width="10" height="1" src="/images/sp.gif" border="0">
							</td>
						</tr>
					</tbody>
				</table>
			</td>
		</tr>
	</tbody>
</table>
	</script>
	<script id="shipping-option-template" type="text/x-jsrender">
		 <div class="shipping-option-item">
			<div class="option-name">
				{{Name}}
				{{formattedShippingOption}}
				<input type="hidden" id="hfShippingRateComputationMethodId" Value="{{ShippingRateComputationMethodId}}"/>
				<input type="hidden" id="hfName" value={{"Name"}} />
			</div>
			<div class="option-description">
				{{"Description"}}
			</div>
		</div>
	</script>
</asp:Content>
