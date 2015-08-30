CREATE TABLE [dbo].[CouponsReceiveActivities] (
    [id]                                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [couponsReceiveActivitiesName]       NVARCHAR (100) NULL,
    [companyId]                          INT            NULL,
    [shopId]                             INT            NULL,
    [couponId]                           INT            NULL,
    [activitiesValidStartTime]           DATETIME       NULL,
    [activitiesValidEndTime]             DATETIME       NULL,
    [couponsReceiveActivitiesDes]        NVARCHAR (300) NULL,
    [couponValidDayCount]                FLOAT (53)     NULL,
    [status]                             INT            NULL,
    [timeType]                           INT            NULL,
    [couponsReceiveActivitiesCreateTime] DATETIME       NULL,
    CONSTRAINT [PK_CouponsReceiveActivities] PRIMARY KEY CLUSTERED ([id] ASC)
);

