CREATE TABLE [dbo].[RechargeLog] (
    [id]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [employeeId]         INT            NULL,
    [amount]             FLOAT (53)     NULL,
    [operateTime]        DATETIME       NULL,
    [remark]             NVARCHAR (300) NULL,
    [customerPhone]      NVARCHAR (MAX) NULL,
    [cookie]             NVARCHAR (300) NULL,
    [status]             INT            NULL,
    [approvalTime]       DATETIME       NULL,
    [approvalEmployeeId] INT            NULL,
    CONSTRAINT [PK_RechargeLog] PRIMARY KEY CLUSTERED ([id] ASC)
);

