<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mealShopRanking.aspx.cs" Inherits="meal_mealShopRanking" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>年夜饭门店排序</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        #dataTable {
            padding-left: 20px;
            margin-left: 10px;
            font-family: verdana,arial,sans-serif;
            font-size: 16px;
            color: #333333;
            border-width: 1px;
            border-color: #999999;
            border-collapse: collapse;
        }

            #dataTable tr {
                background-color: #d4e3e5;
            }

            #dataTable td {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #a9c6c9;
            }
    </style>
    <script type="text/javascript">
        function Query() {
            var selecttype = $('#ddlMealType').val();
            $.ajax({
                type: "Post",
                url: "mealShopRanking.aspx/GetMealShopRankingData",
                data: "{'type':'" + selecttype + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var returnJson = eval("(" + data.d + ")");
                        var count = returnJson.length;
                        var dataStr = '';
                        for (var i = 0; i < returnJson.length; i = i + 3) {
                            if (i == 0) { $("#dataTable").html(''); }
                            if (i + 1 == returnJson.length || i + 2 == returnJson.length) {
                                if (returnJson.length % 3 == 0) {
                                    dataStr += "<tr><td><label>"
                                                   + returnJson[i].shopName
                                                   + "</label><input id="
                                                   + returnJson[i].shopId
                                                   + " type='text' value="
                                                   + returnJson[i].sequenceNumber
                                                   + " /></td>"
                                                   + "<td><label>" + returnJson[i + 1].shopName
                                                   + "</label><input id="
                                                   + returnJson[i + 1].shopId
                                                   + " type='text' value="
                                                   + returnJson[i + 1].sequenceNumber
                                                   + " /></td>"
                                                   + "<td><label>" + returnJson[i + 2].shopName
                                                   + "</label><input id="
                                                   + returnJson[i + 2].shopId
                                                   + " type='text' value="
                                                   + returnJson[i + 2].sequenceNumber
                                                   + " /></td></tr>";
                                }
                                else if (returnJson.length % 3 == 1) {
                                    dataStr += "<tr><td><label>"
                                                   + returnJson[i].shopName
                                                   + "</label><input id="
                                                   + returnJson[i].shopId
                                                   + " type='text' value="
                                                   + returnJson[i].sequenceNumber
                                                   + " /></td>"
                                                   + "<td></td>"
                                                   + "<td></td></tr>";
                                }
                                else if (returnJson.length % 3 == 2) {
                                    dataStr += "<tr><td><label>"
                                                   + returnJson[i].shopName
                                                   + "</label><input id="
                                                   + returnJson[i].shopId
                                                   + " type='text' value="
                                                   + returnJson[i].sequenceNumber
                                                   + " /></td>"
                                                   + "<td><label>" + returnJson[i + 1].shopName
                                                   + "</label><input id="
                                                   + returnJson[i + 1].shopId
                                                   + " type='text' value="
                                                   + returnJson[i + 1].sequenceNumber
                                                   + " /></td>"
                                                   + "<td></td></tr>";
                                }
                            }
                            else {
                                dataStr += "<tr><td><label>"
                                                  + returnJson[i].shopName
                                                  + "</label><input id="
                                                  + returnJson[i].shopId
                                                  + " type='text' value="
                                                  + returnJson[i].sequenceNumber
                                                  + " /></td>"
                                                  + "<td><label>" + returnJson[i + 1].shopName
                                                  + "</label><input id="
                                                  + returnJson[i + 1].shopId
                                                  + " type='text' value="
                                                  + returnJson[i + 1].sequenceNumber
                                                  + " /></td>"
                                                  + "<td><label>" + returnJson[i + 2].shopName
                                                  + "</label><input id="
                                                  + returnJson[i + 2].shopId
                                                  + " type='text' value="
                                                  + returnJson[i + 2].sequenceNumber
                                                  + " /></td></tr>";
                            }
                        }
                        $("#dataTable").append(dataStr);//拼接组装html片段
                    }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) { }
            });
        }
        function ShopRankModel() {
            var shopId;
            var sequenceNumber;
            var type;
            var shopName;
        }
        function Submit() {
            var selecttype = $('#ddlMealType').val();
            var txtItem = $("#dataTable tr td input");
            var array = new Array();
            for (var i = 0; i < txtItem.length; i++) {
                var model = new ShopRankModel();
                var id = txtItem[i].id;
                model.shopId = id;
                model.sequenceNumber = $("#" + id).attr("value") == '' ? 99999 : parseInt($("#" + id).attr("value")); //parseInt($("#" + id).attr("value", 10));
                model.type = selecttype;
                model.shopName = '';
                array.push(model);
            }
            if (array.length <= 0) {
                alert("请先查询修改后再提交，或者无任何可操作数据。");
                return;
            }
            var resultJson = JSON.stringify(array);
            $.ajax({
                type: "Post",
                url: "mealShopRanking.aspx/SubmitMealShopRankingData",
                data: "{'jsonstr':'" + resultJson + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == "1") {
                        alert("修改成功");
                    }
                    else {
                        alert("修改失败，请重试，再次失败请联系管理员。");
                    }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) { }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif" navigationText="" navigationUrl="" headName="年夜饭门店排序" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>年夜饭门店排序</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>套餐类别：<asp:DropDownList ID="ddlMealType" runat="server">
                                    <asp:ListItem Value="1">年夜饭</asp:ListItem>
                                </asp:DropDownList>&nbsp;&nbsp;</td>
                                <td>
                                    <input id="btnQuery" type="button" onclick="Query();" class="button" value="查看" />&nbsp;&nbsp;</td>
                                <td>
                                    <input id="btnSubmit" type="button" onclick="Submit();" class="button" value="提交" />&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table id="dataTable">
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
