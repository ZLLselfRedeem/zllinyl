CREATE TABLE [dbo].[OrderDetailInfo] (
    [OrderDetailID]        INT            IDENTITY (1, 1) NOT NULL,
    [OrderID]              INT            NULL,
    [DishID]               INT            NULL,
    [DishPriceI18nID]      INT            NULL,
    [DishPrice]            FLOAT (53)     NULL,
    [DishPriceOriginal]    FLOAT (53)     NULL,
    [DishPriceSum]         FLOAT (53)     NULL,
    [DishQuantity]         FLOAT (53)     NULL,
    [PrintQuantity]        FLOAT (53)     NULL,
    [TablePrintQuantity]   FLOAT (53)     NULL,
    [RetreatPrintQuantity] FLOAT (53)     NULL,
    [DishName]             NVARCHAR (500) NULL,
    [ScaleName]            NVARCHAR (50)  NULL,
    [OrderDetailStatus]    INT            NULL,
    [OrderDetailNote]      NVARCHAR (500) NULL,
    [Weighed]              BIT            NULL,
    [DishNeedWeigh]        BIT            NULL,
    [orderDetailOtherNote] NVARCHAR (500) NULL,
    [discountTypeId]       INT            NULL,
    [retreatOrderDetailId] INT            NULL,
    CONSTRAINT [PK_OrderDetailInfo] PRIMARY KEY CLUSTERED ([OrderDetailID] ASC)
);

