<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponManage.aspx.cs" Inherits="Coupon_CouponManage" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewCoupon", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table cellspacing="0" cellpadding="0" class="table" style="width: 100%;">

                            <tr>
                                <th>抵价券名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBoxName" Width="150px" runat="server"></asp:TextBox>
                                </td>
                                <th>状态：
                                </th>
                                <td>
                                    <asp:DropDownList ID="DropDownListState" runat="server">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="1">进行中</asp:ListItem>
                                        <asp:ListItem Value="0">失效（停止）</asp:ListItem>
                                        <asp:ListItem Value="-1">待审核</asp:ListItem>
                                        <asp:ListItem Value="-2">未通过</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <th>所属门店：</th>
                                <td>
                                    <asp:TextBox ID="TextBoxShopName" runat="server"></asp:TextBox></td>
                                <th>申请时间：</th>
                                <td>从
                                <asp:TextBox ID="TextBoxCreateTimeFrom" runat="server" CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>到
                                    <asp:TextBox ID="TextBoxCreateTimeTo" CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" runat="server"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <th>抵扣券类型:</th>
                                <td>
                                    <asp:DropDownList ID="DropDownListCouponType" runat="server">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="1">通用券</asp:ListItem>
                                        <asp:ListItem Value="2">运营数据</asp:ListItem>
                                        <asp:ListItem Value="3">会员营销券</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th>&nbsp;</th>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>

                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonSearch" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="查询" OnClick="ButtonSearch_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonAdd" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="新增" OnClientClick="window.location.href='CouponAdd.aspx';return false;" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonExportExcel" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="导出" OnClick="ButtonExportExcel_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button ID="ButtonRefuse" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="拒绝" OnClick="ButtonRefuse_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonAgree" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="通过" OnClick="ButtonAgree_Click" />
                                </td>
                            </tr>
                            <tr>

                                <td colspan="4" style="text-align: center">
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" style="width:auto;height:auto    ">
                                        <asp:GridView ID="GridViewCoupon" Width="1600px" runat="server" ViewStateMode="Enabled" DataKeyNames="CouponId" AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDataBound="GridViewCoupon_RowDataBound" OnRowCommand="GridViewCoupon_RowCommand">
                                            <Columns>
                                                <asp:TemplateField ShowHeader="True">
                                                    <HeaderTemplate>
                                                        复选
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="操作">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLinkDetail" NavigateUrl='<%# Eval("CouponId",@"~/Coupon/CouponDetail.aspx?CouponId={0}") %>' runat="server">查看详情</asp:HyperLink>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="LinkButtonStop" Visible="false" runat="server" CommandName="Stop" CommandArgument='<%# Eval("CouponId") %>'>停用</asp:LinkButton>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="LinkButtonRecovery" runat="server" Visible="false" CommandName="Recovery" CommandArgument='<%# Eval("CouponId") %>'>恢复使用</asp:LinkButton>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CreateTime" DataFormatString="{0:d}" HeaderText="申请时间" />
                                                <asp:TemplateField HeaderText="所在城市">                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelCity" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CouponName" HeaderText="活动名" />
                                                <asp:BoundField DataField="ShopName" HeaderText="申请门店" />
                                                <asp:BoundField DataField="RequirementMoney" DataFormatString="{0:C}元" HeaderText="条件金额" />
                                                <asp:BoundField DataField="DeductibleAmount" DataFormatString="{0:C}元" HeaderText="抵扣金额" />
                                                <asp:TemplateField HeaderText="活动时间">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelStartDate" runat="server"><%# Eval("StartDate","{0:d}") %></asp:Label>-<asp:Label ID="LabelEndDate" runat="server"><%# Eval("EndDate","{0:d}") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SheetNumber" HeaderText="数量" />
                                                <asp:TemplateField HeaderText="有效期">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ValidityPeriod") %>'></asp:Label>天
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="抵扣券类型">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelCouponType" runat="server" >活动数据</asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="状态">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelState" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="申请人">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelCreatedBy" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="审核人">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelAuditEmployee" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center" OnPageChanged="AspNetPager1_PageChanged">
                                        </webdiyer:AspNetPager>
                                    </div>
                    <asp:Panel ID="PanelRefuse" runat="server" CssClass="panelSyle">
                        <table>
                            <tr>
                                <th class="dialogBox_th">拒绝理由
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxRefuseReason" runat="server" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Button ID="ButtonOk" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="确定" OnClick="ButtonOk_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonCancle" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="取消" OnClientClick="HiddenConfirmWindow('PanelRefuse')" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
