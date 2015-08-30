CREATE TABLE [dbo].[AdvertisementConnAdColumn] (
    [id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [name]                  NVARCHAR (500) NULL,
    [advertisementColumnId] INT            NULL,
    [advertisementId]       BIGINT         NULL,
    [displayStartTime]      DATETIME       NULL,
    [displayEndTime]        DATETIME       NULL,
    [displayCount]          BIGINT         NULL,
    [clickCount]            BIGINT         NULL,
    [status]                TINYINT        NULL,
    [cityId]                INT            NULL,
    CONSTRAINT [PK_AdvertisementConnAdColumn] PRIMARY KEY CLUSTERED ([id] ASC)
);

