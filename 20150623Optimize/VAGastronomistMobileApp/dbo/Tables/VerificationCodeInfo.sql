CREATE TABLE [dbo].[VerificationCodeInfo] (
    [verificationCodeId] INT           IDENTITY (1, 1) NOT NULL,
    [verificationCode]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_VerificationCodeInfo] PRIMARY KEY CLUSTERED ([verificationCodeId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [verificationCode]
    ON [dbo].[VerificationCodeInfo]([verificationCode] ASC);

