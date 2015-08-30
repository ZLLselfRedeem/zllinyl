CREATE TABLE [dbo].[ImageInfo] (
    [ImageID]       INT           IDENTITY (1, 1) NOT NULL,
    [DishID]        INT           NULL,
    [ImageName]     NVARCHAR (50) NULL,
    [ImageSequence] INT           NULL,
    [ImageScale]    INT           NULL,
    [ImageStatus]   INT           NULL,
    [ImageXY]       VARCHAR (100) NULL,
    CONSTRAINT [PK_ImageInfo] PRIMARY KEY CLUSTERED ([ImageID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_imageInfo_dishId]
    ON [dbo].[ImageInfo]([DishID] ASC);

