CREATE TABLE [dbo].[ShopVIPSpeedNextTime] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [City]     INT      NOT NULL,
    [NextTime] DATETIME NULL,
    CONSTRAINT [PK_ShopVIPSpeedNextTime] PRIMARY KEY CLUSTERED ([Id] ASC)
);

