(function(mod){ define([], mod);})(function(){
	return new Class(function(section, next){
		var h = $(window).height();
		$(section).css('height', h + 'px').find('.backBtn, .closeBtn').on('click', function(){ window.closeview(1); });
		$('#dopay').on('click', function(){ MEAL.open(16, -999); });
		var lasttime = $('#lasttime');
		if ( lasttime.size() > 0 ){ 
			lasttime.html(section.time); 
			$(section).find('.backBtn, .closeBtn').hide();
		};
		next();
	});
});