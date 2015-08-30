<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comprehensiveStatistics.aspx.cs"
    Inherits="OtherStatisticalStatement_comprehensiveStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>综合统计</title>
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
                        城市：<asp:DropDownList ID="DropDownList_City" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_City_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">所有城市</asp:ListItem>
                            <asp:ListItem Value="1">北京市</asp:ListItem>
                            <asp:ListItem Value="73">上海市</asp:ListItem>
                            <asp:ListItem Value="197">广州市</asp:ListItem>
                            <asp:ListItem Value="199">深圳市</asp:ListItem>
                            <asp:ListItem Value="87">杭州市</asp:ListItem>
                        </asp:DropDownList>
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
                                AutoPostBack="true" Width="85px" OnTextChanged="TextBox_preOrderTimeStr_TextChanged"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                OnTextChanged="TextBox_preOrderTimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="vertical-align: middle">
                            统计就餐时段：
                        </td>
                        <td>
                            <asp:Button ID="Button_allDay" runat="server" CssClass="tabButtonBlueUnClick" CommandName="allDay"
                                Text="全天" OnClick="Button_allDay_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_afternoon_soon" runat="server" CssClass="tabButtonBlueUnClick"
                                CommandName="afternoon_soon" OnClick="Button_allDay_Click" Text="午市(10时~14时)"
                                Width="120px"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_afternoon" runat="server" CssClass="tabButtonBlueUnClick"
                                CommandName="afternoon" OnClick="Button_allDay_Click" Text="下午(14~17时)" Width="120px">
                            </asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_night_soon" runat="server" CssClass="tabButtonBlueUnClick"
                                CommandName="night_soon" OnClick="Button_allDay_Click" Width="120px" Text="晚市(16时~22时) ">
                            </asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_night" runat="server" CssClass="tabButtonBlueUnClick" CommandName="night"
                                OnClick="Button_allDay_Click" Width="120px" Text="夜市(22时~4时)"></asp:Button>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_hourTimerStr" AutoPostBack="true" onFocus="dateTime(this)"
                                Width="70px" runat="server" OnTextChanged="TextBox_hourTimerStr_TextChanged"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_hourTimerEnd" AutoPostBack="true" onFocus="dateTime(this)"
                                OnTextChanged="TextBox_hourTimerStr_TextChanged" Width="70px" runat="server"></asp:TextBox>
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
                <asp:GridView ID="GridView_OrderStatistics" runat="server" DataKeyNames="orderTime,orderCount,orderSumAmount,orderTransaction,isPaidOrderCount,isPaidOrderAmount,isPaidOrderTransaction,payRate"
                    AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_OrderStatistics_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="orderTime" HeaderText="日期" />
                        <asp:BoundField DataField="orderCount" HeaderText="订单笔数" />
                        <asp:BoundField DataField="orderSumAmount" HeaderText="订单总金额(元)" />
                        <asp:BoundField DataField="orderTransaction" HeaderText="订单客单价" />
                        <asp:BoundField DataField="isPaidOrderCount" HeaderText="支付笔数" />
                        <asp:BoundField DataField="isPaidOrderAmount" HeaderText="支付总金额(元)" />
                        <asp:BoundField DataField="isPaidOrderTransaction" HeaderText="支付客单价(元)" />
                        <asp:BoundField DataField="payRate" HeaderText="支付率" />
                        <asp:BoundField DataField="refundOrderCount" HeaderText="退款笔数" />
                        <asp:BoundField DataField="refundOrderAmount" HeaderText="退款金额" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                详情</HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Detail" CssClass="linkButtonDetail" ForeColor="Blue" runat="server"
                                    CommandName="Order" Text="详情"></asp:LinkButton>
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
                            支付总金额
                        </th>
                        <td>
                            ￥<asp:Label ID="Label_prePaidSumSum" runat="server" Text="0" ForeColor="#F40404"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                        </th>
                        <th>
                            入座共
                        </th>
                        <td>
                            <asp:Label ID="Label_confrimCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                            单
                        </th>
                        <th>
                        </th>
                        <th>
                            入座总金额
                        </th>
                        <td>
                            ￥<asp:Label ID="Label_confrimAmount" runat="server" Text="0" ForeColor="#F40404"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                        </th>
                        <th>
                            退款共
                        </th>
                        <td>
                            <asp:Label ID="Label_refundCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                        <th>
                            单
                        </th>
                        <th>
                        </th>
                        <th>
                            退款总金额
                        </th>
                        <td>
                            ￥<asp:Label ID="Label_refundAmount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_DinnerStarTime" runat="server" />
    <asp:HiddenField ID="HiddenField_DinnerEndTime" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_OrderStatistics", "gv_OverRow");
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
        var dateTime = function (elem) {
            WdatePicker({
                dateFmt: 'HH:mm:ss',
                qsEnabled: false,
                onpicked: function (dp) {
                    elem.blur();
                },
                skin: 'defaultTime'
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
