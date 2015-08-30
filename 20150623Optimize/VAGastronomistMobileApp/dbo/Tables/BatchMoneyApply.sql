CREATE TABLE [dbo].[BatchMoneyApply] (
    [batchMoneyApplyId] INT            IDENTITY (1, 1) NOT NULL,
    [createdTime]       DATETIME       NULL,
    [advanceCount]      INT            NULL,
    [practicalCount]    INT            NULL,
    [advanceAmount]     FLOAT (53)     NULL,
    [practicalAmount]   FLOAT (53)     NULL,
    [operateEmployee]   INT            NULL,
    [status]            TINYINT        NULL,
    [remark]            NVARCHAR (500) NULL,
    [cityId]            INT            NULL,
    CONSTRAINT [PK_BatchMoneyApply] PRIMARY KEY CLUSTERED ([batchMoneyApplyId] ASC)
);

