<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntegrationStatistics.aspx.cs"
    Inherits="Integration_IntegrationStatistics" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户积分统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
     <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
             navigationText="用户积分统计" navigationUrl="" headName="用户积分统计" />
      <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th>
                                    城市：
                                </th>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCity"></asp:DropDownList>
                                </td>
                                <th class="auto-style1">业务事件类型:</th>
                                <td>
                                    <asp:DropDownList ID="ddlBussnessEventType" runat="server" OnSelectedIndexChanged="ddlBussnessEventType_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <th class="auto-style1">
                                    业务事件描述:
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlIntegrationRule" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button Text="查询" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnSearch" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    时间:</th>
                                <td colspan="6">
                                   <asp:TextBox ID="tbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="130px"></asp:TextBox>至                                
                                    <asp:TextBox ID="tbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="130px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server">
                                <th class="auto-style1">
                                    发放积分数:
                                </th>
                                <td colspan="6">
                                    <asp:TextBox runat="server" ReadOnly="true" ID="tbIntegration"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
    </form>
</body>
</html>
