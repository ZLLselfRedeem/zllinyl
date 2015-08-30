CREATE TABLE [dbo].[CustomerCouponPreOrderUpdate] (
    [id]                   BIGINT IDENTITY (1, 1) NOT NULL,
    [customerID]           BIGINT NULL,
    [customerConnCouponID] BIGINT NULL,
    [preOrderID]           BIGINT NULL,
    [status]               INT    NULL
);

