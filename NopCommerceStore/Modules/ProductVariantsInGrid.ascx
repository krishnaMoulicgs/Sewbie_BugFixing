<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.ProductVariantsInGridControl"
    CodeBehind="ProductVariantsInGrid.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="DecimalTextBox" Src="~/Modules/DecimalTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductAttributes" Src="~/Modules/ProductAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="GiftCardAttributes" Src="~/Modules/GiftCardAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductPrice1" Src="~/Modules/ProductPrice1.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="TierPrices" Src="~/Modules/TierPrices.ascx" %>
<%@ Reference Control="~/Modules/ProductAttributes.ascx" %>
<%@ Reference Control="~/Modules/EmailTextBox.ascx" %>

<script type="text/javascript">

    var defaultCount = "";
    var wrongSign = 0;
    function attribute(ProductVariantId) {
        var attributeTrans = defaultValue;
        var countTrans = "";
        var strItem = new Array();
        var str = new Array();
        var subStr = "", sign = 1, countBuy;
        countBuy = 1;

        if (wrongSign == 1) {
            alert('<%=GetLocaleResourceString("OsShop.InputInteger")%>');
        }

        if (wrongSign == 2) {
            alert('<%=GetLocaleResourceString("OsShop.MoreOne")%>');
        }


        for (var u = 0; u < 10; u++) {
            if (strTrans[u][0] != "") {
                if (strTrans[u][2] == "" || strTrans[u][3] == "" || strTrans[u][4] == "" || strTrans[u][2] == "0" || strTrans[u][3] == "0" || strTrans[u][4] == "0") {
                    //如果选择错误 不添加属性
                }
                else {
                    strItem = defaultValue.split("|");
                    for (var j = 0; j < strItem.length - 1; j++) {
                        str = strItem[j].toString().split(",");
                        if (str[1] == strTrans[u][1]) {
                            subStr = str[0] + "," + str[1] + "," + str[2] + "|";
                            defaultValue = defaultValue.replace(subStr, "");
                        }
                    }

                    var dataSign = strTrans[u][2] + "@" + strTrans[u][3] + "@" + strTrans[u][4] + "@";
                    defaultValue = defaultValue + strTrans[u][0] + "," + strTrans[u][1] + "," + dataSign + "|";
                }
            }

        }
        attributeTrans = defaultValue;

        if (wrongSign == 0) {
            if (defaultCount != "") {
                countTrans = defaultCount;

                strItem = countTrans.split("|");
                for (var i = 0; i < strItem.length - 1; i++) {
                    str = strItem[i].toString().split(",");
                    if (str[0] == ProductVariantId) {
                        countBuy = parseInt(str[1]);
                        break;
                    }
                }
            }

            strItem = defaultValue.split("|");
            for (var j = 0; j < strItem.length - 1; j++) {
                str = strItem[j].toString().split(",");
                if (str[0] != ProductVariantId) {
                    subStr = str[0] + "," + str[1] + "," + str[2] + "|";
                    attributeTrans = attributeTrans.replace(subStr, "");

                }
            }

            AjaxTemplate.getProductMessage(ProductVariantId, attributeTrans, countBuy);
        }
        wrongSign = 0;
    }

    function isDigit(s) {
        var patrn = /^[0-9]{1,20}$/;
        if (!patrn.exec(s)) return false;
        return true;
    }

    function quantityValue(ProductVariantId, obj) {
        var quantity = obj.value;
        var i, j;
        var strItem = new Array();
        var str = new Array();
        var subStr = "", addStr = "", signCount = 1;


        //     alert("sign:" + sign);
        if (isDigit(quantity) == false)
            wrongSign = 1; //alert("please input integer");
        else {
            //......................................
            strItem = defaultCount.split("|");
            for (j = 0; j < strItem.length - 1; j++) {
                str = strItem[j].toString().split(",");
                if (str[0] == ProductVariantId) {//如果有相同的 用新的替换原来的
                    subStr = str[0] + "," + str[1] + "|";
                    addStr = ProductVariantId + "," + quantity + "|";
                    defaultCount = defaultCount.replace(subStr, addStr);
                    signCount = 0;
                }
            }
            if (signCount == 1) {//如果没有 相同的变种数量
                defaultCount = defaultCount + ProductVariantId + "," + quantity + "|";
            }
            //......................................
            if (quantity < 1)
                wrongSign = 2; //alert("warnning 1~999999");
        }
    }

  
</script>


