/********************************************其他函数*************************************************/
//显示添加优惠券结果弹出框
function ShowResult(strResult) {
    $("#div_content").append("<div id=\"contentResultWindow\" class=\"contentResultWindow\"><div class=\"resultWindowTitle\" onclick=\"HiddenResult('contentResultWindow')\"><label>系统消息</label><img src=\"../Images/Close2.gif\"/></div><div id=\"ImageStorage\" class=\"resultWindow\"><div>" + strResult + "</div><div class=''><input type='button' onclick=\"HiddenResult('contentResultWindow')\" value='确定' style='cursor: pointer;'></div></div></div>");
    ShowDivInTheCentral("contentResultWindow");
}
//隐藏添加优惠券结果弹出框
function HiddenResult(div) {
    $("#" + div).remove();
    $("#divMask").css({ display: "none" });
    $("#Button_Publish").removeAttr("disabled");
    $("#Button_Publish").attr("value", "确认发布");
}

///<summary>
///让一个div居中显示
///</summary>
function ShowDivInTheCentral(div) {
    showMask();
    var dishButtons = $("#divDish :input");
    for (var i = 0; i < dishButtons.length; i++) {
        if (CheckIfDishIsOrdered(dishButtons[i].id)) {
            $(dishButtons[i]).removeClass("dishButton").addClass("dishButtonClick");
        }
        else {
            $(dishButtons[i]).removeClass("dishButtonClick").addClass("dishButton");
        }
    }

    var left = ($(window).width() - $("#" + div).outerWidth()) / 2;
    var top = ($(window).height() - $("#" + div).outerHeight()) / 2 + $(".layout").scrollTop() - 100;
    $("#" + div).css({ "left": left, "top": top, "position": "absolute", "z-index": "1000" });
    $("#" + div).show();
}
//显示灰色遮罩层 
function showMask() {
    var bH = $(".layout")[0].scrollHeight;
    var bW = "100%";
    $("#divMask").css({ width: bW, height: bH, display: "block" });
}
//隐藏一个窗口
function HiddenThis(div) {
    $("#" + div).hide();
    $("#divMask").css({ display: "none" });
}
//判断浏览器是不是安装了flash
function CheckFlash() {
    if (!getFlashPluginsActiveXObject()) {
        $("#div_content").append("<div id=\"contentDownloadFlash\" class=\"contentResultWindow\"><div class=\"resultWindowTitle\" onclick=\"HiddenThis('contentDownloadFlash')\"><label>系统消息</label><img src=\"../Images/Close2.gif\"/></div><div id=\"ImageStorage\" class=\"resultWindow\"><div>本系统需要安装Flash，请点击<a target='_blank' href='http://www.adobe.com/go/getflash'>这里</a>安装</div><div></div></div>");
        ShowDivInTheCentral("contentDownloadFlash");
    }
}
///<summary>
///清空用户选择的数据
///</summary>
function ClearData() {
    //取消选取选中的门店
    $('input:checkbox').removeAttr('checked');
    //先清空listCouponImageAndDish
    listCouponImageAndDish = [];
    //清空用户选择的菜和图片
    $("#divOrderedDishes").html('');
    $("#div_smallCouponImage").html('');
    $("#div_bigCouponImage").html('');
    //    //清空用户填写项
    //    //$(":text").val('');
    //清空时间text
    $("#TextBox_CouponValidStartTime").val('');
    $("#TextBox_CouponValidEndTime").val('');
    $("#TextBox_CouponDisplayStartTime").val('');
    $("#TextBox_CouponDisplayEndTime").val('');
    $("#TextBox_CouponName").val('');
    $("#TextBox_CouponDesc").val('');
    $("#RadioButtonList_CanDownloadOnlyOnce_0").attr("checked", true);
    $("#TextBox_OriginaQuantity").val("99999");
    $(".commonButtonClick").removeClass("commonButtonClick").addClass("commonButtonUnClick");
    $("#labelDishTotalPrice").html("0.00");
    $("#TextBox_Prompt").val(''); //清空特别提示信息
}
/*************************这是调试代码 begin***********************/
function ShowListCouponImageAndDish() {
    var alertStr = "";
    for (var i = 0; i < listCouponImageAndDish.length; i++) {
        alertStr += listCouponImageAndDish[i].couponImageSequence + "   大小：" + listCouponImageAndDish[i].couponImageScale + listCouponImageAndDish[i].dishName + listCouponImageAndDish[i].dishPrice + "\n";
    }
    alert(alertStr);
}
/*************************这是调试代码 end***********************/