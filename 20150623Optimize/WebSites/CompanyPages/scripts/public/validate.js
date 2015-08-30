/*//
标题：收银宝 UIModule
来源：viewallow UI
日期：2013/9/19

* @class validate
* @fileOverview 校验控制
* @version
* @date 2013-11-14

//*/

// import uibase.js
// 		
(function() {
	window['Validate'] = function() {
	    //EventBase.call( this );
		
		this.setLength = function(target,len){
			var isPass = true;
			var id = target.substring(1);
			var dom = document.getElementById(id);
			var inputText = dom.getElementsByTagName('input')[0];
			//设置长度限制
			inputText.maxLength = len?len:1000;
		};
		this.checkNumber = function(target){
			var isPass = true;
			var id = target.substring(1);
			var parent = document.getElementById(id);
			var inputText = parent.getElementsByTagName('input')[0];
			inputText = inputText?inputText:parent;
			//输入类型限制
			EventUtil.addHandler(inputText,'keypress',function(event){
				event = EventUtil.getEvent(event);
				var t = EventUtil.getTarget(event);
				var charCode = EventUtil.getCharCode(event);
				if(!/^\d+(\.\d{1,1})?$/.test(String.fromCharCode(charCode))&&charCode>9&&!event.ctrlKey){
					EventUtil.preventDefault(event);
				}

			});
			EventUtil.addHandler(inputText,'keyup',function(event){
				event = EventUtil.getEvent(event);
				var t = EventUtil.getTarget(event);
				var val = (t.value);
				t.value=val.replace(/[^\d+(\.\d{1,1})?$]/g,'') 
			});
			EventUtil.addHandler(inputText,'onbeforepaste',function(event){
				event = EventUtil.getEvent(event);
				var t = EventUtil.getTarget(event);
				t.clipboardData.setData('text',t.clipboardData.getData('text').replace(/^\d+(\.\d{1,1})?$/g,''))
			});
		};
		this.checkDate = function(target){
			var isPass = true;
			var id = target.substring(1);
			var inputText = document.getElementById(id);
			if(inputText.value==''){ return; };
			if(/^(\d{4}\-\d{1,2}\-\d{1,2})$/g.test(inputText.value)){
				isPass = false;
			}
			return isPass;
		};
	}
})();

