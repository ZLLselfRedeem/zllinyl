CREATE TABLE [dbo].[ShopAuthority] (
    [shopAuthorityId]          INT            IDENTITY (1, 1) NOT NULL,
    [shopAuthorityName]        NVARCHAR (50)  NOT NULL,
    [shopAuthorityDescription] NVARCHAR (500) NULL,
    [shopAuthorityStatus]      INT            NOT NULL,
    [shopAuthorityType]        SMALLINT       NOT NULL,
    [ShopAuthoritySequence]    INT            NULL,
    [isClientShow]             BIT            NULL,
    [isSYBShow]                BIT            NULL,
    [authorityCode]            NVARCHAR (5)   NULL,
    [isViewAllocWorkerEnable]  BIT            NULL,
    CONSTRAINT [PK_ShopAuthority] PRIMARY KEY CLUSTERED ([shopAuthorityId] ASC)
);

