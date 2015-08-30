define(function(){
	return new Class(function(that, response){
		$('.time-delay').on('reload', function(){ window.location.reload(); });
		var w = $(window).width(),
			h = $(window).height();
		
		$('section').css({
			width: w + 'px',
			height: h + 'px'
		});
		
		that.sups.timer((response.totalSeconds - new Date().getTime()) / 1000);
	});
})