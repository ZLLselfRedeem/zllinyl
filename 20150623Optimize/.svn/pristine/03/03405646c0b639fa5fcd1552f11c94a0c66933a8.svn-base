$(function() {
    var pageSize = VA.mobile.widgets.getPageSize();
    var CookBook = function() {
	this.hasDishListener;							// 添加菜品侦听标识
	this.shopData;
	this.pricesPay = 0;
	this.priceTotal = 0;
	this.priceBack = 0;
	this.sundryPriceTotal = 0;
	this.sundryPriceRatio = 0;
	this.pageTitle;
	this.pageMenuName;
    };
    CookBook.prototype.init = function(cook, shopData) {
	this.shopData = shopData;
	var data = cook.menus[0];
	// 设置店头文本
	$(".ui-page").context.title = cookbook.pageTitle = cookbook.pageMenuName = data.menuName[0].Value;
	
	// 沽清列表应用控制
	for(var i=0,len=data.typeList.length;i<len;i++){
	    var dishList = data.typeList[i].dishList;
	    var dishTypeListSale = [], dishTypeListSellOff = [];
	    for(var j=0,dishLen=dishList.length;j<dishLen;j++){
		if(cookbook.shopData.sellOffList.indexOf(dishList[j].dishPrices[0].dishPriceId)===-1){
		    dishTypeListSale.push(dishList[j]);
		}else{
		    dishTypeListSellOff.push(dishList[j]);
		};
	    }
	    data.typeList[i].dishList = dishTypeListSale.concat(dishTypeListSellOff);
	}
	// 设置导航分类栏
	createType(data.typeList);
	
	// 设置panel
	var panelHeight = pageSize.pageH - 140 - 20;// -45 再减去"确认点单+原价+节省"高度 -95;
	$("#cookPanel").css({"top": 45});// headerHeight 45
	$("#cookPanel ul").css({"height": panelHeight});

	bindUI();
    };
    CookBook.prototype.numTrigger = function(parentSelector, min, visible) {// ([Selector string[,min int[,visible boolean]]])
	var parentSelector = parentSelector ? parentSelector : "";
	var numMin = min ? min : 0;// 默认为0
	var slide = $(parentSelector + " .num-trigger>.slide");
	if (visible) {
	    slide.css({"opacity": 1, "left": 0});
	} else {
	    slide.css({"opacity": 0, "left": 75});
	}
	$(parentSelector + " .num-trigger>.num-add").on("tap", function(event) {
	    var s = $(event.target).siblings(".slide");
	    var num = s.find(".num"),
		    quantity = parseInt(num.text());
	    if (!visible) {
		s.stop().animate({
		    left: 0,
		    opacity: 1
		}, 300);
	    }
	    ;
	    if (quantity == 0) {
		s.stop().animate({
		    left: 0,
		    opacity: 1
		}, 300);
	    }
	    num.text(++quantity);

	});
	$(parentSelector + " .num-trigger>.slide>.num-less").on("tap", function(event) {
	    var t = $(event.target);
	    var s = t.parent(".slide");
	    var num = t.siblings(".num"),
		    quantity = parseInt(num.text());
	    if (quantity == numMin) {
		return;
	    }
	    if (quantity == 1) {
		s.stop().animate({
		    left: 75,
		    opacity: 0
		}, 300);
	    }
	    num.text(--quantity);

	});
    };
    CookBook.prototype.priceHandler = function(price, dishPrice) {	// 支付价\是否折扣
	cookbook.pricesPay += price;
	cookbook.pricesPay = getRoundPrice(cookbook.pricesPay);
	$("#prices").text(cookbook.pricesPay);

	// cookbook.priceTotal += discount? (price/cookbook.shopData.priceRate):price;
	cookbook.priceTotal += dishPrice;
	$("#priceTotal").text(getRoundPrice(cookbook.priceTotal));

	cookbook.priceBack = getRoundPrice(cookbook.priceTotal - cookbook.pricesPay);
	$("#priceBack").text(cookbook.priceBack);

    };

    function createType(type) {
	var html = "",
		len = type.length;
	
	var typeIdCookie = 0;
	// var typeIdCookie = $.cookie("typeIds");
	// if (typeIdCookie == "undefind" || typeIdCookie == undefined) {
	//	typeIdCookie = 0;// 初始配置菜项,默认第一分类菜分类导航; 
	// }
	for (var i = 0; i < len; i++) {
	    var dishTypeName = type[i].dishTypeName[0].Value;
	    if (i !== typeIdCookie) {
		html = '<li><a href="#cook_100" class="show-page-loading-msg" data-textonly="false" data-textvisible="true" data-msgtext="" data-inline="true"><span class="txt">' + dishTypeName + '</span></a></li>';
	    } else {
		html = '<li class="active"><a href="#cook_100" class="show-page-loading-msg" data-textonly="false" data-textvisible="true" data-msgtext="" data-inline="true"><span class="txt">' + dishTypeName + '</span></a></li>';
	    }

	    $("#typeScroll>ul").append(html);
	    $("#typeScroll>ul>li>a:last").data("key", type[i]);
	}
	
       
	var typeWidth = Math.round(pageSize.pageW / 5.3), // len条右边线
		headerWidth = len * typeWidth;

	$("#typeScroll").css("width", headerWidth);
	$("#typeScroll li").css({"width":typeWidth});
	// 切换菜分类导航
	$("#typeScroll li").on("tap", "a", typeHandler);
	function typeHandler(event) {
	    var e = $(this),
		    d = e.data("key");
	    e.parent().siblings().removeClass("active");
	    e.parent().addClass("active");
	    $.cookie("typeIds", $("#typeScroll li a").index($(this)));
	    e.off("tap", "a", typeHandler);

	    // 预加载
	    var $this = $(this),
		    theme = $this.jqmData("theme") || $.mobile.loader.prototype.options.theme,
		    msgText = $this.jqmData("msgtext") || $.mobile.loader.prototype.options.text,
		    textVisible = $this.jqmData("textvisible") || $.mobile.loader.prototype.options.textVisible,
		    textonly = !!$this.jqmData("textonly");
	    html = $this.jqmData("html") || "";
	    $.mobile.loading("show", {
		text: msgText,
		textVisible: textVisible,
		theme: theme,
		textonly: textonly,
		html: html
	    });

	    createDishList(d, function() {
		$("#cook_100").css("opacity", 0.5).animate({
		    opacity: 1
		}, 200, function() {

		});
		e.on("tap", "a", typeHandler);
		$.mobile.loading("hide");
	    });
	}

	var d = $("#typeScroll li a:eq(" + typeIdCookie + ")").data("key");
	createDishList(d, function() {
	    $("#cook_100").css("opacity", 0.5).animate({
		opacity: 1
	    }, 200, function() {
		// 菜名自滚动
		// scrollLeft(1); 			scroll() 
		// jQuery.Event("select");	.trigger()
	    });

	    new IScroll('#headerType', {scrollX: true, scrollY: false, mouseWheel: true});
	    $("body").on("pagecontainershow", function(event, ui) {// 处理fade 干扰scroll
		new IScroll('#headerType', {scrollX: true, scrollY: false, mouseWheel: true});
	    });

	    // 预加载完毕 退回至微信公众帐号
	    var pageCook = $("#pageCook");
	    $("body").pagecontainer("change", pageCook, {
		show: function() {
		    //
		}(),
		changeHash: true,
		loadMsgDelay: 0,
		// dataUrl:"/AppPages/wechatOrder/menu.aspx?sc=8e64aa38-2cd5-4d4a-a0a6-b27e3bc13337635360882749159623&cid=87",
		dataUrl:window.location.href,
		reverse: true
	    });
	});
    }
    ;
    function createDishList(dataContainer, callbackFn) {
	$("#cookbook>ul").empty(true);
	var that = this;
	var html = "";
	var data = dataContainer.dishList,
		dishTypeName = dataContainer.dishTypeName[0].Value;
	
	// 沽清菜品的置后处理；

	var pathsArr = [];
	for (var i = 0, len = data.length; i < len; i++) {
	    var dishImage = data[i].dishImages[0];
	    if (!dishImage) {
		dishImage = "../img/pic_none.png";
	    }
	    pathsArr.push(dishImage);
	}

	$.imgpreloader({paths: pathsArr}).always(function($allImages, $properImages, $brokenImages){
	    var scrollLength = 0;
	    
	    for (var i = 0, len = data.length; i < len; i++) {
		
		var d = {
		    dishId: data[i].dishId,
		    dishPrices: data[i].dishPrices,
		    // dishIngredients:[],
		    // dishPriceI18nId
		    // dishPriceName  | scaleName
		    // dishTaste
		    // vipDiscountable
		    dishName: data[i].dishName[0].Value,
		    dishTypeName: dishTypeName,
		    markName: "", // 掌中宝编号，仅为空
		    quantity: 0,
		    unitPrice: 0
		};

		var iconClassName = "";							// 标识 折扣价\已售完
		if (d.dishPrices[0].vipDiscountable === "true"&&cookbook.shopData.priceRate!==1) {
		    d.vipDiscountable = true;
		}
		;
		var dishPrice = d.dishPrices[0].dishPrice;		// 单菜品与多规格，都显示第一索引位置规格价格
		d.unitPrice = getUintPrice(dishPrice, d.vipDiscountable);

		if(d.vipDiscountable) {
		    iconClassName = " uintPrice";
		};
		if(d.dishPrices.length === 1){
		    d.dishPriceI18nId = d.dishPrices[0].dishPriceId;
		    if (cookbook.shopData.sellOffList.indexOf(d.dishPriceI18nId) > -1) {
			iconClassName = " sellOff";
		    }
		} else {
		    var isSellOff = true;
		    for (var j = 0, lenPrices = d.dishPrices.length; j < lenPrices; j++) {
			if (cookbook.shopData.sellOffList.indexOf(d.dishPrices[j].dishPriceId) === -1) {
			    isSellOff = false;// 多规格任规格未售完，则该菜品就为可售状态；
			}
		    }
		    if (isSellOff) {
			iconClassName = " sellOff";
		    }
		}
		
		var dishImage = data[i].dishImages[0];
		var hasBroken = false;
		if($brokenImages){ 
		    for(var j=0,lenBroken = $brokenImages.length;j<lenBroken;j++){
			if(dishImage===$brokenImages[j].src){
			    hasBroken = true;
			}
		    } 
		}
		if (!dishImage || hasBroken) {
		    dishImage = "../img/pic_none.png";
		}

		html = '<li>'
			+ '	<i class="icon' + iconClassName + '"></i>'
			+'	<img src="'+dishImage+'" />'
			+ '	<div class="txt"><h3 class="title"><div><strong>' + d.dishName + '</strong></div></h3><em class="price">' + d.unitPrice + '</em><a class="add" href="javascript:;" data-ajax="true">添加</a></div>'
			+ '</li>';
		$("#cookbook>ul").append(html);
		var $title = $("#cookbook>ul>li:last .txt .title"),
		    $txt = $title.find("strong"),
		    $div = $title.find("div");
		var len1 = $title.width() - 12,
		    len2 = $txt.width();
		if (len2 > len1) {// 预滚动处理
		    $title.css({"height": "100%", "padding-left": "0"});
		    $div.addClass("scroller").addClass("scroller"+i);
		    // $txt
		    // 则 <strong> 为 len1+内\外边距,scroll元素对象 100% {left:len1+内边距;}
		    var scrollWidth = len1 + 20;// 临界值 24
		    scrollLength = (len1 + 28)>scrollLength ? (len1 + 28):scrollLength;
		    $txt.css("width", scrollWidth);

		    $div.append($txt.clone());
		    // $div.append($txt.clone());
		}

		$("#cookbook>ul>li .add:last").data("key", d);
		if (!that.hasDishListener) {// 针对this,所以不可放done匿名函数；
		    that.hasDishListener = $("#cookbook").on("tap", ".add", addCook);
		}
	    }
	    
	    // 文本滚动处理 scroll
	    if(scrollLength>0){
		var cssCode = '@-moz-keyframes scroll {'
		    + '	0% {left:0;}'
		    + '	100% {left:-' + scrollLength + 'px;}'
		    + '}'
		    + '@-webkit-keyframes scroll {'
		    + '	0% {left:0;}'
		    + '	100% {left:-' + scrollLength + 'px;}'
		    + '}'
		    + '@-o-keyframes scroll {'
		    + '	0% {left:0;}'
		    + '	100% {left:-' + scrollLength + 'px;}'
		    + '}'
		    + '@keyframes scroll {'
		    + '	0% {left:0;}'
		    + '	100% {left:-' + scrollLength + 'px;}'
		    + '}';
		// var doc = document.getElementById("cook_100");
		VA.mobile.widgets.insertStyles(cssCode);
	    }
	    
	    /*
	    var $allIcon = $("#cookbook>ul>li .icon");
	    for (var i = 0, len = $allIcon.length; i < len; i++) {
		$($allImages[i]).insertAfter($allIcon[i]);
		
	    }
	    ;
	    */
	   
	    var h = parseInt(pageSize.pageW * 11 / 100);// (pageSize.pageW*0.67*(3/4)*0.24)
	    $("#cookbook .txt").css({"height": h});
	    var $icon = $("#cookbook .icon");
	    var iconWidth = parseInt(pageSize.pageW / 7);
	    $icon.css("width", iconWidth);
	    $icon.css("height", iconWidth);

	    callbackFn();
	});
    }
    
    function bindUI() {
	$("#submitPrice").on("tap", function(event) {
	    event.preventDefault();
	    cookbook.pageTitle = cookbook.pageMenuName;

	    var data = $(event.target).data("key");
	    var ingredientsPriceAllUnit = 0,
		    ingredientsPriceAllDishPrice = 0;
	    $("#ingredients .num").each(function(index, element) {
		var $elm = $(element),
			$price = $elm.parents(".text").find(".price");
		var num = parseInt($elm.text());
		if (num > 0) {
		    var dataIngredientsItem = $(this).data("key");
		    dataIngredientsItem.ingredientsPrice = parseFloat($price.text().slice(1)) * num;
		    dataIngredientsItem.quantity = num;
		    data.dishIngredients.push(dataIngredientsItem);//
		    ingredientsPriceAllUnit += dataIngredientsItem.ingredientsPrice;

		    ingredientsPriceAllDishPrice += parseFloat($price.attr('name')) * num;
		    ;
		}
		;
	    });
	    // （相同规格、口味、配料再来一份吗）
	    var repeatNum = parseInt($("#repeatNum").text());
	    data.quantity = repeatNum ? repeatNum : 1;
	    data.unitPrice = getRoundPrice(data.unitPrice + ingredientsPriceAllUnit); // 暂时不乘份数，只作单份数价格处理；
	    data.dishPrices[0].dishPrice = getRoundPrice(data.dishPrices[0].dishPrice + ingredientsPriceAllDishPrice);
	    var pageCook = $("#pageCook");
	    $("body").pagecontainer("change", pageCook, {
		show: function() {
		    panelListHandler(data);// data 传入的为多规格中的单一规格数据
		}(),
		transition: "slide",
		changeHash: false, //
		reverse: false, // data-direction
		showLoadMsg: true,
		reload: true,
		// dataUrl:'/AppPages/wechatOrder/menu.aspx?sc=8e64aa38-2cd5-4d4a-a0a6-b27e3bc13337635360882749159623&cid=87',
		role: "page"
	    });

	});
	// 处理页面标题显示
	$("body").on("pagecontainerbeforeshow", function(event, ui) {
	    $(".ui-page").context.title = cookbook.pageTitle;
	    // document.title = cookbook.pageTitle;
	    $("#repeatNum").text(1);
	});
	//
	cookbook.numTrigger(".repeat", 1, true);

	// 确认点单
	$("#btnSubmit").on("tap", saveDish);
	//$("#isSundryBtn").on("tap",saveAddSundryDish)
    }
    ;

    function addCook(event) {
	event.preventDefault();//
	//event.stopPropagation();
	var dataBind = $(this).data("key");
	var data = {};
	$.extend(true, data, dataBind);
	if (data.dishPrices.length === 1) {
	    priceSingleHandler(data);
	} else {
	    priceMutilHandler(data);
	}
    }
    ;


    function lessCook(event) {
	var data = $(this).data("key");
	var item = $(this),
		quantity = data.quantity;
	cookbook.priceHandler(-data.unitPrice, -data.dishPrices[0].dishPrice);

	if (quantity > 1) {
	    quantity--;
	    item.find(".count i").text(quantity);
	    var cookPanelList_block_width = item.innerWidth();
	    item.css("left", cookPanelList_block_width);
	    item.stop().animate({
		left: 0
	    }, 300);

	    data.quantity = quantity;
	    item.data("key", data);
	} else {
	    // quantity = 1;
	    item.stop().animate({
		left: item.innerWidth()
	    },
	    300,
		    function() {
			item.remove();
		    });
	}
	;
    }
    ;

    function priceSingleHandler(data) {
	data.dishIngredients = data.dishPrices[0].dishIngredientsList;
	data.dishPriceI18nId = data.dishPrices[0].dishPriceId;
	data.dishPriceName = data.dishPrices[0].scaleName[0].Value;
	// data.dishTaste       = data.dishPrices[0].dishTasteList;
	data.dishTaste = {};
	data.dishTaste.tasteId = 0;
	data.dishTaste.tasteName = "";
	data.vipDiscountable = data.dishPrices[0].vipDiscountable;
	data.quantity = 1;

	if (cookbook.shopData.sellOffList.indexOf(data.dishPriceI18nId) > -1) {// 该菜品今日已售完
	    var $isSellOff = $("#isSellOff");
	    $isSellOff.popup("open");
	    //if(this.isSellOffEventHandler){ // 已售完被固定为一个；
	    //	return;
	    //}
	    $isSellOff.on("tap", "a", sellOffHanler);

	} else {
	    panelListHandler(data);
	}
	;
	function sellOffHanler(event) {
	    $isSellOff.off("tap", "a", sellOffHanler);
	    $isSellOff.popup("close");
	    var className = event.target.className,
		    isContinue = className.indexOf("continue") > -1;
	    if (isContinue) {
		panelListHandler(data);
	    }
	    ;
	}
	;
    }
    ;

    function priceMutilHandler(data) {
	var mutilPrices = $("#mutilPrices");
	cookbook.pageTitle = data.dishName;
	// pagecontainerbeforeshow
	$("body").pagecontainer("change", mutilPrices, {
	    //$("body").pagecontainer("change", "mutildish.html", {
	    create: function() {
		mutilPricesShow();
	    }(),
	    beforeshow: function(event, ui) {
		//
	    }(),
	    show: function() {

	    }(),
	    transition: "slide",
	    changeHash: false, // 与 售完弹层
	    reverse: true, // data-direction
	    showLoadMsg: true,
	    reload: true,
	    role: "page"
	});

	function mutilPricesShow() {
	    var html = "";
	    // 配置规格
	    var prices = data.dishPrices,
		    lenPrices = data.dishPrices.length;
	    var sellOffArray = [],
		    leaveArray = [],
		    pricesArray = [];
	    for (var i = 0; i < lenPrices; i++) {
		var priceShow = prices[i].dishPrice;
		var priceVipDiscountable = true;
		var hasStarIcon = "*";
		if (prices[i].vipDiscountable === "false") {
		    priceVipDiscountable = false;
		}
		;
		//
		if (priceVipDiscountable) {
		    priceShow = getRoundPrice(cookbook.shopData.priceRate * priceShow);
		    hasStarIcon = "";
		} else {
		    priceShow = getRoundPrice(priceShow);
		}
		var isSellOffStr = "",
			isDisabledStr = "";
		if (cookbook.shopData.sellOffList.indexOf(prices[i].dishPriceId) > -1) {
		    isSellOffStr = " <i> ( 已售完 )</i>";
		    isDisabledStr = ' disabled="true"';
		    sellOffArray.push(i);
		}
		pricesArray.push(i);
		html += '<li><label data-iconpos="right" for="radio-prices-' + i + '"><span class="name">' + hasStarIcon + prices[i].scaleName[0].Value + isSellOffStr + '</span><span class="price"> ￥' + priceShow + '</span></label><input type="radio" name="radio-prices" id="radio-prices-' + i + '" value="' + prices[i].dishPriceId + '" ' + isDisabledStr + ' /></li>';
	    }
	    ;
	    $("#dishPrice ul").html(html);
	    //
	    leaveArray = pricesArray.filter(function(item, index, array) {
		return (sellOffArray.indexOf(item) > -1) ? false : true;
	    });
	    var initIndex = leaveArray.sort()[0];
	    $("#dishPrice ul input:eq(" + initIndex + ")").attr('checked', true);
	    pricesItemHandler(prices[initIndex]);

	    //
	    $("#dishPrice ul input[type='radio']").on("click", function(event) {
		// $(this).attr('checked',true).checkboxradio('refresh');
		// $(this).prop('checked');				// 获取原生属性
		// $(this).attr('data-cacheval');		// 
		var index = parseInt($(this).attr('id').slice(13));// radio-prices-
		pricesItemHandler(prices[index]);
	    });
	}
	;

	function pricesItemHandler(pricesItem) {
	    $("#taste").empty();
	    $("#ingredients").empty();
	    hasTasteList(pricesItem);
	    hasIngredientsList(pricesItem);
	    $.mobile.pageContainer.trigger("create");

	    data.dishTaste = pricesItem.dishTasteList[0];
	    data.dishIngredients = [];
	    data.dishPriceI18nId = pricesItem.dishPriceId;
	    data.dishPriceName = pricesItem.scaleName[0].Value;
	    data.vipDiscountable = pricesItem.vipDiscountable == "true" ? true : false;
	    // data.unitPrice = 
	    data.unitPrice = getUintPrice(pricesItem.dishPrice, data.vipDiscountable);
	    data.dishPrices = [];
	    data.dishPrices.push(pricesItem);
	    $("#submitPrice").data("key", data);

	    $("#taste ul input[type='radio']").on("click", function(event) {
		var index = parseInt($(this).attr('id').slice(12));	// radio-taste-
		data.dishTaste = pricesItem.dishTasteList[index];
		$("#submitPrice").data("key", data);
	    });
	}
	;

	function hasTasteList(pricesItem) {
	    var dishTasteList = pricesItem.dishTasteList,
		    dishTasteLen = dishTasteList.length;
	    if (dishTasteLen >= 1) {
		var dishTasteStr = '<h1 class="headerTitle">口味</h1><ul>';
		var tasteChecked = "";
		for (var j = 0; j < dishTasteLen; j++) {
		    tasteChecked = (j == 0) ? ' checked="checked"' : "";
		    dishTasteStr += '<li><label data-iconpos="right" data-enhanced="true" for="radio-taste-' + j + '"><span class="name">' + dishTasteList[j].tasteName + '</span><span class="price"> </span></label><input type="radio" name="radio-taste" id="radio-taste-' + j + '" ' + tasteChecked + ' value="' + dishTasteList[j].tasteId + '" /></li>';
		}
		dishTasteStr += '</ul>';
	    }
	    $("#taste").append(dishTasteStr);
	}
	;
	function hasIngredientsList(pricesItem) {
	    var dishIngredientsList = pricesItem.dishIngredientsList,
		    dishIngredientsLen = dishIngredientsList.length;
	    if (dishIngredientsLen >= 1) {
		var dishIngredientsStr = '<h1 class="headerTitle">配料 <span class="tip">(带*配料项不支持折扣)</span></h1><ul></ul>';
		$("#ingredients").append(dishIngredientsStr);
		for (var k = 0; k < dishIngredientsLen; k++) {
		    var ingredientsPrice = dishIngredientsList[k].ingredientsPrice;
		    var priceVipDiscountable = true;
		    var hasStarIcon = "";
		    if (dishIngredientsList[k].vipDiscountable == "false") {
			priceVipDiscountable = false;
			hasStarIcon = "*";
		    }
		    ;
		    //
		    var priceShow = getUintPrice(ingredientsPrice, priceVipDiscountable);

		    dishIngredientsStr = '<li><div class="text">'
			    + '		<span class="name">' + hasStarIcon + dishIngredientsList[k].ingredientsName + '</span>'
			    + '		<span class="price" name="' + ingredientsPrice + '">￥' + priceShow + '</span>'
			    + '		<span class="control">'
			    + '			<div class="num-trigger">'
			    + '				<div class="slide">'
			    + '					<span class="num-less">-</span>'
			    + '					<span class="num">0</num>'
			    + '				</div>'
			    + '				<span class="num-add">+</span>'
			    + '			</div>'
			    + '		</span>'
			    + '</div></li>';
		    $("#ingredients>ul").append(dishIngredientsStr);
		    $("#ingredients .num:last").data("key", dishIngredientsList[k]);
		}
		;

	    }
	    ;
	    cookbook.numTrigger(".control");
	}
    }
    ;

    function panelListHandler(data) {
	var vipDiscountableStr = data.vipDiscountable ? "" : "*";
	var id,
		quantity = data.quantity;
	if( quantity<=1 ){
	    id = data.dishPriceI18nId;
	} else {
	    var tempId = $("#cookPanelList li").length;
	    id = data.dishPriceI18nId + "_" + tempId;
	}
	cookbook.priceHandler(data.unitPrice*quantity, data.dishPrices[0].dishPrice*quantity);

	var item = $("#item_" + id);// 0

	if (item.length == 0) {
	    // quantity = data.quantity;
	    var html = '<li id="item_' + id + '">'
		    + '  <a class="less" href="javascript:;">减去</a><span class="txt">' + vipDiscountableStr + data.dishName + '</span><span class="count"><i>' + quantity + '</i>份</span>'
		    + '</li>';
	    $("#cookPanelList").append(html);
	    var item = $("#item_" + id);
	    item.on("tap", lessCook);


	} else {
	    quantity = parseInt(item.data("key").quantity) + data.quantity;
	}

	item.find(".count i").text(quantity);						// 单菜份数	

	var cookPanelList_block_width = item.innerWidth();
	item.css("left", cookPanelList_block_width); // .position().left; 
	item.stop().animate({
	    left: 0
	}, 300);

	data.quantity = quantity;
	item.data("key", data);
    }
    ;

    function getUintPrice(price, isDiscount) {
	var unitPrice;
	if (isDiscount) {
	    unitPrice = getRoundPrice(cookbook.shopData.priceRate * price);
	} else {
	    unitPrice = getRoundPrice(price);
	}
	return unitPrice;
    }
    ;
    function getRoundPrice(price) {
	return Math.round(price * 100) / 100;
    }
    ;

    function saveDish(event) {
	event.preventDefault();
	var hasSundry = cookbook.shopData.sundryInfo.length >= 1;
	if (hasSundry) {
	    $("#isSundry").popup("option", "transition", "pop");
	    $("#isSundry").popup("open");
	    saveAddSundryDish();

	} else {// 无杂项
	    submitDishData("[]");
	}
    }
    ;
    function saveAddSundryDish() {	// 杂项
	$("#sundryInfo").empty();
	var items = cookbook.shopData.sundryInfo;
	var mode = 0,
		person = [], hasPerson = false, personOptional = [], hasPersonOptional = false,
		ratio = [], hasRatio = false,
		amount = [], hasAmount = false;
	for (var i = 0, len = items.length; i < len; i++) {
	    var item = items[i];
	    mode = item.sundryChargeMode;
	    //r = ;// true false
	    if (mode == 3) {				// 人数
		if (item.required) {
		    person.push(item);
		    hasPerson = true;
		} else {
		    personOptional.push(item);
		    hasPersonOptional = true;
		}
	    } else if (mode == 2) {			// 比例
		ratio.push(item);
		hasRatio = true;
		//}else if(mode==1){
	    } else {
		amount.push(item);
		hasAmount = true;
	    }
	    ;
	}
	;
	if (hasPerson) {//
	    var personStr = '<li class="person">'
		    + '	<div class="num-control"><span class="title">请输入用餐人数<em class="tips">( <em id="mode-3-per">2.5</em>元/人 )</em></span><div class="repeat">'
		    + '		<div class="num-trigger">'
		    + '			<div class="slide">'
		    + '				<span class="num-less">-</span>'
		    + '				<span class="num">1</span>'
		    + '			</div>'
		    + '			<span class="num-add">+</span>'
		    + '		</div>'
		    + '	</div></div>'
		    + '	<p class="txt"><span class="title" id="mode-3-required"></span><span class="num-price">￥<em class="mode-total">40.08</em></span></p>'
		    + '</li>';
	    $("#sundryInfo").append(personStr);

	    var sundryName = [],
		    per = 0,
		    quantity = 1,
		    total = 0;
	    for (var j = 0, len = person.length; j < len; j++) {
		sundryName.push(person[j].sundryName);
		per += person[j].price;
	    }
	    ;
	    var sundryNameStr = sundryName.join(", ");
	    $("#mode-3-required").text(sundryNameStr);
	    $("#mode-3-per").text(getRoundPrice(per));

	    quantity = parseInt($(".person .num").text());
	    total = per * quantity;
	    $(".person .mode-total").text(getRoundPrice(total));

	    syncSundryTotalPriceInit(total);
	    // per 
	    $("#sundryInfo .person").data("key", {"price": per});
	    getSundryItemPrice(".person", 1);
	    cookbook.numTrigger(".person .repeat", 1, true);
	}
	;
	if (hasPersonOptional) {// 按人次收费项，可选
	    var singlePersonOptionalStr = "";
	    for (var j = 0, len = personOptional.length; j < len; j++) {
		singlePersonOptionalStr = '<li class="person-optional">'
			+ '	<p class="txt"><span class="title">' + personOptional[j].sundryName + '</span><span class="num-price">￥<em class="mode-total">0</em></span></p>'
			+ '	<div class="num-control"><span class="title">&nbsp;</span><div class="repeat">'
			+ '		<div class="num-trigger">'
			+ '			<div class="slide">'
			+ '				<span class="num-less">-</span>'
			+ '				<span class="num">0</span>'
			+ '			</div>'
			+ '			<span class="num-add">+</span>'
			+ '		</div>'
			+ '	</div></div>'
			+ '</li>';

		$("#sundryInfo").append(singlePersonOptionalStr);
		$("#sundryInfo li:last").data("key", personOptional[j]);
	    }
	    ;

	    $("#sundryInfo .mode-3-optional-price").text(0);
	    getSundryItemPrice(".person-optional", 0);
	    cookbook.numTrigger(".person-optional .repeat");
	}
	if (hasAmount) {
	    var total = 0;
	    for (var j = 0, len = amount.length; j < len; j++) {
		var amountStr = '<li>'
			+ '	<p class="txt"><span class="title" class="mode-1">' + amount[j].sundryName + '</span><span class="num-price">￥<em class="mode-1-price">' + amount[j].price + '</em></span></p>'
			+ '</li>';
		$("#sundryInfo").append(amountStr);
		total += amount[j].price;
	    }
	    ;
	    syncSundryTotalPriceInit(total);
	}
	if (hasRatio) {
	    for (var j = 0, len = ratio.length; j < len; j++) {
		var priceRatio = getRoundPrice(ratio[j].price * 100);
		var ratioStr = '<li class="ratio">'
			+ '	<p class="txt"><span class="title"><span class="mode-2">' + ratio[j].sundryName + '</span> <em class="tips">注：按点单总价格(￥<em class="no-ratio-price"></em>)，收取' + priceRatio + '%</em></span><span class="num-price">￥<em class="mode-2-price"></em></span></p>'
			+ '</li>';
		$("#sundryInfo").append(ratioStr);
		$("#sundryInfo li:last").data("key", ratio[j]);
	    }
	    ;

	    syncSundryRatioPriceInit();//perRatio
	}

	function getSundryItemPrice(parent, min) {
	    var isContinue = (min + 1);//2
	    $(parent + " .num-trigger").on("tap", function(event) {
		var t = $(event.target), parentElement = t.parents("li"), quantity = parseInt(parentElement.find(".num").text());
		var d = parentElement.data("key");
		var per = d.price,
			temp = Math.abs(per);
		var total = getRoundPrice(temp * quantity);
		parentElement.find(".mode-total").text(total);
		if (t.attr("class").indexOf("num-less") > -1) {
		    per = -temp;
		} else {
		    per = temp;
		}
		;
		if (quantity > min) {
		    syncSundryTotalPriceInit(per);
		    syncSundryRatioPriceInit();
		    isContinue = (min + 1);
		} else {
		    isContinue--;
		    if (isContinue >= min) {
			syncSundryTotalPriceInit(per);
			syncSundryRatioPriceInit();
		    }
		}
		;
	    });
	}
	function syncSundryTotalPriceInit(price) {
	    cookbook.sundryPriceTotal = (cookbook.sundryPriceTotal + price);
	    $("#sundry-price").text(getRoundPrice(cookbook.sundryPriceTotal));
	}
	;
	function syncSundryRatioPriceInit() {
	    if (hasRatio) {
		var noRatioPrice = (cookbook.sundryPriceTotal + cookbook.priceTotal);
		var totalRatio = 0;
		$(".ratio").each(function(index, element) {
		    var dRatio = $(element).data("key");
		    var per = getRoundPrice(dRatio.price * noRatioPrice);
		    totalRatio += per;
		    $(element).find(".mode-2-price").text(per);
		    $(element).find(".no-ratio-price").text(noRatioPrice);
		});
		var price = cookbook.sundryPriceTotal + totalRatio;
		$("#sundry-price").text(getRoundPrice(price));
	    }
	}
	;

	$("#isSundryBtn").on("tap", function(event) {
	    event.preventDefault();
	    $("#isSundry").popup("option", "transition", "pop");
	    $("#isSundry").popup("close");
	    var sundryJson = [];
	    var personQuantity = parseInt($(".person .num").text());
	    for (var i = 0, len = person.length; i < len; i++) {
		person[i].quantity = personQuantity;
		sundryJson.push(person[i]);
	    }
	    $(".person-optional").each(function(index, element) {
		var t = $(element);
		var d = t.data("key");
		var personOptionalQuantity = parseInt($(".person-optional .num").text());
		d.quantity = personOptionalQuantity;
		sundryJson.push(d);
	    });
	    sundryJson = sundryJson.concat(amount);
	    sundryJson = sundryJson.concat(ratio);
	    submitDishData(sundryJson);// items
	});
    }
    ;

    function submitDishData(sundryJson) {
	var configObj = {};
	var cid = $.cookie("cid"),
		shopCookie = $.cookie("sc"),
		userUuid = $.cookie('userUuid'),
		userCookie = $.cookie('userCookie');
	var sundryPay = getRoundPrice(parseFloat($("#sundry-price").text()));
	var orderJson = [],
		//sundryJson = "[]",
		discountPrice = getRoundPrice(cookbook.pricesPay + sundryPay),
		originalPrice = getRoundPrice(cookbook.priceTotal + sundryPay);

	$("#cookPanelList li").each(function(index, element) {
	    var t = $(element);
	    orderJson.push(t.data("key"));
	});
	var orderJsonStr = JSON.stringify(orderJson);
	var sundryJsonStr = JSON.stringify(sundryJson);

	configObj.data = {"m": "wechat_save_order", "cityId": cid, "shopCookie": shopCookie, "userUuid": userUuid, "userCookie": userCookie, "preOrderId": 0, "orderJson": orderJsonStr, "sundryJson": sundryJsonStr, "discountPrice": discountPrice, "originalPrice": originalPrice};

	app.p = {};
	app.p.discountPrice = discountPrice;
	app.p.originalPrice = originalPrice;
	app.p.priceBack = getRoundPrice(originalPrice - discountPrice);
	app.ajaxHandler(configObj, function(data) {
	    //if(data.list[0].status==1){
	    window.location.href = "dishlist.aspx?pid=" + data.list[0].info + "&p1=" + originalPrice + "&p2=" + discountPrice + "&p3=" + app.p.priceBack;
	    //}

	    /*
	     app.pid = data.list[0].info;
	     $( ":mobile-pagecontainer" ).pagecontainer( "change", "dishlist.aspx?pid="+data.list[0].info, { role: "page" });
	     $( ":mobile-pagecontainer" ).on( "pagecontainershow", function( event, ui ) {
	     app.dishList();// 不可规避刷新清空了隐藏元素
	     });
	     */
	});
    }


    window['cookbook'] = new CookBook();
    app.cookBook();
});


