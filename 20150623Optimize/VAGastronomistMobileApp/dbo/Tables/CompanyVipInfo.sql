CREATE TABLE [dbo].[CompanyVipInfo] (
    [id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]            NVARCHAR (50) NULL,
    [companyId]       INT           NULL,
    [nextRequirement] INT           NULL,
    [sequence]        INT           NULL,
    [discount]        FLOAT (53)    NULL,
    [status]          INT           NULL,
    CONSTRAINT [PK_CompanyVipInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

