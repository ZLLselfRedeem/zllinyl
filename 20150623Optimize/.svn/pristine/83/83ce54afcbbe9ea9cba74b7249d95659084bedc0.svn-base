<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceAccountApply.aspx.cs" Inherits="FinanceManage_BalanceAccountApply" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server" width="80px">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>平账申请</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("balanceaccountapply");
            GridViewStyle("GridView_CheckedNeedToPay", "gv_OverRow");
        });
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
            navigationText="" navigationUrl="" headName="平账申请" />
        <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <th>门店搜索：
                        </th>
                        <td>
                            <input id="text" runat="server" type="text" onkeyup="BalanceAccountApplyShopSearch()" />
                            <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000; background-color: White">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>公司名称：
                        </th>
                        <td>
                            <asp:Label ID="lbCompanyName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>平账金额：
                        </th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlBalanceType">
                                <asp:ListItem Text="扣款" Value="1"></asp:ListItem>
                                <asp:ListItem Text="充值" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBox_pay" runat="server"></asp:TextBox>(最大可扣款金额：<asp:Label ID="lb_maxAmount"
                                runat="server" Text="0"></asp:Label>元，冻结金额<asp:Label runat="server" ID="lbAmountFrozen" Text="0"></asp:Label>元)&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="Label2" runat="server" Text="备注:"></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_remark" runat="server" TextMode="MultiLine" Height="60px"
                                Width="236px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnConfirm" runat="server" Text="申请平账" CssClass="couponButtonSubmit"
                                Width="130px" Height="33px" OnClick="btnConfirm_Click" />
                        </td>
                    </tr>
                </table>
                <div>
                    <hr style="height: 1px; border: none; border-top: 2px solid blue;" />
                </div>
                <table>
                    <tr>
                        <th>单据状态</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlStatus">
                                <asp:ListItem Text="==请选择==" Value="0"></asp:ListItem>
                                <asp:ListItem Text="申请提交至出纳" Value="1"></asp:ListItem>
                                <asp:ListItem Text="出纳已确认账目" Value="2"></asp:ListItem>
                                <asp:ListItem Text="主管已平账" Value="3"></asp:ListItem>
                                <asp:ListItem Text="申请被撤回" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Height="30px" Text="查询" CssClass="couponButtonSubmit" OnClick="btnQuery_Click" /></td>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Height="30px" Text="导出excel" OnClick="btnExport_Click" CssClass="couponButtonSubmit" /></td>
                    </tr>
                </table>
            </div>
             <div>
                <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                    OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                    OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                    OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
            </div>
            <asp:Panel ID="Panel_CheckedNeedToPay" runat="server" CssClass="div_gridview">
                <asp:GridView ID="GridView_CheckedNeedToPay" runat="server" DataKeyNames="accountId,operTime,accountMoney,remark"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" OnSelectedIndexChanged="GridView_CheckedNeedToPay_SelectedIndexChanged" OnDataBound="GridView_CheckedNeedToPay_DataBound">
                    <Columns>
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
                                <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                    runat="server" CausesValidation="False" CommandName="Select" Text="修改备注"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                                <asp:Button ID="Button_confirm" runat="server" Text="确    定" CssClass="button" ValidationGroup="ValidationGroup1" OnClick="Button_confirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_cancel" runat="server" Text="取    消" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Role')" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
