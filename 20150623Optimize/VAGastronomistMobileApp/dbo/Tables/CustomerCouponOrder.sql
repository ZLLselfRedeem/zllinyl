CREATE TABLE [dbo].[CustomerCouponOrder] (
    [id]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [createTime]             DATETIME       NULL,
    [payTime]                DATETIME       NULL,
    [status]                 INT            NULL,
    [customerCookie]         NVARCHAR (100) NULL,
    [customerUUID]           NVARCHAR (100) NULL,
    [couponId]               BIGINT         NULL,
    [quantity]               FLOAT (53)     NULL,
    [priceSum]               FLOAT (53)     NULL,
    [customerId]             BIGINT         NULL,
    [couponName]             NVARCHAR (500) NULL,
    [couponConnActivitiesId] BIGINT         NULL,
    CONSTRAINT [PK_CustomerCouponOrder] PRIMARY KEY CLUSTERED ([id] ASC)
);

