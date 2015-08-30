<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackageStatistics.aspx.cs"
    Inherits="Package_PackageStatistics" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员营销统计</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             GridViewStyle("gdList", "gv_OverRow");
             TabManage();
         });
    </script>
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="会员营销统计" navigationUrl="" headName="会员营销统计" />
            <div class="content">
                    <div style="width: 80%">
                        <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th>
                                    商户名：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                                </td>
                                <th>
                                    发布时间：
                                </th>
                                <td>
                                     <asp:TextBox ID="tbBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 00:00:00'})"
                                        Width="130px"></asp:TextBox>至                                
                                    <asp:TextBox ID="tbEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd 23:59:59'})"
                                        Width="130px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>适用城市：</th>
                                <td>
                                     <asp:DropDownList ID="ddlCity" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                    background-color: White">
                                </div>
                                </td>
                                <th>
                                    营销套餐：
                                </th>
                                <td>
                                   <asp:DropDownList ID="ddlPackage" runat="server">
                                   </asp:DropDownList>
                                </td>
                                <td>
                                <asp:Button Width="130px" Height="33px" CssClass="couponButtonSubmit" ID="btnSearch" runat="server" Text="搜索"
                                     OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <hr size="1" style="border: 1px #cccccc dashed;" />
                       <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td  colspan="3" align="left">
                                    <asp:Label ID="Label_LargePageCount" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="Button_10" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_10"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="10"></asp:Button>
                        <asp:Button ID="Button_50" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_50"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="50"></asp:Button>
                        <asp:Button ID="Button_100" runat="server" CssClass="tabButtonBlueUnClick" CommandName="button_100"
                            OnClick="Button_LargePageCount_Click" Width="50px" Text="100"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                     <div class="div_gridview" id="div_gridview">
                                          <asp:GridView ID="gdList" runat="server" Width="100%" DataKeyNames="CouponId,shopName,SendTime,Name,Amount,CouponName"
                AllowSorting="True" AutoGenerateColumns="False" SkinID="gridviewSkin">
                <Columns>
                    <asp:BoundField DataField="shopName" HeaderText="商户名称" />
                    <asp:BoundField DataField="SendTime" HeaderText="发布时间"/>
                     <asp:BoundField DataField="ActivityName" HeaderText="活动名称"/>
                    <asp:BoundField DataField="Name" HeaderText="营销套餐"/>
                    <asp:BoundField DataField="Amount" HeaderText="付费金额"/>
                    <asp:TemplateField HeaderText="活动数据" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbtnView">查看</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                                          <label id="lbCount" runat="server" ></label>
                                         </div>
                                   
                                     <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                            <div class="gridviewBottom_left">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                    TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                    NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                    PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                                    MoreButtonType="Image" NavigationButtonType="Image" OnPageChanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                            </div>
                        </asp:Panel>
                    </div>
                                </td>
                            </tr>
                        </table>
                </div>
    </form>
</body>
</html>
