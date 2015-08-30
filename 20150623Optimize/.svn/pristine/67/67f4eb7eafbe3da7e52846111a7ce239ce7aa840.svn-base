<%@ Page Language="C#" AutoEventWireup="true" CodeFile="customerRechargeDetail.aspx.cs"
    Inherits="ClientRecharge_customerRechargeDetail" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户充值统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="用户充值统计" navigationUrl="" headName="用户充值统计" />
    <div class="content" id="divList" runat="server">
        <div class="layout">
            <div class="QueryTerms">
                <table style="width: 100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td width="40%">
                            起止时间
                            <asp:TextBox ID="txbBeginTime" runat="server" CssClass="Wdate" onfocus="startDate(this)"
                                Width="160px" OnTextChanged="txbBeginTime_TextChanged" AutoPostBack="true"></asp:TextBox>~
                            <asp:TextBox ID="txbEndTime" runat="server" CssClass="Wdate" onfocus="endDate(this)"
                                Width="160px" OnTextChanged="txbEndTime_TextChanged" AutoPostBack="true"></asp:TextBox>
                            &nbsp;&nbsp; 活动名称
                            <asp:DropDownList runat="server" ID="ddlRecharge" OnSelectedIndexChanged="ddlRecharge_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_RechageList" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvRechageList" AutoGenerateColumns="False" CssClass="gridview"
                        SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="CustomerID" Visible="false" />
                            <asp:BoundField DataField="mobilePhoneNumber" HeaderText="用户" />
                            <asp:BoundField DataField="rechargeCount" HeaderText="充值次数" />
                            <asp:BoundField DataField="rechargeAmount" HeaderText="金额" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_detail.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnDetail" CommandName="detail" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CustomerID") %>'>详情</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                            OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <th>
                                    共
                                </th>
                                <td>
                                    <asp:Label ID="lbCustomerCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    个用户
                                </th>
                                <th>
                                </th>
                                <th>
                                    充值共
                                </th>
                                <td>
                                    <asp:Label ID="lbRechargeCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    次
                                </th>
                                <th>
                                </th>
                                <th>
                                    充值总额
                                </th>
                                <td>
                                    <asp:Label ID="lbRechargeAmount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    元
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div align="right">
                        <asp:Button runat="server" ID="btnBack" Text="返回" OnClick="btnBack_Click" /></div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="content" id="divDetail" runat="server" style="display: none">
        <div class="layout">
            <asp:Panel ID="Panel_RechageDetail" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvRechageDetail" AutoGenerateColumns="False" CssClass="gridview"
                        DataKeyNames="preOrder19dianId" SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="preOrder19dianId" Visible="false" />
                            <asp:BoundField DataField="mobilePhoneNumber" HeaderText="用户" />
                            <asp:BoundField DataField="shopName" HeaderText="门店" />
                            <asp:BoundField DataField="payTime" HeaderText="消费时间" />
                            <asp:BoundField DataField="paySum" HeaderText="消费金额" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_detail.gif" alt="" runat="server" id="image" />
                                    <asp:LinkButton runat="server" ID="lnkbtnDishDetail" CommandName="detail" OnCommand="lnkbtnDishDetail_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"preOrder19dianId") %>'>点菜明细</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                            OnPageChanged="AspNetPager2_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                    <div align="right">
                        <asp:Button runat="server" ID="BackDetail" Text="返回" OnClick="btnBackDetail_Click" /></div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <asp:Panel ID="Panel_QueryDishOrder" runat="server" CssClass="panelSyle">
        <table>
            <tr>
                <th class="dialogBox_th">
                    <asp:Label ID="Label_Title" runat="server" Text="点单详情"></asp:Label>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:GridView runat="server" ID="Gr_DishDetail" AutoGenerateColumns="False" CssClass="gridview"
                        SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="dishDesc" HeaderText="菜名" />
                            <asp:BoundField DataField="dishFraction" HeaderText="份数" />
                            <asp:BoundField DataField="dishPrice" HeaderText="价格" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="Button_cancel" runat="server" Text="确  定" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_QueryDishOrder')" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenField_MedalId" runat="server" />
    </asp:Panel>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvRechageList", "gv_OverRow");
            GridViewStyle("gdvRechageDetail", "gv_OverRow");
            GridViewStyle("Gr_DishDetail", "gv_OverRow");
        });
        var startDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                onpicked: function (dp) {
                    elem.blur();
                },
                skin: 'whyGreen'
            });
        };
        var endDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                onpicked: function (dp) { elem.blur() },
                skin: 'whyGreen'
            });
        }
    </script>
</body>
</html>
