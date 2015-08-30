<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cooperateMaintain.aspx.cs"
    Inherits="ViewAllocWebSite_CorpManage_cooperateMaintain" %>

<%@ Register Src="~/web/CorpManage/ManageMenu.ascx" TagPrefix="Manage"
    TagName="ManageMenu" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部分合作商户维护</title>
    <script type="text/javascript" language="javascript" src="ckfinder/ckfinder.js"></script>
    <script type="text/javascript" language="javascript" src="ckeditor/ckeditor.js"></script>
    <script type="text/javascript" language="javascript">

        function Cancel() {

            this.document.getElementById("txtTitle").value = "";
        }

        //判断value是否为正整数
        function isNumber(value) {
            var rules = "^[1-9][0-9]*$"; //正整数

            if (value.match(rules) == null) {
                return false;
            }
            else {
                return true;
            }
        }

        function Check_FileType() {
            var err = "";
            var msg = "请注意以下内容：\r\n"
            var str = document.getElementById("FileUpload2").value;
            var pos = str.lastIndexOf(".");
            var lastname = str.substring(pos, str.length);
            if (document.getElementById("txtTitle").value.length == 0) {
                err += "【商户名称不能为空】\r\n";
            }
            if (document.getElementById("txtOrder").value.length == 0) {
                err += "【商户序号不能为空】\r\n";
            }
            if (document.getElementById("txtOrder").value.length > 0) {
                if (!isNumber(document.getElementById("txtOrder").value)) {
                    err += "【商户序号必须是正整数】\r\n";
                }
            }
            var obj = document.getElementById("img");

            if (obj == null) {//新增
                if (lastname.length == 0) {
                    err += "【请先点击[浏览]按钮选择要上传的商户图片！】\r\n";
                }
                if (lastname.length > 0 && lastname.toLowerCase() != ".png") {
                    err += "【您上传的文件类型为" + lastname + "，图片必须为.png类型】\r\n";
                }
            }
            else {//修改
                if (lastname.length > 0 && lastname.toLowerCase() != ".png") {
                    err += "【您上传的文件类型为" + lastname + "，图片必须为.png类型】\r\n";
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
                                    部分合作商户维护
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    商户名称
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    商户序号
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtOrder"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    商户照片
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:FileUpload ID="FileUpload2" runat="server" Width="450px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button runat="server" ID="btnSave" Text="保存" 
                                        OnClientClick="return Check_FileType();" onclick="btnSave_Click" />
                                    <input id="btnCancel" type="button" value="清除" causesvalidation="false" onclick="return Cancel();" />
                                    <input id="btnBack" type="button" value="返回" causesvalidation="false" onclick="return window.location.href='cooperateManage.aspx'" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Image ID="img" runat="server" />
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
