<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ManufacturerInfoControl"
    CodeBehind="ManufacturerInfo.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

    
<%if (this.HasLocalizableContent)
  { %>
<div id="localizablecontentpanel" class="tabcontainer-usual">
    <ul class="idTabs">
        <li class="idTab"><a href="#idTab_Info1" class="selected">
            <%=GetLocaleResourceString("VendorAdmin.Localizable.Standard")%></a></li>
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
                    <nopCommerce:ToolTipLabel runat="server" ID="lblName" Text="<% $NopResources:VendorAdmin.ManufacturerInfo.Name %>"
                        ToolTip="<% $NopResources:VendorAdmin.ManufacturerInfo.Name.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <nopCommerce:SimpleTextBox runat="server" ID="txtName" CssClass="adminInput" ErrorMessage="<% $NopResources:VendorAdmin.ManufacturerInfo.Name.ErrorMessage %>">
                    </nopCommerce:SimpleTextBox>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">
                    <nopCommerce:ToolTipLabel runat="server" ID="lblManufacturerDescription" Text="<% $NopResources:VendorAdmin.ManufacturerInfo.Description %>"
                        ToolTip="<% $NopResources:VendorAdmin.ManufacturerInfo.Description.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <FCKeditorV2:FCKeditor ID="txtDescription" runat="server" AutoDetectLanguage="false"
                        Height="350" Width="800px" />
                </td>
            </tr>
        </table>
        <%if (this.HasLocalizableContent)
          { %></div>
    <asp:Repeater ID="rptrLanguageDivs" runat="server" OnItemDataBound="rptrLanguageDivs_ItemDataBound">
        <ItemTemplate>
            <div id="idTab_Info<%# Container.ItemIndex+2 %>" class="tab">
                <i>
                    <%=GetLocaleResourceString("VendorAdmin.Localizable.EmptyFieldNote")%></i>
                <asp:Label ID="lblLanguageId" runat="server" Text='<%#Eval("LanguageId") %>' Visible="false"></asp:Label>
                <table class="adminContent">
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedName" Text="<% $NopResources:VendorAdmin.ManufacturerInfo.Name %>"
                                ToolTip="<% $NopResources:VendorAdmin.ManufacturerInfo.Name.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <asp:TextBox runat="server" ID="txtLocalizedName" CssClass="adminInput">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedDescription" Text="<% $NopResources:VendorAdmin.ManufacturerInfo.Description %>"
                                ToolTip="<% $NopResources:VendorAdmin.ManufacturerInfo.Description.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <FCKeditorV2:FCKeditor ID="txtLocalizedDescription" runat="server" AutoDetectLanguage="false"
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
            <nopCommerce:ToolTipLabel runat="server" ID="lblImage" Text="<% $NopResources:VendorAdmin.ManufacturerInfo.Image %>"
                ToolTip="<% $NopResources:VendorAdmin.ManufacturerInfo.Image.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
        </td>
        <td class="adminData">
            <asp:Image ID="iManufacturerPicture" runat="server" />
            <br />
            <asp:Button ID="btnRemoveManufacturerImage" CssClass="adminButton" CausesValidation="false"
                runat="server" Text="<% $NopResources:VendorAdmin.ManufacturerInfo.Image.Remove %>"
                OnClick="btnRemoveManufacturerImage_Click" Visible="false" />
            <br />
            <asp:FileUpload ID="fuManufacturerPicture" CssClass="adminInput" runat="server" />
        </td>
    </tr>
</table>
