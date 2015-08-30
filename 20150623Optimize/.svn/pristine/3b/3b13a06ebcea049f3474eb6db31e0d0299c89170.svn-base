<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberStatistics.aspx.cs"
    Inherits="OtherStatisticalStatement_MemberStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员统计</title>
    <link href="../Css/css.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="QueryTerms">
        <table cellspacing="5">
            <tr>
                <td>
                    省份：<asp:DropDownList ID="DropDownList_Province" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="DropDownList_Province_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    城市：<asp:DropDownList ID="DropDownList_City" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_City_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button_Member" CssClass="couponButtonSubmit"
                        Width="130px" runat="server" Text="会员统计" OnClick="Button_Member_Click" />
                </td>
                <td>
                    <asp:Button ID="Button_CustomerDetail" CssClass="tabButtonBlueUnClick" runat="server"
                        Text="会员列表明细" Width="130px" OnClick="Button_CustomerDetail_Click" />
                </td>
            </tr>
        </table>
        <hr size="1" style="border: 1px #cccccc dashed;" />
        <table>
            <tr>
                <td style="vertical-align: middle">
                    会员注册时间：
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
                    <asp:TextBox ID="TextBox_registerTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                        OnTextChanged="TextBox_registerTimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                    &nbsp;-&nbsp;
                    <asp:TextBox ID="TextBox_registerTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                        OnTextChanged="TextBox_registerTimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: right">
            <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
            <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
            <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
            <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
        </div>
    </div>
    <div>
        <asp:Panel ID="Panel_CustomerDetail" runat="server">
            <div class="div_gridview" id="div_gridview">
                <asp:GridView ID="GridView_CustomerDetail" runat="server" DataKeyNames="eCardNumber,RegisterDate,cityName,appType"
                    AutoGenerateColumns="False" SkinID="gridviewSkin">
                    <Columns>
                        <asp:BoundField DataField="eCardNumber" HeaderText="VIP号码" />
                        <asp:BoundField DataField="RegisterDate" HeaderText="注册时间" />
                        <asp:BoundField DataField="cityName" HeaderText="所在城市" />
                        <asp:BoundField DataField="appType" HeaderText="设备类型" />
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
        <asp:Panel ID="Panel_MemberStatisticGridview" runat="server">
            <div class="div_gridview" id="div_othergridview">
                <asp:GridView ID="GridView_MemberStatistic" runat="server" DataKeyNames="memberAmount,cityName,appType"
                    AutoGenerateColumns="False" SkinID="gridviewSkin">
                    <Columns>
                        <asp:BoundField DataField="memberAmount" HeaderText="会员数量" />
                        <asp:BoundField DataField="cityName" HeaderText="所在城市" />
                        <asp:BoundField DataField="appType" HeaderText="设备类型" />
                    </Columns>
                </asp:GridView>
                <div>
                    <asp:Label ID="Label_Total" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <asp:Label ID="Label_errorMessage" runat="server" CssClass="Red" Text=""></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        TabManage();
        GridViewStyle("GridView_CustomerDetail", "gv_OverRow");
        GridViewStyle("GridView_MemberStatistic", "gv_OverRow");
    });
    var startDate = function (elem) {
        WdatePicker({
            el: elem,
            isShowClear: false,
            maxDate: '#F{$dp.$D(\'TextBox_registerTimeEnd\')||%y-%M-%d}',
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
            minDate: '#F{$dp.$D(\'TextBox_registerTimeStr\')}',
            onpicked: function (dp) { elem.blur() },
            skin: 'whyGreen'
        });
    }
</script>
</html>
