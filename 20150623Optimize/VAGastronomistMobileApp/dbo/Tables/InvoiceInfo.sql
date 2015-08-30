CREATE TABLE [dbo].[InvoiceInfo] (
    [invoiceId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [invoiceTitle] NVARCHAR (50) NULL,
    [customerId]   INT           NULL,
    CONSTRAINT [PK_InvoiceInfo] PRIMARY KEY CLUSTERED ([invoiceId] ASC)
);

