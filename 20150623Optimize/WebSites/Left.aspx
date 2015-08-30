<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>顶部-美食+后台管理系统</title>
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>
    <link href="Css/css_list.css" rel="stylesheet" type="text/css" />
    <link href="Css/css.css" rel="stylesheet" type="text/css" />
    <!--modify by tong 2011-09-09-->
    <script language="javascript" type="text/javascript">
        function ShowThis(id) {
            var MenuList = document.getElementsByTagName("div");
            if (document.getElementById("sort_" + id)) {
                if (document.getElementById("sort_" + id).style.display == "none") {
                    document.getElementById("sort_" + id).style.display = "block";
                    if (document.getElementById("sort_" + id + "_img_1")) document.getElementById("sort_" + id + "_img_1").src = "images/open.png";
                    if (document.getElementById("sort_" + id + "_img_2")) document.getElementById("sort_" + id + "_img_2").src = "images/minus.gif";
                    if (document.getElementById("cate_sort_" + id)) document.getElementById("cate_sort_" + id).className = "cate1b";
                } else {
                    document.getElementById("sort_" + id).style.display = "none";
                    if (document.getElementById("sort_" + id + "_img_1")) document.getElementById("sort_" + id + "_img_1").src = "images/close.png";
                    if (document.getElementById("sort_" + id + "_img_2")) document.getElementById("sort_" + id + "_img_2").src = "images/plus.gif";
                    if (document.getElementById("cate_sort_" + id)) document.getElementById("cate_sort_" + id).className = "cate1";
                }
            }

            for (var j = 0; j < MenuList.length; j++) {
                if (MenuList[j].className == "sort" && MenuList[j].id != "sort_" + id) {
                    MenuList[j].style.display = "none";
                    if (document.getElementById(MenuList[j].id + "_img_1")) document.getElementById(MenuList[j].id + "_img_1").src = "images/close.png";
                    if (document.getElementById(MenuList[j].id + "_img_2")) document.getElementById(MenuList[j].id + "_img_2").src = "images/plus.gif";
                    if (document.getElementById("cate_" + MenuList[j].id)) document.getElementById("cate_" + MenuList[j].id).className = "cate1";
                }
            }
            RecordID(id);
        }

        function FormatStatus() {
            if (document.getElementById("sort_" + defaultid)) {
                document.getElementById("sort_" + defaultid).style.display = "block";
                document.getElementById("sort_" + defaultid + "_img_1").src = "images/open.png";
                document.getElementById("sort_" + defaultid + "_img_2").src = "images/minus.gif";
                if (document.getElementById("cate_sort_" + defaultid)) document.getElementById("cate_sort_" + defaultid).className = "cate1b";
            }
        }

        function RecordID(id) {
            if (document.getElementById("sid")) document.getElementById("sid").value = id;
        }
    </script>
</head>
<body style="background-color: #F6F9FD;">
    <form runat="server">
    <div style="text-align: center; position: static" id="left_div" class="scrollDiv">
        <div id="leftnavi">
            <img src="Images/meishidiandian.png" height="30px;"  style="vertical-align:middle" />&nbsp;&nbsp;功&nbsp;&nbsp;能&nbsp;&nbsp;列&nbsp;&nbsp;表
        </div>
        <%=menu %>
        <div class="kuaijie">
            
        </div>
    </div>
    </form>
</body>
</html>
