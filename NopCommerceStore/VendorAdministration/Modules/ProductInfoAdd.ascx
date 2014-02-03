<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ProductInfoAddControl"
    CodeBehind="ProductInfoAdd.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DecimalTextBox" Src="DecimalTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DatePicker" Src="DatePicker.ascx" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<script type="text/javascript">
    $(document).ready(function () {
        //toggleShipping();
        toggleManageStock();
    });

    function toggleShipping() {
//        if (getE('=cbIsShipEnabled.ClientID ').checked) {
//            $('#pnlFreeShipping').show();
//            $('#pnlAdditionalShippingCharge').show();
//            $('#pnlWeight').show();
//            $('#pnlLength').show();
//            $('#pnlWidth').show();
//            $('#pnlHeight').show();
//        }
//        else {
//            $('#pnlFreeShipping').hide();
//            $('#pnlAdditionalShippingCharge').hide();
//            $('#pnlWeight').hide();
//            $('#pnlLength').hide();
//            $('#pnlWidth').hide();
//            $('#pnlHeight').hide();
//        }
    }

    function toggleManageStock() {
        var selectedManageInventoryMethod = document.getElementById('<%=ddlManageStock.ClientID %>');
        var selectedManageInventoryMethodId = selectedManageInventoryMethod.options[selectedManageInventoryMethod.selectedIndex].value;
//        if (selectedManageInventoryMethodId == 0) {
//            $('#pnlStockQuantity').hide();
//            $('#pnlDisplayStockAvailability').hide();
//            $('#pnlDisplayStockQuantity').hide();
//            $('#pnlMinStockQuantity').hide();
//            $('#pnlLowStockActivity').hide();
//            $('#pnlNotifyForQuantityBelow').hide();
//            $('#pnlBackorders').hide();
//        }
//        else if (selectedManageInventoryMethodId == 1) {
//            $('#pnlStockQuantity').show();
//            $('#pnlDisplayStockAvailability').show();

//            if (getE('<%=cbDisplayStockAvailability.ClientID %>').checked) {
//                $('#pnlDisplayStockQuantity').show();
//            }
//            else {
//                $('#pnlDisplayStockQuantity').hide();
//            }

//            $('#pnlMinStockQuantity').show();
//            $('#pnlLowStockActivity').show();
//            $('#pnlNotifyForQuantityBelow').show();
//            $('#pnlBackorders').show();
//        }
//        else {
//            $('#pnlStockQuantity').hide();
//            $('#pnlDisplayStockAvailability').hide();
//            $('#pnlDisplayStockQuantity').hide();
//            $('#pnlMinStockQuantity').hide();
//            $('#pnlLowStockActivity').hide();
//            $('#pnlNotifyForQuantityBelow').hide();
//            $('#pnlBackorders').hide();
        //       }

        $('#pnlStockQuantity').show();
        $('#pnlDisplayStockAvailability').show();
        $('#pnlDisplayStockAvailability').hide();
        $('#pnlDisplayStockQuantity').hide();
        $('#pnlMinStockQuantity').hide();
        $('#pnlLowStockActivity').hide();
        $('#pnlNotifyForQuantityBelow').hide();
        $('#pnlBackorders').hide();
    }
