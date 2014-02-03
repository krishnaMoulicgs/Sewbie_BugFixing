<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThreeColumnCheckOut.master" AutoEventWireup="true" CodeBehind="SinglePageGuestCheckOut.aspx.cs" Inherits="NopSolutions.NopCommerce.Web.SinglePageGuestCheckOut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="EmailTextBox" Src="~/Modules/EmailTextBox.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
 <!-- Shipping Address Display and Edit -->
 <div class="checkout-data"> 
    <script type="text/javascript">

        function OpenPopup(IsBilling) {
            window.open("ShippingAddressSelect.aspx?IsBilling=" + IsBilling, "List", "menubar=1,resizable=1,width=700,height=500");
            return false;
        }

     function showPaypalAUP() {
         window.open("https://cms.paypal.com/us/cgi-bin/marketingweb?cmd=_render-content&content_ID=ua/AcceptableUse_full&locale.x=en_US");
     }
</script>
    <table>
<tr>
<td colspan="6">
<div class="checkout-data">
<asp:HiddenField ID="hdnShippingAddressId" runat="server" />
 <!-- shipping Address Display  -->
<asp:Panel runat="server" ID="pnlSelectShippingAddress">
<div class="checkout-data">
        <div class="select-address-title">
           Current Shipping address
         
        </div>         
        <div class="clear"></div>
          
        <div class="address-grid">
            <asp:DataList ID="dlShippingAddresses" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                RepeatLayout="Table" ItemStyle-CssClass="item-box">
                <ItemTemplate>
                    <div class="address-item">
                        <div class="address-box">                      
                                <table width="100%" cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr>
            <td style="vertical-align: middle; padding-left: 10px;">
                <b>
                    <asp:Literal ID="lFullName" Text='<%#(getFullname((Address)Container.DataItem)) %>'  runat="server"></asp:Literal></b>
            </td>          
             <td align="right" style="padding: 5px 5px 5px 0px;">
                <%if (ShowEditButton)
                    { %>
                <asp:LinkButton runat="server" ID="btnEditAddress" OnCommand="btnEditAddress_Click" Text="<% $NopResources:Address.Edit %>"
                    CommandArgument='<%# ((Address)Container.DataItem).AddressId %>' ValidationGroup="EditAddress"
                    CssClass="linkButton" />
                <%} %>
               
            </td>

        </tr>
        
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="2" border="0">
                    <tbody>
                        <tr>
                            <td width="10">
                                <img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>"
                                    alt="sp" />
                            </td>
                            <td>
                                <asp:Literal ID="lFirstName"  Text='<%#((Address)Container.DataItem).FirstName %>' runat="server"></asp:Literal>
                                <asp:Literal ID="lLastName" Text='<%# ((Address)Container.DataItem).LastName %>' runat="server"></asp:Literal><br />
                                <div>
                                    <%=GetLocaleResourceString("Address.Email")%>:
                                    <asp:Literal ID="lEmail" Text='<%# ((Address)Container.DataItem).Email %>' runat="server"></asp:Literal></div>
                                <div>
                                    <%=GetLocaleResourceString("Address.Phone")%>:
                                    <asp:Literal ID="lPhoneNumber" Text='<%# ((Address)Container.DataItem).PhoneNumber %>' runat="server"></asp:Literal></div>
                                <div>
                                    <%=GetLocaleResourceString("Address.Fax")%>:
                                    <asp:Literal ID="lFaxNumber" Text='<%# ((Address)Container.DataItem).FaxNumber %>' runat="server"></asp:Literal></div>
                                <asp:Panel ID="pnlCompany" runat="server">
                                    <asp:Literal ID="lCompany" Text='<%# ((Address)Container.DataItem).Company %>' runat="server"></asp:Literal></asp:Panel>
                                <div>
                                    <asp:Literal ID="lAddress1" Text='<%# ((Address)Container.DataItem).Address1 %>' runat="server"></asp:Literal></div>
                                <asp:Panel ID="pnlAddress2" runat="server">
                                    <asp:Literal ID="lAddress2" Text='<%# ((Address)Container.DataItem).Address2 %>' runat="server"></asp:Literal></asp:Panel>
                                <div>
                                    <asp:Literal ID="lCity" Text='<%# ((Address)Container.DataItem).City %>' runat="server"></asp:Literal>,
                                    <asp:Literal ID="lStateProvince" Text='<%# (getStateProvinceIfnull( (Address)Container.DataItem)) %>' runat="server"></asp:Literal>
                                    <asp:Literal ID="lZipPostalCode" Text='<%# ((Address)Container.DataItem).ZipPostalCode %>' runat="server"></asp:Literal></div>
                                <asp:Panel ID="pnlCountry" runat="server">
                                    <asp:Literal ID="lCountry" Text='<%# (getCountryIfnull( (Address)Container.DataItem)) %>' runat="server"></asp:Literal></asp:Panel>
                            </td>
                            <td width="10">
                                <img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>"
                                    alt="sp" />
                            </td>
                        </tr>
                         
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div> 
          
        </div>   
