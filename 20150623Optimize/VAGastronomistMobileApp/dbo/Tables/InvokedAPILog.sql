CREATE TABLE [dbo].[InvokedAPILog] (
    [id]             BIGINT   IDENTITY (1, 1) NOT NULL,
    [invokedAPIType] INT      NULL,
    [invokedAPITime] DATETIME NULL,
    CONSTRAINT [PK_InvokedAPILog] PRIMARY KEY CLUSTERED ([id] ASC)
);

