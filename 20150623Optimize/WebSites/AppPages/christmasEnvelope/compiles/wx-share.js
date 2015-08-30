define(function() {
    var cookies = function(x) {
        if (typeof x === 'string') {
            return new Date(x);
        } else {
            return new Date(new Date().getTime() + x);
        }
    };
    var cookiexp = 365 * 24 * 60 * 60 * 1000;
    var share = new Class(function(that, response) {
        this.onEvent(that, response);
        that.sups.snow();
        that.sups.music();
        that.sups.addRule(response.activityRule);
        //$('.snow .text').append('<div class="col-xs-12 instr" style="font-size:20px;">(同一设备只能领一次)</div>');
    });

    share.add('onEvent', function(that, response) {
        $('#postShare').on('click', function() {
            var phone = $('#share-phone-number').val();
            if (!/^1[34578][0-9]{9}$/.test(phone)) {
                alert('手机号码格式不正确');
                return;
            }
            $.cookie("mobilePhoneNumber", phone, {
                expires: cookies(cookiexp)
            });
            that.sends({ "m": "shared", "redEnvelopeId": response.redEnvelopeId, "mobilePhoneNumber": phone });
        });
    });

    return share;
});