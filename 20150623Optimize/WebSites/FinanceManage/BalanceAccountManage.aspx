<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceAccountManage.aspx.cs" Inherits="FinanceManage_BalanceAccountManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" width="80px">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>平账管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("balanceAccountManage");
            GridViewStyle("GridView_CheckedNeedToPay", "gv_OverRow");
        });
        function sumAmount() {
            var tb = document.getElementById("GridView_CheckedNeedToPay");
            var sumkk = 0;
            var sumcz = 0;
            var selectkk = 0;
            var selectcz = 0;
            for (var i = 1; i < tb.rows.length; i++) {
                var name = "GridView_CheckedNeedToPay_ckbSelect_" + (i - 1);
                var checkboxT = document.getElementById(name);
                if (checkboxT.checked) {
                    if (tb.rows[i].cells[6].innerHTML.substring(0, 2) == "扣款") {
                        sumkk += parseFloat(tb.rows[i].cells[6].innerHTML.substring(2));
                        selectkk++;
                    }
                    else
                    {
                        sumcz += parseFloat(tb.rows[i].cells[6].innerHTML.substring(2));
                        selectcz++;
                    }
                }
            }

            document.getElementById("lbCount").innerHTML = "注：共勾选" + selectkk + "笔扣款记录：" + sumkk.toFixed(2) + "元，" + selectcz + "笔充值记录：" + sumcz.toFixed(2) + "元";
        }

        function oncbl(Control) {
            var input = document.getElementsByTagName("input");
            for (i = 0; i < input.length; i++) {
                if (input[i].type == "checkbox" && !input[i].disabled) {
                    input[i].checked = Control.checked;
                }
            }

            sumAmount();
        }
    </script>
    <style type="text/css">
        li:hover {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="平账管理" />
        <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <th>门店搜索：
                        </th>
                        <td>
                            <input id="text" runat="server" type="text" onkeyup="BalanceAccountManageShopSearch()" />
                            <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                            </div>
                        </td>
                        <th>公司名称：
                        </th>
                        <td>
                            <asp:Label ID="lbCompanyName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>单号：
                        </th>
                        <td>
                            <asp:TextBox ID="txbAccountId" runat="server"></asp:TextBox>
                        </td>
                        <th>单据状态：</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlStatus">
                                <asp:ListItem Text="==请选择==" Value="0"></asp:ListItem>
                                <asp:ListItem Text="申请提交至出纳" Value="1"></asp:ListItem>
                                <asp:ListItem Text="出纳已确认账目" Value="2"></asp:ListItem>
                                <asp:ListItem Text="主管已平账" Value="3"></asp:ListItem>
                                <asp:ListItem Text="申请被撤回" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>申请时间：
                        </th>
                        <td>
                            <asp:TextBox ID="txtOperateBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"></asp:TextBox>~~
                                 <asp:TextBox ID="txtOperateEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"></asp:TextBox>
                        </td>
                        <th>
                            城市：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="couponButtonSubmit"
                                Width="130px" Height="33px" OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                </table>
                <div>
                    <hr style="height: 1px; border: none; border-top: 2px solid blue;" />
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnBatchConfirm" runat="server" Height="30px" Text="执行批量平账" CssClass="couponButtonSubmit" OnClick="btnBatchConfirm_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnBatchCheck" runat="server" Height="30px" Text="批量确认" CssClass="couponButtonSubmit" OnClick="btnBatchCheck_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnBatchReject" runat="server" Height="30px" Text="批量撤回" CssClass="couponButtonSubmit" OnClick="btnBatchReject_Click" /></td>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Height="30px" Text="导出excel" CssClass="couponButtonSubmit" OnClick="btnExport_Click" /></td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="divCheckAll">
                <input type="checkbox" id="ckbCheckAll" onclick="oncbl(this)" />
                <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                    OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                    OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                    OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
            </div>
            <asp:Panel ID="Panel_CheckedNeedToPay" runat="server" CssClass="div_gridview">
                <asp:GridView ID="GridView_CheckedNeedToPay" runat="server" DataKeyNames="accountId"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" OnDataBound="GridView_CheckedNeedToPay_DataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="checkbox" id="ckbSelect" onclick="sumAmount(this)" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="accountId" HeaderText="单号" />
                        <asp:BoundField DataField="shopName" HeaderText="门店名" />
                        <asp:BoundField DataField="companyName" HeaderText="公司名" />
                        <asp:BoundField DataField="operTime" HeaderText="申请平账时间" />
                        <asp:BoundField DataField="confirmTime" HeaderText="财务平账时间" />
                        <asp:BoundField DataField="accountMoney" HeaderText="平账金额" />
                        <asp:BoundField DataField="remark" HeaderText="备注" />
                        <asp:BoundField DataField="status" HeaderText="状态" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <img src="../Images/key_edit2.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lkbCheck" OnCommand="lnkbtn_OnCommand"
                                    runat="server" CausesValidation="False" CommandName="check" Text="确认" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"accountId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lkbConfirm" OnCommand="lnkbtn_OnCommand"
                                    runat="server" CausesValidation="False" CommandName="confirm" Text="平账" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"accountId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lkbReject" OnCommand="lnkbtn_OnCommand"
                                    runat="server" CausesValidation="False" CommandName="reject" Text="撤回" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"accountId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                 <label id="lbCount" runat="server" ></label>
                <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                    <div class="gridviewBottom_left">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go"
                            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                            MoreButtonType="Image" NavigationButtonType="Image">
                        </webdiyer:AspNetPager>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="Panel_Role" runat="server" CssClass="panelSyle">
                <table>
                    <tr>
                        <th colspan="2" class="dialogBox_th">修改备注
                        </th>
                    </tr>
                    <tr>
                        <th>备注：
                        </th>
                        <td>
                            <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Width="250px" Height="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_remark"
                                ErrorMessage="备注不能为空" ForeColor="Red" ValidationGroup="ValidationGroup1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_confirm" runat="server" Text="确    定" CssClass="button" ValidationGroup="ValidationGroup1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_cancel" runat="server" Text="取    消" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Role')" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
