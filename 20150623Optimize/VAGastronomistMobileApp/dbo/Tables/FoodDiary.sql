CREATE TABLE [dbo].[FoodDiary] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [OrderId]        BIGINT         NOT NULL,
    [Name]           NVARCHAR (50)  NULL,
    [Content]        NVARCHAR (MAX) NULL,
    [Weather]        NVARCHAR (50)  NULL,
    [ShopName]       NVARCHAR (500) NOT NULL,
    [ShoppingDate]   SMALLDATETIME  NOT NULL,
    [Shared]         TINYINT        CONSTRAINT [DF_FoodDiary_Shared] DEFAULT ((0)) NOT NULL,
    [CreateTime]     DATETIME       CONSTRAINT [DF_FoodDiary_CreateTime] DEFAULT (getdate()) NOT NULL,
    [Hit]            INT            DEFAULT ((0)) NULL,
    [IsBig]          BIT            DEFAULT ((0)) NULL,
    [IsHideDishName] BIT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_FoodDiary] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ix_foodDiary_orderId]
    ON [dbo].[FoodDiary]([OrderId] ASC);

