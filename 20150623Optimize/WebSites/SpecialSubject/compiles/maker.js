var maker = new Class(function(querys, getforms){
	this.data = {};

	this.getdetail(querys, getforms);
	this.getlist();
	
	return this.data;
});

maker.add('getdetail', function(querys, getforms){	
	var m = querys.m || '';
	if ( m.length > 0 ){
		this.data.m = m;
		var mode = fs(contrast('../caches/' + m + '.json')).exist().then(function(){
			return require('../caches/' + m + '.json');
		}).fail(function(){
			return {};
		}).value();
		
		this.data.mode = mode || {};
	}else{
		this.data.m = '';
		this.data.mode = {};
	}
});

maker.add('getlist', function(){
	var path = contrast('../caches');
	this.data.files = fs(path, true).files().value();
});

module.exports = maker;