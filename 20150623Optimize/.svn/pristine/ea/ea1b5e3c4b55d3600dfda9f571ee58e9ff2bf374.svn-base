function getCompile(file, callback){
	require(['compiles/' + file], callback);
}
define(
	['jquery', 'status', 'support'],
	true,
function($, status, support, require, exports, module) {
var main = new Class(function() {
        this.sups = new support();
        this.install();
    });

    main.add('install', function() {
        this.sends({
            "m": "pageload",
            "activityId": this.sups.req.activityId,
            "mobilePhoneNumber": this.sups.params.phone,
            "cookie": this.sups.params.cookie
        });
    });

    main.add('slide', function(callback) {
        var active = $('section.active'),
	        unactive = $('section:not(.active)');

        unactive.addClass('active');
        active.remove();
        typeof callback === 'function' && callback();
        //$('#shape').velocity({ rotateY: '-=180deg' }, 600 , function(){ 
        //	var active = $('section.active'),
        //		unactive = $('section:not(.active)');

        //	setTimeout(function(){
        //		unactive.addClass('active');
        //		active.remove();
        //		$('#shape').velocity({ rotateY: '0deg' }, 0, callback);
        //	}, 0);
        //});
    });

    main.add('sends', function(data) {
        var that = this;
        $.ajax({
            url: this.sups.root + 'TreasureChestHandler.ashx',
            type: 'POST',
            dataType: 'json',
            data: data,
            async: true,
            success: function(response) { new status(response, that); },
            error: function() {
                if (that.sups.isRetry == true) {
                    window.setTimeout(function() {
                        that.send(data);
                    }, 1000);
                    that.sups.isRetry = false;
                } else {
                    that.fetch('beng');
                }
            }
        });
    });

    main.add('fetch', function(mark, response) {
        var that = this;
        $.get('views/' + mark + '.asp', function(html) {
            $('#shape').append(html);
            getCompile(mark, function(v) {
                that.slide(function() {
                    if (typeof v === 'function') {
                        new v(that, response);
                    }
                });
            });
        });
    });

    return main;
});

