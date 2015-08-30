CREATE TABLE [dbo].[ApplyPaymentInfo] (
    [identity_Id]         BIGINT       IDENTITY (1, 1) NOT NULL,
    [companyId]           INT          NOT NULL,
    [shopId]              INT          NULL,
    [prePaidSum]          FLOAT (53)   NOT NULL,
    [viewallocCommission] FLOAT (53)   NOT NULL,
    [actualPaidSum]       FLOAT (53)   NOT NULL,
    [applyDate]           DATETIME     NULL,
    [appFromTime]         DATETIME     NULL,
    [appToTime]           DATETIME     NULL,
    [applyStatus]         TINYINT      NOT NULL,
    [checkPersonId]       INT          NULL,
    [accountNum]          VARCHAR (50) NULL,
    [remittanceNum]       VARCHAR (50) NULL,
    CONSTRAINT [PK_ApplyPaymentInfo] PRIMARY KEY CLUSTERED ([identity_Id] ASC)
);

