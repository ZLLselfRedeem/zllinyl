CREATE TABLE [dbo].[RedEnvelopeDetails] (
    [Id]                        BIGINT         IDENTITY (1, 1) NOT NULL,
    [mobilePhoneNumber]         NVARCHAR (50)  NULL,
    [treasureChestId]           BIGINT         NULL,
    [redEnvelopeId]             BIGINT         NULL,
    [redEnvelopeAmount]         FLOAT (53)     NULL,
    [redEnvelopeExpirationTime] DATETIME       NULL,
    [operationTime]             DATETIME       NULL,
    [stateType]                 INT            NULL,
    [usedAmount]                FLOAT (53)     NULL,
    [cookie]                    NVARCHAR (100) NULL,
    [preOrder19dianId]          BIGINT         NULL,
    CONSTRAINT [PK_RedEnvelopeDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RedEnvelopeDetails_unique]
    ON [dbo].[RedEnvelopeDetails]([mobilePhoneNumber] ASC, [redEnvelopeId] ASC, [stateType] ASC) WHERE ([stateType]=(0));

