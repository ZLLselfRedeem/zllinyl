<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerRepeatPay.aspx.cs"
    Inherits="Customer_CustomerRepeatPay" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户重复支付查询</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.4.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-widget-header
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvCustomerList", "gv_OverRow");
        });

        function isNumber(value) {
            var rules = "^[1-9][0-9]*$"; //正整数
            //var rules = "^[1-9]\d*|0$"; //整数

            if (value.match(rules) == null) {
                return false;
            }
            else {
                return true;
            }
        }
        function CheckNumber() {
            var num = document.getElementById("TextBox_mobilePhoneNumber").value;

            if (!isNumber(num) || num.length <= 0 || num.length > 11) {
                alert("请输入正确的电话号码");
            }
        }
    </script>
</head>
<body scroll="no" style="overflow-y: hidden;" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="客户重复支付查询" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>客户重复支付查询</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                用户手机：<asp:TextBox ID="TextBox_mobilePhoneNumber" runat="server" Width="200px" Height="24px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查   询" CssClass="button" OnClick="btnQuery_Click"
                                    OnClientClick="return CheckNumber();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_gridview" id="grid-view">
                    <asp:GridView runat="server" ID="gdvCustomerList" AutoGenerateColumns="False" CssClass="gridview"
                        SkinID="gridviewSkin" EmptyDataText="No Data" DataKeyNames="payType,orderStatus,connId,mobilePhoneNumber,UserName,totalFee"
                        OnRowDataBound="gdvCustomerList_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="connId" HeaderText="点单流水号" />
                            <asp:BoundField DataField="payType" HeaderText="支付方式" />
                            <asp:BoundField DataField="outTradeNo" HeaderText="支付交易号" />
                            <asp:BoundField DataField="UserName" HeaderText="客户昵称" />
                            <asp:BoundField DataField="mobilePhoneNumber" HeaderText="客户手机号码" />
                            <asp:BoundField DataField="totalFee" HeaderText="支付金额" />
                            <asp:BoundField DataField="orderCreateTime" HeaderText="支付请求时间" />
                            <asp:BoundField DataField="orderPayTime" HeaderText="支付完成时间" />
                            <asp:BoundField DataField="body" HeaderText="支付内容" />
                            <asp:TemplateField HeaderText="支付状态">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbPayType"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="refund" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"outTradeNo") %>'>退款</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/customer.js" type="text/javascript"></script>
    <%--   <uc1:CheckUser ID="CheckUser1" runat="server" />--%>
    </form>
</body>
</html>
