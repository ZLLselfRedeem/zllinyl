<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataExportExcel.aspx.cs"
    Inherits="StatisticalStatement_DataExportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户量统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table class="table" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="4">选择或者填写要导出excel表的预点单信息查询条件（不需要可不填写或者不选择）：
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle" class="auto-style1">城市：
                        </td>
                        <td colspan="2" class="auto-style1">
                            <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                        </td>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle" class="auto-style1">手机号码：
                        </td>
                        <td colspan="2" class="auto-style1">
                            <asp:TextBox ID="TextBox_Number" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">下单时间：
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox_orderStartTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_orderEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                            Width="85px"></asp:TextBox>
                        </td>
                        <td>（都填写才有效）</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">支付时间：
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox_preOrderTimeStr" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                            Width="85px"></asp:TextBox>
                        </td>
                        <td>（都填写才有效）</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle">入座（审核）时间：
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox_verificationStartTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_verificationEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                            Width="85px"></asp:TextBox>
                        </td>
                        <td>（都填写才有效）</td>
                    </tr>
                    <tr>
                        <td>公司：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_Company" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>门店：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_Shop" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>支付金额：
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox_paymentMin" runat="server"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_paymentMax" runat="server"></asp:TextBox>
                        </td>
                        <td>（都填写才有效）</td>
                    </tr>
                    <tr>
                        <td>点单金额：
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox_orderMin" runat="server"></asp:TextBox>
                            &nbsp;-&nbsp;
                        <asp:TextBox ID="TextBox_orderMax" runat="server"></asp:TextBox>
                        </td>
                        <td>（都填写才有效）</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 100px; text-align: center">
                            <asp:Button ID="Button_operate" runat="server" CssClass="couponButtonSubmit" Text="导出excel"
                                OnClick="Button_operate_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
