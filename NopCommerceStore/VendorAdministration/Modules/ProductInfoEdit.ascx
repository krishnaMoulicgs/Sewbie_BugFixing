<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ProductInfoControl"
    CodeBehind="ProductInfoEdit.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DecimalTextBox" Src="DecimalTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>


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
                    <span style="color:Red;">*</span>
                    <nopCommerce:ToolTipLabel runat="server" ID="lblProductName" Text="<% $NopResources:Admin.ProductInfo.ProductName %>"
                        ToolTip="<% $NopResources:Admin.ProductInfo.ProductName.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <nopCommerce:SimpleTextBox runat="server" ID="txtName" CssClass="adminInput" ErrorMessage="<% $NopResources:Admin.ProductInfo.ProductName.ErrorMessage %>">
                    </nopCommerce:SimpleTextBox>
                    <%--<input id="txtName" class="adminInput" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">
                    <span style="color:Red;">*</span>
                    <nopCommerce:ToolTipLabel runat="server" ID="lblShortDescription" Text="<% $NopResources:Admin.ProductInfo.ShortDescription %>"
                        ToolTip="<% $NopResources:Admin.ProductInfo.ShortDescription.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <asp:TextBox ID="txtShortDescription" runat="server" CssClass="adminInput" TextMode="MultiLine"
                        Height="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtShortDescription" Font-Name="verdana"
                            Font-Size="9pt" runat="server" Display="None" 
                            ErrorMessage="<% $NopResources:Admin.ProductInfo.ShortDescription.ErrorMessage %>"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvValueE" TargetControlID="rfvValue"
                            HighlightCssClass="validatorCalloutHighlight" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">
                    <span style="color:Red;">*</span>
                    <nopCommerce:ToolTipLabel runat="server" ID="lblFullDescription" Text="<% $NopResources:Admin.ProductInfo.FullDescription %>"
                        ToolTip="<% $NopResources: Admin.ProductInfo.FullDescription.Tooltip%>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <FCKeditorV2:FCKeditor ID="txtFullDescription" runat="server" AutoDetectLanguage="false"
                        Height="350" Width="800px" />
                    <asp:CustomValidator ID="FullDescCustomValidator" Font-Name="verdana"
                        Font-Size="9pt" runat="server" Display="None" OnServerValidate="FullDescription_Validate"
                        ErrorMessage="<% $NopResources:Admin.ProductInfo.FullDescription.ErrorMessage %>"></asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="FullDescCustomValidator"
                        HighlightCssClass="validatorCalloutHighlight" />
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
                            <asp:TextBox runat="server" ID="txtLocalizedName" CssClass="adminInput">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedShortDescription" Text="<% $NopResources:Admin.ProductInfo.ShortDescription %>"
                                ToolTip="<% $NopResources:Admin.ProductInfo.ShortDescription.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <asp:TextBox ID="txtLocalizedShortDescription" runat="server" CssClass="adminInput" MaxLength="255" Height="100"></asp:TextBox>
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
    <tr id="pnlDisplayStockQuantity">
        <td class="adminTitle">
            <nopcommerce:tooltiplabel runat="server" id="lblDisplayStockQuantity" text="<% $NopResources:Admin.ProductVariantInfo.DisplayStockQuantity %>"
                tooltip="<% $NopResources:Admin.ProductVariantInfo.DisplayStockQuantity.Tooltip %>"
                tooltipimage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbDisplayStockQuantity" runat="server"></asp:CheckBox>
        </td>
    </tr>
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
        <td colspan="2">
            <asp:HyperLink ID="hlViewReviews" runat="server" Text="View reviews" ToolTip="<% $NopResources:Admin.ProductInfo.AllowCustomerReviewsView.Tooltip %>" />
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
    <tr id="pnlAdditionalShippingCharge">
        <td class="adminTitle">
            <nopcommerce:tooltiplabel runat="server" id="lblAdditionalShippingCharge" text="<% $NopResources:VendorAdmin.ProductVariantInfo.AdditionalShippingCharge %>"
                tooltip="<% $NopResources:VendorAdmin.ProductVariantInfo.AdditionalShippingCharge.Tooltip %>"
                tooltipimage="~/VendorAdministration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <nopCommerce:DecimalTextBox runat="server" CssClass="adminInput" ID="txtAdditionalShippingCharge"
                Value="0" RequiredErrorMessage="<% $NopResources:VendorAdmin.ProductVariantInfo.AdditionalShippingCharge.RequiredErrorMessage %>"
                MinimumValue="0" MaximumValue="100000000" RangeErrorMessage="<% $NopResources:VendorAdmin.ProductVariantInfo.AdditionalShippingCharge.RangeErrorMessage %>">
            </nopCommerce:DecimalTextBox>
            [<%=this.CurrencyService.PrimaryStoreCurrency.CurrencyCode%>]
        </td>
    </tr>
    <tr class="adminSeparator">
        <td colspan="2">
            <hr />
        </td>
    </tr>
</table>
