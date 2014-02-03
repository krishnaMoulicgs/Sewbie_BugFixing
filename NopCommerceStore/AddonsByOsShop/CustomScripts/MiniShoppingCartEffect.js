
(function ($) {

    $.extend($.fn, {
        livequery: function (type, fn, fn2) {
            var self = this, q;

            // Handle different call patterns
            if ($.isFunction(type))
                fn2 = fn, fn = type, type = undefined;

            // See if Live Query already exists
            $.each($.livequery.queries, function (i, query) {
                if (self.selector == query.selector && self.context == query.context &&
				type == query.type && (!fn || fn.$lqguid == query.fn.$lqguid) && (!fn2 || fn2.$lqguid == query.fn2.$lqguid))
                // Found the query, exit the each loop
                    return (q = query) && false;
            });

            // Create new Live Query if it wasn't found
            q = q || new $.livequery(this.selector, this.context, type, fn, fn2);

            // Make sure it is running
            q.stopped = false;

            // Run it immediately for the first time
            q.run();

            // Contnue the chain
            return this;
        },

        expire: function (type, fn, fn2) {
            var self = this;

            // Handle different call patterns
            if ($.isFunction(type))
                fn2 = fn, fn = type, type = undefined;

            // Find the Live Query based on arguments and stop it
            $.each($.livequery.queries, function (i, query) {
                if (self.selector == query.selector && self.context == query.context &&
				(!type || type == query.type) && (!fn || fn.$lqguid == query.fn.$lqguid) && (!fn2 || fn2.$lqguid == query.fn2.$lqguid) && !this.stopped)
                    $.livequery.stop(query.id);
            });

            // Continue the chain
            return this;
        }
    });

    $.livequery = function (selector, context, type, fn, fn2) {
        this.selector = selector;
        this.context = context || document;
        this.type = type;
        this.fn = fn;
        this.fn2 = fn2;
        this.elements = [];
        this.stopped = false;

        // The id is the index of the Live Query in $.livequery.queries
        this.id = $.livequery.queries.push(this) - 1;

        // Mark the functions for matching later on
        fn.$lqguid = fn.$lqguid || $.livequery.guid++;
        if (fn2) fn2.$lqguid = fn2.$lqguid || $.livequery.guid++;

        // Return the Live Query
        return this;
    };

    $.livequery.prototype = {
        stop: function () {
            var query = this;

            if (this.type)
            // Unbind all bound events
                this.elements.unbind(this.type, this.fn);
            else if (this.fn2)
            // Call the second function for all matched elements
                this.elements.each(function (i, el) {
                    query.fn2.apply(el);
                });

            // Clear out matched elements
            this.elements = [];

            // Stop the Live Query from running until restarted
            this.stopped = true;
        },

        run: function () {
            // Short-circuit if stopped
            if (this.stopped) return;
            var query = this;

            var oEls = this.elements,
			els = $(this.selector, this.context),
			nEls = els.not(oEls);

            // Set elements to the latest set of matched elements
            this.elements = els;

            if (this.type) {
                // Bind events to newly matched elements
                nEls.bind(this.type, this.fn);

                // Unbind events to elements no longer matched
                if (oEls.length > 0)
                    $.each(oEls, function (i, el) {
                        if ($.inArray(el, els) < 0)
                            $.event.remove(el, query.type, query.fn);
                    });
            }
            else {
                // Call the first function for newly matched elements
                nEls.each(function () {
                    query.fn.apply(this);
                });

                // Call the second function for elements no longer matched
                if (this.fn2 && oEls.length > 0)
                    $.each(oEls, function (i, el) {
                        if ($.inArray(el, els) < 0)
                            query.fn2.apply(el);
                    });
            }
        }
    };

    $.extend($.livequery, {
        guid: 0,
        queries: [],
        queue: [],
        running: false,
        timeout: null,

        checkQueue: function () {
            if ($.livequery.running && $.livequery.queue.length) {
                var length = $.livequery.queue.length;
                // Run each Live Query currently in the queue
                while (length--)
                    $.livequery.queries[$.livequery.queue.shift()].run();
            }
        },

        pause: function () {
            // Don't run anymore Live Queries until restarted
            $.livequery.running = false;
        },

        play: function () {
            // Restart Live Queries
            $.livequery.running = true;
            // Request a run of the Live Queries
            $.livequery.run();
        },

        registerPlugin: function () {
            $.each(arguments, function (i, n) {
                // Short-circuit if the method doesn't exist
                if (!$.fn[n]) return;

                // Save a reference to the original method
                var old = $.fn[n];

                // Create a new method
                $.fn[n] = function () {
                    // Call the original method
                    var r = old.apply(this, arguments);

                    // Request a run of the Live Queries
                    $.livequery.run();

                    // Return the original methods result
                    return r;
                }
            });
        },

        run: function (id) {
            if (id != undefined) {
                // Put the particular Live Query in the queue if it doesn't already exist
                if ($.inArray(id, $.livequery.queue) < 0)
                    $.livequery.queue.push(id);
            }
            else
            // Put each Live Query in the queue if it doesn't already exist
                $.each($.livequery.queries, function (id) {
                    if ($.inArray(id, $.livequery.queue) < 0)
                        $.livequery.queue.push(id);
                });

            // Clear timeout if it already exists
            if ($.livequery.timeout) clearTimeout($.livequery.timeout);
            // Create a timeout to check the queue and actually run the Live Queries
            $.livequery.timeout = setTimeout($.livequery.checkQueue, 20);
        },

        stop: function (id) {
            if (id != undefined)
            // Stop are particular Live Query
                $.livequery.queries[id].stop();
            else
            // Stop all Live Queries
                $.each($.livequery.queries, function (id) {
                    $.livequery.queries[id].stop();
                });
        }
    });

    // Register core DOM manipulation methods
    $.livequery.registerPlugin('append', 'prepend', 'after', 'before', 'wrap', 'attr', 'removeAttr', 'addClass', 'removeClass', 'toggleClass', 'empty', 'remove');

    // Run Live Queries when the Document is ready
    $(function () { $.livequery.play(); });


    // Save a reference to the original init method
    var init = $.prototype.init;

    // Create a new init method that exposes two new properties: selector and context
    $.prototype.init = function (a, c) {
        // Call the original init and save the result
        var r = init.apply(this, arguments);

        // Copy over properties if they exist already
        if (a && a.selector)
            r.context = a.context, r.selector = a.selector;

        // Set properties
        if (typeof a == 'string')
            r.context = c || document, r.selector = a;

        // Return the result
        return r;
    };

    // Give the init function the jQuery prototype for later instantiation (needed after Rev 4091)
    $.prototype.init.prototype = $.prototype;

})(jQuery);