</script>
<%if (this.HasLocalizableContent)
  { %>
<div id="localizablecontentpanel" class="tabcontainer-usual">
    <ul class="idTabs">
        <li class="idTab"><a href="#idTab_Info1" class="selected">
            <%=GetLocaleResourceString("Admin.Localizable.Standard")%></a></li>
        <asp:Repeater ID="rptrLanguageTabs" runat="server">
            <ItemTemplate>
                <li class="idTab"><a href="#idTab_Info<%# Container.ItemIndex+2 %>">
                    <asp:Image runat="server" ID="imgCFlag" Visible='<%# !String.IsNullOrEmpty(Eval("IconURL").ToString()) %>'
                        AlternateText='<%#Eval("Name")%>' ImageUrl='<%#Eval("IconURL").ToString()%>' />
                    <%#Server.HtmlEncode(Eval("Name").ToString())%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div id="idTab_Info1" class="tab">
        <%} %>
        <table class="adminContent">
            <tr>
                <td class="adminTitle">
                    <nopCommerce:ToolTipLabel runat="server" ID="lblProductName" Text="<% $NopResources:Admin.ProductInfo.ProductName %>"
                        ToolTip="<% $NopResources:Admin.ProductInfo.ProductName.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <nopCommerce:SimpleTextBox runat="server" CssClass="adminInput" ID="txtName" ErrorMessage="<% $NopResources:Admin.ProductInfo.ProductName.ErrorMessage %>">
                    </nopCommerce:SimpleTextBox>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">
                    <nopCommerce:ToolTipLabel runat="server" ID="lblShortDescription" Text="<% $NopResources:Admin.ProductInfo.ShortDescription %>"
                        ToolTip="<% $NopResources:Admin.ProductInfo.ShortDescription.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <asp:TextBox ID="txtShortDescription" runat="server" CssClass="adminInput" TextMode="MultiLine"
                        Height="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">
                    <nopCommerce:ToolTipLabel runat="server" ID="lblFullDescription" Text="<% $NopResources:Admin.ProductInfo.FullDescription %>"
                        ToolTip="<% $NopResources: Admin.ProductInfo.FullDescription.Tooltip%>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <FCKeditorV2:FCKeditor ID="txtFullDescription" runat="server" AutoDetectLanguage="false"
                        Height="350" Width="800px"   />
                </td>
            </tr>
        </table>
        <%if (this.HasLocalizableContent)
          { %></div>
    <asp:Repeater ID="rptrLanguageDivs" runat="server" OnItemDataBound="rptrLanguageDivs_ItemDataBound">
        <ItemTemplate>
            <div id="idTab_Info<%# Container.ItemIndex+2 %>" class="tab">
                <i>
                    <%=GetLocaleResourceString("Admin.Localizable.EmptyFieldNote")%></i>
                <asp:Label ID="lblLanguageId" runat="server" Text='<%#Eval("LanguageId") %>' Visible="false"></asp:Label>
                <table class="adminContent">
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedProductName" Text="<% $NopResources:Admin.ProductInfo.ProductName %>"
                                ToolTip="<% $NopResources:Admin.ProductInfo.ProductName.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <asp:TextBox runat="server" CssClass="adminInput" ID="txtLocalizedName">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedShortDescription" Text="<% $NopResources:Admin.ProductInfo.ShortDescription %>"
                                ToolTip="<% $NopResources:Admin.ProductInfo.ShortDescription.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <asp:TextBox ID="txtLocalizedShortDescription" runat="server" CssClass="adminInput"
                                TextMode="MultiLine" Height="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedFullDescription" Text="<% $NopResources:Admin.ProductInfo.FullDescription %>"
                                ToolTip="<% $NopResources: Admin.ProductInfo.FullDescription.Tooltip%>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <FCKeditorV2:FCKeditor ID="txtLocalizedFullDescription" runat="server" AutoDetectLanguage="false"
                                Height="350" Width="800px" />
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<%} %>
<table class="adminContent">
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblProductPublished" Text="<% $NopResources:Admin.ProductInfo.Published %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.Published.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbPublished" runat="server" Checked="True"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblProductTags" Text="<% $NopResources:Admin.ProductInfo.ProductTags %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.ProductTags.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:TextBox ID="txtProductTags" runat="server" CssClass="adminInput"></asp:TextBox>
        </td>
    </tr>
    <tr class="adminSeparator">
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblPrice" Text="<% $NopResources:Admin.ProductInfo.Price %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.Price.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:DecimalTextBox runat="server" CssClass="adminInput" ID="txtPrice" Value="0"
                RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.Price.RequiredErrorMessage %>"
                MinimumValue="0" MaximumValue="100000000" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.Price.RangeErrorMessage %>">
            </nopCommerce:DecimalTextBox>
            [<%=this.CurrencyService.PrimaryStoreCurrency.CurrencyCode%>]
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblOldPrice" Text="<% $NopResources:Admin.ProductInfo.OldPrice %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.OldPrice.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:DecimalTextBox runat="server" CssClass="adminInput" ID="txtOldPrice"
                Value="0" RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.OldPrice.RequiredErrorMessage%>"
                MinimumValue="0" MaximumValue="100000000" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.OldPrice.RangeErrorMessage %>">
            </nopCommerce:DecimalTextBox>
            [<%=this.CurrencyService.PrimaryStoreCurrency.CurrencyCode%>]
        </td>
    </tr>
    <tr id="pnlManageStock" style="display:none;">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblManageStock" Text="<% $NopResources:Admin.ProductInfo.ManageStock %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.ManageStock.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlManageStock" runat="server">
                <asp:ListItem Text="<% $NopResources:Admin.ManageInventoryMethod.DontManage %>" Value="0"></asp:ListItem>
                <asp:ListItem Text="<% $NopResources:Admin.ManageInventoryMethod.Manage %>" Value="1"
                    Selected="True"></asp:ListItem>
                <asp:ListItem Text="<% $NopResources:Admin.ManageInventoryMethod.ManageByAttributes %>"
                    Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="pnlStockQuantity">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblStockQuantity" Text="<% $NopResources:Admin.ProductInfo.StockQuantity %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.StockQuantity.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:NumericTextBox runat="server" CssClass="adminInput" ID="txtStockQuantity"
                RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.StockQuantity.RequiredErrorMessage %>"
                MinimumValue="-999999" MaximumValue="999999" Value="1" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.StockQuantity.RangeErrorMessage %>">
            </nopCommerce:NumericTextBox>
        </td>
    </tr>
    <tr id="pnlDisplayStockAvailability">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblDisplayStockAvailability" Text="<% $NopResources:Admin.ProductInfo.DisplayStockAvailability %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.DisplayStockAvailability.Tooltip %>"
                ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbDisplayStockAvailability" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr id="pnlDisplayStockQuantity">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblDisplayStockQuantity" Text="<% $NopResources:Admin.ProductInfo.DisplayStockQuantity %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.DisplayStockQuantity.Tooltip %>"
                ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbDisplayStockQuantity" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr id="pnlMinStockQuantity">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblMinStockQuantity" Text="<% $NopResources:Admin.ProductInfo.MinStockQuantity %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.MinStockQuantity.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:NumericTextBox runat="server" CssClass="adminInput" ID="txtMinStockQuantity"
                RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.MinStockQuantity.RequiredErrorMessage %>"
                MinimumValue="0" MaximumValue="999999" Value="0" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.MinStockQuantity.RangeErrorMessage %>">
            </nopCommerce:NumericTextBox>
        </td>
    </tr>
    <tr id="pnlLowStockActivity">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblLowStockActivity" Text="<% $NopResources:Admin.ProductInfo.LowStockActivity %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.LowStockActivity.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlLowStockActivity" AutoPostBack="False" CssClass="adminInput"
                runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="pnlNotifyForQuantityBelow">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblNotifyForQuantityBelow" Text="<% $NopResources:Admin.ProductInfo.NotifyForQuantityBelow %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.NotifyForQuantityBelow.Tooltip %>"
                ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:NumericTextBox runat="server" CssClass="adminInput" ID="txtNotifyForQuantityBelow"
                RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.NotifyForQuantityBelow.RequiredErrorMessage%>"
                MinimumValue="1" MaximumValue="999999" Value="1" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.NotifyForQuantityBelow.RangeErrorMessage %>">
            </nopCommerce:NumericTextBox>
        </td>
    </tr>
    <tr id="pnlBackorders">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblBackorders" Text="<% $NopResources:Admin.ProductInfo.Backorders %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.Backorders.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlBackorders" CssClass="adminInput" runat="server">
                <asp:ListItem Text="<% $NopResources:Admin.ProductBackorderMode.NoBackorders %>"
                    Value="0"></asp:ListItem>
                <asp:ListItem Text="<% $NopResources:Admin.ProductBackorderMode.AllowQtyBelow0 %>"
                    Value="1"></asp:ListItem>
                <asp:ListItem Text="<% $NopResources:Admin.ProductBackorderMode.AllowQtyBelow0AndNotifyCustomer %>"
                    Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="pnlOrderMinimumQuantity" style="display:none;">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblOrderMinimumQuantity" Text="<% $NopResources:Admin.ProductInfo.OrderMinimumQuantity %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.OrderMinimumQuantity.Tooltip %>"
                ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:NumericTextBox runat="server" CssClass="adminInput" ID="txtOrderMinimumQuantity"
                RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.OrderMinimumQuantity.RequiredErrorMessage %>"
                MinimumValue="1" MaximumValue="999999" Value="1" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.OrderMinimumQuantity.RangeErrorMessage %>">
            </nopCommerce:NumericTextBox>
        </td>
    </tr>
    <tr id="pnlOrderMaximumQuantity" style="display:none;">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblOrderMaximumQuantity" Text="<% $NopResources:Admin.ProductInfo.OrderMaximumQuantity %>"
                ToolTip="<% $NopResources:Admin.ProductInfo.OrderMaximumQuantity.Tooltip %>"
                ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <nopCommerce:NumericTextBox runat="server" CssClass="adminInput" ID="txtOrderMaximumQuantity"
                RequiredErrorMessage="<% $NopResources:Admin.ProductInfo.OrderMaximumQuantity.RequiredErrorMessage %>"
                MinimumValue="1" MaximumValue="999999" Value="10000" RangeErrorMessage="<% $NopResources:Admin.ProductInfo.OrderMaximumQuantity.RangeErrorMessage %>">
            </nopCommerce:NumericTextBox>
        </td>
    </tr>
</table>
