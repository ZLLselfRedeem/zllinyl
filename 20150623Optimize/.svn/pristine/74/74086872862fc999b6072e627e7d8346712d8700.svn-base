<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MealScheduling.aspx.cs" Inherits="Meal_MealScheduling" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>排期管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .innerTD {
            margin: 0 auto;
        }

        .tblBorder {
            border: 1px solid #c0c0c0;
        }

            .tblBorder td {
                border: 1px solid #c0c0c0;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewMeal", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="返回列表" navigationUrl="MealManager.aspx" headName="套餐排期" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <table cellspacing="0" cellpadding="0" width="900px" class="table">
                        <tr>
                            <th style="width: 15%; text-align: center">套餐名称:
                            </th>
                            <td style="text-align: left; width: 25%">
                                <asp:Label ID="LabelMealName" runat="server" Text=""></asp:Label>
                            </td>
                            <th style="width: 15%; text-align: center">状态：</th>
                            <td style="text-align: left; width: 25%">
                                <asp:Label ID="LabelIsActive" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 15%; text-align: center">所属公司：</th>
                            <td style="text-align: left; width: 25%">
                                <asp:Label ID="LabelCompany" runat="server" Text=""></asp:Label>
                            </td>
                            <th style="width: 15%; text-align: center">所属门店:
                            </th>
                            <td style="text-align: left; width: 25%">
                                <asp:Label ID="LabelShop" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 15%; text-align: center">服务经理：</th>
                            <td style="text-align: left; width: 25%">
                                <asp:Label ID="LabelCreator" runat="server" Text=""></asp:Label>
                            </td>
                            <th style="width: 15%; text-align: center">&nbsp;</th>
                            <td style="text-align: left; width: 25%">
                                &nbsp;</td>
                        </tr>
                        <tr>

                            <td style="text-align: center; width: 25%" colspan="4">&nbsp;</td>

                        </tr>
                        <tr>

                            <td colspan="4" style="text-align: center;">
                                <div class="div_gridview">
                                    <asp:GridView ID="GridViewMeal" HeaderStyle-HorizontalAlign="Center" runat="server" Width="95%" SkinID="gridviewSkin"
                                         EmptyDataText="该套餐未排期，请新增排期！"   ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="GridViewMeal_RowDataBound" OnRowCommand="GridViewMeal_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="DinnerTime" DataFormatString="{0:yyyy-MM-dd H:mm}" HeaderText="就餐日期" />
                                            <asp:TemplateField HeaderText="就餐时段">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelDinnerType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ValidTo" DataFormatString="{0:yyyy-MM-dd H:mm}" HeaderText="预定截止时间" />
                                            <asp:BoundField DataField="SoldCount" HeaderText="已售份数" />
                                            <asp:BoundField DataField="TotalCount" HeaderText="套餐份数" />
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButtonEdit" CommandArgument='<%# Eval("MealScheduleID")%>' CommandName="E" runat="server">编辑</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" style="text-align: left;">
                                <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">新增排期</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" style="text-align: center;">&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
       
        <asp:Panel ID="Panel_Window" runat="server"   CssClass="panelSyle">
             <div class="content">
            <div class="layout">
                <table cellspacing="0" cellpadding="0" width="900px" class="table">
                    <tr>
                        <th style="width: 15%; text-align: center">就餐日期:
                        </th>
                        <td style="text-align: left; width: 25%">
                            <asp:TextBox ID="TextBoxDinnerTime" ReadOnly="true" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd  H:mm'})" ></asp:TextBox>
                        </td>
                        <th style="width: 15%; text-align: center">就餐时段：</th>
                        <td style="text-align: left; width: 25%">
                            <asp:DropDownList ID="DropDownListDinnerType" runat="server">
                                <asp:ListItem Selected="True" Value="1">中餐</asp:ListItem>
                                <asp:ListItem Value="2">晚餐</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 15%; text-align: center">预定截止时间：</th>
                        <td style="text-align: left; width: 25%">
                            <asp:TextBox ID="TextBoxValidTo" ReadOnly="true"  CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd  H:mm'})"  runat="server"></asp:TextBox>
                        </td>
                        <th style="width: 15%; text-align: center">总份数:
                        </th>
                        <td style="text-align: left; width: 25%">

                            <asp:TextBox ID="TextBoxTotalCount" runat="server" ></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4">
                            <asp:Button ID="ButtonAdd" Visible="false" class="couponButtonSubmit" OnClick="ButtonAdd_Click" runat="server" Text="新增" />
                            <asp:Button ID="ButtonUpdate" runat="server" class="couponButtonSubmit" Text="更新" OnClick="ButtonUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonCancel" runat="server" class="couponButtonSubmit" Text="返回" OnClick="ButtonCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        </asp:Panel>
    </form>
</body>
</html>
