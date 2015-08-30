CREATE TABLE [dbo].[MedalImageInfo] (
    [id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [medalId]    BIGINT        NULL,
    [medalScale] INT           NULL,
    [medalURL]   VARCHAR (200) NULL,
    CONSTRAINT [PK_MedalImageInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

