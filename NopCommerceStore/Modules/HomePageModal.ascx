<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageModal.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageModal" %>

<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/jquery.simplemodal.js"></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/osx.js"></script>
<script type="text/javascript" src="../Scripts/HomePageModal.js"></script>
<link type="text/css" href="../AddonsByOsShop/Styles/osx.css" />
<style type="text/css" >
    p {text-indent : 20px; text-align:justify;}
    hr 
    {	
        border:none;	
        border-top:1px #CCCCCC solid;
        height: 1px;
    }
</style>

<div id="divShowSplashMessage" style="display:none;" class="showSplashPanel">
    <a class="simplemodal-close" href="#" style="font-size:14pt;float:left;">Close</a>
    <div class="clear"></div>
    <img alt="Click to browse the site!" src="../images/WeWorkingHard.png" id="productShortShowContent"/>
</div>

<input type="hidden" id="hidShowHomePageCount" runat="server" value="0" />