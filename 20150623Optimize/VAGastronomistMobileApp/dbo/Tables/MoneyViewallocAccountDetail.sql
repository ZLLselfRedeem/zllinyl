CREATE TABLE [dbo].[MoneyViewallocAccountDetail] (
    [accountId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [accountMoney]      FLOAT (53)     NULL,
    [remainMoney]       FLOAT (53)     NULL,
    [accountType]       INT            NULL,
    [accountTypeConnId] VARCHAR (100)  NULL,
    [inoutcomeType]     INT            NULL,
    [operUser]          VARCHAR (100)  NULL,
    [operTime]          DATETIME       NULL,
    [outcomeCompanyId]  INT            NULL,
    [outcomeShopId]     INT            NULL,
    [remark]            NVARCHAR (500) NULL,
    CONSTRAINT [PK_MoneyViewallocAccountDetail] PRIMARY KEY CLUSTERED ([accountId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'公司账户明细', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'accountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'accountMoney';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前余额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'remainMoney';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收支来源:支付宝充值，:银联充值，：用户取消订单4.商户退款5.财务对账6.商户结账', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'accountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源关联ID(点单ID，回款ID等)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'accountTypeConnId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收支类型，：收入，：支出', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'inoutcomeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作人员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'operUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'operTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提款公司', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'outcomeCompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提款店铺', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'outcomeShopId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyViewallocAccountDetail', @level2type = N'COLUMN', @level2name = N'remark';

