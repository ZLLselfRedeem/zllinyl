<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchMoneyApplyDetailDS.aspx.cs"
    Inherits="FinanceManage_batchMoneyApplyDetailDS" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建批量打款申请</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initData("batchmoneyapply");
            GridViewStyle("GridView1", "gv_OverRow");
            TabManage();
        });

        function mathAmount()
        {
            var realamount = document.getElementById("txt_applyAmount").value;
            if (isNumber(realamount)) {
                var applyAmount = document.getElementById("tb_remainMoney").value;
                var redEnvelopeAmount = document.getElementById("tb_redEnvelopeAmount").value;
                var foodCouponAmount = document.getElementById("tb_foodCouponAmount").value;
                var alipayAmount = document.getElementById("tb_alipayAmount").value;
                var wechatPayAmount = document.getElementById("tb_wechatPayAmount").value;
                var commissionAmount = document.getElementById("tb_commissionAmount").value;
                var amountP = realamount / applyAmount;

                //var redP = redEnvelopeAmount / applyAmount;
                ////var foodP = Math.Round(tb_foodCouponAmount / applyAmount, 2);
                //var aliP = alipayAmount / applyAmount;
                //var weP = wechatPayAmount / applyAmount;
                var red = redEnvelopeAmount * amountP;
                document.getElementById("txt_redEnvelopeAmount").value = red.toFixed(2);
                var ali = alipayAmount * amountP;
                document.getElementById("txt_alipayAmount").value = ali.toFixed(2);
                var we = wechatPayAmount * amountP;
                document.getElementById("txt_wechatPayAmount").value = we.toFixed(2);
                var food = foodCouponAmount * amountP;
                document.getElementById("txt_foodCouponAmount").value = food.toFixed(2);
                var com = commissionAmount * amountP;
                document.getElementById("txt_commissionAmount").value = com.toFixed(2);
                var volume = parseFloat(com) + parseFloat(realamount);
                document.getElementById("txt_volume").value = volume.toFixed(2);
                if (document.getElementById("tb_viewallocCommissionType").value == "2") {
                    var allamount = parseFloat(realamount) + parseFloat(com);
                    var rcom = com / allamount;
                    document.getElementById("txt_realCommissionValue").value = rcom.toFixed(2);
                }

                var overPlus = realamount - red.toFixed(2) - food.toFixed(2) - we.toFixed(2) - ali.toFixed(2);
                if (parseFloat(overPlus) > 0)
                {
                    if (parseFloat(red.toFixed(2)) > 0)
                    {
                        document.getElementById("txt_redEnvelopeAmount").value = (parseFloat(red.toFixed(2)) + parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                    else if (parseFloat(food.toFixed(2)) > 0)
                    {
                        document.getElementById("txt_foodCouponAmount").value = (parseFloat(food.toFixed(2)) + parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                    else if (parseFloat(we.toFixed(2)) > 0) {
                        document.getElementById("txt_wechatPayAmount").value = (parseFloat(we.toFixed(2)) + parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                    else if (parseFloat(ali.toFixed(2)) > 0) {
                        document.getElementById("txt_alipayAmount").value = (parseFloat(ali.toFixed(2)) + parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                }
                else if (parseFloat(overPlus) < 0) {
                    overPlus = overPlus * -1;
                    if (parseFloat(red.toFixed(2)) > parseFloat(overPlus)) {
                        document.getElementById("txt_redEnvelopeAmount").value = (parseFloat(red.toFixed(2)) - parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                    else if (parseFloat(food.toFixed(2)) > parseFloat(overPlus)) {
                        document.getElementById("txt_foodCouponAmount").value = (parseFloat(food.toFixed(2)) - parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                    else if (parseFloat(we.toFixed(2)) > parseFloat(overPlus)) {
                        document.getElementById("txt_wechatPayAmount").value = (parseFloat(we.toFixed(2)) - parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                    else if (parseFloat(ali.toFixed(2)) > parseFloat(overPlus)) {
                        document.getElementById("txt_alipayAmount").value = (parseFloat(ali.toFixed(2)) - parseFloat(overPlus.toFixed(2))).toFixed(2);
                    }
                }
            }
        }

        function isNumber(oNum) {
            if (!oNum) return false;
            var strP = /^\d+(\.\d+)?$/;
            if (!strP.test(oNum)) return false;
            try {
                if (parseFloat(oNum) != oNum) return false;
            }
            catch (ex) {
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        li:hover
        {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="修改批量打款申请" navigationUrl="" headName="修改批量打款申请" />
        <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 100%">
                         <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th>打款单号:</th>
                                <td>
                                     <asp:TextBox ReadOnly="true" ID="txt_batchMoneyApplyDetailCode" runat="server"></asp:TextBox>
                                    </td>
                                <th>
                                    公司名称:
                                </th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_companyName" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                    门店名:
                                </th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_shopName" runat="server"></asp:TextBox>
                                </td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>
                                    开户银行:
                                </th>
                                <td>
                                    <asp:TextBox ID="txt_bankName" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                    支行名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="txt_PayeeBankName" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                    开户名:</th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_accountName" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                    账号:
                                </th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_bankAccount" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    申请打款时间:</th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_createdTime" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                     财务打款时间: </th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_financePlayMoneyTime" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                    打款标识：
                                </th>
                                <td>
                                    <asp:TextBox ReadOnly="true" ID="txt_isFirst" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    结账扣款金额:</th>
                                 <td>
                                     <input runat="server" id="txt_applyAmount"/>
                                     <input runat="server" id="tb_redEnvelopeAmount" style="display:none"/>
                                     <input runat="server" id="tb_foodCouponAmount" style="display:none"/>
                                     <input runat="server" id="tb_alipayAmount" style="display:none"/>
                                     <input runat="server" id="tb_wechatPayAmount" style="display:none"/>
                                     <input runat="server" id="tb_commissionAmount" style="display:none"/>
                                     <input runat="server" id="tb_applyAmount" style="display:none"/>
                                     <input runat="server" id="tb_remainMoney" style="display:none"/>
                                     <input runat="server" id="tb_amountFrozen" style="display:none"/>
                                     <input runat="server" id="tb_viewallocCommissionType" style="display:none"/>
                                </td>
                                <td colspan="2" style="border-right: none;">
                                    <asp:Label runat="server" ID="lb_maxMin"></asp:Label>
                                </td>
                                <th>
                                    当前佣金比例：
                                </th>
                                <td colspan="4">
                                    <asp:TextBox ID="txt_viewallocCommissionValue" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    回款状态:
                                </th>
                                <td colspan="7">
                                    <asp:DropDownList runat="server" id="ddlStatus" Enabled="false">
                                        <asp:ListItem Value="0">全部</asp:ListItem>
                                        <asp:ListItem Value="5">申请未提交</asp:ListItem>
                                        <asp:ListItem Value="6">申请被撤回</asp:ListItem>
                                        <asp:ListItem Value="7">申请提交至出纳</asp:ListItem>
                                        <asp:ListItem Value="8">出纳已确认帐目</asp:ListItem>
                                        <asp:ListItem Value="9">主管提交至银行</asp:ListItem>
                                        <asp:ListItem Value="10">银行已受理</asp:ListItem>
                                        <asp:ListItem Value="11">银行未受理</asp:ListItem>
                                        <asp:ListItem Value="12">银行打款成功</asp:ListItem>
                                        <asp:ListItem Value="13">银行打款失败</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    流水号或备注:
                                </th>
                                <td colspan="7" style="border-right: none;">
                                    <asp:TextBox runat="server" ReadOnly="true" Height="100px" Width="400px" ID="txt_serialNumberOrRemark" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                        <table>
                            <tr>
                                <td>
                                    <asp:Button Text="修 改" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button Text="返 回" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnBack" OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                         <hr size="1" style="border: 1px #cccccc dashed;" />
                        <!--生成结果<br />
                        批量打款<asp:LinkButton ID="recordId" runat="server" Text="0"></asp:LinkButton>生成成功，包含<asp:Label
                            ID="totleCount" runat="server" Text="0"></asp:Label>笔共计<asp:Label ID="totleAmount"
                                runat="server" Text="0"></asp:Label>元-->
                    </div>
                </div>
    </form>
</body>
</html>
