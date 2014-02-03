/**

*/
(function($){$.fn.hoverIntent=function(f,g){var cfg={sensitivity:7,interval:100,timeout:0};cfg=$.extend(cfg,g?{over:f,out:g}:f);var cX,cY,pX,pY;var track=function(ev){cX=ev.pageX;cY=ev.pageY;};var compare=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);if((Math.abs(pX-cX)+Math.abs(pY-cY))<cfg.sensitivity){$(ob).unbind("mousemove",track);ob.hoverIntent_s=1;return cfg.over.apply(ob,[ev]);}else{pX=cX;pY=cY;ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}};var delay=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);ob.hoverIntent_s=0;return cfg.out.apply(ob,[ev]);};var handleHover=function(e){var p=(e.type=="mouseover"?e.fromElement:e.toElement)||e.relatedTarget;while(p&&p!=this){try{p=p.parentNode;}catch(e){p=this;}}if(p==this){return false;}var ev=jQuery.extend({},e);var ob=this;if(ob.hoverIntent_t){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);}if(e.type=="mouseover"){pX=ev.pageX;pY=ev.pageY;$(ob).bind("mousemove",track);if(ob.hoverIntent_s!=1){ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}}else{$(ob).unbind("mousemove",track);if(ob.hoverIntent_s==1){ob.hoverIntent_t=setTimeout(function(){delay(ev,ob);},cfg.timeout);}}};return this.mouseover(handleHover).mouseout(handleHover);};})(jQuery);

