define(function(require, exports, module){
	
	var maker = new Class(function(){
		this.section_order = $('#sectionItems fieldset').size();
		this.templates = {};
		this.getTemplate('section', 'section.asp');
		this.addSection();
		this.onBind();
		
		this.data = {};
	});
	
	maker.add('getTemplate', function(name, file){
		var that = this;
		$.get('templates/' + file, function(html){
			that.templates[name] = html;
		});
	});

	maker.add("addSection", function(){
		var that = this;
		$('#addSection').on('click', function(){
			that.section_order++;
			var temp = that.templates.section;
			temp = temp.replace(/\{number\}/g, that.section_order).replace(/\{[^\}]+\}/g, '');
			$('#sectionItems').append(temp);
		});
	});
	
	
	maker.add('onBind', function(){
		var that = this;
		$('body').on('click', '.ac-p', function(){
			$(this).parents('.fieldset-content:first').find('.dc-content').append('<div class="form-group remove" data-name="p"><label class="col-sm-2 control-label"><a href="javascript:;" class="fa fa-close"></a>段落内容</label><div class="col-sm-10"><textarea class="form-control" name="p-content"></textarea></div></div>');
		});
		$('body').on('click', '.ac-g', function(){
			$(this).parents('.fieldset-content:first').find('.dc-content').append('<div class="form-group remove" data-name="g"><label class="col-sm-2 control-label"><a href="javascript:;" class="fa fa-close"></a>图片地址</label><div class="col-sm-10"><input type="text" class="form-control" placeholder="" name="p-img" value=""></div></div>');
		});
		$('body').on('click', '.ac-t', function(){
			$(this).parents('.fieldset-content:first').find('.dc-content').append('<div class="form-group remove" data-name="t"><label class="col-sm-2 control-label"><a href="javascript:;" class="fa fa-close"></a>小标题</label><div class="col-sm-10"><input type="text" class="form-control" placeholder="" name="p-title" value=""></div></div>');
		});
		$('#save').on('click', function(){
			that.render();
			$.post(window.modules.maker.render, {
				data: JSON.stringify(that.data)
			}, function(params){
				alert(params.message);
			}, 'json');
		});
		
		$('body').on('click', '.fa-close', function(){
			$(this).parents('.remove:first').remove();
		})
	});
	
	
	maker.add('render', function(){
		var that = this;
		this.data.name = $("input[name='subject_name']").val();
		this.data.file = $("input[name='subject_file']").val();
		this.data.title = $("input[name='subject_title']").val();
		this.data.des = $("textarea[name='subject_des']").val();
		this.data.section = [];
		
		$('fieldset').each(function(){
			var num = $(this).attr('data-number');
			var js = {}, ar = [];
			for ( var i = 0 ; i < 5 ; i++ ){
				js['h' + (i + 1)] = $(this).find("[name='subject_h" + (i + 1) + "']").val();
			}
			$(this).find('.dc-content .form-group .form-control').each(function(){
				var name = $(this).attr('name');
				var value = $(this).val();
				ar.push({
					name: name,
					value: value
				});
			});
			if ( ar.length > 0 ){
				js.items = ar;
			}
			that.data.section.push(js);
		});
	});
	
	
	return maker;
});

