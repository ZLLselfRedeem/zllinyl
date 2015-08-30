CREATE TABLE [dbo].[PreorderShopConfirmedInfo] (
    [id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [preOrder19dianId]  BIGINT         NULL,
    [employeeId]        INT            NULL,
    [employeeName]      NVARCHAR (100) NULL,
    [employeePosition]  NVARCHAR (100) NULL,
    [shopConfirmedTime] DATETIME       NULL,
    [status]            TINYINT        NULL,
    CONSTRAINT [PK_PreorderShopConfirmedInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

