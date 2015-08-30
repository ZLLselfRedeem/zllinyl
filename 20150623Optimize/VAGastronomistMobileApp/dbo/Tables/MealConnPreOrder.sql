CREATE TABLE [dbo].[MealConnPreOrder] (
    [id]               INT    IDENTITY (1, 1) NOT NULL,
    [preOrder19dianId] BIGINT NOT NULL,
    [MealScheduleID]   INT    NULL,
    CONSTRAINT [PK_MealConnPreOrder] PRIMARY KEY CLUSTERED ([id] ASC)
);

