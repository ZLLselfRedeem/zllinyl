CREATE TABLE [dbo].[PreorderEvaluation] (
    [PreorderEvaluationId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [PreOrder19dianId]     BIGINT         NOT NULL,
    [ShopId]               INT            NOT NULL,
    [CustomerId]           BIGINT         NOT NULL,
    [EvaluationValue]      SMALLINT       NOT NULL,
    [EvaluationContent]    NVARCHAR (400) NOT NULL,
    [EvaluationTime]       DATETIME       NOT NULL,
    [EvaluationLevel]      INT            NOT NULL,
    CONSTRAINT [PK_PreorderEvaluation] PRIMARY KEY CLUSTERED ([PreorderEvaluationId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点单评价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PreorderEvaluationId', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'PreorderEvaluationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点单编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'PreOrder19dianId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'ShopId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'EvaluationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'EvaluationContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'EvaluationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价等级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreorderEvaluation', @level2type = N'COLUMN', @level2name = N'EvaluationLevel';