</asp:Panel>

<!---  -->
<asp:Panel runat="server" ID="pnlEditShippingAddress">


<div class="checkout-data">
<table>
<tr>
<td   >
<div class="checkout-data">
 <!-- Shipping Address  Edit -->
    <div class="enter-address-title">
        <asp:Label runat="server" ID="lEnterShippingAddress"></asp:Label>
    </div>
    <div class="clear"></div>    
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
        <div class="clear"> </div>
 <div class="button">
 <asp:Button runat="server" ID="btnSaveShippingAddress" Text="Save and Ship to this address" CssClass="newaddressnextstepbutton" ValidationGroup="EnterShippingAddress"  OnClick="btnSaveShippingAddress_Click" />
</div>
</div>
</div>
</td>
<td>
<!-- Billing Address Display and Edit -->  
<div id="Div1" class="checkout-data" runat="server" visible="false">
<!-- Billing Address Display and Edit -->     
    <div class="enter-address-title">
        <asp:Label runat="server" ID="lEnterBillingAddress"></asp:Label>
        </div>
    <div class="clear"></div>
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
    <div class="clear"></div>
    <div class="button">
        <asp:Button runat="server" ID="btnSaveBillingAddress" Text="Save and Bill to this address"  CssClass="newaddressnextstepbutton" OnClick="btnSaveBillingAddress_Click" />
    </div>
    </div>
    </div>

</td>
</tr>
</table>
</div>
</asp:Panel>
<!--- -->


