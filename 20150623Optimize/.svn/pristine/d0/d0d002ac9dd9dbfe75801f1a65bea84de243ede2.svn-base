<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeAdvert.aspx.cs" Inherits="HomeNew_HomeAdvert" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" width="80px">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户广告列表管理</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/searchShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GridViewStyle("GridView_Advert", "gv_OverRow");
        });
        
        function oncbl(Control) {
            var input = document.getElementsByTagName("input");
            for (i = 0; i < input.length; i++) {
                if (input[i].type == "checkbox") {
                    input[i].checked = Control.checked;
                }
            }
        }

        function checkConfirm(obj)
        {
            var message = "";
            //var linkUpdateValue = $("#lnkUpdate").val();
            debugger;

            if (obj.innerText == "客户端下线")
            {
                message = "你确认下线吗？";
            }
            else
            {
                message = "你确认上线吗？";
            }
            if(confirm(message))
            {
                return true;
            }
            return false;
        }
    </script>
    <style type="text/css">
        li:hover {
            text-decoration: underline;
            color: #39c;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc3:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
            navigationText="" navigationUrl="" headName="商户广告列表管理" />
        <div class="content" id="divDetail" runat="server" style="width: 100%; display: '';">
            <div style="width: 80%">
                <table class="table" cellpadding="0" cellspacing="0" style="width: 100%; margin-bottom: 0px;">
                    <tr>
                        <th>
                            &nbsp;
                            城市
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlCity" runat="server" Height="40px" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <th>
                            一级栏目
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlTitle" runat="server" Height="40px" Width="100px" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <th>
                            二级栏目
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlSubTitle" runat="server" Height="40px" Width="100px"></asp:DropDownList>
                        </td>
                        <th>
                            商户
                        </th>
                        <td>
                            <asp:TextBox ID="txtShop" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div>
                    <hr style="height: 1px; border: none; border-top: 2px solid blue;" />
                </div>
                <table>
                    <tr>
                        <td>全选：<input type="checkbox" id="ckbCheckAll" onclick="oncbl(this)" /></td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Height="30px" Text="搜索" CssClass="couponButtonSubmit" OnClick="btnSearch_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Height="30px" Text="新增" CssClass="couponButtonSubmit" OnClick="btnAdd_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Height="30px" Text="删除" OnClientClick="return confirm('你确定删除吗？')" CssClass="couponButtonSubmit" OnClick="btnDelete_Click"/></td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" Height="30px" Text="预览" CssClass="couponButtonSubmit" OnClick="btnShow_Click"/></td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_CheckedNeedToPay" runat="server" CssClass="div_gridview">
                <asp:GridView ID="GridView_Advert" runat="server" DataKeyNames="id,cityID,index,title,status,secondTitleID,firstTitleID"
                    AutoGenerateColumns="False" AllowSorting="True"  SkinID="gridviewSkin" OnRowCommand="GridView_Advert_RowCommand" OnRowDeleting="GridView_Advert_RowDeleting" OnRowUpdating="GridView_Advert_RowUpdating" OnSorting="GridView_Advert_Sorting" OnRowDataBound="GridView_Advert_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input type="checkbox" id="ckbSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="行号">
                                <ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="title" HeaderText="商户" />
                        <asp:BoundField DataField="firstTitleName" HeaderText="一级栏目" />
                        <asp:BoundField DataField="secondTitleName" HeaderText="二级栏目" />
                        <asp:BoundField DataField="index" HeaderText="顺序"  SortExpression="index"  />
                        <asp:TemplateField HeaderText="客户端是否上线" SortExpression="status">
                            <ItemTemplate>
                                <%#Eval("status").ToString().Equals("1") ? "<font color=greed>√</font>":"<font color=red>×</font>" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="createTime" HeaderText="创建时间"  SortExpression="createTime"  />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <img src="../Images/key_edit2.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="edit" Text="编辑" ></asp:LinkButton>
                                <img src="../Images/delete.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="delete" OnClientClick="return confirm('你确定删除吗？')" Text="删除"></asp:LinkButton>
                                <img src="../Images/key_edit2.gif" />&nbsp;&nbsp;<asp:LinkButton ID="lnkUpdate"  runat="server" CausesValidation="False" CommandName="update" OnClientClick="return checkConfirm(this);" Text='<%#Eval("status").ToString().Equals("0") ? "客户端上线":"客户端下线"%>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                 <label id="lbCount" runat="server" ></label>
                <asp:HiddenField ID="hidTotalCount" runat="server" Value="0"/>
                <asp:Panel CssClass="gridviewBottom" runat="server" ID="PanelPage">
                    <div class="gridviewBottom_left">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="10" OnPageChanged="AspNetPager1_PageChanged"
                            FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go"
                            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" CurrentPageButtonClass="currentButton"
                            PageIndexBoxClass="listPageText" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                            MoreButtonType="Image" NavigationButtonType="Image">
                        </webdiyer:AspNetPager>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
        <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
