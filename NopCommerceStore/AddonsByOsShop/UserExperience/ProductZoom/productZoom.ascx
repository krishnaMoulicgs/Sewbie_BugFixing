<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="productZoom.ascx.cs" Inherits="NopSolutions.NopCommerce.Web.Templates.Products.WebUserControl1" %>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/productzoom.mousewheel.min.js" ></script>
<script type="text/javascript" src="/AddonsByOsShop/CustomScripts/productzoom.iviewer.js" ></script>

<script language="javascript" type="text/javascript">
    var sWidth = 90;
    var visible = 3;
    var mr = 0;
    var bWidth;
    var listLength;
    var listWidth;
    var listLeft;
    var bId;
    var listId;
    var trendLeft;
    var maxMr;
    function init() {
        bId = $("#bigDiv");
        listId = $("#myList");
        bWidth = bId.width();
        listLength = listId.find("li").length;
        listWidth = listLength * sWidth;
        listLeft = parseInt(listId.css('left'));
    }
    function picList(fx) {
        init();
        maxMr = listLength - visible;
        if (listWidth > bWidth) {
            if (fx == 'next') {
                if (-mr < maxMr) {
                    mr--;
                    trendLeft = mr * sWidth;
                    listId.animate({
                        left: trendLeft + "px"
                    }, 200);
                }
            } else if (fx == 'pre') {
                if (mr < 0) {
                    mr++;
                    trendLeft = mr * sWidth;
                    listId.animate({
                        left: trendLeft + "px"
                    }, 200);
                }
            }
        }

        if (-mr == maxMr) {
            $("input#btnNext").attr("disabled", "disabled");
            $("input#btnNext").css("background", "#fff");
            $("input#btnPre").css("background", "url(../App_Themes/Sewbie/images/buttons_left.gif) no-repeat center center");
            $("input#btnPre").attr("disabled", "");
        } else if (mr == 0) {
            $("input#btnPre").attr("disabled", "disabled");
            $("input#btnNext").css("background", "url(../App_Themes/Sewbie/images/buttons_right.gif) no-repeat center center");
            $("input#btnPre").css("background", "#fff");
            $("input#btnNext").attr("disabled", "");
        } else {
            $("input#btnNext").attr("disabled", "");
            $("input#btnNext").css("background", "url(../App_Themes/Sewbie/images/buttons_right.gif) no-repeat center center");
            $("input#btnPre").css("background", "url(../App_Themes/Sewbie/images/buttons_left.gif) no-repeat center center");
            $("input#btnPre").attr("disabled", "");
        }
        var pnum = $("#myList > li").size();
        if (pnum < 4) {
            $(".lvProductPictures_smal_prev").css("background", "#fff");
            $(".lvProductPictures_smal_next").css("background", "#fff");
        }
    }

    $(function () {
        picList();
    })
</script>
<script type="text/javascript">
    var $ = jQuery;
    var iviewer = {};
    function sun(img) {
        iviewer.loadImage(img);
        return false;
    };
    $(document).ready(
    function () {
        var imgMain = document.getElementById('<%=defaultImage.ClientID%>');
        var imgUrl = imgMain.src;
        $("#viewer2").iviewer(
        {
            src: imgUrl,
            initCallback: function () {
                iviewer = this;
            }
        });
    });
</script>




        <script language="javascript" type="text/javascript">
            function UpdateMainImage(url) {
                var imgMain = document.getElementById('<%=defaultImage.ClientID%>');
                imgMain.src = url;
            }
        </script>

        <div class="picture">
            <a style="display:none;" runat="server" id="lnkMainLightbox">
                <asp:Image ID="defaultImage" runat="server" />
            </a> 
                <div class="wrapper">
                <div id="viewer2" class="viewer"></div>
                <div class="buttons_pic">
                <div class="iviewer_zoom_in iviewer_common iviewer_button "></div>
                <div class="iviewer_zoom_out iviewer_common iviewer_button "></div>
                <div class="iviewer_zoom_zero iviewer_common iviewer_button "></div>
                <div class="iviewer_zoom_fit iviewer_common iviewer_button "></div>
                <div class="iviewer_zoom_status iviewer_common "></div>
                </div>
                <br/>
                </div>
             <input type="button" class="lvProductPictures_smal_prev"  onclick="picList('pre')" value="" id="btnPre"  />
               <div class="mainDiv" id="bigDiv">
               <asp:ListView ID="lvProductPictures" runat="server" GroupItemCount="3">
                <LayoutTemplate>
                    <div style="width:100%;">
                     <ul id="myList">
                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                     </ul>
                    </div>
                </LayoutTemplate>
                <GroupTemplate>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </GroupTemplate>
                <ItemTemplate>
                    <li class="lvProductPictures_smal" >
                        <a href="#" onclick="sun('<%#PictureService.GetPictureUrl((Picture)Container.DataItem)%>')">
                            <img src="<%#PictureService.GetPictureUrl((Picture)Container.DataItem, 70)%>" alt="Product image" />
                        </a>
                    </li>
                </ItemTemplate>
                
            </asp:ListView>
            </div> 
                <input type="button" class="lvProductPictures_smal_next"  onclick="picList('next')" value="" id="btnNext" />
        </div>
    

   