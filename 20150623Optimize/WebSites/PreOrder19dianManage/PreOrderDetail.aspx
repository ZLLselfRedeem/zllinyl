<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreOrderDetail.aspx.cs" Inherits="PreOrder19dianManage_PreOrderDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body style="overflow-y: hidden">
    <form id="form1" runat="server">
    <asp:Panel ID="Panel_divOperate" runat="server">
        <div class="divOperate">
            <table class="table" cellpadding="0" cellspacing="0" style="empty-cells: show; text-align: left;
                margin-bottom: 5px; margin-left: 10px; width: 40%; border-collapse: collapse;">
                <tr>
                    <th>
                        客服操作：
                    </th>
                    <td runat="server" id="tr_CheckPreOrderAmount">
                        <asp:Button ID="Button_CheckPreOrderAmount" CssClass="button" runat="server" Text="对 账"
                            OnClick="Button_CheckPreOrderAmount_Click" OnClientClick="return confirm('是否确定执行次操作？') " />
                    </td>
                    <td runat="server" id="tr_ConfirmPreOrder">
                        <asp:Button ID="Button_ConfirmPreOrder" CssClass="button" runat="server" Text="审 核"
                            OnClick="Button_ConfirmPreOrder_Click" OnClientClick="return confirm('是否确定执行次操作？')" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div style="margin: 10px;">
        <div class="couponStep">
            <div class="divDetailContentTitle">
                <label>
                    ▲基本信息</label>
            </div>
            <div class="divDetailContentContent">
                <table class="table" cellpadding="0" cellspacing="0" style="empty-cells: show; text-align: left;
                    margin-bottom: 5px; width: 100%; border-collapse: collapse;">
                    <tr>
                        <th>
                            流水号
                        </th>
                        <th>
                            Vip卡号
                        </th>
                        <th>
                            用户昵称
                        </th>
                        <th>
                            手机号码
                        </th>
                        <th>
                            发票抬头
                        </th>
                        <th>
                            支付金额
                        </th>
                        <th>
                            支付时间
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label_preOrder19dianId" runat="server" Font-Bold="True" Font-Size="15px"
                                ForeColor="#F40404"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_eCardNumber" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_UserName" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_PhoneNum" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_invoiceTitle" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            ￥<asp:Label ID="Label_preOrderServerSum" runat="server" Text="" ForeColor="#F40404"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_preOrderTime" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="couponStep">
            <div class="divDetailContentTitle">
                <label>
                    ▲支付信息</label>
            </div>
            <div class="divDetailContentContent">
                <table class="table" cellpadding="0" cellspacing="0" style="empty-cells: show; text-align: left;
                    margin-bottom: 5px; width: 100%; border-collapse: collapse">
                    <tr>
                        <th>
                            是否支付
                        </th>
                        <th>
                            支付时间
                        </th>
                        <th>
                            支付金额
                        </th>
                        <th>
                            无忧退款截止时间
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label_isPaid" runat="server" Text="" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_prePayTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            ￥<asp:Label ID="Label_prePaidSum" runat="server" Text="" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_refundDeadline" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="couponStep">
            <div class="divDetailContentTitle">
                <label>
                    ▲退款信息</label>
            </div>
            <div class="divDetailContentContent">
                <asp:GridView ID="GridView_Refund" runat="server" AutoGenerateColumns="False" SkinID="GridviewSkinBlue">
                    <Columns>
                        <asp:TemplateField HeaderText="行号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="退款状态">
                            <ItemTemplate>
                                已退款
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="refundMoney" HeaderText="退款金额" />
                        <asp:BoundField DataField="operTime" HeaderText="退款时间" />
                        <asp:BoundField DataField="remark" HeaderText="退款原因" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="couponStep">
        <div class="divDetailContentTitle">
            <label>
                ▲菜品信息</label>
        </div>
        <div class="divDetailContentContent">
            <asp:GridView ID="GridView_Dish" runat="server" AutoGenerateColumns="False" SkinID="GridviewSkinBlue">
                <Columns>
                    <asp:TemplateField HeaderText="行号">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="dishName" HeaderText="菜名" />
                    <asp:BoundField DataField="dishPriceName" HeaderText="规格" />
                    <asp:BoundField DataField="unitPrice" HeaderText="价格" />
                    <asp:BoundField DataField="quantity" HeaderText="数量" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="couponStep">
        <div class="divDetailContentTitle">
            <label>
                ▲对账信息</label>
        </div>
        <div class="divDetailContentContent">
            <asp:Label ID="Label_PreOrderCheckInfo" runat="server" Text="该点单暂无对账信息"></asp:Label>
            <asp:GridView ID="GridView_PreOrderCheckInfo" runat="server" AutoGenerateColumns="False"
                SkinID="GridviewSkinBlue" DataKeyNames="status">
                <Columns>
                    <asp:TemplateField HeaderText="行号">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="employeeName" HeaderText="操作者" />
                    <asp:BoundField DataField="employeePosition" HeaderText="操作者职位" />
                    <asp:BoundField DataField="checkTime" HeaderText="操作者时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="Label_status" runat="server" Text="Label"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="couponStep">
        <div class="divDetailContentTitle">
            <label>
                ▲审核信息</label>
        </div>
        <div class="divDetailContentContent">
            <asp:Label ID="Label_PreorderShopConfirmedInfo" runat="server" Text="该点单暂无确认信息"></asp:Label>
            <asp:GridView ID="GridView_PreorderShopConfirmedInfo" runat="server" AutoGenerateColumns="False"
                SkinID="GridviewSkinBlue" DataKeyNames="status">
                <Columns>
                    <asp:TemplateField HeaderText="行号">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="employeeName" HeaderText="操作者" />
                    <asp:BoundField DataField="employeePosition" HeaderText="操作者职位" />
                    <asp:BoundField DataField="shopConfirmedTime" HeaderText="操作者时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="Label_PreorderShopConfirmedInfo_status" runat="server" Text="Label"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </div>
    <br />
    <br />
    </form>
</body>
</html>
