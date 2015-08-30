CREATE TABLE [dbo].[Preorder19DianLine]
(
	[Preorder19DianLineId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Preorder19DianId] BIGINT NOT NULL, 
    [CustomerId] BIGINT NOT NULL, 
    [PayType] INT NOT NULL, 
    [PayAccount] NCHAR(200) NOT NULL,
    [Amount] FLOAT NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [Remark] NCHAR(400) NULL, 
    [State] INT NOT NULL, 
    [Uuid] NCHAR(200) NOT NULL , 
    [RefundAmount] FLOAT NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'点单编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'Preorder19DianId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'CustomerId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'支付方式',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'PayType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'支付金额',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'Amount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备注',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'Remark'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Preorder19DianLineId',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'Preorder19DianLineId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'支付帐号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'PayAccount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'设备编号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = N'Uuid'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退款金额',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Preorder19DianLine',
    @level2type = N'COLUMN',
    @level2name = 'RefundAmount'
GO

CREATE INDEX [IX_Preorder19DianLine] ON [dbo].[Preorder19DianLine] ([Uuid], [CustomerId], [CreateTime], [PayAccount])
