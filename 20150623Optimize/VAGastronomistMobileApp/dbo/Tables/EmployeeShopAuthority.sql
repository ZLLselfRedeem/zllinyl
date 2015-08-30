CREATE TABLE [dbo].[EmployeeShopAuthority] (
    [employeeShopAuthorityId]     INT IDENTITY (1, 1) NOT NULL,
    [employeeConnShopId]          INT NOT NULL,
    [shopAuthorityId]             INT NOT NULL,
    [employeeShopAuthorityStatus] INT NOT NULL,
    CONSTRAINT [PK_EmployeeShopAuthority] PRIMARY KEY CLUSTERED ([employeeShopAuthorityId] ASC)
);

