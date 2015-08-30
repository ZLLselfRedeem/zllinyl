<%@ Page Language="C#" AutoEventWireup="true" CodeFile="customerRechargeStatistic.aspx.cs"
    Inherits="ClientRecharge_customerRechargeStatistic" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>充值统计概览</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="充值统计概览" navigationUrl="" headName="充值统计概览" />
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
                            <asp:BoundField DataField="payTime" HeaderText="日期" DataFormatString="{0:yyyy/MM/dd}" />
                            <asp:BoundField DataField="rechargeCount" HeaderText="充值次数" />
                            <asp:BoundField DataField="rechargeAmount" HeaderText="金额" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_detail.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnDetail" CommandName="detail" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"payTime") %>'>详情</asp:LinkButton>
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
                                    充值总金额
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
                </div>
            </asp:Panel>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvRechageList", "gv_OverRow");
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
