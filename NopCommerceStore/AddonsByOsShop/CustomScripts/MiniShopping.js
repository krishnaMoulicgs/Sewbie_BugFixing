
var AjaxTemplate = {
    createProductid: function (id1) {
        var id = id1;
        var queryString = { productQueryId: id };
        return queryString;
    },

    createProductMessage: function (proudctVariantId1, productMessage1, Quantity1) {
        //将购物卡信息一同添加进属性
        var giftCard = "";
        for (var i = 0; i < 5; i++) {
            var strItem = new Array();
            var str = new Array();
            strItem = GiftCardData[i].split("|");
            for (var j = 0; j < strItem.length - 1; j++) {
                str = strItem[j].toString().split(",");
                if (str[0] == proudctVariantId1.toString()) {//如果属性ID有 则去掉以前的 再把新的放到后面
                    giftCard += str[1] + "|";
                    break;
                }
            }
        }

        var queryString = { productVariantIdAtrr: proudctVariantId1, productAttributeMessage: productMessage1, Quantity: Quantity1, giftGardMessage: giftCard };
        return queryString;

    },



    createQueryString: function (pvId) {
        pvId = pvId;
        var queryString = { productVariantId: pvId };
        return queryString;
    },

    createMessage: function () {
        var id1 = "", id2 = "", id3 = "";
        var queryString = { productQueryId: id1, productVariantId: id2, productId: id3 };
        return queryString;
    },
    showShoppingCart: function (data, actionDeal) {
        var dataProduct = data;
        dataProduct = dataProduct + ""; //转换成字符串


        var strItem = new Array();
        var str = new Array();
        strItem = dataProduct.split("<!>"); //每个商品之间用 <!> 分割
        if (strItem[0] == "error")
            alert(strItem[1]);
        else {

            $(".itemsClmn").empty(); //清空一起的div
            var num, allMoney = 0, allCount = 0;
            for (var i = 0; i < strItem.length - 1; i++) {
                str = strItem[i].toString().split("|"); //商品的各个属性用 | 号隔开
                allMoney = allMoney + parseInt(str[3].toString()) * parseFloat(str[2].toString()); //计算商品的总价格 
                allCount = allCount + parseInt(str[3]);
                var send = "<div class='item false'><a href=" + str[5] + "><img class=thumb width='90' height='115' src=" + str[1] + " /></a> <div class='detail'> <p class='price'><span class='price'> " + str[6] + str[2] + "</span></p><p class='title'>" + str[0] + "</p><p class='quantity'>"+getQty()+" "+ str[3] + "</p><p><span> " + str[7] + "</span></p></div></div> ";
                send = send + "<p><a onclick='AjaxTemplate.delProduct(" + str[4] + ")' class='replace remove'  href='#'><span></span></a></p>"; //删除按钮
                $(".itemsClmn").append(send); //显示各个商品
            }
            num = strItem.length - 1;

            //总额保留两位有效数字
            var ws = Math.pow(10, 2);
            allMoney = Math.round(allMoney * ws) / ws;

            if (allMoney != 0) {
                $("#MiniShoppingShow").html(ShowMiniShopping(allCount, str[6] + String(allMoney)));
                //$("#MiniShoppingShow").html("YOUR BAG " + allCount + " items " + str[6] + String(allMoney)); //显示 商品的个数和价格
            }
            else {
                $("#MiniShoppingShow").html(ShowMiniShopping(allCount, ""));
            }

            $("#totalMoney").html(str[6] + allMoney); //显示总价格

            if (num == 0) {
                $('#miniBasket .full').hide();
                $('#miniBasket .empty').show();
            }
            else {
                $('#miniBasket .empty').hide();
                $('#miniBasket .full').show();
            }


            if (num == 1) {
                $('#miniBasket .full.body .items').css({ "height": "150px" });
            }
            if (num == 2) {

                $('#miniBasket .full.body .items').css({ "height": "280px" });
            }
            if (num == 3) {

                $('#miniBasket .full.body .items').css({ "height": "411px" });
            }
            //处理按钮
            mini_basket_holder.doPageButtonVisibilities();
            //alert("num:" + num);
            //actionDeal 添加为1 删除为2
            if (actionDeal == 1) {
                var pageSign;
                if (num < 3)
                    pageSign = 1;
                else {
                    if (num % 3 == 0) {
                        pageSign = num / 3;
                    }
                    else {
                        pageSign = parseInt(num / 3) + 1;
                    }

                }

                var countTrans = pageSign - mini_basket_holder.currPage;
                mini_basket_holder.currPage = pageSign;

                var currentTop = parseInt($('#miniBasket .itemsClmn').css('top'), 10);
                if (isNaN(currentTop)) {
                    currentTop = 0;
                }
                var newTop = currentTop - mini_basket_holder.itemContainerHeight * parseInt(countTrans);

                $('#miniBasket .itemsClmn').animate({ top: newTop + 'px' }, 500, function () { self.paging = false; });
                mini_basket_holder.doPageButtonVisibilities();

            }
            else
                if (actionDeal == 2) {
                    if (num % 3 == 0 && mini_basket_holder.currPage != 1) {
                        var currentTop = parseInt($('#miniBasket .itemsClmn').css("top"), 10);
                        mini_basket_holder.currPage--; //向前翻一页
                        if (isNaN(currentTop)) {
                            currentTop = 0;
                        }
                        var newTop = currentTop + mini_basket_holder.itemContainerHeight;
                        $('#miniBasket .itemsClmn').animate({ top: newTop + 'px' }, 500, function () { self.paging = false; });
                        mini_basket_holder.doPageButtonVisibilities();
                    }
                }
        }

    },

    //初始化购物车 
    InitializationCart: function () {
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getMiniShoppingInfo.aspx?" + new Date().getTime(),
            type: "Get",
            data: this.createMessage(),
            success: function (data, statue) {
                AjaxTemplate.showShoppingCart(data, 0);
            }
        });
    },

    //删除商品
    delProduct: function (id) {
        var productVariantId = id;
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getMiniShoppingInfo.aspx?" + new Date().getTime(),
            type: "Get",
            data: this.createProductid(productVariantId),
            success: function (data, statue) {
                AjaxTemplate.showShoppingCart(data, 2);
            }
        });

    },

    //添加商品列表
    getProductMessage: function (proudctVariantId, attributeMessage, Quantity) {

        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getMiniShoppingInfo.aspx?" + new Date().getTime(), //加入时间 url欺骗 重新运行ajax
            type: "Get",
            data: this.createProductMessage(proudctVariantId, attributeMessage, Quantity),
            success: function (data, statue) {
                AjaxTemplate.showShoppingCart(data, 1);
                mini_basket_holder.maximizeBasket(true); //点击添加商品时，自动弹出购物车               
            }

        });

    },

    //获取内容
    getPageContent: function (proudctVariantId) {
        jQuery.ajax({
            url: "/AddonsByOsShop/AjaxPages/getMiniShoppingInfo.aspx?" + new Date().getTime(), //加入时间 url欺骗 重新运行ajax
            type: "Get",
            data: this.createQueryString(proudctVariantId),
            success: function (data, statue) {
                AjaxTemplate.showShoppingCart(data, 0);

                mini_basket_holder.maximizeBasket(true); //点击添加商品时，自动弹出购物车               
            }

        });
    }
};

