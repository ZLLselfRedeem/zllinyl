(function () {
    var page = function () {
        this.employeeInfo = null;
        this.cityId = null;
		this.msg = "";
    };
	page.prototype.pageIndex = function () {
		var qs = this.getQueryStringArgs();
		$.cookie('c', qs.c); 
		$.cookie('t', qs.t); 

		var that = this;
        var bgList = $(".bgImg");
        var _width,
			_height;
        var ratioBgHeader = (357 / 640),
			ratioAward = (1 / 1);

        // 按触屏缩放
        doDraw(ratioBgHeader, ".bgImg");
        function doDraw(ratio, node) {
            var bgList = $(node);
            _width = window.innerWidth;
            _height = window.innerHeight - 20;
            for (var i = 0, len = bgList.length; i < len; i++) {
                scaleSpriteImage(bgList[i], _width, _height, ratio);
            }

        }
        function scaleSpriteImage(ImgD, _width, _height, ratio) {
            var h = Math.round(_width * ratio);
            ImgD.style.height = h + "px";
        }

        // 子元素，按父元素缩放；百分比固定宽度，后等比例得到高度值并设置；
        doParentDraw(ratioAward, ".bgAward");
        function doParentDraw(ratio, node) {
            var bgList = $(node);
            _width = bgList.width();
            for (var i = 0, len = bgList.length; i < len; i++) {
                scaleChildrenImage(bgList[i], _width, _height, ratio);
            }

        }
        function scaleChildrenImage(ImgD, _width, _height, ratio) {
            var h = Math.round(_width * ratio);
            ImgD.style.height = h + "px";
        }
		
        window.onresize = function () {
            doDraw(ratioBgHeader, ".bgImg");
            //doParentDraw(ratioAward, ".award");
        };
	};
    page.prototype.pageInit = function (callback) {
        // var qs = this.getQueryStringArgs();
        page.cookie = $.cookie('c');
		page.cityId = $.cookie('t');
        var ajaxOption = new Object();
        ajaxOption.type = 'post';
        ajaxOption.url = 'index.aspx/QueryEmployeeByCookie';
        ajaxOption.data = '{"cookie":"' + page.cookie + '"}'; //
        ajaxOption.contentType = "application/json; charset=utf-8";
        ajaxOption.dataType = 'json';
        ajaxOption.success = function (data) {
            var d = $.parseJSON(data.d);
            page.employeeInfo = d.employeeInfo[0].TableJson[0];
			
            callback();
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        $.ajax(ajaxOption);
    };

    /*
    * 积分商城
    * 未结算积分、已结算积分
    *
    */
    page.prototype.getScore = function () {
        var score = $("#score .num");
		var notSettlementPoint = Number(page.employeeInfo.notSettlementPoint),
			notSettlementPoint = Math.round(notSettlementPoint*100)/100;;
		var settlementPoint = Number(page.employeeInfo.settlementPoint),
			settlementPoint = Math.round(settlementPoint*100)/100;
			
        $(score[0]).html(notSettlementPoint);
        $(score[1]).html(settlementPoint);
    };

    page.prototype.getNewScoreLog = function () {
        var configObj = {};
        configObj.data = { "m": "point_lastLog", "employeeId": page.employeeInfo.EmployeeID };
        this.ajaxHandler(configObj, function (data) {
			var userName = data.TableJson[0].name;
			if(userName.length>5){
				userName = userName.slice(0,4)+"...";
			}
			var pointVariation = data.TableJson[0].pointVariation;
			if(Number(pointVariation)>=0){
				pointVariation = "+"+pointVariation;
			};
			$("#scoreUpdate").html(
			'<ul>'
			+'	<li><a href="javascript:;">'+userName+"  "+data.TableJson[0].remark+'</a></li>'
			+'	<li>'+data.TableJson[0].operateTime+'</li>'
			+'</ul>'
			+'<span class="val">'+pointVariation+'</span>');

        });
    };

    /*
    * 积分商城
    * 积分变动记录
    *
    */
	page.prototype.scoreAll = function(){
		var configObj = {};
        configObj.data = { "m":"point_log", "employeeId":page.employeeInfo.EmployeeID };
		this.ajaxHandler(configObj, function (data) {
			var d = data.TableJson;
			var html = "<ul>";
			for(var i=0,len=d.length;i<len;i++){
				var pointVariation = d[i].pointVariation;
				if(Number(pointVariation)>=0){
					pointVariation = '<span class="txtColor">+'+pointVariation+'</span>';
				};
				html +='<li>'
					+'<table class="txt" border="0" cellspacing="0" cellpadding="0">'
					+'	<tr>'
					+'		<td align="middle">'+d[i].operateTime
					+'		</td>'
					+'		<td align="middle">'+d[i].remark
					+'		</td>'
					+'		<td align="middle">'+pointVariation
					+'		</td>'
					+'	</tr>'
					+'</table>'
				+'</li>'
			}
			html += "</ul>";
			$("#scoreAll").append(html);
		});
		
	};
	

    /*
    * 积分商城
    * 商品列表
    *
    */
	page.prototype.getAward = function () {
		var configObj = {};
        configObj.data = { "m":"point_goods"};
		var that = this;
		this.ajaxHandler(configObj, function (data) {
			var d = data;

			var html = "",
				btnStr = "";
			for(var i=0,len=d.length;i<len;i++){
				if(Number(d[i].residueQuantity)>0){
					btnStr ='<li><a class="btn able" href="#popupLayout" data-position-to="window" data-transition="pop" data-rel="popup">立即兑换</a></li>';
				}else{
					btnStr ='<li><a class="btn disabled" href="javascript:;">抢完了</a></li>';
				};
				html = '<ul class="item">'
						+'<li class="pic"><a href="javascript:;"><img src="'+d[i].pictureName+'" /></a></li>'
						+'<li class="title">'
						+'	<table class="t" border="0" cellspacing="0" cellpadding="0">'
						+'		<tr>'
						+'			<td align="middle">'+d[i].name
						+'			</td>'
						+'		</tr>'
						+'	</table>'
						+'</li>'
						+'<li class="score"><span class="txtColor">需要积分：'+d[i].exchangePrice+'分</span></li>'
						+'<li class="store">库存<span class="num">'+d[i].residueQuantity+'</span>件</li>'
						+ btnStr
					+'</ul>';
				$("#dataAward").append(html);
				$("#dataAward .item .btn:last").data("key",d[i]);
			};
			
			$("#dataAward .item .able").on("tap",function(evt){
				$("#popupLayout").blur();
				var d = $(this).data("key");
				var price = Number(d.exchangePrice),
					settlementPoint = Number(page.employeeInfo.settlementPoint);
				var htmlStr = "";
				if(price<=settlementPoint){
					$("#proName").text(d.name);
					$("#scoreNumber").text(price);
					$("#popupContent .abled").css("display","block").siblings().css("display","none");
					// url传参
					var url = $("#popupContent .btnSprite .ok").attr("href"),
						index = url.indexOf("?");
					if(index>-1){
						url = url.slice(0,index);
					}
					url = that.addURIParam(url,"id",d.id);
					$("#popupContent .btnSprite .ok").attr("href",url);
					
				}else{
					$("#popupContent .disabled").css("display","block").siblings().css("display","none");
				}
			});
		});
		
		
	};
	
	page.prototype.getValidateCode = function () {
		var phoneNumber = page.employeeInfo.phoneNumber;
		var p = phoneNumber.slice("0","3");
		p+= "****";
		p+= phoneNumber.slice("7");
		$("#phone").text(p);
		
		var that = this;
		
		var getValidateHandler = function(evt){
			var t = null,
				c = 30,
				e = evt.currentTarget;
			$(e).off();
			function setDelay(){
				$(e).text("获取验证码（ "+c+" 秒）");
				if(c==0){
					clearTimeout(t);
					$(e).text("重新获取验证码");
					$("#getValidate").on("click",getValidateHandler);
					return;
				}
				c -= 1;
				t = setTimeout(arguments.callee,1000);
			};
			setDelay();
			var configObj = {};
			configObj.data = { "m":"point_exchangeRequest","phoneNumber":phoneNumber};
			that.ajaxHandler(configObj, function (data) {
				//
			});
		};
		
		$("#getValidate").on("click",getValidateHandler);

		
		$("#btnSubmit").on("click",function(evt){
			var verificationCode = $("#getCode").val();
			var qs = that.getQueryStringArgs();
			var configObj = {};
			configObj.data = { "m":"point_exchangeConfirm","phoneNumber":phoneNumber,"verificationCode":verificationCode,"goodsId":qs.id};
			that.ajaxHandler(configObj, function (data) {
				var d = data,
					msg = "";
				//
				if(d==1){
					window.location.href="scoresuccess.html";
				}else{
					switch(d){
						case -7:
							msg = "商品很紧俏，已经没有库存了！";
						break;
						case -6:
							msg = "兑换失败，可重新兑换操作！";
						break;
						case -5:
							msg = "可用积分不足，无法兑换当前商品！";
						break;
						case -4:
							msg = "验证码错误，请检查验证码是否正确并重试！";
						break;
						case -3:
							msg = "验证码过期，请重新获取验证码后重试！";
						break;
						case -2:
							msg = "用户手机号未绑定，请绑定后重试！";
						break;
						case -1:
							msg = "兑换失败，验证码不能为空！";
							$.jqtimertips(msg);
							return;
						break;
						default:
						break;
					}
					
					var url = that.addURIParam("scorefailure.html","info",msg);
					window.location.href = url;
				}
			});
		});
		
	};
	page.prototype.getErrorMsg = function () {
		var qs = this.getQueryStringArgs();
		$("#msg").text(qs.info);
	};
	
	// point_exchangeLog
	page.prototype.getExchangeNote = function () {
		var employeeID = page.employeeInfo.EmployeeID;
		var configObj = {};
		configObj.data = { "m":"point_exchangeLog","employeeId":employeeID};
		this.ajaxHandler(configObj, function (data) {
			var d = data.TableJson,
				html = "";
			for(var i=0,len=d.length;i<len;i++){
				var status = "";
				if(d[i].exchangeStatus=="处理中"){
					status = ' class="txtColor"';
				}
				html += '<li>'
					+'<table class="txt" border="0" cellspacing="0" cellpadding="0">'
					+'	<tr>'
					+'		<td align="middle">'+d[i].operateTime+'</td>'
					+'		<td align="middle">'+d[i].name+'</td>'
					+'		<td align="middle">'
					+'			<span '+status+'>'+d[i].exchangeStatus+'</span>'
					+'		</td>'
					+'	</tr>'
					+'</table>'
				+'</li>';
			}
			
			$("#noteList").append(html);
		});
	};
		
	page.prototype.getRuleHtml = function () {
		var configObj = {};
        configObj.data = { "m":"point_html","cityId":page.cityId};
		var that = this;
		this.ajaxHandler(configObj, function (data) {
			var d = data;
			$("#rule").append(d.html);
		});
	};
	
    page.prototype.ajaxHandler = function (param, callback) {
        var ajaxOption = new Object();
        ajaxOption.type = 'POST';
        ajaxOption.url = 'ajax/clintPoint.ashx';
        ajaxOption.data = param.data;
        // ajaxOption.contentType = "application/json; charset=utf-8";  // XML
        // ajaxOption.dataType = 'json';                                // parse 解析出错
        ajaxOption.success = function (data) {
            var d = $.parseJSON(data);
            callback(d);
        };
        ajaxOption.error = function (XmlHttpRequest, textStatus, errorThrown) {
            //
        };
        $.ajax(ajaxOption);
    };
	page.prototype.addURIParam = function(url,name,value){ 
		url += (url.indexOf("?")==-1?"?":"&");
		url += encodeURIComponent(name)+"="+encodeURIComponent(value);
		return url;
	};
	page.prototype.getQueryStringArgs = function(){ 
		var qs = (location.search.length>0?location.search.substring(1):"");
		var args ={};
		//处理查询字符串
		var items = qs.split("&"),
			item = null,
			name = null,
			value = null;
		for(var i=0;i<items.length;i++){ 
			item = items[i].split("=");
			name = decodeURIComponent(item[0]);
			value = decodeURIComponent(item[1]);
			args[name]=value;
		}
		return args;
	};

	window['ScorePage'] = {};
	ScorePage = new page();
})();


 