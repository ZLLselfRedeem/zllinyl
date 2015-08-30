<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batchMoneyManage.aspx.cs"
    Inherits="CustomerServiceProcessing_batchMoneyManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>批量打款申请管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_batchMoneyManage", "gv_OverRow");
            TabManage();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="批量打款申请管理" navigationUrl="" headName="批量打款申请管理" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>批量打款申请管理</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>城市：</td>
                                <td colspan="6">
                                    <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: middle">统计时间：
                                </td>
                                <td>
                                    <asp:Button ID="Button_1day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                        CommandName="1" Text="今天"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="Button_yesterday" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                        CommandName="yesterday" Text="昨天"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="Button_7day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                        CommandName="7" Text="最近7天"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="Button_14day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                        CommandName="14" Text="最近14天"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="Button_30day" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                        CommandName="30" Text="1个月"></asp:Button>
                                </td>
                                <td>
                                    <script type="text/javascript">
                                        var startDate = function (elem) {
                                            WdatePicker({
                                                el: elem, isShowClear: false, maxDate: '#F{$dp.$D(\'TextBox_preOrderTimeEnd\')||%y-%M-%d}', startDate: '2013-07-26',
                                                onpicked: function (dp) { elem.blur(); }
                                            });
                                        };
                                        var endDate = function (elem) {
                                            WdatePicker({
                                                el: elem, isShowClear: false, maxDate: '%y-%M-{%d+1}', minDate: '#F{$dp.$D(\'TextBox_preOrderTimeStr\')}', startDate: '2013-07-28',
                                                onpicked: function (dp) { elem.blur() }
                                            });
                                        }
                                    </script>
                                    <asp:TextBox ID="TextBox_preOrderTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                        AutoPostBack="true" Width="85px" OnTextChanged="TextBox_TextChanged"></asp:TextBox>
                                    &nbsp;-&nbsp;
                                <asp:TextBox ID="TextBox_preOrderTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                    OnTextChanged="TextBox_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>完成情况：
                                </td>
                                <td colspan="6">
                                    <asp:Button ID="btnAll" runat="server" CssClass="tabButtonBlueUnClick"
                                        CommandName="All" Text="所有" OnClick="btnStatus_Click"></asp:Button>
                                    <asp:Button ID="btnYes" runat="server" CssClass="tabButtonBlueUnClick"
                                        CommandName="Yes" Text="已完成" OnClick="btnStatus_Click"></asp:Button>
                                    <asp:Button ID="btnNot" runat="server" CssClass="tabButtonBlueUnClick"
                                        CommandName="Not" Text="未完成" OnClick="btnStatus_Click"></asp:Button></td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="Panel_batchMoneyManage" runat="server" CssClass="div_gridview">
                        <asp:GridView ID="GridView_batchMoneyManage" runat="server" DataKeyNames="batchMoneyApplyId"
                            AutoGenerateColumns="False" SkinID="gridviewSkin">
                            <Columns>
                                <asp:BoundField DataField="batchMoneyApplyId" HeaderText="ID" />
                                <asp:BoundField DataField="createdTime" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                                <asp:BoundField DataField="advanceCount" HeaderText="预打笔数" />
                                <asp:BoundField DataField="advanceAmount" HeaderText="预打金额" />
                                <asp:BoundField DataField="practicalCount" HeaderText="成功笔数" />
                                <asp:BoundField DataField="practicalAmount" HeaderText="成功金额" />
                                <asp:TemplateField ShowHeader="True">
                                    <HeaderTemplate>
                                        查看详情
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="queryText" runat="server" CausesValidation="False" CommandName="Select"
                                            Text="查看" CssClass="linkButtonDetail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                            <div class="gridviewBottom_left">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                    MoreButtonType="Image" NavigationButtonType="Image"
                                    OnPageChanged="AspNetPager1_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hiddenField" runat="server" />
    </form>
</body>
</html>
