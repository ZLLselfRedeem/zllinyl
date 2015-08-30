CREATE TABLE [dbo].[ShopReport] (
    [ShopReportId] INT      IDENTITY (1, 1) NOT NULL,
    [CustomId]     BIGINT   NULL,
    [ReportTime]   DATETIME NOT NULL,
    [ShopId]       INT      NOT NULL,
    [ReportValue]  INT      NOT NULL,
    CONSTRAINT [PK_SHOPREPORT] PRIMARY KEY CLUSTERED ([ShopReportId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺举报', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺举报', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopReport', @level2type = N'COLUMN', @level2name = N'ShopReportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopReport', @level2type = N'COLUMN', @level2name = N'CustomId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'举报时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopReport', @level2type = N'COLUMN', @level2name = N'ReportTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopReport', @level2type = N'COLUMN', @level2name = N'ShopId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'举报值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ShopReport', @level2type = N'COLUMN', @level2name = N'ReportValue';

