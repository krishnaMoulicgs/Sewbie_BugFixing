<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MiniShoppingBox.ascx.cs"
 Inherits="NopSolutions.NopCommerce.Web.AddonsByOsShop.Modules.MiniShoppingBox1" %>

<link id="ctl00_layoutCss" rel="stylesheet" type="text/css" href="/AddonsByOsShop/Styles/layout.css" />

<script type="text/javascript">
    //Shopping cart display localized
    function ShowMiniShopping(count, allmoney) {
        var str = '<%=GetLocaleResourceString("OsShop.ShowMiniShoppingCart")%>';
        str = str.replace("{0}", count);
        str = str.replace("{1}", allmoney);
        return str;
    }
	
	function getQty(){
		return '<%=GetLocaleResourceString("ShoppingCart.Quantity")%>';
	}
	function tips(){
		return '<%=GetLocaleResourceString("OsShop.tips")%>';
	}
	
	function searchtips(){
		return '<%=GetLocaleResourceString("OsShop.searchtips")%>';
	}

</script>
<script type="text/javascript" src="../../Scripts/jquery-migrate-1.2.1.min.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/MiniShoppingCartEffect.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/MiniShopping.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/attributeValue.js"></script>

<script type="text/javascript"">
    //Initialization Cart
    this.AjaxTemplate.InitializationCart();
</script>

<div class="site-wrapper">
    <ul class="actions">
        <li class="minishoppinginput">
            <input id="openMiniBasketOnHover" value="true" type="hidden"/> 
            <input id="miniBasketDisabledOrEnabledMvt" value="true" type="hidden"/>  
        </li>
       
        <li id="miniBasket">
        <!--This shows the number and total shopping cart price-->
        <a class="have_items" >
            <span id="MiniShoppingShow">
            </span>
        </a> 
     
        <div style="display: none" class="mini-bag-wrapper">
            <div class="mini-header"></div>
            <div class="mini-bag-wrapper-top">
                <div class="mini-bag-wrapper-bottom">
                    <div id="mini-bag-body" class="mini-bag-wrapper-right">

                    <!--There are no products to display start -->   
                    <div class="empty body">
		                <div class="emptycart">
                            <a><%=GetLocaleResourceString("ShoppingCart.CartIsEmpty")%></a>
                        </div> 
                        <div class="emptybottom"></div> 
                    </div>
                    <!--There are no products to display end -->             

                    <!-- Show Shopping Cart -->                    
                    <div style="display: block" class="full body" >
                        <!--Show Shopping Cart (products list) start --> 
                        <div class="items" >
                            <div style="top: 0px" class="itemsClmn" >
                            <!--Shows the number of a three-->
                            <!--Buy goods in MiniShopping display dynamic loading-->
                            </div>
                        
                            <div style="clear: both"></div>
                        </div>   
                        <!--Show Shopping Cart (products list)end --> 
		                <div class="MiniShoppingCart_bottom">
		                    <div class="MiniShoppingCart_bottom_buttons">
                                <div style="display: block;" class="prev disabled" ></div>
                                <div style="display: block;" class="next" ></div>
                            </div>
            
                            <div class="summary">
                                <div class="total-price">
                                    <!--Display the total price of goods-->
                                    <div id="totalMoney"></div>
                                </div>
                                <div class="total-label"><%=GetLocaleResourceString("ShoppingCart.ItemTotal")%>&nbsp;  </div>
                            </div>
 
                            <div id="minibagLinks" class="links">
                                <a class="replace go-to-bag" href="<%=Page.ResolveUrl("~/ShoppingCart.aspx")%>" ><span><%=GetLocaleResourceString("OsShop.ViewAll")%></span></a> 
                                <a class="replace proceed-to-checkout" style="display:none;"   href="<%=Page.ResolveUrl("~/ShoppingCart.aspx")%>" ><span><%=GetLocaleResourceString("MiniShoppingCartBox.CheckoutButton")%></span></a> 
                            </div>
                            <div>           
                            </div>          
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>
        </li>
    </ul>
            
</div>
