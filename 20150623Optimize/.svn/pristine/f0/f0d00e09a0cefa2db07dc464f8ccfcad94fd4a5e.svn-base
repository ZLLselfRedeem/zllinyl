/// <reference path="jquery-1.4.1.min.js" />
/*created by wangc
*20140414
*门店列表搜索
*/
function initData(status) {
    if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
        // document.getElementById('text').attachEvent("onkeyup", keyTest);
        return;
    }
    else {
        //火狐不支持onkeyup方法
        switch (status) {
            case "medalManage"://门店勋章管理
                document.getElementById('text').addEventListener("input", medalManage, true);
                break;
            case "shophandle": //门店审核
                document.getElementById('text').addEventListener("input", keyTest, true);
                break;
            case "shopmanage": //门店列表
                document.getElementById('text').addEventListener("input", companyShopSearch, true);
                break;
            case "shopapplypayment": //打款管理列表
                document.getElementById('text').addEventListener("input", shopSearch, true);
                break;
            case "shopmenulist": //菜谱列表
                document.getElementById('text').addEventListener("input", shopMenuSearch, true);
                break;
            case "addmenu": //菜谱添加
                document.getElementById('text').addEventListener("input", menuAddSearchCompany, true);
                break;
            case "addshop": //门店添加
                document.getElementById('companyText').addEventListener("input", shopAddSearchCompany, true);
                break;
            case "foodPlazaConfig":
                document.getElementById('foodPlazaConfigCheckShop').addEventListener("input", checkShop, true);//监听器，注册事件
                break;
            case "balanceaccountapply": //打款管理列表
                document.getElementById('text').addEventListener("input", BalanceAccountApplyShopSearch, true);
                break;
            case "batchmoneyapply": //批量打款申请
                document.getElementById('text').addEventListener("input", shopAndCompanySearch, true);
                break;
            case "batchmoneyapplyManager": //打款管理列表
                document.getElementById('text').addEventListener("input", BatchmoneyapplyManagerShopSearch, true);
                break;
            case "shopAmountLog": //商户余额日志
                document.getElementById('text').addEventListener("input", ShopAountLogSearch, true);
                break;
            case "financialReconciliation": //财务对账查询
                document.getElementById('text').addEventListener("input", FinancialReconciliationShopSearch, true);
                break;
            default:
                break;
        }
    }
}
/*
门店审核搜索门店
*/
function keyTest() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetData",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "" && data.d != null) {
                var returnJson = eval("(" + data.d + ")");
                var strHtml = "<ul>";
                for (var i = 0; i < returnJson.length; i++) {
                    if (i == 0) {
                        $("#init_date").html('');
                    }
                    strHtml += "<li onclick='select(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                }
                strHtml += "</ul>";
                $("#init_date").append(strHtml);
            }
        },
        error: errorFun
    });
}
function select(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "ShopHandle.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
门店勋章管理
*/
function medalManage() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetData",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "" && data.d != null) {
                var returnJson = eval("(" + data.d + ")");
                var strHtml = "<ul>";
                for (var i = 0; i < returnJson.length; i++) {
                    if (i == 0) {
                        $("#init_date").html('');
                    }
                    strHtml += "<li onclick='selectMedalShop(this)' id='"
                                       + returnJson[i].shopId + "' data-name='"
                                       + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                                       + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                }
                strHtml += "</ul>";
                $("#init_date").append(strHtml);
            }
        },
        error: errorFun
    });
}
function selectMedalShop(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "MedalManage.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
门店列表搜索门店
*/
function companyShopSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetShop",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        strHtml += "<li onclick='selectCompanyShop(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectCompanyShop(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "ShopManage.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
门店添加搜索公司
*/
function shopAddSearchCompany() {
    var str = $("#companyText").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/QueryAllCompany",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#company_init_data").html('');
                        }
                        strHtml += "<li onclick='selectShopCompany(this)' id='"
                            + returnJson[i].companyID + "' data-name='"
                            + returnJson[i].companyName + "'>&nbsp;&nbsp;"
                            + returnJson[i].companyName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#company_init_data").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectShopCompany(company) {
    var name = $("#" + company.id).attr('data-name');
    $("#company_init_data").html('');
    $("#companyText").val(name);
    $("#hidden_companyId").val(company.id);
}
/*
打款管理搜索门店
*/
function shopSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetData",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        strHtml += "<li onclick='selectShop(this)' id='"
                                + returnJson[i].shopId + "' data-name='"
                                + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                                + returnJson[i].shopName + "&nbsp;&nbsp;</li>";
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectShop(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "BusniessApplyPayment.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
菜谱列表
*/
function shopMenuSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetShop",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        strHtml += "<li onclick='selectShopMenu(this)' id='"
                                + returnJson[i].shopId + "' data-name='"
                                + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                                + returnJson[i].shopName + "&nbsp;&nbsp;</li>";
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectShopMenu(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "MenuList.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
菜谱添加
*/
function menuAddSearchCompany() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetCompany",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        strHtml += "<li onclick='selectCompanyMenuAdd(this)' id='"
                            + returnJson[i].companyID + "' data-name='"
                            + returnJson[i].companyName + "'>&nbsp;&nbsp;"
                            + returnJson[i].companyName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}

function selectCompanyMenuAdd(company) {
    var name = $("#" + company.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "MenuAdd.aspx?id=" + company.id + "&name=" + name + "";
}
function foodPlazaConfigCheckShop() {
    var shopName = $("#checkShop").val();
    var cityId = $('#ddlCity option:selected').val();
    if (shopName == "") {
        $("#flag").val('true');
        return;
    }
    $("#flag").val('false');
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetSearchShop",
        data: "{'shopName':'" + shopName + "','cityId':'" + cityId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != "" && data.d != null) {
                var returnJson = eval("(" + data.d + ")");
                var strHtml = "<ul>";
                for (var i = 0; i < returnJson.length; i++) {
                    if (i == 0) {
                        $("#shopList").html('');
                    }
                    strHtml += "<li onclick='selectfoodPlazaShop(this)' id='"
                            + returnJson[i].shopID + "' data-name='"
                            + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                }
                strHtml += "</ul>";
                $("#shopList").append(strHtml);
            }
        },
        error: errorFun
    });
}
function selectfoodPlazaShop(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#shopList").html('');
    window.location.href = "foodPlazaConfig.aspx?id=" + shop.id + "&name=" + name + "";
}

