function HistoryManager() {
	this.listener = null;
    this.adapterIframe = null;
    this._initialize();
}

(function() {
	var flag = false,
		isIE = !!window.ActiveXObject && /msie (\d)/i.test(navigator.userAgent) ? RegExp['$1'] : false,
		$pointer = this;
	
	this.makeIEHistory = function(url) {
		if (!url) {
			return ;
		}
		
		var frameDoc = $pointer.adapterIframe.contentWindow.document;
		
		frameDoc.open();
		frameDoc.write([
			"<html>",
				"<head>",
					"<script type='text/javascript'>",
						"function pageLoaded() {",
							"try {top.window.historyManager.fireOnHashChange(\""+url+"\");} catch(ex) {}",
						"}",
					"</script>",
				"</head>",
				"<body onload='pageLoaded();'>",
					"<input type='value' value='"+url+"' id='history'/>",
				"</body>",
			"</html>"
		].join(""));
		frameDoc.title = document.title;
		frameDoc.close();
	}

	this.fireOnHashChange = function(url) {
		location.hash = "#" + url.replace(/^#/, "");
		
		if (window.onhashchange) {
			window.onhashchange();
		}
	}

	this.add = function(url) {
		flag = true;
		if (isIE && isIE < 8&& isIE>5) {// isIE当为10时
			$pointer.makeIEHistory(url);
		} else {
			location.hash = "#" + url;
		}
	}

	this.fire = function(url) {
		if (!url) {
			url = document.location.hash.slice(1);
		}
		$pointer.listener(url);
	}

	this.addListener = function(fn){
		$pointer.listener = typeof fn === 'function' ? fn : function() {};
	};

	this._initialize = function() {
		if (isIE && isIE < 8) {
			$pointer.adapterIframe = document.getElementById("HISTORY_ADAPTER");
			$pointer.makeIEHistory();
		}

		window.onhashchange = function() {
			if (flag) {
				flag = false;
				return ;
			}

			$pointer.fire();
		}
	}

}).call(HistoryManager.prototype);


var historyManager = new HistoryManager();//  ie7 contentWin 对象要求处于页面内(即不能处于匿名函数或沙箱内)实例化
historyManager.addListener(function(){
	var url = arguments[0];
	if(url==''){
		var okHandler = function(){
			window.location.href = 'login.aspx';
		};
		var cancelHandler = function(){
			//
		};
		YUI().use('cookie','node-base','node-style','node-event-delegate','io-base','json-parse','node-load',function(Y){
			VA.Singleton.popup.panel.set('headerContent','提示信息');
			VA.Singleton.popup.panel.set('bodyContent','您确认要退出悠先收银宝系统!');
			VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton'),VA.Singleton.popup.get('cancelButton')]);
			VA.Singleton.popup.set('ok',okHandler);
			VA.Singleton.popup.set('cancel',cancelHandler);
			VA.Singleton.popup.showPanel();
		});
		return;
	}
	setHashHistory(url);
});

var mainNav = document.getElementById("mainNav");
mainNav.onclick = function(ev) {
	ev = ev || window.event;
	var elem = ev.srcElement || ev.target;

	if (elem.tagName && elem.tagName.toLowerCase() == "a") {
		if (ev.preventDefault) {
			ev.preventDefault();	
		} else {
			ev.returnValue = false;
		}
		var href = elem.getAttribute("href", 2);
		href = menu.getType(href)+".aspx";
		historyManager.add(href);
		
	}
};
function setHashHistory(_url) {
	var m = menu.getType(_url);
	YUI().use('cookie','node-base','node-style','node-event-delegate','io-base','json-parse','node-load',function(Y){
		Y.all('#mainNav a').each(function(element) {
			if(m == menu.getType(element.getAttribute('href'))){
				element.ancestor().addClass('cur');
				element.ancestor().siblings().removeClass('cur');
			};
		});
	});
	menu.tab(_url,m);
	menu.navIcon();
};
