<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Templates.Products.VariantsInGrid"
    CodeBehind="VariantsInGrid.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductVariantsInGrid" Src="~/Modules/ProductVariantsInGrid.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductInfo" Src="~/Modules/ProductInfo.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductCategoryBreadcrumb" Src="~/Modules/ProductCategoryBreadcrumb.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductSpecs" Src="~/Modules/ProductSpecifications.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RelatedProducts" Src="~/AddonsByOsShop/Modules/RelatedProducts.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductReviews" Src="~/Modules/ProductReviews.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductsAlsoPurchased" Src="~/AddonsByOsShop/Modules/ProductsAlsoPurchased.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductTags" Src="~/Modules/ProductTags.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductShareButton" Src="~/Modules/ProductShareButton.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RecentlyViewedProducts" Src="~/AddonsByOsShop/Modules/RecentlyViewedProducts.ascx" %>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
    EnableScriptLocalization="true" ID="sm1" ScriptMode="Release" CompositeScript-ScriptMode="Release" />
<% if (this.SettingManager.GetSettingValueBoolean("Media.CategoryBreadcrumbEnabled"))
   { %>
<nopCommerce:ProductCategoryBreadcrumb ID="ctrlProductCategoryBreadcrumb" runat="server">
</nopCommerce:ProductCategoryBreadcrumb>
<% } %>
<div class="clear">
</div>
<div class="product-details-page">
    <div class="product-essential">
        <nopCommerce:ProductInfo ID="ctrlProductInfo" runat="server"></nopCommerce:ProductInfo>
        <nopCommerce:RelatedProducts ID="ctrlRelatedProducts" runat="server"></nopCommerce:RelatedProducts>
    </div>
    <div class="clear">
    </div>
    <div class="product-collateral">
        
       
        <div class="clear">
        </div>
        <nopCommerce:ProductVariantsInGrid ID="ctrlProductVariantsInGrid" runat="server">
        </nopCommerce:ProductVariantsInGrid>
        <div class="clear">
        </div>





         <div class="ProductsPage-grey">
        <ajaxToolkit:TabContainer runat="server" ID="ProductsTabs" ActiveTabIndex="1" CssClass="grey">



              <ajaxToolkit:TabPanel runat="server" ID="pnlPurchased" HeaderText="<% $NopResources:Products.AlsoPurchased %>">
                <ContentTemplate>
                    <nopCommerce:ProductsAlsoPurchased ID="ctrlPurchased" runat="server" ShowWriteReview="true" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlRecently" HeaderText="<% $NopResources:Products.RecentlyViewedProducts %>">
                <ContentTemplate>
                    <nopCommerce:RecentlyViewedProducts ID="ctrlRecently" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" ID="pnlProductReviews" HeaderText="<% $NopResources:Products.ProductReviews %>">
                <ContentTemplate>
                    <nopCommerce:ProductReviews ID="ctrlProductReviews" runat="server" ShowWriteReview="true">
                    </nopCommerce:ProductReviews>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductSpecs" HeaderText="<% $NopResources:Products.ProductSpecs %>">
                <ContentTemplate>
                    <nopCommerce:ProductSpecs ID="ctrlProductSpecs" runat="server"></nopCommerce:ProductSpecs>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductTags" HeaderText="<% $NopResources:Products.ProductTags %>">
                <ContentTemplate>
                    <nopCommerce:ProductTags ID="ctrlProductTags" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>


        </ajaxToolkit:TabContainer>
        </div>
 


        <div class="clear">
        </div> 
     </div>
</div>
