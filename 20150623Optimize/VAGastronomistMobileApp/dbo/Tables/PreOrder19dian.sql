CREATE TABLE [dbo].[PreOrder19dian] (
    [preOrder19dianId]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [menuId]                        INT            NULL,
    [companyId]                     INT            NULL,
    [shopId]                        INT            NULL,
    [customerId]                    BIGINT         NULL,
    [orderInJson]                   NVARCHAR (MAX) NULL,
    [customerUUID]                  NVARCHAR (100) NULL,
    [status]                        TINYINT        NULL,
    [preOrderSum]                   FLOAT (53)     NULL,
    [preOrderServerSum]             FLOAT (53)     NULL,
    [preOrderTime]                  DATETIME       NULL,
    [isPaid]                        TINYINT        NULL,
    [prePaidSum]                    FLOAT (53)     NULL,
    [prePayTime]                    DATETIME       NULL,
    [viewallocCommission]           FLOAT (53)     NULL,
    [transactionFee]                FLOAT (53)     NULL,
    [viewallocNeedsToPayToShop]     FLOAT (53)     NULL,
    [viewallocPaidToShopSum]        FLOAT (53)     NULL,
    [viewallocTransactionCompleted] TINYINT        NULL,
    [isApproved]                    TINYINT        NULL,
    [verifiedSaving]                FLOAT (53)     CONSTRAINT [DF_PreOrder19dian_verifiedSaving] DEFAULT ((0)) NULL,
    [isShopConfirmed]               TINYINT        NULL,
    [invoiceTitle]                  NVARCHAR (50)  NULL,
    [sundryJson]                    NVARCHAR (MAX) NULL,
    [refundMoneySum]                FLOAT (53)     NULL,
    [discount]                      FLOAT (53)     NULL,
    [refundMoneyClosedSum]          FLOAT (53)     NULL,
    [deskNumber]                    NVARCHAR (50)  NULL,
    [refundRedEnvelope]             FLOAT (53)     NULL,
    [appType]                       INT            NULL,
    [appBuild]                      NVARCHAR (100) NULL,
    [isEvaluation]                  TINYINT        CONSTRAINT [DF_PreOrder19dian_isEvaluation] DEFAULT ((0)) NULL,
    [expireTime]                    DATETIME       NULL,
    CONSTRAINT [PK_PreOrder19dian] PRIMARY KEY CLUSTERED ([preOrder19dianId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [customerId]
    ON [dbo].[PreOrder19dian]([customerId] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_preorder19dian_shopId]
    ON [dbo].[PreOrder19dian]([shopId] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_preOrder19dian_isPaid_Staus]
    ON [dbo].[PreOrder19dian]([isPaid] ASC, [status] ASC)
    INCLUDE([preOrder19dianId], [shopId], [customerId], [prePaidSum], [prePayTime], [isApproved], [isShopConfirmed], [refundMoneySum]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已评价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreOrder19dian', @level2type = N'COLUMN', @level2name = N'isEvaluation';

