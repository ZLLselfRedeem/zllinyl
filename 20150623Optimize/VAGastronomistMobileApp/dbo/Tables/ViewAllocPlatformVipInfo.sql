CREATE TABLE [dbo].[ViewAllocPlatformVipInfo] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [name]             NVARCHAR (20)  NULL,
    [isMonetary]       BIT            NULL,
    [consumptionLevel] FLOAT (53)     NULL,
    [status]           INT            NULL,
    [vipImg]           NVARCHAR (100) NULL,
    CONSTRAINT [PK_ViewAllocPlatformVipInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

