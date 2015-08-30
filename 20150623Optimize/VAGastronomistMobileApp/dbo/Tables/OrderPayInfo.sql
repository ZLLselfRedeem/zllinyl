CREATE TABLE [dbo].[OrderPayInfo] (
    [OrderPayID]           INT        IDENTITY (1, 1) NOT NULL,
    [OrderID]              INT        NULL,
    [PaymentID]            INT        NULL,
    [Amount]               FLOAT (53) NULL,
    [OrderPayTime]         DATETIME   NULL,
    [OrderPayStatus]       INT        NULL,
    [cityLedgerCompanyID]  INT        NULL,
    [cityLedgerCustomerID] INT        NULL,
    CONSTRAINT [PK_OrderPayInfo] PRIMARY KEY CLUSTERED ([OrderPayID] ASC)
);

