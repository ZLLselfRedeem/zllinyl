CREATE TABLE [dbo].[DishTypeInfo] (
    [DishTypeID]       INT IDENTITY (1, 1) NOT NULL,
    [MenuID]           INT NULL,
    [DishTypeSequence] INT NULL,
    [DishTypeStatus]   INT NULL,
    CONSTRAINT [PK_DishTypeInfo] PRIMARY KEY CLUSTERED ([DishTypeID] ASC)
);

