define(function() {
    var cookies = function(x) {
        if (typeof x === 'string') {
            return new Date(x);
        } else {
            return new Date(new Date().getTime() + x);
        }
    };
    var cookiexp = 365 * 24 * 60 * 60 * 1000;
    return new Class(function(that, response) {


        that.sups.snow();
        that.sups.music();
        if (that.sups.enit.wx) {
            that.sups.button('下载悠先', that.sups.download);
            $('.snow .text').append('<div class="col-xs-12 instr">下载APP，还可以领专属天天红包</div>')
        } else {
            that.sups.button('免费给好友发红包', Library.proxy(that.sups.share, that.sups));
            if (!response.isGet) $('.snow .text').append('<div class="col-xs-12 instr1">（点击分享链接，自己也可以领一个哦）</div>');
        }
        
        that.sups.addRule(response.activityRule);


        $('#moneyText span').text(response.amount);
        $('#phoneNumber span').text(response.mobilePhoneNumber);
        $('#dcmobilePhone').val(response.mobilePhoneNumber);
        if (response.isChange) {
            $('#change').remove();
        }
        if (that.sups.enit.wx) {
            $('#promptShare').removeClass('hide');
        }




        $('#change').on('click', function() {
            try {
                $("#myModal").modal().css({
                    'margin-top': '0',
                    'padding-top': '0',
                    'top': '0',
                    'position': 'fixed',
                    'z-index': 9999
                });
            } catch (e) { alert(e.message) }
        });

        $('#button_cancel').on('click', function() {
            $("#myModal").modal('hide');
        });

        $('#button_smash').on('click', function() {
            if (window.doing) {
                return;
            }
            var val = $('#dcmobilePhone').val();
            var id = response.redEnvelopeId;
            if (!/^1[34578][0-9]{9}$/.test(val)) {
                alert('手机号码格式不正确');
                return;
            }
            doing = true;
            $.ajax({
                url: that.sups.root + 'TreasureChestHandler.ashx',
                type: 'POST',
                dataType: 'json',
                data: { "m": "modify", "redEnvelopeId": id, "mobilePhoneNumber": val },
                success: function(msg) {
                    window.doing = false;
                    if (msg.status == 0) {
                        $.cookie("mobilePhoneNumber", val, {
                            expires: cookies(cookiexp)
                        });
                        $('#phoneNumber').html(val);
                        $('#change').remove();
                        alert('修改号码成功');
                    } else if (msg.status === 1007 || msg.status === -10) {
                        alert("该号码已领过红包");
                    } else {
                        alert('修改失败');
                    }
                    $('#button_cancel').trigger('click');
                },
                error: function() {
                    alert('修改失败');
                    window.doing = false;
                }
            });
        });

        response.ranklist.forEach(function(o, i) {
            var h = '';

            h += '<li class="clearfix">';
            h += '<div class="rSquare rSquare' + (i + 1) + ' pull-right">' + (i + 1) + '</div>';
            h += '<div class="text-left" style="margin-right: 76px;">';
            h += '<span class="pNum">' + o.mobilePhoneNumber + '</span>';
            h += '累积已领￥<span class="moneys">' + o.amount + '</span><br />';
            h += '<span class="stext">' + o.context + '</span>';
            h += '</div>';
            h += '</li>';

            $('#rank').append(h);
        });


    });
});