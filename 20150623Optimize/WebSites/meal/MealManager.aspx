<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MealManager.aspx.cs" Inherits="Meal_MealManager" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广告管理</title>
     <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <style type="text/css">
        .innerTD
        {
            margin: 0 auto;
        }
        .tblBorder
        {
            border: 1px solid #c0c0c0;
        }
        .tblBorder td
        {
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
         headName="套餐维护" />
    <div>
     <div class="content">
            <div class="layout"> 
                    <table cellspacing="0" cellpadding="0" width="100%" class="table">
                        <tr>
                            <th style="width: 15%; text-align: center">
                                套餐名称:
                            </th>
                            <td style="text-align: left; width: 25%">
                                <asp:TextBox ID="TextBoxMealName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                             <th style="width: 15%; text-align: center">
                                 状态：</th>
                            <td style="text-align: left; width: 25%">
                                <asp:DropDownList ID="DropDownListIsActive" runat="server"> 
                                    <asp:ListItem>--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">已上架</asp:ListItem>
                                    <asp:ListItem Value="0">未上架</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        <tr>
                            <th style="width: 15%; text-align: center">
                                所属公司：</th>
                            <td style="text-align: left; width: 25%">
                                <asp:DropDownList ID="DropDownListCompany" DataTextField="companyName" DataValueField="companyID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <th style="width: 15%; text-align: center">
                                所属门店:
                            </th>
                            <td style="text-align: left; width: 25%">
                                <asp:DropDownList ID="DropDownListShop" DataTextField="shopName" DataValueField="shopID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        <tr>
                            <th style="width: 15%; text-align: center">
                                所属城市：</th>
                            <td style="text-align: left; width: 25%">
                                <asp:DropDownList ID="DropDownListCity" runat="server">
                                    <asp:ListItem>--请选择--</asp:ListItem>
                                    <asp:ListItem Value="87">杭州</asp:ListItem>
                                    <asp:ListItem Value="73">上海</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <th style="width: 15%; text-align: center">
                                提交时间：</th>
                            <td style="text-align: left; width: 25%">
                                从<asp:TextBox ID="TextBoxCreatedFrom" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"  runat="server"></asp:TextBox>
                                到<asp:TextBox ID="TextBoxCreatedTo" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"  runat="server"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr>
                             
                            <td style="text-align: center; width: 25%" colspan="4">

                                <asp:Button ID="ButtonQuery" runat="server" class="couponButtonSubmit"   Text="查询" OnClick="ButtonQuery_Click"   />

                               </td>
                            
                        </tr> 
                        <tr>
                           
                            <td colspan="4" style="text-align: center;  ">
                                <div class="div_gridview"  >
                                    <asp:GridView ID="GridViewMeal" runat="server" Width="95%" SkinID="gridviewSkin" AutoGenerateColumns="False" OnRowDataBound="GridViewMeal_RowDataBound"
                                         EmptyDataText="未查询到数据，请重新选择条件！"   ShowHeaderWhenEmpty="true">
                                        <Columns>


                                            <asp:BoundField DataField="MealName" HeaderText="套餐名称" />
                                            <asp:BoundField DataField="Price" ItemStyle-HorizontalAlign="Right" HeaderText="价格" DataFormatString="¥{0:N2}">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderNumber" HeaderText="套餐序号" />
                                            <asp:TemplateField HeaderText="套餐状态" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelIsActive" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="companyName" HeaderText="所属公司" />
                                            <asp:BoundField DataField="shopName" HeaderText="所属门店" />
                                            <asp:TemplateField HeaderText="门店状态" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelShopState" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="服务经理" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelCreator" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CreationDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="提交时间" />
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLinkEdit" runat="server" NavigateUrl='<%# Eval("MealID", "MealAdd.aspx?MealID={0}") %>' Text="编辑"></asp:HyperLink>
                                                    <asp:HyperLink ID="HyperLinkMealScheduling" runat="server" NavigateUrl='<%# Eval("MealID", "MealScheduling.aspx?MealID={0}") %>' Text="排期"></asp:HyperLink>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <EmptyDataRowStyle ForeColor="Black" />

                                    </asp:GridView>
                                </div>
                            </td> 
                        </tr> 
                        <tr>

                            <td colspan="4" style="text-align: center;">
                                <webdiyer:AspNetPager ID="AspNetPagerMeal" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            OnPageChanged="AspNetPagerMeal_PageChanged"></webdiyer:AspNetPager>
                            </td>
                        </tr> 
                        </table>
                </div>
         </div>
    </div>
    </form>
</body>
</html>
