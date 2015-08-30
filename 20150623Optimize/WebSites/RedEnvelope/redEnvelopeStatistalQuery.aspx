<%@ Page Language="C#" AutoEventWireup="true" CodeFile="redEnvelopeStatistalQuery.aspx.cs" Inherits="RedEnvelope_redEnvelopeStatistalQuery" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>红包相关统计查询</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="红包相关统计查询" />
            <div class="content" id="divList" runat="server">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 90%">
                        <tr>
                            <th style="width: 12%">活动名称  
                            </th>
                            <td colspan="7">
                                <asp:TextBox runat="server" ID="txbActivityName"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                <asp:Button runat="server" ID="btnQuery" Text="查询" CssClass="button" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 12%">获取红包时间  
                            </th>
                            <td colspan="7">
                                <asp:TextBox ID="txbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>&nbsp; ~&nbsp;
                                <asp:TextBox ID="txbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 12%">用户注册时间  
                            </th>
                            <td colspan="7">
                                <asp:TextBox ID="txbRegisterBegin" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>&nbsp; ~&nbsp;
                                <asp:TextBox ID="txbRegisterEnd" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox runat="server" ID="ckbRegisterTime" Text="参考注册时间" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height: 40px">-------------------------------------红包相关-------------------------------------</td>
                        </tr>
                        <tr>
                            <th style="width: 12%">参与人数  
                            </th>
                            <td style="width: 12%">
                                <asp:Label runat="server" ID="lbParticipateCount" Text=""></asp:Label>
                            </td>
                            <th style="width: 12%">抢到红包人数  
                            </th>
                            <td style="width: 12%">
                                <asp:Label runat="server" ID="lbRealCount" Text=""></asp:Label>
                            </td>
                            <th style="width: 12%">使用红包人数  
                            </th>
                            <td style="width: 12%" colspan="3">
                                <asp:Label runat="server" ID="lbUseCount" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>生成红包金额  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbParticipateAmout" Text=""></asp:Label>
                            </td>
                            <th>抢到红包金额  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbRealAmount" Text=""></asp:Label>
                            </td>
                            <th>使用红包金额  
                            </th>
                            <td colspan="3">
                                <asp:Label runat="server" ID="lbUseAmount" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height: 40px">-------------------------------------点单相关-------------------------------------</td>
                        </tr>
                        <tr>
                            <th>城市</th>
                            <td>
                                <asp:DropDownList ID="ddlCity" runat="server">
                                    <asp:ListItem Value="0" Text="==全部=="></asp:ListItem>
                                    <asp:ListItem Value="87" Text="杭州"></asp:ListItem>
                                    <asp:ListItem Value="73" Text="上海"></asp:ListItem>
                                </asp:DropDownList></td>

                            <th>红包类型</th>
                            <td colspan="5">
                                <asp:DropDownList ID="ddlActivityType" runat="server">
                                    <asp:ListItem Value="0" Text="==全部=="></asp:ListItem>
                                    <asp:ListItem Value="1" Text="大红包"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="天天红包"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="节日红包"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th style="width: 12%">支付时间  
                            </th>
                            <td colspan="7">
                                <asp:TextBox ID="txbPayTimeBegin" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>&nbsp; ~&nbsp;
                                <asp:TextBox ID="txbPayTimeEnd" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm:ss'})"
                                    Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <%--<asp:CheckBox runat="server" ID="ckbPayTime" Text="参考支付时间" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height: 40px">
                                <br />
                                此数据参考红包获取时间及支付时间；用户注册时间、城市、红包类型为可选项</td>
                        </tr>
                        <tr>
                            <th style="width: 12%">订单数量  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbOrderCount" Text=""></asp:Label>
                            </td>
                            <th>红包抵扣总额  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbRedenvelopeUsedAmount" Text=""></asp:Label>
                            </td>
                            <th>支付总额  
                            </th>
                            <td style="width: 12%">
                                <asp:Label runat="server" ID="lbPrePaidSum" Text=""></asp:Label>
                            </td>
                            <th style="width: 12%">退款总额  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbRefundMoneySum" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height: 40px">
                                <br />
                                此数据参考红包获取时间及支付时间；用户注册时间、城市、红包类型为可选项</td>
                        </tr>
                        <tr>
                            <th style="width: 12%">订单数量Top1门店  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbTopShopName" Text=""></asp:Label>
                            </td>
                            <th>门店订单数量
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbShopOrderCount" Text=""></asp:Label>
                            </td>
                            <th>门店支付总额  
                            </th>
                            <td style="width: 12%">
                                <asp:Label runat="server" ID="lbShopPrePaidSum" Text=""></asp:Label>
                            </td>
                            <th style="width: 12%">门店退款总额  
                            </th>
                            <td>
                                <asp:Label runat="server" ID="lbShopRefundMoneySum" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="height: 40px"></td>
                        </tr>
                        <tr>
                            <th>指定支付时间
                            </th>
                            <td colspan="8">从
                                <asp:TextBox ID="txbPayTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd'})"
                                    Width="150px"></asp:TextBox>&nbsp;&nbsp;&nbsp;开始，往前推
                                <asp:TextBox runat="server" ID="txbDayNum" Text="30" Width="40px">30</asp:TextBox>天，消费过<asp:TextBox runat="server" ID="txbPayNum" Width="40px" Text="2"></asp:TextBox>次及以上的人有<asp:TextBox runat="server" ID="txbCount" Width="50px"></asp:TextBox>个&nbsp;&nbsp;&nbsp;（此数据与红包无关）</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
