CREATE TABLE [dbo].[ServiceChargeInfo] (
    [serviceChargeId]       INT          IDENTITY (1, 1) NOT NULL,
    [serviceChargeName]     VARCHAR (50) NULL,
    [serviceChargeValue]    FLOAT (53)   NULL,
    [serviceChargeStatus]   INT          NULL,
    [serviceChargeSequence] INT          NULL,
    CONSTRAINT [PK_ServiceChargeInfo] PRIMARY KEY CLUSTERED ([serviceChargeId] ASC)
);

