CREATE TABLE [dbo].[RedEnvelopeConnPreOrder] (
    [Id]                BIGINT     IDENTITY (1, 1) NOT NULL,
    [preOrder19dianId]  BIGINT     NULL,
    [redEnvelopeId]     BIGINT     NULL,
    [currectUsedAmount] FLOAT (53) NULL,
    [currectUsedTime]   DATETIME   NULL,
    CONSTRAINT [PK_RedEnvelopeConnPreOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_redEnvelopeConnPreOrder_connId]
    ON [dbo].[RedEnvelopeConnPreOrder]([preOrder19dianId] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_redEnvelopeConnPreOrder_redEnvelopeId]
    ON [dbo].[RedEnvelopeConnPreOrder]([redEnvelopeId] ASC);

