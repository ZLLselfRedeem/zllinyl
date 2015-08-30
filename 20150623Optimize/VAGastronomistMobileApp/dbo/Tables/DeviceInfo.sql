CREATE TABLE [dbo].[DeviceInfo] (
    [deviceId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [uuid]       NVARCHAR (100) NULL,
    [pushToken]  NVARCHAR (100) NULL,
    [updateTime] DATETIME       NULL,
    [appType]    INT            NULL,
    [appBuild]   NVARCHAR (50)  NULL,
    [unlockTime] DATETIME       NULL,
    CONSTRAINT [PK_DeviceInfo] PRIMARY KEY CLUSTERED ([deviceId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_deviceInfo_uuid]
    ON [dbo].[DeviceInfo]([uuid] ASC);

