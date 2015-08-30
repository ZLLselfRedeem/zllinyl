CREATE TABLE [dbo].[TreasureChestAccessInfo] (
    [id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [activityId]        INT            NULL,
    [sourceType]        NVARCHAR (50)  NULL,
    [appType]           INT            NULL,
    [mobilePhoneNumber] NVARCHAR (50)  NULL,
    [cookie]            NVARCHAR (100) NULL,
    [ip]                NVARCHAR (20)  NULL,
    [url]               NVARCHAR (100) NULL,
    [accessTime]        DATETIME       NULL,
    CONSTRAINT [PK_RedEnvelopeAccessInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

