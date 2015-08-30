CREATE TABLE [dbo].[ShopRevealImage] (
    [id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [shopId]          INT           NULL,
    [revealImageName] NVARCHAR (50) NULL,
    [status]          INT           NULL,
    CONSTRAINT [PK_ShopRevealImage] PRIMARY KEY CLUSTERED ([id] ASC)
);

