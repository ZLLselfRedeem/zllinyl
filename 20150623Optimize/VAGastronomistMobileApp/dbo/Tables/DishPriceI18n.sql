CREATE TABLE [dbo].[DishPriceI18n] (
    [DishPriceI18nID]     INT           NOT NULL,
    [DishPriceID]         INT           NULL,
    [LangID]              INT           NULL,
    [ScaleName]           NVARCHAR (50) NULL,
    [DishPriceI18nStatus] INT           NULL,
    [markName]            NVARCHAR (50) NULL,
    CONSTRAINT [PK_DishPriceI18n] PRIMARY KEY CLUSTERED ([DishPriceI18nID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [dishPriceId]
    ON [dbo].[DishPriceI18n]([DishPriceID] ASC);

