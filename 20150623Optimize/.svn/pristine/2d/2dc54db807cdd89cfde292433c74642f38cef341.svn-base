<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var timeRefresh = setInterval("Refresh();", 1000); //代码错误，跳转到错误页面
        //        function Refresh() {
        //            var label_time = $("#label_time");
        //            var currentTime = label_time.text();
        //            newTime = currentTime - 1;
        //            label_time.text(newTime);
        //            if (newTime == 0) {
        //                window.open('Login.aspx', target = '_top')
        //            }
        //        }
        //页面会闪退到login页面
        function Refresh() {
            window.open('preIndex.aspx', target = '_top');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%-- 由于您长时间没有操作，您必须重新登录，系统将在秒<label style="color: Red" id="label_time">5</label>内跳转到登录页面……</div>--%>
        由于您长时间没有操作，您必须重新登录，系统将跳转到登录页面……</div>
    </form>
</body>
</html>
