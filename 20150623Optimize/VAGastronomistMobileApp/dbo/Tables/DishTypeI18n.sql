CREATE TABLE [dbo].[DishTypeI18n] (
    [DishTypeI18nID]     INT           IDENTITY (1, 1) NOT NULL,
    [LangID]             INT           NULL,
    [DishTypeID]         INT           NULL,
    [DishTypeName]       NVARCHAR (50) NULL,
    [DishTypeI18nStatus] INT           NULL,
    CONSTRAINT [PK_DishTypeI18n] PRIMARY KEY CLUSTERED ([DishTypeI18nID] ASC)
);

