-- =============================================
-- Author:		xiaoyu
-- Create date: 2011-10-25
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[TurnoverStatisticsByMonth] 
	-- Add the parameters for the stored procedure here
	@StartTime DateTime,@EndTime DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
declare @s nvarchar(max),@s1 nvarchar(max),@sql nvarchar(max)
--获得列表头[a],[b]
select @s=isnull(@s+',','')+'sum(['+ltrim(PaymentID)+ ']) as ['+ PaymentName +']',@s1=isnull(@s1+',','')+'['+ltrim(PaymentID)+']' from PaymentInfo
set @sql='select m.*,n.总金额 from
(select time as [时间],'+@s+' from(select convert(varchar(7),[OrderPayTime],120)time,'+@s1+' from [OrderPayInfo] 
pivot(sum(Amount) for PaymentID in('+@s1+'))b where OrderPayStatus > 0 and OrderPayTime > '''+convert(varchar(10),@StartTime,120)+''' and OrderPayTime < '''+convert(varchar(10),@EndTime,120)+''')t group by time) m,
(select convert(varchar(7),[OrderPayTime],120) as 时间,SUM(Amount) as 总金额
FROM [OrderPayInfo] where OrderPayStatus > 0 group by convert(varchar(7),[OrderPayTime],120))n
where m.时间 = n.时间'

exec(@sql)
END
