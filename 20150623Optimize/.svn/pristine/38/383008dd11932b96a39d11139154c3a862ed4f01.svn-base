<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendRedEnvelope.aspx.cs" Inherits="RedEnvelope_SendRedEnvelope" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发送红包</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
                navigationText="" navigationUrl="" headName="发送红包" />           
            <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
                <div style="width: 80%">
                    <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <th style="width: 30%">导入客户手机
                            </th>
                            <td>
                                <asp:FileUpload runat="server" ID="fileUploadPhone" Width="410px" Height="25px" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnImport" Text="导入Excel" class="button" OnClick="btnImport_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th>客户手机
                            </th>
                            <td>
                                <asp:TextBox runat="server" ID="txbMobilePhoneNumber" TextMode="MultiLine" Width="400px" Height="52px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                【多个电话号码请用英文逗号隔开】
                            </td>
                        </tr>
                        <tr runat="server" id="trActivity">
                            <th style="width: 20%; text-align: left">活动:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:RadioButtonList runat="server" ID="rblActivity" RepeatColumns="4" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                                <br />
                                （请选择相应的活动）
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div style="width: 80%; text-align: center">
                    <asp:Button runat="server" ID="btnSave" Text="发放" CssClass="button"
                        OnClientClick="return Check();" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                </div>
            </div>
            <uc1:CheckUser ID="CheckUser1" runat="server" />
        </div>
    </form>
</body>
</html>
