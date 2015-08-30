/********************************************优惠券图片相关代码*************************************************/
/// <summary>
/// 查看某个图片是不是已经在listCouponImageAndDish中存在
/// <summary>
function CheckIfImageIsExist(couponImageName) {
    var returnValue = false;
    $.each(listCouponImageAndDish, function (index, item) {
        if (item.couponImageName == couponImageName) {
            returnValue = true;
        }
    });
    return returnValue;
}
/// <summary>
/// 根据条件，选出价格最低的菜，或者价格最高的菜
/// 选价格高的sort条件函数，这是array.sort方法的一个参数
/// <summary>
function sortproc(a, b) {
    if (a.dishPrice < b.dishPrice) {
        return a.dishPrice;
    }
}
function ChoseShowThumbnail(queryType) {
    if (listCouponImageAndDish.length > 0) {
        listCouponImageAndDish.sort(sortproc); //排序
        for (var i = 0; i < listCouponImageAndDish.length; i++) {
            listCouponImageAndDish[i].couponImageSequence = 1; //全部重置图片显示顺序标志
        }
        if (queryType == "lowest") {
            if (listCouponImageAndDish[listCouponImageAndDish.length - 1].couponImageScale == 1) {//如果第一张是小图
                listCouponImageAndDish[listCouponImageAndDish.length - 1].couponImageSequence = 0; //重置图片显示顺序标志，档couponImageSequence=0时，悠先显示
            }
            else {
                listCouponImageAndDish[listCouponImageAndDish.length - 2].couponImageSequence = 0; //显示倒数第二张图
            }
        }
        else if (queryType == "highest") {
            if (listCouponImageAndDish[0].couponImageScale == 1) {
                listCouponImageAndDish[0].couponImageSequence = 0;
            }
            else {
                listCouponImageAndDish[1].couponImageSequence = 0;
            }
        }
    }
}
//选择默认缩略图
function ChooseDefaultThumbnail(couponImageName, liSelect) {

    for (var i = 0; i < listCouponImageAndDish.length; i++) {
        if (listCouponImageAndDish[i].couponImageName == couponImageName && listCouponImageAndDish[i].couponImageScale == 1) {
            listCouponImageAndDish[i].couponImageSequence = 0;
        }
        else {
            listCouponImageAndDish[i].couponImageSequence = 1;
        }

    }
    $("#" + liSelect).append("<div class='thumbnailSelectedTag'><img src='../Images/checked.gif'/></div>");
    $("#" + liSelect).addClass("couponThumbnailSelected");
    if ($("#" + liSelect).siblings().length > 0) {
        $("#" + liSelect).siblings().children(".thumbnailSelectedTag").remove();
        $("#" + liSelect).siblings().removeClass('couponThumbnailSelected');
    }
}
//点击删除图片按钮
function DeleteImageButtonClick(couponImageName) {
    for (var i = 0; i < listCouponImageAndDish.length; i++) {
        if (listCouponImageAndDish[i].couponImageName == couponImageName) {
            if (listCouponImageAndDish[i].dishPriceI18nID != "0") {//如果这个图片是菜的图片，则不能直接从listCouponImageAndDish中删除。而是把这条记录的couponImageName设置成空
                listCouponImageAndDish[i].couponImageName = "";
                listCouponImageAndDish[i].couponImagePath = "";
                listCouponImageAndDish[i].couponImageStatus = "0";
            }
            else {//如果是客户添加的图片，则直接删除
                listCouponImageAndDish.splice(i, 1);
            }
            if (listCouponImageAndDish[i].couponImageScale == "1") {
                $($('#FileUpload_CouponThumbnail')[0].parentElement).css("display", 'block');
            }
        }
    }
    ShowCouponImage();
}
function setcookievalue(sname, svalue) {
    var expire = "";
    expire = new Date((new Date()).getTime() + 31536000);
    expire = "; expires=" + expire.toGMTString();
    document.cookie = sname + "=" + escape(svalue) + expire;
}
//显示删除图片按钮
function ShowDeleteImageButton(liSelect, couponImageName) {
    $(liSelect).parent().append("<div class='deleteImageButton'  onmouseout=\"HiddenDeleteImageButton(this)\"  onclick=\"DeleteImageButtonClick('" + couponImageName + "')\"><img src='../Images/deleteImage.png'/></div>");
}
//隐藏删除图片按钮
function HiddenDeleteImageButton(liSelect) {
    $(".deleteImageButton").remove();
}
/// <summary>
/// 显示图片
/// <summary>
function ShowCouponImage() {
    $("#div_smallCouponImage").html('');
    $("#div_bigCouponImage").html('');
    if (listCouponImageAndDish.length > 0) {
        var strHtmlBig = "<ul>";
        var strHtmlSmall = "<ul>";
        //var imageRootPath = $("#Label_ImageRootPath").text();
        var imageRootPath = "../UploadFiles/Images/";
        var listImage = new Array(); //用来存放已经显示出来的图片，如果图片已经显示，则不重复显
        var ifHaveDefaultThumbnail = false;
        $.each(listCouponImageAndDish, function (inx, item) {
            if (item.couponImageName != "") {//图片名称不能为空
                var isImageExist = false; //判断图片是不是已经显示过
                for (var i = 0; i < listImage.length; i++) {
                    if (item.couponImageName == listImage[i]) {
                        isImageExist = true;
                    }
                }
                if (!isImageExist) {
                    listImage.push(item.couponImageName);
                    if (item.couponImageScale == 1) {//小图
                        strHtmlSmall += "<li id='thumbnailId" + inx + "' onclick=\"ChooseDefaultThumbnail('" + item.couponImageName + "','thumbnailId" + inx + "');\"><img onmouseover=\"ShowDeleteImageButton(this,'" + item.couponImageName + "');\" width='100' height='75'  src='" + imageRootPath + item.couponImagePath + "'/></li>";
                    }
                    if (item.couponImageScale == 0) {
                        strHtmlBig += "<li><img onmouseover=\"ShowDeleteImageButton(this,'" + item.couponImageName + "');\" width='192' height='144' src='" + imageRootPath + item.couponImagePath + "'/></li>";
                    }
                }
            }
        })
        strHtmlBig += "</ul>";
        strHtmlSmall += "</ul>";
        $("#div_smallCouponImage").append(strHtmlSmall);
        $("#div_bigCouponImage").append(strHtmlBig);
        //如果没有设置默认的缩略图，则默认把第一张图片设置成默认缩略图
        if (!ifHaveDefaultThumbnail) {
            if (listCouponImageAndDish[0].couponImageName != "") {

                ChooseDefaultThumbnail(listCouponImageAndDish[0].couponImageName, $("#div_smallCouponImage li:first").attr("id"));
            }
        }
    }
    else {

    }
}
/// <summary>
/// 上传图片
/// <summary>
function UploadImage(imageScale) {
    if (imageScale == "1") {//小图
        $('#FileUpload_CouponThumbnail').uploadifyUpload(); //上传图片
    }
    else if (imageScale == "0") { //大图
        $('#FileUpload_CouponImage').uploadifyUpload(); //上传图片
    }
}
/// <summary>
/// 让不在可视范围内的图片，稍后加载
/// <summary>
function LazyShowImage() {
    var divImageHeight = $("#contentImageStorage").height();
    var divImageTop = $("#contentImageStorage").offset().top;
    var dishImage = $("#ImageStorage .imageStorageImage");
    for (var i = 0; i < dishImage.length; i++) {
        var imageTop = $(dishImage[i]).offset().top; //距离网页顶部的高度
        var totalHeight = divImageHeight + divImageTop;
        //alert("imageTop" + imageTop + "totalHeight" + totalHeight);
        if (Number(imageTop) < Number(totalHeight)) {
            var imagePath = $(dishImage[i]).attr("data-original");
            $(dishImage[i]).attr("src", imagePath);

            $(dishImage[i]).css("width", 200);
            $(dishImage[i]).css("height", 150);

        }
    }
}
///// <summary>
///// 打开图库
///// <summary>
//function SelectCouponImage(imageScale) {
//    var buttons = $("#divDish :button"); //按钮name中有
//    $("#ImageStorage").html('');
//    var strImage = "<ul>";
//    var imageRootPath = $("#Label_ImageRootPath").text();
//    var listImage = new Array(); //用来存放已经显示出来的图片，如果图片已经显示，则不重复显
//    for (var i = 0; i < buttons.length; i++) {
//        var couponImageAndDishs = JsonDeserialize(buttons[i].name);
//        for (var j = 0; j < couponImageAndDishs.length; j++) {
//            if (couponImageAndDishs[j].couponImageScale == imageScale) {
//                //判断这个图片是不是已经显示过
//                var isImageExist = false;
//                $.each(listImage, function (inx, item) {
//                    if (item == couponImageAndDishs[j].couponImageName) {
//                        isImageExist = true;
//                    }
//                })
//                if (!isImageExist) {
//                    listImage.push(couponImageAndDishs[j].couponImageName);
//                    //如果这个图片在listCouponImageAndDish中已经存在，则把li的样式设置成选中状态
//                    if (CheckIfImageIsExist(couponImageAndDishs[j].couponImageName)) {
//                        //strImage += "<li><div style='position:absolute;top:0;right:0'><img style='width:34px;height:34px;border:none;' src='../Images/gou.png'/></div><img  class='imageStorageImage'  onload=\"ImageLoadComplate(this,'imageStorageSpan_" + i + j + "');\" src=\"" + imageRootPath + couponImageAndDishs[j].couponImagePath + "\" onclick=\"ChooseCouponImageFromStorage(this,'" + couponImageAndDishs[j].couponImagePath + "','" + couponImageAndDishs[j].couponImageScale + "','" + couponImageAndDishs[j].couponImageName + "');\"/><span id='imageStorageSpan_" + i + j + "'>图片加载中……</span></li>";
//                        strImage += "<li><div style='position:absolute;top:0;right:0'><img style='width:34px;height:34px;border:none;' src='../Images/gou.png'/></div><img  class='imageStorageImage'   data-original=\"" + imageRootPath + couponImageAndDishs[j].couponImagePath + "\" onclick=\"ChooseCouponImageFromStorage(this,'" + couponImageAndDishs[j].couponImagePath + "','" + couponImageAndDishs[j].couponImageScale + "','" + couponImageAndDishs[j].couponImageName + "');\"/></li>";
//                    }
//                    else {
//                        //strImage += "<li><img class='imageStorageImage' onload=\"ImageLoadComplate(this,'imageStorageSpan_" + i + j + "');\" src=\"" + imageRootPath + couponImageAndDishs[j].couponImagePath + "\" onclick=\"ChooseCouponImageFromStorage(this,'" + couponImageAndDishs[j].couponImagePath + "','" + couponImageAndDishs[j].couponImageScale + "','" + couponImageAndDishs[j].couponImageName + "');\"/><span id='imageStorageSpan_" + i + j + "'>图片加载中……</span></li>";
//                        strImage += "<li><img class='imageStorageImage' data-original=\"" + imageRootPath + couponImageAndDishs[j].couponImagePath + "\" onclick=\"ChooseCouponImageFromStorage(this,'" + couponImageAndDishs[j].couponImagePath + "','" + couponImageAndDishs[j].couponImageScale + "','" + couponImageAndDishs[j].couponImageName + "');\"/></li>";
//                    }
//                }
//            }
//        }
//    }
//    $("#ImageStorage").append(strImage);
//    //获取弹出框的位置
//    if (imageScale == "0") {
//        $("#contentImageStorageLabel").html("&nbsp;&nbsp;请点击选择详情图");
//    }
//    else if (imageScale == "1") {
//        $("#contentImageStorageLabel").html("&nbsp;&nbsp;请点击选择缩略图");
//    }
//    ShowDivInTheCentral("contentImageStorage");
//    LazyShowImage();
//}

