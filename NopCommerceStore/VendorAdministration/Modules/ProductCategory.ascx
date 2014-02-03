<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ProductCategoryControl"
    CodeBehind="ProductCategory.ascx.cs" %>
    <%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
    <%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="SimpleTextBox.ascx" %>
    <%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<asp:UpdatePanel ID="upCat" runat="server">
    <ContentTemplate>
        <asp:GridView ID="gvCategoryMappings" runat="server" AutoGenerateColumns="false"
            Width="100%" OnPageIndexChanging="gvCategoryMappings_PageIndexChanging" AllowPaging="true"
            PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="<% $NopResources:VendorAdmin.ProductCategory.Category %>"
                    ItemStyle-Width="60%">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbCategoryInfo" runat="server" Text='<%# Server.HtmlEncode(Eval("CategoryInfo").ToString()) %>'
                            Checked='<%# Eval("IsMapped") %>' ToolTip="<% $NopResources:Admin.ProductCategory.Category.Tooltip %>" />
                        <asp:HiddenField ID="hfCategoryId" runat="server" Value='<%# Eval("CategoryId") %>' />
                        <asp:HiddenField ID="hfProductCategoryId" runat="server" Value='<%# Eval("ProductCategoryId") %>' />
                        <asp:HiddenField ID="hfCategoryDisplayOrder" runat="server" Value='<%# Eval("DisplayOrder") %>' />
                        <asp:HiddenField ID="hfCategoryIsFeatured" runat="server" Value='<%# Eval("IsFeatured") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="up1" runat="server" AssociatedUpdatePanelID="upCat">
    <ProgressTemplate>
        <div class="progress">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/UpdateProgress.gif"
                AlternateText="update" />
            <%=GetLocaleResourceString("Admin.Common.Wait...")%>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
