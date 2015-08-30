CREATE TABLE [dbo].[UnitInfo] (
    [unitID]     INT          IDENTITY (1, 1) NOT NULL,
    [unitName]   VARCHAR (50) NULL,
    [unitStatus] INT          NULL,
    CONSTRAINT [PK_UnitInfo] PRIMARY KEY CLUSTERED ([unitID] ASC)
);

