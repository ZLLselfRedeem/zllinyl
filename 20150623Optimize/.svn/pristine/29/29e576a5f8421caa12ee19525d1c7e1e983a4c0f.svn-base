<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dataStatistics.aspx.cs" Inherits="StatisticalStatement_dataStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据统计</title>
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
                        <td>城市：<asp:DropDownList ID="DropDownList_City" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_City_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        <td>公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        <td>门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel_OrderStatistics_Table" runat="server">
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table>
                        <tr>
                            <td style="vertical-align: middle">统计周期(下单时间)：
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
                                <asp:Button ID="Button1" runat="server" CssClass="button" Text="查  询" OnClick="Button1_Click"></asp:Button>
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
                <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
            </asp:Panel>
        </div>
        <div class="div_gridview" id="div_gridview">
            <asp:GridView ID="GridView_OrderStatistics" runat="server" DataKeyNames="orderTime,orderCount,orderSumAmount,isPaidOrderCount,payRate,isPaidOrderAmount,refundOrderCount,refundOrderAmount"
                AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_OrderStatistics_RowCommand"
                OnSorting="GridView_Sorting">
                <Columns>
                    <asp:BoundField DataField="orderTime" HeaderText="日期" SortExpression="orderTime" />
                    <asp:BoundField DataField="orderCount" HeaderText="订单量" SortExpression="orderCount" />
                    <asp:BoundField DataField="orderSumAmount" HeaderText="订单额" SortExpression="orderSumAmount" />
                    <%-- <asp:BoundField DataField="timesOrderQuantity" HeaderText=">1次订单量" SortExpression="timesOrderQuantity" />
                    <asp:TemplateField SortExpression="timersOrderRate" HeaderText=">1次订单比">
                        <ItemTemplate>
                            <asp:Label ID="Label_timersOrderRate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="isPaidOrderCount" HeaderText="支付量" SortExpression="isPaidOrderCount" />
                    <%-- <asp:BoundField DataField="timersPayCount" HeaderText=">1次支付量" SortExpression="timersPayCount" />
                    <asp:TemplateField SortExpression="timersPayRate" HeaderText=">1次支付比">
                        <ItemTemplate>
                            <asp:Label ID="Label_timersPayRate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField SortExpression="payRate" HeaderText="支付率">
                        <ItemTemplate>
                            <asp:Label ID="Label_payRate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="isPaidOrderAmount" HeaderText="支付额" SortExpression="isPaidOrderAmount" />
                    <asp:BoundField DataField="refundOrderCount" HeaderText="退单数" SortExpression="refundOrderCount" />
                    <asp:BoundField DataField="refundOrderAmount" HeaderText="退单额" SortExpression="refundOrderAmount" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            详情
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_Detail" CssClass="linkButtonDetail" ForeColor="Blue" runat="server"
                                CommandName="Order" Text="门店详情"></asp:LinkButton>
                            ---
                            <asp:LinkButton ID="secondProportionDetail" CssClass="linkButtonDetail" ForeColor="Blue" runat="server"
                                CommandName="SecondProportion" Text="二次比例"></asp:LinkButton>
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
                    <th>共
                    </th>
                    <td>
                        <asp:Label ID="Label_OrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                    </td>
                    <th>单
                    </th>
                    <th></th>
                    <th>总金额
                    </th>
                    <td>￥<asp:Label ID="Label_preOrderServerSumSum" runat="server" Text="0" ForeColor="#F40404"
                        Font-Bold="True"></asp:Label>
                    </td>
                    <th></th>
                    <th>支付共
                    </th>
                    <td>
                        <asp:Label ID="Label_payOrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                    </td>
                    <th>单
                    </th>
                    <th></th>
                    <th>支付总金额
                    </th>
                    <td>￥<asp:Label ID="Label_prePaidSumSum" runat="server" Text="0" ForeColor="#F40404"
                        Font-Bold="True"></asp:Label>
                    </td>
                    <th></th>
                    <th>退款共
                    </th>
                    <td>
                        <asp:Label ID="Label_refundCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                    </td>
                    <th>单
                    </th>
                    <th></th>
                    <th>退款总金额
                    </th>
                    <td>￥<asp:Label ID="Label_refundAmount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <br />
                备注：<br />
                <div style="font-size: 12px">
                    >1次订单量表示在当前时间（天）的00：00：00之前用悠先点菜下过订单的用户的订单数量；<br />
                    >1次订单比=>1次订单量/订单量；<br />
                    >1次支付量表示在当前时间（天）的00：00：00之前用悠先点菜下过订单并且支付过的用户的订单数量；<br />
                    >1次支付量=>1次支付量/支付量<br />
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:HiddenField ID="HiddenField_DinnerStarTime" runat="server" />
        <asp:HiddenField ID="HiddenField_DinnerEndTime" runat="server" />
        <asp:Panel ID="Panel_Detail" runat="server" CssClass="panelSyle">
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="orderTime"
                AutoGenerateColumns="False" SkinID="gridviewSkin"
                OnSorting="GridView_Sorting">
                <Columns>
                    <asp:BoundField DataField="orderTime" HeaderText="日期" />
                    <asp:BoundField DataField="orderCount" HeaderText="订单量" />
                    <asp:BoundField DataField="orderSumAmount" HeaderText="订单额" />
                    <asp:BoundField DataField="timesOrderQuantity" HeaderText=">1次订单量" />
                    <asp:BoundField DataField="timersOrderRate" HeaderText=">1次订单比" />
                    <asp:BoundField DataField="isPaidOrderCount" HeaderText="支付量" />
                    <asp:BoundField DataField="isPaidOrderAmount" HeaderText="支付额" />
                    <asp:BoundField DataField="payRate" HeaderText="支付率" />
                    <asp:BoundField DataField="timersPayCount" HeaderText=">1次支付量" />
                    <asp:BoundField DataField="timersPayRate" HeaderText=">1次支付比" />
                </Columns>
            </asp:GridView>
            <div style="width: 100%; text-align: right">
                <br />
                <asp:Button ID="Button_cancel" runat="server" Text="退  出" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Detail')" />
            </div>
        </asp:Panel>
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
