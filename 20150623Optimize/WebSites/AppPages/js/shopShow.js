﻿define(function () {
    var date = {};
    date.format = function (DateObject, type) {

        if (Object.prototype.toString.call(DateObject).split(" ")[1].toLowerCase().replace("]", "") !== "date") {
            DateObject = new Date(DateObject);
        }

        var date = DateObject,
			year = (date.getFullYear()).toString(),
			_month = date.getMonth(),
			month = (_month + 1).toString(),
			day = (date.getDate()).toString(),
			hour = (date.getHours()).toString(),
			miniter = (date.getMinutes()).toString(),
			second = (date.getSeconds()).toString(),
			_day, _year;

        var dateArray = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

        month = month.length === 1 ? "0" + month : month;
        _day = day;
        day = day.length === 1 ? "0" + day : day;
        hour = hour.length === 1 ? "0" + hour : hour;
        miniter = miniter.length === 1 ? "0" + miniter : miniter;
        second = second.length === 1 ? "0" + second : second;

        return type.replace(/y/g, year)
				.replace(/m/g, month)
				.replace(/d/g, day)
				.replace(/h/g, hour)
				.replace(/i/g, miniter)
				.replace(/s/g, second)
				.replace(/D/g, _day)
				.replace(/M/g, dateArray[_month]);

    }

    var shopShow = new Class(function () {
        this.req = this.createServer();
        this.installData = {};
        this.getContent();
        this.scrolls();
    });

    shopShow.add('scrolls', function () {
        var that = this;
        $(window).on('scroll', function () {
            var top = $('body').scrollTop();
            var h = $('body').outerHeight();
            if (($(window).height() + top + 50) >= h && window.isMore && !window.doing) {
                that.getContent(window.page + 1);
            }
        });
    })

    shopShow.add('getContent', function (page) {
        var that = this;
        if (!page) { page = 1; }
        if (window.doing) {
            return;
        };
        window.doing = true;
        window.page = page;
        $.ajax({
            contentType: "application/json",
            url: 'shopShow.aspx/GetShopInfo',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify({ "shopID": this.req.shopId, "pageIndex": page, "pageSize": 10, "customerId": req.customerId }),
            success: function (msg) {
                msg = JSON.parse(msg.d);
                window.doing = false;
                window.isMore = msg.isMore;
                try {
                    if (page === 1) {
                        if (msg.publicityPhotoPath.length > 0) {
                            $('#shoplogo').attr('src', msg.publicityPhotoPath);
                        }

                        var shopLevels = that.level(msg.shopLevel);
                        for (var i = 0; i < shopLevels[1]; i++) {
                            $('.drillShow').append('<img src="http://image.u-xian.com/UploadFiles/Images/15/grade/level-' + shopLevels[0] + '.png" />');
                        }

                        $('#storeAdd').html(msg.shopAddress);
                        $('#tel').html(msg.shopTelephone);
                        $('#time').html(msg.openTimes);
                        $('#prepayOrderCount').html(msg.goodEvaluationCount);
                        that.evaluationPercent(msg.evaluationPercent);
                    }
                    that.comments(msg.evaluationList);
                } catch (e) { }
            },
            error: function (e) {
                window.doing = false;
                console.log('服务端出错');
            }
        });
    });

    shopShow.add('createServer', function () {
        var qs = (window.location.search.length > 0 ? location.search.substring(1) : "");
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
    });

    shopShow.add('level', function (lv) {
        var icons = ['s', 'd', 'g'], deep = [], indexs = 0, length = 1;
        icons.forEach(function (o, i) { deep.push(Math.pow(6, i + 1)); });
        if (lv > 0) {
            for (var i = 0; i < deep.length; i++) {
                if (Math.floor(lv / deep[i]) < (i === 0 ? 1 : i)) {
                    indexs = i;
                    length = Math.floor(lv / deep[i - 1 < 0 ? 0 : i - 1]);
                    if (length === 0) { length = 1; }
                    break;
                }
            }
        }

        //临时解决办法
        if (lv < 6) { length = lv; }

        return [icons[indexs], length];
    });

    shopShow.add('evaluationPercent', function (m) {
        var a, b, c;
        for (var i = 0; i < m.length; i++) {
            if (m[i].evaluationValue === 1) {
                a = m[i].percent * 100;
            };
            if (m[i].evaluationValue === 0) {
                b = m[i].percent * 100;
            };
            if (m[i].evaluationValue === -1) {
                c = m[i].percent * 100;
            };
        };

        a = a.toFixed(2);
        b = b.toFixed(2);
        c = c.toFixed(2);

        $('.eav_good').css('width', a + '%');
        $('.eav_normall').css('width', b + '%');
        $('.eav_bad').css('width', c + '%');

        $("#eva_good_per").html(a + '%');
        $("#eva_normall_per").html(b + '%');
        $("#eva_bad_per").html(c + '%');
    });

    shopShow.add('comments', function (list) {
        list.forEach(function (o) {
            var h = '';
            h += '<li class="col-xs-12">';
            h += '<span class="phoneNum">' + o.mobilePhoneNumber + '</span>';
            if (o.evaluationValue === 1) {
                h += '<span class="eva good">好评</span>';
            } else if (o.evaluationValue === 0) {
                h += '<span class="eva normall">中评</span>';
            } else if (o.evaluationValue === -1) {
                h += '<span class="eva bad">差评</span>';
            }
            h += '<span class="thatdate pull-right">' + date.format(new Date(o.evaluationDate * 1000), 'y.m.d') + '</span><br />';
            h += '<span class="evaContent">' + o.evaluationContent + '</span>';
            h += '</li>';
            $('#comments').append(h);
        });
    });

    return shopShow;
});