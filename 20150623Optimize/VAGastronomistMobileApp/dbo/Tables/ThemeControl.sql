CREATE TABLE [dbo].[ThemeControl] (
    [TControlID]          INT          IDENTITY (1, 1) NOT NULL,
    [TPageID]             INT          NULL,
    [TControlDisplayName] VARCHAR (50) NULL,
    [TControlName]        VARCHAR (50) NULL,
    [TControlType]        VARCHAR (50) NULL,
    [TParentControlID]    INT          NULL,
    CONSTRAINT [PK_ThemeControl] PRIMARY KEY CLUSTERED ([TControlID] ASC)
);

