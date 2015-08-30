<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAmountStatistics.aspx.cs"
    Inherits="OtherStatisticalStatement_UserAmountStatistics" %>

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
                </tr>
            </table>
            <asp:Panel ID="Panel_UserAmountStatistics_Table" runat="server">
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
                            统计时段：
                        </td>
                        <td>
                            <asp:Button ID="Button_allDay" runat="server" CssClass="tabButtonBlueUnClick" CommandName="allDay"
                                Text="全天" OnClick="Button_allDay_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_afternoon_soon" runat="server" CssClass="tabButtonBlueUnClick"
                                OnClick="Button_allDay_Click" CommandName="afternoon_soon" Text="午市(10时~14时)"
                                Width="120px"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_afternoon" runat="server" CssClass="tabButtonBlueUnClick"
                                OnClick="Button_allDay_Click" CommandName="afternoon" Text="下午(14~17时)" Width="120px">
                            </asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_night_soon" runat="server" CssClass="tabButtonBlueUnClick"
                                OnClick="Button_allDay_Click" CommandName="night_soon" Width="120px" Text="晚市(16时~22时) ">
                            </asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_night" runat="server" CssClass="tabButtonBlueUnClick" CommandName="night"
                                OnClick="Button_allDay_Click" Width="120px" Text="夜市(22时~0时)"></asp:Button>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_hourTimerStr" AutoPostBack="true" onFocus="dateTime(this)"
                                runat="server" Width="70px" OnTextChanged="TextBox_hourTimerStr_TextChanged"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_hourTimerEnd" AutoPostBack="true" onFocus="dateTime(this)"
                                OnTextChanged="TextBox_hourTimerStr_TextChanged" Width="70px" runat="server"></asp:TextBox>
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
    <asp:HiddenField ID="HiddenField_StarTime" runat="server" />
    <asp:HiddenField ID="HiddenField_EndTime" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_DayUsersStatistics", "gv_OverRow");
            GridViewStyle("GridView_UserAmountStatisticsDetail", "gv_OverRow");
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
