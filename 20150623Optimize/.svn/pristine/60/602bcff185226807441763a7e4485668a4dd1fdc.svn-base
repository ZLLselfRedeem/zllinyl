CREATE TABLE [dbo].[Activity] (
    [activityId]                    INT             IDENTITY (1, 1) NOT NULL,
    [name]                          NVARCHAR (100)  NULL,
    [beginTime]                     DATETIME        NULL,
    [endTime]                       DATETIME        NULL,
    [enabled]                       BIT             NULL,
    [status]                        BIT             NULL,
    [createTime]                    DATETIME        NULL,
    [createdBy]                     INT             NULL,
    [updateTime]                    DATETIME        NULL,
    [updatedBy]                     INT             NULL,
    [expirationTimeRule]            INT             NULL,
    [ruleValue]                     INT             NULL,
    [activityRule]                  NVARCHAR (2000) NULL,
    [activityType]                  INT             NULL,
    [redEnvelopeEffectiveBeginTime] DATETIME        NULL,
    [redEnvelopeEffectiveEndTime]   DATETIME        NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED ([activityId] ASC)
);