/*
平账申请搜索门店
*/
function BalanceAccountApplyShopSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetData",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        strHtml += "<li onclick='balanceAccountApplySelectShop(this)' id='"
                                + returnJson[i].shopId + "' data-name='"
                                + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                                + returnJson[i].shopName + "&nbsp;&nbsp;</li>";
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function balanceAccountApplySelectShop(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "BalanceAccountApply.aspx?id=" + shop.id + "&name=" + name + "";
}

/*
<<<<<<< .mine
门店搜索公司
*/
function shopAndCompanySearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetDataAndCompany",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        //alert(returnJson[i].shopName + "|" + returnJson[i].companyName + "|" + returnJson[i].remainMoney);
                        strHtml += "<li onclick='selectShopAndCompany(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "," + returnJson[i].companyName + "," + returnJson[i].remainMoney + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectShopAndCompany(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "batchMoneyApplyDS.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
=======
平账管理搜索门店
*/
function BalanceAccountManageShopSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetData",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        strHtml += "<li onclick='balanceAccountManageSelectShop(this)' id='"
                                + returnJson[i].shopId + "' data-name='"
                                + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                                + returnJson[i].shopName + "&nbsp;&nbsp;</li>";
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function balanceAccountManageSelectShop(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "BalanceAccountManage.aspx?id=" + shop.id + "&name=" + name + "";
}

/*
<<<<<<< .mine
门店搜索公司
*/
function BatchmoneyapplyManagerShopSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetDataAndCompany",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        //alert(returnJson[i].shopName + "|" + returnJson[i].companyName + "|" + returnJson[i].remainMoney);
                        strHtml += "<li onclick='selectBatchmoneyapplyManager(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "," + returnJson[i].companyName + "," + returnJson[i].remainMoney + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectBatchmoneyapplyManager(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "batchMoneyApplyManager.aspx?id=" + shop.id + "&name=" + name + "";
}


function ShopAmountLogSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetDataAndCompany",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        //alert(returnJson[i].shopName + "|" + returnJson[i].companyName + "|" + returnJson[i].remainMoney);
                        strHtml += "<li onclick='selectShopAmountLogSearch(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "," + returnJson[i].companyName + "," + returnJson[i].remainMoney + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectShopAmountLogSearch(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "ShopAmountLogQuery.aspx?id=" + shop.id + "&name=" + name + "";
}

function FinancialReconciliationShopSearch() {
    var str = $("#text").val();
    if (str == "") {
        return;
    }
    $.ajax({
        type: "Post",
        url: "../Handlers/commonAjaxPage.aspx/GetDataAndCompany",
        data: "{'str':'" + str + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.d != "" && data.d != null) {
                    var returnJson = eval("(" + data.d + ")");
                    var strHtml = "<ul>";
                    for (var i = 0; i < returnJson.length; i++) {
                        if (i == 0) {
                            $("#init_date").html('');
                        }
                        //alert(returnJson[i].shopName + "|" + returnJson[i].companyName + "|" + returnJson[i].remainMoney);
                        strHtml += "<li onclick='selectFinancialReconciliationShopSearch(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "," + returnJson[i].companyName + "," + returnJson[i].remainMoney + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                    }
                    strHtml += "</ul>";
                    $("#init_date").append(strHtml);
                }
            }
        },
        error: errorFun
    });
}
function selectFinancialReconciliationShopSearch(shop) {
    var name = $("#" + shop.id).attr('data-name');
    $("#init_date").html('');
    window.location.href = "financialReconciliation.aspx?id=" + shop.id + "&name=" + name + "";
}
/*
>>>>>>> .r1352
js回调解析错误
*/
function errorFun() {
    //alert("获取数据失败");
}

