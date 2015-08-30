<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="AppPages_manual_feedback" %>

<!DOCTYPE html >
<html runat="server">
<head>
    <title>意见反馈</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link rel="stylesheet" type="text/css" href="../css/jquery.mobile.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/common.css" />
	<script src="../js/jquery.js"></script>
    <script src="../../Scripts/CommonScript.js" type="text/javascript"></script>
	<script src="../js/jquery.mobile-1.3.0.min.js"></script>
    <style type="text/css">
        .link
        {
            text-decoration: none;
            color: Black;
        }
       
        #back
        {
            color: #5a5e65;
        }
        #back span{
            font-size: 44px;
            font-family: "宋体";
            font-weight: normal;
            position: absolute;
            top: -22px;
            left: 0px;
        }
		#layout #msg{
			height:25px;
			color:#ffffff;
			text-align:center;
			font-size:1.3rem;
		}
		#layout{
			text-align:center;
		}
		#layout .close{
			border-radius:5px;
		}
		#popupContent{
			text-align:center;
		}
		#tb_feedbackTxt{
			height:200px;
			width:90%;
			margin:5px auto;
		}
		#submitSave{
			color:#5a5e65;
		}
    </style>
    <script language="javascript" type="text/javascript">
		var msg = "";
        function save() {
            var cookie = GetRequestParam("c");
            var feedbackMsg = $("#tb_feedbackTxt").val();
            $.ajax({
                type: "Post",
                url: "feedback.aspx/Save",
                data: "{'cookie':'" + cookie + "','feedbackMsg':'" + feedbackMsg + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "" && data.d != null) {
                        switch (data.d) {
                            case 1:
                                $("#tb_feedbackTxt").val('');
                                msg = "发送成功";
                                break;
                            case -1:
                                msg = "发送失败，请重试";
                                break;
                            case -2:
                                msg = "用户信息有误";
                                break;
                            case -3:
                                msg = "反馈信息不能为空";
                                break;
                            default:
                                msg = "发送失败，请重试";
                        }
                    }
                    else {
                        alert("发送失败，请重试");
                    }
					$("#msg").text(msg);
					setTimeout(function(){
						$("#layout").popup( "close" );
					},1000);
                },
                error: function (XmlHttpRequest, textStatus, errorThrown) {
                    alert("发送失败，请重试");
                }
            });
        };
		$(function(){
			$("#submitSave").on("tap",save);
		});
    </script>
</head>
<body>
    <table style="width:90%;margin:22px auto 0 auto; text-align: center;" cellspacing="0" cellpadding="0">
        <tr>
            <td align="left">
                <a href="javascript:;" id="back">返回</a>
            </td>
            <td style="font-size: 22px;">
                意见反馈
            </td>
            <td align="right">
                <a id="submitSave" href="#layout" data-position-to="window" data-transition="pop" data-rel="popup">发送</a>
            </td>
        </tr>
    </table>
	<div class="">
		<textarea id="tb_feedbackTxt" name="tb_feedbackTxt" style="height:200px;"></textarea>
	</div>
	<div data-role="popup" id="layout" data-overlay-theme="a" data-theme="b" data-dismissible="false" style="max-width:400px;" >
		<div role="main" class="ui-content" style="padding:8px;">
			<p id="msg" style="width:130px;"></p>
		</div>
	</div>
</body>
</html>
