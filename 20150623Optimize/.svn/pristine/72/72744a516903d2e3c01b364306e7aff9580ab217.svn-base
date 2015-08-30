<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>悠先后台管理系统</title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="Css/css.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonScript.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var topHeight = 0;
            $("#left_div").css({ "height": $(window).height() - topHeight });
            $("#LeftShow").css({ "height": $(window).height() - topHeight });
            document.getElementById("left_div").style.display = "none";
            $("#main").css({ "height": $(window).height() - topHeight });
            htCheckBrowser();
        });

        function ShowLeft() {

            //open
            if (document.getElementById("left_div").style.display == "none") {
                document.getElementById("left_div").style.display = "block";
                document.getElementById("LeftShow").style.backgroundImage = "url(images/close_on.gif)";
                $("#left").css({ "width": 200 });
            }
            //close
            else {
                document.getElementById("left_div").style.display = "none";
                document.getElementById("LeftShow").style.backgroundImage = "url(images/open_on.gif)";
                $("#left").css({ "width": 0 });
            }

        }
    </script>
</head>
<body style="margin: 0px;">
    <div id="content" class="main_content">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="vertical-align: middle;" id="left">
                    <div id="left_div" style="width: 200px;">
                        <iframe frameborder="0" name="leftFrame" width="100%" scrolling="auto" id="leftFrame"
                            src="left.aspx" height="100%"></iframe>
                    </div>
                </td>
                <td style="vertical-align: middle; width: 40px;">
                    <div id="LeftShow" onclick="ShowLeft();" style="cursor: pointer; width: 40px; background: url(images/open_on.gif) center no-repeat;
                        border-left: 1px solid #C7C5D9; border-right: 1px solid #C7C5D9;">
                    </div>
                </td>
                <td style="vertical-align: top; text-align: center;">
                    <div id="main">
                        <iframe frameborder="0" name="mainFrame" width="99%" id="mainFrame" height="100%"
                            runat="server" src="" scrolling="auto"></iframe>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
