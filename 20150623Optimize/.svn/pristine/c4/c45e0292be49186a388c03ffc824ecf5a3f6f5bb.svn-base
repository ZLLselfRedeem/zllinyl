<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recentNewsMaintain.aspx.cs"
    Inherits="ViewAllocWebSite_CorpManage_recentNewsMaintain" %>

<%@ Register Src="~/web/CorpManage/ManageMenu.ascx" TagPrefix="Manage" TagName="ManageMenu" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>最新动态</title>
    <script type="text/javascript" defer="defer" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" language="javascript" src="ckfinder/ckfinder.js"></script>
    <script type="text/javascript" language="javascript" src="ckeditor/ckeditor.js"></script>
    <script type="text/javascript" language="javascript">

        function Cancel() {
            this.document.getElementById("ddlType").value = "";
            this.document.getElementById("txtDate").value = "";
            this.document.getElementById("txtTitle").value = "";
            this.document.getElementById("txbContent").value = "";
            //CKEDITOR.instances.content.getData();
        }
        function check() {
            //if (Page_ClientValidate())解决验证控件和JS冲突
            if (Page_ClientValidate()) {
                var a = CKEDITOR.instances.CKEditor1.getData();

                //"bmp", "gif", "jpeg", "jpg",
                if (a.indexOf("img") > 0 && (a.indexOf("bmp") > 0 || a.indexOf("gif") > 0 || a.indexOf("jpeg") > 0 || a.indexOf("jpg") > 0)) {
                    alert("请确保图片为png格式！");
                    return false;
                }
            }
        }
    </script>
    <link type="text/css" rel="stylesheet" href="../css/manage.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrap">
        <Manage:ManageMenu ID="Menu" runat="server" />
        <br />
        <br />
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="5" width="100%" bordercolorlight="#c0c0c0" bordercolordark="#ffffff"
                            border="1">
                            <tr>
                                <td colspan="2">
                                    最新动态维护
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    类别
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlType" Height="22px" Width="154px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTyep" ControlToValidate="ddlType"
                                        Text="必填" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    日期
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDate"></asp:TextBox>
                                    <img height="15" onclick="return WdatePicker({el:$dp.$('txtDate'),dateFmt:'yyyy-MM-dd'});"
                                        src="../img/date.gif" style="cursor: hand" alt="" />
                                    <asp:RequiredFieldValidator runat="server" ID="rfvDate" ControlToValidate="txtDate"
                                        Text="必填" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvDate" runat="server" Text="无效日期" ControlToValidate="txtDate"
                                        Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    标题
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTitle" ControlToValidate="txtTitle"
                                        Text="必填" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    内容
                                </td>
                            </tr>
                            <tr>
                                <%--   <td colspan="2">
                                    <CKEditor:CKEditorControl ID="CKEditor1" runat="server"></CKEditor:CKEditorControl>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCKEditor" ControlToValidate="CKEditor1"
                                        Text="必填" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>--%>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txbContent" TextMode="MultiLine" Height="260px" 
                                        Width="943px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" OnClientClick="return check();" />
                                    <input id="btnCancel" type="button" value="清除" causesvalidation="false" onclick="return Cancel();" />
                                    <input id="btnBack" type="button" value="返回" causesvalidation="false" onclick="return window.location.href='recentNewsManage.aspx'" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
