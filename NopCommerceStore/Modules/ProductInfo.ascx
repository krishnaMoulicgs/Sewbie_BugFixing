<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.ProductInfoControl"
    CodeBehind="ProductInfo.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductShareButton" Src="~/Modules/ProductShareButton.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="productzoom" Src="~/AddonsByOsShop/UserExperience/ProductZoom/productZoom.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductEmailAFriendButton" Src="~/Modules/ProductEmailAFriendButton.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductAddToCompareList" Src="~/Modules/ProductAddToCompareList.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductRating" Src="~/Modules/ProductRating.ascx" %>

<div class="product-details-info">
        <div >
        <nopCommerce:productzoom ID="ProductZoom" runat="server" />
        </div>
    <div class="overview">
        <h3 class="productname">
            <asp:Literal ID="lProductName" runat="server" />
        </h3>
        <br />
        <div class="shortdescription">
            <asp:Literal ID="lShortDescription" runat="server" />
        </div>
        <div class="clear">
        </div>
        <asp:PlaceHolder runat="server" ID="phManufacturers">
            <div class="manufacturers">
                <asp:Literal ID="lManufacturersTitle" runat="server" />
                <asp:Repeater runat="server" ID="rptrManufacturers">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlManufacturer" runat="server" Text='<%#Server.HtmlEncode(Eval("LocalizedName").ToString()) %>'
                            NavigateUrl='<%#SEOHelper.GetManufacturerUrl((Manufacturer)(Container.DataItem)) %>' />
                    </ItemTemplate>
                    <SeparatorTemplate>
                        ,
                    </SeparatorTemplate>
                </asp:Repeater>
            </div>
        </asp:PlaceHolder>
        <div class="clear">
        </div>


         <div class="fulldescription">
        <asp:Literal ID="lFullDescription" runat="server" />
        </div>

        <div class="clear">
        </div>
        <nopCommerce:ProductRating ID="ctrlProductRating" runat="server"></nopCommerce:ProductRating>

        <div class="clear">
        </div>

         <nopCommerce:ProductEmailAFriendButton ID="ctrlProductEmailAFriendButton" runat="server">
        </nopCommerce:ProductEmailAFriendButton>
        &nbsp;
        <nopCommerce:ProductAddToCompareList ID="ctrlProductAddToCompareList" runat="server">
        </nopCommerce:ProductAddToCompareList>


        <nopCommerce:ProductShareButton ID="ctrlProductShareButton" runat="server" />
    </div>

</div>
