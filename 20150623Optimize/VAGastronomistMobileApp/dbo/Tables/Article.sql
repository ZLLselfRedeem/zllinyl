CREATE TABLE [dbo].[Article] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [City]       INT            CONSTRAINT [DF_Article_City] DEFAULT ((0)) NOT NULL,
    [Content]    NVARCHAR (MAX) NOT NULL,
    [CreateTime] DATETIME       CONSTRAINT [DF_Article_CreateTime] DEFAULT (getdate()) NOT NULL,
    [Enable]     BIT            CONSTRAINT [DF_Article_Enable] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([Id] ASC)
);

