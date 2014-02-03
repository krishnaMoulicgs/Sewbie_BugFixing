
var sign = 0;
var count = 0;

$(document).ready(
   function () {
       var signA = 0;

       $("#showSearchResult").hide();
       $(".showSearchResult_loading").css("display", "none");
       $(".searchbox_text").click(function () {
           $(this).fadeOut(300);
           $("#txtSearchTerms").focus();
       });

       //$(".searchbox_text").bind("onkeyup", findProducts);
       //$(".searchbox_text").bind("onkeydown", txtSearch_onKeyDown);

       $("input#txtSearchTerms,input#btnSearch,div#showSearchResult,.searchbox_text").click(
            function () {
                signA = 1
            }
           );

       $(document).click(
            function () {
                if (signA == 1) {
                    signA = 0;
                }
                else {
                    //$("#txtSearchTerms").val("");
                    //$("#showSearchResult").fadeOut(100);
                    //$("#showSearchResult_remove").fadeOut(100);
                    //$(".searchbox_text").fadeIn(300);

                    $("#showSearchResult").hide();
                    $("#showSearchResult_remove").hide();
                    $(".searchbox_text").show();
                }
            }
           );



   });


function onfocusValue() {
    if ($("#txtSearchTerms").val("") != null)
        //$("#txtSearchTerms").val("");
		//$(".searchbox_text").fadeOut(300);
        $(".searchbox_text").hide();
		
}

function onblurValue() {
   
    if (sign == 1 || count == 0){
       //$("#txtSearchTerms").val(searchstore());
        //$(".searchbox_text").fadeIn(300);
        $(".searchbox_text").show();
		$("#txtSearchTerms").val("");
		}
      
}

function txtSearch_onKeyDown(){
    switch(event.keyCode){
        case 13: 
            searchPage();
            break;
    }
}


function findProducts() {

    if ($("#txtSearchTerms").val().length >= 3) {
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getSearchProducts.aspx?" + new Date().getTime(), //joined url to prevent re-running ajax
            type: "Get",
            data: 'txtSearchTerm=' + $("#txtSearchTerms").val(),
            beforeSend: function () {
                $(".showSearchResult_loading").css("display", "block");
            },
            success: function (data, statue) {

                var dataProduct = data + "";
                var strItem = new Array();
                var str = new Array();
                strItem = dataProduct.split("<!>"); //Between Each <!> Split
                if (strItem.length > 1) {
                    $("#showSearchResult").empty(); //Clear the previous div
                    count++; sign = 0;
                    $("#showSearchResult").append("<div class='showSearchResult_tips'><a>" + searchtips() + "</a></div>");
                    for (var i = 0; i < strItem.length - 1; i++) {
                        var re = new RegExp();
                        re = new RegExp($("#txtSearchTerms").val(), "gi"); //the second parameter, case-insensitive match

                        str = strItem[i].toString().split("|"); //The various attributes of the goods | number separated
                        var send = "<div class='showSearchResultBox'>" + "<div class='showSearchResult_left'>" + "<a href='" + str[3] + "'>" + "<img src=" + str[1] + "></img>" + "</a>" + "<b>" + "<a href='" + str[3] + "'>" + str[0].replace(re, "<font color='#02b6ff'>" + $("#txtSearchTerms").val() + "</font>") + "</a>" + "</b>" + "<p>" + str[2] + "</p>" + "</div>" + "<div class='showSearchResult_right'>" + "<p>" + str[4].replace(re, "<font color='#02b6ff'>" + $("#txtSearchTerms").val() + "</font>") + "</p>" + "</div>" + "</div>" + "<div class='clear'></div>";
                        $("#showSearchResult").append(send); //Display various items
                    }
                    $("#showSearchResult").fadeIn(200);
                    $("#showSearchResult_remove").fadeIn(200);
                    $(".showSearchResult_loading").css("display", "none");

                }
                else {
                    sign = 1;
                    $("#showSearchResult").hide();
                    $("#showSearchResult_remove").hide();
                    $(".showSearchResult_loading").css("display", "none");
                }


            }

        });
    }
    else {
        sign = 1;
        $("#showSearchResult").hide();
        $("#showSearchResult_remove").hide();
        $(".showSearchResult_loading").css("display", "none");
    }
}