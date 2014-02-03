
(function($){
    $.fn.jufade = function (options) {

        var opts = $.extend({}, $.fn.jufade.defaults, options);

        return this.each(function(){
            var $this = $(this);
            var scrolling;
            var max = 0;
            var stop = 0;
            (function jusetUp(){
                //calculate max
                $(opts.items).each(function(i){
                    max = $(opts.items).eq(i).position().left + $(opts.items).eq(i).outerWidth() > max? $(opts.items).eq(i).position().left + $(opts.items).eq(i).outerWidth() : max;
                });
                    stop = $this.width() - max;
                $(opts.items).wrapAll('<div id="juCover" style="position:absolute; left:0px, width:' + max + '" />')
            })();
              function moveMe(obj, dir, x){
                    if (!scrolling) {
                        obj.stop(true,true);
                    } else {
                        if(x =="left"){
                       $("#juCover").animate(
                            {"left": "0px"},1200
                       );
                        }else{
					       $("#juCover").animate({
                            "left": "-417px"
                        },1200);
                        }
                    }
            }

            $this.bind("hover mousemove" , function(e){
                $('#juCover').stop();
                x = e.pageX - $this.offset().left;
                m = x < opts.left? 50:-50;
                if(x < opts.left){
                           scrolling = true;
                           moveMe($('#juCover'),m, "left");
                }else if(x > $this.width() - opts.right){
                        scrolling = true;
                        moveMe($('#juCover'),m, "right");
                }else{
                     scrolling = false;
                     $('#juCover').stop(true,true).dequeue();

                }
            },function(){
                scrolling = false;
            });
            $this.mouseleave(function(){
                 scrolling = false;
                $(opts.items, $this).fadeTo(opts.speed, 1);
            });

             $(opts.items).mouseenter( function(e){  
                         $(opts.items).not($(this)).animate({
                             "opacity": opts.opacity
                         },opts.speed);
                         $(this).animate({
                             "opacity": 1
                         }, opts.speed);
                       
                });
                $(opts.items).mouseleave(function(e){
                    $(opts.items).dequeue();
                    $(this).stop(true,true);
                });
         
        }); // This  each

    }; // Return anon function

    $.fn.jufade.defaults = { // So defaults can be overriden on global scale
        items: '.item',
        opacity: 0.5,
        speed:"fast",
        left: 120,
        right:120
    };

    $(document).ready(function () {
        $("#cont").jufade({ items: ".itemm" });
    });

})(jQuery);


