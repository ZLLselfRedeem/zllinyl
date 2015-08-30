CREATE TABLE [dbo].[CustomerForgetPassword] (
    [id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [customerId]        BIGINT         NULL,
    [sendEmailTime]     DATETIME       NULL,
    [resetPasswordTime] DATETIME       NULL,
    [status]            INT            NULL,
    [verifyCode]        NVARCHAR (500) NULL,
    CONSTRAINT [PK_CustomerForgetPassword] PRIMARY KEY CLUSTERED ([id] ASC)
);

