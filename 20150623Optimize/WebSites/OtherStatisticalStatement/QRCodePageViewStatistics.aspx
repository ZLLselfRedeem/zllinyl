<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodePageViewStatistics.aspx.cs"
    Inherits="OtherStatisticalStatement_QRCodePageViewStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>下载页访问统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="QueryTerms">
            <table cellspacing="5">
                <tr>
                    <td>
                        类别：<asp:DropDownList ID="DropDownList_Type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Type_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        公司：<asp:DropDownList ID="DropDownList_Company" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList_Company_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        门店：<asp:DropDownList ID="DropDownList_Shop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Shop_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button_Pay" CssClass="couponButtonSubmit"
                            Width="130px" runat="server" Text="访问量统计" OnClick="Button_QRCodePV_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel_PayStatistics_Table" runat="server">
                <hr size="1" style="border: 1px #cccccc dashed;" />
                <table>
                    <tr>
                        <td style="vertical-align: middle">
                            统计周期：
                        </td>
                        <td>
                            <asp:Button ID="Button_1day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="1"
                                Text="今天" OnClick="Button_day_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_yesterday" runat="server" CssClass="tabButtonBlueUnClick"
                                OnClick="Button_day_Click" CommandName="yesterday" Text="昨天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_7day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="7"
                                OnClick="Button_day_Click" Text="最近7天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_14day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="14"
                                OnClick="Button_day_Click" Text="最近14天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_30day" runat="server" CssClass="tabButtonBlueUnClick" CommandName="30"
                                OnClick="Button_day_Click" Text="最近30天"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="Button_self" runat="server" CssClass="tabButtonBlueUnClick" CommandName="self"
                                OnClick="Button_day_Click" Text="自定义"></asp:Button>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_visitTimeStr" runat="server" CssClass="Wdate" onFocus="startDate(this)"
                                OnTextChanged="TextBox_visitTimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="TextBox_visitTimeEnd" runat="server" CssClass="Wdate" onFocus="endDate(this)"
                                OnTextChanged="TextBox_visitTimeStr_TextChanged" AutoPostBack="true" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="text-align: right">
                    <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                        OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                    <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                        OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                    <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                        OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel_QRCodePV" runat="server">
            <div class="div_gridview" id="div_gridview">
                <asp:GridView ID="GridView_QRCodePV" runat="server" AutoGenerateColumns="False" SkinID="gridviewSkin">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="类别" />
                        <asp:BoundField DataField="companyName" HeaderText="公司名称" />
                        <asp:BoundField DataField="shopName" HeaderText="店铺名称" />
                        <asp:BoundField DataField="visitTime" HeaderText="访问时间" />
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="Panel1" CssClass="gridviewBottom" runat="server">
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                    <div class="gridviewBottom_right">
                        <table>
                            <tr>
                                <th>
                                    共
                                </th>
                                <td>
                                    <asp:Label ID="Label_OrderCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    单
                                </th>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <asp:Label ID="Label_massage" runat="server" CssClass="Red" Text=""></asp:Label>
        </asp:Panel>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_QRCodePV", "gv_OverRow");
        });
        var startDate = function (elem) {
            WdatePicker({
                el: elem,
                isShowClear: false,
                maxDate: '#F{$dp.$D(\'TextBox_visitTimeEnd\')||%y-%M-%d}',
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
                maxDate: '%y-%M-{%d+1}',
                minDate: '#F{$dp.$D(\'TextBox_visitTimeStr\')}',
                onpicked: function (dp) { elem.blur() },
                skin: 'whyGreen'
            });
        }
    </script>
</body>
</html>
