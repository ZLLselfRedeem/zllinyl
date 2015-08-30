CREATE TABLE [dbo].[ClientOrderDetailConfig] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [Type]        INT            NULL,
    [Status]      INT            CONSTRAINT [DF_ClientOrderDetailConfig_Status] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ClientOrderDetailConfig] PRIMARY KEY CLUSTERED ([Id] ASC)
);

