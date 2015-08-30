<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerList.aspx.cs" Inherits="Customer_CustomerList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridViewCustomer", "gv_OverRow");
        });
    </script>
    <style type="text/css">
        .countStyle
        {
            color: Red;
        }
        .style1
        {
            width: 250px;
        }
        .style2
        {
            width: 200px;
        }
    </style>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="客户列表" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>客户列表</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td style="vertical-align: middle">
                                注册时间：
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_TimeStr" runat="server" CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd '})"
                                    Width="85px"></asp:TextBox>&nbsp;-&nbsp;<asp:TextBox ID="TextBox_TimeEnd" runat="server"
                                        CssClass="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd '})" Width="85px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;用户数量&nbsp;&nbsp;<asp:Label ID="Label_count" CssClass="countStyle"
                                    runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 700px; height: 39px;">
                        <tr>
                            <td class="style2">
                                用户手机：<asp:TextBox ID="TextBox_mobilePhoneNumber" runat="server" Width="100"></asp:TextBox>
                            </td>
                            <td class="style1">
                                Email：<asp:TextBox ID="TextBox_customerEmail" runat="server" Width="180"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Button_Check" runat="server" Text="查   询" CssClass="button" OnClick="Button_Check_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_gridview">
                    <asp:GridView ID="GridViewCustomer" runat="server" DataKeyNames="CustomerID,cookie"
                        AutoGenerateColumns="False" CssClass="gridview" OnRowDeleting="GridViewCustomer_RowDeleting"
                        OnSelectedIndexChanged="GridViewCustomer_SelectedIndexChanged" SkinID="gridviewSkin"
                        OnRowCommand="GridViewCustomer_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="用户名" />
                            <asp:BoundField DataField="mobilePhoneNumber" HeaderText="电话" />
                            <asp:BoundField DataField="customerEmail" HeaderText="Email" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="Select" Text="查看详情"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" Visible="false">
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton_ShowMoney19dianDetail"
                                        runat="server" CausesValidation="False" CommandName="ShowMoney19dianDetail" Text="查看奖励"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton3"
                                        runat="server" CausesValidation="False" CommandName="ChangePassword" Text="重置密码"
                                        OnClientClick="return confirm('确定重置密码为123456吗？')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" Visible="false">
                                <ItemTemplate>
                                    <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                        CausesValidation="False" CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                            CenterCurrentPageButton="True" MoreButtonType="Text">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
