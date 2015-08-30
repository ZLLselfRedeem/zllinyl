<!DOCTYPE html>
<html lang="zh-cn">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title><%=mode.name%></title>

    <!-- Bootstrap -->
    <link href="../AppPages/assets/bootstrap/css/bootstrap.css" rel="stylesheet">
    <link href="../AppPages/assets/bootstrap/css/font-awesome.css" rel="stylesheet">
    <link href="../AppPages/assets/css/doubleEleven.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="http://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
  </head>
  <style>
  h6{ color:#C00; font-weight:bold; font-size:14px;}
  </style>
  <body>
    <div class="container">
    	<section class="intro">
        	<h3 class="text-center text-uppercase"><%=mode.title%></h3>
            <p><%=mode.des%></p>
        </section>
        
        <%
			mode.section.forEach(function(o){
		%>
        <section class="pans">
        	<%if (o.h1 && o.h1.length > 0){%>
          <h5><i class="fa fa-map-marker"></i><%=o.h1%></h5>
					<%}; if (o.h4 && o.h4.length > 0){%>
            <p class="author clearfix"><button class="btn btn-danger pull-right app-order" app-type="<%=o.h4%>" app-value="<%=o.h5%>"><i class="fa fa-random"></i>点菜</button><%=o.h2%></p>
            <%}; if (  o.h3 && o.h3.length > 0 ){ %>
            <h6><%=o.h3%></h6>
            <% };
				
				var items = o.items;
				if ( items && items.length > 0 ){
					items.forEach(function(z){
						if ( z.name === 'p-content' ){
						%><p><%=z.value%></p>
						<%			
						}
									
						else if ( z.name === 'p-img' ){
						%><p><img src="<%=z.value%>" /></p>
                        <%			
						}
									
						else if ( z.name === 'p-title' ){
						%>
                        	<p class="producename text-info bg-info"><%=z.value%></p>
                        <%			
						}
					});
				}
			%>
        </section>
        <%		
			});
		%>
    </div>
    
	<section class="last text-info bg-info">
    	<div class="container">
        <p><i class="fa fa-heart-o"></i> 想吃！！</p>
        <p>这些这些还有这些...一路上有悠先，多吃点不怕。</p>
        <p>微博: @悠先点菜</p>
        <p>微信订阅号: youxiandc</p>
        <p>微信服务号: youxiandiancai</p>
        </div>
    </section>
    
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="http://cdn.bootcss.com/jquery/1.11.1/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="../AppPages/assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="../AppPages/assets/tronjs/tron.js"></script>
    <script language="javascript">
	require('../AppPages/assets/scripts/main', function(project){new project();});
	</script>
  </body>
</html>