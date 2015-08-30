CREATE TABLE [dbo].[ClientStartImgConfig] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ImgUrl]     NVARCHAR (500) NULL,
    [Type]       INT            NULL,
    [Status]     INT            CONSTRAINT [DF_ClinetStartImgConfig_Status] DEFAULT ((0)) NOT NULL,
    [CreateDate] DATETIME       NULL,
    [ScaleType]  INT            NULL,
    [Sequence]   INT            CONSTRAINT [DF_ClinetStartImgConfig_Sequence] DEFAULT ((0)) NOT NULL,
    [AppType]    INT            NULL,
    CONSTRAINT [PK_ClinetStartImgConfig] PRIMARY KEY CLUSTERED ([Id] ASC)
);

