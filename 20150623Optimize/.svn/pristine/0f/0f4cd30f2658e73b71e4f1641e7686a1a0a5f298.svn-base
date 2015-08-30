CREATE TABLE [dbo].[FoodDiaryDish] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FoodDiaryId] BIGINT         NOT NULL,
    [DishId]      INT            NOT NULL,
    [DishName]    NVARCHAR (500) NOT NULL,
    [ImagePath]   VARCHAR (500)  NOT NULL,
    [Source]      TINYINT        CONSTRAINT [DF_FoodDiaryDish_IsDefaultConfigDish] DEFAULT ((0)) NOT NULL,
    [Sort]        INT            CONSTRAINT [DF_FoodDiaryDish_Sort] DEFAULT ((0)) NOT NULL,
    [Status]      BIT            NOT NULL,
    CONSTRAINT [PK_FoodDiaryDish] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_foodDiaryDish_foodDiaryId]
    ON [dbo].[FoodDiaryDish]([FoodDiaryId] ASC);

