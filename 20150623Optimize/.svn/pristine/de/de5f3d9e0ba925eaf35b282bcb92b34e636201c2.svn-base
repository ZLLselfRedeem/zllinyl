
CREATE VIEW [dbo].[View_MoneyMerchantAccountDetail] with SCHEMABINDING
AS
SELECT accountId,accountTypeConnId,accountType,accountMoney,remainMoney,shopId,
case when a.accountType=6 then a.operTime else
(select max([shopConfirmedTime]) from [dbo].[PreorderShopConfirmedInfo] b where b.preOrder19dianId= a.accountTypeConnId and b.status=1) end as operTime,
case when a.accountType=6 then '' else
(SELECT d.mobilePhoneNumber
  FROM [dbo].[PreOrder19dian] c
  inner join [dbo].[CustomerInfo] d on c.[customerId]=d.CustomerID
where c.[preOrder19dianId]=a.accountTypeConnId
) end as mobilePhoneNumber
  FROM [dbo].MoneyMerchantAccountDetail a
  where a.accountType in(4,5,6,13);

