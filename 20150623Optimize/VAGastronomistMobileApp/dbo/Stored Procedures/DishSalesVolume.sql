-- =============================================
-- Author:		xiaoyu
-- Create date: 2011-10-27
-- Description:	根据起始时间按照菜名统计菜销量
-- =============================================
CREATE PROCEDURE [dbo].[DishSalesVolume] 
	-- Add the parameters for the stored procedure here
	@StartTime DateTime, 
	@EndTime DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select DISTINCT DishName as 菜名,sum(DishQuantity) as 数量,sum(DishPriceSum) as 金额,OrderDetailInfo.discountTypeId,OrderDetailInfo.DishID
from OrderDetailInfo INNER JOIN OrderInfo ON OrderInfo.OrderID = OrderDetailInfo.OrderID
--Inner join DishInfo ON OrderDetailInfo.DishID = DishInfo.DishID
where OrderInfo.OrderStatus = '6' and OrderDetailInfo.OrderDetailStatus > '0' and OrderInfo.OrderTime > @StartTime and OrderInfo.OrderTime < @EndTime
group by DishName,OrderDetailInfo.discountTypeId,OrderDetailInfo.DishID order by 数量 DESC
END
