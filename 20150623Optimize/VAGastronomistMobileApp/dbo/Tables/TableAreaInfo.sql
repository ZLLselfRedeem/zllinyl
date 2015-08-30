CREATE TABLE [dbo].[TableAreaInfo] (
    [tableAreaID]       INT            IDENTITY (1, 1) NOT NULL,
    [tableAreaName]     NVARCHAR (100) NULL,
    [tableAreaSequence] INT            NULL,
    [tableAreaStatus]   INT            NULL,
    [interfacePccode]   CHAR (5)       NULL,
    [billPrinter]       NVARCHAR (50)  NULL,
    [huacaiPrinter]     NVARCHAR (50)  NULL,
    [tablePrinter]      NVARCHAR (50)  NULL,
    CONSTRAINT [PK_TableAreaInfo] PRIMARY KEY CLUSTERED ([tableAreaID] ASC)
);

