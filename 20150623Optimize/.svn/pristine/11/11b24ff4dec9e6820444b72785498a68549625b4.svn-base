/************************************公共数据类型begin**************************************/
//优惠券菜的Model
function CouponImageAndDish() {
    var dishName;
    var dishId;
    var couponId;
    var couponImageAddDishId;
    var dishPriceI18nID;
    var scaleName;
    var dishDescDetail;
    var dishDescShort;
    var dishPrice;
    var couponImageName;
    var couponImagePath;
    var couponImageScale;
    var couponImageSequence;
    var couponImageStatus;
    var dishQuantity;
    var ImageID;
    var MenuID;
}
//用来存放优惠券的菜
var listCouponImageAndDish = new Array();

//按步骤添加优惠券时，记录到了哪一步
var currentStep = 0;
var couponStepArray = new Array(); //记录有哪些步骤，比如如果是特殊券的话，有第二步（点菜）,则couponStepArray为1,2,3,4,5  如果没有菜

var currentCouponType;
//优惠券类型枚举
if (typeof couponType == "undefined") {
    var couponType = {
        DISCOUNT_GENERAL_CAMPAIGN_TYPE: 1, // 通用折扣券 不设特定菜品
        DISCOUNT_DISH_CAMPAIGN_TYPE: 2,    // 特定折扣券，至少一个特定菜品
        DEDUCT_GENERAL_CAMPAIGN_TYPE: 21,   // 通用抵价券，不设特定菜品
        DEDUCT_DISH_CAMPAIGN_TYPE: 22,      // 特定抵价券，至少一个特定菜品
        MEAL_SPECIAL_CAMPAIGN_TYPE: 30,     // 套餐特价券，至少一个菜品
        OTHER_COUPON_TYPE: 99    //其他
    }
}

//优惠券的Model
function CouponInfo() {
        var couponID;
        var companyId;
        var couponCreatTime;
        var couponDesc;
        var couponDisplayEndTime;
        var couponDisplayStartTime;
        var couponDownloadPrice;
        var couponName;
        var couponRefreshTime;
        var couponRequirementType;
        var couponStatus;
        var couponType;
        var couponValidEndTime;
        var couponValidStartTime;
        var currentPrice;
        var currentQuantity;
        var isVIPOnly
        var originalPrice;
        var originaQuantity;
        var discount;
        var discountedAmount;
        var canDownloadOnlyOnce;
        var downloadQuantity;
        var couponVerifyReward;
        var viewedCount;
        var verifyCount;
        var canUseNumberOnesOrder; //一次点单可使用同种优惠券的数量
        var lowPayment; //单次最低消费
        var prompt;//特别提示

    }