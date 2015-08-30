<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntegrationRuleDetail.aspx.cs"
    Inherits="Integration_IntegrationRuleDetail" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑积分规则</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 198px;
        }
    </style>
    <script type="text/javascript">
        function gbcount(total, used, remain) {
            var max;
            max = total.value;
            if (document.getElementById("tbActivityExplain").value.length > max) {
                document.getElementById("tbActivityExplain").value = document.getElementById("tbActivityExplain").value.substring(0, max);
                used.value = max;
                remain.value = 0;
            }
            else {
                used.value = document.getElementById("tbActivityExplain").value.length;
                remain.value = max - used.value;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
             navigationText="编辑积分规则" navigationUrl="" headName="编辑积分规则" />
      <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th class="auto-style1">业务事件类型:</th>
                                <td>
                                     <asp:DropDownList ID="ddlBussnessEventType" runat="server" OnSelectedIndexChanged="ddlBussnessEventType_SelectedIndexChanged" AutoPostBack="True">
                                   </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    业务事件描述:
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDescription" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    积分值:</th>
                                <td>
                                    <asp:TextBox ID="tbIntegration" runat="server" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    事件类型:</th>
                                <td>
                                   <asp:DropDownList runat="server" ID="ddlEventType" AutoPostBack="True" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged">
                                       <asp:ListItem Text="首次" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="区间" Value="2"></asp:ListItem>
                                       <asp:ListItem Text="单次" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="累积" Value="4"></asp:ListItem>
                                   </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" id="trEventComplement">
                                <th class="auto-style1">
                                    事件补充:
                                </th>
                                <td colspan="7">
                                    <asp:DropDownList ID="ddlEventComplement" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEventComplement_SelectedIndexChanged">
                                        <asp:ListItem Text="订单金额" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="入座次数" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="评论次数" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Label runat="server" ID="lbBegin" Text="在"></asp:Label><asp:TextBox ID="tbConditionalMinValue" runat="server"></asp:TextBox><asp:Label runat="server" ID="lbEnd" Text="至"></asp:Label><asp:TextBox ID="tbConditionalMaxValue" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    启停状态：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Text="已启用" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="已停用" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button Text="保 存" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" />
                               
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
    </form>
</body>
</html>
