CREATE TABLE [dbo].[ShopVIP] (
    [Id]                    BIGINT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]            BIGINT          NOT NULL,
    [ShopId]                INT             NOT NULL,
    [CityId]                INT             NOT NULL,
    [PreOrderTotalQuantity] INT             CONSTRAINT [DF_Table_1_ConsumptionCount] DEFAULT ((0)) NOT NULL,
    [PreOrderTotalAmount]   DECIMAL (18, 2) CONSTRAINT [DF_ShopVIP_PreOrderTotalAmount] DEFAULT ((0)) NOT NULL,
    [CreateTime]            DATETIME        CONSTRAINT [DF_ShopVIP_CreateTime] DEFAULT (getdate()) NOT NULL,
    [LastPreOrderTime]      DATETIME        CONSTRAINT [DF_ShopVIP_LastPreOrderTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ShopVIP] PRIMARY KEY CLUSTERED ([Id] ASC)
);

