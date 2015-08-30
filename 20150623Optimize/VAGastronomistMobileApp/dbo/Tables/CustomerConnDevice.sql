CREATE TABLE [dbo].[CustomerConnDevice] (
    [customerDeviceId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [customerId]       BIGINT   NULL,
    [deviceId]         BIGINT   NULL,
    [updateTime]       DATETIME NULL,
    CONSTRAINT [PK_customerConnDevice] PRIMARY KEY CLUSTERED ([customerDeviceId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_customerConnDevice_customerId_deviceId]
    ON [dbo].[CustomerConnDevice]([customerId] ASC, [deviceId] ASC);

