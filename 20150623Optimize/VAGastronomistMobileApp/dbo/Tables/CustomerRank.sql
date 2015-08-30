CREATE TABLE [dbo].[CustomerRank] (
    [CustomerRankID]       INT           IDENTITY (1, 1) NOT NULL,
    [CustomerRankName]     NVARCHAR (50) NULL,
    [CustomerRankSequence] INT           NULL,
    [CustomerRankStatus]   INT           NULL,
    CONSTRAINT [PK_CustomerRank] PRIMARY KEY CLUSTERED ([CustomerRankID] ASC)
);

