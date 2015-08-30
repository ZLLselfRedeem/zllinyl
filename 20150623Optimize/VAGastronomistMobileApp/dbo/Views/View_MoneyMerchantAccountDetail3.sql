

CREATE VIEW [dbo].[View_MoneyMerchantAccountDetail3] with SCHEMABINDING
AS
select [date],shopid,sum(t.accountMoney) as total,
(select count(1) from [dbo].[View_MoneyMerchantAccountDetail2]  where accountType=5 and shopid=t.shopid
 and opertime>=t.date and opertime<dateadd(day,1,t.date)) as [count]
from 
(
SELECT cast(cast(year([operTime]) as varchar(4))+'-'+cast(MONTH([operTime]) as varchar(2))+'-'+cast(day([operTime]) as varchar(2)) as datetime) as [date],
shopid,accountMoney
 FROM [dbo].[View_MoneyMerchantAccountDetail2]) as t
 group by [date],shopid

