CREATE TABLE [dbo].[FoodDiariesShareConfig] (
    [id]                   INT            IDENTITY (1, 1) NOT NULL,
    [foodDiariesShareInfo] NVARCHAR (500) NULL,
    [type]                 TINYINT        NULL,
    [status]               INT            NULL,
    CONSTRAINT [PK_FoodDiariesShareConfig] PRIMARY KEY CLUSTERED ([id] ASC)
);

