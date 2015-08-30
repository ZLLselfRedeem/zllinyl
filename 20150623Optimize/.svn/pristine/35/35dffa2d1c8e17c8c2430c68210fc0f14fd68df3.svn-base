var post = new Class(function(querys, getforms){
	var x = getforms().data;
	var forms = JSON.parse(x);

	if ( forms.file && forms.file.length > 0 ){
		var status = false;
		fs(contrast('../caches/' + forms.file + '.json')).create(x).then(function(){
			status = true;
		}).fail(function(){
			status = false;
		});
	}
	
	if ( status ){
		return { success: true, message: '保存缓存成功' };
	}else{
		return { success: true, message: '保存缓存失败' };
	}
});

module.exports = post;