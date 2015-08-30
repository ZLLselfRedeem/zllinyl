<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackageStatisticsView.aspx.cs"
    Inherits="Package_PackageStatisticsView" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看活动数据</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 198px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="查看活动数据" navigationUrl="" headName="查看活动数据" />
      <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th width="15%">商户名:</th>
                            <td>
                                    <asp:TextBox ID="tbShopName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <th width="13%">
                                发布时间：
                            </th>
                            <td>
                                <asp:TextBox ID="tbSendTime" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                             <th >
                                活动名称:
                            </th>
                            <td>
                                <asp:TextBox ID="tbCouponName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <th >
                                营销套餐:
                            </th>
                            <td>
                                <asp:TextBox ID="tbName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                             <th>
                                活动内容：
                            </th>
                            <td>
                                <asp:TextBox ID="tbMJ" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <th>
                                付费金额：
                            </th>
                            <td>
                                <asp:TextBox ID="tbAmount" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th width="15%">
                                    发送数:
                                </th>
                                <td>
                                    <asp:TextBox ID="tbSendUsers" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th width="13%">
                                    查看数：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbClickCount" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th >
                                    领取数:
                                </th>
                                <td>
                                  <asp:TextBox ID="tbReceiveCount" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    使用数：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUsedCount" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    订单金额:
                                </th>
                                <td>
                                   <asp:TextBox ID="tbPrePaidSum" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    优惠金额：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDiscountAmount" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                 <th >
                                     活动回报率：
                                 </th>
                                 <td>
                                    <asp:TextBox ID="tbROI" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                 </td>
                             </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button Text="返 回" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnBack" OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
    </form>
</body>
</html>
