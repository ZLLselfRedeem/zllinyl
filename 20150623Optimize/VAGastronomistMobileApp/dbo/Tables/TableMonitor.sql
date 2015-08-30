CREATE TABLE [dbo].[TableMonitor] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [ChangeTag]       BIT          NULL,
    [ChangeTime]      DATETIME     NULL,
    [ChangeMethod]    VARCHAR (50) NULL,
    [ChangeTableName] VARCHAR (50) NULL,
    [Session]         VARCHAR (50) NULL,
    CONSTRAINT [PK_TableMonitor] PRIMARY KEY CLUSTERED ([id] ASC)
);

