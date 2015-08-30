CREATE TABLE [dbo].[InventoryType] (
    [InventoryTypeID]     INT           IDENTITY (1, 1) NOT NULL,
    [InventoryTypeName]   NVARCHAR (50) NULL,
    [InventoryTypeStatus] INT           NULL,
    CONSTRAINT [PK_InventoryType] PRIMARY KEY CLUSTERED ([InventoryTypeID] ASC)
);

