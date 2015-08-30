<!DOCTYPE html>
<html lang="zh-cn">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>专题制作系统</title>

<!-- Bootstrap -->
<link href="../AppPages/assets/bootstrap/css/bootstrap.css" rel="stylesheet">
<link href="../AppPages/assets/bootstrap/css/font-awesome.css" rel="stylesheet">

<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="http://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<style>
body{font: normal 12px/1.6em Microsoft YaHei,Tahoma,simsun; color:#777; background-color:#f3f3f3;}

.fa {
	margin-right: 5px;
}

.actions a{ font-size:18px; margin:0px 10px;}
.form-control{ padding:3px 8px; height:28px; font-size:12px; border-radius:0; border-color:#ddd; box-shadow:3px 3px 1px rgba(0,0,0,.04); resize:none;}
.fa-close:link,
.fa-close:visited{ color:#ff0000;}
.fa-close:hover{ color:#CC3}
fieldset{ display:block; background-color:#fff; padding:15px 25px; margin-bottom:50px; box-shadow:4px 4px 1px rgba(0,0,0,.04)}
fieldset legend{ display:block; line-height:50px; padding:0; margin:0; padding-top:60px;}
.list-group-item{ border-radius:0px!important;}
</style>
<body>




<nav class="navbar navbar-default navbar-fixed-top" role="navigation">
  <div class="container-fluid">
    <!-- Brand and toggle get grouped for better mobile display -->
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a class="navbar-brand" href="#">iPress</a>
    </div>

    <!-- Collect the nav links, forms, and other content for toggling -->
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav">
        <%if ( m && m.length > 0 ){%><li><a href="<%=iPress.setURL('view', 'web', { m: m })%>" target="_blank">预览本专题</a></li><%}%>
      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li><a href="javascript:;" id="addSection">添加区块</a></li>
        <li><a href="<%=iPress.setURL('maker', 'web')%>">生成新专题</a></li>
      </ul>
    </div><!-- /.navbar-collapse -->
  </div><!-- /.container-fluid -->
</nav>

<div class="container" style="margin-top:60px;">
  <div class="row">
    <div class="col-xs-3 tools">
    	<h5>过往专题文件</h5>
    	<div class="list-group">
        	<%
				files.forEach(function(o){
			%>
            <a href="<%=iPress.setURL('maker', 'web', {m: o.split('.').slice(0, -1).join('.')})%>" class="list-group-item <%=m == o.split('.').slice(0, -1).join('.') ? 'active' : ''%>"><%=o%></a>
            <%
				});
			%>
        </div>
    </div>
    <div class="col-xs-9">
      <form action="<%=iPress.setURL('maker', 'post')%>" method="post" class="form-horizontal" role="form" style="margin-top:30px;">
      	<div style="background-color:#fff; padding:15px 25px;box-shadow:4px 4px 1px rgba(0,0,0,.04)">
        <h4 style="margin-bottom:30px;">基本参数设置</h4>
        <div class="form-group">
          <label for="inputEmail3" class="col-sm-2 control-label">专题名称</label>
          <div class="col-sm-10">
            <input type="email" class="form-control" id="inputEmail3" placeholder="" name="subject_name" value="<%=mode.name || ''%>">
          </div>
        </div>
        <div class="form-group">
          <label for="inputPassword3" class="col-sm-2 control-label">专题生成文件名</label>
          <div class="col-sm-10">
            <input type="text" class="form-control" id="inputPassword3" placeholder="" name="subject_file" value="<%=mode.file || ''%>">
          </div>
        </div>
        <div class="form-group">
          <label for="inputEmail3" class="col-sm-2 control-label">专题标题</label>
          <div class="col-sm-10">
            <input type="email" class="form-control" id="inputEmail3" placeholder="" name="subject_title" value="<%=mode.title || ''%>">
          </div>
        </div>
        <div class="form-group">
          <label for="inputEmail3" class="col-sm-2 control-label">专题描述</label>
          <div class="col-sm-10">
            <textarea class="form-control" name="subject_des" style="height: 150px;"><%=mode.des || ''%></textarea>
          </div>
        </div>
        </div>
        <div id="sectionItems">
        	<%
			if ( mode.section ){
				mode.section.forEach(function(o, i){
			%>
            <fieldset data-number="<%=i+1%>" class="remove">
                  <legend><a href="javascript:;" class="fa fa-close"></a>专题内容区块<%=i+1%></legend>
                  <div class="fieldset-content">
                    <div class="form-group remove">
                      <label for="inputPassword3" class="col-sm-2 control-label">区块标题</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="inputPassword3" placeholder="" name="subject_h1" value="<%=o.h1%>">
                      </div>
                    </div>
                    <div class="form-group remove">
                      <label for="inputPassword3" class="col-sm-2 control-label">店铺名称</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="inputPassword3" placeholder="" name="subject_h2" value="<%=o.h2%>">
                      </div>
                    </div>
                    <div class="form-group remove">
                      <label for="inputEmail3" class="col-sm-2 control-label">说明</label>
                      <div class="col-sm-10">
                        <textarea class="form-control" name="subject_h3" style="height: 100px;"><%=o.h3%></textarea>
                      </div>
                    </div>
                    <div class="form-group remove">
                      <label for="inputPassword3" class="col-sm-2 control-label">点菜按钮type</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="inputPassword3" placeholder="" name="subject_h4" value="<%=o.h4%>">
                      </div>
                    </div>
                    <div class="form-group remove">
                      <label for="inputPassword3" class="col-sm-2 control-label">点菜按钮value</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="inputPassword3" placeholder="" name="subject_h5" value="<%=o.h5%>">
                      </div>
                    </div>
                    <div class="actions" style="border:1px dashed #ddd; padding:10px 30px; margin-bottom:30px; text-align:center;">
                        <a class="fa fa-plus ac-p" href="javascript:;"></a>
                        <a class="fa fa-picture-o ac-g" href="javascript:;"></a>
                        <a class="fa fa-tag ac-t" href="javascript:;"></a>
                    </div>
                    <div class="dc-content">
                    	<%
							if ( o.items && o.items.length > 0 ){
								o.items.forEach(function(z){
									if ( z.name === 'p-content' ){
						%>
                        	<div class="form-group remove" data-name="p"><label class="col-sm-2 control-label"><a href="javascript:;" class="fa fa-close"></a>段落内容</label><div class="col-sm-10"><textarea class="form-control" name="p-content"><%=z.value%></textarea></div></div>
                        <%			
									}
									
									else if ( z.name === 'p-img' ){
						%>
                        	<div class="form-group remove" data-name="g"><label class="col-sm-2 control-label"><a href="javascript:;" class="fa fa-close"></a>图片地址</label><div class="col-sm-10"><input type="text" class="form-control" placeholder="" name="p-img" value="<%=z.value%>"></div></div>
                        <%			
									}
									
									else if ( z.name === 'p-title' ){
						%>
                        	<div class="form-group remove" data-name="t"><label class="col-sm-2 control-label"><a href="javascript:;" class="fa fa-close"></a>小标题</label><div class="col-sm-10"><input type="text" class="form-control" placeholder="" name="p-title" value="<%=z.value%>"></div></div>
                        <%			
									}
								});
							}
						%>
                    </div>
                  </div>
                </fieldset>
            <%
				});
				}
			%>
        </div>
        <div class="form-group" style="margin-top:30px;">
            <button class="btn btn-info" type="button" id="save">保存专题到缓存</button>
        </div>
      </form>
    </div>
  </div>
</div>
<script src="http://tron.webkits.cn/tron.min.js"></script> 
<script src="http://tron.webkits.cn/tron.maps.js"></script> 
<script language="javascript">
	Library.setBase('SpecialSubject');
	require('jquery').then(function(){
		return require('../AppPages/assets/bootstrap/js/bootstrap.min.js');
	}).then(function(){
		require('compiles/makers.js', function(project){new project();});
	});
</script>
<%
modules.scriptExec(function( data ){
	window.modules.maker = data;
}, {
	render: iPress.setURL('maker', 'post')
});
%>
</body>
</html>