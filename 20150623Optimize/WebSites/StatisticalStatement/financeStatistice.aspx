<%@ Page Language="C#" AutoEventWireup="true" CodeFile="financeStatistice.aspx.cs" Inherits="StatisticalStatement_financeStatistice" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户余额统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="用户余额统计" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>基本信息</li>
                </ul>
            </div>
            <div class="QueryTerms">
                <asp:Panel ID="panelHead" runat="server">
                    <table>
                        <tr>
                            <td>手机号码：<asp:TextBox ID="txtCustomerPhone" runat="server"></asp:TextBox></td>
                            <td>用户名：<asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="button"
                                    Text="查  询" OnClick="btnSearch_Click"></asp:Button></td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <%--<table>
                        <tr>
                            <td>城市：<asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                            <td>公司：<asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                            <td>门店：<asp:DropDownList ID="ddlShop" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />--%>
                    <table>
                        <tr>
                            <td style="vertical-align: middle">统计周期：
                            </td>
                            <td>
                                <asp:TextBox ID="txtTimeStar" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                    Width="85px"></asp:TextBox>
                                &nbsp;-&nbsp;
                            <asp:TextBox ID="txtTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                Width="85px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" CssClass="button"
                                    Text="查  询" OnClick="btnQuery_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <asp:Panel ID="panelContent" runat="server">
                <p style="font-size: 12pt; font-weight: bold; padding-left: 10px">
                    期初余额：<asp:Label ID="lbStarAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                    期末余额：<asp:Label ID="lbEndAmount" runat="server" ForeColor="Red" Text="0"></asp:Label><br />
                    本期活动赠送金额：<asp:Label ID="lbPresentAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                    本期账户余额支付金额：<asp:Label ID="lbPayAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                    本期账户充值收款金额：<asp:Label ID="lbGatheringAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                    本期账户支付宝收款金额：<asp:Label ID="lbAliAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                    本期账户财富通收款金额：<asp:Label ID="lbWechatAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                    本期退款金额：<asp:Label ID="lbRefundAmount" ForeColor="Red" runat="server" Text="0"></asp:Label><br />
                </p>
            </asp:Panel>
            <asp:Panel ID="panelMain" runat="server">
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="grDataList" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        SkinID="gridviewSkin">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="changeTime" HeaderText="时间" />
                            <asp:BoundField DataField="decription" HeaderText="交易摘要" />
                            <asp:BoundField DataField="changeValue" HeaderText="账户收支金额" />
                            <asp:BoundField DataField="remainMoney" HeaderText="账户余额" />
                        </Columns>
                    </asp:GridView>
                    <asp:Panel ID="panelPaging" CssClass="gridviewBottom" runat="server">
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center" OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("grDataList", "gv_OverRow");
        });
        var startDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                minDate: '2014-09-26',
                startDate: '2014-09-26',
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
                minDate: '2014-09-26',
                startDate: '2014-09-26',
                onpicked: function (dp) { elem.blur() },
                skin: 'whyGreen'
            });
        }
    </script>
</body>
</html>
