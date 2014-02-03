<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OneVariant.ascx.cs"
    Inherits="NopSolutions.NopCommerce.Web.Templates.Products.OneVariant" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductCategoryBreadcrumb" Src="~/Modules/ProductCategoryBreadcrumb.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductRating" Src="~/Modules/ProductRating.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductEmailAFriendButton" Src="~/Modules/ProductEmailAFriendButton.ascx" %>
<%--<%@ Register TagPrefix="nopCommerce" TagName="ProductAddToCompareList" Src="~/Modules/ProductAddToCompareList.ascx" %>--%>
<%@ Register TagPrefix="nopCommerce" TagName="ProductSpecs" Src="~/Modules/ProductSpecifications.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RelatedProducts" Src="~/AddonsByOsShop/Modules/RelatedProducts.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductReviews" Src="~/Modules/ProductReviews.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductsAlsoPurchased" Src="~/AddonsByOsShop/Modules/ProductsAlsoPurchased.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductAttributes" Src="~/Modules/ProductAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="GiftCardAttributes" Src="~/Modules/GiftCardAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductPrice1" Src="~/Modules/ProductPrice1.ascx" %>
<%--<%@ Register TagPrefix="nopCommerce" TagName="TierPrices" Src="~/Modules/TierPrices.ascx" %>--%>
<%@ Register TagPrefix="nopCommerce" TagName="ProductTags" Src="~/Modules/ProductTags.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductShareButton" Src="~/Modules/ProductShareButton.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="productzoom" Src="~/AddonsByOsShop/UserExperience/ProductZoom/productZoom.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RecentlyViewedProducts" Src="~/AddonsByOsShop/Modules/RecentlyViewedProducts.ascx" %>

<script type="text/javascript">
  function attribute() {
   for (var u = 0; u < 10; u++) {
        if(strTrans[u][0] != ""){
            if(strTrans[u][2] == "" || strTrans[u][3] == "" || strTrans[u][4] == "" || strTrans[u][2] == "0" || strTrans[u][3] == "0" || strTrans[u][4] == "0"){
                //If errors do not add attributes
            }
            else
            {
                var strItem = new Array();
                var str = new Array();
                strItem = defaultValue.split("|");
                for (var j = 0; j < strItem.length - 1; j++) {
                str = strItem[j].toString().split(",");
                 if (str[1] == strTrans[u][1]) {
                        subStr = str[0] + "," + str[1] + "," + str[2] + "|";
                        defaultValue = defaultValue.replace(subStr, "");
                    }
                }

                var dataSign = strTrans[u][2] + "@" + strTrans[u][3] + "@" + strTrans[u][4]+"@";
                defaultValue = defaultValue + strTrans[u][0] + "," + strTrans[u][1] + "," + dataSign + "|";
            }
        }
        
    }
    
        var quantity = document.getElementById("Quantity").value;
 
        if(isDigit(quantity))
        {
            if (quantity < 1)
                alert('<%=GetLocaleResourceString("OsShop.MoreOne")%>');
            else
                AjaxTemplate.getProductMessage(<%=ProductVariant.ProductVariantId%>,defaultValue,quantity);
        }
        else
            alert('<%=GetLocaleResourceString("OsShop.InputInteger")%>');

    }

    function isDigit(s) 
    { 
        var patrn=/^[0-9]{1,20}$/; 
        if (!patrn.exec(s)) return false; 
        return true; 
    } 

</script>


<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
    EnableScriptLocalization="true" ID="sm1" ScriptMode="Release" CompositeScript-ScriptMode="Release" />