$(document).ready(function () {


    $('#miniBasket .next').click(function () {
        if ($('#miniBasket .next').hasClass('disabled')) {
            return false;
        } else {
            mini_basket_holder.nextPage();
            this.blur();
            return false;
        }
    });

    $('#miniBasket .prev').click(function () {
        if ($('#miniBasket .prev').hasClass('disabled')) {
            return false;
        } else {
            mini_basket_holder.prevPage();
            this.blur();
            return false;
        }
    });

    $(".remove").livequery("click", function () {
        $(this).parent().prev('.item').fadeOut();
        $(this).parent().fadeOut();
        $(this).hide();
    });


    $('#miniBasket').bind('mouseenter', function () {
        if ($("#openMiniBasketOnHover").val() === "true") {
            mini_basket_holder.maximizeBasket(false);
        }
    });
    $('#miniBasket').hover(function () {
        mini_basket_holder.isMouseOverMiniBasket = true;
    }, function () {
        mini_basket_holder.isMouseOverMiniBasket = false;
    });

});

/**
* Class for handling miniBasket
*
*/
function miniBasketHolder() {
    this.basketLoadingOrHasLoaded = false;
    this.products = [];
    this.qtyPerPage = 3;
    this.currPage = 1;
    this.qtyPage = 1;
    this.minimizing = false;
    this.maximized = false;
    this.paging = false;
    this.minimizeAfterAddTimeout = 4000;
    this.minimizeAfterMouseLeaveTimeout = 500;
    this.productAddedTimeout = this.minimizeAfterAddTimeout;
    this.minimiseBasketTimeout = null;
    this.totalPrice = 0;
    this.totalQuantity = 0;
    this.containsVoucher = false;
    this.errorMessageHideTimeout = null;
    this.openSpeed = 700;
    this.closeSpeed = 500;
    this.itemTemplate = "";
    this.itemHighlight = "#d7d7d7";
    this.itemContainerHeight = 423;
    this.itemHeight = 137;
    this.isMouseOverMiniBasket = false;

}

miniBasketHolder.prototype.applyLayoutTemplate = function (itemData) {
    this.currPage = 1;
    var tpResult = TrimPath.parseTemplate(this.itemTemplate).process(itemData, null);
    $('#miniBasket .body.full .items').html(tpResult);
    $(".item.true").animate({ backgroundColor: mini_basket_holder.itemHighlight }, this.openSpeed);
};







miniBasketHolder.prototype.nextPage = function () {
    if (this.paging) {
        return;
    }
    this.paging = true;
    var self = this;
    this.currPage++;
    //alert("dqy：" + this.currPage);
    var currentTop = parseInt($('#miniBasket .itemsClmn').css('top'), 10);
    if (isNaN(currentTop)) {
        currentTop = 0;
    }
    //alert("Down_currentTop:" + currentTop);
    var newTop = currentTop - mini_basket_holder.itemContainerHeight;
    //alert("Down_newTop:" + newTop);
    //newTop = newTop * 2;
    $('#miniBasket .itemsClmn').animate({ top: newTop + 'px' }, 500, function () { self.paging = false; });
    this.doPageButtonVisibilities();
    return false;
};

