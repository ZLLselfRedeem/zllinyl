
/*****************点击添加优惠券的时候，验证客户端输入是不是合法**********************/
///<summary>
///验证基本输入
///</summary>
function ValidDiscountAndMoney() {
    var discount = $("#TextBox_Discount").val();
    var discountedAmount = $("#TextBox_DiscountedAmount").val();
    var originalPrice = $("#TextBox_OriginalPrice").val();
    var currentPrice = $("#TextBox_CurrentPrice").val();
    var originaQuantity = $("#TextBox_OriginaQuantity").val();
    if (discount == "") {
        ShowResult("折扣不能为空");
        return false;
    }
    if (discountedAmount == "") {
        ShowResult("抵价金额不能为空");
        return false;
    }
    if (originalPrice == "") {
        ShowResult("原价不能为空");
        return false;
    }
    if (currentPrice == "") {
        ShowResult("团购金额不能为空");
        return false;
    }
    if (originaQuantity == "") {
        ShowResult("发布数量不能为空");
        return false;
    }
    return true;
}

///<summary>
///验证时间输入
///</summary>
function ValidTime() {
    //验证时间输入
    var couponDisplayStartTime = $("#TextBox_CouponDisplayStartTime").val();
    var couponDisplayEndTime = $("#TextBox_CouponDisplayEndTime").val();
    var couponValidStartTime = $("#TextBox_CouponValidStartTime").val();
    var couponValidEndTime = $("#TextBox_CouponValidEndTime").val();

    var dateCouponDisplayStartTime = Date.parse(couponDisplayStartTime);
    var dateCouponDisplayEndTime = Date.parse(couponDisplayEndTime);
    var dateCouponValidStartTime = Date.parse(couponValidStartTime);
    var dateCouponValidEndTime = Date.parse(couponValidEndTime);

    if (couponDisplayStartTime == "" || couponDisplayEndTime == "" || couponValidStartTime == "" || couponValidEndTime == "") {
        ShowResult("优惠券时间不能为空");
        return false;
    }
    if (dateCouponValidStartTime > dateCouponValidEndTime) {
        ShowResult("活动开始时间必须早于结束时间");
        return false;
    }
    if (dateCouponDisplayStartTime > dateCouponDisplayEndTime) {
        ShowResult("展示开始时间必须早于结束时间");
        return false;
    }
    if (dateCouponDisplayEndTime > dateCouponValidEndTime) {
        ShowResult("展示结束时间必须早于活动结束时间");
        return false;
    }
    return true;
}
///<summary>
///验证图片相关
///</summary>
function ValidImage() {
    //验证是否有图片
    var smallCouponImageHTML = $("#div_smallCouponImage").find("img");
    var bigCouponImageHTML = $("#div_bigCouponImage").find("img");
    if (smallCouponImageHTML.length == 0 || bigCouponImageHTML.length == 0) {
        ShowResult("您没有添加优惠券图片");
        return false;
    }
    return true;
}
///<summary>
///验证优惠券名称
///</summary>
function ValidCouponName() {
    if ($("#TextBox_CouponName").val() == "") {
        ShowResult("优惠券名称不能为空");
        return false;
    }
    return true;
}
///<summary>
///判断门店选择是否为空，提示选择门店
///</summary>
function CheckBoxIsNull() {
    var obj = document.getElementsByName("cbshopNamesndId_addr");
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked == true) {
            return true;
            break;
        }
    }
    ShowResult("请选择对应的门店");
    return false;
}
///<summary>
///一次点单可使用数量
///</summary>
function ValidCouponCanUseOnce() {
    if ($("#Text_DowmloadCount").val() == "") {
        ShowResult("单次点单可使用数量不能为空");
        return false;
    }
    if ($("#TextBox_CouponName").val().length > 15) {
        ShowResult("优惠券名称最多15个字符");
        return false;
    }
    return true;
}