<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomPushConfig.aspx.cs"
    Inherits="SystemConfig_CustomPushConfig" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义推送配置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        //        $(document).ready(function () {
        //            $(".edit").attr("disabled", "disabled");
        //            $(".divCustom").hide();
        //            $("#ddlType").val("NA");

        //            $("#btnCheck").click(function () {
        //                var phone = $("#txbMobilePhoneNumber").val();
        //                $.ajax({
        //                    type: "Post",
        //                    url: "CustomPushConfig.aspx/CheckUser",
        //                    data: "{ 'phone':'" + phone + "' }",
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: function (data) {
        //                        //返回的数据用data.d获取内容      
        //                        var obj = eval('(' + data.d + ')');

        //                        if (obj.CustomerID != null) {
        //                            $("#lbCustomerID").text(obj.CustomerID);
        //                            $("#lbPushToken").text(obj.pushToken);
        //                            switch (obj.appType) {
        //                                case 1:
        //                                    $("#lbAppType").text("IOS");
        //                                    break;
        //                                case 3:
        //                                    $("#lbAppType").text("Android");
        //                                    break;
        //                                default:
        //                                    break;
        //                            }
        //                            $(".edit").attr("disabled", "");
        //                        }
        //                        else {
        //                            $("#lbCustomerID").text("");
        //                            $("#lbPushToken").text("");
        //                            $("#lbAppType").text("");
        //                            alert(obj.err);
        //                        }
        //                    },
        //                    error: function (err) {
        //                        alert('出错啦');
        //                    }
        //                });
        //            });

        //            $("#ddlType").change(function () {
        //                var selectedValue = $("#ddlType").val();
        //                switch (selectedValue) {
        //                    case "recommend":
        //                        $("#divRecommend").show();
        //                        $("#divOrder").hide();
        //                        //                                $("#divRedEnvelope").hide();
        //                        //                                $("#divOrderDetail").hide();
        //                        break;
        //                    case "order":
        //                        $("#divRecommend").hide();
        //                        $("#divOrder").show();
        //                        //                                $("#divRedEnvelope").hide();
        //                        //                                $("#divOrderDetail").hide();
        //                        break;
        //                    case "redEnvelope":
        //                        $("#divRecommend").hide();
        //                        $("#divOrder").hide();
        //                        //                                $("#divRedEnvelope").show();
        //                        //                                $("#divOrderDetail").hide();
        //                        break;
        //                    case "orderDetail":
        //                        $("#divRecommend").hide();
        //                        $("#divOrder").hide();
        //                        //                                $("#divRedEnvelope").hide();
        //                        //                                $("#divOrderDetail").show();
        //                        break;
        //                }
        //            });

        //            $("#btnSave").click(function () {
        //                var attachment = "";
        //                var type = $("#ddlType").val();
        //                switch (type) {
        //                    case "recommend":
        //                        attachment = $("#txbRecommendUrl").val();
        //                        break;
        //                    case "order":
        //                        attachment = $("#txbShopName").val();
        //                        break;
        //                    case "redEnvelope":
        //                    case "orderDetail":
        //                        break;
        //                }

        //                var message = $("#txbMessage").val();
        //                var customSendTime = $("#txbCustomSendTime").val();

        //                $.ajax({
        //                    type: "Post",
        //                    url: "CustomPushConfig.aspx/Save",
        //                    data: "{ 'type': '" + type + "', 'message': '" + message + "', 'attachment': '" + attachment + "', 'customSendTime': '" + customSendTime + "'}",
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: function (data) {
        //                        var obj = eval('(' + data.d + ')');
        //                        alert(obj.result);
        //                    },
        //                    error: function (err) {
        //                        alert("出错啦");
        //                    }
        //                });

        //            });

        //            function clear() {
        //                $("#txbMobilePhoneNumber").text("");
        //                $("#lbCustomerID").text("");
        //                $("#lbPushToken").text("");
        //                $("#lbAppType").text("");
        //                $("#ddlType").text("NA");
        //                $("#txbMessage").text("");
        //                $("#txbCustomSendTime").text("");

        //            }
        //        });

        function shopSearch() {
            var str = $("#shopName").val();
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
                error: function (err) {
                    alert("获取数据失败");
                }
            });
        }

        function selectShop(shop) {
            var name = $("#" + shop.id).attr('data-name');
            $("#init_date").html('');
            $("#shopName").val(name);
            $("#hiddenShopId").val(shop.id);
            //window.location.href = "BusniessApplyPayment.aspx?id=" + shop.id + "&name=" + name + "";
        }

        //        onclick = 'selectShop(this)'
        function checkPhone() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            if (document.getElementById("txbMobilePhoneNumber").value.length == 0) {
                err += "【请先输入客户手机】\r\n";
            }
            if (err.length > 0) {
                alert(msg + err);
                return false;
            }
            else {
                return true;
            }
        }

        function Check() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            if (document.getElementById("txbMobilePhoneNumber").value.length == 0) {
                err += "【请先验证客户手机】\r\n";
            }
            if (document.getElementById("ddlType").value == 'NA') {
                err += "【请选择推送类别】\r\n";
            }
            if (document.getElementById("txbMessage").value.length == 0) {
                err += "【推送消息内容不能为空】\r\n";
            }
            if (document.getElementById("txbCustomSendTime").value.length == 0) {
                err += "【自定义推送时间不能为空】\r\n";
            }
            var select = document.getElementById("ddlType").value;
            switch (select) {
                case "recommend":
                    if (document.getElementById("txbRecommendUrl").value.length == 0) {
                        err += "【url链接不能为空】\r\n";
                    }
                    break;
                case "order":
                    if (document.getElementById("shopName").value.length == 0) {
                        err += "【店铺名称不能为空】\r\n";
                    }
                    break;
                case "redEnvelope":
                case "orderDetail":
                    break;
            }
            if (err.length > 0) {
                alert(msg + err);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="自定义推送配置" />
            <div class="content" id="divList" runat="server">
                <div class="layout">
                    <div class="QueryTerms">
                        <table style="width: 100%; border: 1px;" cellpadding="5" cellspacing="5">
                            <tr>
                                <td style="text-align: right;">自定义发送时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txbBegin" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                        Width="150px"></asp:TextBox>~~
                                <asp:TextBox ID="txbEnd" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">类别
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList runat="server" ID="ddlTypeQuery">
                                        <asp:ListItem Text="==请选择==" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="推荐专题" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="点菜页面" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="红包列表" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="订单列表" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="领红包H5" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="美食广场" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="首页" Value="23"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">是否推送成功
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="rblIsSent" RepeatDirection="Horizontal" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblIsSent_SelectedIndexChanged">
                                        <asp:ListItem Value="true" Text="True"></asp:ListItem>
                                        <asp:ListItem Value="false" Text="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: right;">客户手机
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txbMobilePhoneNumberQuery" Width="160px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="新建" CssClass="button" OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel_PushList" runat="server">
                        <div class="div_gridview">
                            <asp:GridView runat="server" ID="gdvPushList" AutoGenerateColumns="False" CssClass="gridview"
                                SkinID="gridviewSkin" OnDataBound="gdvPushList_DataBound" DataKeyNames="customType,isSent">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="流水号" Visible="false" />
                                    <asp:BoundField DataField="customType" HeaderText="类别" HeaderStyle-Width="15%" />
                                    <%-- <asp:BoundField DataField="customValue" HeaderText="参数" HeaderStyle-Width="20%" />--%>
                                    <asp:BoundField DataField="message" HeaderText="推送消息内容" HeaderStyle-Width="30%" />
                                    <%-- <asp:BoundField DataField="appType" HeaderText="设备类别" HeaderStyle-Width="3%" />--%>
                                    <asp:BoundField DataField="customSendTime" HeaderText="设定时间" HeaderStyle-Width="15%" />
                                    <%--<asp:BoundField DataField="sendCount" HeaderText="推送次数" HeaderStyle-Width="3%" />--%>
                                    <asp:BoundField DataField="isSent" HeaderText="状态" HeaderStyle-Width="5%" />
                                    <asp:BoundField DataField="sendTime" HeaderText="发送时间" HeaderStyle-Width="15%" />
                                    <asp:TemplateField HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <img src="../Images/key_delete.gif" alt="" />
                                            <asp:LinkButton runat="server" ID="lnkbtnDel" CommandName="del" OnCommand="lnkbtn_OnCommand"
                                                OnClientClick="return confirm('确认删除？');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>删除</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <img src="../Images/key_detail.gif" alt="" />
                                            <asp:LinkButton runat="server" ID="lnkbtnDetail" CommandName="detail" OnCommand="lnkbtn_OnCommand"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>详情</asp:LinkButton>
                                            <%-- <asp:LinkButton runat="server" ID="lnkbtnDetail" title='<%# DataBinder.Eval(Container.DataItem, "mobilePhoneNumber") %>'>查看</asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="asp_page">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                    CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                    OnPageChanged="AspNetPager1_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="content" id="divDetail" runat="server" style="width: 100%; display: none;">
                <div style="width: 80%">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th style="width: 30%">接收推送客户手机
                            </th>
                            <td>
                                <asp:FileUpload runat="server" ID="fileUploadPhone" Width="410px" Height="25px" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnImport" Text="导入Excel" class="button" OnClick="btnImport_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th>接收推送客户手机
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbMobilePhoneNumber" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <%--<input type="button" id="btnCheck" class="button" value="验证" />--%>
                                <asp:Button runat="server" ID="btnCheck" class="button" Text="验证" OnClick="btnCheck_Click"
                                    OnClientClick="return checkPhone();" />
                                <br />
                                【多个电话号码请用英文逗号隔开】
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView runat="server" ID="gdvCustomerDevice" AutoGenerateColumns="false" CssClass="gridview"
                                    SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:BoundField DataField="CustomerID" HeaderText="用户ID" />
                                        <asp:BoundField DataField="UserName" HeaderText="昵称" />
                                        <asp:BoundField DataField="mobilePhoneNumber" HeaderText="手机号码" />
                                        <asp:BoundField DataField="pushToken" HeaderText="pushToken" />
                                        <asp:BoundField DataField="appType" HeaderText="设备类别" />
                                    </Columns>
                                </asp:GridView>
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                        OnPageChanged="AspNetPager2_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>推送类别
                            </th>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlType" class="edit" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Text="==请选择==" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="推荐专题" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="点菜页面" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="红包列表" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="订单列表" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="领红包H5" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="美食广场" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="首页" Value="23"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>推送消息内容
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbMessage" class="edit" TextMode="MultiLine" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>自定义推送时间
                            </th>
                            <td>
                                <asp:TextBox ID="txbCustomSendTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'})"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divRecommend" runat="server" style="width: 80%">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th style="width: 30%">url链接
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbRecommendUrl" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divOrder" runat="server" class="layout" style="width: 80%">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th style="width: 30%">店铺名称
                            </th>
                            <td>
                                <%-- <asp:TextBox runat="server" ID="txbShopName"></asp:TextBox>--%>
                                <asp:HiddenField runat="server" ID="hiddenShopId" />
                                <input id="shopName" runat="server" type="text" onkeyup="shopSearch()" style="width: 150px" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--<div id="divRedEnvelope" runat="server" class="layout" style="width: 60%;">
            </div>
            <div id="divOrderDetail" runat="server" class="layout" style="width: 60%;">
            </div>--%>
                <br />
                <div style="width: 80%; text-align: center">
                    <%--<input type="button" id="btnSave" value="保存" class="button" />--%>
                    <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClick="btnSave_Click"
                        OnClientClick="return Check();" />
                    <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                </div>
            </div>
            <uc1:CheckUser ID="CheckUser1" runat="server" />
        </div>
    </form>
</body>
</html>
