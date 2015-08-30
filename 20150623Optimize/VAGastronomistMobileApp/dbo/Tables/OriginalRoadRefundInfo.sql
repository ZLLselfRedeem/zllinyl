CREATE TABLE [dbo].[OriginalRoadRefundInfo] (
    [id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [type]                INT            NULL,
    [connId]              BIGINT         NULL,
    [refundAmount]        FLOAT (53)     NULL,
    [applicationTime]     DATETIME       NULL,
    [remittanceTime]      DATETIME       NULL,
    [status]              INT            NULL,
    [remitEmployee]       INT            NULL,
    [customerMobilephone] NVARCHAR (50)  NULL,
    [note]                NVARCHAR (500) NULL,
    [customerUserName]    NVARCHAR (100) NULL,
    [employeeId]          INT            NULL,
    [RefundPayType]       INT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_OriginalRoadRefundInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

