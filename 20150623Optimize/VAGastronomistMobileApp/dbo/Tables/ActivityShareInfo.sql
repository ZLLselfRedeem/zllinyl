CREATE TABLE [dbo].[ActivityShareInfo] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [activityId] INT            NULL,
    [type]       INT            NULL,
    [remark]     NVARCHAR (500) NULL,
    [status]     BIT            NULL,
    CONSTRAINT [PK_ActivityShareInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

