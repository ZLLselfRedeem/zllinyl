CREATE TABLE [dbo].[TreasureChest] (
    [treasureChestId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [treasureChestConfigId] INT            NULL,
    [activityId]            INT            NULL,
    [amount]                FLOAT (53)     NULL,
    [remainAmount]          FLOAT (53)     NULL,
    [count]                 INT            NULL,
    [lockCount]             INT            NULL,
    [mobilePhoneNumber]     NVARCHAR (20)  NULL,
    [cookie]                NVARCHAR (100) NULL,
    [createTime]            DATETIME       NULL,
    [url]                   NVARCHAR (100) NULL,
    [executedTime]          DATETIME       NULL,
    [isExpire]              BIT            NULL,
    [expireTime]            DATETIME       NULL,
    [status]                BIT            NULL,
    CONSTRAINT [PK_TreasureChest] PRIMARY KEY CLUSTERED ([treasureChestId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_activityId]
    ON [dbo].[TreasureChest]([activityId] ASC);

