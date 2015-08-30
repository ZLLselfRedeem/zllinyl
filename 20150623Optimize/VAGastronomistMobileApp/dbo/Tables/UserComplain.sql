CREATE TABLE [dbo].[UserComplain] (
    [identity_Id]       BIGINT          NOT NULL,
    [complainType]      TINYINT         NOT NULL,
    [complainDate]      DATETIME        NULL,
    [companyId]         INT             NOT NULL,
    [correspondId]      BIGINT          NOT NULL,
    [verificationCode]  VARCHAR (50)    NULL,
    [correspondApplyId] BIGINT          NULL,
    [complainStatus]    TINYINT         NOT NULL,
    [reparation]        FLOAT (53)      NULL,
    [isPaid]            TINYINT         NULL,
    [eCardNumber]       NVARCHAR (50)   NOT NULL,
    [staffId]           INT             NOT NULL,
    [remark]            NVARCHAR (3000) NULL,
    [isFrozen]          TINYINT         NULL,
    CONSTRAINT [PK_UserComplain] PRIMARY KEY CLUSTERED ([identity_Id] ASC)
);

