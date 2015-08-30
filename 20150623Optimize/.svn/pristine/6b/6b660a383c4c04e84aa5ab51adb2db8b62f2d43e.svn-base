<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodDiaryDefaultConfigDishAdd.aspx.cs" Inherits="Shared_FoodDiaryDefaultConfigDishAdd" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="美食日记缺省菜品图片配置" navigationImage="~/images/icon/list.gif"
            navigationText="美食日记缺省菜品图片管理" navigationUrl="~/Shared/FoodDiaryDefaultConfigDishManage.aspx" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>基本信息</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>菜品名称：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox_DishName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <asp:FileUpload ID="ImageName_File" runat="server" onchange="Text_SelectFile.value=this.value"
                                    CssClass="fileUpload" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Image ID="ImageName_Img" runat="server" ImageUrl="~/Images/bigimage.jpg" Width="300px"
                                    Height="225px" />
                            </td>
                        </tr>
                        <tr>
                            <th>&nbsp;
                            </th>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="添    加" CssClass="button" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
         <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
