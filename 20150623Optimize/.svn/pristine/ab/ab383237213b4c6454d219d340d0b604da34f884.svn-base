/********************************************优惠券时间相关代码*************************************************/
/// <summary>
/// 快捷选择时间
/// <summary>
function ChooseValidTime(chooseTime, control) {
    $(control).removeClass().addClass("commonButtonClick");
    $(control).siblings().removeClass("commonButtonClick").addClass("commonButtonUnClick");
    var strTime = new Date();
    var year = strTime.getFullYear();
    var month = strTime.getMonth();
    var day = strTime.getDate();
    var hours = strTime.getHours();
    var minutes = strTime.getMinutes();
    var seconds = strTime.getSeconds();
    var week = strTime.getDay();
    var addDay = 0;
    var countDays = GetCountDays(year, month);
    $("#TextBox_CouponValidStartTime").val(FormatStr(strTime));
    if (chooseTime == "today") {
    }
    else if (chooseTime == "friday") {
        if (week == 6) { //星期六
            addDay = 6;
        }
        else {
            addDay = 5 - week;
        }
    }
    else if (chooseTime == "sunday") {
        addDay = 7 - week;
    }
    else if (chooseTime == "month") {
        addDay = countDays - day;
    }
    var endTime = new Date(year, month, day + addDay, "23", "59", "59");
    $("#TextBox_CouponValidEndTime").val(FormatStr(endTime));

    //让TextBox只读
    $("#TextBox_CouponValidStartTime").unbind("focus");
    $("#TextBox_CouponValidEndTime").unbind("focus");
    //$("#TextBox_CouponValidStartTime").attr("readonly", "readonly");
    //$("#TextBox_CouponValidEndTime").attr("readonly", "readonly");
    //$("#TextBox_CouponDisplayStartTime").attr("readonly", "readonly");
    //$("#TextBox_CouponDisplayEndTime").attr("readonly", "readonly");
    ChooseDisplayTime('Button_DisplayTimeDefault');
}
function ChooseDisplayTime(control) {
    $("#" + control).removeClass().addClass("commonButtonClick");
    $("#" + control).siblings().removeClass("commonButtonClick").addClass("commonButtonUnClick");
    $("#TextBox_CouponDisplayStartTime").val($("#TextBox_CouponValidStartTime").val());
    $("#TextBox_CouponDisplayEndTime").val($("#TextBox_CouponValidEndTime").val());
    //让TextBox只读
    $("#TextBox_CouponDisplayStartTime").unbind("focus");
    $("#TextBox_CouponDisplayEndTime").unbind("focus");
    //$("#TextBox_CouponDisplayStartTime").attr("readonly", "readonly");
    //$("#TextBox_CouponDisplayEndTime").attr("readonly", "readonly");
}
/// <summary>
/// 格式化时间
/// yyyy-MM-dd HH:mm:ss
/// <summary>
function FormatStr(d) {
    var ret = d.getFullYear() + "-"
    ret += ("00" + (d.getMonth() + 1)).slice(-2) + "-"
    ret += ("00" + d.getDate()).slice(-2) + " "
    ret += ("00" + d.getHours()).slice(-2) + ":"
    ret += ("00" + d.getMinutes()).slice(-2) + ":"
    ret += ("00" + d.getSeconds()).slice(-2)
    return ret;
}
/// <summary>
/// 获取某年，某月一共有多少天
/// <summary>
function GetCountDays(year, month) {
    var date = new Date();
    date.setFullYear(year, month + 1);
    date.setDate(0);
    return date.getDate();
}
/// <summary>
/// 自定义活动周期
/// <summary>
function CustomValidTime(Button_ValidTimeCustom) {
    $(Button_ValidTimeCustom).removeClass().addClass("commonButtonClick");
    $(Button_ValidTimeCustom).siblings().removeClass("commonButtonClick").addClass("commonButtonUnClick");
    //$("#TextBox_CouponValidStartTime").removeAttr("readonly");
    //$("#TextBox_CouponValidEndTime").removeAttr("readonly");
    $("#TextBox_CouponValidStartTime").focus(function () {
        WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
    });
    $("#TextBox_CouponValidEndTime").focus(function () {
        WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
    });
}
/// <summary>
/// 自定义展示周期
/// <summary>
function CustomDisplayTime(Button_DesplayTimeCustom) {
    $(Button_DesplayTimeCustom).removeClass().addClass("commonButtonClick");
    $(Button_DesplayTimeCustom).siblings().removeClass("commonButtonClick").addClass("commonButtonUnClick");
    //$("#TextBox_CouponDisplayStartTime").removeAttr("readonly");
    //$("#TextBox_CouponDisplayEndTime").removeAttr("readonly");
    $("#TextBox_CouponDisplayStartTime").focus(function () {
        WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
    });
    $("#TextBox_CouponDisplayEndTime").focus(function () {
        WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });
    });
}