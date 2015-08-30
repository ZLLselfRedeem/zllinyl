﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMenuInfo.aspx.cs" Inherits="Award_EditMenuInfo" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑赠菜信息</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <%--<script src="../Scripts/messages_cn.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>--%>
    <script language="javascript">
        function medalManage() {
            var str = $("#txtDishName").val();
            if (str == "") {
                return;
            }
            $.ajax({
                type: "Post",
                url: 'AwardMsg.aspx/SearchDishMeau',
                data: JSON.stringify({
                    pageIndex: 1,
                    pageSize: 10,
                    key: str,
                    shopID: $("#hidShopID").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var returnJson = eval("(" + data.d + ")");
                        var strHtml = "<ul>";
                        for (var i = 0; i < returnJson.dishInfoDetailList.length; i++) {
                            if (i == 0) {
                                $("#init_date").html('');
                            }
                            strHtml += "<li onclick='selectDish(this)' id='"
                                               + returnJson.dishInfoDetailList[i].dishID + "' dishName='"
                                               + returnJson.dishInfoDetailList[i].dishName + "' dishPriceID='" + returnJson.dishInfoDetailList[i].dishPriceID + "'>&nbsp;&nbsp;"
                                               + returnJson.dishInfoDetailList[i].dishName + "&nbsp;&nbsp;</li>"
                        }
                        strHtml += "</ul>";
                        $("#init_date").append(strHtml);
                    }
                }
            });
        }

        function selectDish(dish) {
            $("#hidDishID").val(dish.id);
            $("#hidDishPriceId").val(dish.attributes["dishPriceID"].value);
            $("#txtDishName").val(dish.attributes["dishName"].value);

            //var name = $("#" + dish.id).attr('dishName');
            //$("#hidDishID").val(dish.id);
            //$("#hidDishPriceId").val($("#" + dish.id).attr('dishPriceID'));
            //$("#txtDishName").val(name);

            $("#init_date").html('');
        }
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="编辑赠菜信息" navigationImage="~/images/icon/list.gif"
        navigationText="编辑商户详情" navigationUrl="javascript:history.go(-1)" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>基本信息</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            赠菜名称：
                        </th>
                        <td>
                            <input id="txtDishName" runat="server" type="text" onkeyup="medalManage()" value=""/>
                            <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                            </div>
                            <asp:HiddenField ID="hidDishID" runat="server" />
                            <asp:HiddenField ID="hidDishPriceId" runat="server" />
                            <asp:HiddenField ID="hidOldDishID" runat="server" />
                            <asp:HiddenField ID="hidOldDishPriceId" runat="server" />
                            <asp:HiddenField ID="hidShopID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            赠菜份数：
                        </th>
                        <td>
                            <asp:TextBox ID="txtDishCount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="保    存"  CssClass="button" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancle" runat="server" Text="取    消"  CssClass="button" OnClientClick="javascript:history.go(-1);"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
