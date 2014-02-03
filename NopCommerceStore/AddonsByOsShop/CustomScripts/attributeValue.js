
var defaultValue = "";
var GiftCardData = new Array();
for (var i = 0; i < 5; i++) {
    GiftCardData[i] = "";
}
var strTrans = new Array();
for (var i = 0; i < 10; i++) {
    strTrans[i] = new Array();
    strTrans[i][0] = "";
    strTrans[i][1] = "";
    strTrans[i][2] = "";
    strTrans[i][3] = "";
    strTrans[i][4] = "";
}


function getMethod1(obj) {
    var chkSpan = obj.getElementsByTagName("option");
    var dropDownValue = obj.options[obj.selectedIndex].value;
    var strItem = new Array();
    var str = new Array();
    var subStr = "", sign = 1;


    strItem = defaultValue.split("|");
    for (var i = 0; i < strItem.length - 1; i++) {
        str = strItem[i].toString().split(",");

        if (str[1] == chkSpan[0].getAttribute("headId").toString()) {//如果属性ID有 则去掉以前的 再把新的放到后面
            subStr = str[0] + "," + str[1] + "," + str[2] + "|";
            defaultValue = defaultValue.replace(subStr, "");
            defaultValue = defaultValue + chkSpan[0].getAttribute("pvId") + "," + chkSpan[0].getAttribute("headId") + "," + dropDownValue + "|";
            sign = 0;
            break;
        }
    }
    if (sign == 1)//循环了一般 如果没有，则添加到后面
        defaultValue = defaultValue + chkSpan[0].getAttribute("pvId") + "," + chkSpan[0].getAttribute("headId") + "," + dropDownValue + "|";
   
};
function getMethod2(obj) {
    var chkSpan = obj.getElementsByTagName("span");
    var radInput = obj.getElementsByTagName("INPUT");
    var radLable = obj.getElementsByTagName("Label");

    for (var i = 0; i < radInput.length; i++) {
        if (radInput[i].checked) {
            var strItem = new Array();
            var str = new Array();
            var subStr = "", sign = 1;
            strItem = defaultValue.split("|");
            for (var j = 0; j < strItem.length - 1; j++) {
                str = strItem[j].toString().split(",");
                if (str[1] == chkSpan[0].getAttribute("headId").toString()) {
                    subStr = str[0] + "," + str[1] + "," + str[2] + "|";
                    defaultValue = defaultValue.replace(subStr, "");
                    defaultValue = defaultValue + chkSpan[0].getAttribute("pvId") + "," + chkSpan[0].getAttribute("headId") + "," + radInput[i].value + "|";
                    sign = 0;
                    break;

                }

            }
            if (sign == 1)
                defaultValue = defaultValue + chkSpan[0].getAttribute("pvId") + "," + chkSpan[0].getAttribute("headId") + "," + radInput[i].value + "|";

        }
    }

};


function getMethod3(obj) {
    threeValue = "";
    var chkSpan = obj.getElementsByTagName("span");
    var chkInput = obj.getElementsByTagName("INPUT");
    var chkLable = obj.getElementsByTagName("Label");
       for (var i = 0; i < chkSpan.length; i++) {
        //获取选中的属性值的ID
        if (chkInput[i].checked) {
            var strItem = new Array();
            var str = new Array();
            var subStr = "", sign = 1;
            strItem = defaultValue.split("|");
            for (var j = 0; j < strItem.length - 1; j++) {
                str = strItem[j].toString().split(",");
                if (str[1] == chkSpan[0].getAttribute("headId").toString() && str[2] == chkSpan[i].getAttribute("footId").toString()) {
                    sign = 0; //循环一遍 有完全相同的 sign=0 不加到后面
                }
            }
            if (sign == 1)//循环一遍 没有完全相同的 加到后面
                defaultValue = defaultValue + chkSpan[0].getAttribute("pvId") + "," + chkSpan[0].getAttribute("headId") + "," + chkSpan[i].getAttribute("footId") + "|";
        }
        else
            defaultValue = defaultValue.replace(chkSpan[0].getAttribute("pvId") + "," + chkSpan[0].getAttribute("headId") + "," + chkSpan[i].getAttribute("footId") + "|", ""); //去掉没选中的
    }
   
};


