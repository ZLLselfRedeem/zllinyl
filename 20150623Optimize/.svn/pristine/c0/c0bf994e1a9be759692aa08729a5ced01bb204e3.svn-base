<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeRoleManage.aspx.cs"
    Inherits="AuthorizationManagement_EmployeeRoleManage" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户分配角色</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView1", "gv_OverRow");
            GridViewStyle("GridView_Role", "gv_OverRow");
            GridViewStyle("GridView_EmployeeRole", "gv_OverRow");
            $("#div_content").css({ "height": $(window).height() - $("#div_gridview").height() - 150 });
        });       
    </script>
    <style type="text/css">
        .roleTable
        {
            width: 100%;
        }
        .roleTable th
        {
            font-weight: bold;
            font-size: 15px;
            background-color: #518BCB;
            color: White;
            height: 30px;
        }
        .roleTable td
        {
            vertical-align: top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="用户分配角色" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>用户分配角色</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                用户名或姓名：
                                <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Button_QueryEmplooer" runat="server" CssClass="button" Text="查 询"
                                    OnClick="Button_QueryEmplooer_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_gridview" id="div_gridview">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="EmployeeID" AutoGenerateColumns="False"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" SkinID="gridviewSkin">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="用户名" />
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:Label ID="Label_Name" runat="server" Text='<%#Bind("EmployeeFirstName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" SelectText="查看角色" />
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server" Visible="false" CssClass="div_gridview">
                    <table class="roleTable" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="xian" style="width: 50%; text-align: left;">
                                &nbsp;&nbsp;全部角色：
                            </th>
                            <th style="width: 10px">
                            </th>
                            <th class="xian" style="width: 50%; text-align: left;">
                                &nbsp;&nbsp;已选角色：
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_Role" runat="server" DataKeyNames="RoleID,RoleName" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="GridView_Role_SelectedIndexChanged" SkinID="gridviewSkin"
                                    ShowHeader="False">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RoleName" HeaderText="角色名称"></asp:BoundField>
                                        <asp:CommandField ShowSelectButton="True" SelectText="添加&gt;&gt;"></asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:GridView ID="GridView_EmployeeRole" runat="server" DataKeyNames="EmployeeRoleID,RoleName"
                                    OnRowDeleting="GridView_EmployeeRole_RowDeleting" ShowHeader="false" SkinID="gridviewSkin">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="行号">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RoleName" HeaderText="角色名称"></asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;div id=&quot;de&quot; onclick=&quot;JavaScript:return confirm('确定删除吗？')&quot;&gt;删除&lt;/div&gt; " />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
