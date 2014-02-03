var timer = "";
var AjaxClient = {
    $: function (domId) {
        return document.getElementById(domId);
    },
    decode: function (value) {
        return decodeURIComponent(value);
    },
    encode: function (value) {
        return encodeURIComponent(decodeURIComponent(value));
    },

    BindValue: function (str, value) {
        if (value)
            this.$(str).value = value;
        else
            return this.$(str).value;
    },

    showCover: function () {
        $(".overlay").css("display", "block");
        var offset = $("#productBox").offset();
        var height = $("#productBox").height();
        $(".overlay").css("height", height + "px");
        $(".overlay").css("left", offset.left + "px");
        $(".overlay").css("top", offset.top + "px");
    },

    hideCover: function () {
        $(".overlay").css("display", "none");
    },

    Sort: function (obj) {
        this.BindValue("filterSortTxt", obj.options[obj.selectedIndex].value);
        this.CreateUrl();
        this.GetPageContent();
        return false;
    },

    OnSubcategoryClick: function (obj) {
        //change status of checkboxes
        if (jQuery.className.has(obj, "disabled")) {
            return;
        };
        var state = jQuery.className.has(obj, "selected");
        if (state) {
            $(obj).get(0).className = "unSelected";
        } else {
            $(obj).get(0).className = "selected";
        }

        this.BindValue("filterProductSubcategories", this.SubcategoriesSelected());
        this.BindValue("filterPageIndex", "1");
        this.CreateUrl();
        this.GetPageContent();

        return false;
    },

    OnManufacturerClick: function (obj) {
        if (jQuery.className.has(obj, "disabled")) {
            return;
        };

        var state = jQuery.className.has(obj, "selected");
        if (state) {
            $(obj).get(0).className = "unSelected";
        } else {
            $(obj).get(0).className = "selected";
        }

        this.BindValue("filterProductManufacturers", this.ManufacturersSelected());
        this.BindValue("filterPageIndex", "1");
        this.CreateUrl();
        this.GetPageContent();

        return false;
    },

    OnPageIndexClick: function (num) {
        this.BindValue("filterPageIndex", num.toString());
        this.CreateUrl();
        this.GetPageContent();
        return false;
    },

    OnPageSizeClick: function (num) {
        this.BindValue("filterPageSize", num.toString());
        this.BindValue("filterPageIndex", "1");
        this.CreateUrl();
        this.GetPageContent();
        return false;
    },

    OnAttrClick: function (obj) {
        if (jQuery.className.has(obj, "disabled")) {
            return;
        };
        var state = jQuery.className.has(obj, "selected");
        if (state) {
            $(obj).get(0).className = "unSelected";
        } else {
            $(obj).get(0).className = "selected";
        }

        this.BindValue("filterAttrs", menu_getAttrValue());
        this.BindValue("filterSpec", menu_getSpecValue());
        this.BindValue("filterProductSubcategories", this.SubcategoriesSelected());
        this.BindValue("filterProductManufacturers", this.ManufacturersSelected());
        this.BindValue("filterPageIndex", "1");
        this.CreateUrl();
        this.GetPageContent();
        return false;
    },

    SubcategoriesSelected: function () {
        var length = $("#ProductSubcategories li a.selected").size();
        var values = " ";

        for (var i = 0; i < length; i++) {
            if (i == length - 1)
                values += this.decode($("#ProductSubcategories li a.selected").get(i).href.split("#")[1]);
            else {
                values += this.decode($("#ProductSubcategories li a.selected").get(i).href.split("#")[1]) + "|";
            }
        }
        return values;
    },

    ManufacturersSelected: function () {
        var length = $("#ProductManufacturers li a.selected").size();
        var values = " ";

        for (var i = 0; i < length; i++) {
            if (i == length - 1)
                values += this.decode($("#ProductManufacturers li a.selected").get(i).href.split("#")[1]);
            else {
                values += this.decode($("#ProductManufacturers li a.selected").get(i).href.split("#")[1]) + "|";
            }
        }

        return values;
    },

    AttrSelected: function (id) {
        var length = $("#" + id + " li a.selected").size();
        var values = " ";

        for (var i = 0; i < length; i++) {
            values += this.decode($("#" + id + " li a.selected").get(i).href.split("#")[1]) + "|";
        }

        return values;
    },

    CreateUrl: function () {
        var strUrl = this.encode("urlSubcategoriesSelected") + "=" + this.encode(this.$("filterProductSubcategories").value) + "&";
        strUrl += this.encode("urlManufacturersSelected") + "=" + this.encode(this.$("filterProductManufacturers").value) + "&";
        strUrl += this.encode("urlPriceRange") + "=" + this.encode(this.$("filterPriceMinTxt").value + "|" + this.$("filterPriceMaxTxt").value) + "&";
        strUrl += this.encode("urlSort") + "=" + this.encode(this.$("filterSortTxt").value) + "&";
        strUrl += this.encode("urlPageIndex") + "=" + this.encode(this.$("filterPageIndex").value) + "&";
        strUrl += this.encode("urlPageSize") + "=" + this.encode(this.$("filterPageSize").value) + "&";
        strUrl += this.encode("urlAttr") + "=" + this.encode(this.$("filterAttrs").value) + "&";
        strUrl += this.encode("urlSpec") + "=" + this.encode(this.$("filterSpec").value) + "&";
        strUrl += "m" + (new Date()).getTime();
        
        window.location.hash = strUrl;
    },

    GetPageContent: function () {
        var me = this;
        me.showCover();
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/RgetProducts.aspx?" + location.hash.split("#")[1],
            type: "GET",
            dataType: "html",

            complete: function (res, status) {
                $.getScript("/AddonsByOsShop/CustomScripts/osx.js");
                me.$("productBox").innerHTML = res.responseText.match(/<form(.|\s)*?>((.|\s)*?)<\/form>/)[2];
                me.hideCover();
            }
        })
    }
};

//Loaded Initialization
$(function () {
    if (menu_maxPrice > 0) {
        $("#slider").slider({
            animate: true,
            max: menu_maxPrice,
            min: menu_minPrice,
            range: true,
            values: [menu_minPrice, menu_maxPrice],
            orientation: 'auto',
            slide: function (event, ui) {
                AjaxClient.BindValue("filterPriceMaxTxt", ui.values[1]);
                AjaxClient.BindValue("filterPriceMinTxt", ui.values[0]);
                AjaxClient.$("userMin").innerHTML = ui.values[0];
                AjaxClient.$("userMax").innerHTML = ui.values[1];
                if (timer) {
                    clearTimeout(timer);
                    timer = setTimeout(function () {
                        AjaxClient.BindValue("filterPageIndex", "1");
                        AjaxClient.CreateUrl();
                        AjaxClient.GetPageContent();
                    }, 1000);
                } else {
                    timer = setTimeout(function () {
                        AjaxClient.BindValue("filterPageIndex", "1");
                        AjaxClient.CreateUrl();
                        AjaxClient.GetPageContent();
                    }, 1000);
                }
            }
        });
    }
    //    AjaxTemplate.getUrlHashInfo();
});