function getMethod4(obj) {

    var categoryId = obj.getAttribute("pvId").toString();
    var headId = obj.getAttribute("headId").toString();
    var contentValue = obj.value.toString();
    contentValue = contentValue.toString().trim();
    if (contentValue != "")
        contentValue = contentValue + ".";
    var strItem = new Array();
    var str = new Array();
    var subStr = "", sign = 1;

  
    strItem = defaultValue.split("|");
    for (var i = 0; i < strItem.length - 1; i++) {
        str = strItem[i].toString().split(",");
        if (str[1] == headId) {//如果属性ID有 则去掉以前的 再把新的放到后面
            subStr = str[0] + "," + str[1] + "," + str[2] + "|";
            defaultValue = defaultValue.replace(subStr, "");
            defaultValue = defaultValue + categoryId + "," + headId + "," + contentValue + "|";
            sign = 0;
            break;
        }
    }
    if (sign == 1)//循环了一般 如果没有，则添加到后面
        defaultValue = defaultValue + categoryId + "," + headId + "," + contentValue + "|";
    

};


function getMethod5(obj) {

    var categoryId = obj.getAttribute("pvId").toString();
    var headId = obj.getAttribute("headId").toString();
    var contentValue = obj.value.toString();
    contentValue = contentValue.toString().trim();
    if (contentValue != "")
        contentValue = contentValue + ".";
    
    var strItem = new Array();
    var str = new Array();
    var subStr = "", sign = 1;

    strItem = defaultValue.split("|");
    for (var i = 0; i < strItem.length - 1; i++) {
        str = strItem[i].toString().split(",");
        if (str[1] == headId) {//如果属性ID有 则去掉以前的 再把新的放到后面
            subStr = str[0] + "," + str[1] + "," + str[2] + "|";
            defaultValue = defaultValue.replace(subStr, "");
            defaultValue = defaultValue + categoryId + "," + headId + "," + contentValue + "|";
            sign = 0;
            break;
        }
    }
    if (sign == 1)//循环了一般 如果没有，则添加到后面
        defaultValue = defaultValue + categoryId + "," + headId + "," + contentValue + "|";

};





function getMethod6(obj) {
    var dropDownValue = obj.options[obj.selectedIndex].value;
  
    //如果刚开始 将当前ID存入数组中
    if (strTrans[0][0] == "") {
        strTrans[0][0] = obj.getAttribute("pvId");
        strTrans[0][1] = obj.getAttribute("headId");
        //alert("数组长度.....：" + str.length);
        if (obj.getAttribute("DataType") == "year") {
            strTrans[0][2] = dropDownValue;
        }
        if (obj.getAttribute("DataType") == "month") {
            strTrans[0][3] = dropDownValue;
        }
        if (obj.getAttribute("DataType") == "day") {
            strTrans[0][4] = dropDownValue;
        }
    }
    else {
        for (var j = 0; j < 10; j++) {

            if (obj.getAttribute("headId") == strTrans[j][1]) {

                if (obj.getAttribute("DataType") == "year") {
                    strTrans[j][2] = dropDownValue;
                }
                if (obj.getAttribute("DataType") == "month") {
                    strTrans[j][3] = dropDownValue;
                }
                if (obj.getAttribute("DataType") == "day") {
                    strTrans[j][4] = dropDownValue;
                }
                break;
            }

            if (strTrans[j][0] == "") {
                strTrans[j][0] = obj.getAttribute("pvId");
                strTrans[j][1] = obj.getAttribute("headId");
                if (obj.getAttribute("DataType") == "year") {
                    strTrans[j][2] = dropDownValue;
                }
                if (obj.getAttribute("DataType") == "month") {
                    strTrans[j][3] = dropDownValue;
                }
                if (obj.getAttribute("DataType") == "day") {
                    strTrans[j][4] = dropDownValue;
                }
                break;
            }
        }
    }


}







function getMethodDays(obj) {
    //alert("Days:" + obj);
    //var chkSpan = obj.getElementsByTagName("span");
    var chkSpan = obj.getElementsByTagName("span");
    var dropDownValue = obj.options[obj.selectedIndex].value;
    alert("dropDownValue:" + dropDownValue);

    alert(obj.getAttribute("pvId"));
    alert(obj.getAttribute("headId"));
    //alert(chkSpan.length);
    //alert(chkSpan.getAttribute("pvId") + "," + chkSpan.getAttribute("headId"));

};

function getMethodMonths(obj) {
    var chkSpan = obj.getElementsByTagName("option");
    var dropDownValue = obj.options[obj.selectedIndex].value;

    alert("dropDownValue:" + dropDownValue);
    alert(obj.getAttribute("pvId"));
    alert(obj.getAttribute("headId"));
};

function getMethodYears(obj) {
    var chkSpan = obj.getElementsByTagName("option");
    var dropDownValue = obj.options[obj.selectedIndex].value;

    alert("dropDownValue:" + dropDownValue);
    alert(obj.getAttribute("pvId"));
    alert(obj.getAttribute("headId"));
};
