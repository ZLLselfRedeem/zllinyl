<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeManage.aspx.cs" Inherits="AuthorizationManagement_EmployeeManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>员工管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        function changeColor(cb1, index, id) {
            var tr = document.getElementById(cb1).parentElement.parentElement;
            tr.style.backgroundColor = index == 1 ? "red" : "blue";
            var cbs = document.getElementById("<%= hf.ClientID %>");
            if (document.getElementById(cb1).checked) {
                if (cbs.value.indexOf(id + "," + cb1) < 0)
                    cbs.value += id + "," + cb1 + "|";
            }
            else {
                tr.style.backgroundColor = "white";
            }
        }
        function RowClick(obj) {
            var chkColl = obj.all;
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox") {//alert(23); 
                    chkColl[i].checked = true;
                }
            }
        }
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView1", "gv_OverRow");
            $("#div_gridview").css({ "height": $(window).height() - 200 });
            $("#div_gridview").css({ "overflow": "auto" });
        });

    </script>
</head>
<body scroll="no" style="overflow-y: hidden" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <form id="form2" runat="server">
        <uc2:HeadControl ID="HeadControl1" runat="server" headName="员工管理" navigationImage="~/images/icon/new.gif"
            navigationText="员工添加" navigationUrl="~/AuthorizationManagement/EmployeeAdd.aspx" />
        <div id="box" class="box">
            <div class="tagMenu">
                <ul class="menu">
                    <li>员工管理</li>
                </ul>
            </div>
            <div class="content">
                <div class="layout">
                    <div class="QueryTerms">
                        <table>
                            <tr>
                                <td>用户名或姓名：
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
                            CssClass="gridview" 
                            SkinID="gridviewSkin" OnRowCommand="GridView1_OnRowCommand" OnRowDeleting="GridView1_RowDeleting">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <asp:BoundField DataField="position" HeaderText="职位" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                            runat="server" CausesValidation="False" CommandName="Select" Text="修改" CommandArgument='<%#Bind("EmployeeID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--  <asp:HyperLinkField Text="权限管理"
                                    DataNavigateUrlFields="EmployeeID"
                                    DataNavigateUrlFormatString="ShopAuthorityManage.aspx?EmployeeID={0}"></asp:HyperLinkField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton3"
                                            runat="server" CausesValidation="false" CommandName="Role" CommandArgument='<%#Bind("EmployeeID")%>'  Text="权限管理"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                            CausesValidation="False" CommandName="delete" Text="删除"  CommandArgument='<%#Bind("EmployeeID")%>' OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hf" runat="server" />
                        <div class="asp_page">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="Panel_Operate" runat="server" CssClass="panelSyle">
            <asp:HiddenField runat="server" ID="hiddenEmployee" />
            <asp:GridView ID="GridView2" runat="server" DataKeyNames="roleId" CssClass="gridview" AutoGenerateColumns="False" 
                OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="roleName" HeaderText="权限名" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="checkBoxIsHave" runat="server" Checked='<%#Bind("isHave")%>' Enabled="False" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <img src="../Images/key_edit3.gif" />
                            <asp:LinkButton ID="LinkButton4"
                                runat="server" CausesValidation="False" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "isHave").Equals(true)?"取消":"添加"%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="button1" Text="完成" OnClientClick="HiddenConfirmWindow('Panel_Operate')"/>
        </asp:Panel>
        <uc1:CheckUser ID="CheckUser2" runat="server" />
    </form>
</body>
</html>
