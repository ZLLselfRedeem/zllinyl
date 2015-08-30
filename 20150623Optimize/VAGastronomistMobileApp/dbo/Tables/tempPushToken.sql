CREATE TABLE [dbo].[tempPushToken] (
    [CustomerID]        BIGINT         NOT NULL,
    [UserName]          NVARCHAR (100) NULL,
    [mobilePhoneNumber] NVARCHAR (50)  NULL,
    [pushToken]         NVARCHAR (100) NULL,
    [appType]           INT            NULL
);

