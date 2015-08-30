YUI.add('calendarCquery-plugin',function(Y){//self
	//plugin								 //[host]
	Y.Plugin.CalendarCquery = Y.Base.create('CalendarCqueryNS',Y.Plugin.Base,[],{
		initializer:function(){
			if (this.get('rendered')) {
                this.addData();
            } else {
				this.afterHostMethod('renderUI',this.addData);
            }
		},
		destructor: function () { 
			this.get('contentBox').remove(true);
		},
		addData:function(){
			var self = this;
			(function($) {
				var MAX_STAY = 28 * 24 * 3600 * 1000;
				var InteHotel = {	
					regCalendar: function() {
						var that = this;
						var start = $("#dateStr");
						var end = $("#dateEnd");
						//start.mod.aaa('sayHello!2222');
						this.modStartDate = start.regMod("calendar", "3.0", {
							options: {
								showWeek: true,
								minDate:self.get('minDate'),//beenDate
								maxDate:new Date().toStdDateString()
							}
						});

						this.modBackDate = end.regMod("calendar", "3.0", {
							options: {
								showWeek: true,
								reference: '#' + start[0].id,//
								minDate: start.value().toDate() ? start.value().toDate().addDays(1).toStdDateString() : new Date().addDays(1).toStdDateString(),
								maxDate: new Date().addDays(1).toStdDateString()
							}
						});
						
						// document.getElementById('dateStr').attachEvent("onpropertychange",function(){alert('111');});
						// document.getElementById("dateStr").addEventListener("input",function(){alert('111')});
						
						start.bind('change', function(e) {
							e = e || window.event;
							var target = e.target || e.srcElement;
							var startDate = target.value.toDate();
							if (startDate) {
								var nextDate = startDate.addDays(0);
								end.data('minDate', nextDate.toStdDateString());
								nextDate = nextDate.addDays(0);
								var endDate = end.value().toDate();
								if (!endDate || endDate <= startDate) {
									//end.value(nextDate.toFormatString('yyyy-MM-dd'));
									end.value(new Date().toFormatString('yyyy-MM-dd'));//设置结束(初始日期)
									endDate = nextDate;
								}
								if (endDate && endDate - startDate > MAX_STAY) {
									//
								}
							} else {
								end.removeData('minDate');
							}
						}.bind(this)).bind('focus', function() {
							if (this.timer) {
								clearTimeout(this.timer);
							}
						}.bind(this));

						end.bind('focus', function(e) {
							e = e || window.event;
							var target = e.target || e.srcElement;
							var startDate = start.value().toDate();
							var endDate = target.value.toDate();
							if (startDate && endDate && endDate - startDate > MAX_STAY) {
								//
							}
						}.bind(this)).bind('blur', function() {
							this.timer = setTimeout(function() {
								var startDate = start.value().toDate();
								var endDate = end.value().toDate();
								if (startDate && endDate && endDate > startDate) {
									if (endDate - startDate > MAX_STAY) {
										//
									}
								}
							}.bind(this), 100);
						}.bind(this));
					},
					init: function() {
						this.regCalendar();
					}
				};
				InteHotel.init();
			})(cQuery);
			
		}
	},{
		NS: 'CalendarCqueryNS',//host
		ATTRS:{
			minDate:{ 
				value:'1972-10-01'
			}
		}
	});
	
},'v1.0',{requires:['base-build','plugin','datatable','cQuery','calendar-3.0']});


