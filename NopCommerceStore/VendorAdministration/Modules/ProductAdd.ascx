<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ProductAddControl"
    CodeBehind="ProductAdd.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductInfoAdd" Src="ProductInfoAdd.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductSEO" Src="ProductSEO.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductCategory" Src="ProductCategory.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductManufacturer" Src="ProductManufacturer.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RelatedProducts" Src="RelatedProducts.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="CrossSellProducts" Src="CrossSellProducts.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductPictures" Src="ProductPictures.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductSpecifications" Src="ProductSpecifications.ascx" %>
<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="<%=GetLocaleResourceString("VendorAdmin.ProductAdd.AddNewProduct")%>" />
        <%=GetLocaleResourceString("VendorAdmin.ProductAdd.AddNewProduct")%>
        <a href="Products.aspx" title="<%=GetLocaleResourceString("VendorAdmin.ProductAdd.BackToProductList")%>">
            (<%=GetLocaleResourceString("VendorAdmin.ProductAdd.BackToProductList")%>)</a>
    </div>
    <div class="options">
        <asp:Button ID="SaveButton" runat="server" Text="<% $NopResources:VendorAdmin.ProductAdd.SaveButton.Text %>"
            CssClass="adminButtonBlue" OnClick="SaveButton_Click" ToolTip="<% $NopResources:VendorAdmin.ProductAdd.SaveButton.Tooltip %>" />
        <asp:Button ID="SaveAndStayButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ProductAdd.SaveAndStayButton.Text %>"
            OnClick="SaveAndStayButton_Click" />
    </div>
</div>
<table width="100%" cellpadding="2" cellspacing="2">
    <tr>
        <td class="section-header">
           <asp:LinkButton runat="server" ID="lnkGeneral" > <asp:Image ID="imgGeneral" runat="server" />&nbsp;General Information</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
         <asp:Panel runat="server" ID="pnlGeneral">
            <nopCommerce:ProductInfoAdd runat="server" ID="ctrlProductInfoAdd" />
          </asp:Panel>
          <ajaxToolkit:CollapsiblePanelExtender ID="cpeGeneral" runat="Server" TargetControlID="pnlGeneral"
                CollapsedSize="0" ExpandedSize="700" Collapsed="False" ExpandControlID="lnkGeneral"
                CollapseControlID="lnkGeneral" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                TextLabelID="Label1" CollapsedText="Show" ExpandedText="Hide" 
                ExpandedImage="~/App_Themes/SewbieAdmin/images/collapse.jpg"  CollapsedImage="~/App_Themes/SewbieAdmin/images/expand.jpg"
                 ImageControlID="imgGeneral"
                SuppressPostBack="true" ExpandDirection="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="section-header">
           <asp:LinkButton runat="server" ID="lnkSEO" > <asp:Image ID="imgSEO" runat="server" />&nbsp;SEO</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel  runat="server" ID="pnlProductSEO">
                <nopCommerce:ProductSEO ID="ctrlProductSEO" runat="server" />
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeSEO" runat="Server" TargetControlID="pnlProductSEO"
                CollapsedSize="0" ExpandedSize="225" Collapsed="True" ExpandControlID="lnkSEO"
                CollapseControlID="lnkSEO" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                TextLabelID="Label1" CollapsedText="Show" ExpandedText="Hide" 
                ExpandedImage="~/App_Themes/SewbieAdmin/images/collapse.jpg"  CollapsedImage="~/App_Themes/SewbieAdmin/images/expand.jpg"
                 ImageControlID="imgSEO"
                SuppressPostBack="true" ExpandDirection="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="section-header">
           <asp:LinkButton runat="server" ID="lnkCategories" > <asp:Image ID="imgCategories" runat="server" />&nbsp;Product Categories</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td> <asp:Panel  runat="server" ID="pnlCategories">
            <nopCommerce:ProductCategory ID="ctrlProductCategory" runat="server" />
            </asp:Panel>
             <ajaxToolkit:CollapsiblePanelExtender ID="cpeCategories" runat="Server" TargetControlID="pnlCategories"
                CollapsedSize="0" ExpandedSize="750" Collapsed="True" ExpandControlID="lnkCategories"
                CollapseControlID="lnkCategories" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                TextLabelID="Label1" CollapsedText="Show" ExpandedText="Hide" 
                ExpandedImage="~/App_Themes/SewbieAdmin/images/collapse.jpg"  CollapsedImage="~/App_Themes/SewbieAdmin/images/expand.jpg"
                 ImageControlID="imgCategories"
                SuppressPostBack="true" ExpandDirection="Vertical" />
        </td>
    </tr>
</table>
