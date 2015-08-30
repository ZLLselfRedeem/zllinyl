<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EvaluationStatic.aspx.cs" Inherits="ShopStatic_EvaluationStatic" %>

<!DOCTYPE html>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户评价</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewShopEvaluation", "gv_OverRow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="统计管理 - 用户评价" />
        <div id="box" class="box">
            <div class="content">
                <div class="layout">
                    <div class="layout">
                        <table class="table" style="width: 90%;">

                            <tr>
                                <th>类别：</th>
                                <td>
                                    <asp:DropDownList ID="DropDownEvaluationValue" runat="server" Width="150px">
                                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="-1">差评</asp:ListItem>
                                        <asp:ListItem Value="0">中评</asp:ListItem>
                                        <asp:ListItem Value="1">好评</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th>商户名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBoxShopName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>用户号码：</th>
                                <td>
                                    <asp:TextBox ID="TextBoxPhoneNumber" runat="server"></asp:TextBox>
                                </td>
                                <th>评价时间：
                                </th>
                                <td>从
                                    <asp:TextBox ID="TextBoxEvaluationTimeFrom" runat="server" CssClass="Wdate" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                    到
                                    <asp:TextBox ID="TextBoxEvaluationTimeTo" runat="server" CssClass="Wdate" onfocus="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查询" CssClass="button" OnClick="ButtonQuery_Click" />
                                </td>

                            </tr> 
                        </table>
                    </div>
                    <div class="div_gridview" style="width: 90%">
                        <asp:GridView ID="GridViewShopEvaluation" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                            ViewStateMode="Enabled" SkinID="gridviewSkin" OnRowDataBound="GridViewShopEvaluation_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNumber" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderText="商户名称">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelShopName" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="用户">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>(<asp:Label ID="LabelPhoneNumber" runat="server" Text='<%# Bind("MobilePhoneNumber") %>'></asp:Label>)
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="评价时间" ItemStyle-HorizontalAlign="Center" DataField="EvaluationTime" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="评价类型" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelEvaluationLevel" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="评价内容" ItemStyle-HorizontalAlign="Center" DataField="EvaluationContent">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" Wrap="true"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="liPage" CurrentPageButtonClass="currentButton" CurrentPageButtonPosition="Center" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" NumericButtonType="Text" PageIndexBoxClass="listPageText" PageSize="10" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonClass="listPageBtn" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
