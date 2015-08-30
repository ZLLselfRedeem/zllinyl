CREATE TABLE [dbo].[DishInfo] (
    [DishID]              INT           IDENTITY (1, 1) NOT NULL,
    [DiscountTypeID]      INT           NULL,
    [MenuID]              INT           NULL,
    [DishDisplaySequence] INT           NULL,
    [SendToKitchen]       BIT           NULL,
    [IsActive]            BIT           NULL,
    [DishTotalQuantity]   FLOAT (53)    NULL,
    [DishStatus]          INT           NULL,
    [InterfaceID]         CHAR (10)     NULL,
    [cookPrinterName]     NVARCHAR (50) NULL,
    [cookOrderCopy]       INT           NULL,
    [dishSalesIn19dian]   BIGINT        NULL,
    [dishPraiseNum]       INT           CONSTRAINT [DF_DishInfo_dishPraiseNum] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DishInfo] PRIMARY KEY CLUSTERED ([DishID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [menuId]
    ON [dbo].[DishInfo]([MenuID] ASC);

