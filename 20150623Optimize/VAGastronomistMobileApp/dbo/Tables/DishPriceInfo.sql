CREATE TABLE [dbo].[DishPriceInfo] (
    [DishPriceID]              INT        IDENTITY (1, 1) NOT NULL,
    [DishPrice]                FLOAT (53) NULL,
    [DishID]                   INT        NULL,
    [DishSoldout]              BIT        NULL,
    [DishPriceStatus]          INT        NULL,
    [DishNeedWeigh]            BIT        NULL,
    [vipDiscountable]          BIT        NULL,
    [backDiscountable]         BIT        NULL,
    [dishIngredientsMinAmount] INT        NULL,
    [dishIngredientsMaxAmount] INT        NULL,
    CONSTRAINT [PK_DishPriceInfo] PRIMARY KEY CLUSTERED ([DishPriceID] ASC)
);

