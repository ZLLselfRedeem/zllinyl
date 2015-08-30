<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classicCaseMaintain.aspx.cs"
    Inherits="ViewAllocWebSite_CorpManage_classicCaseMaintain" %>

<%@ Register Src="~/web/CorpManage/ManageMenu.ascx" TagPrefix="Manage" TagName="ManageMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>精选成功案例维护</title>
    <link type="text/css" rel="stylesheet" href="../css/manage.css" />
    <script type="text/javascript">

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
        }

        function Cancel() {

            this.document.getElementById("txtTitle").value = "";
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
                                    精选成功案例维护
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
                                <td>
                                    商户图片
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload2" runat="server" Width="450px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnSave" Text="保存" CssClass="button" OnClientClick="return Check_FileType();"
                                        runat="server" OnClick="btnSave_Click" />
                                    <input id="btnCancel" type="button" value="清除" causesvalidation="false" onclick="return Cancel();"
                                        class="button" />
                                    <input id="btnBack" type="button" value="返回" causesvalidation="false" onclick="return window.location.href='classicCaseManage.aspx'"
                                        class="button" />
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
