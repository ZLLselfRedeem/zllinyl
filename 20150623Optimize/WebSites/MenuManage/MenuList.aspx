<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuList.aspx.cs" Inherits="MenuManage_MenuList" %>

<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>菜谱管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/CouponManage/CommonFunction.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("p").click(function () {
                $(this).hide();
            });
            GridViewStyle("GridView1", "gv_OverRow");
            TabManage();
            initData("shopmenulist");
        });
        function ShowWait() {
            $(".content").append("<div id=\"loadImgDiv\" class=\"contentResultWindow\"><div style='text-align:center'><img src='../AppPages/uploads/Loading/loading7.gif'/></div><br/><div style='text-align:center'>正在更新到服务器，请稍后...</div></div>");
            ShowDivInTheCentral("loadImgDiv");
        }
        ///让一个div居中显示
        function ShowDivInTheCentral(div) {
            showMask();
            var left = ($(window).width() - $("#" + div).outerWidth()) / 2;
            var top = ($(window).height() - $("#header").outerHeight()) / 2 + $(".layout").scrollTop();
            $("#" + div).css({ "left": left, "top": top, "position": "absolute" });
            $("#" + div).show();
        }
        //显示遮盖层
        function showMask() {
            var top = $("#header").height() + $(".tagMenu").height();
            var bH = $("body")[0].scrollHeight - top; //顶部不遮盖
            var bW = "100%";
            $("#divMask").css({ width: bW, height: bH, "top": top, display: "block" });
        }     
    </script>
    <style type="text/css">
        li:hover
        {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <div id="header">
        <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/new.gif"
            navigationText="菜谱添加" navigationUrl="~/MenuManage/MenuAdd.aspx" headName="菜谱列表" />
    </div>
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>菜谱列表</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <div class="QueryTerms">
                    <table>
                        <tr>
                            <td>
                                门店搜索：
                            </td>
                            <td>
                                <input id="text" runat="server" type="text" onkeyup="shopMenuSearch()" />
                                <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                    background-color: White">
                                </div>
                            </td>
                            <td>
                                公司名称：
                            </td>
                            <td>
                                <asp:Label ID="Label_companyName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_gridview">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="MenuI18nID,MenuID,LangID,MenuName,MenuDesc,MenuI18nStatus,menuImagePath"
                        AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        SkinID="gridviewSkin" OnRowUpdating="GridView1_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MenuName" HeaderText="菜谱名称" />
                            <asp:BoundField DataField="MenuVersion" HeaderText="版本" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/key_edit3.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                        runat="server" CausesValidation="False" CommandName="Select" Text="修改"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton3" runat="server"
                                        CausesValidation="False" OnClientClick="ShowWait()" CommandName="update" Text="更新到服务器"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="divMask">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
