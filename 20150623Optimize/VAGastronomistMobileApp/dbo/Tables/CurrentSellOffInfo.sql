CREATE TABLE [dbo].[CurrentSellOffInfo] (
    [id]                BIGINT   IDENTITY (1, 1) NOT NULL,
    [companyId]         INT      NULL,
    [shopId]            INT      NULL,
    [menuId]            INT      NULL,
    [DishI18nID]        INT      NULL,
    [status]            INT      NULL,
    [DishPriceI18nID]   INT      NULL,
    [expirationTime]    DATETIME NULL,
    [operateTime]       DATETIME CONSTRAINT [DF_CurrentSellOffInfo_operateTime] DEFAULT (getdate()) NOT NULL,
    [operateEmployeeId] INT      NULL,
    CONSTRAINT [PK_CurrentSellOffInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

