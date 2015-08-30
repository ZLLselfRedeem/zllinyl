/********************************************点菜相关代码*************************************************/
//看看这个菜是不是已经点过了
function CheckIfDishIsOrdered(dishPriceI18nID) {
    var returnValue = false;
    $.each(listCouponImageAndDish, function (index, item) {
        if (item.dishPriceI18nID == dishPriceI18nID) {
            returnValue = true;
        }
    });
    return returnValue;
}
/// <summary>
/// 将服务器端传过来字符串，反序列化为结构
/// <summary>
function JsonDeserialize(jsonStr) {
    var re = new RegExp("@", "g"); //第一个参数表示你要替换的字符串，第二个参数表示替换所有（i表示替换第一个）
    var jsonStr = "[" + jsonStr.replace(re, "\"") + "]"; //把@替换成"
    jsonStr = jsonStr.replace(/\/\*((\n|\r|.)*?)\*\//mg, "");  //去掉多行注释/*..*/ 
    jsonStr = jsonStr.replace(/(\s+)\/\/(.*)\n/g, "");  //去掉单行注释//(前面有空格的注释) 
    jsonStr = jsonStr.replace(/;\/\/(.*)\n/g, ";");  //去掉单行注释//(前面是分号的注释)	
    jsonStr = jsonStr.replace(/\/\/[^"][^']\n/g, ""); //去掉单行注释//(//后面只有一个'或一个"的不替换)	
    jsonStr = jsonStr.replace(/[\r]/g, "");  //替换换行 
    jsonStr = jsonStr.replace(/[\n]/g, "");  //替换回车 
    var couponImageAndDishs = new Array();
    try {
        var jsonDish = eval(jsonStr);
        $.each(jsonDish, function (inx, item) {
            var imageAndDish = new CouponImageAndDish();
            imageAndDish.dishName = jsonDish[inx].dishName;
            imageAndDish.dishId = jsonDish[inx].dishId;
            imageAndDish.couponId = jsonDish[inx].couponId;
            imageAndDish.couponImageAddDishId = jsonDish[inx].couponImageAddDishId;
            imageAndDish.dishPriceI18nID = jsonDish[inx].dishPriceI18nID;
            imageAndDish.scaleName = jsonDish[inx].scaleName;
            imageAndDish.dishDescDetail = jsonDish[inx].dishDescDetail;
            imageAndDish.dishDescShort = jsonDish[inx].dishDescShort;
            imageAndDish.dishPrice = jsonDish[inx].dishPrice;
            imageAndDish.couponImageName = jsonDish[inx].couponImageName;
            imageAndDish.couponImagePath = jsonDish[inx].couponImagePath;
            imageAndDish.couponImageScale = jsonDish[inx].couponImageScale;
            imageAndDish.couponImageSequence = jsonDish[inx].couponImageSequence;
            imageAndDish.couponImageStatus = jsonDish[inx].couponImageStatus;
            imageAndDish.dishQuantity = jsonDish[inx].dishQuantity;
            couponImageAndDishs.push(imageAndDish);
        });
    }
    catch (e) {
        alert("下面菜品数据结构有错误:" + jsonStr);
    }
    return couponImageAndDishs;
}
//点菜，参数来源是：后台绘制菜的html时，后台给出的
function OrderDish(button) {
    var couponAndImageStr = $(button).attr("name");
    var couponImageAndDishs = JsonDeserialize(couponAndImageStr);
    var dishPriceI18nID = couponImageAndDishs[0].dishPriceI18nID;
    if ($("#Label_couponTypeClickValue").val() == "1") {//表示选中的是套餐券
        if (CheckIfDishIsOrdered(dishPriceI18nID)) {
            //加份量
            AddPulseOrderedDish(dishPriceI18nID, "add");
        }
        else {
            //listCouponImageAndDish = []; //清空数组，显示当前选中的菜
            for (var i = 0; i < couponImageAndDishs.length; i++) {
                listCouponImageAndDish.push(couponImageAndDishs[i]);
            }
        }
        //原代码
        $(button).removeClass("dishButton").addClass("dishButtonClick");
        //$(button).removeClass("dishButton").addClass("dishButtonClick").siblings().removeClass("dishButtonClick").addClass("dishButton").parent().parent().siblings().children().children("input").removeClass("dishButtonClick").addClass("dishButton");
    }
    else {//通用券和特定券，只能选择一个菜，并且只能是一份。

        //if (CheckIfDishIsOrdered(dishPriceI18nID)) {
        // AddPulseOrderedDish(dishPriceI18nID, "add");
        //}
        // else {
        listCouponImageAndDish = []; //清空数组，显示当前选中的菜
        for (var i = 0; i < couponImageAndDishs.length; i++) {
            listCouponImageAndDish.push(couponImageAndDishs[i]);
        }
        //}
        $(button).removeClass("dishButton").addClass("dishButtonClick").siblings().removeClass("dishButtonClick").addClass("dishButton");
        $(button).removeClass("dishButton").addClass("dishButtonClick").parent().parent().siblings().children().children("input").removeClass("dishButtonClick").addClass("dishButton");
    }
    GetOrderedDishes();
}

//显示已经点过的菜
function GetOrderedDishes() {
    $("#divOrderedDishes").html('');
    $("#TextBox_CouponDesc").val(''); //清空使用说明
    var strHtml = "";
    var strDes = "";
    var arrayDishPriceI18nID = new Array(); //存放dishPriceI18nID，如果已经有了，就不再显示重复的
    var dishTotalPrice = 0;
    $.each(listCouponImageAndDish, function (index, item) {
        var dishPriceI18nID = item.dishPriceI18nID;
        if (dishPriceI18nID != 0 && dishPriceI18nID != null) {
            var haveShowed = false; //判断当前dishPriceI18nID是不是显示过了(后台数据中一个dishPriceI18nID会有两个图片)
            for (var i = 0; i < arrayDishPriceI18nID.length; i++) {
                if (dishPriceI18nID == arrayDishPriceI18nID[i]) {
                    haveShowed = true;
                }
            }
            if (!haveShowed) {
                var arrayLength = arrayDishPriceI18nID.length;
                arrayDishPriceI18nID[arrayLength] = dishPriceI18nID;
                if ($("#Label_couponTypeClickValue").val() == "1") {//表示选中的是套餐券
                    //可以加减菜
                    var button_jia = "<input type=\"button\" name=\"orderedDishJiaButtonName_" + item.dishId + "\" value=\"+\" id=orderedDishJiaButtonId_\"" + item.dishId + "\" class=\"dishButtonJiaJian\"  onclick=\"AddPulseOrderedDish('" + item.dishPriceI18nID + "','add');\">";
                    var button_dish = "<input type=\"button\" name=\"orderedDishButtonName_" + item.dishId + "\" value=\"" + item.dishName + "（" + item.dishQuantity + item.scaleName + "）（单价：￥" + item.dishPrice + "）" + "\" id=orderedDishButtonId_\"" + item.dishId + "\" class=\"dishButton\"  onclick=\"DeleteOrderedDish('" + item.dishPriceI18nID + "','delete');\">";
                    var button_jian = "";
                    if (parseInt(item.dishQuantity) > 1) {
                        button_jian = "<input type=\"button\"   name=\"orderedDishJianButtonName_" + item.dishId + "\" value=\"-\" id=orderedDishJianButtonId_\"" + item.dishId + "\" class=\"dishButtonJiaJian\"  onclick=\"AddPulseOrderedDish('" + item.dishPriceI18nID + "','pulse');\">";
                    }
                    strHtml += "<tr><td>" + button_jia + button_dish + button_jian + "</td></tr>";
                }
                else {
                    //可以减菜（删除），不可以加菜
                    var button_dish = "<input type=\"button\" name=\"orderedDishButtonName_" + item.dishId + "\" value=\"" + item.dishName + "（" + item.dishQuantity + item.scaleName + "）（单价：￥" + item.dishPrice + "）" + "\" id=orderedDishButtonId_\"" + item.dishId + "\" class=\"dishButton\"  onclick=\"DeleteOrderedDish('" + item.dishPriceI18nID + "','delete');\">";
                    strHtml += "<tr><td>" + button_dish + "</td></tr>";
                }
                dishTotalPrice += item.dishPrice * item.dishQuantity;
                //动态生成使用说明
                strDes += item.dishName + item.dishQuantity + "份" + "，";
            }
        }
    });
    $("#TextBox_CouponDesc").val(strDes.substring(0, strDes.length - 1)); //去掉最后一个逗号字符
    //计算行数
    //var trTotal = arrayDishPriceI18nID.length / 2;
    $("#divOrderedDishes").append("<table>" + strHtml + "</table>");
    $("#divContentOrderedDishes").css("display", "block");
    $("#labelDishTotalPrice").html(dishTotalPrice.toFixed(2));
    $("#TextBox_OriginalPrice").val(dishTotalPrice.toFixed(2));
    ShowCouponImage(); //显示优惠券图片
}
//增加、减少某个菜份数
function AddPulseOrderedDish(dishPriceI18nID, type) {
    for (var i = 0; i < listCouponImageAndDish.length; i++) {
        if (listCouponImageAndDish[i].dishPriceI18nID == dishPriceI18nID) {
            if (type == "add") {
                listCouponImageAndDish[i].dishQuantity = listCouponImageAndDish[i].dishQuantity + 1;
            }
            else if (type == "pulse") {
                listCouponImageAndDish[i].dishQuantity = listCouponImageAndDish[i].dishQuantity - 1;
            }
        }
    }

    GetOrderedDishes();
}

//删除某个菜
function DeleteOrderedDish(dishPriceI18nID) {
    for (var i = 0; i < listCouponImageAndDish.length; i++) {
        if (listCouponImageAndDish[i].dishPriceI18nID == dishPriceI18nID) {
            listCouponImageAndDish.splice(i, 1); //从数组中移除一个元素 
            DeleteOrderedDish(dishPriceI18nID);
        }
    }

    GetOrderedDishes();
}