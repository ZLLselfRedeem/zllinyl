CREATE TABLE [dbo].[CommonCurrentSellOffInfo] (
    [id]                  BIGINT IDENTITY (1, 1) NOT NULL,
    [menuId]              INT    NULL,
    [DishI18nID]          INT    NULL,
    [status]              INT    NULL,
    [currentSellOffCount] INT    NULL,
    [DishPriceI18nID]     INT    NULL,
    CONSTRAINT [PK_CommonCurrentSellOffInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

