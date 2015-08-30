YUI.add('Ny', function(Y) {
    Y.NyClass = Y.Base.create('NyNS', Y.Widget, [], {
	destructor: function() {
	    this.get('contentBox').remove(true);
	},
	btnSuccess: function(rsp) {
	    var result = Y.JSON.parse(rsp.responseText);
	    var d = Y.JSON.parse(result.d),
		    dataRow = '',
		    type = this.get('pageType');
		    
	    if (d == '-1' || d == '-7' || d == '-8' || d == '-9') {
		var tips = '';
		if (this.get('isRefund') == 1) {
		    tips = '<span class="txt">您已执行<strong>[退款操作]</strong>，无法取消入座</span>';
		    var btn = Y.one('#comment .operate');
		    btn.addClass('cancel');
		    btn.set('text', '取消入座');
		} else if ( d == '-8' ) {
		    tips = '<span class="txt">顾客已执行<strong>[退款操作]</strong>，无法入座</span>';
		    var btn = Y.one('#comment .operate');
		    btn.removeClass('cancel');
		    btn.set('text', '入座');
		} else if ( d == '-9' ) {
        tips = '<span class="txt">当前单子已补差价，无法取消入座，请选择退款</span>';
        var btn = Y.one('#comment .operate');
        btn.addClass('cancel');
        btn.set('text', '取消入座');
    } else {
		    tips = '<span class="txt">当前点单<strong>[已对账]</strong>，无法取消入座</span>';
		    var btn = Y.one('#comment .operate');
		    btn.addClass('cancel');
		    btn.set('text', '取消入座');
		}
		var popupSprite = Y.one('#popupSprite');
		popupSprite.empty(true);
		popupSprite.append('<h3 class="popup">' + tips + '</h3>');
		var popup = Y.one('.popup');
		popup.transition({
		    duration: 0.2,
		    easing: 'ease-in',
		    top: '-56px',
		    height: '56px',
		    on: {
			start: function() {
			},
			end: function() {
			    popup.transition({
				delay: 0.5,
				easing: 'ease-out',
				height: '0px',
				top: '-25px',
				opacity: {
				    duration: 1,
				    value: 0
				}
			    }, function() {
				popup.remove(true);
			    });
			}
		    }
		});
		//this.set('isVerified',1);
	    } else {
		if (this.get('isStatus') == '1' && type == 'preOrderConfirmedDetail') {
		    //已经入座,而后台方法只有在传status 0 和 preOrder19dianId 时才返回 -1
		}
		if (!!d.preOrderVerifiedInfo) {
		    Y.one('#comment .verified').hide();
		    for (var i = 0; i < d.preOrderVerifiedInfo.TableJson.length; i++) {
			var employeeName = d.preOrderVerifiedInfo.TableJson[i].employeeName;
			var employeePosition = d.preOrderVerifiedInfo.TableJson[i].employeePosition;
			var checkTime = d.preOrderVerifiedInfo.TableJson[i].checkTime;
			var status = d.preOrderVerifiedInfo.TableJson[i].status == '1' ? '对账' : '取消对账';
			if (i % 2 == 0) {
			    dataRow += '<tr class="gray">'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + checkTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			} else {
			    dataRow += '<tr>'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + checkTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			}
		    }
		    Y.one('#verifiedInfo tbody').setHTML(dataRow);
		    dataRow = '';
		} else {
		    Y.one('#comment .verified').show();
		}
		;
		if (!!d.preOrderConfirmedInfo) {
		    Y.one('#comment .confirmed').hide();
		    for (var i = 0; i < d.preOrderConfirmedInfo.TableJson.length; i++) {
			var employeeName = d.preOrderConfirmedInfo.TableJson[i].employeeName;
			var employeePosition = d.preOrderConfirmedInfo.TableJson[i].employeePosition;
			var shopConfirmedTime = d.preOrderConfirmedInfo.TableJson[i].shopConfirmedTime;
			var status = d.preOrderConfirmedInfo.TableJson[i].status == '1' ? '入座' : '取消入座';
			if (i % 2 == 0) {
			    dataRow += '<tr class="gray">'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + shopConfirmedTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			} else {
			    dataRow += '<tr>'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + shopConfirmedTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			}
		    }
		    Y.one('#confirmedInfo tbody').setHTML(dataRow);
		    dataRow = '';
		} else {
		    Y.one('#comment .confirmed').show();
		}
		;
	    }
	},
	btnHandler: function(e) {
	    var t = e.currentTarget,
		    type = this.get('pageType');
	    var className = t.getAttribute('class'),
		    isCancel = className.indexOf('cancel'),
		    isOperate = className.indexOf('operate'),
		    isBack = className.indexOf('back') > -1;
	    if (isCancel > -1) {
		if (this.get('isVerified') != 1) {
		    this.set('isStatus', 0);
		} else {
		    var popupSprite = Y.one('#popupSprite');
		    popupSprite.empty(true);
		    popupSprite.append('<h3 class="popup"><span class="txt">当前点单<strong>[已对账]</strong>，无法取消入座</span></h3>');
		    var popup = Y.one('.popup');
		    popup.transition({
			duration: 0.2,
			easing: 'ease-in',
			top: '-56px',
			height: '56px',
			on: {
			    start: function() {
			    },
			    end: function() {
				popup.transition({
				    delay: 0.5,
				    easing: 'ease-out',
				    height: '0px',
				    top: '-25px',
				    opacity: {
					duration: 1,
					value: 0
				    }
				}, function() {
				    popup.remove(true);
				});
			    }
			}
		    });
		}
	    } else if (isBack) {
		e.preventDefault();
		var url = '',
			qs = this.get('queryString');

		switch (type) {
		    case 'preOrderConfirmedDetail':
			url = 'preOrderShopConfirmed.aspx';
			break;
		    case 'preOrderVerifiedDetail':
			url = 'preOrderShopVerified.aspx';
			break;
		    case 'couponPreOrderDetail':
			url = 'couponUsedDetail.aspx';
			break;
		    case 'accountPreOrderDetail':
		    case 'accountPayDetail':
			url = 'accountDetails.aspx';
			break;
		}
		;

		VA.argPage.qs = qs;

		var index = url.indexOf('?'),
			indexDot = url.indexOf('.');
		var urlAspx = (index > -1) ? url.substring(0, index) : url,
			cur = url.substring(0, indexDot);
		menu.prevPage(urlAspx, cur);

	    } else if (isOperate > -1) {
		this.set('isStatus', 1);
	    }
	},
	btnOperate: function() {
	    var isStatus = this.get('isStatus'),
	    type = this.get('pageType');
	    var self = this;
	    var ioHandler = {
		method: 'POST',
		data: '{"statusFlag":' + isStatus + ',"preOrder19dianId":"' + this.get('queryString').a + '"}',
		headers: {'Content-Type': 'application/json; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			self.btnSuccess(rsp);
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		}
	    };

	    if (type == 'preOrderVerifiedDetail') { //
		Y.io('preOrderVerifiedDetail.aspx/ShopVerifiedOperate', ioHandler);
	    }

	    if (type == 'preOrderConfirmedDetail') {
		Y.io('preOrderConfirmedDetail.aspx/ShopConfirmedOperate', ioHandler);
	    }
	},
	dataSuccess: function(rsp) {
	    var result = Y.JSON.parse(rsp.responseText);
	    var d = Y.JSON.parse(result.d);
	    this.set('isStatus', d.isStatus);
	    var dataRow = '';

	    if (d.commonInfo) {
		for (var i = 0; i < d.commonInfo[0].TableJson.length; i++) {
		    var preOrder19dianId = d.commonInfo[0].TableJson[i].preOrder19dianId;
		    var UserName = d.customerPartInfo[0].TableJson[i].UserName;

		    var mobilePhoneNumber = d.customerPartInfo[0].TableJson[i].mobilePhoneNumber;
		    mobilePhoneNumber = !!mobilePhoneNumber ? mobilePhoneNumber : (d.commonInfo[0].TableJson[i].eCardNumber + d.commonInfo[0].TableJson[i].verificationCode);

		    var invoiceTitle = d.commonInfo[0].TableJson[i].invoiceTitle;
		    var afterDiscountAmount = d.commonInfo[0].TableJson[i].afterDiscountAmount;
		    var prePayTime = d.commonInfo[0].TableJson[i].prePayTime;

		    this.set('isVerified', d.commonInfo[0].TableJson[i].isApproved);//isApproved

		    if (i % 2 == 0) {
			dataRow += '<tr class="gray">'
				+ '<td>' + preOrder19dianId + '</td><td>' + UserName + '</td><td>' + mobilePhoneNumber + '</td><td>' + invoiceTitle + '</td><td>' + afterDiscountAmount + '</td><td>' + prePayTime + '</td>'
				+ '</tr>';
		    } else {
			dataRow += '<tr>'
				+ '<td>' + preOrder19dianId + '</td><td>' + UserName + '</td><td>' + mobilePhoneNumber + '</td><td>' + invoiceTitle + '</td><td>' + afterDiscountAmount + '</td><td>' + prePayTime + '</td>'
				+ '</tr>';
		    }
		};
		Y.one('#commonInfo tbody').setHTML(dataRow);
		dataRow = '';

		for (var i = 0; i < d.commonInfo[0].TableJson.length; i++) {
		    var prePaidSum = d.commonInfo[0].TableJson[i].prePaidSum;
		    var preOrderSum = d.payment.preOrderSum;
		    var prePayTime = d.commonInfo[0].TableJson[i].prePayTime;
		    var refundDeadline = d.commonInfo[0].TableJson[i].refundDeadline;
		    var thirdAmount = d.payment.thirdAmount;
		    var redEnvelopeAmount = d.payment.redEnvelopeAmount;
		    var balanceAmount = d.payment.balanceAmount;
		    var paymentPrePaidSum = d.payment.prePaidSum;
		    var isPaid = d.commonInfo[0].TableJson[i].isPaid == '1' ? '已支付' : '未支付';
		    var fillpostAmount = d.payment.fillpostAmount;
		    if (i % 2 == 0) {
			dataRow += '<tr class="gray">'
				+ '    <td>' + paymentPrePaidSum + ' </td><td>'+preOrderSum+'</td><td>' + fillpostAmount + '</td><td>' + thirdAmount + '</td><td>' + redEnvelopeAmount + '</td><td>' + balanceAmount + '</td>'
				+ '</tr>';
		    } else {
			dataRow += '<tr>'
				+ '    <td>' + prePaidSum + ' </td><td>'+preOrderSum+'</td><td>' + fillpostAmount + '</td><td>' + thirdAmount + '</td><td>' + redEnvelopeAmount + '</td><td>' + balanceAmount + '</td>'
				+ '</tr>';
		    }
		}
		Y.one('#payInfo tbody').setHTML(dataRow);
		dataRow = '';
	    }
		
		//优惠信息
		var discountAmount = d.payment.discountAmount;
		var discount = d.payment.discount == "1" ? "" : "("+d.payment.discount*10+"折)";
		var couponAmount = d.payment.couponAmount;
		var couponName = d.payment.couponName ? "("+d.payment.couponName+")" : "";
		var totalAmount = discountAmount + couponAmount;
		
		var discountRow = '<tr>';
		discountRow += '<td>'+totalAmount+'</td><td>'+discountAmount+discount+'</td><td>'+couponAmount+couponName+'</td>';
		discountRow += '</tr>';
		
		Y.one('#discountInfo tbody').setHTML(discountRow);
		
		
	    if (d.orderInfo) {
		for (var i = 0; i < d.orderInfo.length; i++) {
		    var dishName = d.orderInfo[i].dishName;
		    var commentStr = '<p class="labelInfo">';
		    var dishPriceName = '规格：' + d.orderInfo[i].dishPriceName,
			    tasteName = d.orderInfo[i].dishTaste ? (Boolean(d.orderInfo[i].dishTaste.tasteName) ? ('&nbsp;&nbsp;&nbsp;&nbsp;口味：' + d.orderInfo[i].dishTaste.tasteName) : '') : '';
		    var dishIngredients = d.orderInfo[i].dishIngredients ? d.orderInfo[i].dishIngredients : '',
			    ingredientsNameStr = '';
		    if (dishIngredients.length > 0) {
			ingredientsNameStr = '&nbsp;&nbsp;&nbsp;&nbsp;配料：';

			var nameStr = '';
			for (var j = 0, len = dishIngredients.length; j < len; j++) {
			    nameStr += dishIngredients[j].ingredientsName + dishIngredients[j].quantity + "份,";
			}
			if (nameStr) {
			    nameStr = nameStr.slice(0, nameStr.length - 1);
			}
			ingredientsNameStr += nameStr;
		    }
		    commentStr += dishPriceName + tasteName + ingredientsNameStr + '</p>';
		    var unitPrice = d.orderInfo[i].unitPrice;
		    var quantity = d.orderInfo[i].quantity;
		    if (i % 2 == 0) {
			dataRow += '<tr class="gray">'
				+ '<td>' + (i + 1) + '</td><td class="dishName">' + dishName + commentStr + '</td><td>' + unitPrice + '</td><td>' + quantity + '</td>'
				+ '</tr>'
		    } else {
			dataRow += '<tr>'
				+ '<td>' + (i + 1) + '</td><td class="dishName">' + dishName + commentStr + '</p></td><td>' + unitPrice + '</td><td>' + quantity + '</td>'
				+ '</tr>'
		    }
		}
		Y.one('#orderInfo tbody').setHTML(dataRow);
		dataRow = '';
	    }

	    if (d.refundInfo) {
		for (var i = 0; i < d.refundInfo.length; i++) {
		    this.set('isRefund', 1);

		    var staus = '已退款';
		    var refundMoney = d.refundInfo[i].refundMoney;
		    var operTime = d.refundInfo[i].operTime;
		    var remark = d.refundInfo[i].remark;
		    var operUser = d.refundInfo[i].operUser;

		    if (i % 2 == 0) {
			dataRow += '<tr class="gray">'
				+ '    <td>' + staus + ' </td><td>' + refundMoney + '</td><td>' + operTime + '</td><td>' + remark + '</td><td>' + operUser + '</td>'
				+ '</tr>';
		    } else {
			dataRow += '<tr>'
				+ '    <td>' + staus + ' </td><td>' + refundMoney + '</td><td>' + operTime + '</td><td>' + remark + '</td><td>' + operUser + '</td>'
				+ '</tr>';
		    }
		}
		Y.one('#refundInfo tbody').setHTML(dataRow);
		dataRow = '';
	    }

	    if (d.preOrderVerifiedInfo) {
		if (!!d.preOrderVerifiedInfo[0]) {
		    Y.one('#comment .verified').hide();
		    for (var i = 0; i < d.preOrderVerifiedInfo[0].TableJson.length; i++) {
			var employeeName = d.preOrderVerifiedInfo[0].TableJson[i].employeeName;
			var employeePosition = d.preOrderVerifiedInfo[0].TableJson[i].employeePosition;
			var checkTime = d.preOrderVerifiedInfo[0].TableJson[i].checkTime;
			var status = d.preOrderVerifiedInfo[0].TableJson[i].status == '1' ? '对账' : '取消对账';
			if (i % 2 == 0) {
			    dataRow += '<tr class="gray">'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + checkTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			} else {
			    dataRow += '<tr>'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + checkTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			}
		    }
		} else {
		    Y.one('#comment .verified').show();
		}
		;
		Y.one('#verifiedInfo tbody').setHTML(dataRow);
		dataRow = '';
	    }

	    if (d.preOrderConfirmedInfo) {
		if (!!d.preOrderConfirmedInfo[0]) {
		    Y.one('#comment .confirmed').hide();
		    for (var i = 0; i < d.preOrderConfirmedInfo[0].TableJson.length; i++) {
			var employeeName = d.preOrderConfirmedInfo[0].TableJson[i].employeeName;
			var employeePosition = d.preOrderConfirmedInfo[0].TableJson[i].employeePosition;
			var shopConfirmedTime = d.preOrderConfirmedInfo[0].TableJson[i].shopConfirmedTime;
			var status = d.preOrderConfirmedInfo[0].TableJson[i].status == '1' ? '入座' : '取消入座';
			if (i % 2 == 0) {
			    dataRow += '<tr class="gray">'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + shopConfirmedTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			} else {
			    dataRow += '<tr>'
				    + '	<td>' + (i + 1) + '</td><td>' + employeeName + '</td><td>' + employeePosition + '</td><td>' + shopConfirmedTime + '</td><td>' + status + '</td>'
				    + '</tr>'
			}
		    }
		} else {
		    Y.one('#comment .confirmed').show();
		}
		;
		Y.one('#confirmedInfo tbody').setHTML(dataRow);
		dataRow = '';
	    }

	    if (d.accountInfo) {
		for (var i = 0; i < d.accountInfo[0].TableJson.length; i++) {
		    var item = d.accountInfo[0].TableJson[i];
		    dataRow += '<tr>'
			    + '   <td>' + item.operEmployeeName + '</td><td>' + item.operTime + '</td><td>' + item.accountMoney + '</td><td>' + item.remark + '</td>'
			    + '</tr>';
		}
		Y.one('#accountInfo tbody').setHTML(dataRow);
		dataRow = '';
	    }
	},
	slidePanelAnim: function(e) {//toggle
	    e.preventDefault();
	    this.slideModule.toggleClass('open');
	    this.slidePanel.fx.set('reverse', !this.slidePanel.fx.get('reverse'));
	    this.slidePanel.fx.run();
	},
	renderUI: function() {
	    var self = this;
	    var m = 'preOrder19dianId';
	    if (this.get('pageType') == 'accountPayDetail') {
		m = 'accountId';
	    }
	    var ioHandler = {
		method: 'POST',
		data: '{"' + m + '":"' + this.get('queryString').a + '"}',
		headers: {'Content-Type': 'application/json; charset=utf-8'},
		on: {
		    success: function(id, rsp) {
			self.dataSuccess(rsp);
		    },
		    failure: function(id, rsp) {
			Y.log(rsp.status);
		    }
		},
		sync: true
	    };
	    Y.io(this.get('ioURL'), ioHandler);

	    var slidePanel = Y.one('#slidePanel');
	    if (slidePanel) {
		this.slideModule = Y.one('#showList');

		this.slidePanel = this.slideModule.one('#slidePanel').plug(Y.Plugin.NodeFX, {
		    from: {
			height: function(node) {
			    return node.get('scrollHeight');
			}
		    },
		    to: {
			height: 0
		    },
		    easing: 'easeIn',
		    duration: 1.5
		});

		this.slideModule.one('#slidePanel').setStyle('height', '0');
	    }
	},
	bindUI: function() {
	    btnContainer = Y.one('#comment');
	    btnContainer.delegate('click', this.btnHandler, '.btn', this);

	    this.after('isStatusChange', this.syncUI, this);
	    this.after('isStatusChange', this.btnOperate, this);

	    var slidePanel = Y.one('#slidePanel');
	    if (slidePanel) {
		Y.one('#showList .bar').on('click', this.slidePanelAnim, this);//
	    }
	},
	syncUI: function() {
	    //初始化"操作按扭状态"
	    var btn = Y.one('#comment .operate'),
		    isStatus = this.get('isStatus'), //0 | 1
		    type = this.get('pageType');
	    var self = this;
	    var isVerified = this.get('isVerified');

	    if (type == 'preOrderConfirmedDetail') {
    		if (isStatus) {
    		    btn.addClass('cancel');
    		    btn.set('text', '取消入座');
    		} else if (isVerified == '1') {
    		    btn.addClass('cancel');
    		    btn.set('text', '入座');
    		} else {
    		    btn.removeClass('cancel');
    		    btn.set('text', '入座');
    		}
	    }
	}
    }, {
	ATTRS: {
	    pageType: {
		value: ''
	    },
	    ioURL: {
		value: ''
	    },
	    isStatus: {
		value: 0
	    },
	    isVerified: {
		value: 0
	    },
	    isRefund: {
		value: 0
	    },
	    queryString: {
		value: ''
	    }
	}
    });
}, '1.1', {requires: ['base-build', 'node-event-delegate', 'widget', 'node', 'transition', 'anim', 'io-base', 'json-parse']});