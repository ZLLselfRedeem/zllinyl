CREATE TABLE [dbo].[PointManageLog] (
    [id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [pointLogId] BIGINT         NULL,
    [remark]     NVARCHAR (200) NULL,
    [createTime] DATETIME       NULL,
    [createdBy]  INT            NULL,
    [status]     INT            NULL,
    CONSTRAINT [PK_PointManageLog] PRIMARY KEY CLUSTERED ([id] ASC)
);