miniBasketHolder.prototype.prevPage = function () {
    if (this.paging) {
        return;
    }
    this.paging = true;
    var self = this;
    this.currPage--;
    //alert("dqy：" + this.currPage);
    var currentTop = parseInt($('#miniBasket .itemsClmn').css("top"), 10);
    if (isNaN(currentTop)) {
        currentTop = 0;
    }
    //alert("UP_currentTop:" + currentTop);
    var newTop = currentTop + mini_basket_holder.itemContainerHeight;
    //newTop = currentTop + mini_basket_holder.itemContainerHeight;
    //alert("UP_newTop:" + newTop);
    //newTop = newTop * 2;
    $('#miniBasket .itemsClmn').animate({ top: newTop + 'px' }, 500, function () { self.paging = false; });
    this.doPageButtonVisibilities();
    return false;
};

miniBasketHolder.prototype.doPageButtonVisibilities = function () {
    var pnum = $(".itemsClmn > .item").size();

    var pageSign;
    if (pnum < 3)
        pageSign = 1;
    else {
        if (pnum % 3 == 0) {
            pageSign = pnum / 3;
        }
        else {
            pageSign = parseInt(pnum / 3) + 1;
        }

    }

    if (this.currPage == 1) {//如果当前页是第一页 隐藏向上按钮
        $('.MiniShoppingCart_bottom .prev').css({ "display": "block", "background-image": "url(/AddonsByOsShop/Styles/images/MiniShoppingCart_prev1.png)" });
        $('.MiniShoppingCart_bottom .prev').addClass('disabled');
    }
    else {
        $('.MiniShoppingCart_bottom .prev').css({ "display": "block", "background-image": "url(/AddonsByOsShop/Styles/images/MiniShoppingCart_prev.png)" });
        $('.MiniShoppingCart_bottom .prev').removeClass('disabled');
    }

    if (this.currPage == pageSign) {//如果当前页是最后一页的话 隐藏向下按钮
        $('.MiniShoppingCart_bottom .next').css({ "display": "block", "background-image": "url(/AddonsByOsShop/Styles/images/MiniShoppingCart_next1.png)" });
        $('.MiniShoppingCart_bottom .next').addClass('disabled');
    }
    else {
        $('.MiniShoppingCart_bottom .next').css({ "display": "block", "background-image": "url(/AddonsByOsShop/Styles/images/MiniShoppingCart_next.png)" });
        $('.MiniShoppingCart_bottom .next').removeClass('disabled');
    }

};


miniBasketHolder.prototype.doError = function (errorMessage) {
    $('#miniBasketError').html(errorMessage);
    $('#miniBasketError').show();
    $(".add-to-bag-notification").html("");
    mini_basket_holder.maximizeBasket(true);
    this.errorMessageHideTimeout = setTimeout(function () {
        $('#miniBasketError').hide();
    }, mini_basket_holder.productAddedTimeout);
};

miniBasketHolder.prototype.maximizeBasket = function (notInvokedDirectlyByUser) {
    if (this.minimizing) {
        return;
    }

    this.maximized = true;

    $('#miniBasket').addClass("sel");

    $('#miniBasket .mini-bag-wrapper').slideDown(this.openSpeed);

    $('#miniBasket .top').slideDown(miniBasketHolder.openSpeed);
    var self = this;

    $('#miniBasket').hover(
        function () {
            clearTimeout(this.minimiseBasketTimeout);
        },
        function () {
            clearTimeout(this.minimiseBasketTimeout);
            this.minimiseBasketTimeout = setTimeout(function () { self.minimizeBasket(); }, self.minimizeAfterMouseLeaveTimeout);
        }
    );
    clearTimeout(this.minimiseBasketTimeout);
    if (notInvokedDirectlyByUser) {
        this.minimiseBasketTimeout = setTimeout(function () { self.minimizeBasket(); }, this.minimizeAfterAddTimeout);
    }
};

miniBasketHolder.prototype.resetMinimiseAfterClick = function () {
    var self = this;
    clearTimeout(this.minimiseBasketTimeout);
    this.minimiseBasketTimeout = setTimeout(function () { self.minimizeBasket(); }, self.minimizeAfterAddTimeout);
};

miniBasketHolder.prototype.minimizeBasket = function () {
    if (!this.maximized || this.isMouseOverMiniBasket) {
        return;
    }
    this.maximized = false;
    this.minimizing = true;
    $('#miniBasket .mini-bag-wrapper').slideUp(this.closeSpeed);
    $('#miniBasket .top').slideUp(this.closeSpeed);
    var self = this;
    setTimeout(function () { $('#miniBasket').removeClass("sel"); self.minimizing = false; }, 500);
};

mini_basket_holder = new miniBasketHolder();
