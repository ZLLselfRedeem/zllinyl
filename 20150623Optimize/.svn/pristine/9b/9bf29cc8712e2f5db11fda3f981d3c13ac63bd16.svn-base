<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uxianRecharge.aspx.cs" Inherits="CustomerServiceProcessing_uxianRecharge" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>悠先账户充值</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/CommonScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_List", "gv_OverRow");
        });
        function customerModel() {
            var customerPhone; //用户手机
            var customerName; //用户昵称
        }
        function customer() {
            var customerPhone;
            var customerName;
            var amount;
            var remark;
        }
        function addEmployee() {
            var phone = $("#txt_phone").val();
            if (phone == "") { alert("手机号码不能为空"); return; }
            var amount = $("#txt_amount").val(); //获取金额文本框金额内容
            var remark = $("#txt_remark").val(); //获取备注文本框内容
            var reg = /^[-+]?(([1-9]\d*)|0)(\.\d{1,2})?$/;
            if (!reg.test(amount)) { alert("金额输入不合法"); return; }
            if (remark == "") { alert("备注内容不能为空"); return; }
            $.ajax({
                type: "Post",
                url: "uxianRecharge.aspx/GetCustomerInfo",
                data: "{'phone':'" + phone + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var a = $("#rechergeObject tr").children();
                        var b = $("#rechergeObject tr").length;
                        var list = new Array();
                        for (var i = 1; i < b; i++) {
                            var model = new customerModel();
                            model.customerPhone = a.eq(i * 2 + 1).text();
                            model.customerName = a.eq(i * 2).text();
                            list.push(model);
                        }

                        var returnJson = eval("(" + data.d + ")"); //获取后台查询返回的json，反序列化
                        var model1 = new customerModel();
                        model1.customerPhone = returnJson.customerPhone;
                        model1.customerName = returnJson.customerName;
                        if (list.length > 0) {//数组
                            for (var i = 0; i < list.length; i++) {
                                if (list[i].customerPhone == model1.customerPhone) { alert("当前手机号码已添加到列表，请注意核实"); return; }
                                else { continue; }
                            }
                        }
                        var hanghao = parseInt($('#divlist table tr:last td:first').text()) + 1;
                        $("#divlist table").append("<tr>"
                                                   + "<td>" + hanghao + "</td>"
                                                   + "<td>" + model1.customerName + "</td>"
                                                   + "<td>" + model1.customerPhone + "</td>"
                                                   + "<td>" + amount + "</td>"
                                                   + "<td>" + remark + "</td>"
                                                   + "</tr>");
                        $("#lbTotalAmount").val(parseInt($("#lbTotalAmount").val()) + parseInt($("#txt_amount").val()));
                        $("#txt_phone").val('');
                        $("#txt_amount").val('');
                        $("#txt_remark").val('');
                        list = [];
                    }
                    else { alert("手机号码输入有误"); }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert("error"); }
            });
        }
        //执行打款操作，充值操作
        function rechargeOperate() {
            var arr = new Array();
            var flag = 1;
            $('#rechergeObject').find('tr').each(function () {
                if (flag == 1) {
                    flag = 2;
                }
                else {
                    var that = $(this).find('td');
                    var model = new customer();
                    model.customerName = that.eq(1).text();
                    model.customerPhone = that.eq(2).text();
                    model.amount = that.eq(3).text();
                    model.remark = that.eq(4).text();
                    arr.push(model);
                }
            });
            //alert(JSON.stringify(arr));
            if (arr.length <= 0) { alert("请先添加需要充值的员工"); return; }
            $.ajax({
                type: "Post",
                url: "uxianRecharge.aspx/RechargeOperate",
                data: "{'data':'" + jQuery.toJSON(arr) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        if (data.d == "充值申请提交成功") {
                            arr.length = 0; //清空数组
                            $("#txt_phone").val('');
                            $("#txt_amount").val('');
                            $("#txt_remark").val('');
                            $("#divlist table").html('').append("<tr><td>行号</td><td>昵称</td><td>手机号码</td><td>金额</td><td>备注</td></tr>"); //清空页面trtd
                        }
                        alert(data.d);
                    }
                    else { alert("充值失败，请重试"); }
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) { alert("error"); }
            });
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 500px;
            height: 30px;
        }

        .auto-style3 {
            height: 30px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="" navigationText=""
            navigationUrl="" headName="悠先账户充值" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    操作类型
                <asp:RadioButton ID="rb_add" GroupName="rb" Text="充值" OnCheckedChanged="rb_query_CheckedChanged"
                    Checked="true" AutoPostBack="true" runat="server" />
                    <asp:RadioButton ID="rb_query" AutoPostBack="true" GroupName="rb" Text="历史记录" runat="server"
                        OnCheckedChanged="rb_query_CheckedChanged" />
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <asp:Panel ID="panel_add" CssClass="div_gridview" runat="server">
                        <div class="QueryTerms">
                            <table>
                                <tr>
                                    <td>批量添加：</td>
                                    <td>
                                        <asp:FileUpload runat="server" ID="fileUploadPhone" Width="200px" Height="25px" /></td>
                                    <td colspan="3">
                                        <asp:Button ID="btnExportData" CssClass="button" runat="server" Text="导入数据" OnClick="btnExportData_Click" />
                                        (excel数据为3列：手机号码、金额、备注)</td>
                                </tr>
                                <tr>
                                    <td>单个添加：</td>
                                    <td>
                                        <label>
                                            手机号码：</label><input id="txt_phone" type="text" />
                                    </td>
                                    <td>
                                        <label>
                                            金额：</label><input id="txt_amount" type="text" />
                                    </td>
                                    <td>
                                        <label>
                                            备注：</label><textarea id="txt_remark" cols="20" style="height: 40px; width: 200px"
                                                rows="2"></textarea>
                                    </td>
                                    <td>
                                        <input id="btn_add" type="button" class="button" value="添  加" onclick="addEmployee()" />
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="divlist">
                            <table class="table" runat="server" id="rechergeObject">
                                <tr>
                                    <td class="auto-style3">行号
                                    </td>
                                    <td class="auto-style3">昵称
                                    </td>
                                    <td class="auto-style3">手机号码
                                    </td>
                                    <td class="auto-style3">金额
                                    </td>
                                    <td class="auto-style1">备注
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <input id="btn_operate" type="button" class="couponButtonSubmit" value="提交申请" onclick="rechargeOperate()" />（总打款金额共计<asp:Label ID="lbTotalAmount" runat="server" Text="0"></asp:Label>元）
                    </asp:Panel>
                    <asp:Panel ID="panel_list" runat="server">
                        <table>
                            <tr>
                                <td>&nbsp;
                                    <label>
                                        开始时间：</label><asp:TextBox ID="TextBox_startTime" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                            Style="width: 140px"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                    <label>
                                        结束时间：<asp:TextBox ID="TextBox_endTime" class="Wdate" runat="server" Style="width: 140px"
                                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                                    </label>
                                </td>
                                <td>
                                    <asp:Button ID="btn_Search" class="button" runat="server" Text="查   询" OnClick="btn_Search_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView_List" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                            SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="operateTime" HeaderText="申请时间" />
                                <asp:BoundField DataField="EmployeeFirstName" HeaderText="申请人" />
                                <asp:BoundField DataField="amount" HeaderText="金额" />
                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                <asp:BoundField DataField="customerPhone" HeaderText="手机号码" />
                                <asp:BoundField DataField="status" HeaderText="操作状态" />
                                <asp:BoundField DataField="approvalEmployee" HeaderText="审核人" />
                                <asp:BoundField DataField="approvalTime" HeaderText="审核时间" />
                            </Columns>
                        </asp:GridView>
                        <div class="gridviewBottom_left">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" FirstPageText="首页"
                                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
