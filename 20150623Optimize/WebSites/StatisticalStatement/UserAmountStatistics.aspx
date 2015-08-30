<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAmountStatistics.aspx.cs"
    Inherits="StatisticalStatement_UserAmountStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户量统计</title>
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
                            <asp:Button ID="Button_Users" CssClass="couponButtonSubmit" Width="130px" runat="server"
                                Text="用户量统计" OnClick="Button_Users_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_UsersDetail" CssClass="tabButtonBlueUnClick" runat="server"
                                Text="用户量时段明细" Width="130px" OnClick="Button_UsersDetail_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_ActityUser" CssClass="tabButtonBlueUnClick" runat="server"
                                Text="活跃用户统计" Width="130px" OnClick="Button_ActityUser_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel_UserAmountStatistics_Table" runat="server">
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
                                <asp:Button ID="Button_query" runat="server" CssClass="button" Text="查  询"
                                    OnClick="Button_query_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <asp:Panel ID="Panel_DayUsersStatistics" runat="server">
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="GridView_DayUsersStatistics" runat="server" DataKeyNames="registerDate,cityHangzhouUsersAddAmount,cityBeijingUsersAddAmount,cityShanghaiUsersAddAmount,usersAddAmount,dayAddAmout"
                        AutoGenerateColumns="False" SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="registerDate" HeaderText="日期" />
                            <asp:BoundField DataField="cityHangzhouUsersAddAmount" HeaderText="杭州新增" />
                            <asp:BoundField DataField="cityShanghaiUsersAddAmount" HeaderText="上海新增" />
                            <asp:BoundField DataField="cityBeijingUsersAddAmount" HeaderText="北京新增" />
                            <asp:BoundField DataField="dayAddAmout" HeaderText="日新增量" />
                            <asp:BoundField DataField="usersAddAmount" HeaderText="总量" />
                            <asp:BoundField DataField="userCount" HeaderText="累计用户" />
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
                </div>
                <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel_UserAmountStatisticsDetail_Gridview" runat="server">
            <div class="div_gridview" id="div_othergridview">
                <asp:GridView ID="GridView_UserAmountStatisticsDetail" runat="server" DataKeyNames="registerHourTime,cityHangzhouUsersCount,cityBeijingUsersCount,cityShanghaiUsersCount,nationwideUsersCount"
                    AutoGenerateColumns="False" SkinID="gridviewSkin">
                    <Columns>
                        <asp:BoundField DataField="registerHourTime" HeaderText="时间" />
                        <asp:BoundField DataField="cityHangzhouUsersCount" HeaderText="杭州新增" />
                        <asp:BoundField DataField="cityShanghaiUsersCount" HeaderText="上海新增" />
                        <asp:BoundField DataField="cityBeijingUsersCount" HeaderText="北京新增" />
                        <asp:BoundField DataField="nationwideUsersCount" HeaderText="全国新增合计" />
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="Label_errorMessage" runat="server" CssClass="Red" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panel_ActityUser" runat="server">
            <div class="div_gridview" id="div1">
                <asp:GridView ID="GridView_ActityUser" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                    SkinID="gridviewSkin">
                    <Columns>
                        <asp:BoundField DataField="loginTime" HeaderText="日期" />
                        <asp:BoundField DataField="dayActiveUserCount" HeaderText="当天活跃用户登录次数" />
                        <asp:BoundField DataField="dayActiveCustomerCount" HeaderText="当天活跃用户数量" />
                    </Columns>
                </asp:GridView>
                <div class="asp_page">
                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页"
                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                        OnPageChanged="AspNetPager2_PageChanged">
                    </webdiyer:AspNetPager>
                </div>
                <br />
                当前时段累计活跃用户数量 
                <asp:Label ID="lb_totalCount" runat="server" CssClass="Red" Text=""></asp:Label><br />
            </div>
            <asp:Label ID="Label1" runat="server" CssClass="Red" Text=""></asp:Label>
        </asp:Panel>
        <asp:HiddenField ID="HiddenField_StarTime" runat="server" />
        <asp:HiddenField ID="HiddenField_EndTime" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_DayUsersStatistics", "gv_OverRow");
            GridViewStyle("GridView_UserAmountStatisticsDetail", "gv_OverRow");
            GridViewStyle("GridView_ActityUser", "gv_OverRow");
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
