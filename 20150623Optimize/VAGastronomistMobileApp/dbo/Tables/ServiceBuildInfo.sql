CREATE TABLE [dbo].[ServiceBuildInfo] (
    [id]                      INT            IDENTITY (1, 1) NOT NULL,
    [latestBuild]             NVARCHAR (50)  NULL,
    [latestUpdateDescription] NVARCHAR (100) NULL,
    [latestUpdateUrl]         NVARCHAR (500) NULL,
    [type]                    TINYINT        NULL,
    [oldBuildSupport]         NVARCHAR (50)  NULL,
    [updateTime]              DATETIME       NULL,
    CONSTRAINT [PK_ServiceBuildInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

