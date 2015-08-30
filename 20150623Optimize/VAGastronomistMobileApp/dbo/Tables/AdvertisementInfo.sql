CREATE TABLE [dbo].[AdvertisementInfo] (
    [id]                       BIGINT         IDENTITY (1, 1) NOT NULL,
    [name]                     NVARCHAR (500) NULL,
    [imageURL]                 NVARCHAR (500) NULL,
    [status]                   TINYINT        NULL,
    [advertisementType]        TINYINT        NULL,
    [value]                    NVARCHAR (500) NULL,
    [advertisementDescription] NVARCHAR (300) NULL,
    [webAdvertisementUrl]      NVARCHAR (300) NULL,
    [advertisementClassify]    TINYINT        NULL,
    CONSTRAINT [PK_AdvertisementInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

