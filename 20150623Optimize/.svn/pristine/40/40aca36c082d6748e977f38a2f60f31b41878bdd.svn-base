CREATE TABLE [dbo].[DishTaste] (
    [tasteId]       INT            IDENTITY (1, 1) NOT NULL,
    [menuId]        INT            NOT NULL,
    [tasteName]     NVARCHAR (50)  NOT NULL,
    [tasteRemark]   NVARCHAR (200) NULL,
    [tasteSequence] INT            NULL,
    [tasteStatus]   BIT            NOT NULL,
    CONSTRAINT [PK_DishTaste] PRIMARY KEY CLUSTERED ([tasteId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'口味配置表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜的口味Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste', @level2type = N'COLUMN', @level2name = N'tasteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜谱Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste', @level2type = N'COLUMN', @level2name = N'menuId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'口味名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste', @level2type = N'COLUMN', @level2name = N'tasteName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste', @level2type = N'COLUMN', @level2name = N'tasteRemark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste', @level2type = N'COLUMN', @level2name = N'tasteSequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DishTaste', @level2type = N'COLUMN', @level2name = N'tasteStatus';

