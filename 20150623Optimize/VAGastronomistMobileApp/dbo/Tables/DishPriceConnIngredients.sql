CREATE TABLE [dbo].[DishPriceConnIngredients] (
    [dishPriceId]   INT NOT NULL,
    [ingredientsId] INT NOT NULL,
    CONSTRAINT [PK_DishPriceConnIngredients] PRIMARY KEY CLUSTERED ([dishPriceId] ASC, [ingredientsId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规格对于配料表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishPriceConnIngredients';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规格Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishPriceConnIngredients', @level2type = N'COLUMN', @level2name = N'dishPriceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配料Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishPriceConnIngredients', @level2type = N'COLUMN', @level2name = N'ingredientsId';

