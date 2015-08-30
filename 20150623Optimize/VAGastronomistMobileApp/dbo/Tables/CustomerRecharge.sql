CREATE TABLE [dbo].[CustomerRecharge] (
    [id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [customerId]        BIGINT         NULL,
    [customerCookie]    NVARCHAR (100) NULL,
    [customerUUID]      NVARCHAR (100) NULL,
    [rechargeId]        INT            NULL,
    [shopId]            INT            NULL,
    [preOrder19dianId]  BIGINT         NULL,
    [rechargeTime]      DATETIME       NULL,
    [payStatus]         INT            NULL,
    [payTime]           DATETIME       NULL,
    [payMode]           INT            NULL,
    [rechargeCondition] FLOAT (53)     NULL,
    [rechargePresent]   FLOAT (53)     NULL,
    CONSTRAINT [PK_CustomerRecharge] PRIMARY KEY CLUSTERED ([id] ASC)
);

