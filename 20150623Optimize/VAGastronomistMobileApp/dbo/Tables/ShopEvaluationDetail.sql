CREATE TABLE [dbo].[ShopEvaluationDetail] (
    [ShopEvaluationDetailId] INT IDENTITY (1, 1) NOT NULL,
    [EvaluationValue]        INT NOT NULL,
    [EvaluationCount]        INT NOT NULL,
    [ShopId]                 INT NOT NULL,
    CONSTRAINT [PK_SHOPEVALUATIONDETAIL] PRIMARY KEY CLUSTERED ([ShopEvaluationDetailId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_ShopEvaluationDetail_shopId]
    ON [dbo].[ShopEvaluationDetail]([ShopId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺评价详情', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopEvaluationDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺评价详情编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopEvaluationDetail', @level2type = N'COLUMN', @level2name = N'ShopEvaluationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopEvaluationDetail', @level2type = N'COLUMN', @level2name = N'EvaluationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价次数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopEvaluationDetail', @level2type = N'COLUMN', @level2name = N'EvaluationCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopEvaluationDetail', @level2type = N'COLUMN', @level2name = N'ShopId';

