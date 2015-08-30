jQuery.jqtimertips = function(text, title, fn, timerMax){
	var dd = $(
    '<div class="dialog tips-message">' +
    '  <p>' +
    '    <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 18px 7px 0 7px;"></span>' + text +
    '  </p>' +
    '</div>');
    dd[0].timerMax = timerMax || 1;
    return dd.dialog({
      //autoOpen: false,
      resizable: false,
      //modal: true,
      show: {
        effect: 'fade',
        duration: 300
      },
	  hide: {
        effect: 'fade',
        duration: 300
      },
      open: function(e, ui) {
        var me = this,
          dlg = $(this),
          btn = dlg.parent().find(".ui-button-text").text("确定(" + me.timerMax + ")");
		dlg.parent().find(".ui-dialog-titlebar").css("display","none"); 
		dlg.parent().find(".ui-dialog-buttonpane").css("display","none"); 
		
        --me.timerMax;
        me.timer = window.setInterval(function() {
          btn.text("确定(" + me.timerMax + ")");
          if (me.timerMax-- <= 0) {
            dlg.dialog("close");
            fn && fn.call(dlg);
            window.clearInterval(me.timer);
          }
        }, 1000);
      },
      title: title || "提示信息",
      buttons: {
        "确定": function() {
          var dlg = $(this).dialog("close");
          fn && fn.call(dlg);
          window.clearInterval(this.timer);
        }
      },
      close: function() {
        window.clearInterval(this.timer);
      }
    });
};