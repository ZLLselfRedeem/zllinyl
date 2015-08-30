CREATE TABLE [dbo].[MenuConnShop] (
    [menuShopId] INT IDENTITY (1, 1) NOT NULL,
    [menuId]     INT NULL,
    [companyId]  INT NULL,
    [shopId]     INT NULL,
    CONSTRAINT [PK_MenuShop] PRIMARY KEY CLUSTERED ([menuShopId] ASC)
);

