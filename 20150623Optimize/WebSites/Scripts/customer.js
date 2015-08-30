(function () {
    var id = "";
    var $sumLayout = $("#sum-layout").dialog({
        resizable: false,
        closeOnEscape: false,
        autoOpen: false,
        draggable: false,
        //hide: { effect: 'drop', direction: 'up',duration:500 },  
        modal: true
    });

    $(".area").on("focus", function (event) {
        var val = $(this).val();
        if (val == "可选填") {
            $(this).val("");
        }
    });
    $("#grid-view .sum").click(function (event) {
        event.preventDefault();
        id = $(event.target).siblings("input").val();
        $sumLayout.dialog("open");
    });
    $("#sum-layout .btn").on("click", function (event) {
        var isCancel = $(this).attr("class").indexOf("cancel") > -1;
        if (isCancel) {
            $sumLayout.dialog("close");
            return;
        }
        var num = parseFloat($("#sum-layout .inputText").val());
        var amount = num ? num : 0;
        var remark = $("#sum-layout .area").val();

        if (!amount) {
            $(".configMoney .tips").css({ "display": "block", "opacity": 1 }).animate({
                opacity: 0
            }, 1000, function () {
                $(".configMoney .tips").css({ "display": "none" });
            });
            return;
        }
        if (!remark) {
            $(".remark .tips").css({ "display": "block", "opacity": 1 }).animate({
                opacity: 0
            }, 1000, function () {
                $(".remark .tips").css({ "display": "none" });
            });
            return;
        }

        $sumLayout.dialog("close");
        var configObj = {};
        configObj.urlMethod = 'CustomerRetrospect.aspx/changeBalance';
        configObj.data = '{ "cookie":"' + id + '","amount":' + amount + ',"remark":"' + remark + '" }';
        webMethodHandler(configObj, function (msg) {
            //window.location.reload()
            alert(msg);
        });
    });

    function webMethodHandler(param, callback) {
        var ajaxOption = new Object();
        ajaxOption.type = 'POST';
        ajaxOption.url = param.urlMethod; 	//
        ajaxOption.data = param.data; 	//
        ajaxOption.contentType = "application/json; charset=utf-8";
        ajaxOption.dataType = 'json';
        ajaxOption.success = function (data) {
            callback(data.d);
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        $.ajax(ajaxOption);
    };
})();