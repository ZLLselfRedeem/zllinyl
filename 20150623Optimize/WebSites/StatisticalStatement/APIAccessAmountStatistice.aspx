<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APIAccessAmountStatistice.aspx.cs"
    Inherits="StatisticalStatement_APIAccessAmountStatistice" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>API访问次数统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="QueryTerms">
            <asp:Panel ID="Panel_APIAccessAmountStatistics" runat="server">
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
                                CommandName="yesterday" OnClick="Button_day_Click" Text="昨天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_7day" OnClick="Button_day_Click" runat="server" CssClass="tabButtonBlueUnClick"
                                CommandName="7" Text="最近7天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_14day" OnClick="Button_day_Click" runat="server" CssClass="tabButtonBlueUnClick"
                                CommandName="14" Text="最近14天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_30day" OnClick="Button_day_Click" runat="server" CssClass="tabButtonBlueUnClick"
                                CommandName="30" Text="最近30天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_self" runat="server" CssClass="tabButtonBlueUnClick" CommandName="self"
                                OnClick="Button_day_Click" Text="自定义"></asp:Button>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_preOrderTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                Width="85px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="button" CommandName="30" Text="查  询"
                                OnClick="Button1_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Label ID="Label_Message" runat="server" CssClass="Red" Text=""></asp:Label>
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
        <asp:Panel ID="Panel_APIAccessStatistics" runat="server">
            <div class="div_gridview" id="div_gridview">
                <asp:GridView ID="GridView_APIAccessAmountStatistics" runat="server" DataKeyNames="invokedAPITime,invokedAPIAmount_companyList,invokedAPIAmount_preorderList,invokedAPIAmount_userLogin,invokedAPIAmount_userRegister"
                    AutoGenerateColumns="False" SkinID="gridviewSkin">
                    <Columns>
                        <asp:BoundField DataField="invokedAPITime" HeaderText="日期" />
                        <asp:BoundField DataField="invokedAPIAmount_companyList" HeaderText="刷新公司列表" />
                        <asp:BoundField DataField="invokedAPIAmount_userLogin" HeaderText="用户登录" />
                        <asp:BoundField DataField="invokedAPIAmount_userRegister" HeaderText="用户激活" />
                        <asp:BoundField DataField="invokedAPIAmount_preorderList" HeaderText="查询预点单列表" />
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
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_APIAccessAmountStatistics", "gv_OverRow");
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
