CREATE TABLE [dbo].[ClientRecharge] (
    [id]                INT            IDENTITY (1, 1) NOT NULL,
    [name]              NVARCHAR (100) NULL,
    [rechargeCondition] FLOAT (53)     NULL,
    [present]           FLOAT (53)     NULL,
    [beginTime]         NVARCHAR (50)  NULL,
    [endTime]           NVARCHAR (50)  NULL,
    [externalSold]      INT            NULL,
    [actualSold]        INT            NULL,
    [status]            INT            NULL,
    [createTime]        DATE           NULL,
    [sequence]          INT            NULL,
    CONSTRAINT [PK_Recharge] PRIMARY KEY CLUSTERED ([id] ASC)
);

