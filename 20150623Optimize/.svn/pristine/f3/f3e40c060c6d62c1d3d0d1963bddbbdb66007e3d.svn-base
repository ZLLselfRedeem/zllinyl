CREATE TABLE [dbo].[FoodDiaryDefaultConfigDish] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [DishName]  NVARCHAR (50) NOT NULL,
    [ImageName] VARCHAR (256) NOT NULL,
    [Status]    BIT           CONSTRAINT [DF_FoodDiaryDefaultConfigDish_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_FoodDiaryDefaultConfigDish] PRIMARY KEY CLUSTERED ([Id] ASC)
);

