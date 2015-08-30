CREATE TABLE [dbo].[BusniessPay] (
    [id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [Btime]     DATETIME      NULL,
    [pay]       FLOAT (53)    NULL,
    [type]      INT           NULL,
    [companyId] INT           NULL,
    [shopId]    INT           NULL,
    [paymentId] VARCHAR (100) NULL,
    CONSTRAINT [PK_BusniessPay] PRIMARY KEY CLUSTERED ([id] ASC)
);

