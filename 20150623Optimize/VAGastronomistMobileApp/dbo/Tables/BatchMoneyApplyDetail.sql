CREATE TABLE [dbo].[BatchMoneyApplyDetail] (
    [batchMoneyApplyDetailId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [batchMoneyApplyId]       INT            NULL,
    [accountId]               BIGINT         NULL,
    [operateEmployee]         INT            NULL,
    [companyId]               INT            NULL,
    [shopId]                  INT            NULL,
    [accountNum]              VARCHAR (40)   NULL,
    [bankName]                NVARCHAR (100) NULL,
    [accountName]             NVARCHAR (100) NULL,
    [applyAmount]             FLOAT (53)     NULL,
    [serialNumberOrRemark]    NVARCHAR (500) NULL,
    [status]                  INT            NULL,
    [haveAdjustAmount]        FLOAT (53)     NULL,
    [cityId]                  INT            NULL,
    CONSTRAINT [PK_BatchMoneyApplyDetail] PRIMARY KEY CLUSTERED ([batchMoneyApplyDetailId] ASC)
);

