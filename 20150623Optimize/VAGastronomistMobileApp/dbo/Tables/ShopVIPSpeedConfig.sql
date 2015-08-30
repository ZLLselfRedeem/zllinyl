CREATE TABLE [dbo].[ShopVIPSpeedConfig] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [City]      INT      NOT NULL,
    [StartHour] SMALLINT NOT NULL,
    [EndHour]   SMALLINT NOT NULL,
    [PreUnit]   INT      NOT NULL,
    [Unit]      SMALLINT NOT NULL,
    [MinSpeed]  INT      NOT NULL,
    [MaxSpeed]  INT      NOT NULL,
    CONSTRAINT [PK_ShopVIPSpeedConfig] PRIMARY KEY CLUSTERED ([Id] ASC)
);

