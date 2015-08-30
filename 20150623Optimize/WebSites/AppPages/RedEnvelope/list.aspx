﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="AppPages_RedEnvelope_list" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<meta name="renderer" content="webkit" />
<title>我的红包</title>
<link href="css/style.css" rel="stylesheet" />
<script type="text/javascript" src="../../series/series.min.js"></script>
<script type="text/javascript" src="../../series/series.map.js"></script>
</head>
<body>
<div id="content" class="hide">
  <div id="top"> <img src="img/topBg.gif" />
    <p class="pleft"><span class="topPrice" id="canUsePrice">￥<#=executedRedEnvelopeAmount#></span><span>可使用金额</span></p>
    <p class="pright"><span class="topPrice" id="cantUsePrice">￥<#=notExecutedRedEnvelopeAmount#></span><span>未生效金额</span></p>
  </div>
  <div id="choose"> <a class="selected getLists" href="javascript:;" data-type="present">当前</a> <a href="javascript:;" class="getLists" data-type="history">历史</a> </div>
  <div class="c"></div>
  <span id="remind"><#if ( expireCount > 0 ){#>您有<#=expireCount#>个红包即将过期<#};#></span>
  <div id="redEnvelope"></div>
  <div class="tip"></div>
</div>
<div id="template" class="hide">
<#_.each(detailList, function(list){#>
  <div class="redEnvelopeMain">
    <div class="redEnvelopeImg">
      <div class="redEnvelopeImg-1"> <img src="img/RedEnvelope.png" /> <span>红包</span> </div>
    </div>
    <div class="redEnvelopeInfo"> <span class="price price<#=list.statusType#>">￥<#=list.amount#> <#if (list.usedAmount != 0){#><span class="priceDesc">(已使用￥<#=list.usedAmount#>)</span><#};#></span>
      <div class="effectiveTime">
      	<#if (list.statusType == 1){#>
        		生效时间：<#=list.effectTime#>
        <#}else{#>
        	<#if(list.effectTime == 'inactive'){#>
        		订单消费完成后生效
        	<#}else{#>
        		有效期至：<#=list.expireTime#>
        	<#};#>
        <#};#>
      </div>
      <img class="effextiveStatus" src="img/status<#=list.statusType#>.png" /> </div>
  </div>
  <div class="c"></div>
<#});#>
</div>
<script type="text/javascript"> require(['js/NewRedEnvelope']); </script>
</body>
</html>