<div class="product-variant-list">
    <asp:Repeater ID="rptVariants" runat="server" OnItemCommand="rptVariants_OnItemCommand"
        OnItemDataBound="rptVariants_OnItemDataBound">
        <ItemTemplate>
            <div class="product-variant-line">
             <div class="product-variant-line-left">
                <div class="picture">
                    <asp:Image ID="iProductVariantPicture" runat="server" />
                
                <div class="overview">
                    <div class="productname">
                        <%#Server.HtmlEncode(Eval("LocalizedName").ToString())%>
                    </div>
                    <asp:Label runat="server" ID="ProductVariantId" Text='<%#Eval("ProductVariantId")%>'
                        Visible="false" />
                </div>
                </div>
                 </div>
                
                <div class="product-variant-line-right">
                <div class="description">
                    <asp:Literal runat="server" ID="lDescription" Visible='<%# !String.IsNullOrEmpty(Eval("LocalizedDescription").ToString()) %>'
                        Text='<%# Eval("LocalizedDescription")%>'>
                    </asp:Literal>
                </div>
                <asp:Panel runat="server" ID="pnlDownloadSample" Visible="false" CssClass="downloadsample">
                    <span class="downloadsamplebutton">
                        <asp:HyperLink runat="server" ID="hlDownloadSample" Text="<% $NopResources:Products.DownloadSample %>">
                        </asp:HyperLink>
                    </span>
                </asp:Panel>
                <nopCommerce:TierPrices ID="ctrlTierPrices" runat="server" ProductVariantId='<%#Eval("ProductVariantId") %>'>
                </nopCommerce:TierPrices>
                <div class="clear">
                </div>
                <div class="attributes">
                    <nopCommerce:ProductAttributes ID="ctrlProductAttributes" runat="server" ProductVariantId='<%#Eval("ProductVariantId") %>'>
                    </nopCommerce:ProductAttributes>
                </div>
                <div class="clear">
                </div>
                <asp:Panel ID="pnlStockAvailablity" runat="server" class="stock">
                    <asp:Label ID="lblStockAvailablity" runat="server">
                    </asp:Label>
                </asp:Panel>
                <asp:PlaceHolder runat="server" ID="phSKU">
                    <div class="clear">
                    </div>
                    <div class="sku">
                        <%=GetLocaleResourceString("Products.SKU")%> <asp:Literal runat="server" ID="lSKU" />
                    </div>
                </asp:PlaceHolder>
                <div class="clear">
                    </div>
                 <nopCommerce:GiftCardAttributes ID="ctrlGiftCardAttributes" runat="server" ProductVariantId='<%#Eval("ProductVariantId") %>' />
                 <div class="clear">
                    </div>
               <div class="price">
                    <nopCommerce:ProductPrice1 ID="ctrlProductPrice" runat="server" ProductVariantId='<%#Eval("ProductVariantId") %>' />
                    <nopCommerce:DecimalTextBox runat="server" ID="txtCustomerEnteredPrice" Value="1"
                        RequiredErrorMessage="<% $NopResources:Products.CustomerEnteredPrice.EnterPrice %>"
                        MinimumValue="0" MaximumValue="100000000" Width="100" />
                </div> 
                  <div class="attributeCheckout"><%=GetLocaleResourceString("OsShop.Quantity")%>:&nbsp;&nbsp;  <input type="text" id="Quantity" onchange="quantityValue(<%#Eval("ProductVariantId")%>,this)" value="1" style="width:50px; border: solid 1px #eee;"/>
                    <input type="button" value="Add to Cart" onclick="attribute(<%#Eval("ProductVariantId")%>);" class="productvariantaddtocartbutton"/>
                    
                </div>
                  <div class="add-info" style="display: none;">
                    <nopCommerce:NumericTextBox runat="server" ID="txtQuantity" Value="1" RequiredErrorMessage="<% $NopResources:Products.EnterQuantity %>"
                        RangeErrorMessage="<% $NopResources:Products.QuantityRange %>" MinimumValue="1"
                        MaximumValue="999999" Width="50"></nopCommerce:NumericTextBox>
                    <asp:Button ID="btnAddToCart" runat="server" Text="<% $NopResources:Products.AddToCart %>"
                        CommandName="AddToCart" CommandArgument='<%#Eval("ProductVariantId")%>' CssClass="productvariantaddtocartbutton">
                    </asp:Button>
                    <asp:Button ID="btnAddToWishlist" runat="server" Text="<% $NopResources:Wishlist.AddToWishlist %>"
                        CommandName="AddToWishlist" CommandArgument='<%#Eval("ProductVariantId")%>' CssClass="productvariantaddtowishlistbutton">
                    </asp:Button>
                </div>
                 <div class="clear">
                </div>
                 <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="error" />



   
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
             
             </div>
            </div>
            <div class="clearOne"></div>
        </ItemTemplate>
    </asp:Repeater>
</div>
