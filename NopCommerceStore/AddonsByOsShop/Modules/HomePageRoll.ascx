<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageRoll.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageRollRight" %>


<%@ Register TagPrefix="nopCommerce" TagName="HomePageProducts" Src="/AddonsByOsShop/Modules/HomePageProducts.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="BestSellers" Src="/AddonsByOsShop/Modules/BestSellers.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RecentlyAddedProducts" Src="/AddonsByOsShop/Modules/RecentlyAddedProducts.ascx" %>

<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/homepageroll.js"></script>
<link type='text/css' href='/AddonsByOsShop/Styles/homepageRoll.css' rel='stylesheet'  /> 
    <div class="clear"></div>
<div class="homepage_products">

            <ul class="homepage_roll_nav">
                <li class="HomePageProducts-button"><%=GetLocaleResourceString("HomePage.FeaturedProducts")%></li>
                <li class="RecentlyAddedProducts-button"><%=GetLocaleResourceString("Products.NewProducts")%></li>
                <li class="BestSellers-button"><%=GetLocaleResourceString("Reports.BestSellingProducts")%></li>
            </ul>
            <div class="clear"></div>
        	<div class="Products_home">
        		<div class="HomePageProducts_home">
        			<nopCommerce:HomePageProducts ID="ctrlHomePageProducts" runat="server" />
        		</div>
        		 <div class="RecentlyAddedProducts_home">
                    <nopCommerce:RecentlyAddedProducts ID="ctrlRecentlyAddedProducts" runat="server" />
        		 </div>
        		 <div class="BestSellers_home" >
        		    <nopCommerce:BestSellers ID="ctrlBestSellers" runat="server" />
        		 </div>
          </div>
 </div>