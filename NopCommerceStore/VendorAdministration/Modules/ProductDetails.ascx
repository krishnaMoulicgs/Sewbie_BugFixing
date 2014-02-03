<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.VendorAdministration.Modules.ProductDetailsControl"
    CodeBehind="ProductDetails.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductInfoEdit" Src="ProductInfoEdit.ascx" %>
<%--<%@ Register TagPrefix="nopCommerce" TagName="ProductVariantTierPrices" Src="ProductVariantTierPrices.ascx" %>--%>
<%@ Register TagPrefix="nopCommerce" TagName="ProductCategory" Src="SewbieProductCategory.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductManufacturer" Src="ProductManufacturer.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductPictures" Src="ProductPictures.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ConfirmationBox" Src="ConfirmationBox.ascx" %>
<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="<%=GetLocaleResourceString("VendorAdmin.ProductDetails.EditProductDetails")%>" />
        <%=GetLocaleResourceString("VendorAdmin.ProductDetails.EditProductDetails")%>
        -
        <asp:Label runat="server" ID="lblTitle" />
        <a href="Products.aspx" title="<%=GetLocaleResourceString("VendorAdmin.ProductDetails.BackToProductList")%>">
            (<%=GetLocaleResourceString("VendorAdmin.ProductDetails.BackToProductList")%>)</a>
    </div>
    <div class="options">
        <asp:Button ID="PreviewButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ProductDetails.PreviewButton.Text %>"
            ToolTip="<% $NopResources:VendorAdmin.ProductDetails.PreviewButton.ToolTip %>" />
        <asp:Button ID="SaveAndStayButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ProductDetails.SaveButton.Text %>"
            OnClick="SaveAndStayButton_Click" />
        <asp:Button ID="DeleteButton" runat="server" CssClass="adminButtonBlue" Text="<% $NopResources:VendorAdmin.ProductDetails.DeleteButton.Text %>"
            OnClick="DeleteButton_Click" CausesValidation="false" ToolTip="<% $NopResources:VendorAdmin.ProductDetails.DeleteButton.Tooltip %>" />
    </div>
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td class="section-header">
            <asp:LinkButton runat="server" ID="lnkGeneral">General Information</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <nopCommerce:ProductInfoEdit runat="server" ID="ctrlProductInfoEdit" />
        </td>
    </tr>
    <tr>
        <td class="section-header">
            <span style="color:Red;">*</span>
            <asp:LinkButton runat="server" ID="lnkCategories">Product Categories</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <nopCommerce:ProductCategory ID="ctrlProductCategory" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="section-header">
            <span style="color:Red;">*</span>
            <asp:LinkButton runat="server" ID="lnkPictures">Pictures</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <nopCommerce:ProductPictures ID="ctrlProductPictures" runat="server" />
        </td>
    </tr>
    <%--<tr>
        <td class="section-header">
            <asp:LinkButton runat="server" ID="lnkPricing">Pricing</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <nopCommerce:ProductVariantTierPrices runat="server" ID="ctrPrices" />
        </td>
    </tr>--%>
</table>
<nopCommerce:ConfirmationBox runat="server" ID="cbDelete" TargetControlID="DeleteButton"
    YesText="<% $NopResources:VendorAdmin.Common.Yes %>" NoText="<% $NopResources:VendorAdmin.Common.No %>"
    ConfirmText="<% $NopResources:VendorAdmin.Common.AreYouSure %>" />

    <div style="display:none;">
        <nopCommerce:ProductManufacturer ID="ctrlProductManufacturer" runat="server" />
    </div>
</asp:Panel>
