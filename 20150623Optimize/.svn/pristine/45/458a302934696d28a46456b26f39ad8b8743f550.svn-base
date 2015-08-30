<%@ Page Language="C#" AutoEventWireup="true" CodeFile="orderStatusDetail.aspx.cs" Inherits="FinancialReporting_orderStatusDetail" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单状态明细</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="订单状态明细" navigationImage="~/images/icon/list.gif" navigationText="订单状态明细" navigationUrl="" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>订单状态明细</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>城市：<asp:DropDownList ID="ddlCity" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList></td>
                                <td>公司：<asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList></td>
                                <td>门店：<asp:DropDownList ID="ddlShop" runat="server"></asp:DropDownList></td>
                                <td rowspan="2">
                                    <asp:Button ID="btnSearch" runat="server" Text="查  询" CssClass="button" OnClick="btnSearch_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnExportExcel" runat="server" Text="导出excel" CssClass="button" OnClick="btnExportExcel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>时间：<asp:TextBox ID="txtTimeStart" runat="server" CssClass="Wdate" onFocus="startDate(this)" Width="85px"></asp:TextBox>
                                </td>
                                <td>-<asp:TextBox ID="txtTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)" Width="85px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="panelContent" runat="server">
                        <div class="div_gridview" id="div_gridview">
                            <asp:GridView ID="gvListContent" runat="server" DataKeyNames="" AutoGenerateColumns="False" SkinID="gridviewSkin">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <%# (this.dataPager.CurrentPageIndex - 1) * this.dataPager.PageSize + Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--点单基本信息--%>
                                    <asp:BoundField DataField="OrderPhone" HeaderText="订单手机" />
                                    <asp:BoundField DataField="OrderNumber" HeaderText="订单号" />
                                    <asp:BoundField DataField="OrderTime" HeaderText="订单时间" />
                                    <asp:BoundField DataField="OrderShop" HeaderText="订单门店" />
                                    <asp:BoundField DataField="OrderAmount" HeaderText="订单金额" />
                                    <asp:BoundField DataField="OrderBalance" HeaderText="订单粮票 " />
                                    <asp:BoundField DataField="OrderWechat" HeaderText="订单微信" />
                                    <asp:BoundField DataField="OrderAli" HeaderText="订单支付宝" />
                                    <asp:BoundField DataField="OrderOther" HeaderText="订单其他" />
                                    <%-- 入座信息--%>
                                    <asp:BoundField DataField="ConfrimStatus" HeaderText="入座状态" />
                                    <asp:BoundField DataField="ConfrimTime" HeaderText="入座时间" />
                                    <asp:BoundField DataField="ConfrimAmount" HeaderText="入座金额" />
                                    <asp:BoundField DataField="ConfrimBalance" HeaderText="入座粮票" />
                                    <asp:BoundField DataField="ConfrimWechat" HeaderText="入座微信" />
                                    <asp:BoundField DataField="ConfrimAli" HeaderText="入座支付宝" />
                                    <asp:BoundField DataField="ConfrimAther" HeaderText="入座其他" />
                                    <%-- 对账信息--%>
                                    <asp:BoundField DataField="ApproveStatus" HeaderText="结算状态" />
                                    <asp:BoundField DataField="ApproveTime" HeaderText="结算日期" />
                                    <asp:BoundField DataField="ApproveAmount" HeaderText="结算金额" />
                                </Columns>
                            </asp:GridView>
                            <div class="asp_page">
                                <webdiyer:AspNetPager ID="dataPager" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                    CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center" OnPageChanged="dataPager_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gvListContent", "gv_OverRow");
        });
        var startDate = function (elem) { WdatePicker({ el: elem, isShowClear: false, onpicked: function (dp) { elem.blur(); }, skin: 'whyGreen' }); };
        var endDate = function (elem) { WdatePicker({ el: elem, isShowClear: false, onpicked: function (dp) { elem.blur() }, skin: 'whyGreen' }); }
    </script>
</body>
</html>
