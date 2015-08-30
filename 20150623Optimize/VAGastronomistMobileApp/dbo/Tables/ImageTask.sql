CREATE TABLE [dbo].[ImageTask] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (150) NOT NULL,
    [FileName]        VARCHAR (150) NOT NULL,
    [Extension]       VARCHAR (50)  NOT NULL,
    [Path]            VARCHAR (256) NOT NULL,
    [EqualProportion] BIT           NULL,
    [UserId]          INT           NULL,
    [ShopId]          INT           NULL,
    [MenuId]          INT           NULL,
    [CreateTime]      DATETIME      CONSTRAINT [DF_ImageTask_CreateTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ImageTask] PRIMARY KEY CLUSTERED ([Id] ASC)
);

