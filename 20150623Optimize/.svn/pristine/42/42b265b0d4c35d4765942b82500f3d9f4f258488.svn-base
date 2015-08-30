<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FoodDiariesShow.aspx.cs" Inherits="AppPages_FoodDiariesShow" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="renderer" content="webkit" />
    <title>美食日记</title>
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/fooddiaries.css" />
</head>
<body>
    <div class="main">
        <div id="loading"></div>
        <div class="show-header-top">
            <img src="img/share_bg_header.png">
            <div id="shareDate"></div>
            <div id="shopName"></div>
        </div>
        <div class="note"></div>
        <div class="img-container double-line">
            <ul>
            </ul>
        </div>

        <div class="footer"></div>
    </div>

    <script src="../Scripts/jquery.min.js"></script>
    <script>
        $(function () {
            var queryString = getQueryStringArgs();
            var screenWidth = $(window.screen).attr('width');
            var bigImgWidth = screenWidth;
            if (bigImgWidth > 640)
                bigImgWidth = 640;
            //var smallImgWidth = Math.floor(bigImgWidth / 2);
            var bigImgHeight = Math.floor(bigImgWidth * 0.75);
            //var smallImgHeight = Math.floor(smallImgWidth * 0.75);
            // console.log(bigImgWidth + "," + smallImgWidth + ";" + bigImgHeight + ',' + smallImgHeight);

            $.ajax(
            {
                type: "POST",
                contentType: "application/json",
                url: "FoodDiariesShow.aspx/GetJson",
                data: '{ "id":"' + queryString.id + '" }',
                dataType: "json",
                success: function (msg) {
                    //console.log(msg);
                    if (msg.d == "") {
                        //没有加载到数据!!!!
                        location.href = "redirect.aspx";
                    } else {
                        var foodDiary = $.parseJSON(msg.d);
                        $("#shareDate").html(foodDiary.shoppingDate + " " + foodDiary.weather);
                        $("#shopName").html("@" + foodDiary.shopName);
                        $(".note").html(foodDiary.content);

                        var ul = $(".img-container > ul");

                        if (foodDiary.isBig)
                            ul.addClass('big');
                        else
                            ul.addClass('small');
                        ul.empty(true);
                        $.each(foodDiary.foodDiaryDishes, function (i, n) {
                            showList(ul, i, n, foodDiary.isHideDishName);

                        });

                        var li = $("<li />");
                        li.addClass('tips');
                        li.html(foodDiary.name + '的美食日记');
                        ul.append(li);

                    }
                },
                beforeSend: function () {
                    $("#loading").text("数据加载中...");
                },
                complete: function () {
                    $("#loading").empty();


                }
            });

            function showList(ul, index, foodDiaryDish, isHideDishName) {
                //console.log(foodDiaryDish.dishName);
                if (foodDiaryDish.status) {


                    var li = $("<li />");
                    var img = $("<img />");
                    img.attr('src', foodDiaryDish.imagePath + "@" + bigImgWidth + "w_" + bigImgHeight + "h_50Q_1e_1c");
                    img.addClass('dishImag');
                    img.load(function () {
                        //ul.append(li);
                    }).error(function () {
                        /* Act on the event */
                        li.remove();
                    });
                    



                    //菜名
                    var span = $("<span />");
                    span.html('<div>'+foodDiaryDish.dishName+'</div>');
                    span.addClass('dishName');

                    if (isHideDishName) {
                        span.hide();
                    }

                    var hidden = $('<input type="hidden"/>');
                    hidden.val(foodDiaryDish.sort);

                    var mask = $('<img src="img/mask.png" class="mask">');

                    li.append(img);
                    li.append(span);
                    li.append(mask);
                    li.append(hidden);
                    ul.append(li);
                }
            }

            writeFooter();

            function writeFooter() {
                var pageType = 'app';
                if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i) || navigator.userAgent.match(/android/i)) {
                    //移动设备                     
                    if (queryString.app != undefined && queryString.app == "true") {
                        return;
                    }
                    else {
                        //console.log("2");

                    }

                } else {
                    pageType = 'web';
                    //console.log("3");
                }

                $.ajax({
                    type: "Post",
                    url: "FoodDiariesShow.aspx/GetFooderHtml",
                    data: "{'pageType':'" + pageType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //console.log(data);
                        $(".footer").append(data.d);
                    },
                    error: function (err) {

                    }
                });
            }


            function getQueryStringArgs() {
                var qs = (location.search.length > 0 ? location.search.substring(1) : "");
                var args = {};
                //处理查询字符串
                var items = qs.split("&"),
                    item = null,
                    name = null,
                    value = null;
                for (var i = 0; i < items.length; i++) {
                    item = items[i].split("=");
                    name = decodeURIComponent(item[0]);
                    value = decodeURIComponent(item[1]);
                    args[name] = value;
                }
                return args;
            };


        });
    </script>
    
</body>
</html>
