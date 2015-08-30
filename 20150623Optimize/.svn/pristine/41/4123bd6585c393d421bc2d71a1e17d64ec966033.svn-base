
function TabManage() {
    $("ul.menu li:first-child").addClass("current");
    $("div.content").find("div.layout:not(:first-child)").hide();
    $("div.content div.layout").attr("id", function () { return idNumber("No") + $("div.content div.layout").index(this) });
    $("ul.menu li").click(function () {
        var c = $("ul.menu li");
        var index = c.index(this);
        var p = idNumber("No");
        show(c, index, p);
    });
    function show(controlMenu, num, prefix) {
        var content = prefix + num;
        //点击某个选项卡时，让这个选项卡下面的页面刷新 start
        if ($('#' + content).find('iframe').length > 0) {
            var iframeId = $('#' + content).find('iframe')[0].id;
            document.getElementById(iframeId).contentWindow.document.location.href = document.getElementById(iframeId).contentWindow.document.location;
            var iframe = document.getElementById(iframeId);
            //判断iframe是否加载完成
            if (iframe.attachEvent) {
                iframe.attachEvent("onload", function () {
                    $('#' + content).siblings().hide();
                    $('#' + content).show();
                    controlMenu.eq(num).addClass("current").siblings().removeClass("current");
                });
            }
            else {
                iframe.onload = function () {
                    $('#' + content).siblings().hide();
                    $('#' + content).show();
                    controlMenu.eq(num).addClass("current").siblings().removeClass("current");
                };
            }
        }
        //点击某个选项卡时，让这个选项卡下面的页面刷新 end
        else {
            $('#' + content).siblings().hide();
            $('#' + content).show();
            controlMenu.eq(num).addClass("current").siblings().removeClass("current");
        }
    };
    function idNumber(prefix) {
        var idNum = prefix;
        return idNum;
    };
	
}