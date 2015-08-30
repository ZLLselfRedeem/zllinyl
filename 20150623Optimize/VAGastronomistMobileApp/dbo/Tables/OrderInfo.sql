CREATE TABLE [dbo].[OrderInfo] (
    [OrderID]          INT            IDENTITY (1, 1) NOT NULL,
    [OrderTime]        DATETIME       NULL,
    [TableName]        NVARCHAR (50)  NULL,
    [TableID]          INT            NULL,
    [PeopleNumber]     SMALLINT       NULL,
    [TotalToPay]       FLOAT (53)     NULL,
    [OrderAssessment]  NVARCHAR (MAX) NULL,
    [OrderScore]       FLOAT (53)     NULL,
    [OrderStatus]      SMALLINT       NULL,
    [EmployeeID]       NVARCHAR (50)  NULL,
    [DisCount]         FLOAT (53)     NULL,
    [ServiceCharge]    FLOAT (53)     NULL,
    [OrderNote]        NVARCHAR (500) NULL,
    [CustomerUserName] NVARCHAR (50)  NULL,
    [QueueID]          INT            NULL,
    [OpenTableTime]    DATETIME       NULL,
    [removeChange]     FLOAT (53)     NULL,
    [roomNumber]       NVARCHAR (50)  NULL,
    [customerTypeId]   INT            NULL,
    [isChecked]        BIT            NULL,
    CONSTRAINT [PK_OrderInfo] PRIMARY KEY CLUSTERED ([OrderID] ASC)
);

