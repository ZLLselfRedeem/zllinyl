<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clientRechargeManage.aspx.cs"
    Inherits="ClientRecharge_ClientRechargeManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>充值活动管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            GridViewStyle("gdvRechageList", "gv_OverRow");
        });
        //    //必需是整数
        function isNumber(value) {
            //var rules = "^[1-9][0-9]*$"; //正整数
            var rules = "^[1-9]\d*|0$"; //整数

            if (value.match(rules) == null) {
                return false;
            }
            else {
                return true;
            }
        }
        function isFloat(value) {
            var rules = "^[1-9]{1}[0-9]{0,}\.[0-9]{2}$"; //第一位为 1-9 之间的数字，之后为 0-9 不限定位数，一个小数点后两位数字
            if (value.match(rules) == null) {
                return false;
            }
            else {
                return true;
            }
        }

        function isTimePart(timeStr) {
            var parts;
            parts = timeStr.split(':');
            if (parts.length < 1) {
                //日期部分不允许缺少小时、分钟、秒中的任何一项
                return false;
            }
            for (i = 0; i < parts.length; i++) {
                //如果构成时间的某个部分不是数字，则返回false
                if (isNaN(parts[i])) {
                    return false;
                }
            }
            h = parts[0]; //小時
            m = parts[1]; //分
            s = parts[2]; //秒
            if (h < 0 || h > 23) {
                //限制小时的范围
                return false;
            }
            if (m < 0 || m > 59) {
                //限制分钟的范围
                return false;
            }
            if (s < 0 || s > 59) {
                //限制秒的范围
                return false;
            }
            return true;
        }

        function Check() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            if (document.getElementById("txbName").value.length == 0) {
                err += "【活动名称不能为空】\r\n";
            }
            if (document.getElementById("txbRechargeCondition").value.length == 0) {
                err += "【充值条件不能为空】\r\n";
            }
            if (document.getElementById("txbRechargeCondition").value.length > 0) {
                if (!isFloat(document.getElementById("txbRechargeCondition").value) && !isNumber(document.getElementById("txbRechargeCondition").value)) {
                    err += "【请输入正确的充值条件】\r\n";
                }
            }
            if (document.getElementById("txbPresent").value.length == 0) {
                err += "【赠送金额不能为空】\r\n";
            }
            if (document.getElementById("txbPresent").value.length > 0) {
                if (!isFloat(document.getElementById("txbPresent").value) && !isNumber(document.getElementById("txbPresent").value)) {
                    err += "【请输入正确的赠送金额】\r\n";
                }
            }
            if (document.getElementById("txbBeginTime").value.length == 0) {
                err += "【开始时间不能为空】\r\n";
            }
            if (document.getElementById("txbBeginTime").value.length > 0) {
                if (!isTimePart(document.getElementById("txbBeginTime").value)) {
                    err += "【请输入正确的开始时间】\r\n";
                }
            }
            if (document.getElementById("txbEndTime").value.length == 0) {
                err += "【结束时间不能为空】\r\n";
            }
            if (document.getElementById("txbEndTime").value.length > 0) {
                if (!isTimePart(document.getElementById("txbEndTime").value)) {
                    err += "【请输入正确的结束时间】\r\n";
                }
            }
            if (document.getElementById("txbExternalSold").value.length == 0) {
                err += "【对外已售数量不能为空】\r\n";
            }
            if (document.getElementById("txbExternalSold").value.length > 0) {
                if (!isNumber(document.getElementById("txbExternalSold").value)) {
                    err += "【请输入正确的对外已售数量】\r\n";
                }
            }
            if (document.getElementById("txbSequence").value.length == 0) {
                err += "【排序号不能为空】\r\n";
            }
            if (document.getElementById("txbSequence").value.length > 0) {
                if (!isNumber(document.getElementById("txbSequence").value)) {
                    err += "【请输入正确的排序号】\r\n";
                }
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
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="充值活动管理" navigationUrl="" headName="充值活动管理" />
    <div class="content" id="divList" runat="server">
        <div class="layout">
            <div class="QueryTerms">
                <table style="width: 100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td width="12%">
                            <asp:RadioButtonList runat="server" ID="rblStatus" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblStatus_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="已开启" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已关闭" Value="-1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            活动名称&nbsp;
                            <asp:TextBox runat="server" ID="txbRechageName"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="新建活动" CssClass="button" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_RechageList" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvRechageList" AutoGenerateColumns="False" CssClass="gridview"
                        DataKeyNames="id,status" SkinID="gridviewSkin" OnRowCommand="gdvRechageList_RowCommand"
                        OnDataBound="gdvRechageList_DataBound">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="编号" />
                            <asp:BoundField DataField="name" HeaderText="名称" />
                            <asp:BoundField DataField="rechargeCondition" HeaderText="充值条件(元)" />
                            <asp:BoundField DataField="present" HeaderText="赠送(元)" />
                            <asp:BoundField DataField="beginTime" HeaderText="开始时间" />
                            <asp:BoundField DataField="endTime" HeaderText="结束时间" />
                            <asp:BoundField DataField="externalSold" HeaderText="对外已售" />
                            <asp:BoundField DataField="actualSold" HeaderText="实际已售" />
                            <asp:BoundField DataField="sequence" HeaderText="排序号" />
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <asp:Label ID="Label_status" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="SetStatus" Text="开启/关闭"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>修改</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <th>
                                    共
                                </th>
                                <td>
                                    <asp:Label ID="lbRechargeCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    个活动
                                </th>
                                <th>
                                </th>
                                <th>
                                    实际已售总数
                                </th>
                                <td>
                                    <asp:Label ID="lbActualSoldCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    个
                                </th>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="content" id="divDetail" runat="server" style="display: none">
        <div class="layout">
            <table class="table" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <th width="20%">
                        活动编号
                    </th>
                    <td width="90%">
                        <asp:Label runat="server" ID="lbId"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        活动名称
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        充值条件(元)
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbRechargeCondition"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        赠送(元)
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbPresent"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        开始时间:
                    </th>
                    <td>
                        <asp:TextBox ID="txbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'HH:mm:ss'})"
                            Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        结束时间:
                    </th>
                    <td>
                        <asp:TextBox ID="txbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'HH:mm:ss'})"
                            Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        对外已售
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbExternalSold"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        实际已售
                    </th>
                    <td>
                        <asp:Label runat="server" ID="lbActualSold"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        排序号
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbSequence"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        状态
                    </th>
                    <td>
                        <asp:Label runat="server" ID="lbStatus"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClick="btnSave_Click"
                            OnClientClick="return Check();" />
                        <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
