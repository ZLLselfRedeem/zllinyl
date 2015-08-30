CREATE TABLE [dbo].[PreOrder19dianExtend] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [preOrder19DianId] BIGINT         NULL,
    [orderInJson]      NVARCHAR (MAX) NULL,
    [sundryJson]       NVARCHAR (MAX) NULL,
    [ExtendPay] FLOAT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_PreOrder19dianExtend] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [preOrder19dianId]
    ON [dbo].[PreOrder19dianExtend]([preOrder19DianId] ASC);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'额外支付金额',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'PreOrder19dianExtend',
    @level2type = N'COLUMN',
    @level2name = N'ExtendPay'