CREATE TABLE [dbo].[DiscountInfo] (
    [discountID]       INT          IDENTITY (1, 1) NOT NULL,
    [discountName]     VARCHAR (50) NULL,
    [discountValue]    FLOAT (53)   NULL,
    [discountStatus]   INT          NULL,
    [discountSequence] INT          NULL,
    CONSTRAINT [PK_DiscountInfo] PRIMARY KEY CLUSTERED ([discountID] ASC)
);

