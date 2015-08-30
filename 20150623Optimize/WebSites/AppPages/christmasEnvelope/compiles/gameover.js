define(function(){
	return new Class(function(that, response){
		that.sups.snow();
		if ( that.sups.enit.wx ){
		that.sups.button('下载悠先', that.sups.download);
		$('.snow .text').append('<div class="col-xs-12 instr">下载APP，还可以领专属天天红包</div>');
}

		that.sups.addRule(response.activityRule);
		
		
		response.ranklist.forEach(function(o, i){
			var h = '';
			
			h += 	'<li class="clearfix">';
    		h += 	'<div class="rSquare rSquare' + (i + 1) + ' pull-right">' + (i + 1) + '</div>';
    		h += 		'<div class="text-left" style="margin-right: 76px;">';
    		h += 			'<span class="pNum">' + o.mobilePhoneNumber + '</span>';
    		h += 			'累积已领￥<span class="moneys">' + o.amount + '</span><br />';
    		h += 			'<span class="stext">' + o.context + '</span>';
    		h += 		'</div>';
    		h += 	'</li>';
			
			$('#rank').append(h);
		});
		
	});
});