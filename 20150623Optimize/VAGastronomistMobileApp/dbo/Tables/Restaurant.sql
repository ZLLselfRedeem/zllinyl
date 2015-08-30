CREATE TABLE [dbo].[Restaurant] (
    [RestID]          INT            NOT NULL,
    [RestName]        NVARCHAR (500) NULL,
    [RestForeignName] NVARCHAR (500) NULL,
    [RestNameShort]   NVARCHAR (500) NULL,
    [RestProvince]    NVARCHAR (50)  NULL,
    [RestCity]        NVARCHAR (50)  NULL,
    [RestAddress]     NVARCHAR (500) NULL,
    [RestBoss]        NVARCHAR (50)  NULL,
    [RestBossTel]     NVARCHAR (50)  NULL,
    [ResConnector]    NVARCHAR (50)  NULL,
    [ResConnectorTel] NVARCHAR (50)  NULL,
    [RestPublicTel]   NVARCHAR (50)  NULL,
    [RestReserveTel]  NVARCHAR (50)  NULL,
    [RestFax]         NVARCHAR (50)  NULL,
    [RestLogTiny]     NVARCHAR (500) NULL,
    [RestLogMid]      NVARCHAR (500) NULL,
    [RestLogLarge]    NVARCHAR (500) NULL,
    [RestPic]         NVARCHAR (500) NULL,
    [RestDesc]        NVARCHAR (MAX) NULL,
    [RestHistory]     NVARCHAR (MAX) NULL
);

