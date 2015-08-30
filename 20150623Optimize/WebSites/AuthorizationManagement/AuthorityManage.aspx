<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthorityManage.aspx.cs"
    Inherits="AuthorizationManagement_AuthorityManage" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能模块管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView1", "gv_OverRow");
        });       
    </script>
    <script type="text/javascript">
        function GetUpdatePanel() {
            //遮罩层
            var sWidth, sHeight;
            sWidth = $(window).width(); //屏幕宽
            sHeight = $(window).height(); //屏幕高
            var bgObj = document.createElement("div");
            bgObj.setAttribute('id', 'bgDiv');
            bgObj.style.position = "absolute";
            bgObj.style.top = "0";
            bgObj.style.background = "#cccccc";
            bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75)";
            bgObj.style.opacity = "0.6";
            bgObj.style.left = "0";
            bgObj.style.width = sWidth + "px";
            bgObj.style.height = sHeight + "px";
            bgObj.style.zIndex = "2";
            document.body.appendChild(bgObj);
            //弹出层
            var msgObj = document.getElementById("Panel1");
            msgObj.style.display = "block";
            $("#Panel1").css("position", "absolute");
            var left = ($(document).width() - $("#Panel1").width()) / 2;
            var top = (document.documentElement.clientHeight - $("#Panel1").height()) / 2 + $(document).scrollTop();
            $("#Panel1").css("top", top);
            $("#Panel1").css("left", left);
            $("#Panel1").css("zIndex", "3");
        }
        function Close() {
            var bgObj = document.getElementById("bgDiv");
            document.body.removeChild(bgObj);
            var msgObj = document.getElementById("Panel1");
            msgObj.style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onkeypress="if(event.keyCode==13||event.which==13){return false;}">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="功能模块管理" navigationImage="~/images/icon/new.gif"
        navigationText="功能模块添加" navigationUrl="~/AuthorizationManagement/AuthorityAdd.aspx" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>功能模块列表</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                功能模块列表
                            </td>
                            <td>
                                <asp:Button ID="Button_ChangeAuthoritySequence" runat="server" Text="修改排序号" CssClass="commonButton"
                                    OnClick="Button_ChangeAuthoritySequence_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="asp_page">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页"
                        SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                        NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                        CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" CurrentPageButtonPosition="Center"
                        Visible="false">
                    </webdiyer:AspNetPager>
                </div>
                <table>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="AuthorityID,AuthorityName,AuthorityURL,AuthorityRank,AuthorityDescription,AuthorityStatus,AuthoritySequence,AuthorityType"
                                AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                SkinID="gridviewSkin" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="行号">
                                        <ItemTemplate>
                                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AuthorityName" HeaderText="权限名" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="排序号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AuthoritySequence") %>'
                                                Width="50"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/key_edit.gif" />
                                            &nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                                CommandName="Select" Text="修改"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/delete.gif" />
                                            &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                                                CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/key_detail.gif" />
                                            &nbsp;&nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                                CommandName="getSon" Text="查看子模块"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="vertical-align: top">
                            <asp:GridView ID="GridView_Son" runat="server" DataKeyNames="AuthorityID,AuthorityName,AuthorityURL,AuthorityRank,AuthorityDescription,AuthorityStatus,AuthoritySequence,AuthorityType"
                                AutoGenerateColumns="False" OnRowDeleting="GridView_Son_RowDeleting" OnSelectedIndexChanged="GridView_Son_SelectedIndexChanged"
                                ShowHeader="false" SkinID="gridviewSkin">
                                <Columns>
                                    <asp:TemplateField HeaderText="行号">
                                        <ItemTemplate>
                                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AuthorityName" HeaderText="权限名" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/key_edit.gif" />
                                            &nbsp;&nbsp;<asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False"
                                                CommandName="Select" Text="修改"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <img src="../Images/delete.gif" />
                                            &nbsp;&nbsp;<asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False"
                                                CommandName="delete" Text="删除" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="Panel_Authority" runat="server" CssClass="panelSyle">
                                <table>
                                    <tr>
                                        <th colspan="3" class="dialogBox_th">
                                            &nbsp; 功能模块修改 &nbsp;<asp:HiddenField ID="HiddenField_AuthorityID" runat="server" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            权限名：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TextBox_AuthorityName" runat="server" Width="200"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_AuthorityName"
                                                ErrorMessage="权限名不能为空" ForeColor="Red" Display="Dynamic" ValidationGroup="Button1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            权限类型：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_AuthorityType" runat="server">
                                                <asp:ListItem Value="page">页面</asp:ListItem>
                                                <asp:ListItem Value="button">按钮</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            权限：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TextBox_AuthorityURL" runat="server" Width="300"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            选择父级：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_AuthorityRank" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            显示序号：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TextBox_AuthoritySequence" runat="server" Width="100px"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_AuthoritySequence"
                                                Display="Dynamic" ErrorMessage="请输入数字" ForeColor="Red" ValidationExpression="^[0-9]{1,20}$"
                                                ValidationGroup="Button1"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            权限描述：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="TextBox_AuthorityDescription" runat="server" Height="50px" TextMode="MultiLine"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            &nbsp;
                                        </th>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" CssClass="button" OnClick="Button1_Click"
                                                Text="修    改" />
                                            <asp:Button ID="Button2" runat="server" CssClass="button" OnClientClick="HiddenConfirmWindow('Panel_Authority');"
                                                Text="取    消" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
