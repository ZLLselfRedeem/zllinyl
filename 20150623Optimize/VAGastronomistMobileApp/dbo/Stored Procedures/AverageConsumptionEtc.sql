-- =============================================
-- Author:		xiaoyu
-- Create date: 2012-02-29
-- Description:	根据起始时间统计客流量，总消费，点单数等等
-- =============================================
CREATE PROCEDURE [dbo].[AverageConsumptionEtc] 
	-- Add the parameters for the stored procedure here
	@StartTime DateTime, 
	@EndTime DateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT sum([PeopleNumber]) as 客流量,SUM(TotalToPay) as 总消费, round(SUM(TotalToPay)/sum([PeopleNumber]),2) as 客人平均消费,
  count(OrderID) as 点单数,round(SUM(TotalToPay)/count(OrderID),2) as 每单平均消费,count(DISTINCT [TableName]) as 餐桌数,
  round(SUM(TotalToPay)/count(DISTINCT [TableName]),2) as 每个餐桌平均消费,sum(DATEDIFF(MI, OpenTableTime,OrderTime))/count(OrderID) as 每单平均时间
  FROM [VAGastronomist].[dbo].[OrderInfo] 
  where OrderStatus = 6 and OrderInfo.OrderTime > @StartTime and OrderInfo.OrderTime < @EndTime
END