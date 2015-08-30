CREATE TABLE [dbo].[CustomerConnCoupon] (
    [customerConnCouponID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [customerID]           BIGINT        NULL,
    [couponID]             BIGINT        NULL,
    [verificationCode]     NVARCHAR (50) NULL,
    [status]               INT           NULL,
    [limitedSerial]        INT           NULL,
    [verifyRewardTime]     DATETIME      NULL,
    [downloadTime]         DATETIME      NULL,
    [couponVerifyReward]   FLOAT (53)    NULL,
    [couponDownloadPrice]  FLOAT (53)    NULL,
    [couponValidStartTime] DATETIME      NULL,
    [couponValidEndTime]   DATETIME      NULL,
    [useTime]              DATETIME      NULL,
    [Encouragetype]        INT           NULL,
    [EncourageID]          BIGINT        NULL,
    CONSTRAINT [PK_CustomerConnCoupon] PRIMARY KEY CLUSTERED ([customerConnCouponID] ASC)
);

