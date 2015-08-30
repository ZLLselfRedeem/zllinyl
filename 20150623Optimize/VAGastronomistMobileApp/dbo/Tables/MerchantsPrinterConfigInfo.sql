CREATE TABLE [dbo].[MerchantsPrinterConfigInfo] (
    [id]                    INT           IDENTITY (1, 1) NOT NULL,
    [printerName]           NVARCHAR (50) NULL,
    [printIp]               NVARCHAR (50) NULL,
    [printInterface]        NVARCHAR (50) NULL,
    [printCopies]           INT           NULL,
    [serialPrintSecondTab]  INT           NULL,
    [serialPrintThirdTab]   INT           NULL,
    [serialPrintFourthTab]  INT           NULL,
    [isPrintPrice]          INT           NULL,
    [isPrintTotal]          INT           NULL,
    [serialPrintLeftBlank]  INT           NULL,
    [serialPrintPaperWidth] INT           NULL,
    [thirdFontSize]         INT           NULL,
    [secondFontSize]        INT           NULL,
    [serialPrintLineFeed]   INT           NULL,
    [fontSizeHeight]        INT           NULL,
    [status]                INT           NULL,
    [employeeId]            INT           NULL,
    [shopId]                INT           NULL,
    [isOpen]                BIT           NULL,
    CONSTRAINT [PK_MerchantsPrinterConfigInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

