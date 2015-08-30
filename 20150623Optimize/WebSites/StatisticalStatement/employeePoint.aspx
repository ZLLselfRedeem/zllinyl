<%@ Page Language="C#" AutoEventWireup="true" CodeFile="employeePoint.aspx.cs" Inherits="StatisticalStatement_employeePoint" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单店单日订单列表</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="QueryTerms">
            <asp:Panel ID="Panel_shopOrderList_Table" runat="server">
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
                                ontextchanged="TextBox_preOrderTimeStr_TextChanged" OnClick="Button_day_Click"
                                Text="自定义"></asp:Button>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_preOrderTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                AutoPostBack="true" Width="85px" OnTextChanged="TextBox_preOrderTimeStr_TextChanged"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                AutoPostBack="true" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel_shopOrderList" runat="server">
            <div class="div_gridview" id="div_gridview">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="vertical-align: top; vertical-align: text-top">
                            <asp:GridView ID="gv_activeEmployee" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                                SkinID="gridviewSkin">
                                <Columns>
                                    <asp:BoundField DataField="statisticsTime" HeaderText="时间" />
                                    <asp:BoundField DataField="employeeCount" HeaderText="悠先服务活跃用户" />
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
                        </td>
                        <td style="vertical-align: top; vertical-align: text-top">
                            <asp:GridView ID="gv_point" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                                SkinID="gridviewSkin">
                                <Columns>
                                    <asp:BoundField DataField="statisticsTime" HeaderText="时间" />
                                    <asp:BoundField DataField="pointConsumptionCount" HeaderText="积分消耗人数" />
                                    <asp:BoundField DataField="proportion" HeaderText="积分消耗比例" />
                                    <asp:BoundField DataField="pointConsumptionAmount" HeaderText="积分消耗总额" />
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="Panel2" CssClass="gridviewBottom" runat="server">
                                <div class="asp_page">
                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                        TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                        OnPageChanged="AspNetPager2_PageChanged">
                                    </webdiyer:AspNetPager>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gv_activeEmployee", "gv_OverRow");
            GridViewStyle("gv_point", "gv_OverRow");
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
