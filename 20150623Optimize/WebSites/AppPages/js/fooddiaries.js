var foodDiary = {};

$(function () {
    var screenWidth = $(window.screen).attr('width');
    var bigImgWidth = screenWidth;
    if (bigImgWidth > 640)
        bigImgWidth = 640;

    var bigImgHeight = Math.floor(bigImgWidth * 0.75);

    var queryString = getQueryStringArgs();
    $("#content").autosize();
    $.ajax(
    {
        type: "POST",
        contentType: "application/json",
        url: "FoodDiaries.aspx/GetJson",
        data: '{ "orderId":"' + queryString.id + '" }',
        dataType: "json",
        success: function (msg) {
            //console.log(msg);                    
            if (msg.d == "") {
                //没有加载到数据!!!!
                location.href = "redirect.aspx";
            } else {
                foodDiary = $.parseJSON(msg.d);
                $("#shareDate").html(foodDiary.shoppingDate + " " + foodDiary.weather);
                $("#shopName").html("@" + foodDiary.shopName);
                $("#content").val(foodDiary.content);
                $(".big-img").attr('checked', foodDiary.isBig);
                $(".remove-name").attr('checked', foodDiary.isBig);
                var ul = $(".img-container > ul");
                //console.log(foodDiary.isBig);
                if (foodDiary.isBig)
                    ul.addClass('big');
                else
                    ul.addClass('small');
                ul.empty(true);
                $.each(foodDiary.foodDiaryDishes, function (i, n) {
                    showList(ul, i, n);

                });

            }
        },
        beforeSend: function () {
            $("#loading").text("数据加载中...");
        },
        complete: function () {
            $("#loading").empty();


        }
    });


    //清文字
    $(".clear-txt").click(function (event) {
        /* Act on the event */
        $("#content").val("");
        foodDiary.content = "";

    });

    // 换文字
    $(".switch-txt").click(function (event) {
        $.ajax({
            url: 'FoodDiaries.aspx/GetContent',
            contentType: "application/json",
            type: 'POST',
            dataType: 'json',
            data: '{ "id":"' + foodDiary.id + '" }',
            success: function (msg) {
                $("#content").val(msg.d);

                foodDiary.content = msg.d;
            }
        });
    });

    //清菜名
    $(".remove-name").click(function (event) {
        //console.log($(this).is(':checked'));
        if ($(this).is(':checked')) {
            $(".dishName").hide();
            foodDiary.isHideDishName = true;
        }
        else {
            $(".dishName").show();
            foodDiary.isHideDishName = false;
        }
    });

    //大图模式
    $(".big-img").click(function (event) {
        var ul = $(".img-container > ul");
        ul.removeClass();
        if ($(this).is(':checked')) {
            ul.addClass('big');
            foodDiary.isBig = true;
        } else {
            ul.addClass('small');
            foodDiary.isBig = false;
        }
    });

    $("#content").change(function (event) {
        /* Act on the event */
        foodDiary.content = $(this).val();
    });
    $("#content").on('keyup blur', function () {
        foodDiary.content = $(this).val();
    })


    function showList(ul, index, foodDiaryDish, isHideDishName, isBig) {
        //console.log(foodDiaryDish.dishName);
        if (foodDiaryDish.status) {


            var li = $("<li />");
            var img = $("<img />");


            img.attr('src', foodDiaryDish.imagePath + "@" + bigImgWidth + "w_" + bigImgHeight + "h_30Q_1e_1c");

            img.addClass('dishImag');
            img.load(function () {
                //ul.append(li);
            }).error(function () {
                li.remove();
            });
            img.click(function () {
                var remove = $('<span class="glyphicon glyphicon-remove ico-remove"></span>');
                var li = $(this).parent();
                remove.click(function () {
                    //删除
                    foodDiary.foodDiaryDishes[index].status = false;
                    li.remove();

                    //恢复区
                    showARcoverArea(ul, index, foodDiaryDish);
                });
                li.append(remove);
                $(this).unbind('click');
            });


            //菜名
            var span = $("<span />");
            span.html('<div>' + foodDiaryDish.dishName + '</div>');
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
        } else {
            //console.log("111");
            showARcoverArea(ul, index, foodDiaryDish);
        }
    }

    function showARcoverArea(ul, index, foodDiaryDish) {
        var recover_ul = $(".recover > ul");
        var recover_li = $("<li />");
        //
        recover_li.html('<div>' + foodDiaryDish.dishName + '</div>');


        recover_ul.append(recover_li);
        recover_li.click(function (event) {
            //console.log("333");
            //恢复
            $(this).remove();
            foodDiary.foodDiaryDishes[index].status = true;


            var li = $(".img-container > ul").children().last();
            var hidden = li.children('[type="hidden"]');
            var sort = parseInt(hidden.val());
            if (foodDiary.foodDiaryDishes[index].sort < sort) {
                foodDiary.foodDiaryDishes[index].sort = sort + 1;
            }

            showList(ul, index, foodDiary.foodDiaryDishes[index]);

        });
    }

});

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

function formatDate(shoppingDate) {
    var shareDate = new Date(Number(shoppingDate.slice(6, -7)));
    var y = shareDate.getFullYear(), m = shareDate.getMonth() + 1, d = shareDate.getDate();
    m = m < 10 ? ("0" + m) : m;
    d = d < 10 ? ("0" + d) : d;
    var shareDateStr = y + "." + m + "." + d;
    return shareDateStr;
}

function sendDataHandler() {
    foodDiary.content = $("#content").val();
    window.getFoodDiaryDishes.runOnAndroidJavaScript(JSON.stringify(foodDiary));
}

function sendDataIosHandler() {
    foodDiary.content = $("#content").val();
    return JSON.stringify(foodDiary);
}
