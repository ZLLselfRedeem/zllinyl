/********************************************优惠券类型相关代码begin*************************************************/
function SelectCouponTypeClick(couponTypeId, couponTypeControl) {
    ClearData();
    if (couponTypeId == couponType.DISCOUNT_GENERAL_CAMPAIGN_TYPE) {//通用折扣券
        //不选菜
        $("#Panel2").hide(500);
        //控制填写项
        $("#Panel_Discount").show();
        $("#Panel_DiscountedAmount").hide();
        $("#Panel_Taocan").hide();

        $("#TextBox_Discount").val("");
        $("#TextBox_DiscountedAmount").val("-1");
        $("#TextBox_OriginalPrice").val("-1");
        $("#TextBox_CurrentPrice").val("-1");
        couponStepArray = new Array(1, 3, 4, 5);
        //显示所有门店信息
        ShowAllCbx();
        //
        $("#Label_couponTypeClickValue").val("0");
        $("#Text_DowmloadCount").attr("readonly", true).val('1').attr("disabled", true); //
    }
    else if (couponTypeId == couponType.DISCOUNT_DISH_CAMPAIGN_TYPE) {//特定折扣券
        //选菜
        $("#Panel2").show(500);
        //控制填写项
        $("#Panel_Discount").show();
        $("#Panel_DiscountedAmount").hide();
        $("#Panel_Taocan").hide();

        $("#TextBox_Discount").val("");
        $("#TextBox_DiscountedAmount").val("-1");
        $("#TextBox_OriginalPrice").val("");
        $("#TextBox_CurrentPrice").val("-1");
        couponStepArray = new Array(1, 2, 3, 4, 5);
        ShowAllCbx();
        //
        $("#Label_couponTypeClickValue").val("0");
        $("#Text_DowmloadCount").attr("readonly", true).val('1').attr("disabled", true); //
    }
    else if (couponTypeId == couponType.DEDUCT_GENERAL_CAMPAIGN_TYPE) {//通用抵价券
        //不选菜
        $("#Panel2").hide(500);
        //控制填写项
        $("#Panel_Discount").hide();
        $("#Panel_DiscountedAmount").show();
        $("#Panel_Taocan").hide();

        $("#TextBox_Discount").val("-1");
        $("#TextBox_DiscountedAmount").val("");
        $("#TextBox_OriginalPrice").val("-1");
        $("#TextBox_CurrentPrice").val("-1");
        couponStepArray = new Array(1, 3, 4, 5);
        ShowAllCbx();
        //
        $("#Label_couponTypeClickValue").val("0");
        $("#Text_DowmloadCount").attr("readonly", false).val('1').attr("disabled", false); //
    }
    else if (couponTypeId == couponType.DEDUCT_DISH_CAMPAIGN_TYPE) {//特定抵价券
        //选菜
        $("#Panel2").show(500);
        //控制填写项
        $("#Panel_Discount").hide();
        $("#Panel_DiscountedAmount").show();
        $("#Panel_Taocan").hide();

        $("#TextBox_Discount").val("-1");
        $("#TextBox_DiscountedAmount").val("");
        $("#TextBox_OriginalPrice").val("-1");
        $("#TextBox_CurrentPrice").val("-1");
        couponStepArray = new Array(1, 2, 3, 4, 5);
        ShowAllCbx();
        //
        $("#Label_couponTypeClickValue").val("0");
        $("#Text_DowmloadCount").attr("readonly", false).val('1').attr("disabled", false); //
    }
    else if (couponTypeId == couponType.MEAL_SPECIAL_CAMPAIGN_TYPE) {//套餐特价券
        //选菜
        $("#Panel2").show(500);
        //控制填写项
        $("#Panel_Discount").hide();
        $("#Panel_DiscountedAmount").hide();
        $("#Panel_Taocan").show();

        $("#TextBox_Discount").val("-1");
        $("#TextBox_DiscountedAmount").val("-1");
        $("#TextBox_OriginalPrice").val("");
        $("#TextBox_CurrentPrice").val("");
        couponStepArray = new Array(1, 2, 3, 4, 5);
        ShowAllCbx();
        //
        $("#Label_couponTypeClickValue").val("1");
        $("#Text_DowmloadCount").attr("readonly", true).val('1').attr("disabled", true); //
    }
    else if (couponTypeId == couponType.OTHER_COUPON_TYPE) {//其他券

    }
    currentCouponType = couponTypeId; //记录当前选择的优惠券类型
    $(couponTypeControl).removeClass().addClass("onePageCouponTypeClick");
    $(couponTypeControl).siblings().removeClass("onePageCouponTypeClick").addClass("onePageCouponTypeUnClick");
    if (currentStep > 0) {
        CurrentCouponStepManage("xia");
    }
}
///***************当页面加载，显示所有的门店信息******************/
function ShowAllCbx() {
    $("#mendianSelect").html('');
    GetMuchShopCb();
}