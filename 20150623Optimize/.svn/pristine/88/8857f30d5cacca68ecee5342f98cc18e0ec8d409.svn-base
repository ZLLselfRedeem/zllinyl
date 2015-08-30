<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeFile="FoodDiaryDefaultConfigDishManage.aspx.cs" Inherits="Shared_FoodDiaryDefaultConfigDishManage" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Import Namespace="VAGastronomistMobileApp.Model" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="美食日记缺省菜品列表" navigationImage="~/images/icon/list.gif"
            navigationText="美食日记缺省菜品图片管理" navigationUrl="~/Shared/FoodDiaryDefaultConfigDishManage.aspx" />
        <asp:ListView ID="DishView" AllowPaging="true"
            GroupItemCount="4" 
            runat="server" OnItemCommand="DishView_ItemCommand" OnDataBound="DishView_DataBound" OnItemDeleting="DishView_OnItemDeleting">
            <LayoutTemplate>
                <table cellpadding="2" width="100%" id="tbl1" runat="server">
                    <tr runat="server" id="groupPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr runat="server" id="tr1">
                    <td runat="server" id="itemPlaceholder"></td>
                </tr>
            </GroupTemplate>
            <GroupSeparatorTemplate>
                <tr runat="server">
                    <td colspan="7">
                        <div class="groupSeparator">
                            <hr>
                        </div>
                    </td>
                </tr>
            </GroupSeparatorTemplate>
            <ItemTemplate>
                <td align="center" runat="server">
                    <%# Eval("DishName") %><br />
                    <asp:Image ID="DishImage" runat="server" Width="200" Height="150"
                        ImageUrl='<%# Eval("ImageName") %>' /><br />
                    <asp:Button ID="deleteButton" Text="删除" CommandArgument='<%# Eval("Id") %>' CommandName="delete" runat="server" />
                </td>
            </ItemTemplate>
            <ItemSeparatorTemplate>
                <td class="itemSeparator" runat="server">&nbsp;</td>
            </ItemSeparatorTemplate>
        </asp:ListView>

        <asp:DataPager ID="datapager1" runat="server" PagedControlID="DishView" 
            PageSize="20" >
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link"
                    ShowFirstPageButton="true"
                    ShowNextPageButton="false"
                    ShowPreviousPageButton="false"
                    FirstPageText="首页" LastPageText="尾页" />
                <asp:NumericPagerField ButtonCount="5" />
                <asp:NextPreviousPagerField ButtonType="Link"
                    ShowLastPageButton="true"
                    ShowNextPageButton="false"
                    ShowPreviousPageButton="false"
                   FirstPageText="首页" LastPageText="尾页" />
            </Fields>
        </asp:DataPager>
         <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
