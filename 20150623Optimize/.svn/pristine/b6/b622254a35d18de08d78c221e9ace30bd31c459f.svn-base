<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchMoneyApplyDS.aspx.cs"
    Inherits="FinanceManage_batchMoneyApplyDS" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量打款申请</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("batchmoneyapply");
            GridViewStyle("GridView1", "gv_OverRow");
            GridViewStyle("gdList", "gv_OverRow");
            TabManage();
        });

        function oncbl(Control) {
            var input = document.getElementsByTagName("input");
            for (i = 0; i < input.length; i++) {
                if (input[i].type == "checkbox" && !input[i].disabled) {
                    input[i].checked = Control.checked;
                }
            }
            sumAmount();
        }
        function sumAmount() {
            var tb = document.getElementById("gdList");
            var sum = 0;
            var select = 0;
            for (var i = 1; i < tb.rows.length; i++) {
                var name = "gdList_gdCheck_" + (i - 1);
                var checkboxT = document.getElementById(name);
                if (checkboxT.checked) {
                    sum += parseFloat(tb.rows[i].cells[11].innerHTML);
                    select++;
                }
            }

            document.getElementById("lbCount").innerHTML = "注：共勾选" + select + "笔记录，申请结账金额为：" + sum.toFixed(2);
        }
    </script>
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="批量打款申请" navigationUrl="" headName="批量打款申请" />
            <div class="content">
                    <div style="width: 80%">
                        <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th>门店搜索：</th>
                                <td>
                                     <input id="text" runat="server" type="text" onkeyup="shopAndCompanySearch()" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                    background-color: White">
                                </div>
                                </td>
                                <th>
                                    公司名称：
                                </th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_companyName" runat="server"></asp:TextBox>
                                </td>
                                <td rowspan="2">
                                <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btn_create" runat="server" Text="生成打款申请"
                                     OnClick="btn_create_Click" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    商户余额：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_remainMoney" style= "TEXT-ALIGN: right"  ReadOnly="true" runat="server"></asp:TextBox>元
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                         <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th style="width:16%">城市：</th>
                                <td style="width:23%">
                                    <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                </td>
                                <th>
                                    金额大于等于
                                </th>
                                <td>
                                    <asp:TextBox style= "TEXT-ALIGN: right" ID="txt_remainMoney_city" runat="server" Width="99px"></asp:TextBox>元
                                </td>
                                <td>
                                    <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btn_create_city" runat="server" Text="生成打款申请" OnClick="btn_create_city_Click" />
                                </td>
                            </tr>
                        </table>
                         <hr size="1" style="border: 1px #cccccc dashed;" />
                         <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th style="width:16%">城市：</th>
                                <td style="width:23%">
                                    <asp:DropDownList ID="ddlCityWithdrawType" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width:39%">
                                根据商户提款方式设置进行生成
                                </td>
                                <td>
                                    <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnWithdrawType" runat="server" Text="批量生成打款申请" OnClick="btnWithdrawType_Click"/>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="4">
                                    <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="批量提交打款申请" runat="server" ID="btnAllSubmit" OnClick="btnAllSubmit_Click" />&nbsp;&nbsp;
                                    <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="批量删除" runat="server" ID="btnAllCancel" OnClick="btnAllCancel_Click" />
                                </td>
                            </tr>
                        </table>
                         <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table class="table" runat="server" id="gdTable" width="118%">
                            <tr>
                                <th style="width:8%">
                                    状态：
                                    </th>
                                <td><asp:DropDownList runat="server" id="ddlStatus">
                                        <asp:ListItem Value="0">全部</asp:ListItem>
                                        <asp:ListItem Value="5">申请未提交</asp:ListItem>
                                        <asp:ListItem Value="6">申请被撤回</asp:ListItem>
                                        <asp:ListItem Value="7">申请提交至出纳</asp:ListItem>
                                        <asp:ListItem Value="8">出纳已确认帐目</asp:ListItem>
                                        <asp:ListItem Value="9">主管提交至银行</asp:ListItem>
                                        <asp:ListItem Value="10">银行已受理</asp:ListItem>
                                        <asp:ListItem Value="12">银行打款成功</asp:ListItem>
                                        <asp:ListItem Value="13">银行打款失败</asp:ListItem>
                                     </asp:DropDownList>
                                    </td>
                                <th style="width:8%">
                                    城市:
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlCitySearch" runat="server"></asp:DropDownList>
                                </td>
                                <th style="width:10%">
                                    打款标识:
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlIsFirst" runat="server">
                                        <asp:ListItem Value="2">全部</asp:ListItem>
                                        <asp:ListItem Value="1">首次打款</asp:ListItem>
                                        <asp:ListItem Value="0">非首次打款</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th style="width:8%">
                                    门店：
                                </th>
                                <td>
                                    <asp:TextBox ID="text_ShopName" runat="server"></asp:TextBox>
                                </td>
                                 <td style="width:10%">
                                   <asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="查 询" ID="btnSearch" OnClick="btnSearch_Click" />&nbsp;<asp:Button runat="server" Width="130px" Height="33px" CssClass="couponButtonSubmit" Text="导出到Excel" ID="btnExcel" OnClick="btnExcel_Click" />
                                </td>
                            </tr>
                            </table>
                        <table>
                            <tr>
                                <td  colspan="3" align="left">
                                    <input id="allCheck" runat="server" type="checkbox" onclick="oncbl(this)" />全选
                                    <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                        <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                        <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                     <div class="div_gridview" id="div_gridview">
                                          <asp:GridView ID="gdList" runat="server" Width="1650px" DataKeyNames="batchMoneyApplyId,batchMoneyApplyDetailId,batchMoneyApplyDetailCode,shopName,companyName,
                                              bankName,PayeeBankName,accountName,accountNum,isFirst,createdTime,financePlayMoneyTime,applyAmount,serialNumberOrRemark,status,shopID,companyId,viewallocCommissionValue"
                AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_OrderStatistics_RowCommand" OnRowDataBound="gdList_RowDataBound" OnSorting="gdList_Sorting">
                <Columns>
                     <asp:TemplateField HeaderText="选择">
                                                <ItemTemplate>
                                                     <input type="checkbox" id="gdCheck" onclick="sumAmount()" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                    <asp:BoundField DataField="batchMoneyApplyDetailCode" HeaderText="单号" SortExpression="batchMoneyApplyDetailCode" />
                    <asp:BoundField DataField="shopName" HeaderText="门店名" SortExpression="shopName" />
                    <asp:BoundField DataField="companyName" HeaderText="公司名" SortExpression="companyName" />
                    <asp:BoundField DataField="bankName" HeaderText="开户银行" SortExpression="bankName" />
                    <asp:BoundField DataField="PayeeBankName" HeaderText="支行名称" SortExpression="PayeeBankName" />
                    <asp:BoundField DataField="accountName" HeaderText="开户名" HeaderStyle-Width="5%" SortExpression="accountName" />
                    <asp:BoundField DataField="accountNum" HeaderText="账号" SortExpression="accountNum" />
                    <asp:BoundField DataField="isFirst" HeaderText="打款标识" HeaderStyle-Width="6%" SortExpression="isFirst" />
                    <asp:BoundField DataField="createdTime" HeaderText="申请打款时间" SortExpression="createdTime" />
                    <asp:BoundField DataField="financePlayMoneyTime" HeaderText="财务打款时间" SortExpression="financePlayMoneyTime" />
                    <asp:BoundField DataField="applyAmount" HeaderText="申请结款金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="applyAmount" />
                    <asp:BoundField DataField="viewallocCommissionValue" HeaderText="当前佣金比例" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}" SortExpression="viewallocCommissionValue" />
                    <asp:BoundField DataField="serialNumberOrRemark" HeaderText="流水号或备注" HeaderStyle-Width="5%" SortExpression="serialNumberOrRemark" />
                    <asp:BoundField DataField="status" HeaderText="状态" HeaderStyle-Width="7%" SortExpression="status" />
                     <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CommandName="update" ID="lbtnUpdate">修改</asp:LinkButton>&nbsp;<asp:LinkButton runat="server" CommandName="del" ID="lbtnCancel">删除</asp:LinkButton>
                                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:TemplateField>
                </Columns>
            </asp:GridView>
                                          <label id="lbCount" runat="server" ></label>
                                         </div>
                                   
                                     <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                            <div class="gridviewBottom_left">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                    MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </div>
                                </td>
                            </tr>
                        </table>
                        <!--生成结果<br />
                        批量打款<asp:LinkButton ID="recordId" runat="server" Text="0"></asp:LinkButton>生成成功，包含<asp:Label
                            ID="totleCount" runat="server" Text="0"></asp:Label>笔共计<asp:Label ID="totleAmount"
                                runat="server" Text="0"></asp:Label>元-->
                </div>
    </form>
</body>
</html>
