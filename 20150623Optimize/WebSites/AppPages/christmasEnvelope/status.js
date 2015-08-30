define(
	['jquery', 'event'],
function($, events, require, exports, module) {

    var Event = new events();
    var status = new Class(function(response, that) {
    //alert(response.status)
    //alert('text:' + response.shareImage)
        that.sups.shareContent(response);
        if (this[response.status]) {
            this[response.status].call(that, response);
        }
    });

    status.add('3', function(response) {
        this.fetch('countdown', response, Event);
    });

    status.add('0', function(response) {
        // 节日红包
        if (response.activityType === 3) {
            // app端
            if (this.sups.enit.ap) {
                if (response.isGet) {
                    this.fetch('getted', response, Event);
                } else {
                    this.fetch('app-share', response, Event);
                }
            }
            // 微信端
            else if (this.sups.enit.wx) {
                if (response.isGet) {
                    this.fetch('getted', response, Event);
                } else {
                    this.fetch('wx-share', response, Event);
                }
            }
        }
    });

    status.add('1004', function(response) {
        if (this.sups.enit.wx) {
            this.fetch('getted', response, Event);
        } else {
            if (response.isGet) {
                this.fetch('getted', response, Event);
            } else {
                this.fetch('app-share', response, Event);
            }
        }
    });

    status.add('4', function(response) {
        if (this.sups.enit.wx) {
            this.fetch('getted', response, Event);
        } else {
            this.fetch('app-share', response, Event);
        }
    });

    status.add('2', function(response) {
        this.fetch('gameover', response, Event);
    });

    status.add('1007', function(response) {
        alert(response.context);
    });

    status.add('-6', function(response) {
        alert(response.context);
    });

    return status;

});