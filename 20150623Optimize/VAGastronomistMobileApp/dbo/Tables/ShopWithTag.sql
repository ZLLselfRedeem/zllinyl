CREATE TABLE [dbo].[ShopWithTag] (
    [Id]      INT                 IDENTITY (1, 1) NOT NULL,
    [TagNode] [sys].[hierarchyid] NOT NULL,
    [ShopId]  INT                 NOT NULL,
    CONSTRAINT [PK_ShopWithTag] PRIMARY KEY CLUSTERED ([Id] ASC)
);

