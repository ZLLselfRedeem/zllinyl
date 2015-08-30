CREATE TABLE [dbo].[Task] (
    [Id]           BIGINT   IDENTITY (1, 1) NOT NULL,
    [Status]       TINYINT  CONSTRAINT [DF_Task_Status] DEFAULT ((0)) NOT NULL,
    [CreateTime]   DATETIME CONSTRAINT [DF_Task_CreateTime] DEFAULT (getdate()) NOT NULL,
    [BeginTime]    DATETIME NULL,
    [EndTime]      DATETIME NULL,
    [FailureCount] INT      CONSTRAINT [DF_Task_FailureCount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC)
);

