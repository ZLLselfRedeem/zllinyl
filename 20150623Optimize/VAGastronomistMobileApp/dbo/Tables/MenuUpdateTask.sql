CREATE TABLE [dbo].[MenuUpdateTask] (
    [Id]         BIGINT NOT NULL,
    [MenuId]     INT    NOT NULL,
    [EmployeeId] INT    NOT NULL,
    CONSTRAINT [PK_MenuUpdateTask] PRIMARY KEY CLUSTERED ([Id] ASC)
);

