<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bigDataStatistics_1.aspx.cs" Inherits="StatisticalStatement_bigDataStatistics_1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="订单周统计" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" />
        <div>
            <div class="QueryTerms">
                <table cellspacing="5">
                    <tr>
                        <td>年份：
                            <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                <asp:ListItem Value="2014" Selected="True">2014</asp:ListItem>
                                <asp:ListItem Value="2015">2015</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>周期选择：<asp:RadioButton ID="rbWeek" Text="周" GroupName="time" AutoPostBack="true" runat="server" Checked="true" OnCheckedChanged="rbMonth_CheckedChanged" />
                            &nbsp; &nbsp;<asp:RadioButton ID="rbMonth" Text="月" GroupName="time" AutoPostBack="true" runat="server" OnCheckedChanged="rbMonth_CheckedChanged" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="div_gridview" id="div_gridview">
            <asp:GridView ID="gvDigDataStatistics_1" runat="server" DataKeyNames=""
                AutoGenerateColumns="False" SkinID="gridviewSkin">
                <Columns>
                    <asp:BoundField DataField="monthweek" HeaderText="周/月" />
                    <asp:BoundField DataField="orderCount" HeaderText="订单量" />
                    <asp:BoundField DataField="orderAmount" HeaderText="订单额" />
                    <asp:BoundField DataField="payOrderCount" HeaderText="支付量" />
                    <asp:BoundField DataField="payOrderAmount" HeaderText="支付额" />
                    <asp:BoundField DataField="refundCount" HeaderText="退单数" />
                    <asp:BoundField DataField="refundAmount" HeaderText="退单额" />

                    <asp:BoundField DataField="activeUserCount" HeaderText="活跃用户" />
                    <asp:BoundField DataField="addUserCount" HeaderText="新增用户" />
                    <asp:BoundField DataField="totalUserCount" HeaderText="累计用户" />

                    <asp:BoundField DataField="twiceUserCount" HeaderText="复购用户" />
                    <asp:BoundField DataField="twiceOrderCount" HeaderText="复购单数" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gvDigDataStatistics_1", "gv_OverRow");
        });
    </script>
</body>
</html>
