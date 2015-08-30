<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgeCouponAdd.aspx.cs" Inherits="Coupon_ForgeCouponAdd" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=xIaOmBpthTUf8zF1WurZyBkU"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="返回列表" navigationUrl="" headName="抵扣券运营数据添加" />
     <div id="box" class="box">
        <div class="content">
            <div class="layout"> 
                    <table cellspacing="0" cellpadding="0" width="700px" class="table">
                        <tr>
                            <th style="width: 20%; text-align: center">
                                活动名称:
                            </th>
                            <td style="text-align: left; width: 95%">
                                &nbsp;<asp:TextBox ID="TextBoxName" runat="server" Width="199px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName"  ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                所属城市:</th>
                            <td style="text-align: left; width: 95%">
                            <asp:DropDownList ID="ddlCity" Width="200px" runat="server"> 
                                 <asp:ListItem Value="87">杭州市</asp:ListItem>
                                 <asp:ListItem Value="73">上海市</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                所属门店:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxShopName" Width="200px" MaxLength="20" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxShopName"  ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                活动日期:
                            </th>
                            <td style="text-align: left; width: 95%">
                                从<asp:TextBox ID="TextBoxStartDate" runat="server"  CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" ></asp:TextBox>至
                                <asp:TextBox ID="TextBoxEndDate" runat="server"  CssClass="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" ></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxStartDate" ControlToValidate="TextBoxEndDate" Display="Dynamic" ErrorMessage="截止日期不能小于起始日期" ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                条件金额:
                            </th>
                            <td style="text-align: left; width: 95%;">
                                <asp:TextBox ID="TextBoxRequirementMoney" runat="server"></asp:TextBox>
                                 <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBoxRequirementMoney" ErrorMessage="*" ForeColor="Red" Display="Dynamic"  Type="Double" MinimumValue="0"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxRequirementMoney" Display="Dynamic" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">
                                抵用金额:
                            </th>
                            <td style="text-align: left; width: 95%">
                                <asp:TextBox ID="TextBoxDeductibleAmount" runat="server" ></asp:TextBox> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxDeductibleAmount" Display="Dynamic"  ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                 <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="TextBoxDeductibleAmount" ErrorMessage="*" ForeColor="Red" Type="Double" MinimumValue="0"></asp:RangeValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="TextBoxRequirementMoney" ControlToValidate="TextBoxDeductibleAmount" Display="Dynamic" ErrorMessage="抵用金额不能大于条件金额" ForeColor="Red" Operator="LessThan" Type="Double"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr >
                            <th style="width: 20%; text-align: center">领取数量：</th>
                            <td style="text-align: left; width: 95%"> <asp:TextBox ID="TextBoxSheetNumber" runat="server" ></asp:TextBox> &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxSheetNumber" Display="Dynamic"  ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                 <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="TextBoxSheetNumber" ErrorMessage="*" ForeColor="Red" Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 20%; text-align: center">门店地址：</th>

                            <td style="text-align: left; width: 95%"> <asp:TextBox ID="TextBoxShopAddress" runat="server" Width="371px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxShopAddress"  ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator> 
                            </td>
                        </tr> 
                        <tr>
                            <th style="width: 20%; text-align: center">
                                门脸图:</th>
                            <td style="text-align: left; width: 95%;">
                                &nbsp;&nbsp;&nbsp;
                                <asp:FileUpload ID="fileUpload" runat="server" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="ButtonUpload" runat="server" CssClass="tabButtonBlueClick"  CausesValidation="false"  Text="上传" OnClick="ButtonUpload_Click" />
                                <br />
                                            <asp:Image ID="Big_Img" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="320px"
                                                Height="110px" CssClass="innerTD" />
                            </td>
                        </tr>
                        <tr  >
                            <th style="width: 20%; text-align: center">
                                备注：</th>
                            <td style="text-align: left; width: 95%;">
                                
                                <asp:TextBox ID="TextBoxRemark" runat="server" Height="81px" Width="371px" TextMode="MultiLine" MaxLength="200" ></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr  >
                             
                            <td style="text-align: center; width: 95%;" colspan="2">
                                 <asp:Button ID="ButtonCancel" runat="server" CssClass="tabButtonBlueClick" Text="返回" OnClientClick="window.history.back();return false;" OnClick="ButtonCancel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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

