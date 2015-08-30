CREATE TABLE [dbo].[FoodPlaza] (
    [foodPlazaId]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [preOrder19DianId]        BIGINT         NULL,
    [cityId]                  INT            NULL,
    [shopId]                  INT            NULL,
    [shopName]                NVARCHAR (500) NULL,
    [status]                  BIT            NULL,
    [latestUpdateTime]        DATETIME       NULL,
    [isListTop]               BIT            NULL,
    [dishIds]                 NVARCHAR (100) NULL,
    [customerId]              BIGINT         NULL,
    [latestOperateEmployeeId] INT            NULL,
    [orderAmount]             FLOAT (53)     NULL,
    CONSTRAINT [PK_FoodPlaza] PRIMARY KEY CLUSTERED ([foodPlazaId] ASC)
);

