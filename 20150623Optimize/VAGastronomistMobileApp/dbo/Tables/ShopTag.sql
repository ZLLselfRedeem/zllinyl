CREATE TABLE [dbo].[ShopTag] (
    [TagNode]   [sys].[hierarchyid] NOT NULL,
    [TagLevel]  AS                  ([TagNode].[GetLevel]()),
    [TagId]     INT                 IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50)       NOT NULL,
    [Enable]    BIT                 CONSTRAINT [DF_ShopTag_Enable] DEFAULT ((1)) NOT NULL,
    [ShopCount] INT                 CONSTRAINT [DF_ShopTag_ShopCount] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([TagNode] ASC)
);

