<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSystemShareCoupon.aspx.cs" Inherits="Coupon_AddSystemShareCoupon" %>
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
</head>
<body>
    <form id="form1" runat="server">
          <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="返回列表" navigationUrl="" headName="新建抵扣券二维码" />
    <div id="box" class="box">
        <div class="content">
            <div class="layout"> 
                    <table cellspacing="0" cellpadding="0" width="700px" class="table">
                        <tr>
                            <th style="width: 20%; text-align: center">
                                活动名称:
                            </th>
                            <td style="text-align: left; width: 95%">
                                &nbsp;<asp:TextBox ID="TextBoxName" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName"  ForeColor="Red" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>  
                        <tr>
                            <th style="width: 20%; text-align: center">
                                所属城市:
                            </th>
                            <td style="text-align: left; width: 95%">

                                <asp:DropDownList ID="DropDownListCity" runat="server" Width="200px" >
                                    <asp:ListItem Value="73">上海</asp:ListItem>
                                    <asp:ListItem Value="87">杭州</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>  
                        <tr>
                            <th style="width: 20%; text-align: center">
                                备注：</th>
                            <td style="text-align: left; width: 95%;">
                                
                                <asp:TextBox ID="TextBoxRemark" runat="server" Height="81px" Width="371px" TextMode="MultiLine" MaxLength="200" ></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr  >
                             
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