eval(function(p,a,c,k,e,r){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)r[e(c)]=k[c]||e(c);k=[function(e){return r[e]}];e=function(){return'\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}('(b($){$.m.E=$.m.g=b(s){h($.x.10&&/6.0/.I(D.B)){s=$.w({c:\'3\',5:\'3\',8:\'3\',d:\'3\',k:M,e:\'F:i;\'},s||{});C a=b(n){f n&&n.t==r?n+\'4\':n},p=\'<o Y="g"W="0"R="-1"e="\'+s.e+\'"\'+\'Q="P:O;N:L;z-H:-1;\'+(s.k!==i?\'G:J(K=\\\'0\\\');\':\'\')+\'c:\'+(s.c==\'3\'?\'7(((l(2.9.j.A)||0)*-1)+\\\'4\\\')\':a(s.c))+\';\'+\'5:\'+(s.5==\'3\'?\'7(((l(2.9.j.y)||0)*-1)+\\\'4\\\')\':a(s.5))+\';\'+\'8:\'+(s.8==\'3\'?\'7(2.9.S+\\\'4\\\')\':a(s.8))+\';\'+\'d:\'+(s.d==\'3\'?\'7(2.9.v+\\\'4\\\')\':a(s.d))+\';\'+\'"/>\';f 2.T(b(){h($(\'> o.g\',2).U==0)2.V(q.X(p),2.u)})}f 2}})(Z);',62,63,'||this|auto|px|left||expression|width|parentNode||function|top|height|src|return|bgiframe|if|false|currentStyle|opacity|parseInt|fn||iframe|html|document|Number||constructor|firstChild|offsetHeight|extend|browser|borderLeftWidth||borderTopWidth|userAgent|var|navigator|bgIframe|javascript|filter|index|test|Alpha|Opacity|absolute|true|position|block|display|style|tabindex|offsetWidth|each|length|insertBefore|frameborder|createElement|class|jQuery|msie'.split('|'),0,{}));





$(document).ready(function() {
    renderMenu();
    if ($("#searchForm .search-box").val() !== "") {
        $("#searchForm label").hide();
    }
    $("#searchForm label").click(function() {
        $(this).fadeOut();
        $("#searchForm .search-box").focus();
    });
    $("#searchForm .search-box").focus(function() {
        $("#searchForm label").fadeOut();
    });
    $("#searchForm .search-box").blur(function() {
        if ($("#searchForm .search-box").val() === "") {
            $("#searchForm label").fadeIn();
        }
    });
    $("#searchForm .go").mousedown(function() {
        if ($("#searchForm .search-box").val() === "") {
            $("#searchForm .search-box").focus();
            return false;
        }
    });
    $("#searchForm .go").click(function() {
        if ($("#searchForm .search-box").val() === "") {
            return false;
        }
    });
});


function renderMenu(e) {

    siteWidth = $("body").width();
    parentOffsetLeft = $('#floor_nav').offset().left;

    $(".sub-floor-menus").each(function() {
        $(this).css("width", $(this).width()); //For IE6
    });
	$(".menu").removeClass("css-menu"); //remove css nav functionality
	$("#floor_nav ul.floors > li").each(function (i) {
	    offset = $(this).offset(); //calculate the left placement for the menus based on the bottom left corner of the matching top floor buttons
	    menu = $('#sub_' + this.id);
	    menu.hide().css("width", "").css("width", menu.width()); //this prevents the wrapping which occours in the css version of the nav
	    leftPosition = offset.left - parentOffsetLeft; //find the sub menu and reposition relative to the parent

	    if (leftPosition < 0) {
	        leftPosition = offset.left - parentOffsetLeft;
	    }

	    //Check if the sub menu is too wide to fit in the site wrapper
	    if (menu.width() + leftPosition > siteWidth) {
	        leftPosition = offset.left - parentOffsetLeft + $(this).width() - menu.width() + parseInt($(this).find(".sub-menu-wrapper-right").css("padding-right")); //find the sub menu and reposition relative to the parent

	        if (leftPosition < 0) {
	            //leftPosition = offset.left - parentOffsetLeft + ($(this).width() / 2) - (menu.width() / 2);
	            leftPosition = 0;
	        }
	    }
	    menu.css("left", leftPosition + "px");
	    menu.bgiframe(); //fix for ie6
	    $(this).dropdown({ menu: menu, type: 'slide', speedIn: 300, speedOut: 1 });
	});
}

(function($) {
    $.fn.dropdown = function(opt) {

        var defaults = {
            speedIn: 200,
            speedOut: 200,
            delay: 50,
            type: 'slide',
            transIn: { opacity: 'show' },
            transOut: { opacity: 'hide' }
        };

        var options = $.extend(defaults, opt);

        var menu = $(options.menu);
        var speedIn = options.speedIn;
        var speedOut = options.speedOut;
        var delay = options.delay;
        var type = options.type;
        var transIn = options.transIn;
        var transOut = options.transOut;

        var menuOver = false;
        var buttonOver = false;

        //find top level anchor
        var topLevelAnchor = $(this).find('a')[0];

        var topNavItem = "";

        var topNavConfig = {
            sensitivity: 20, // number = sensitivity threshold (must be 1 or higher)    
            interval: 50, // number = milliseconds for onMouseOver polling interval    
            over: topNavOver, // function = onMouseOver callback (REQUIRED)    
            timeout: 200, // number = milliseconds delay before onMouseOut    
            out: topNavOut // function = onMouseOut callback (REQUIRED)    
        };

        $(menu).hide();
        $(this).hoverIntent(topNavConfig);

		/* Basic version for touch screens */
		$(this).bind("touchstart",function(){
			$(menu).show();
		});
		$(this).bind("touchend",function(){
			setTimeout(function(){
				$(menu).hide();
			},3000);
		});

        function topNavOver() {
            if (menu.is(':animated')) { return };
            switch (type) {
                case 'slide':
                    $(menu).slideDown(speedIn);
                    break;
                case 'fade':
                    $(menu).fadeIn(speedIn);
                    break;
                case 'simple':
                    $(menu).show();
                    break;
                case 'blind':
                    $(menu).show("blind", { direction: "vertical" }, 200);
                    break;
                case 'custom':
                    $(menu).animate(transIn, speedIn);
            }
            buttonOver = true;
            $(topLevelAnchor).addClass("active");
        }

        function topNavOut() {
            buttonOver = false;
            setTimeout(function() {
                if (menuOver == false && buttonOver == false) {
                    switch (type) {
                        case 'slide':
                            $(menu).slideUp(speedOut);
                            break;
                        case 'fade':
                            $(menu).fadeOut(speedOut);
                            break;
                        case 'simple':
                            $(menu).hide();
                            break;
                        case 'blind':
                            $(menu).hide("blind", { direction: "vertical" }, 200);
                            break;
                        case 'custom':
                            $(menu).animate(transOut, speedOut)
                    }
                    $(topLevelAnchor).removeClass("active");
                }
            }, delay);

        }
    }
})

(jQuery);