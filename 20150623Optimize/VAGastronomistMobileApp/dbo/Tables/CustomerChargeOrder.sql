CREATE TABLE [dbo].[CustomerChargeOrder] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [createTime]     DATETIME       NULL,
    [payTime]        DATETIME       NULL,
    [status]         TINYINT        NULL,
    [customerCookie] NVARCHAR (100) NULL,
    [customerUUID]   NVARCHAR (100) NULL,
    [priceSum]       FLOAT (53)     NULL,
    [customerId]     BIGINT         NULL,
    [subjectName]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_CustomerChargeOrder] PRIMARY KEY CLUSTERED ([id] ASC)
);

