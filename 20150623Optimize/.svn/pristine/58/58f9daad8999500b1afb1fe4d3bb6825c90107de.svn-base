// <reference path="../../Scripts/jquery-1.4.1.min.js" />//添加智能提示
//created by wangcheng 
//2013/11/8
//添加无图菜基本信息
function addDish() {
    if (checkText()) {
        var dishName = $("#txtDishName").val();  //菜品名称
        $.get("../Handlers/CN2PY.ashx", { Hz: dishName, typeID: 3 },
     function (data) {
         if (data != "" && data != null) {
             var pyResult = data; //获得菜品名称汉字的全拼和简拼
             var dishPrice = $("#txtDishPrice").val(); //价格
             var dishUnit = $("#txtDishUnit").val(); //规格
             $.ajax({
                 type: "Post",
                 url: "dishBatchAddDetailNoImg.aspx/InsertDishInfo",
                 data: "{'dishName':'" + dishName + "','dishPrice':'" + dishPrice + "','dishUnit':'" + dishUnit + "','dishPinYin':'" + pyResult + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     if (data.d != "" && data.d != null) {
                         switch (data.d) {
                             case -1:
                                 alert("价格或者规格填写有误");
                                 break;
                             case -2:
                                 alert("添加失败");
                                 break;
                             default:
                                 alert("添加成功");
                                 break;
                         }
                     }
                     else {
                         alert("添加失败"); //获得菜品名称汉字的全拼和简拼失败
                     }
                 },
                 error: function (XmlHttpRequest, textStatus, errorThrown) {
                     alert("添加失败");
                 }
             });
         }
         else {
             alert("添加失败"); //获得菜品名称汉字的全拼和简拼失败
         }
     });
    }
}
//初始化加载，清空文本框
function cleanText() {
    $("#txtDishName").val('');
    $("#txtDishPrice").val('');
    $("#txtDishUnit").val('');
}
//提交表单检查文本框的合法性
function checkText() {
    var reg = new RegExp(/^(:?(:?\d+.\d+)|(:?\d+))$/);
    if ($("#txtDishName").val() == "") {
        alert("菜品名称不能为空");
        return false;
    }
    else if (!reg.test($("#txtDishPrice").val())) {
        alert("请输入合法菜品价格");
        return false;
    }
    else if ($("#txtDishUnit").val() == "") {
        alert("菜品单位不能为空");
        return false;
    }
    else {
        return true;
    }
}

//删除无图菜信息列表
function deleteDishInfo(DishID) {
    $.ajax({
        type: "Post",
        url: "dishBatchAddDetailNoImg.aspx/DeleteDishInfo",
        data: "{'dishID':'" + DishID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "" && data.d != null) {
                switch (data.d) {
                    case -1:
                        alert("删除失败");
                        break;
                    case 1:
                        alert("删除成功");
                        break;
                }
            }
            else {
                alert("删除失败");
            }
        },
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            alert("删除失败");
        }
    });
}
//更新修改无图菜信息列表
function updateDishInfo(DishID) {
    var dishName = $("#txtDishName").val(); //菜品名称
    if (checkText()) {
        $.get("../Handlers/CN2PY.ashx", { Hz: dishName, typeID: 3 },
     function (data) {
         var dishPrice = $("#txtDishPrice").val(); //价格
         var dishUnit = $("#txtDishUnit").val(); //规格
         $.ajax({
             type: "Post",
             url: "dishBatchAddDetailNoImg.aspx/DeleteDishInfo",
             data: "{'dishID':'" + DishID + "','dishName':'" + dishName + "','dishPrice':'" + dishPrice + "','dishUnit':'" + dishUnit + "'}",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 if (data.d != "" && data.d != null) {
                     switch (data.d) {
                         case -1:
                             alert("价格或者规格填写有误");
                             break;
                         case -2:
                             alert("修改失败");
                             break;
                         default:
                             alert("修改成功");
                             break;
                     }
                 }
                 else {
                     alert("修改失败");
                 }
             },
             error: function (XmlHttpRequest, textStatus, errorThrown) {
                 alert("修改失败");
             }
         });
     });
    }
}
