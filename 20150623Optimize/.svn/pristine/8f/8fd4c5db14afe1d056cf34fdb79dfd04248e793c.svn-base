<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopRanking.aspx.cs" Inherits="PointsManage_shopRanking" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>门店排名</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content" id="divList" runat="server">
        <div class="layout">
            <div class="QueryTerms">
                <table cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
                            地区选择
                        </td>
                        <td colspan="3">
                            <asp:DropDownList runat="server" ID="ddlArea" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="全国" Selected="True" Value=""></asp:ListItem>
                                <asp:ListItem Text="杭州" Value="87"></asp:ListItem>
                                <asp:ListItem Text="上海" Value="9"></asp:ListItem>
                                <asp:ListItem Text="北京" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <hr size="1" style="border: 1px #cccccc dashed;" />
                <table>
                    <tr>
                        <td>
                            排名周期
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rblPeriod" OnSelectedIndexChanged="rblPeriod_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="上一月" Selected="True" Value="month">                            
                                </asp:ListItem>
                                <asp:ListItem Text="上一周" Value="week"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            自定义
                            <asp:TextBox ID="TextBox_TimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                OnTextChanged="TextBox_TimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_TimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                OnTextChanged="TextBox_TimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnClear" Text="重置" CssClass="button" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
                <hr size="1" style="border: 1px #cccccc dashed;" />
                <table>
                    <tr>
                        <td>
                            排序规则
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList runat="server" ID="rblOrder" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblOrder_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="金额高到底" Selected="True" Value="desc"></asp:ListItem>
                                <asp:ListItem Text="金额低到高" Value="asc"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_ShopRankingList" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvShopRankingList" AutoGenerateColumns="False"
                        CssClass="gridview" SkinID="gridviewSkin">
                        <Columns>
                            <asp:BoundField DataField="row_number" HeaderText="排名" />
                            <asp:BoundField DataField="shopName" HeaderText="门店" />
                            <asp:BoundField DataField="ApprovedMoney" HeaderText="验证金额" />
                        </Columns>
                    </asp:GridView>
                    <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                OnPageChanged="AspNetPager1_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </div>
    </div>
     <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("gdvShopRankingList", "gv_OverRow");
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
