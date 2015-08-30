<%@ Page Language="C#" AutoEventWireup="true" CodeFile="waiterRanking.aspx.cs" Inherits="PointsManage_waiterRanking" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>服务员排名</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_WaiterRanking", "gv_OverRow");
            TabManage();
            var headControlHeight = $("#headControl").height();
            var tagMenuHeight = $("div.tagMenu").height();
            var layoutHeight = $(window).height() - headControlHeight - tagMenuHeight - 210;
            $("#mainFrame").height(layoutHeight);
        });
    </script>
</head>
<body onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <div id="box" class="box">
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table cellspacing="5">
                        <tr>
                            <td>
                                筛选条件（城市）：<asp:DropDownList ID="DropDownList_City" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList_City_SelectedIndexChanged">
                                    <asp:ListItem Text="全国" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="杭州" Value="87"></asp:ListItem>
                                    <asp:ListItem Text="上海" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="北京" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                手机号码：<asp:TextBox ID="txt_phoneNum" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                单笔订单金额：<asp:TextBox ID="txt_amount" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="button" Text="查 询" 
                                    onclick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table>
                        <tr>
                            <td style="vertical-align: middle">
                                排名周期：
                            </td>
                            <td>
                                <asp:Button ID="Button_Months" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="months" Text="上一月"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="Button_Week" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="week" Text="上一周"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="Button_UserDefined" runat="server" OnClick="Button_Time_Click" CssClass="tabButtonBlueUnClick"
                                    CommandName="userDefined" Text="自定义"></asp:Button>
                            </td>
                            <td>
                                <script type="text/javascript">
                                    var startDate = function (elem) {
                                        WdatePicker({
                                            el: elem,
                                            isShowClear: false,
                                            startDate: '2014-01-01',
                                            onpicked: function (dp) {
                                                elem.blur();
                                            }
                                        });
                                    };
                                    var endDate = function (elem) {
                                        WdatePicker({
                                            el: elem,
                                            isShowClear: false,
                                            startDate: new Date(),
                                            onpicked: function (dp) { elem.blur() }
                                        });
                                    }
                                </script>
                                <asp:TextBox ID="TextBox_TimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                    Enabled="false" AutoPostBack="true" Width="85px" OnTextChanged="TextBox_Time_TextChanged"></asp:TextBox>
                                &nbsp;-&nbsp;
                                <asp:TextBox ID="TextBox_TimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                    OnTextChanged="TextBox_Time_TextChanged" Enabled="false" AutoPostBack="true"
                                    Width="85px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <hr size="1" style="border: 1px #cccccc dashed;" />
                    <table>
                        <tr>
                            <td>
                                排序规则：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList_OrderBy" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList_OrderBy_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1">金额高到底</asp:ListItem>
                                    <asp:ListItem Value="2">金额低到高</asp:ListItem>
                                    <asp:ListItem Value="3">总积分高到低</asp:ListItem>
                                    <asp:ListItem Value="4">总积分低到高</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="Panel_WaiterRanking" runat="server" CssClass="div_gridview">
                    <asp:GridView ID="GridView_WaiterRanking" runat="server" DataKeyNames="EmployeeID,EmployeeFirstName"
                        AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowCommand="GridView_WaiterRanking_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="排名">
                                <ItemTemplate>
                                    <%# (this.AspNetPager_WaiterRanking.CurrentPageIndex - 1) * this.AspNetPager_WaiterRanking.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Name" ForeColor="Blue" runat="server" CommandName="Name"
                                        Text=""></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="手机" />
                            <asp:BoundField DataField="verifyAmount" HeaderText="验证金额" />
                            <asp:BoundField DataField="settlementPoint" HeaderText="获得积分" />
                            <asp:BoundField DataField="verifyCount" HeaderText="验证单数" />
                        </Columns>
                    </asp:GridView>
                    <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                        <div class="gridviewBottom_left">
                            <webdiyer:AspNetPager ID="AspNetPager_WaiterRanking" runat="server" FirstPageText="首页"
                                LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager_WaiterRanking_PageChanged">
                            </webdiyer:AspNetPager>
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel_Detail" runat="server" CssClass="div_gridview" Visible="false">
                    <div class="divDetailTitle">
                        <asp:Button ID="Button_back" runat="server" CssClass="couponButtonSubmit" Text="返     回"
                            OnClick="Button_back_Click" /></div>
                    <div id="divIframeContent" class="divDetailContent">
                        <iframe runat="server" frameborder="0" name="mainFrame" width="100%" id="mainFrame"
                            height="" src="" scrolling="auto"></iframe>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
