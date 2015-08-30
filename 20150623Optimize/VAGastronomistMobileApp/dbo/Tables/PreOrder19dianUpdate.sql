CREATE TABLE [dbo].[PreOrder19dianUpdate] (
    [changePreOrder19dianId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [preOrder19dianId]       BIGINT         NOT NULL,
    [changeOrderInJson]      NVARCHAR (MAX) NULL,
    [changeSundryJson]       NVARCHAR (MAX) NULL,
    [preOrderSum]            FLOAT (53)     NULL,
    [status]                 INT            NULL,
    CONSTRAINT [PK_PreOrder19dianUpdate] PRIMARY KEY CLUSTERED ([changePreOrder19dianId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'已支付单子修改主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreOrder19dianUpdate', @level2type = N'COLUMN', @level2name = N'changePreOrder19dianId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'预点单ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreOrder19dianUpdate', @level2type = N'COLUMN', @level2name = N'preOrder19dianId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改后的点单json', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreOrder19dianUpdate', @level2type = N'COLUMN', @level2name = N'changeOrderInJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改后的杂项json', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreOrder19dianUpdate', @level2type = N'COLUMN', @level2name = N'changeSundryJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总价', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PreOrder19dianUpdate', @level2type = N'COLUMN', @level2name = N'preOrderSum';

