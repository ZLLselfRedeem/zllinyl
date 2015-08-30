<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dataExportExcel.aspx.cs"
    Inherits="StatisticalStatement_Other_dataExportExcel" %>

<%@ Register Src="../../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Src="../../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>excel导出</title>
    <link href="../../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="excel导出" navigationImage="~/Other/images/icon/list.gif"
            navigationText="" navigationUrl="" />
        <div>
            <div class="tagMenu">
                <ul class="menu">
                    <li>excel导出</li>
                </ul>
            </div>
            <div>
                <table class="table" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2">请准确填写需要导出excel表的信息查询条件：
                        </td>
                    </tr>
                    <tr>
                        <td>城市：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCity" runat="server">
                                <asp:ListItem Value="">--请选择--</asp:ListItem>
                                 <asp:ListItem Value="87">杭州</asp:ListItem>
                                 <asp:ListItem Value="73">上海</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">时间：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_orderStartTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_orderEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                            Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>支付金额(不填写默认为0-10000000)：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_paymentMin" runat="server"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_paymentMax" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>门店状态：</td>
                        <td>
                            <asp:DropDownList ID="DropDownListShopState" runat="server">
                                <asp:ListItem Value ="" Selected="True" >--请选择--</asp:ListItem>
                                <asp:ListItem Value ="1">已上线</asp:ListItem>
                                <asp:ListItem Value="2" >未上线</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="display:none">
                        <td>汇总类型：
                        </td>
                        <td>
                            <asp:RadioButton ID="sum" Checked="true" GroupName="rb" Text="金额" runat="server" />
                            <asp:RadioButton ID="count" GroupName="rb" Text="点单数量" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>筛选条件：
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection ="Horizontal">
                                <asp:ListItem  Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem  Value="103" >已入座（已对账）</asp:ListItem>
                                <asp:ListItem  Value="102" >未入座</asp:ListItem>
                            </asp:RadioButtonList>
                            <%--<asp:RadioButton ID="rbNone" Checked="true" GroupName="type" Text="全部" runat="server" />
                            <asp:RadioButton ID="rbConfrim" GroupName="type" Text="已入座（已对账）" runat="server" />
                            <asp:RadioButton ID="rbNotConfrim" GroupName="type" Text="未入座" runat="server" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 100px; text-align: center">
                            <asp:Button ID="Button_operate" runat="server" CssClass="couponButtonSubmit" Text="导出excel"
                                OnClick="Button_operate_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        备注：
         <br />
        筛选条件选择全部和未入座导出数据时间根据支付时间查询，导出数据为已支付数据，金额为支付金额减去退款金额；
        <br />
        选择条件选择入座导出数据时间根据最后入座时间查询，导出数据为已支付、已入座、已对账，金额为支付金额减去退款金额；
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
