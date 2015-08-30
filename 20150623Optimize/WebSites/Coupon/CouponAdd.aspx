﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponAdd.aspx.cs" Inherits="Coupon_CouponAdd" %>

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
        function oncbl(Control) {
            if (Control.checked) {
                document.getElementById("txtBeginTime").style.color = "white";
                document.getElementById("txtEndTime").style.color = "white";
            }
            else {
                document.getElementById("txtBeginTime").style.color = "#3F6293";
                document.getElementById("txtEndTime").style.color = "#3F6293";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="返回列表" navigationUrl="" headName="优惠券添加" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <table cellspacing="0" cellpadding="0" width="700px" class="table">
                        <tr>
                            <th style="width: 21%; text-align: center">活动名称:
                            </th>
                            <td style="text-align: left; width: 95%">&nbsp;<asp:TextBox ID="TextBoxName" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName" ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">活动日期:
                            </th>
                            <td style="text-align: left; width: 95%">从<asp:TextBox ID="TextBoxStartDate" runat="server" CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>至
                                <asp:TextBox ID="TextBoxEndDate" runat="server" CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxStartDate" ControlToValidate="TextBoxEndDate" Display="Dynamic" ErrorMessage="截止日期不能小于起始日期" ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                抵扣券类型：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlCouponType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCouponType_SelectedIndexChanged">
                                    <asp:ListItem Text="通用券" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="会员营销券" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">条件金额:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBoxRequirementMoney" runat="server"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBoxRequirementMoney" ErrorMessage="请检查内容正确性" ForeColor="Red" Display="Dynamic" Type="Double" MaximumValue="9999" MinimumValue="0.01"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxRequirementMoney" Display="Dynamic" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">抵用金额:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxDeductibleAmount" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxDeductibleAmount" Display="Dynamic" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="TextBoxDeductibleAmount" ErrorMessage="请检查内容正确性" ForeColor="Red" Type="Double" MaximumValue="9999" MinimumValue="0.01"></asp:RangeValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="TextBoxRequirementMoney" ControlToValidate="TextBoxDeductibleAmount" Display="Dynamic" ErrorMessage="抵用金额不能大于条件金额" ForeColor="Red" Operator="LessThan" Type="Double"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">最多减：
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxMaxAmount" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMaxAmount" runat="server" ControlToValidate="TextBoxMaxAmount" Display="Dynamic" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidatorMaxAmount" runat="server" ControlToValidate="TextBoxMaxAmount" ErrorMessage="请检查内容正确性" ForeColor="Red" Type="Double" MinimumValue="0"></asp:RangeValidator>
                            </td>
                        </tr>
                        <!--tr>
                            <th style="width: 21%; text-align: center">
                                使用限制：
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:DropDownList runat="server" ID="ddlIsGeneralHolidays">
                                    <asp:ListItem Value="0">无限制</asp:ListItem>
                                    <asp:ListItem Value="1">仅工作日可用</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">
                                每日使用时间：
                            </th>
                            <td style="text-align: left; width: 95%">
                                从<asp:TextBox ID="txtBeginTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'HH:mm:ss'})"></asp:TextBox>至<asp:TextBox ID="txtEndTime" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'HH:mm:ss'})"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <input id="checkAllDay" runat="server" type="checkbox" onclick="oncbl(this)" />可全天使用</td>
                        </tr-->
                        <tr>
                            <th style="width: 21%; text-align: center">补贴金额:</th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxSubsidyAmount" runat="server">0.00</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubsidyAmount" runat="server" ControlToValidate="TextBoxSubsidyAmount" Display="Dynamic" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>

                                <asp:RangeValidator ID="RangeValidatorSubsidyAmount" runat="server" ControlToValidate="TextBoxSubsidyAmount" ErrorMessage="补贴金额必须为正数" ForeColor="Red" Type="Double" MaximumValue="9999" MinimumValue="0"></asp:RangeValidator>

                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">数量：</th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxSheetNumber" runat="server"></asp:TextBox>
                                份
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxSheetNumber" Display="Dynamic" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="TextBoxSheetNumber" ErrorMessage="请检查内容正确性" ForeColor="Red" Type="Integer" MaximumValue="9999" MinimumValue="1"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">有效期：</th>

                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxValidityPeriod" runat="server"></asp:TextBox>天
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxValidityPeriod" ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="TextBoxValidityPeriod" ErrorMessage="请检查内容正确性" ForeColor="Red" Display="Dynamic" Type="Integer" MaximumValue="9999" MinimumValue="1"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">所属门店:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBoxShopName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="ButtonSearch" runat="server" CssClass="tabButtonBlueClick" CausesValidation="false" Text="查询" OnClick="ButtonSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">&nbsp;</th>
                            <td style="text-align: left; width: 95%;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="RadioButtonListShop" ForeColor="Red" Display="Dynamic" ErrorMessage="请选择正确的门店"></asp:RequiredFieldValidator>

                                <asp:RadioButtonList ID="RadioButtonListShop" runat="server" DataTextField="shopName" DataValueField="shopID" RepeatDirection="Vertical" RepeatColumns="4">
                                </asp:RadioButtonList>


                            </td>
                        </tr>
                        <tr>
                            <th style="width: 21%; text-align: center">备注：</th>
                            <td style="text-align: left; width: 95%;">

                                <asp:TextBox ID="TextBoxRemark" runat="server" Height="81px" Width="371px" TextMode="MultiLine" MaxLength="200"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>

                            <td style="text-align: center; width: 95%;" colspan="2">
                                <asp:Button ID="ButtonCancel" runat="server" CssClass="tabButtonBlueClick" Text="返回" OnClientClick="window.history.go(-2);return false;" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="ButtonAdd" runat="server" CssClass="tabButtonBlueClick" Text="添加" OnClick="ButtonAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
