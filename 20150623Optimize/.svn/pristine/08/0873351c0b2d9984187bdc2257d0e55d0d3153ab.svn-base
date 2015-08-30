CREATE TABLE [dbo].[DishI18n] (
    [DishI18nID]     INT            IDENTITY (1, 1) NOT NULL,
    [DishID]         INT            NULL,
    [LangID]         INT            NULL,
    [DishName]       NVARCHAR (500) NULL,
    [DishDescShort]  NVARCHAR (500) NULL,
    [DishDescDetail] NVARCHAR (MAX) NULL,
    [DishHistory]    NVARCHAR (MAX) NULL,
    [DishI18nStatus] INT            NULL,
    [dishQuanPin]    NVARCHAR (500) NULL,
    [dishJianPin]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_DishI18n] PRIMARY KEY CLUSTERED ([DishI18nID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [dishId]
    ON [dbo].[DishI18n]([DishID] ASC);

