/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.configurationPrinter = function(){
	YUI().use('dataTablePack','configurationPrinter-plugin',function(Y){
		menu.createTab("configurationPrinter");

		VA.renderPage = new Y.DataTableClass({pageType:'configurationPrinter',shopId:VA.argPage.loginId.get('id'),queryString:VA.argPage.qs,ioURL:'ajax/doSybWeb.ashx?m=merchants_printer_config_query'});
		VA.renderPage.plug(Y.Plugin.ConfigurationPrinter);
		VA.renderPage.render();
		
	});
};


/* 
 * editStatus
	新增	1
	修改	2
	删除	3
 * 
 *
 */
YUI.add('configurationPrinter-plugin', function (Y) {
    Y.Plugin.ConfigurationPrinter = Y.Base.create('configurationPrinter', Y.Plugin.Base, [], {
        initializer: function () {
            this.initBind();
            if (this.get('rendered')) {
                this.addData();
            } else {
                this.afterHostMethod('dataSuccess', this.addData);
            }
        },
        destructor: function () {
            this.titleNode.remove(true);
        },
		btnHandler:function(e){ 
			e.preventDefault();
			e.stopPropagation();
			var host = this.get('host');
            var contentTable = Y.one("#printerData"),
				count = id = contentTable.get('childNodes')._nodes.length;
			
			var t = e.target;
			var eClass = t.getAttribute('class'),
				isAdd = eClass.indexOf('add')>-1,
				isSave = eClass.indexOf('save')>-1;
			
			if(isAdd){ 
				var msg = Y.one('.yui3-datatable-message');
				if(msg) msg.hide();
				
				var isOpen 				= true,			// 是否开启
				printerName 			= '自定义打印机1',			// 打印机名称
				printIp 				= '192.168.1.100',			// IP
				printCopies 			= 1,			// 份数
				serialPrintPaperWidth 	= 530,			// 纸张宽度
				thirdFontSize 			= 3,			// 抬头字体大小
				secondFontSize 			= 2,			// 小标题字体大小
				fontSizeHeight 			= 1,			// 正文字体大小
				serialPrintLineFeed 	= 30,			// 行距
				isPrintPrice 			= 0,			// 是否显示单价
				isPrintTotal 			= 1,			// 是否显示小计
				serialPrintLeftBlank 	= 20,			// 页面左边距
				serialPrintSecondTab 	= 335,			// 显示数量左边距
				serialPrintThirdTab 	= 420,			// 显示单价左边距
				serialPrintFourthTab 	= 480,			// 显示小计左边距
				
				employeeId 				= 0,				
				shopId 					= host.get('shopId'),					
				status 					= 0,					 
				// id 						= 0,			// 打印机标识id
				printInterface 			= 0;			// 打印机模式
				
				/*
					// gray
				var bgGray;
				if(count%2==0){
					bgGray = ' gray';
				}else{
					bgGray = '';
				}
				*/
				var openY = isOpen ?'checked="checked" ':'';
				var openN = !isOpen ?'checked="checked" ':'';
				var priceY = isPrintPrice ?'checked="checked" ':'';
				var priceN = !isPrintPrice ?'checked="checked" ':'';
				var totalY = isPrintTotal ?'checked="checked" ':'';
				var totalN = !isPrintTotal ?'checked="checked" ':'';
				
				
				
				var dataRow = '<tr class="itemSprite">'
						+'<td colspan="2" class="item">'
						+'	<p>'
						+'		<strong><em>&gt;&gt;</em>开启：</strong><span class="radioSprite"><input type="radio" value="1" '+openY+' name="isOpen_add_'+id+'" /> 是</span>'
						+'		<span class="radioSprite"><input type="radio" value="0" '+openN+' name="isOpen_add_'+id+'" /> 否</span>'
						+'	</p>'
						+'	<p>'
						+'		<label class="title leftTitle" for="name_'+id+'">打印机名称：</label><span class="inputSprite"><input type="text" class="inputText printerName_text" maxlength=50 id="name_'+id+'" value="'+printerName+'" /></span>'
						+'		<label class="title" for="ip_'+id+'">IP：</label><span class="inputSprite"><input type="text" class="inputText ip_text" maxlength=50 id="ip_'+id+'" value="'+printIp+'" /></span>'
						+'		<label class="title" for="printCopies_'+id+'">份数：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="printCopies_'+id+'" value="'+printCopies+'" /></span>'
						+'		<label class="title" for="serialPrintPaperWidth'+id+'">纸张宽度：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintPaperWidth'+id+'" value="'+serialPrintPaperWidth+'" /></span><span class="unitName">（mm）</span>'
						+'		<label class="title" for="thirdFontSize'+id+'">抬头字号：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="thirdFontSize'+id+'" value="'+thirdFontSize+'" /></span>'
						+'		<label class="title" for="secondFontSize'+id+'">小标题字号：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="secondFontSize'+id+'" value="'+secondFontSize+'" /></span>'
						+'		<label class="title leftTitle" for="fontSizeHeight'+id+'">正文字号：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="fontSizeHeight'+id+'" value="'+fontSizeHeight+'" /></span>'
						+'		<label class="title" for="serialPrintLineFeed'+id+'">行距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintLineFeed'+id+'" value="'+serialPrintLineFeed+'" /></span>'
						+'		<label class="title" for="serialPrintLeftBlank'+id+'">页面左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintLeftBlank'+id+'" value="'+serialPrintLeftBlank+'" /></span><span class="unitName">（mm）</span>'
						
						+'	</p>'
						
						+'	<div class="aside">'
						+'		<label class="title leftTitle" for="isPrintPrice'+id+'">显示单价：</label><div class="radioCon"><span class="radioSprite"><input type="radio" value="1" '+priceY+' name="isPrintPrice_'+id+'" />是</span><span class="radioSprite"><input type="radio" value="0" '+priceN+' name="isPrintPrice_'+id+'" />否</span></div>'

						+'		<label class="title" for="isPrintTotal'+id+'">显示小计：</label><div class="radioCon"><span class="radioSprite"><input type="radio" value="1" '+totalY+' name="isPrintTotal_'+id+'" />是</span><span class="radioSprite"><input type="radio" value="0" '+totalN+' name="isPrintTotal_'+id+'" />否</span></div>'
						+'	</div>'
						
						+'	<div class="aside">'
						+'		<label class="title leftTitle" for="serialPrintSecondTab'+id+'">显示数量的左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintSecondTab'+id+'" value="'+serialPrintSecondTab+'" /></span><span class="unitName">（mm）</span>'	
						+'		<label class="title" for="serialPrintThirdTab'+id+'">显示单价的左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintThirdTab'+id+'" value="'+serialPrintThirdTab+'" /></span><span class="unitName">（mm）</span>'
						+'		<label class="title" for="serialPrintFourthTab'+id+'">显示小计的左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintFourthTab'+id+'" value="'+serialPrintFourthTab+'" /></span><span class="unitName">（mm）</span>'
						+'	</div>'
						
						+'</td>'
						+'<td><div class=""><a class="btn delete" name="" rel="1" href="configurationPrinter.aspx?id='+id+'&employeeId='+employeeId+'&shopId='+shopId+'&status='+status+'&printInterface='+printInterface+'">删除</a></div></td>'
						+'</tr>';
						
				//contentTable.prepend(dataRow);
				contentTable.append(dataRow);
				
			}else if(isSave){
				var host = this.get('host'),
					data = this.getData(),
					rsp = new Object();
				rsp = data;
				
				// 提交数据检测
				//var rsp,
				//var tipsLayout = dataPrice.all('.tip')._nodes;
					// rsp.printerName
					
					// rsp.printIp
					
					// 大于0的数字
				
				
				var rspStr = Y.JSON.stringify(rsp);
				var dataStr = 'm=merchants_printer_config_save&shopid='+host.get('shopId');
				dataStr += '&json='+rspStr;
				var dataHandler = {
					method: 'POST',
					data: dataStr,
					headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
					on: {
						success: function (id, rsp) {
							var res = Y.JSON.parse(rsp.responseText);
							var okHandler = function(){
								host.syncUI();
							}
							VA.Singleton.popup.panel.set('headerContent','打印机配置');
							VA.Singleton.popup.panel.set('bodyContent',res.list[0].info);
							VA.Singleton.popup.panel.set('buttons',[VA.Singleton.popup.get('okButton')]);
							VA.Singleton.popup.set('ok',okHandler);
							VA.Singleton.popup.showPanel();
						},
						failure: function (id, rsp) {
							Y.log(rsp.status);
						}
					}
				};
				Y.io('ajax/doSybWeb.ashx',dataHandler);
			};
		},
        initBind: function () {
			// 数据表格操作
			var host = this.get('host');
			Y.one('#printerData').delegate('click', host.checkSubmit, '.btn', host);
			// 表头操作
			Y.all('.configurationPrinter .printerHeader .btn').on('click',this.btnHandler,this);
        },
		getData:function(){
			var host = this.get('host');
			var contentTable = Y.one("#printerData"),
				nodes = contentTable.get('childNodes')._nodes,
				count = id = contentTable.get('childNodes')._nodes.length;
				
			var data = [];
			for(var i=0;i<count;i++){
				var inputs 			= Y.one(nodes[i]).all('input')._nodes,
					isOpen 			= inputs[0].checked?1:0,
					
					isPrintPrice 	= inputs[11].checked?1:0,
					isPrintTotal 	= inputs[13].checked?1:0;
				//获取静态数据
				var btn = Y.one(nodes[i]).one('.btn');
				var btnData = UIBase.getNyQueryStringArgs(btn.getAttribute('href'));	

				data.push({
					'isOpen':isOpen,
					'isPrintPrice':isPrintPrice,
					'isPrintTotal':isPrintTotal,
					
					'id':btnData.id,//
					'employeeId':btnData.employeeId,
					'shopId':btnData.shopId,
					'status':btnData.status,
					'printInterface':btnData.printInterface,
					
					'printerName':inputs[2].value,
					'printIp':inputs[3].value,
					'printCopies':inputs[4].value,
					'serialPrintPaperWidth':inputs[5].value,
					'thirdFontSize':inputs[6].value,
					'secondFontSize':inputs[7].value,
					'fontSizeHeight':inputs[8].value,
					'serialPrintLineFeed':inputs[9].value,
					'serialPrintLeftBlank':inputs[10].value,
					'serialPrintSecondTab':inputs[15].value,	// 显示数量左边距
					'serialPrintThirdTab':inputs[16].value,		// 显示单价左边距
					'serialPrintFourthTab':inputs[17].value,	// 显示小计左边距
					
					'editStatus':btn.getAttribute('rel')
					
				});
			}
			return data;
		},
        addData: function () {
            var self = this;
			var host = this.get('host');

            var d = host.get('dataTemp'),
				dataRow = '';

			for(var i=0;i<d.length;i++){
				var isOpen 				= d[i].isOpen,					// 是否开启
				printerName 			= d[i].printerName,				// 打印机名称
				printIp 				= d[i].printIp,					// IP
				printCopies 			= d[i].printCopies,				// 份数
				serialPrintPaperWidth 	= d[i].serialPrintPaperWidth,	// 纸张宽度
				thirdFontSize 			= d[i].thirdFontSize,			// 抬头字体大小
				secondFontSize 			= d[i].secondFontSize,			// 小标题字体大小
				fontSizeHeight 			= d[i].fontSizeHeight,			// 正文字体大小
				serialPrintLineFeed 	= d[i].serialPrintLineFeed,		// 行距
				isPrintPrice 			= d[i].isPrintPrice,			// 是否显示单价
				isPrintTotal 			= d[i].isPrintTotal,			// 是否显示小计
				serialPrintLeftBlank 	= d[i].serialPrintLeftBlank,	// 页面左边距
				serialPrintSecondTab 	= d[i].serialPrintSecondTab,	// 显示数量左边距
				serialPrintThirdTab 	= d[i].serialPrintThirdTab,		// 显示单价左边距
				serialPrintFourthTab 	= d[i].serialPrintFourthTab,	// 显示小计左边距
				
				employeeId 				= d[i].employeeId,				
				shopId 					= d[i].shopId,					
				status 					= d[i].status,					 
				id 						= d[i].id,						// 打印机标识id
				printInterface 			= d[i].printInterface;			// 打印机模式
				
				/* 数据检测 */
					// gray
					// 
				var openY = isOpen ?'checked="checked" ':'';
				var openN = !isOpen ?'checked="checked" ':'';
				var priceY = isPrintPrice ?'checked="checked" ':'';
				var priceN = !isPrintPrice ?'checked="checked" ':'';
				var totalY = isPrintTotal ?'checked="checked" ':'';
				var totalN = !isPrintTotal ?'checked="checked" ':'';
				
				dataRow += '<tr class="itemSprite">'
						+'<td colspan="2" class="item">'
						+'	<p>'
						+'		<strong><em>&gt;&gt;</em>开启：</strong><span class="radioSprite"><input type="radio" value="1" '+openY+' name="isOpen_'+id+'" /> 是</span>'
						+'		<span class="radioSprite"><input type="radio" value="0" '+openN+' name="isOpen_'+id+'" /> 否</span>'
						+'	</p>'
						+'	<p>'
						/*
						+'		<span class="required"><span class="tip">需要输入<i class="txt">打印机名称</i>!<i class="close">x</i></span></span>'
						*/
						+'		<label class="title leftTitle" for="name_'+id+'">打印机名称：</label><span class="inputSprite"><input type="text" class="inputText printerName_text" maxlength=50 id="name_'+id+'" value="'+printerName+'" /></span>'
						+'		<label class="title" for="ip_'+id+'">IP：</label><span class="inputSprite"><input type="text" class="inputText ip_text" maxlength=50 id="ip_'+id+'" value="'+printIp+'" /></span>'
						+'		<label class="title" for="printCopies_'+id+'">份数：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="printCopies_'+id+'" value="'+printCopies+'" /></span>'
						+'		<label class="title" for="serialPrintPaperWidth'+id+'">纸张宽度：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintPaperWidth'+id+'" value="'+serialPrintPaperWidth+'" /></span><span class="unitName">（mm）</span>'
						+'		<label class="title" for="thirdFontSize'+id+'">抬头字号：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="thirdFontSize'+id+'" value="'+thirdFontSize+'" /></span>'
						+'		<label class="title" for="secondFontSize'+id+'">小标题字号：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="secondFontSize'+id+'" value="'+secondFontSize+'" /></span>'
						+'		<label class="title leftTitle" for="fontSizeHeight'+id+'">正文字号：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="fontSizeHeight'+id+'" value="'+fontSizeHeight+'" /></span>'
						+'		<label class="title" for="serialPrintLineFeed'+id+'">行距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintLineFeed'+id+'" value="'+serialPrintLineFeed+'" /></span>'
						+'		<label class="title" for="serialPrintLeftBlank'+id+'">页面左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintLeftBlank'+id+'" value="'+serialPrintLeftBlank+'" /></span><span class="unitName">（mm）</span>'
						
						+'	</p>'
						
						+'	<div class="aside">'
						+'		<label class="title leftTitle" for="isPrintPrice'+id+'">显示单价：</label><div class="radioCon"><span class="radioSprite"><input type="radio" value="1" '+priceY+' name="isPrintPrice_'+id+'" />是</span><span class="radioSprite"><input type="radio" value="0" '+priceN+' name="isPrintPrice_'+id+'" />否</span></div>'

						+'		<label class="title" for="isPrintTotal'+id+'">显示小计：</label><div class="radioCon"><span class="radioSprite"><input type="radio" value="1" '+totalY+' name="isPrintTotal_'+id+'" />是</span><span class="radioSprite"><input type="radio" value="0" '+totalN+' name="isPrintTotal_'+id+'" />否</span></div>'
						+'	</div>'
						
						+'	<div class="aside">'
						+'		<label class="title leftTitle" for="serialPrintSecondTab'+id+'">显示数量的左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintSecondTab'+id+'" value="'+serialPrintSecondTab+'" /></span><span class="unitName">（mm）</span>'	
						+'		<label class="title" for="serialPrintThirdTab'+id+'">显示单价的左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintThirdTab'+id+'" value="'+serialPrintThirdTab+'" /></span><span class="unitName">（mm）</span>'
						+'		<label class="title" for="serialPrintFourthTab'+id+'">显示小计的左边距：</label><span class="inputSprite"><input type="text" class="inputText" maxlength=8 id="serialPrintFourthTab'+id+'" value="'+serialPrintFourthTab+'" /></span><span class="unitName">（mm）</span>'
						+'	</div>'
						
						+'</td>'
						+'<td><div class=""><a class="btn delete" name="" rel="2" href="configurationPrinter.aspx?id='+id+'&employeeId='+employeeId+'&shopId='+shopId+'&status='+status+'&printInterface='+printInterface+'">删除</a></div></td>'
						+'</tr>';
				
			}
			var contentBox = Y.one("#printerData");
			contentBox.empty(true);
			contentBox.append(dataRow);
			
        }
    }, {
        NS: 'configurationPrinter',
        ATTRS: {
            dataTemp111: { value: '' }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message','io-base','json-stringify'] });


