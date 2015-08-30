CREATE TABLE [dbo].[DefauleSundryInfo] (
    [sundryId]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [shopId]                INT            NULL,
    [sundryName]            NVARCHAR (20)  NULL,
    [sundryStandard]        NVARCHAR (20)  NULL,
    [sundryChargeMode]      TINYINT        NULL,
    [supportChangeQuantity] BIGINT         NULL,
    [price]                 FLOAT (53)     NULL,
    [status]                INT            NULL,
    [description]           NVARCHAR (300) NULL,
    CONSTRAINT [PK_DefauleSundryInfo] PRIMARY KEY CLUSTERED ([sundryId] ASC)
);

