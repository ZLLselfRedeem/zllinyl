CREATE TABLE [dbo].[VersionInfo] (
    [POSLiteVersion]           NVARCHAR (50)  NULL,
    [SoftwareVersion]          NVARCHAR (50)  NULL,
    [UpdateTime]               DATETIME       NULL,
    [downloadURL]              NVARCHAR (500) NULL,
    [POSLiteUpdatePackageName] NVARCHAR (50)  NULL,
    [type]                     INT            NULL,
    [typename]                 VARCHAR (50)   NULL
);

