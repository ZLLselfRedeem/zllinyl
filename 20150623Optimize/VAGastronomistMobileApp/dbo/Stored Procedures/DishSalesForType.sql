-- =============================================
-- Author:		xiaoyu
-- Create date: 2011-10-27
-- Description:	根据起始时间按照结算分类统计菜销量
-- =============================================
CREATE PROCEDURE [dbo].[DishSalesForType] 
	-- Add the parameters for the stored procedure here
	@StartTime DateTime, 
	@EndTime DateTime,
	@customerTypeId Int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    if(@customerTypeId = 0)
		--select DISTINCT DiscountTypeName as 分类名,sum(DishQuantity) as 数量,sum(DishPriceSum) as 金额
		--from OrderDetailInfo INNER JOIN OrderInfo ON OrderInfo.OrderID = OrderDetailInfo.OrderID
		--INNER JOIN DishInfo ON OrderDetailInfo.DishID = DishInfo.DishID
		--INNER JOIN DiscountType ON DishInfo.DiscountTypeID = DiscountType.DiscountTypeID
		--where OrderInfo.OrderStatus = '6' and OrderDetailInfo.OrderDetailStatus > '0' and OrderInfo.OrderTime > @StartTime and OrderInfo.OrderTime < @EndTime
		--group by DiscountTypeName order by 数量 DESC
		select DISTINCT DiscountTypeName as 分类名,sum(DishQuantity) as 数量,sum(DishPriceSum) as 金额
		from OrderDetailInfo INNER JOIN OrderInfo ON OrderInfo.OrderID = OrderDetailInfo.OrderID
		INNER JOIN DiscountType ON OrderDetailInfo.discountTypeId = DiscountType.DiscountTypeID
		where OrderInfo.OrderStatus = '6' and OrderDetailInfo.OrderDetailStatus > '0' and OrderInfo.OrderTime > @StartTime and OrderInfo.OrderTime < @EndTime
		group by DiscountTypeName order by 数量 DESC

	else
		--select DISTINCT DiscountTypeName as 分类名,sum(DishQuantity) as 数量,sum(DishPriceSum) as 金额
		--from OrderDetailInfo INNER JOIN OrderInfo ON OrderInfo.OrderID = OrderDetailInfo.OrderID
		--INNER JOIN DishInfo ON OrderDetailInfo.DishID = DishInfo.DishID
		--INNER JOIN DiscountType ON DishInfo.DiscountTypeID = DiscountType.DiscountTypeID
		--where OrderInfo.OrderStatus = '6' and OrderDetailInfo.OrderDetailStatus > '0' and OrderInfo.OrderTime > @StartTime and OrderInfo.OrderTime < @EndTime
		--and OrderInfo.customerTypeId = @customerTypeId
		--group by DiscountTypeName order by 数量 DESC
		select DISTINCT DiscountTypeName as 分类名,sum(DishQuantity) as 数量,sum(DishPriceSum) as 金额
		from OrderDetailInfo INNER JOIN OrderInfo ON OrderInfo.OrderID = OrderDetailInfo.OrderID
		INNER JOIN DiscountType ON OrderDetailInfo.discountTypeId = DiscountType.DiscountTypeID
		where OrderInfo.OrderStatus = '6' and OrderDetailInfo.OrderDetailStatus > '0' and OrderInfo.OrderTime > @StartTime and OrderInfo.OrderTime < @EndTime
		and OrderInfo.customerTypeId = @customerTypeId
		group by DiscountTypeName order by 数量 DESC
END