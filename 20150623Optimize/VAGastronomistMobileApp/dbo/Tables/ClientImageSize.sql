CREATE TABLE [dbo].[ClientImageSize] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [appType]     INT            NULL,
    [screenWidth] NVARCHAR (50)  NULL,
    [imageType]   INT            NULL,
    [value]       NVARCHAR (200) NULL,
    [status]      INT            NULL,
    CONSTRAINT [PK_ClientImageSize] PRIMARY KEY CLUSTERED ([id] ASC)
);

