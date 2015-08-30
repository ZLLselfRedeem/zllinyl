<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClientImageSizeConfig.aspx.cs"
    Inherits="SystemConfig_ClientImageSizeConfig" %>

<%@ Register Src="../WebUserControl/HeadControl.ascx" TagName="HeadControl" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../WebUserControl/CheckUser.ascx" TagName="CheckUser" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客户端图片处理参数</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ManageControls.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            GridViewStyle("gdvRechageList", "gv_OverRow");
        });

        function Check() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            if (document.getElementById("ddlAppType").value == "NA") {
                err += "【请选择设备类别】\r\n";
            }
            if (document.getElementById("txbScreenWidth").value.length == 0) {
                err += "【屏幕宽度不能为空】\r\n";
            }
            if (document.getElementById("ddlImageType").value == "NA") {
                err += "【请选择图片类别】\r\n";
            }
            if (document.getElementById("txbValue").value.length == 0) {
                err += "【处理参数不能为空】\r\n";
            }
            var txbValue = document.getElementById("txbValue").value;
            if (txbValue.length > 0) {
                if (txbValue.indexOf("@") < 0 || txbValue.indexOf("w_") < 0 || txbValue.indexOf("h_") < 0 || txbValue.indexOf("Q") < 0 || txbValue.length < 9) {
                    err += "【请输入正确的处理参数】\r\n";
                }
            }
            if (err.length > 0) {
                alert(msg + err);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:HeadControl ID="HeadControl1" runat="server" navigationImage="~/images/icon/list.gif"
        navigationText="客户端图片处理" navigationUrl="" headName="客户端图片处理参数" />
    <div class="content" id="divList" runat="server">
        <div class="layout">
            <div class="QueryTerms">
                <table style="width: 100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td width="20%">
                            设备类别&nbsp;
                            <asp:DropDownList runat="server" ID="ddlAppTypeQuery" OnSelectedIndexChanged="ddlAppTypeQuery_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td width="30%">
                            图片类别&nbsp;
                            <asp:DropDownList runat="server" ID="ddlImageTypeQuery" AutoPostBack="true" OnSelectedIndexChanged="ddlImageTypeQuery_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="20%">
                            屏幕宽度&nbsp;
                            <asp:TextBox runat="server" ID="txbQueryScreenWidth"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Text="搜索" CssClass="button" OnClick="btnQuery_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="新建" CssClass="button" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel_List" runat="server">
                <div class="div_gridview">
                    <asp:GridView runat="server" ID="gdvList" AutoGenerateColumns="False" CssClass="gridview"
                        DataKeyNames="id,status" SkinID="gridviewSkin" OnDataBound="gdvList_DataBound">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="编号" Visible="false" />
                            <asp:BoundField DataField="appType" HeaderText="设备" />
                            <asp:BoundField DataField="screenWidth" HeaderText="屏幕宽度" />
                            <asp:BoundField DataField="imageType" HeaderText="图片类别" />
                            <asp:BoundField DataField="value" HeaderText="处理参数" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img src="../Images/key_edit2.gif" alt="" />
                                    <asp:LinkButton runat="server" ID="lnkbtnModify" CommandName="modify" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'>修改</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkbtnDelete" CommandName="del" OnCommand="lnkbtnEdit_OnCommand"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>' OnClientClick="return confirm('您确定要删除吗？')">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="asp_page">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PageSize="15" PrevPageText="上一页" SubmitButtonText="Go" TextAfterPageIndexBox="页"
                            TextBeforePageIndexBox="转到" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条"
                            NumericButtonType="Text" CssClass="liPage" SubmitButtonClass="listPageBtn" PageIndexBoxClass="listPageText"
                            CurrentPageButtonClass="currentButton" ShowPageIndexBox="Always" currentpagebuttonposition="Center"
                            OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <th>
                                    共
                                </th>
                                <td>
                                    <asp:Label ID="lbCount" runat="server" Text="0" ForeColor="#F40404" Font-Bold="True"></asp:Label>
                                </td>
                                <th>
                                    条数据
                                </th>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="content" id="divDetail" runat="server" style="display: none">
        <div class="layout">
            <table class="table" cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <th width="20%">
                        设备类别
                    </th>
                    <td width="90%">
                        <asp:DropDownList runat="server" ID="ddlAppType" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        屏幕宽度
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbScreenWidth" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        图片类别
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlImageType" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        处理参数
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txbValue" Width="200px"></asp:TextBox>（格式：@[widthValue]w_[heightValue]h_[QuantityValue]Q，如：@640w_450h_50Q）
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="button" OnClick="btnSave_Click"
                            OnClientClick="return Check();" />
                        <asp:Button runat="server" ID="btnCancle" Text="取消" CssClass="button" OnClick="btnCancle_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
      <uc1:CheckUser ID="CheckUser1" runat="server" />
    </form>
</body>
</html>
