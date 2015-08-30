YUI.add('CouponsChart', function (Y) {
	Y.CouponsChartClass = Y.Base.create('CouponsChartNS', Y.Widget, [], {
		destructor: function () { 
			this.get('containerBox').remove(true);
		},
		dataSuccess:function(rsp){ 
			var result = Y.JSON.parse(rsp.responseText);
			var d = Y.JSON.parse(result.d);

			var dataSend = [],
				date = [],
				dateMonth = [],
				used = [],
				grant = [],
				dateKey = [];
				
			var getDurDays = function(str,end){ 
				var mmms = end.getTime()-str.getTime();
				return (mmms / 3600000 / 24); 
			};
			var dateStrStart = this.get('theStrTime'),
				dateStrEnd = this.get('theEndTime'),
				duraDay = getDurDays(new Date(dateStrStart),new Date(dateStrEnd)),
				startTimeArr = dateStrStart.split('/');
			var y = startTimeArr[0]-0,
				m = startTimeArr[1]-1,
				day = startTimeArr[2]-0;
			
			for(var i=0;i<=duraDay;i++){ 
				var tempDay = new Date(y,m,day+i);
				var cates = Y.DataType.Date.format(tempDay, { format: '%Y/%m/%d' });//时区切换 GMT 8
				date.push(cates);
				dateMonth.push(cates);
			};
			var getDataArr = function(len,d){ 
				var index = [],
					value = [],
					arr = [];
				for(var i=0;i<len;i++){ 
					var count = d[i].count,
						dateStr = d[i].time;
					var time = new Date(dateStr.replace(/-/g,'/')).getTime();	
					for(var j=0;j<=duraDay;j++){ 
						var tempDay = new Date(date[j]);
						var tempTime = tempDay.getTime();
						if(time==tempTime){ 
							index.push(j);
							value.push(count);
						}
					}
				}
				for(var m=0;m<=duraDay;m++){ 
					arr.push(0);
				}
				for(var n=0;n<index.length;n++){ 
					arr.splice(index[n],1,value[n]);
				}
				return arr;
			};

			used = getDataArr(d.couponUsedInfo[0].TableJson.length, d.couponUsedInfo[0].TableJson);
			grant = getDataArr(d.couponGrantInfo[0].TableJson.length, d.couponGrantInfo[0].TableJson);
			var dataProvider = [];
			if(duraDay<=7){ 
				for(var i=0;i<date.length;i++){ 
					dataProvider.push({"日期":date[i],"使用数":used[i],"发放数":grant[i]});
				}
			}else{ 
				for(var i=0;i<dateMonth.length;i++){ 
					dataProvider.push({"日期":dateMonth[i],"使用数":used[i],"发放数":grant[i]});
				}
			}
			Y.one('#couponChart').empty();
			var styleDef = {
				axes:{
					values:{
						label:{
							rotation:-45,
							color:"#949494"
						}
					},
					'日期':{
						label:{
							rotation:-45,
							color: "#949494"
						}
					}
				},
				series:{
					international:{
						marker:{
							fill:{
								color:"#ff8888"
							},
							border:{
								color:"#ff0000"
							},
							over:{
								fill:{
									color:"#ffffff"
								},
								border:{
									color:"#fe0000"
								},
								width: 12,
								height: 12
							}
						},
						line:{
							color:"#ff0000"
						}
					},
					expenses:{
						line:{
							color:"#999"
						},
						marker:
						{
							fill:{
								color:"#ddd"
							},
							border:{
								color:"#999"
							},
							over: {
								fill: {
									color: "#eee"
								},
								border: {
									color: "#000"
								},
								width: 12,
								height: 12
							}
						}
					},
					domestic: {
						marker: {
							over: {
								fill: {
									color: "#eee"
								},
								width: 12,
								height: 12
							}
						}
					}
				}
			};
			
			var chartLine = new Y.Chart({
				dataProvider:dataProvider,
				categoryKey:"日期",
				width:906,
				height:230,
				styles:styleDef, 
                horizontalGridlines:true, 
                verticalGridlines:true, 
				render:"#couponChart"
			});
		},
		renderUI:function(){ 
			
		},
		bindUI:function(){ 
			var self = this,
				cur = 0,
				d = new Date();
			var w = d.getDay(),
				m = d.getMonth()+1,
				y = d.getFullYear();

			var soonDays = 6-w,
				beenDays = w;
			strTime = new Date(y,m-1,d.getDate()-beenDays);
			endTime = new Date(y,m-1,d.getDate()+soonDays);
			strTime = Y.DataType.Date.format(strTime, { format: '%Y/%m/%d' });
			endTime = Y.DataType.Date.format(endTime, { format: '%Y/%m/%d' });
			this.set('theStrTime',strTime);
			this.set('theEndTime',endTime);
				
			Y.one('#days').delegate('click',function(e){
				e.preventDefault();
				e.stopPropagation();
				var t = e.currentTarget;
				var phase = Number(t.getAttribute('name')),
					status = 0;
				var week = Y.one('#statusDate .week').getAttribute('class');
				var month = Y.one('#statusDate .month').getAttribute('class');
				
				if(week.indexOf('cur')>-1){ 
					status = 2;
				}else if(month.indexOf('cur')>-1){ 
					status = 1;
				};
				
				if(status==1){ 
					var curMonthDays = new Date(y, m-1+cur, 0).getDate();
					if(phase==1){ //上一月
						--cur;
						strTime = new Date(y,m-1+cur,1);
						endTime = new Date(y,m-1+cur,curMonthDays);
					}else if(phase==2){ //下一月
						++cur;
						strTime = new Date(y,m-1+cur,1);
						endTime = new Date(y,m-1+cur,curMonthDays);
					}
				}
				if(status==2){ 
					if(phase==1){ //上一周
						--cur;
						strTime = new Date(y,m-1,d.getDate()-beenDays+7*cur);
						endTime = new Date(y,m-1,d.getDate()+soonDays+7*cur);
					}else if(phase==2){ //下一周
						++cur;
						strTime = new Date(y,m-1,d.getDate()-beenDays+7*cur);
						endTime = new Date(y,m-1,d.getDate()+soonDays+7*cur);
					}					
				}
				
				strTime = Y.DataType.Date.format(strTime, { format: '%Y/%m/%d' });
				endTime = Y.DataType.Date.format(endTime, { format: '%Y/%m/%d' });
				self.set('theStrTime',strTime);
				self.set('theEndTime',endTime);
			},'a',this);
			Y.one('#statusDate').delegate('click',function(e){
				e.preventDefault();
				e.stopPropagation();
				cur = 0;
				var t = e.currentTarget;
				t.ancestor('li').addClass('cur');
				t.ancestor('li').siblings().removeClass('cur');
					
				var status = Number(t.getAttribute('name'));
				var d = new Date(),
					m = d.getMonth(),
					y = d.getFullYear();
				
				if(status==1){
					var curMonthDays = new Date(y, m-2, 0).getDate();
					strTime = new Date(y,m,1);
					endTime = new Date(y,m,curMonthDays);
				}else if(status==2){
					strTime = new Date(y,m,d.getDate()-beenDays);
					endTime = new Date(y,m,d.getDate()+soonDays);
				}
				strTime = Y.DataType.Date.format(strTime, { format: '%Y/%m/%d' });
				endTime = Y.DataType.Date.format(endTime, { format: '%Y/%m/%d' });
				self.set('theStrTime',strTime);
				self.set('theEndTime',endTime);
			},'a',this);
			
			this.after('theEndTimeChange', this.syncUI, this);
		},
		syncUI:function(){
			var self = this;
			this.qs = this.get('queryString');
			var ioHandler = {
				method:'POST',
				data:'{"strTime":"'+this.get('theStrTime')+'","endTime":"'+this.get('theEndTime')+'","couponId":"'+this.qs.b+'"}',
				headers: { 'Content-Type':'application/json; charset=utf-8' },
				on:{ 
					success:function(id,rsp){
						self.dataSuccess(rsp);
					},
					failure: function (id, rsp) {
						Y.log(rsp.status);
					}
				},
				sync:true
			};
			Y.io(this.get('ioURL'),ioHandler);
		}
	},{//
        ATTRS: {
			ioURL:{
				value:''
			},
			theStrTime:{ 
				value:''
			},
			theEndTime:{ 
				value:''
			},
			dataTemp:{ 
				value:''
			},
			contentBox:{ 
				value:'#chart .chartContent'
			},
			queryString:{ 
				value:''
			}
		}
	});
}, '1.0', {requires: ['base-build','node-base','widget','io-base','json-parse','charts']});