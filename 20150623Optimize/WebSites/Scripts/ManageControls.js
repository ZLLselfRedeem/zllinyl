//实现鼠标放上去，text里面的字消失，鼠标移走，字再次显示功能
function clearDefaultText(el, message) {
    var obj = el;
    if (typeof (el) == "string")
        obj = document.getElementById(id);
    if (obj.value == message) {
        obj.value = "";
    }
    obj.onblur = function () {
        if (obj.value == "") {
            obj.value = message;
        }
    }
}

//实现checkbox全选，全不选
function CheckUnCheck(checkbox, form) {
    if (checkbox.checked) {
        CheckAll(form);
    }
    else {
        UnCheckAll(form);
    }
}

//实现checkbox全选
function CheckAll(form) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        e.checked = true;
    }
}

//实现checkbox全不选
function UnCheckAll(form) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        e.checked = false;
    }
}

//鼠标移动到gridview行时，改变颜色
function GridViewStyle(gridviewID, trClass) {
    $("#" + gridviewID + " tr").mouseover(function () {
        var trIndex = $(this).index();
        if (trIndex != 0) {
            $(this).children().addClass(trClass); //鼠标移上去添加背景色
        }
    }).mouseout(function () { $(this).children().removeClass(trClass); }); //鼠标移开去除背景色"gv_OverRow"
}

//弹出提示框comfirm
function ConfirmWindow(panel_name) {
    var msgObj = $("#" + panel_name);
    msgObj.css("display", "block");
    msgObj.css("position", "absolute");
    var left = ($(document).width() - msgObj.width()) / 2;
    //var top = ($(window).height() - msgObj.height()) / 2; //(document.documentElement.clientHeight - msgObj.height()) / 2
    var top = 100;
    msgObj.css("top", top);
    msgObj.css("left", left);
    msgObj.css("zIndex", "3");
}

//隐藏提示框
function HiddenConfirmWindow(panel_name) {
    var msgObj = $("#" + panel_name);
    msgObj.css("display", "none");
    msgObj.css("position", "absolute");
    var left = ($(document).width() - msgObj.width()) / 2;
    var top = (document.documentElement.clientHeight - msgObj.height()) / 2 + $(document).scrollTop();
    msgObj.css("top", top);
    msgObj.css("left", left);
    msgObj.css("zIndex", "3");
}
/************************************是先选择子节点，父节点也跟着显示**************************************/
function public_GetParentByTagName(element, tagName) {
    var parent = element.parentNode;
    var upperTagName = tagName.toUpperCase();
    while (parent && (parent.tagName.toUpperCase() != upperTagName)) {
        parent = parent.parentNode ? parent.parentNode : parent.parentElement;
    }
    return parent;
}
function setParentChecked(objNode) {
    var objParentDiv = public_GetParentByTagName(objNode, "div");
    if (objParentDiv == null || objParentDiv == "undefined") {
        return;
    }
    var objID = objParentDiv.getAttribute("ID");
    objID = objID.substring(0, objID.indexOf("Nodes"));
    objID = objID + "CheckBox";
    var objParentCheckBox = document.getElementById(objID);
    if (objParentCheckBox == null || objParentCheckBox == "undefined") {
        return;
    }
    if (objParentCheckBox.tagName != "INPUT" && objParentCheckBox.type == "checkbox")
        return;
    objParentCheckBox.checked = true;
    setParentChecked(objParentCheckBox);
}
function setChildUnChecked(divID) {
    var objchild = divID.children;
    var count = objchild.length;
    for (var i = 0; i < objchild.length; i++) {
        var tempObj = objchild[i];
        if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
            tempObj.checked = false;
        }
        setChildUnChecked(tempObj);
    }
}
function setChildChecked(divID) {
    var objchild = divID.children;
    var count = objchild.length;
    for (var i = 0; i < objchild.length; i++) {
        var tempObj = objchild[i];
        if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
            tempObj.checked = true;
        }
        setChildChecked(tempObj);
    }
}
//触发事件
function CheckEvent() {
    var objNode = event.srcElement;
    if (objNode.tagName != "INPUT" || objNode.type != "checkbox")
        return;
    if (objNode.checked == true) {
        setParentChecked(objNode);
        var objID = objNode.getAttribute("ID");
        var objID = objID.substring(0, objID.indexOf("CheckBox"));
        var objParentDiv = document.getElementById(objID + "Nodes");
        if (objParentDiv == null || objParentDiv == "undefined") {
            return;
        }
        setChildChecked(objParentDiv);
    }
    else {
        var objID = objNode.getAttribute("ID");
        var objID = objID.substring(0, objID.indexOf("CheckBox"));
        var objParentDiv = document.getElementById(objID + "Nodes");
        if (objParentDiv == null || objParentDiv == "undefined") {
            return;
        }
        setChildUnChecked(objParentDiv);
    }
}
/************************************tab标签**************************************/
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
    //GetLayoutHeight();
}
function GetLayoutHeight() {
//    //计算layout的高度
    var headControlHeight = $("#headControl").height();
    var tagMenuHeight = $("div.tagMenu").height();
    var layoutHeight = $(window).height() - headControlHeight - tagMenuHeight - 5;
    //$("div.layout").css({ "height": layoutHeight });
    //var layoutWidth = window.screen.width - 80;
    //$("div.layout").css({ "width": layoutWidth });
}

function iframeParent(frameId)
{
	$(window.parent.document).find(frameId).load(function(){
		var iframeMain = $(window.parent.document).find(frameId);
		var iframeWin = $(document).height()+0;
		iframeMain.height(iframeWin);
	});
}
