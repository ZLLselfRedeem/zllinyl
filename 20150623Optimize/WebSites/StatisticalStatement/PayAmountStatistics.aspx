<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayAmountStatistics.aspx.cs"
    Inherits="StatisticalStatement_PayAmountStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>支付量统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_PayStatistics", "gv_OverRow");
            GridViewStyle("GridView_PayDetailGridView", "gv_OverRow");
        });
    </script>
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
                        <td>公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        <td>门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        <td>&nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button_Pay" CssClass="couponButtonSubmit"
                            Width="130px" runat="server" Text="支付量统计" OnClick="Button_Pay_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_PayDetail" CssClass="tabButtonBlueUnClick" runat="server"
                                Text="支付量时段明细" Width="130px" OnClick="Button_PayDetail_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel_PayStatistics_Table" runat="server">
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table>
                        <tr>
                            <td style="vertical-align: middle">统计周期：
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
                                <asp:Button ID="Button_query" runat="server" CssClass="button" CommandName="self"
                                    Text="查  询" OnClick="Button_query_Click"></asp:Button>
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
            <asp:Panel ID="Panel_PayStatistics" runat="server">
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="GridView_PayStatistics" runat="server" DataKeyNames="payOrderTime,orderNumber,payOrderNumber,payCompleteProportion,
                payOrderNumberFloat,payOrderAmount,payOrderAmountIncrement,payPerCustomerTransaction,payPerCustomerTransactionDiurnalVariation"
                        AllowSorting="True" OnSorting="GridView_Sorting" AutoGenerateColumns="False"
                        SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="payOrderTime" HeaderText="订单支付日期" SortExpression="payOrderTime" />
                            <asp:BoundField DataField="orderNumber" HeaderText="订单笔数" SortExpression="orderNumber" />
                            <asp:BoundField DataField="payOrderNumber" HeaderText="支付笔数" SortExpression="payOrderNumber" />
                            <asp:BoundField DataField="payCompleteProportion" HeaderText="支付完成比例" SortExpression="payCompleteProportion" />
                            <asp:BoundField DataField="payOrderNumberFloat" HeaderText="支付笔数浮动" SortExpression="payOrderNumberFloat" />
                            <asp:BoundField DataField="payOrderAmount" HeaderText="支付金额" SortExpression="payOrderAmount" />
                            <asp:BoundField DataField="payOrderAmountIncrement" HeaderText="支付金额增量" SortExpression="payOrderAmountIncrement" />
                            <asp:BoundField DataField="payPerCustomerTransaction" HeaderText="支付客单价" SortExpression="payPerCustomerTransaction" />
                            <asp:BoundField DataField="payPerCustomerTransactionDiurnalVariation" HeaderText="支付客单价日变化"
                                SortExpression="payPerCustomerTransactionDiurnalVariation" />
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
                            <th>共
                            </th>
                            <td>
                                <asp:Label ID="Label_OrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                            </td>
                            <th>单
                            </th>
                            <th>支付共
                            </th>
                            <td>
                                <asp:Label ID="Label_payOrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                            </td>
                            <th>单
                            </th>
                            <th></th>
                            <th>支付总金额
                            </th>
                            <td>￥<asp:Label ID="Label_prePaidSumSum" runat="server" Text="0" ForeColor="#F40404"
                                Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel_PayDetailGridview" runat="server">
            <div class="div_gridview" id="div_othergridview">
                <asp:GridView ID="GridView_PayDetailGridView" runat="server" DataKeyNames="hourTime,cityHangzhouOrdersCount,cityBeijingOrdersCount
            ,cityShanghaiOrdersCount,nationwideOrdersCount"
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