/// <summary>
/// 打开指定上传图片的图库(wangcheng)
///大图小图上传指定不同的文件夹。
/// <summary>
function SelectCouponImage(imageScale) {
    //
    var tbsource = $("#Label_CompanyImagePath").text() + "Coupon/1/"; //小图路径
    var tbsourcebig = $("#Label_CompanyImagePath").text() + "Coupon/0/"; //大图路径
    //
    var imageRootPath = $("#Label_ImageRootPath").text();
    //上传存放图片的完整路径,后面加上文件名得到图片。
    var imageAllPath = imageRootPath + tbsource; //小图完整路径
    var imageAllPathbig = imageRootPath + tbsourcebig; //大图完整路径
    $("#ImageStorage").html('');
    //判断点击的是浏览大图还是小图
    if (imageScale == "1") {
        $.ajax({
            type: "post",
            url: "CouponAdd.aspx/GetcompanyImagePathName",
            contentType: "application/json; charset=utf-8",
            data: "{'tbsource' :'" + tbsource + "'}",
            dataType: "json",
            success: function (data) {
                $().ready(function () {
                    //document.getElementById("ImageStorage").innerHTML = strImage;
                    // var obj = eval('(' + this + ')');
                    for (var i = 0; i < data.d.length; i++) {
                        if (!CheckIfImageIsExist(data.d[i])) {
                            // alert(data.d[i].toString());                  
                            strImage = "<ul><li><div style='position:absolute;top:0;right:0'></div><img  class='imageStorageImage' data-original=\"" + imageAllPath + data.d[i] + "\"  onclick=\"ChooseCouponImageFromStorage(this,'" + tbsource + data.d[i] + "','" + imageScale + "','" + data.d[i] + "');\"/></li></ul>";
                            $("#ImageStorage").append(strImage);

                        }
                        else {
                            strImage = "<ul><li><div style='position:absolute;top:0;right:0'><img style='width:34px;height:34px;border:none;' src='../Images/gou.png'/></div><img  class='imageStorageImage' data-original=\"" + imageAllPath + data.d[i] + "\"  onclick=\"ChooseCouponImageFromStorage(this,'" + tbsource + data.d[i] + "','" + imageScale + "','" + data.d[i] + "');\"/></li></ul>";
                            $("#ImageStorage").append(strImage);
                        }
                    }

                    $("#contentImageStorageLabel").html("&nbsp;&nbsp;请点击选择缩略图");
                    ShowDivInTheCentral("contentImageStorage");
                    LazyShowImage();
                });
            },
            error: function (err) {
                // ShowResult("图片量为空，请先选择上传图片");
                alert(err);
            }
        });
    }
    else if (imageScale == "0") {
        $.ajax({
            type: "post",
            url: "CouponAdd.aspx/GetcompanyImagePathName",
            contentType: "application/json; charset=utf-8",
            data: "{'tbsource' :'" + tbsourcebig + "'}",
            dataType: "json",
            success: function (data) {
                $().ready(function () {
                    for (var i = 0; i < data.d.length; i++) {
                        if (!CheckIfImageIsExist(data.d[i])) {
                            strImage = "<ul><li><div style='position:absolute;top:0;right:0'></div><img  class='imageStorageImage' data-original=\"" + imageAllPathbig + data.d[i] + "\"  onclick=\"ChooseCouponImageFromStorage(this,'" + tbsourcebig + data.d[i] + "','" + imageScale + "','" + data.d[i] + "');\"/></li></ul>";
                            $("#ImageStorage").append(strImage);
                        }
                        else {
                            strImage = "<ul><li><div style='position:absolute;top:0;right:0'><img style='width:34px;height:34px;border:none;' src='../Images/gou.png'/></div><img  class='imageStorageImage' data-original=\"" + imageAllPathbig + data.d[i] + "\"  onclick=\"ChooseCouponImageFromStorage(this,'" + tbsourcebig + data.d[i] + "','" + imageScale + "','" + data.d[i] + "');\"/></li></ul>";
                            $("#ImageStorage").append(strImage);
                        }
                    }
                    $("#contentImageStorageLabel").html("&nbsp;&nbsp;请点击选择详情图");
                    ShowDivInTheCentral("contentImageStorage");
                    LazyShowImage();
                });
            },
            error: function (err) {
                alert(err);
            }
        });
    }
}

