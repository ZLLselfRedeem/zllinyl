CREATE TABLE [dbo].[Money19dianDetail] (
    [id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [customerId]        BIGINT        NULL,
    [changeReason]      NVARCHAR (50) NULL,
    [changeValue]       FLOAT (53)    NULL,
    [changeTime]        DATETIME      NULL,
    [remainMoney]       FLOAT (53)    NULL,
    [accountType]       INT           NULL,
    [accountTypeConnId] VARCHAR (100) NULL,
    [inoutcomeType]     INT           NULL,
    [flowNumber]        VARCHAR (50)  NULL,
    [companyId]         INT           NULL,
    [shopId]            INT           NULL,
    CONSTRAINT [PK_Money19dian] PRIMARY KEY CLUSTERED ([id] ASC)
);

