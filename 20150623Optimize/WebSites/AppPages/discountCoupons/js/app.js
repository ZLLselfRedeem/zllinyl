(function(){
	
	var 
		express = window.Express = _.noop,
		OJS = new ojs({ cache: true });
	
	express.prototype.listen = function(){
		this.req = this.request();
	};
	
	express.prototype.request = function(){
		var req = window.location.href.split('?');
		if ( req.length === 2 ){
			req = _.fromQuery(req[1])
		}else{
			req = {};
		};
		return req;
	};
	
	express.prototype.render = function(id, data){
		return OJS.render(id, data);
	};
	
	express.prototype.a
	
}).call(this);