<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ManufacturerSEOControl"
    CodeBehind="ManufacturerSEO.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<%if (this.HasLocalizableContent)
  { %>
<div id="localizablecontentpanel" class="tabcontainer-usual">
    <ul class="idTabs">
        <li class="idTab"><a href="#idTab_SEO1" class="selected">
            <%=GetLocaleResourceString("Admin.Localizable.Standard")%></a></li>
        <asp:Repeater ID="rptrLanguageTabs" runat="server">
            <ItemTemplate>
                <li class="idTab"><a href="#idTab_SEO<%# Container.ItemIndex+2 %>">
                    <asp:Image runat="server" ID="imgCFlag" Visible='<%# !String.IsNullOrEmpty(Eval("IconURL").ToString()) %>'
                        AlternateText='<%#Eval("Name")%>' ImageUrl='<%#Eval("IconURL").ToString()%>' />
                    <%#Server.HtmlEncode(Eval("Name").ToString())%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div id="idTab_SEO1" class="tab">
        <%} %>
        <table class="adminContent">
            <tr>
                <td class="adminTitle">
                    <nopCommerce:ToolTipLabel runat="server" ID="lblMetaKeywords" Text="<% $NopResources:VendorAdmin.ManufacturerSEO.MetaKeywords %>"
                        ToolTip="<% $NopResources:VendorAdmin.ManufacturerSEO.MetaKeywords.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                </td>
                <td class="adminData">
                    <asp:TextBox ID="txtMetaKeywords" CssClass="adminInput" runat="server"></asp:TextBox>
                </td>
            </tr>            
        </table>
        <%if (this.HasLocalizableContent)
          { %></div>
    <asp:Repeater ID="rptrLanguageDivs" runat="server" OnItemDataBound="rptrLanguageDivs_ItemDataBound">
        <ItemTemplate>
            <div id="idTab_SEO<%# Container.ItemIndex+2 %>" class="tab">
                <i>
                    <%=GetLocaleResourceString("Admin.Localizable.EmptyFieldNote")%></i>
                <table class="adminContent">
                    <asp:Label ID="lblLanguageId" runat="server" Text='<%#Eval("LanguageId") %>' Visible="false"></asp:Label>
                    <tr>
                        <td class="adminTitle">
                            <nopCommerce:ToolTipLabel runat="server" ID="lblLocalizedMetaKeywords" Text="<% $NopResources:VendorAdmin.ManufacturerSEO.MetaKeywords %>"
                                ToolTip="<% $NopResources:VendorAdmin.ManufacturerSEO.MetaKeywords.Tooltip %>" ToolTipImage="<% $NopResources:VendorAdmin.HelpIcon.Path %>" />
                        </td>
                        <td class="adminData">
                            <asp:TextBox ID="txtLocalizedMetaKeywords" CssClass="adminInput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<%} %>
<table class="adminContent">
</table>
