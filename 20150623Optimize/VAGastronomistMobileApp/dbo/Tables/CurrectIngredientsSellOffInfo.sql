CREATE TABLE [dbo].[CurrectIngredientsSellOffInfo] (
    [Id]                BIGINT   IDENTITY (1, 1) NOT NULL,
    [ingredientsId]     INT      NULL,
    [shopId]            INT      NULL,
    [companyId]         INT      NULL,
    [status]            BIT      NULL,
    [expirationTime]    DATETIME NULL,
    [operateTime]       DATETIME CONSTRAINT [DF_CurrectIngredientsSellOffInfo_operateTime] DEFAULT (getdate()) NOT NULL,
    [operateEmployeeId] INT      NULL,
    CONSTRAINT [PK_CurrectIngredientsSellOffInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
);

