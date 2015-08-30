<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mealOrderConfirm.aspx.cs" Inherits="PreOrder19dianManage_mealOrderConfirm" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单报表</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("grOrderList", "gv_OverRow");
            TabManage();
        });
        var orderStartTime = function (elem) { WdatePicker({ el: elem, isShowClear: false, maxDate: '%y-%M-%d', startDate: '2015-01-01' }) };
        var orderEndTime = function (elem) { WdatePicker({ el: elem, isShowClear: false, maxDate: '%y-%M-{%d+1}', startDate: '2015-01-01' }); }
        var psyStartDate = function (elem) { WdatePicker({ el: elem, isShowClear: false, maxDate: '%y-%M-%d', startDate: '2015-01-01' }); };
        var payEndTime = function (elem) { WdatePicker({ el: elem, isShowClear: false, maxDate: '%y-%M-{%d+1}', startDate: '2015-01-01' }); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl=""
            headName="订单报表" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>订单报表</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>订单状态：<asp:RadioButton ID="rbNotPayment" GroupName="rb" Text="未付款" runat="server" />&nbsp;
                                    <asp:RadioButton ID="rbNotConfrim" GroupName="rb" Text="待确定" Checked="true" runat="server" />&nbsp;
                                    <asp:RadioButton ID="rbYesConfrim" GroupName="rb" Text="已确认" runat="server" />&nbsp;
                                    <asp:RadioButton ID="rbYesRefund" GroupName="rb" Text="已退款" runat="server" />&nbsp;
                                    <asp:RadioButton ID="rbOvertimeNotPayment" GroupName="rb" Text="超时未付款" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table>
                            <tr>
                                <td>城市：<asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>订单日期：<asp:TextBox ID="txtOrderStartTime" runat="server" onFocus="orderStartTime(this)" CssClass="Wdate" Width="85px"></asp:TextBox>至<asp:TextBox ID="txtOrderEndTime" onFocus="orderEndTime(this)" runat="server" CssClass="Wdate" Width="85px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>支付日期：<asp:TextBox ID="txtPayStartTime" runat="server" onFocus="psyStartDate(this)" CssClass="Wdate" Width="85px"></asp:TextBox>至<asp:TextBox ID="txtPayEndTime" onFocus="payEndTime(this)" runat="server" CssClass="Wdate" Width="85px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table>
                            <tr>
                                <td>商户名称：<asp:TextBox ID="txtShopName" runat="server"></asp:TextBox>&nbsp;&nbsp;</td>
                                <td>服务经理：<asp:TextBox ID="txtServieManager" runat="server"></asp:TextBox>&nbsp;&nbsp;</td>
                                <td>手机号码：<asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnQuery" runat="server" CssClass="button" Text="查 询" OnClick="btnQuery_Click"></asp:Button></td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="panelGr" runat="server" CssClass="div_gridview">
                        <asp:GridView ID="grOrderList" runat="server" DataKeyNames="" AutoGenerateColumns="False" SkinID="gridviewSkin">
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="shopName" HeaderText="商户名称" />
                                <asp:BoundField DataField="customerName" HeaderText="用户名称" />
                                <asp:BoundField DataField="customerPhone" HeaderText="手机号码" />
                                <asp:BoundField DataField="orderTime" HeaderText="预订日期" />
                                <asp:BoundField DataField="orderTimeFrame" HeaderText="预订时段" />
                                <asp:BoundField DataField="mealName" HeaderText="预订套餐" />
                                <asp:BoundField DataField="orderPayTime" HeaderText="支付时间" />
                                <asp:BoundField DataField="orderStatusDesc" HeaderText="订单状态" />
                                <asp:BoundField DataField="employeeName" HeaderText="服务经理" />
                            </Columns>
                        </asp:GridView>
                        <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                            <div class="gridviewBottom_left">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                                    SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                    MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
