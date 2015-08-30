// JavaScript Document
$(function () {
    $("#receiveLink").on("click", function () {
        var phonenum = $("#phonenum").val();
        $.ajax({
            type: "Post",
            url: "index.aspx/SendMessage",
            data: "{mobile:'" + phonenum + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != "" && data.d != null) {
                    var d = data.d;
                    if (d == "1") {
                        alert("短信已发送，请注意查收");
                    } else if (d == "-3") {
                        alert("短信发送失败，请重试")
                    } else {
                        alert("手机号码不正确");
                    }
                }
            },
            error: function (XmlHttpRequest, textStatus, errorThrown) {
                //
            }
        });

    });
});



