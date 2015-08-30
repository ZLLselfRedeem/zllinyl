<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadImageConfig.aspx.cs" Inherits="SystemConfig_uploadImageConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>上传广告图片</title>
    <link href="../Css/css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellspacing="0" cellpadding="0"  class="table">
                        <tr>
                            <th style="text-align: center; width: 20%">
                                广告图片：
                            </th>
                            <td style="text-align: left; width: 80%" colspan="2">
                                <table width="100%" class="table">
                                    <tr>
                                        <td align="center">
                                            <asp:FileUpload ID="fileUpload" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button_Upload" runat="server" CssClass="tabButtonBlueClick" OnClick="Button_Upload_Click"
                                                Text="上传" />
                                           <br /><font color="red">（请上传小于1024KB的PNG,JPG,GIF格式图片）</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            上传后的图片链接为:
                                            <asp:Label runat="server" ID="lblImgUrl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Image ID="Big_Img" runat="server" style="max-width:800px;" ImageUrl="~/Images/bigimage.jpg" CssClass="innerTD" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
    </div>
    </form>
</body>
</html>
