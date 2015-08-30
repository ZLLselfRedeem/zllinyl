/*//
标题：商户宝 UIController
来源：viewallow UI
日期：2013/10/22
//*/

VA.initPage.dishMix = function () {
    YUI().use('dataTablePack', 'dishMix-plugin', function (Y) {
        menu.createTab("dishMix");

        VA.renderPage = new Y.DataTableClass({ pageType: 'dishMix', contentBox: '#dataTable', shopId: VA.argPage.loginId.get('id'), queryString: VA.argPage.qs, ioURL: 'ajax/doSybWeb.ashx?m=dish_ingredients_query' });
        VA.renderPage.plug(Y.Plugin.DishMix);
        VA.renderPage.render();
    });
};

YUI.add('dishMix-plugin', function (Y) {
    Y.Plugin.DishMix = Y.Base.create('dishMixPlugin', Y.Plugin.Base, [], {
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
        btnHandler: function (e) {
            e.preventDefault();
            e.stopPropagation();
            var that = this;
            var host = this.get('host');
            var contentBox = host.get('contentBox'),
				contentTable = contentBox.one('table tbody.yui3-datatable-data'),
				count = contentTable.get('childNodes')._nodes.length;

            var t = e.target;
            var eClass = t.getAttribute('class'),
				isAdd = eClass.indexOf('add') > -1,
				isSave = eClass.indexOf('save') > -1;
            if (isAdd) {
                var msg = Y.one('.yui3-datatable-message');
                if (msg) msg.hide();
                var dataRow = '<tr>'
						+ '	<td class="yui3-datatable-col-ingredientsName  yui3-datatable-cell">名称：<input type="text" value="" class="inputText" maxlength="50"></td>'
						+ '	<td class="yui3-datatable-col-ingredientsPrice"><div class="inputCont"><span class="tip">请输入<i class="txt">正确数值</i>（最多两位小数）!<i class="close">x</i></span>价格（￥）：<input type="text" value="0" class="inputText" maxlength="8" /></div></td>'
						+ '<td class="yui3-datatable-col-vipDiscountable"><span class="title">vip折扣：</span><span class="discount"><input type="radio" name="vipadd' + count + '" checked="checked" value="1">是</span><span class="discount"><input type="radio" name="vipadd' + count + '" value="0"> 否</span></td>'
						//+'	<td class="yui3-datatable-col-backDiscountable"><span class="title">支持返送：</span><span class="discount"><input type="radio" name="discountadd'+count+'" checked="checked" value="">是</span><span class="discount"><input type="radio" name="discountadd'+count+'" value=""> 否</span></td>'
						+ '	<td class="yui3-datatable-col-ingredientsSequence">排序：<input class="inputText" maxlength="8" type="text" value="1"></td>'
						+ '	<td class="yui3-datatable-col-btn" ><div class="disMixBtn"><div class="txt-tips-sprite"><i class="txt-tips">删除配菜成功！</i></div><a rel="1" name="" href="dishMix.aspx?id=0" class="btn delete">删除</a></div></td>'
						+ '</tr>';
                contentTable.prepend(dataRow);
            } else if (isSave) {
                Y.one('#add-txt-tips').ancestor().setStyles({ "position": "relative", "top": -36 });
                var txtTips = Y.one('#add-txt-tips').setStyles({ "display": "block", "left": 650, "opacity": 0 });
                var tips = popup.tips(txtTips);

                var host = this.get('host'),
					p = host.get('dataPage'),
					data = this.getData(),
					rsp = new Object();
                rsp.list = data;
                rsp.page = p;

                for (var i = 0, len = rsp.list.length; i < len; i++) {
                    if (!rsp.list[i].isPass) {
                        txtTips.set('text', '输入配菜数据不正确!');
                        tips.stop().run();
                        return;
                    }
                }
                var rspStr = Y.JSON.stringify(rsp);
                var dataStr = 'm=dish_ingredients_save&shopid=' + host.get('shopId');
                dataStr += '&json=' + rspStr;
                var dataHandler = {
                    method: 'POST',
                    data: dataStr,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' },
                    on: {
                        success: function (id, rsp) {
                            var res = Y.JSON.parse(rsp.responseText);
                            if (res.list[0].status == 1) {
                                txtTips.set('text', res.list[0].info);
                                tips.stop().run();
                            } else {
                                that.okHandler = function () {
                                    //
                                };
                                VA.Singleton.popup.panel.set('headerContent', '配菜管理');
                                VA.Singleton.popup.panel.set('bodyContent', res.list[0].info);
                                VA.Singleton.popup.panel.set('buttons', [VA.Singleton.popup.get('okButton')]);
                                VA.Singleton.popup.set('ok', that.okHandler);
                                VA.Singleton.popup.showPanel();
                            }
                            host.syncUI();
                        },
                        failure: function (id, rsp) {
                            Y.log(rsp.status);
                        }
                    }
                };
                Y.io('ajax/doSybWeb.ashx', dataHandler);
            };
        },
        initBind: function () {
            var btnSprite = Y.one('.dishMix .headTable');
            btnSprite.append('<div class="txt-tips-sprite"><i class="txt-tips" id="add-txt-tips">添加配菜成功！</i></div>');
            btnSprite.all('.btn').on('click', this.btnHandler, this);
        },
        getData: function () {
            var host = this.get('host');
            var contentBox = host.get('contentBox'),
				contentTable = contentBox.one('table tbody.yui3-datatable-data'),
				nodes = contentTable.get('childNodes')._nodes,
				count = contentTable.get('childNodes')._nodes.length;
            var data = [];
            for (var i = 0; i < count; i++) {
                var inputs = Y.one(nodes[i]).all('input')._nodes,
					vip = inputs[2].checked ? true : false;
                //back =inputs[4].checked ? true : false;
                //获取静态数据
                var btn = Y.one(nodes[i]).one('.delete');
                var btnData = UIBase.getNyQueryStringArgs(btn.getAttribute('href'));
                var ingredientsPrice = inputs[1].value ? inputs[1].value : 0;
                var operStatus = btn.getAttribute('rel');

                var tipsLayout = Y.one(nodes[i]).all('.tip')._nodes;
                var isPass = true;
                if (!/^\d+((\.{1})\d{1,2})?$/.test(ingredientsPrice) && operStatus != '3') {
                    // "请输入正确数值（最多两位小数）";
                    tipsLayout[0].style.display = 'block';
                    Y.all('.close').on('click', function (e) {
                        var t = e.currentTarget;
                        t.ancestors('.tip').hide();
                    }, this);
                    isPass = false;
                };

                data.push({
                    'backDiscountable': true,
                    'ingredientsId': btnData.id,//
                    'ingredientsName': inputs[0].value,
                    'ingredientsPrice': ingredientsPrice,
                    'ingredientsSequence': inputs[4].value,//
                    'operStatus': operStatus,
                    'vipDiscountable': vip,
                    'isPass': isPass
                });
            }
            return data;
        },
        addData: function () {
            var self = this;
            var host = this.get('host');

            var contentBox = host.get('contentBox'),
				data = [],
				d = host.get('dataTemp');
            data = d;
            var nameHandler = function (o) {
                return '名称：<input type="text" class="inputText" maxlength="50" value="' + o.value + '" />';
            };
            var priceHandler = function (o) {
                return '<div class="inputCont"><span class="tip">请输入<i class="txt">正确数值</i>（最多两位小数）!<i class="close">x</i></span>价格（￥）：<input type="text" class="inputText" maxlength="8" value="' + o.value + '" /></div>';
            };
            var vipHandler = function (o) {
                var vipYes = o.data.vipDiscountable ? 'checked="checked" ' : '';
                var vipNo = !o.data.vipDiscountable ? 'checked="checked" ' : '';
                return '<span class="title">vip折扣：</span><span class="discount"><input type="radio" value="1" ' + vipYes + ' name="vip' + o.data.ingredientsId + '" />是</span><span class="discount"><input type="radio" value="0" ' + vipNo + ' name="vip' + o.data.ingredientsId + '" /> 否</span>';
            };

            //var backHandler = function (o) {
            //	var backYes = o.data.backDiscountable?'checked="checked" ':'';
            //	var backNo = !o.data.backDiscountable?'checked="checked" ':'';
            //    return '<span class="title">支持返送：</span><span class="discount"><input type="radio" value="1" '+backYes+' name="discount'+o.data.ingredientsId+'" />是</span><span class="discount"><input type="radio" value="0" '+backNo+' name="discount'+o.data.ingredientsId+'" /> 否</span>';
            //};
            var sequenceHandler = function (o) {
                return '排序：<input type="text" class="inputText" maxlength="8" value="' + o.value + '" />';
            };
            var btnHandler = function (o) {
                var html = '<div class="disMixBtn" >';
                if (o.data.isSellOff == false) {
                    html += ' <a class="btn btnSoon" rel="' + o.data.operStatus + '" id=' + o.data.ingredientsId + ' onclick="dishingredientsselloff(this);" name="notselloff" href="dishMix.aspx?id=' + o.data.ingredientsId + '">沽清</a>';
                }
                else {
                    html += ' <a class="btn cancel" rel="' + o.data.operStatus + '" id=' + o.data.ingredientsId + '  onclick="dishingredientsselloff(this);"  name="yesselloff" href="dishMix.aspx?id=' + o.data.ingredientsId + '">取消沽清</a>';
                }
                //html += '<div class="txt-tips-sprite"><i class="txt-tips">保存数据,将成功删除！</i></div>'
                //  + '	<a class="btn delete" rel="' + o.data.operStatus + '" name="" href="dishMix.aspx?id=' + o.data.ingredientsId + '">删除</a>'
                //  + '</div>';
                html +='<a class="btn delete" rel="' + o.data.operStatus + '" name="" href="dishMix.aspx?id=' + o.data.ingredientsId + '">删除</a>'
               + '</div>';
                return html;
            };

            var table = new Y.DataTable({
                columns: [{
                    key: 'ingredientsName', label: '名称', formatter: nameHandler, allowHTML: true
                }, {
                    key: 'ingredientsPrice', label: '价格', formatter: priceHandler, allowHTML: true
                },
                {
                    key: 'vipDiscountable', label: 'vip折扣', formatter: vipHandler, allowHTML: true
                },
                //{
                  //  key: 'backDiscountable', label: '支持返送', formatter: backHandler, allowHTML: true
               // }, 
                {
                    key: 'ingredientsSequence', label: '排序', formatter: sequenceHandler, allowHTML: true
                },
                 {
                     key: 'btn', label: '操作', formatter: btnHandler, allowHTML: true
                 }],
                data: data,
                strings: { emptyMessage: '暂无数据显示', loadingMessage: '数据加载中' }
            });
            contentBox.empty(true);
            table.render(contentBox);

        }
    }, {
        NS: 'dishMixPlugin',
        ATTRS: {
            dataTemp232: { value: '' },
            okHandler: {
                valueFn: function () {
                    //
                }
            }
        }
    });

}, '1.0', { requires: ['base-build', 'plugin', 'datatable-base', 'datatable-message', 'io-base', 'json-stringify'] });

