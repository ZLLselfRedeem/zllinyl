CREATE TABLE [dbo].[EmployeeConnShop] (
    [employeeShopID]      INT      IDENTITY (1, 1) NOT NULL,
    [employeeID]          INT      NULL,
    [shopID]              INT      NULL,
    [companyId]           INT      NULL,
    [isSupportEnterSyb]   BIT      NULL,
    [status]              INT      NULL,
    [serviceStartTime]    DATETIME NULL,
    [serviceEndTime]      DATETIME NULL,
    [isSupportReceiveMsg] BIT      NULL,
    CONSTRAINT [PK_EmployeeShop] PRIMARY KEY CLUSTERED ([employeeShopID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_employeeConnShop_employeeId]
    ON [dbo].[EmployeeConnShop]([employeeID] ASC);

