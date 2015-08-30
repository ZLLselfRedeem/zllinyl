var view = new Class(function(querys){
	this.data = {};
	var m = querys.m || '';
	if ( m.length > 0 ){
		var mode = fs(contrast('../caches/' + m + '.json')).exist().then(function(){
			return require('../caches/' + m + '.json');
		}).fail(function(){
			return {};
		}).value();
		
		this.data.mode = mode || {};
	}else{
		this.data.mode = {};
	}
	
	return this.data;
});
module.exports = view;