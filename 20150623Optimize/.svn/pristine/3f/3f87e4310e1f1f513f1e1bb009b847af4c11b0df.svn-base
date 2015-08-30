<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchMoneyApply.aspx.cs"
    Inherits="CustomerServiceProcessing_batchMoneyApply" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建批量打款申请</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="新建批量打款申请" navigationUrl="" headName="新建批量打款申请" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>新建批量打款申请</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div>
                        <table>
                            <tr>
                                <td colspan="3">城市：<asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                </td>
                                <td rowspan="2">&nbsp; &nbsp;
                                <asp:Button ID="btn_create" runat="server" Height="50px" Width="80px" Text="生 成"
                                    Font-Size="X-Large" OnClick="btn_create_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>余额大于等于
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_amount" runat="server"></asp:TextBox>
                                </td>
                                <td>元
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        生成结果<br />
                        批量打款<asp:LinkButton ID="recordId" runat="server" Text="0"></asp:LinkButton>生成成功，包含<asp:Label
                            ID="totleCount" runat="server" Text="0"></asp:Label>笔共计<asp:Label ID="totleAmount"
                                runat="server" Text="0"></asp:Label>元
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
