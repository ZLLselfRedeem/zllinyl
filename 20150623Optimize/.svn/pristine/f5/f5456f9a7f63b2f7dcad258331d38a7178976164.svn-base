/***************针对多家菜谱多家么门店的操作********************/
///// <summary>
///// 多个菜谱生成菜谱button
///// <summary>
function GetMuchMenuBtn() {
    $.ajax({
        type: "Post",
        url: "CouponAdd.aspx/QueryManuNameAndMenuID",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //递归获取数据   
            $(data.d).each(function () {
                var getMenuInformation = eval('(' + this + ')');
                var getMenuName = getMenuInformation.menuName;
                var getMenuID = getMenuInformation.menuId;
                // alert(obj);
                $("#menuShow").append('&nbsp;&nbsp;&nbsp;' + '<input type="button" class="menubutton" name="spot_addr" onclick="ShowDivContentDishes(event);" id="' + getMenuID + '"  value="' + getMenuName.toString() + '" /> ');
            });
        },
        error: function (err) {
            alert(err);
        }
    });
    return false;
}
///// <summary>
///// 页面加载生成多个门店的checkbox
///// <summary>
function GetMuchShopCb() {
    $.ajax({
        type: "Post",
        url: "CouponAdd.aspx/QueryShopNameAndShopID",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //插入前先清空 ,必要的时候执行一下操作  
            //$("#mendianSelect").html("");
            //递归获取数据   
            $(data.d).each(function () {
                var obj = eval('(' + this + ')');
                var idid = obj.shopId;
                var namename = obj.shopName.toString();
                //插入结果到动态生成checkbox表单   
                $("#mendianSelect").append(namename + '<input type="checkbox" id="' + idid + '" name="cbshopNamesndId_addr"   value= "' + namename + '"/>   ' + '&nbsp;&nbsp;&nbsp;');
            });
        },
        error: function (err) {
            alert(err);
        }
    });
    //禁用按钮的提交   
    return false;
}
///// <summary>
///// 显示点菜框
///// <summary>
function ShowDivContentDishes(event) {
    //获得当前点击对象的属性id
    var getclickInputId = 0;
	var evt = event||window.event;
	var t = evt.target||evt.srcElement;
	getclickInputId = parseInt(t.id);
    //设置点击选中按钮颜色
    $('#' + getclickInputId).css("background", "#99FFFF").siblings("input").css("background", "none");
    //清除页面上的内容
    ClearData();
    var t = $("#divOrderedDishes").html();
    t = t.replace(/[\r\n]/g, "").replace(/[ ]/g, "");
    if (t == '') {
        //显示菜单
        GetMenuinformation(getclickInputId);
    }
    //    else {
    //        var msg = confirm("不能同时对两个菜谱进行操作，“确定”将清除您以前的点菜信息，“取消”将返回上步操作！！！");
    //        if (msg == true) {
    //            $("#divOrderedDishes").html('');
    //            //清除点菜的信息
    //           // ClearDataTwo();
    //            //弹出另一个菜单
    //            GetMenuinformation(getclickInputId);
    //        }
    //        else {
    //        }
    //    }
}
///// <summary>
///// 获取菜单
///// <summary>
function GetMenuinformation(getclickInputId) {
    $.ajax({
        type: "post",
        url: "CouponAdd.aspx/GetDishAll",
        contentType: "application/json; charset=utf-8",
        data: "{'menuId' :'" + getclickInputId + "'}",
        dataType: "json",
        success: function (data) {
            //将对应的菜单输出
            document.getElementById("divDish").innerHTML = data.d;
            //显示隐藏的div
            ShowDivInTheCentral("divContentDishes");
            //根据点击的菜单输出对应的门店(重新绘制出门店的信息，表示得到当前的菜谱下的门店（菜谱→多家门店）)
            GetNewShopinforFromMenuId(getclickInputId);
        },
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            alert(XmlHttpRequest.responseText);
        }
    });
}
///// <summary>
///// 根据上面的id查出对应的shop的信息
///// <summary>
function GetNewShopinforFromMenuId(getclickInputId) {
    $.ajax({
        type: "post",
        url: "CouponAdd.aspx/QueryMenuIDtoShopInfo",
        contentType: "application/json; charset=utf-8",
        data: "{'menuId' :'" + getclickInputId + "'}",
        dataType: "json",
        success: function (data) {
            //清空原有的信息
            $("#mendianSelect").html("");
            //递归获取数据   
            $(data.d).each(function () {
                var obj = eval('(' + this + ')');
                //插入结果到动态生成对应的shop 的checkbox表单   
                $("#mendianSelect").append(obj.shopName.toString() + '<input type="checkbox" id="' + obj.shopId + '"  name="cbshopNamesndId_addr"  value= "' + obj.shopName.toString() + '"  /> ' + '&nbsp;&nbsp;&nbsp;');
            });
        },
        error: function (err) {
            alert(err);
        }
    });
}
