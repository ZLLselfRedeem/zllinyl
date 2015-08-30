/*!
 * tron maps.
 * the route of tronjs maps.
 * user script tags which insert into document.
 */
(function(){
	
	// tronjs online http url
	var website = '/tron';
	
	// check ie brower
	function isIE(){
		if(!!window.ActiveXObject || "ActiveXObject" in window){
			return true;
		}else{
			return false;
		}
	};
	
	//check ie version
	function IEVersion()
	{
	  var rv = -1;
	  if (navigator.appName == 'Microsoft Internet Explorer')
	  {
		var ua = navigator.userAgent;
		var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
		if (re.exec(ua) != null)
		  rv = parseFloat( RegExp.$1 );
	  }
	  else if (navigator.appName == 'Netscape')
	  {
		var ua = navigator.userAgent;
		var re  = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
		if (re.exec(ua) != null)
		  rv = parseFloat( RegExp.$1 );
	  }
	  return rv;
	};
	
	// make http modules url
	function resolveHTTP( file ){
		return website + '/coms/' + file;
	}
	
	// make jQuery http url
	function getjQueryResolveURI(){
		var jqueryPath = website + '/coms/jquery/';
		if (isIE() && IEVersion() <= 8) {
			jqueryPath += 'jquery-1.11.1/jquery-1.11.1.min.js';
		}else{
			jqueryPath += 'jquery-2.1.1/jquery-2.1.1.min.js';
		}
		return jqueryPath;
	}
	
	// on map modules
	Library
			.onMap('jquery', getjQueryResolveURI())
			.onMap('jquery-plugin-mousewheel', resolveHTTP('jquery-plugins/jquery.mousewheel/jquery.mousewheel.min.js'))
			.onMap('velocity', resolveHTTP('jquery-plugins/jquery.velocity/jquery.velocity.min.js'))
			.onMap('com-plugin-prettify', resolveHTTP('com-plugins/prettify/prettify.package.js'))
			.onMap('jquery-plugin-chosen', resolveHTTP('jquery-plugins/jquery.chosen/jquery.chosen.package.js'))
			.onMap('jquery-plugin-toastr', resolveHTTP('jquery-plugins/jquery.toastr/jquery.toastr.package.js'))
			.onMap('jquery-plugin-jbox', resolveHTTP('jquery-plugins/jquery.jbox/jquery.jbox.package.js'))
			.onMap('jquery-plugin-cookie', resolveHTTP('jquery-plugins/jquery.cookie/jquery.cookie.js'))
			.onMap('com-plugin-pace', resolveHTTP('com-plugins/pace/pace.package.js'))
			.onMap('jquery-plugin-nicescroll', resolveHTTP('jquery-plugins/jquery.nicescroll/jquery.nicescroll.min.js'))
			.onMap('jquery-plugin-lsotope', resolveHTTP('jquery-plugins/jquery.lsotope/isotope.package.js'))
			.onMap('jquery-plugin-sortable', resolveHTTP('jquery-plugins/jquery.sortable/jquery.sortable.min.js'))
			.onMap('jquery-plugin-webui-popover', resolveHTTP('jquery-plugins/jquery.webui-popover/jquery.webui-popover.package.js'))
			.onMap('com-plugin-codemirror', resolveHTTP('com-plugins/codemirror/codemirror.package.js'));
			
})();