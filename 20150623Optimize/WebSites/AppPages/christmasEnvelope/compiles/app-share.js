define(function() {
    return new Class(function(that, response) {
        that.sups.addRule(response.activityRule);
        that.sups.snow();
        that.sups.music();
        //alert(response.isGet)
        that.sups.button('免费给好友发红包', Library.proxy(that.sups.share, that.sups));
        $('.snow .text').append('<div class="col-xs-12 instr1">（点击分享链接，自己也可以领一个哦）</div>');
    });
})

