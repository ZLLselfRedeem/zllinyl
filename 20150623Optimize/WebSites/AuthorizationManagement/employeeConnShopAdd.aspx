<%@ Page Language="C#" AutoEventWireup="true" CodeFile="employeeConnShopAdd.aspx.cs"
    Inherits="AuthorizationManagement_employeeConnShopAdd" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店权限添加</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/CommonScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            TabManage();
            GridViewStyle("GridView_List", "gv_OverRow");
        });
        function keyTest() {
            var str = $("#text").val();
            if (str == "") {
                return;
            }
            $.ajax({
                type: "Post",
                url: "../Handlers/commonAjaxPage.aspx/GetData",
                data: "{'str':'" + str + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        var returnJson = eval("(" + data.d + ")");
                        var strHtml = "<ul>";
                        for (var i = 0; i < returnJson.length; i++) {
                            if (i == 0) {
                                $("#init_date").html('');
                            }
                            strHtml += "<li onclick='select(this)' id='"
                            + returnJson[i].shopId + "' data-name='"
                            + returnJson[i].shopName + "'>&nbsp;&nbsp;"
                            + returnJson[i].shopName + "&nbsp;&nbsp;</li>"
                        }
                        strHtml += "</ul>";
                        $("#init_date").append(strHtml);
                    }
                },
                error: errorFun
            });
        }
        function select(shop) {
            var name = $("#" + shop.id).attr('data-name');
            $("#init_date").html('');
            $("#text").val(name);
            $("#hidden").val(shop.id);
        }
        function errorFun() {
            alert("获取数据失败");
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
<body scroll="no">
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" headName="门店权限添加" navigationImage="~/images/icon/list.gif"
        navigationText="" navigationUrl="" />
    <div id="box" class="box">
        <div class="tagMenu">
            <ul class="menu">
                <li>门店权限添加</li>
            </ul>
        </div>
        <div class="content">
            <div class="layout">
                <table class="order_main" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%; text-align: left; font-size: 12px;">
                            <table>
                                <tr>
                                    <th>
                                        <asp:Label ID="lb_head" runat="server" Text=""></asp:Label>
                                        &nbsp; &nbsp; 门店搜索：
                                    </th>
                                    <td>
                                        <input id="text" runat="server" type="text" onkeyup="keyTest()" />
                                        <div id="init_date" runat="server" style="position: absolute; clear: both; border: 1px solid #000;
                                            background-color: White">
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_add" runat="server" CssClass="button" Text="添  加" OnClick="btn_add_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr size="1" style="border: 1px #cccccc dashed;" />
                            <div>
                                <div class="asp_page">
                                    <asp:GridView ID="GridView_List" runat="server" DataKeyNames="employeeShopID" AutoGenerateColumns="False"
                                        SkinID="gridviewSkin" OnRowDeleting="GridView_List_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="shopName" HeaderText="门店名称" />
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server"
                                                        CausesValidation="False" CommandName="delete" Text="删除权限" OnClientClick="return confirm('你确定删除吗？')"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="asp_page">
                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                            NextPageText="下一页" PageSize="10" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                                            OnPageChanged="AspNetPager1_PageChanged">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidden" runat="server" />
    </form>
</body>
</html>
