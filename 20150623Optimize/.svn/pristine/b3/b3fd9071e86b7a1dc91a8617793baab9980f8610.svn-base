<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderStatistics.aspx.cs"
    Inherits="StatisticalStatement_OrderStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单量统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="QueryTerms">
            <table cellspacing="5">
                <tr>
                     <td>城市：<asp:DropDownList ID="DropDownList_City" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_City_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                    <td>
                        公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp; 订单类型：
                        <asp:RadioButton ID="RadioButton_AllOrder" GroupName="groupName" AutoPostBack="true"
                            Checked="true" Text="所有订单" runat="server" OnCheckedChanged="RadioButton_AllOrder_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_Verified" GroupName="groupName" AutoPostBack="true"
                            Text="已入座订单" OnCheckedChanged="RadioButton_AllOrder_CheckedChanged" runat="server" />
                    </td>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button_Change" CssClass="couponButtonSubmit"
                            Width="130px" runat="server" Text="订单量统计" OnClick="Button_Change_Click" />
                    </td>
                    <td>
                        <asp:Button ID="Button_ChangeDetail" CssClass="tabButtonBlueUnClick" runat="server"
                            Text="订单量时段明细" OnClick="Button_ChangeDetail_Click" Width="130px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel_OrderStatistics_Table" runat="server">
                <hr size="1" style="border: 1px #cccccc dashed;" />
                <table>
                    <tr>
                        <td style="vertical-align: middle">
                            统计周期：
                        </td>
                        <td>
                            <asp:Button ID="Button_1day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="1"
                                Text="今天" OnClick="Button_day_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_yesterday" runat="server" CssClass="tabButtonBlueUnClick"
                                OnClick="Button_day_Click" CommandName="yesterday" Text="昨天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_7day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="7"
                                OnClick="Button_day_Click" Text="最近7天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_14day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="14"
                                OnClick="Button_day_Click" Text="最近14天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_30day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="30"
                                OnClick="Button_day_Click" Text="最近30天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_self" runat="server" CssClass="tabButtonBlueUnClick" CommandName="self"
                                OnClick="Button_day_Click" Text="自定义"></asp:Button>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_preOrderTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                Width="85px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button_Query" runat="server" CssClass="button" CommandName="self"
                                Text="查  询" OnClick="Button_Query_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <div style="text-align: left">
                    <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                        OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                    <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                        OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                    <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                        OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel_OrderStatistics" runat="server">
            <div class="div_gridview" id="div_gridview">
                <asp:GridView ID="GridView_OrderStatistics" runat="server" DataKeyNames="orderTime,orderNumber,orderNumberIncrement,orderAmount,orderAmountIncrement,payOrderCount,payOrderAmount,payOrderAmountIncrement,perCustomerTransaction,perCustomerTransactionDiurnalVariation"
                    AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin" OnSorting="GridView_Sorting">
                    <Columns>
                        <asp:BoundField DataField="orderTime" HeaderText="订单日期" SortExpression="orderTime" />
                        <asp:BoundField DataField="orderNumber" HeaderText="订单笔数" SortExpression="orderNumber" />
                        <asp:BoundField DataField="orderNumberIncrement" HeaderText="订单笔数增量" SortExpression="orderNumberIncrement" />
                        <asp:BoundField DataField="orderAmount" HeaderText="订单金额" SortExpression="orderAmount" />
                        <asp:BoundField DataField="orderAmountIncrement" HeaderText="订单金额增量" SortExpression="orderAmountIncrement" />
                        <asp:BoundField DataField="payOrderCount" HeaderText="支付单数" SortExpression="payOrderCount" />
                        <asp:BoundField DataField="payOrderAmount" HeaderText="支付金额" SortExpression="payOrderAmount" />
                        <asp:BoundField DataField="payOrderAmountIncrement" HeaderText="支付金额增量" SortExpression="payOrderAmountIncrement" />
                        <asp:BoundField DataField="perCustomerTransaction" HeaderText="客单价" SortExpression="perCustomerTransaction" />
                        <asp:BoundField DataField="perCustomerTransactionDiurnalVariation" HeaderText="客单价日变化"
                            SortExpression="perCustomerTransactionDiurnalVariation" />
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                </asp:Panel>
                <table>
                    <tr>
                        <th>
                            共
                        </th>
                        <td>
                            <asp:Label ID="Label_OrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                            单
                        </th>
                        <th>
                        </th>
                        <th>
                            总金额
                        </th>
                        <td>
                            ￥<asp:Label ID="Label_preOrderServerSumSum" runat="server" Text="0" ForeColor="#F40404"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                        </th>
                        <th>
                            支付共
                        </th>
                        <td>
                            <asp:Label ID="Label_payOrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                            单
                        </th>
                        <th>
                        </th>
                        <th>
                            支付金额
                        </th>
                        <td>
                            ￥<asp:Label ID="Label_prePaidSumSum" runat="server" Text="0" ForeColor="#F40404"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
        </asp:Panel>
    </div>
    <asp:Panel ID="Panel_othergridview" runat="server">
        <div class="div_gridview" id="div_othergridview">
            <asp:GridView ID="GridView_othergridview" runat="server" DataKeyNames="hourTime,cityHangzhouOrdersCount,cityBeijingOrdersCount,cityShanghaiOrdersCount,nationwideOrdersCount"
                AutoGenerateColumns="False" SkinID="gridviewSkin">
                <Columns>
                    <asp:BoundField DataField="hourTime" HeaderText="时间" />
                    <asp:BoundField DataField="cityHangzhouOrdersCount" HeaderText="杭州" />
                    <asp:BoundField DataField="cityShanghaiOrdersCount" HeaderText="上海" />
                    <asp:BoundField DataField="cityBeijingOrdersCount" HeaderText="北京" />
                    <asp:BoundField DataField="nationwideOrdersCount" HeaderText="全国合计" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Label ID="Label_errorMessage" runat="server" CssClass="Red" Text=""></asp:Label>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField_DinnerStarTime" runat="server" />
    <asp:HiddenField ID="HiddenField_DinnerEndTime" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_OrderStatistics", "gv_OverRow");
            GridViewStyle("GridView_othergridview", "gv_OverRow");
        });
        var startDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                maxDate: '#F{$dp.$D(\'TextBox_preOrderTimeEnd\')||%y-%M-%d}',
                onpicked: function (dp) {
                    elem.blur();
                },
                skin: 'whyGreen'
            });
        };
        var endDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                maxDate: '%y-%M-{%d+1}',
                minDate: '#F{$dp.$D(\'TextBox_preOrderTimeStr\')}',
                onpicked: function (dp) { elem.blur() },
                skin: 'whyGreen'
            });
        }
    </script>
</body>
</html>
