CREATE TABLE [dbo].[PrePayPrivilegeConnCoupon] (
    [id]                BIGINT     IDENTITY (1, 1) NOT NULL,
    [prePayPrivilegeId] INT        NULL,
    [prePayCashMax]     FLOAT (53) NULL,
    [prePayCashMin]     FLOAT (53) NULL,
    [couponId]          INT        NULL,
    [returnCouponRule]  TINYINT    NULL,
    CONSTRAINT [PK_PrePayPrivilegeConnCoupon] PRIMARY KEY CLUSTERED ([id] ASC)
);