/// <summary>
/// 每个图片加载完成，隐藏“图片加载中……”
/// 显示图片
///这个方法以及弃用
/// <summary>
function ImageLoadComplate(img, span) {
    $(img).css("display", "block");
    $("#" + span).css("display", "none");
}
/// <summary>
/// 从图片库中选择图片
/// <summary>
function ChooseCouponImageFromStorage(div, couponImagePath, couponImageScale, couponImageName) {
    if (!CheckIfImageIsExist(couponImageName)) {
        var imageAndDish = new CouponImageAndDish();
        imageAndDish.dishName = "";
        imageAndDish.dishId = 0;
        imageAndDish.couponId = 0;
        imageAndDish.couponImageAddDishId = 0;
        imageAndDish.dishPriceI18nID = 0;
        imageAndDish.scaleName = "";
        imageAndDish.dishDescDetail = "";
        imageAndDish.dishDescShort = "";
        imageAndDish.dishPrice = 0;
        imageAndDish.couponImageName = couponImageName;
        imageAndDish.couponImagePath = couponImagePath;
        imageAndDish.couponImageScale = couponImageScale;
        imageAndDish.couponImageSequence = 1;
        imageAndDish.couponImageStatus = 1;
        imageAndDish.dishQuantity = 0;
        listCouponImageAndDish.push(imageAndDish);
        //让已经选择的图片加上一个对勾
        $(div).parent().append("<div style='position:absolute;top:0;right:0'><img style='width:34px;height:34px;border:none;' src='../Images/gou.png'/></div>");
        ShowCouponImage(); //显示图片
        //SelectCouponImage(couponImageScale);
    }
}