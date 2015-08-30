﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponFinancialStatistics.aspx.cs" Inherits="Coupon_CouponFinancialStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
            navigationText="返回列表" navigationUrl="" headName="财务对账门店统计" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table cellspacing="0" cellpadding="0" class="table" style="width: 100%;">

                            <tr>
                                <th style=" width:15%">门店名称：
                                </th>
                                <td style=" width:35%">
                                    <asp:TextBox ID="TextBoxName" Width="150px" runat="server"></asp:TextBox>
                                </td>
                                <th style=" width:15%">所属城市： </th>
                                <td style=" width:35%">
                                    <asp:DropDownList ID="DropDownListCity" runat="server">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="87">杭州</asp:ListItem>
                                        <asp:ListItem Value="73">上海</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <th>使用时间：</th>
                                <td>从
                                <asp:TextBox ID="TextBoxUseTimeFrom" runat="server" CssClass="Wdate"   > </asp:TextBox>到
                                    <asp:TextBox ID="TextBoxUseTimeTo" CssClass="Wdate"  runat="server"></asp:TextBox></td>

                                <th></th>
                                <td></td>
                            </tr>

                            <tr>

                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonSearch" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="查询" OnClick="ButtonSearch_Click"  />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ButtonExportExcel" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="导出" OnClick="ButtonExportExcel_Click"   />
                                </td>
                            </tr>
                            <tr>

                                <td colspan="4" style="text-align: center"></td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_gridview" style="width: auto; height: auto">
                        <asp:GridView ID="GridViewCoupon"  Width="1500px"  runat="server" ViewStateMode="Enabled" DataKeyNames="CouponId" AutoGenerateColumns="False" SkinID="gridviewSkin" OnRowDataBound="GridViewCoupon_RowDataBound"  >
                            <Columns>
                                <asp:TemplateField HeaderText="城市" ItemStyle-Width="65px">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelCity" runat="server"></asp:Label>
                                    </ItemTemplate>

<ItemStyle Width="65px"></ItemStyle>
                                </asp:TemplateField> 
                                <asp:BoundField DataField="ShopName" HeaderText="申请门店" /> 
                                <asp:TemplateField HeaderText="开户行"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBankName" runat="server" >-</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="开户名"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelAccountName" runat="server"  >-</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="帐号"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelAccount" runat="server"  >-</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客户经理"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelAccountManager" runat="server"  >-</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="活动内容"> 
                                    <ItemTemplate>
                                        满<asp:Label ID="LabelRequirementMoney" runat="server" Text='<%# Bind("RequirementMoney" ) %>'></asp:Label>减<asp:Label ID="LabelDeductibleAmount" runat="server" Text='<%# Bind("DeductibleAmount" ) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="领取数"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelGetCount" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="使用数"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelUseCount" runat="server"  ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:BoundField DataField="SubsidyAmount" DataFormatString="{0:C}" HeaderText="单笔补贴" />
                                <asp:TemplateField HeaderText="补贴总额"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelTotalSubsidyAmount" runat="server"  >-</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="活动时间">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelStartDate" runat="server"><%# Eval("StartDate","{0:yyyy/MM/dd}") %></asp:Label>-<asp:Label ID="LabelEndDate" runat="server"><%# Eval("EndDate","{0:yyyy/MM/dd}") %></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center" OnPageChanged="AspNetPager1_PageChanged"  >
                        </webdiyer:AspNetPager>
                        共<asp:Label ForeColor="Red"  ID="LabelShopCount" runat="server"  ></asp:Label>个商户参与活动&nbsp;&nbsp;&nbsp;&nbsp;
                        领取数<asp:Label ForeColor="Red"  ID="LabelGetCount" runat="server"  ></asp:Label>张&nbsp;&nbsp; 
                        使用数<asp:Label ForeColor="Red"  ID="LabelUseCount" runat="server"  ></asp:Label>张&nbsp;&nbsp;&nbsp; 
                        补贴总额<asp:Label ForeColor="Red"  ID="LabelTotalSubsidyAmount" runat="server"  ></asp:Label>
                    </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
