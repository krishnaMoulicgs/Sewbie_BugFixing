function ShowProductsshort_button(obj) {
    //$('.item-box div img').addClass('highlight');
};
function HideProductsshort_button(obj) {
    //$('.item-box div img').removeClass('highlight');
};


var AjaxProductList = {
    createProductid: function (id1) {
        var id = id1;
        var queryString = { productListTransId: id };
        return queryString;
    },
    pictureTrans: function (trans) {
        var str = new Array();
        str = allPicture.split("|");
        $(".ProductShortShow_img").empty();
        $(".ProductShortShow_img").hide();
        $(".ProductShortShow_img").fadeIn(500);
        $(".ProductShortShow_img").append("<img src=" + str[parseInt(trans)] + "></img>");

    },

    getPageContent: function (proudctId) {

        jQuery.ajax({

            url: "/AddonsByOsShop/AjaxPages/getProductShortList.aspx?" + new Date().getTime(),
            type: "Get",
            data: this.createProductid(proudctId),
            beforeSend: function () {
                //$("#osx-modal-data  .ProductShortShow_move").css("display","none");
                //$("#osx-modal-data  input", self.container).hide();
                $("#osx-container").find("#ProductShortShow_LeftRight").hide();
                $("#osx-container").find("#ProductShortShow_move").hide();
                $("#osx-modal-data  #ProductListMessage", self.container).html("<div class='ProductShortShow_load'></div>");

            },
            complete: function () {
                //$("#osx-modal-data  .ProductShortShow_move").css("display", "block");
                //$("#osx-modal-data  input", self.container).show();
                $("#osx-container").find("#ProductShortShow_LeftRight").show();
                $("#osx-container").find("#ProductShortShow_move").show();

            },
            success: function (data, statue) {
                //$("#ProductListMessage").html("Preliminary OK");
                
                var productMessage = "", defalutPicture = "", OtherPicture = "";
                dataProduct = data + "";
                var strItem = new Array();
                var str = new Array();
                strItem = dataProduct.split("<!>");
                //Alex Garcia 8/16/2011 remove buy now from quick look
                productMessage = "<div class='ProductShortShow_Message'>" + "<div class='ProductShortShow_name'>" + "<a href='" + strItem[3] + "'>" + strItem[0] + "</a>" + "</div>" + "<div class='ProductShortShow_price'>" + strItem[2] + "</div>" + "<div class='ProductShortShow_describe'>" + strItem[1] + "</div>" + "<div class='ProductShortShow_buy' style='display:none'> " + "<a href='" + strItem[3] + "'>" + shortShowBuyNow() + "</a>" + "</div><div class='clear'></div>" + "</div>";
                str = strItem[4].split("|");

                allPicture = strItem[4];

                for (var i = 0; i < str.length - 1; i++) {
                    if (i == 0) {
                        defalutPicture = "<div class='ProductShortShow_img'>" + "<img src='" + str[0] + "' ></img>" + "</div>";
                    }

                    OtherPicture += "<li class='lvProductPictures_smal' >" + "<img onclick='AjaxProductList.pictureTrans(" + i + ")' src='" + str[i] + "' ></img>" + "</li>";

                    //productMessage += "<div class='ProductShortShow_img'>" + "<img src='" + str[i] + "' />" + "</div>";
                }
                productMessage += defalutPicture;
                productMessage += "<div class='ProductShortShow_imgss'><div class='mainDiv' id='bigDiv'><ul id='myList'>" + OtherPicture + "</ul></div></div>";

                //$("#osx-modal-data #ProductListMessage", self.container).html(productMessage);
                $("#osx-container").find("#ProductListMessage").html(productMessage);



            }
        });
    }
};

var proId = "", allProId = "";
function sunTest(productId, allProductId) {
    proId = productId;
    allProId = allProductId;

    $("#osx-modal-data  #moveLeft", self.container).show();
    $("#osx-modal-data  #moveRight", self.container).show();
    var str = new Array();
    str = allProId.split("|");
    for (var i = 0; i < str.length - 1; i++) {
        if (str[i] == proId) {

            if (i == 0) {
                //Hide            
                $("#osx-modal-data  #moveLeft", self.container).hide();
            }
            if (i == (str.length - 2)) {
                //alert("ii=:" + (str.length - 2));
                //Hide
                $("#osx-modal-data  #moveRight", self.container).hide();
            }
            break;
        }
    }


    AjaxProductList.getPageContent(productId);


}

function productLeft() {

    var str = new Array();
    str = allProId.split("|");
    for (var i = 0; i < str.length - 1; i++) {
        if (str[i] == proId) {
            if (i != 0) {
                proId = str[i - 1];
                $("#osx-container").find("#osx-modal-data  #moveLeft", self.container).show();
                $("#osx-container").find("#osx-modal-data  #moveRight", self.container).show();
                if ((i - 1) == 0)
                    $("#osx-container").find("#osx-modal-data  #moveLeft", self.container).hide();
                AjaxProductList.getPageContent(str[i - 1]);
            }
            else {
                //Hide
                $("#osx-container").find("#osx-modal-data  #moveLeft", self.container).hide();
            }
            break;
        }
    }
}
function productRight() {

    var str = new Array();
    str = allProId.split("|");
    for (var i = 0; i < str.length - 1; i++) {
        if (str[i] == proId) {
            if (i != (str.length - 2)) {
                proId = str[i + 1];
                $("#osx-container").find("#osx-modal-data  #moveLeft", self.container).show();
                $("#osx-container").find("#osx-modal-data  #moveRight", self.container).show();
                if ((i + 1) == (str.length - 2))
                    $("#osx-container").find("#osx-modal-data  #moveRight", self.container).hide();
                AjaxProductList.getPageContent(str[i + 1]);
            } else {
                //Hide
                $("#osx-container").find("#osx-modal-data  #moveRight", self.container).hide();
            }
            break;
        }
    }
};





















//产品小图的左右滚动点击
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
    listId = $("#osx-modal-data #myList");
    bWidth = bId.width();
    listLength = bId.find("li").length;
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
        $("#osx-modal-data input#btnNext").attr("disabled", "disabled");
        $("#osx-modal-data input#btnNext").css("background", "url(../App_Themes/Sewbie/images/buttons_right.gif) no-repeat center center");
        $("#osx-modal-data input#btnPre").css("background", "url(../App_Themes/Sewbie/images/buttons_left.gif) no-repeat center center");
        $("#osx-modal-data input#btnPre").attr("disabled", "");
    } else if (mr == 0) {
        //$("#osx-modal-data input#btnPre").attr("disabled", "disabled");
        $("#osx-modal-data input#btnNext").css("background", "url(../App_Themes/Sewbie/images/buttons_right.gif) no-repeat center center");
        $("#osx-modal-data input#btnPre").css("background", "url(../App_Themes/Sewbie/images/buttons_left.gif) no-repeat center center");
        $("#osx-modal-data input#btnNext").attr("disabled", "");
    } else {
        $("#osx-modal-data input#btnNext").attr("disabled", "");
        $("#osx-modal-data input#btnNext").css("background", "url(../App_Themes/Sewbie/images/buttons_right.gif) no-repeat center center");
        $("#osx-modal-data input#btnPre").css("background", "url(../App_Themes/Sewbie/images/buttons_left.gif) no-repeat center center");
        $("#osx-modal-data input#btnPre").attr("disabled", "");
    }


}

$(function () {
    picList();
})

