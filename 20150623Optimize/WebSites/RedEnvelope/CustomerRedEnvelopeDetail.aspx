<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerRedEnvelopeDetail.aspx.cs" Inherits="RedEnvelope_CustomerRedEnvelopeDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../AppPages/js/CommonScript.js"></script>
    <script type="text/javascript">
        var cookie = GetRequestParam("c");//获取地址栏上用户cookie
        var p = GetRequestParam("p");//获取地址栏上用户手机号码
        function loadData(parameters) {
            var pageIndex = 1;
            var pageSize = 10;
            $.ajax({
                type: "Post",
                url: "CustomerRedEnvelopeDetail.aspx/GetWebRedEnvelopeDetail",
                data: "{ 'pageIndex': '" + pageIndex + "','pageSize': '" + pageSize + "' ,'cookie': '" + cookie + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var modelData = eval("(" + data.d + ")");
                    for (var i = 0; i < modelData.detailsList.length; i++) {
                        var pageAmount = modelData.detailsList[i].pageAmount;
                        var pageTime = modelData.detailsList[i].pageTime;
                        var pageStatusDes = modelData.detailsList[i].pageStatusDes;
                        //TODO，循环列表展示
                    }
                },
                error: function () {
                    alert("数据加载失败");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
