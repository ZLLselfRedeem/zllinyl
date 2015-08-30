///<summary>
///选择显示方式
///</summary>
function SelectShowType(showType, thisLi) {
    $("#Panel1").show();
    $("#Panel2").hide();
    if (showType == "step") {
        $("#Panel3").hide();
        $("#Panel4").hide();
        $("#Panel5").hide();
        $("#couponAddSteps").show();
        $("#ButtonCouponStep_shang").hide();
        $("#ButtonCouponStep_xia").show();
        $("#Button_Publish").hide();
        currentStep = 1;
        $(".stepOnePageTitle").css("background-color", "gray");
    }
    else if (showType == "onePage") {
        $("#Panel3").show();
        $("#Panel4").show();
        $("#Panel5").show();
        $("#couponAddSteps").hide();
        $("#ButtonCouponStep_shang").hide();
        $("#ButtonCouponStep_xia").hide();
        $("#Button_Publish").show();
        currentStep = 0;
        $(".stepOnePageTitle").css("background-color", "#518BCB");
    
    }
    $(thisLi).addClass("current").siblings().removeClass("current");
}
///<summary>
///点击上一步下一步按钮
///</summary>
function CurrentCouponStepManage(shang_xia) {
    var currentStepIndex = couponStepArray[currentStep - 1];
    if (shang_xia == "shang") {
        currentStep--;
        ShowShangXia();
    }
    if (shang_xia == "xia") {
        
        if (currentStepIndex == 3) {
            if (ValidDiscountAndMoney() && ValidTime()) {
                currentStep++;
                ShowShangXia();
            }
        }
        else if (currentStepIndex == 4) {
            if (ValidImage()) {
                currentStep++;
                ShowShangXia();
            }
        }
        else {
            currentStep++;
            ShowShangXia();
        }
    }
    
}
///<summary>
///根据步骤，显示相应输入框
///</summary>
function ShowShangXia() {
    var currentStepIndex = couponStepArray[currentStep - 1];
    $("#Panel" + currentStepIndex).show(500);

    $("#Panel" + currentStepIndex).siblings().not("#Panel6").not("#couponAddSteps").hide();
    if (currentStepIndex == 1) {
        $("#ButtonCouponStep_shang").hide();
        $("#ButtonCouponStep_xia").show();
        $("#Button_Publish").hide();
    }
    else if (currentStepIndex > 1 && currentStepIndex < 5) {
        $("#ButtonCouponStep_shang").show();
        $("#ButtonCouponStep_xia").show();
        $("#Button_Publish").hide();
    }
    else if (currentStepIndex == 5) {
        $("#ButtonCouponStep_shang").show();
        $("#ButtonCouponStep_xia").hide();
        $("#Button_Publish").show();
    }
    $("#couponAddSteps td:eq(" + (currentStepIndex - 1) + ")").addClass("tdCurrent");
    $("#couponAddSteps td:eq(" + (currentStepIndex - 1) + ")").siblings().removeClass("tdCurrent");
}