CREATE TABLE [dbo].[FinancialType] (
    [financialTypeID]       INT           IDENTITY (1, 1) NOT NULL,
    [financialTypeName]     NVARCHAR (50) NULL,
    [financialTypeStatus]   INT           NULL,
    [financialTypeSequence] INT           NULL,
    CONSTRAINT [PK_FinancialType] PRIMARY KEY CLUSTERED ([financialTypeID] ASC)
);