</div>
</td>
<td valign="top" style="vertical-align:top;">
<div class="checkout-data" style="vertical-align:top">
     <!-- Billing Address Display  -->
    <asp:Panel runat="server" ID="pnlSelectBillingAddress" Visible="false">    
    <div class="checkout-data">
        <div class="select-address-title">
          Current Billing Address         
       
        </div>        
        <div class="clear"></div>
        <div class="address-grid">
            <asp:DataList ID="dlBillingAddresses" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                RepeatLayout="Table" ItemStyle-CssClass="item-box">
                <ItemTemplate>
                    <div class="address-item">
                        <div class="address-box">                          
                                 <table width="100%" cellspacing="0" cellpadding="2" border="0">
    <tbody>
        <tr>
            <td style="vertical-align: middle; padding-left: 10px;">
                <b>
                    <asp:Literal ID="lFullName" Text='<%# (getFullname( (Address)Container.DataItem)) %> '  runat="server"></asp:Literal></b>
            </td>
         

             <td align="right" style="padding: 5px 5px 5px 0px;">
                <%if (ShowBillEditButton)
                    { %>
                <asp:LinkButton runat="server" ID="btnBillEditAddress" OnCommand="btnBillEditAddress_Click"
                    Text="<% $NopResources:Address.Edit %>" CommandArgument='<%# ((Address)Container.DataItem).AddressId %>'
                    ValidationGroup="EditAddress" CssClass="linkButton" />
                <%} %>
                <%if (ShowBillDeleteButton)
                    { %>
                <asp:LinkButton runat="server" ID="btnBillDeleteAddress" OnCommand="btnBillDeleteAddress_Click"
                    CommandArgument='<%# ((Address)Container.DataItem).AddressId %>' Text="<% $NopResources:Address.Delete %>"
                    CausesValidation="false" CssClass="linkButton" />
                <%} %>
            </td>

        </tr>
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="2" border="0">
                    <tbody>
                        <tr>
                            <td width="10">
                                <img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>"
                                    alt="sp" />
                            </td>
                            <td>
                                <asp:Literal ID="lFirstName"  Text='<%# ((Address)Container.DataItem).FirstName %>' runat="server"></asp:Literal>
                                <asp:Literal ID="lLastName" Text='<%# ((Address)Container.DataItem).LastName %>' runat="server"></asp:Literal><br />
                                <div>
                                    <%=GetLocaleResourceString("Address.Email")%>:
                                    <asp:Literal ID="lEmail" Text='<%# ((Address)Container.DataItem).Email %>' runat="server"></asp:Literal></div>
                                <div>
                                    <%=GetLocaleResourceString("Address.Phone")%>:
                                    <asp:Literal ID="lPhoneNumber" Text='<%# ((Address)Container.DataItem).PhoneNumber %>' runat="server"></asp:Literal></div>
                                <div>
                                    <%=GetLocaleResourceString("Address.Fax")%>:
                                    <asp:Literal ID="lFaxNumber" Text='<%# ((Address)Container.DataItem).FaxNumber %>' runat="server"></asp:Literal></div>
                                <asp:Panel ID="pnlCompany" runat="server">
                                    <asp:Literal ID="lCompany" Text='<%# ((Address)Container.DataItem).Company %>' runat="server"></asp:Literal></asp:Panel>
                                <div>
                                    <asp:Literal ID="lAddress1" Text='<%# ((Address)Container.DataItem).Address1 %>' runat="server"></asp:Literal></div>
                                <asp:Panel ID="pnlAddress2" runat="server">
                                    <asp:Literal ID="lAddress2" Text='<%# ((Address)Container.DataItem).Address2 %>' runat="server"></asp:Literal></asp:Panel>
                                <div>
                                    <asp:Literal ID="lCity" Text='<%# ((Address)Container.DataItem).City %>' runat="server"></asp:Literal>,
                                    <asp:Literal ID="lStateProvince" Text='<%# (getStateProvinceIfnull( (Address)Container.DataItem)) %>' runat="server"></asp:Literal>
                                    <asp:Literal ID="lZipPostalCode" Text='<%# ((Address)Container.DataItem).ZipPostalCode %>' runat="server"></asp:Literal></div>
                                <asp:Panel ID="pnlCountry" runat="server">
                                    <asp:Literal ID="lCountry" Text='<%# (getCountryIfnull( (Address)Container.DataItem)) %>' runat="server"></asp:Literal></asp:Panel>
                            </td>
                            <td width="10">
                                <img height="1" width="10" border="0" src="<%=Page.ResolveUrl("~/images/sp.gif")%>"
                                    alt="sp" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>  

        </div>      
    </asp:Panel>
    
    </div>
</td>
</tr>
</table>
</div>
</asp:Content>


 
<asp:Content ID="Content2" ContentPlaceHolderID="cphBottom1" Visible="false" runat="server">
<asp:Panel runat="server" ID="pnladdressEdit" Visible="true">  


</asp:Panel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphBottom" runat="server">
 <div class="checkout-data">
  <!-- Order Summary -->