//沽清配菜操作
//wangc
//经过尝试，当前项目封装yui好像与jquery不兼容，url路径解析不出来
//故使用原生的js ajax提交数据
var xmlHttpRequest = null;
function dishingredientsselloff(ingredients) {
    var id = ingredients.id;
    var name = ingredients.name;
    if (window.ActiveXObject) // IE浏览器
    {
        xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else if (window.XMLHttpRequest) // 其他浏览器
    {
        xmlHttpRequest = new XMLHttpRequest();
    }
    if (null != xmlHttpRequest) {
        var status = name == "notselloff" ? 1 : 0;//1：表示沽清操作，0：表示取消沽清操作
        // 向服务器发出一个请求
        xmlHttpRequest.open("POST", "ajax/doSybWeb.ashx?m=ingredients_selloff&ingredientId=" + id + "&status=" + status, true);
        // 回调函数
        xmlHttpRequest.onreadystatechange = function () {
            if (xmlHttpRequest.readyState == 4) {
                if (xmlHttpRequest.status == 200) {
                    var content = xmlHttpRequest.responseText;
                    if (content == "ok") {//沽清，取消沽清成功
                        if (name == "notselloff") {
                            $(ingredients).removeClass("btn btnSoon").addClass("btn cancel").attr("name", "yesselloff");
                            $(ingredients).text("取消沽清");
                        }
                        else {//取消沽清操作
                            $(ingredients).removeClass("btn cancel").addClass("btn btnSoon").attr("name", "notselloff");
                            $(ingredients).text("沽清");
                        }
                    }
                    else {
                        alert(content);
                    }
                }
            }
        };
        // post提交时
        xmlHttpRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        // 向服务器发出一个请求
        xmlHttpRequest.send("");
    }
}

