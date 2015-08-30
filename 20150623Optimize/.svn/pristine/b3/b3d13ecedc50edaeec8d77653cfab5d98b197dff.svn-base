<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetWechatMenu.aspx.cs" Inherits="WeChatPlatManage_SetWechatMenu" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>微信菜单管理</title>
    <link href="../Css/css.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var orderStatisticsUrl = "";
        var retStr = "";
        var retObj;
        var iIndex;
        var jIndex;
        $(document).ready(function () {
            $("ul.menu li:first-child").addClass("current");
            for (var i = 1; i < 4; i++) {
                var txtMain = "txt1-" + i;
                $("#" + txtMain).bind("mouseup", function (evt) { txtClick(evt); });
                for (var j = 1; j < 6; j++) {
                    var txtSub = "txt2-" + j + "-" + i;
                    $("#" + txtSub).bind("mouseup", function (evt) {
                        txtClick(evt);
                    });
                }
            }
            retStr = "({ 'menu': { 'button': [{ 'name': '推荐餐厅', 'sub_button': [{ 'type': 'click', 'name': '北京', 'key': 'btnBeijing', 'sub_button': [] }, { 'type': 'click', 'name': '上海', 'key': 'btnShanhai', 'sub_button': [] }, { 'type': 'click', 'name': '广州', 'key': 'btnGuangzhou', 'sub_button': [] }, { 'type': 'click', 'name': '杭州', 'key': 'btnHangzhou', 'sub_button': []}, { 'type': 'click', 'name': '', 'key': '', 'sub_button': []}] }, { 'name': '悠先资讯', 'sub_button': [{ 'type': 'click', 'name': '本期大奖', 'key': 'btnBqdj', 'sub_button': [] }, { 'type': 'click', 'name': '本期免单', 'key': 'btnBqmd', 'sub_button': [] }, { 'type': 'click', 'name': '本期热菜', 'key': 'btnBqrc', 'sub_button': [] }, { 'type': 'click', 'name': '亲聆老板娘', 'key': 'btnQllbn', 'sub_button': []}, { 'type': 'click', 'name': '', 'key': '', 'sub_button': []}] }, { 'name': '悠先服务', 'sub_button': [{ 'type': 'click', 'name': '悠先下载', 'key': 'btnYxxz', 'sub_button': [] }, { 'type': 'click', 'name': '悠先合作', 'key': 'btnYxhz', 'sub_button': [] }, { 'type': 'click', 'name': '常见问答', 'key': 'btnCjwd', 'sub_button': [] }, { 'type': 'click', 'name': '意见建议', 'key': 'btnYjjy', 'sub_button': [] }, { 'type': 'click', 'name': '投诉处理', 'key': 'btnTscl', 'sub_button': []}]}]} })";
        });

        function DeleteMenu() {
            var Token = $("#txtToken").val();
            if (Token == "") {
                alert("请输入Token");
                return false;
            }

            $.ajax({
                type: "Post",
                url: "../Wechatashx/DeleteMenu.ashx?Token=" + Token,
                success: function (data) {
                    alert(data);
                    try {
                        retObj = eval(data);
                        if (retObj.errmsg == "ok") {
                            $("#divResult").text("操作状态:ok");
                        } else {
                            $("#divResult").text("操作状态:删除菜单出错，稍后重试.");
                        }
                    }
                    catch (e) {
                        $("#divResult").text("操作状态:--");
                        alert(e.Message);
                    }
                },
                Error: function () {
                    alert("出错了.");
                }
            });
        }

        function GetAccessToken() {
            var AppId = $("#txtAppid").val();
            var AppSecret = $("#txtAppSecret").val();
            if (AppId == "" || AppSecret == "") {
                alert("请输入AppId和AppSecret.");
                return false;
            }

            $.ajax({
                type: "Post",
                url: "../Wechatashx/GetAccessToken.ashx?AppId=" + AppId + "&AppSecret=" + AppSecret,
                success: function (data) {
                    try {
                        var retObj = eval(data);
                        $("#txtToken").val(retObj.access_token);
                    }
                    catch (e)
                    { alert(e.Message); }
                },
                Error: function () {
                    alert("出错了");

                }
            });
        }
        //({ "menu": { "button": [{ "name": "推荐餐厅", "sub_button": [{ "type": "click", "name": "北京", "key": "btnBeijing", "sub_button": [] }, { "type": "click", "name": "上海", "key": "btnShanhai", "sub_button": [] }, { "type": "click", "name": "广州", "key": "btnGuangzhou", "sub_button": [] }, { "type": "click", "name": "杭州", "key": "btnHangzhou", "sub_button": []}] }, { "name": "悠先资讯", "sub_button": [{ "type": "click", "name": "本期大奖", "key": "btnBqdj", "sub_button": [] }, { "type": "click", "name": "本期免单", "key": "btnBqmd", "sub_button": [] }, { "type": "click", "name": "本期热菜", "key": "btnBqrc", "sub_button": [] }, { "type": "click", "name": "亲聆老板娘", "key": "btnQllbn", "sub_button": []}] }, { "name": "悠先服务", "sub_button": [{ "type": "click", "name": "悠先下载", "key": "btnYxxz", "sub_button": [] }, { "type": "click", "name": "悠先合作", "key": "btnYxhz", "sub_button": [] }, { "type": "click", "name": "常见问答", "key": "btnCjwd", "sub_button": [] }, { "type": "click", "name": "意见建议", "key": "btnYjjy", "sub_button": [] }, { "type": "click", "name": "投诉处理", "key": "btnTscl", "sub_button": []}]}]} })

        function GetCurrentMenu() {
            var Token = $("#txtToken").val();
            if (Token == "") {
                alert("请输入Token");
                return false;
            }

            $.ajax({
                type: "Post",
                url: "../Wechatashx/GetCurrentMenu.ashx?Token=" + Token,
                success: function (data) {
                    alert(data);
                    retStr = data;
                    try {
                        retObj = eval(data);
                        for (var i = 0; i < retObj.menu.button.length; i++) {
                            var txtMainName = "txt1-" + (i + 1);
                            $("#" + txtMainName).val(retObj.menu.button[i].name);
                            for (var j = 0; j < retObj.menu.button[i].sub_button.length; j++) {
                                var txtSubName = "txt2-" + (j + 1) + "-" + (i + 1);
                                $("#" + txtSubName).val(retObj.menu.button[i].sub_button[j].name);
                                //alert(retObj.menu.button[i].sub_button[j].name);
                                //$("#" + txtSubName).click(function (retObj, i, j) { alert(retObj.menu.button[i].sub_button[j].key); });
                            }
                        }
                        $("#divResult").text("操作状态:ok");
                        alert(retStr);
                    }
                    catch (e) {
                        $("#divResult").text("操作状态:--");
                        alert(e.Message);
                    }
                },
                Error: function () {
                    alert("出错了.");
                }
            });
        }

        function UpdateMenuToServer() {
            var Token = $("#txtToken").val();
            if (Token == "") {
                alert("请输入Token");
                return false;
            }
            alert(retStr);
            $.ajax({
                type: "Post",
                url: "../Wechatashx/UpdateMenuToServer.ashx",
                data: "MenuJson=" + retStr + "&Token=" + Token,
                success: function (data) {
                    alert(data);
                    try {
                        retObj = eval(data);
                        if (retObj.errmsg == "ok") {
                            $("#divUpdateResult").text("更新状态:ok");
                        } else {
                            $("#divUpdateResult").text("更新状态:更新菜单出错，稍后重试.");
                        }
                    }
                    catch (e) {
                        $("#divUpdateResult").text("更新状态:--");
                        alert(e.Message);
                    }
                },
                Error: function () {
                    alert("出错了.");
                }
            });
        }


        function txtClick(evt) {
            var a = evt.target.getAttribute('id');
            var splitStr = a.split("-");
            //alert(splitStr);
            $("#btnName").val($("#" + a).val());
            $("#txtKey").val("");
            $("#txtUrl").val("");

            if (splitStr.length == 2) {
                //alert(2);
                iIndex = parseInt(splitStr[1]);
                showSettings(iIndex, 0);
            } else if (splitStr.length == 3) {
                //alert(3);
                iIndex = parseInt(splitStr[1]);
                jIndex = parseInt(splitStr[2]);
                showSettings(iIndex, jIndex);
            }
            //alert("iIndex:" + iIndex + "--jIndex:" + jIndex);

        }

        function showSettings(m, n) {
            if (n == 0) {
                //$("#btnName").val(retObj.menu.button[m - 1].name);
                var bType = retObj.menu.button[m - 1].type;
                //alert(bType);
                setRadioButton(bType, m - 1, -1);
            }
            else {
                //$("#btnName").val(retObj.menu.button[n - 1].sub_button[m - 1].name);
                var sType = retObj.menu.button[n - 1].sub_button[m - 1].type;
                //alert(sType);
                setRadioButton(sType, n - 1, m - 1);
            }
        }

        function setRadioButton(seType, iMain, iSub) {
            alert("seType:" + seType);
            if (seType == "click") {
                $("#typeClick").attr("checked", true);
                radioChanged(0);
                if (iSub == -1) {
                    $("#txtKey").val(retObj.menu.button[iMain].key);
                }
                else {
                    $("#txtKey").val(retObj.menu.button[iMain].sub_button[iSub].key);
                }
            } else {
                $("#typeUrl").attr("checked", true);
                radioChanged(1);
                if (iSub == -1) {
                    $("#txtUrl").val(retObj.menu.button[iMain].url);
                }
                else {
                    $("#txtUrl").val(retObj.menu.button[iMain].sub_button[iSub].url);
                }
            }
        }

        function radioChanged(type) {
            if (type == 0) {
                $("#divKey").css("display", "block");
                $("#divUrl").css("display", "none");
            }
            else {
                $("#divUrl").css("display", "block");
                $("#divKey").css("display", "none");
            }
        }
    </script>
    <style type="text/css">
        li
        {
            white-space: nowrap;
        }
        .tbButtons input
        {
            width: 90%;
            height: 32px;
            font-size: larger;
        }
        .btnButton
        {
            background-color: rgb(211, 220, 224);
            border: 1px solid rgb(120, 120, 120);
            cursor: pointer;
            font-size: 1.2em;
            font-weight: 300;
            padding: 7px;
            margin-right: 8px;
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 头部菜单 Start -->
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="设置菜单" navigationUrl="" headName="微信平台-设置菜单" />
    <!-- 头部菜单 end -->
    <!-- 主编辑区 -->
    <div id="box" class="box">
        <div class="tagMenu" id="tagMenu">
            <ul class="menu">
                <li onclick="ClickFirstLi()">设置菜单</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div style="width:96%;margin:2px auto;">
                    <h3>使用说明及规则，请仔细阅读</h3>
                    <ul>
                        <li>1.官方要求：一级菜单按钮个数为2-3个</li>
                        <li>2.官方要求：如果设置了二级菜单，子按钮个数为2-5个</li>
                        <li>3.官方要求：一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以"..."代替</li>
                        <li>4.如果name不填，此按钮将被忽略</li>
                        <li>5.如果一级菜单为空，该列所有设置的二级菜单都会被忽略</li>
                        <li>6.key仅在SingleButton（单击按钮，无下级菜单）的状态下设置，如果此按钮有下级菜单，key将被忽略</li>
                        <li>7.所有二级菜单都为SingleButton</li>
                        <li>8.如果要快速看到微信上的菜单最新状态，需要重新关注，否则需要静静等待N小时</li>
                    </ul>
                    <h3>编辑工具</h3>
                    <div>
                        <table style="line-height:38px;width:600px;">
                            <tr><td style="width:80px;text-align:right;">AppId:</td><td><input type="text" id="txtAppid" style="width:90%;font-size:larger;" /></td></tr>
                            <tr><td style="width:80px;text-align:right;">AppSecret:</td><td><input type="text" id="txtAppSecret" style="width:90%;font-size:larger;" /></td></tr>
                        </table>
                        <input type="button" id="btnGetAccessToken" value="获取AccessToken" onclick="GetAccessToken()" class="btnButton" />
                    </div>
                    <table style="line-height:38px;">
                        <tr>
                            <td style="width:80px;text-align:right;">当前Token:</td>
                            <td><asp:TextBox ID="txtToken" Width="800px" style="width:800px;font-size:larger;" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    <div style="line-height:35px;">
                        <input type="button" id="btnGetCurentMenu" value="获取当前菜单" onclick="GetCurrentMenu()" class="btnButton" />
                        <input type="button" id="btnDeleteMenu" value="删除菜单" onclick="DeleteMenu()" class="btnButton" />
                        <br />
                        <div id="divResult">操作状态:--</div>
                    </div>
                    <div style="float:left;">
                        <div style="width:650px;float:left;">
                            <table style="line-height:38px;width:100%;" id="tbButtons" class="tbButtons">
                                <tr><td style="width:120px;"></td><td><b>第一列</b></td><td><b>第二列</b></td><td><b>第三列</b></td></tr>
                                <tr><td>二级菜单No.1</td><td><input type="text" id="txt2-1-1" /></td><td><input type="text" id="txt2-1-2" /></td><td><input type="text" id="txt2-1-3" /></td></tr>
                                <tr><td>二级菜单No.2</td><td><input type="text" id="txt2-2-1" /></td><td><input type="text" id="txt2-2-2" /></td><td><input type="text" id="txt2-2-3" /></td></tr>
                                <tr><td>二级菜单No.3</td><td><input type="text" id="txt2-3-1" /></td><td><input type="text" id="txt2-3-2" /></td><td><input type="text" id="txt2-3-3" /></td></tr>
                                <tr><td>二级菜单No.4</td><td><input type="text" id="txt2-4-1" /></td><td><input type="text" id="txt2-4-2" /></td><td><input type="text" id="txt2-4-3" /></td></tr>
                                <tr><td>二级菜单No.5</td><td><input type="text" id="txt2-5-1" /></td><td><input type="text" id="txt2-5-2" /></td><td><input type="text" id="txt2-5-3" /></td></tr>
                                <tr><td>一级菜单按钮</td><td><input type="text" id="txt1-1" /></td><td><input type="text" id="txt1-2" /></td><td><input type="text" id="txt1-3" /></td></tr>
                            </table>
                        </div>
                        <div style="width:300px;float:left;">
                            <table style="line-height:38px;width:100%;">
                                <tr><td colspan="2"><b>按钮其它参数</b></td></tr>
                                <tr><td style="width:50px;">Name:</td><td><input type="text" id="btnName" disabled="disabled" style="width:90%;font-size:larger;" /></td></tr>
                                <tr><td>Type:</td><td><input type="radio" name="radSelect" onclick="radioChanged(0)" id="typeClick" />点击事件(传回服务器)<br /><input type="radio" name="radSelect" onclick="radioChanged(1)" id="typeUrl" />访问网页(直接跳转)</td></tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="divKey">
                                            <table style="line-height:38px;width:100%;">
                                                <tr><td style="width:50px;">Key:</td><td><input type="text" id="txtKey" style="width:90%;font-size:larger;" /></td></tr>
                                            </table>
                                        </div>
                                        <div id="divUrl" style="display:none;">
                                            <table style="line-height:38px;width:100%;">
                                                <tr><td style="width:50px;">Url:</td><td><input type="text" id="txtUrl" style="width:90%;font-size:larger;" /></td></tr>
                                            </table>
                                        </div>
                                        <div>
                                            如果还有下级菜单请忽略Type和Key、Url.
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="height:100px;width: 100%; margin-top: 20px; float: left;line-height:35px;">
                        <input type="button" id="updateToServer" value="更新到服务器" onclick="UpdateMenuToServer()" class="btnButton" />
                        <br />
                        <div id="divUpdateResult">更新状态：--</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>