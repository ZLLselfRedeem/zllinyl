CREATE TABLE [dbo].[CustomerFavoriteCompany] (
    [id]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [customerId]  BIGINT   NULL,
    [companyId]   INT      NULL,
    [collectTime] DATETIME NULL,
    [shopId]      INT      NULL,
    CONSTRAINT [PK_CustomerFavoriteCompany] PRIMARY KEY CLUSTERED ([id] ASC)
);

