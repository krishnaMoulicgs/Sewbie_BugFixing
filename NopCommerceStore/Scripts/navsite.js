
$(document).ready(function() {

    $('#nav li._nav').hover(function () {
        //alert($('ul.level2 li.level2', this).length);
        if ($('ul._nav1 li._nav1', this).length < 8) {
            $('ul._nav1', this).addClass('lv2smallSub');
        }
        $("._nav1", this).addClass("navShow");
    }, function () {
        $("._nav1", this).removeClass("navShow");
    });

    $('#nav li ul._nav1 li').hover(function () {
        $("._nav2", this).addClass("navShow");
    }, function () {
        $("._nav2", this).removeClass("navShow");
    });

    $('#nav li._nav:gt(3)').addClass('endChild');

 });