<% if (this.SettingManager.GetSettingValueBoolean("Media.CategoryBreadcrumbEnabled"))
   { %>
<nopCommerce:ProductCategoryBreadcrumb ID="ctrlProductCategoryBreadcrumb" runat="server" />
<% } %>
<div class="clear">
</div>
<div class="product-details-page">
    <div class="product-essential product-details-info">
        <div >
        <nopCommerce:productzoom ID="ProductZoom" runat="server" />
        </div>

        <div class="overview">
            <h3 class="productname">
                <asp:Literal ID="lProductName" runat="server" />
            </h3>
            <div class="clear">
            </div>
            <div class="shortdescription">
                <asp:Literal ID="lShortDescription" runat="server" />
            </div>
            <asp:PlaceHolder runat="server" ID="phSKU">
                <div class="clear">
                </div>
                <div class="sku">
                    <%=GetLocaleResourceString("Products.SKU")%> <asp:Literal runat="server" ID="lSKU" />
                </div>
            </asp:PlaceHolder>
            <div class="clear">
                </div>
            <asp:PlaceHolder runat="server" ID="phManufacturerPartNumber">
                <div class="clear">
                </div>
                <div class="manufacturerpartnumber">
                    <%=GetLocaleResourceString("Products.ManufacturerPartNumber")%> <asp:Literal runat="server" ID="lManufacturerPartNumber" />
                </div>
            </asp:PlaceHolder>
            <div class="clear">
            </div>
            <asp:PlaceHolder runat="server" ID="phManufacturers">
                <div class="manufacturers">
                    <asp:Literal ID="lManufacturersTitle" runat="server" />
                    <asp:Repeater runat="server" ID="rptrManufacturers">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlManufacturer" runat="server" Text='<%#Server.HtmlEncode(Eval("LocalizedName").ToString()) %>' NavigateUrl='<%#SEOHelper.GetManufacturerUrl((Manufacturer)(Container.DataItem)) %>' />
                        </ItemTemplate>
                        <SeparatorTemplate>
                            ,
                        </SeparatorTemplate>
                    </asp:Repeater>
                </div>
            </asp:PlaceHolder>
            <div class="clear">
            </div>
        <div class="clear">
        </div>
            <div class="product-collateral">
                <nopCommerce:ProductRating ID="ctrlProductRating" runat="server" />
                <br />
             <div class="attributes">
                <nopCommerce:ProductAttributes ID="ctrlProductAttributes" runat="server" />
            </div>

                <div class="clear">
                </div>

                <div class="one-variant-price">
                    <nopCommerce:ProductPrice1 ID="ctrlProductPrice" runat="server" />
                    <nopCommerce:DecimalTextBox runat="server" ID="txtCustomerEnteredPrice" Value="1"
                        RequiredErrorMessage="<% $NopResources:Products.CustomerEnteredPrice.EnterPrice %>"
                        MinimumValue="0" MaximumValue="100000000" Width="100" />
                </div>
                <div class="clear">
                <div class="one-variant-price">
                    
                </div>
                </div>


                <div><%=GetLocaleResourceString("OsShop.Quantity")%>:&nbsp;&nbsp;  <input type="text" id="Quantity" value="1" style="width:50px; border: solid 1px #eee;"/>
                    <input type="button" value="Add to Cart" onclick="attribute();" class="productvariantaddtocartbutton"/>
					<asp:Button ID="btnAddToWishlist" runat="server" OnCommand="OnCommand" Text="<% $NopResources:Wishlist.AddToWishlist %>"
                        CommandName="AddToWishlist" CommandArgument='<%#Eval("ProductVariantId")%>' CssClass="productvariantaddtowishlistbutton" />
                    <br/><br/>
                </div>
                

                <div class="add-info" style="display:none;">
                    <nopCommerce:NumericTextBox runat="server" ID="txtQuantity" Value="1" RequiredErrorMessage="<% $NopResources:Products.EnterQuantity %>"
                        RangeErrorMessage="<% $NopResources:Products.QuantityRange %>" MinimumValue="1"
                        MaximumValue="999999" Width="50" />
                    <asp:Button ID="btnAddToCart" runat="server" OnCommand="OnCommand" Text="<% $NopResources:Products.AddToCart %>"
                        CommandName="AddToCart" CommandArgument='<%#Eval("ProductVariantId")%>' CssClass="productvariantaddtocartbutton" />
                   
                </div>
                <asp:Panel runat="server" ID="pnlDownloadSample" Visible="false" CssClass="one-variant-download-sample">
                    <span class="downloadsamplebutton">
                        <asp:HyperLink runat="server" ID="hlDownloadSample" Text="<% $NopResources:Products.DownloadSample %>" />
                    </span>
                </asp:Panel>
               
                <asp:Panel ID="pnlStockAvailablity" runat="server" class="stock">
                    <asp:Label ID="lblStockAvailablity" runat="server" />
                </asp:Panel>

                 <div class="clear">
                </div>
                <nopCommerce:GiftCardAttributes ID="ctrlGiftCardAttributes" runat="server" />
                <div class="clear">
                </div>


                 <ajaxToolkit:TabContainer runat="server" ID="ProductsTabs" ActiveTabIndex="1" CssClass="grey">
             <ajaxToolkit:TabPanel runat="server" ID="pnlProductDescription" HeaderText="<% $NopResources:Admin.ProductInfo.FullDescription %>">
                <ContentTemplate>
         			<div class="fulldescription">
                      <%var product = this.ProductService.GetProductById(this.ProductId);
                      lFullDescription.Text = product.LocalizedFullDescription; %>
                		<asp:Literal ID="lFullDescription" runat="server" />
            		</div>
             	</ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductReviews" HeaderText="<% $NopResources:Products.ProductReviews %>">
                <ContentTemplate>
                    <nopCommerce:ProductReviews ID="ctrlProductReviews" runat="server" ShowWriteReview="true" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductSpecs" HeaderText="<% $NopResources:Products.ProductSpecs %>">
                <ContentTemplate>
                    <nopCommerce:ProductSpecs ID="ctrlProductSpecs" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductTags" HeaderText="<% $NopResources:Products.ProductTags %>">
                <ContentTemplate>
                    <nopCommerce:ProductTags ID="ctrlProductTags" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
                <br />


                <nopCommerce:ProductEmailAFriendButton ID="ctrlProductEmailAFriendButton" runat="server" /><%--
                <nopCommerce:ProductAddToCompareList ID="ctrlProductAddToCompareList" runat="server" />--%>
                <nopCommerce:ProductShareButton ID="ctrlProductShareButton" runat="server" />
                 <div class="clear">
                </div>


            </div>
        </div>
         <div>
            <nopCommerce:RelatedProducts ID="ctrlRelatedProducts" runat="server" />
        </div>

    </div>
    <div class="clear">
    </div>
    <div class="product-collateral">
        <div class="product-variant-line">
            <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="error" />
            <div class="clear">
            </div>
            <%--<nopCommerce:TierPrices ID="ctrlTierPrices" runat="server" />
            <div class="clear">
            </div>--%>
        </div>
           

      
          <div class="ProductsPage-grey">
          <ajaxToolkit:TabContainer runat="server" ID="PicturesTabs" ActiveTabIndex="1" CssClass="grey">
            
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
           
        </ajaxToolkit:TabContainer>
        </div>
        
        <div class="clear">
        </div>

   
    </div>
</div>
