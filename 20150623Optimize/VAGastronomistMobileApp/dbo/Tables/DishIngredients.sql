CREATE TABLE [dbo].[DishIngredients] (
    [ingredientsId]       INT            IDENTITY (1, 1) NOT NULL,
    [menuId]              INT            NOT NULL,
    [ingredientsName]     NVARCHAR (50)  NOT NULL,
    [ingredientsPrice]    FLOAT (53)     NOT NULL,
    [vipDiscountable]     BIT            NOT NULL,
    [backDiscountable]    BIT            NOT NULL,
    [ingredientsRemark]   NVARCHAR (200) NULL,
    [ingredientsSequence] INT            NULL,
    [ingredientsStatus]   BIT            NOT NULL,
    CONSTRAINT [PK_DishIngredients] PRIMARY KEY CLUSTERED ([ingredientsId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配料表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜的配料Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'menuId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配料名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配料价格', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否能享受Vip折扣', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'vipDiscountable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否支持返送', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'backDiscountable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsRemark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsSequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsStatus';

