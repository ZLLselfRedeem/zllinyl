CREATE TABLE [dbo].[Meal] (
    [MealID]         INT            IDENTITY (1, 1) NOT NULL,
    [ShopID]         INT            NOT NULL,
    [Price]          FLOAT (53)     NOT NULL,
    [MealName]       NVARCHAR (100) NOT NULL,
    [Menu]           NVARCHAR (MAX) NOT NULL,
    [Suggestion]     NVARCHAR (200) NOT NULL,
    [ImageURL]       NVARCHAR (200) NOT NULL,
    [OriginalPrice]  FLOAT (53)     NOT NULL,
    [IsActive]       INT            NOT NULL,
    [OrderNumber]    INT            NOT NULL,
    [CreatedBy]      INT            NULL,
    [CreationDate]   DATETIME       NULL,
    [LastUpdatedBy]  INT            NULL,
    [LastUpdateDate] DATETIME       NULL,
    CONSTRAINT [PK_MEAL] PRIMARY KEY CLUSTERED ([MealID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'套餐', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'MealID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'ShopID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价格', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'套餐名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'MealName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'Menu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建议', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'Suggestion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配图', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'ImageURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meal', @level2type = N'COLUMN', @level2name = N'OriginalPrice';

