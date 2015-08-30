<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyCommissionAndFreeRefundHour.aspx.cs"
    Inherits="CompanyManage_CompanyCommissionAndFreeRefundHour" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司佣金设置</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            TabManage();
            $("#form1").validate({
                rules: {
                    TextBox_CommissionValue: { "required": true, "number": true, "min": 0 },
                    TextBox_FreeRefundHour: { "number": true, "min": 0 }
                },
                messages: {
                    TextBox_CommissionValue: { "required": "佣金值不能为空", "number": "请输入不小于0的数字" },
                    TextBox_FreeRefundHour: "请输入不小于0的数字"
                },
                success: function (label) {
                    label.html("&nbsp;").addClass("checked");
                }
            });
        });
    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="公司佣金设置" navigationImage="~/images/icon/list.gif"
        navigationText="公司列表" navigationUrl="javascript:history.go(-1)" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>公司佣金设置</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            公司名称：
                        </th>
                        <td>
                            <asp:Label ID="Label_CompanyName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            佣金形式：
                        </th>
                        <td>
                            <asp:DropDownList ID="DropDownList_CommissionType" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList_CommissionType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th>
                            佣金值：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_CommissionValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label_Alert" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            无忧退款时间：
                        </th>
                        <td>
                            <asp:TextBox ID="TextBox_FreeRefundHour" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            单位：小时
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button_Set" runat="server" Text="设    置" CssClass="button" OnClick="Button_Set_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
