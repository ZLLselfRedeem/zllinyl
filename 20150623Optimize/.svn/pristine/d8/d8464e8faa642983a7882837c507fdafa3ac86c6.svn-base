<%@ Page Language="C#" AutoEventWireup="true" CodeFile="exchangeQuery.aspx.cs" Inherits="PointsManage_exchangeQuery" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>兑换查询</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content" id="divList" runat="server">
        <div class="layout">
            <asp:Panel ID="Panel_List" runat="server">
                <asp:Panel ID="Panel_QueryCondition" runat="server">
                    <div class="QueryTerms">
                        <table cellpadding="5" cellspacing="5">
                            <tr>
                                <td>
                                    周期选择
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rblPeriod" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblPeriod_SelectedIndexChanged">
                                        <asp:ListItem Text="本周一至今" Selected="True" Value="week">                            
                                        </asp:ListItem>
                                        <asp:ListItem Text="本月1日至今" Value="month"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    自定义
                                    <asp:TextBox ID="TextBox_TimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                        OnTextChanged="TextBox_TimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                                    &nbsp;-&nbsp;
                                    <asp:TextBox ID="TextBox_TimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                        OnTextChanged="TextBox_TimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table>
                            <tr>
                                <td>
                                    发货状态
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rblShipStatus"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblShipStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="未发货" Selected="True" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="已发货" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table>
                            <tr>
                                <td>
                                    服务员手机号码&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txbPhoneNumber"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp; &nbsp;<asp:Button runat="server" ID="btnQuery" Text="搜索" CssClass="button"
                                        OnClick="btnQuery_Click" />
                                    &nbsp; &nbsp;
                                    <asp:Button runat="server" ID="btnClear" Text="重置" CssClass="button" OnClick="btnClear_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_ExchangeList" runat="server">
                    <div class="div_gridview">
                        <asp:GridView runat="server" ID="gdvExchangeList" AutoGenerateColumns="False" CssClass="gridview"
                            SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="记录ID" />
                                <asp:BoundField DataField="GoodsName" HeaderText="商品名" />
                                <asp:BoundField DataField="EmployeeFirstName" HeaderText="服务员姓名" />
                                <asp:BoundField DataField="UserName" HeaderText="联系电话" />
                                <asp:BoundField DataField="address" HeaderText="收货门店和地址" />
                                <asp:BoundField DataField="confirmStatus" HeaderText="客户确认" />
                                <asp:BoundField DataField="shipStatus" HeaderText="发货状态" />
                                <asp:BoundField DataField="exchangeRemark" HeaderText="备注" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="../Images/key_edit2.gif" alt="" />
                                        <asp:LinkButton runat="server" ID="lnkbtnConfirm" CommandName="confirm" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'
                                            OnCommand="lnkbtnEdit_OnCommand" OnClientClick="return confirm('确认兑换？')">确认</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="../Images/key_edit2.gif" alt="" />
                                        <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'
                                            OnCommand="lnkbtnEdit_OnCommand">编辑</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="../Images/key_detail.gif" alt="" />
                                        <asp:LinkButton runat="server" ID="lnkbtnDetail" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'
                                            OnCommand="lnkbtnEdit_OnCommand">详情</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                            <div class="asp_page">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                    CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                    OnPageChanged="AspNetPager1_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
    <asp:Panel ID="Panel_Detail" runat="server" Visible="false">
        <div class="content" id="divDetail" runat="server" style="display: none">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0" width="60%">
                    <tr>
                        <td colspan="2">
                            查看兑换详情
                        </td>
                    </tr>
                    <tr>
                        <th width="30%">
                            记录ID
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbIDDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            兑换状态
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbExchangeStatusDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商品名
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbGoodsNameDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            服务员姓名
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbEmployeeNameDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系电话
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbPhoneNumberDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            收货门店和地址
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbAddressDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            兑换时间
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbExchangeTimeDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            积分扣除记录
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbPointVariationDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认状态
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbConfirmStatusDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认时间
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbConfirmTimeDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认人
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbConfirmByDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbRemarkDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            发货状态
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbShipStatusDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司或充值平台
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbPlatformDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            单号或重置流水号
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbSerialNumberDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            发货操作人
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbShipByDetail"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button runat="server" ID="btnBackDetail" Text="返回" CssClass="button" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="div_gridview">
                                <div>
                                    操作日志</div>
                                <asp:GridView runat="server" ID="gdvLogDetail" AutoGenerateColumns="False" CssClass="gridview"
                                    SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:BoundField DataField="createTime" HeaderText="时间" />
                                        <asp:BoundField DataField="EmployeeFirstName" HeaderText="操作人" />
                                        <asp:BoundField DataField="remark" HeaderText="操作" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content" id="divEdit" runat="server" style="display: none">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0" width="60%">
                    <tr>
                        <td colspan="2">
                            修改兑换记录
                        </td>
                    </tr>
                    <tr>
                        <th width="30%">
                            记录ID
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbIDEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            兑换状态
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbExchangeStatusEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            商品名
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbGoodsNameEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            服务员姓名
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbEmployeeNameEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            联系电话
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbPhoneNumberEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            收货门店和地址
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbAddressEdit" TextMode="MultiLine" Width="245px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            兑换时间
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbExchangeTimeEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            积分扣除记录
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbPointVariationEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认状态
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbConfirmStatusEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认时间
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbConfirmTimeEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            确认人
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbConfirmByEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbRemarkEdit" TextMode="MultiLine" Width="245px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            发货状态
                        </th>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblShipStatusEdit" RepeatDirection="Horizontal">
                                <asp:ListItem Text="未发货/未充值" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="已发货/已充值" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label runat="server" ID="lbMsg" Text="未确认单据无法发货" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            公司或充值平台
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbPlatformEdit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            单号或重置流水号
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txbSerialNumberEdit"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            发货操作人
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lbShipByEdit"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClick="btnSave_Click" />
                            <asp:Button runat="server" ID="btnBackEdit" Text="返回" CssClass="button" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="div_gridview">
                                <div>
                                    操作记录</div>
                                <asp:GridView runat="server" ID="gdvLogEdit" AutoGenerateColumns="False" CssClass="gridview"
                                    SkinID="gridviewSkin">
                                    <Columns>
                                        <asp:BoundField DataField="createTime" HeaderText="时间" />
                                        <asp:BoundField DataField="EmployeeFirstName" HeaderText="操作人" />
                                        <asp:BoundField DataField="remark" HeaderText="操作" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
    <script type="text/javascript">

        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvExchangeList", "gv_OverRow");
        });
        var startDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                onpicked: function (dp) { elem.blur(); },
                skin: 'whyGreen'
            });
        };
        var endDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                onpicked: function (dp) { elem.blur() },
                skin: 'whyGreen'
            });
        }
    </script>
</body>
</html>
