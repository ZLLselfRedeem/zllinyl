create proc [dbo].[SqlPager] 
( 
@tblName varchar(8000), -- 表名(注意：可以多表链接) 
@strGetFields varchar(8000) = '*', -- 需要返回的列
@OrderfldName varchar(8000)='', -- 排序的字段名
@realOrderfldName varchar(8000)='', -- 真-排序的字段名
@PageSize int = 10, -- 页尺寸
@PageIndex int = 1, -- 页码
@doCount int = 1 output, --查询到的记录数
@OrderType bit = 0, -- 设置排序类型, 非0 值则降序
@strWhere varchar(8000) = '', -- 查询条件(注意: 不要加where)
@strSqldata varchar(5000) = '' OUTPUT --返回
) 
AS 
declare @strSQL nvarchar(4000) -- 主语句
declare @strTmp varchar(8000) -- 临时变量
declare @strOrder varchar(8000) -- 排序类型

if @realOrderfldName =''
begin
set @realOrderfldName =@OrderfldName
end

if @strWhere != '' 
set @strSQL = 'select @doCount=count(c) from (select count(*) c from ' + @tblName + ' where '+@strWhere+')tmp' 
else 
set @strSQL = 'select @doCount=count(*) from ' + @tblName 
exec sp_executesql @strSQL,N'@doCount int out',@doCount out 
--以上代码的意思是如果@doCount传递过来的不是，就执行总数统计。以下的所有代码都是@doCount为的情况
set @strSQL=''; 

if @OrderType != 0 
begin 
set @strTmp = '<(select min' 
set @strOrder = ' order by ' + @OrderfldName +' desc'--如果@OrderType不是，就执行降序，这句很重要！
end 
else 
begin 
set @strTmp = '>(select max' 
set @strOrder = ' order by ' + @OrderfldName +' asc' 
end 
if @PageIndex = 1 
begin 
if @strWhere != '' 
set @strSQL = 'select top ' + str(@PageSize) +' '+@strGetFields+ ' from ' + @tblName + ' where ' + @strWhere + ' ' + @strOrder 
else 
set @strSQL = 'select top ' + str(@PageSize) +' '+@strGetFields+ ' from '+ @tblName + ' '+ @strOrder--如果是第一页就执行以上代码，这样会加快执行速度
end 
else 
begin--以下代码赋予了@strSQL以真正执行的SQL代码
set @strSQL = 'select top ' + str(@PageSize) +' '+@strGetFields+ ' from ' 
+ @tblName + ' where ' + @OrderfldName + '' + @strTmp + '('+ @realOrderfldName + ') from (select top ' + str((@PageIndex-1)*@PageSize) + ' '+ @OrderfldName + ' from ' + @tblName + ' ' + @strOrder + ') as tblTmp having max('+@realOrderfldName+') >0)'+ @strOrder 
if @strWhere != '' 
set @strSQL = 'select top ' + str(@PageSize) +' '+@strGetFields+ ' from ' 
+ @tblName + ' where ' + @OrderfldName + '' + @strTmp + '(' 
+ @realOrderfldName + ') from (select top ' + str((@PageIndex-1)*@PageSize) + ' ' 
+ @OrderfldName + ' from ' + @tblName + ' where ' + @strWhere + ' ' 
+ @strOrder + ') as tblTmp having max('+@realOrderfldName+') >0) and ' + @strWhere + ' ' + @strOrder 
set @strSqldata=@strSQL
end 
exec (@strSQL)