<asp:Panel runat="server" ID="pnlConfirmContent" class="stepcontent">
    <div class="order-summary-title">
        &nbsp;&nbsp;&nbsp;<%=GetLocaleResourceString("Checkout.OrderSummary")%></div>
    <div class="clear"></div>
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
    <asp:Repeater ID="rptShopHeader" runat="server" 
        onitemdatabound="rptShopHeader_ItemDataBound" 
        onitemcommand="rptShopHeader_ItemCommand">
        <ItemTemplate>
            <table class="cart">
                
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
                    <td colspan="4" style="text-align:left;">
                        <h2><%# (Eval("VendorName"))%> &nbsp; </h2>
                        <asp:HiddenField runat="server" ID="hidVendorId" Value='<%# Eval("VendorId") %>' />
                    </td>
                </tr>
                <tr>                                                                                                                                                                                                                            <tr class="cart-header-row">
                 
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
                                  
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# ((ShoppingCartItem)Container.DataItem).Quantity %>' CssClass="Label" />
                                   
                                </td>
                                <td style="white-space: nowrap;" class="end">
                                    <%#GetShoppingCartItemSubTotalString((ShoppingCartItem)Container.DataItem)%>
                                    <asp:Label ID="lblShoppingCartItemId" runat="server" Visible="false" Text='<%# ((ShoppingCartItem)Container.DataItem).ShoppingCartItemId %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
             <div id="ShowDetialFood">
                <%--<nopCommerce:OrderTotals runat="server" ID="ctrlOrderTotals" />--%>
                <div class="total-info" style="display:none;" >
                <table class="cart-total"  >
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
             </div>              
        </ItemTemplate>
    </asp:Repeater>
    <div class="clear"> </div>
    <div class="selected-checkout-attributes"><%=GetCheckoutAttributeDescription()%></div>  
</asp:Panel>
    </div>
    <div class="clear"></div>
</asp:Panel>    
  </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="chp2" runat="server">  
<table style="margin-left:-70px">
<tr>
<td>
  <!-- ShippingMethods -->
 <div runat="server" id="pnlConfirm" class="checkoutstep">

 <div runat="server" id="pnlShippingMethods" class="checkoutstep">
                    <div class="select-address-title">
                        Shipping Methods
                    </div>
                    <div class="checkout-data">
    <div class="shipping-options">
        <asp:Panel runat="server" ID="phSelectShippingMethod">
            <asp:DataList runat="server" ID="dlShippingOptions" OnItemCommand="dlShippingOptions_ItemCommand" >
                <ItemTemplate>
                    <div class="shipping-option-item">
                        <div class="option-name">
                            <%-- <nopCommerce:GlobalRadioButton runat="server" ID="rdShippingOption"  AutoPostBack="true" Checked="false"   CommandName="Select"
                                GroupName="shippingOptionGroup"   />  --%> 
                            <%#Server.HtmlEncode(Eval("Name").ToString()) %>
                            <%#Server.HtmlEncode(FormatShippingOption(((ShippingOption)Container.DataItem)))%>
                            <asp:HiddenField ID="hfShippingRateComputationMethodId" runat="server" Value='<%# Eval("ShippingRateComputationMethodId") %>' />
                            <asp:HiddenField ID="hfName" runat="server" Value='<%# Eval("Name") %>' />
                        </div>
                        <div class="option-description">
                            <%#Eval("Description") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <div class="clear">
            </div>
        </asp:Panel>
        <div class="clear">
        </div>
        <div class="error-block">
            <div class="message-error">
                <asp:Literal runat="server" ID="lShippingMethodsError" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
</div>
         </div>
 </div>
 <!-- Seller note and Confirm CheckOut -->
  <div class="checkout-data">
    
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
    
     <input type="hidden" runat="server" id="hidPaypalURL" value=""/>
</div>
</td>
<td align="center" style="vertical-align:top;" >
 

    <!--class="confirm-order select-button"-->

<!---OrderTotal----->
<div class="select-address-title">
                        Order Total
                    </div><div class="clear"></div>
<div class="checkout-data" >
 <div  class="checkout-data">
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
<div style="vertical-align:top;text-align:right" class="checkoutstep" >       
        <asp:Label runat="server" ID="lMinOrderTotalAmount" />
   <asp:Button runat="server" ID="btnNextStep" Text="<% $NopResources:Checkout.CheckoutWithPaypalButton %>"
                            OnClick="btnNextStep_Click" CssClass="editaddressbutton" ValidationGroup="CheckoutConfirm" />
            <input type="hidden" name="expType" value="light"/>        
    </div>
</div>

</td>
</tr>
</table>      
</asp:Content>