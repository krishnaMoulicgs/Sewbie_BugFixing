<%@ Page Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true" ViewStateMode="Disabled"
    Inherits="NopSolutions.NopCommerce.Web.Default" CodeBehind="Default.aspx.cs" %>
     
<%@ Register TagPrefix="nopCommerce" TagName="HomePageRoll" Src="~/AddonsByOsShop/Modules/HomePageRoll.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="Topic" Src="~/Modules/Topic.ascx" %>
<asp:Content id="contentcontent" ContentPlaceHolderID="cphHead" runat="server">

    <script type="text/javascript" src="/AddonsByOsShop/CustomScripts/homepagebanner-jquery.js"></script>
    <script type="text/javascript" src="/AddonsByOsShop/CustomScripts/homepagebanner-fade.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    
    <div class="HomepageBannerWrapper">
        
    <div class="flashWrapper">
        <div id="cont">
            <div class="itemm" id="item-1">
                <a href="http://sewbie.com/manufacturer/37-carolina-benoit.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner1.jpg" /></a>
            </div>
            <div class="itemm" id="item-3">
                <a href="http://sewbie.com/manufacturer/18-clo-hair-bows.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner2.jpg" />
                </a>
            </div>
            <div class="itemm" id="item-4">
                <a href="http://sewbie.com/manufacturer/12-hello-again-vintage.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner3.jpg" />
                </a>
            </div>
            <div class="itemm" id="item-5">
                <a href="http://sewbie.com/manufacturer/28-caviar-noir.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner4.jpg" />
                </a>
            </div>
            <div class="itemm" id="item-6">
                <a href="http://sewbie.com/manufacturer/22-alex-afton.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner5.jpg" />
                </a>
            </div>
            <div class="itemm" id="item-7">
                <a href="http://sewbie.com/manufacturer/39-feisty-babies.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner6.jpg" />
                </a>
            </div>
            <div class="itemm" id="item-8">
                <a href="http://sewbie.com/manufacturer/39-feisty-babies.aspx">
                    <img src="/BannerImages/Homepage/homepagebanner7.jpg" />
                </a>
            </div>
        </div>
    </div> 
    </div>

    <nopCommerce:Topic ID="topicHomePageText" runat="server" TopicName="HomePageText"
        OverrideSEO="false"></nopCommerce:Topic>
    <div class="clear">
    </div>
   
    <div class="clear">
    </div>
    <div class="homepage_products">
      <nopCommerce:HomePageRoll ID="ctrlHomePageRoll" runat="server" />
    </div>
</asp:Content>