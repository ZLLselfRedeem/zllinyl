<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackageDetail.aspx.cs"
    Inherits="Package_PackageDetail" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建礼券套餐</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 198px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="新建礼券套餐" navigationUrl="" headName="新建礼券套餐" />
      <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <th class="auto-style1">套餐名称:</th>
                                <td>
                                     <asp:TextBox ReadOnly="true" ID="tbName" runat="server" Width="413px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    套餐描述:
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDescription" runat="server" Width="413px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    位置选择:</th>
                                <td>
                                    <asp:RadioButton runat="server" GroupName="a" Text="本店" ID="rbThisShop" /> <asp:RadioButton runat="server" GroupName="a" Text="附近" ID="rbNearby" /><asp:TextBox ID="tbDistance" runat="server"></asp:TextBox>
                                    KM</td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    时间范围:</th>
                                <td>
                                    <asp:TextBox runat="server" ID="tbTimeRange"></asp:TextBox>天内
                                </td>
                            </tr>
                            <tr>
                                <th class="auto-style1">
                                    客单价要求:
                                </th>
                                <td colspan="7">
                                   <asp:CheckBox ID="cbISGuestUnitPrice" Text="金额范围" runat="server" AutoPostBack="True" OnCheckedChanged="cbISGuestUnitPrice_CheckedChanged" /><asp:TextBox ID="tbMinGuestUnitPrice" runat="server" ReadOnly="True"></asp:TextBox>至<asp:TextBox ID="tbMaxGuestUnitPrice" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th rowspan="2" class="auto-style1">
                                    套餐价格:
                                </th>
                                <td>
                                    <asp:RadioButton ID="rbStarted" Text="按次计费" GroupName="b" runat="server" /><asp:TextBox runat="server" ID="tbStarted"></asp:TextBox>次
                                </td>
                            </tr>
                             <tr>
                                  <td>
                                    <asp:RadioButton ID="rbByPerson" Text="按发送人次计费" GroupName="b" runat="server" /><asp:TextBox runat="server" ID="tbByPerson"></asp:TextBox>次
                                </td>
                             </tr>
                             <tr>
                                 <th class="auto-style1">
                                     套餐购买要求：
                                 </th>
                                 <td>
                                     <asp:DropDownList ID="ddlLevelRequirements" runat="server">
                                        <asp:ListItem Text="一星" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="二星" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三星" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="四星" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="五星" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="一钻" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="二钻" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="三钻" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="四钻" Value="24"></asp:ListItem>
                                        <asp:ListItem Text="五钻" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="一冠" Value="36"></asp:ListItem>
                                        <asp:ListItem Text="二冠" Value="72"></asp:ListItem>
                                        <asp:ListItem Text="三冠" Value="108"></asp:ListItem>
                                        <asp:ListItem Text="四冠" Value="144"></asp:ListItem>
                                        <asp:ListItem Text="五冠" Value="180"></asp:ListItem>
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr>
                                 <th class="auto-style1">
                                     允许商户筛选点过的菜：
                                 </th>
                                 <td>
                                     <asp:RadioButton ID="rbYes" runat="server" GroupName="c" Text="是" /><asp:RadioButton ID="rbNo" runat="server" GroupName="c" Text="否" />
                                 </td>
                             </tr>
                             <tr>
                                 <th class="auto-style1">
                                     每次使用间隔：
                                 </th>
                                 <td>
                                     <asp:TextBox runat="server" Text="0" ID="tbSendLnterval" Width="66px"></asp:TextBox>天(允许输入小数)
                                 </td>
                             </tr>
                             <tr>
                                 <th class="auto-style1">
                                     套餐状态：
                                 </th>
                                 <td>
                                     <asp:DropDownList ID="ddlStatus" runat="server">
                                         <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="停用" Value="0"></asp:ListItem>
                                     </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr>
                                 <th class="auto-style1">
                                     适用城市
                                 </th>
                                 <td>
                                     <asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList>
                                 </td>
                             </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button Text="修 改" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" />
                               
                                    &nbsp;&nbsp;&nbsp;
                               
                                    <asp:Button Text="返 回" Width="130px" Height="33px" CssClass="couponButtonSubmit" runat="server" ID="btnBack" OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
    </form>
</body>
</html>
