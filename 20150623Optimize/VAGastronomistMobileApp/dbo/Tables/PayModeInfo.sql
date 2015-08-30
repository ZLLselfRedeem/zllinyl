CREATE TABLE [dbo].[PayModeInfo] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [payModeName]  NVARCHAR (50) NULL,
    [payModeValue] INT           NULL,
    [status]       INT           NULL,
    CONSTRAINT [PK_PayModeInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

