


CREATE VIEW [dbo].[View_MoneyMerchantAccountDetail2] with SCHEMABINDING
AS
SELECT accountId,accountTypeConnId,accountType,accountMoney,remainMoney,shopId,
(select max([shopConfirmedTime]) from [dbo].[PreorderShopConfirmedInfo] b where b.preOrder19dianId= a.accountTypeConnId and b.status=1) as operTime
  FROM [dbo].MoneyMerchantAccountDetail a
  where a.accountType in(4,5,13)


