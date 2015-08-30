CREATE TABLE [dbo].[MedalConnShopCompany] (
    [id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [medalType]        INT           NULL,
    [companyOrShopId]  BIGINT        NULL,
    [medalName]        VARCHAR (200) NULL,
    [medalDescription] VARCHAR (100) NULL,
    CONSTRAINT [PK_MedalConnShopCompany] PRIMARY KEY CLUSTERED ([id] ASC)
);

