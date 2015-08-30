CREATE TABLE [dbo].[RedEnvelope] (
    [redEnvelopeId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [treasureChestId]   BIGINT         NULL,
    [Amount]            FLOAT (53)     NULL,
    [mobilePhoneNumber] NVARCHAR (20)  NULL,
    [status]            BIT            NULL,
    [lastUpdateTime]    DATETIME       NULL,
    [getTime]           DATETIME       NULL,
    [isExecuted]        INT            NULL,
    [isExpire]          BIT            NULL,
    [expireTime]        DATETIME       NULL,
    [unusedAmount]      FLOAT (53)     NULL,
    [activityId]        INT            NULL,
    [isOwner]           BIT            NULL,
    [isOverflow]        BIT            NULL,
    [cookie]            NVARCHAR (100) NULL,
    [isChange]          BIT            CONSTRAINT [DF_RedEnvelope_isChange] DEFAULT ((0)) NULL,
    [uuid]              NVARCHAR (100) NULL,
    [effectTime]        DATETIME       NULL,
    CONSTRAINT [PK_RedEnvelope] PRIMARY KEY CLUSTERED ([redEnvelopeId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_treasureChestAndMobile]
    ON [dbo].[RedEnvelope]([treasureChestId] ASC, [mobilePhoneNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_activityId]
    ON [dbo].[RedEnvelope]([activityId] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_treasureChestAndCookie]
    ON [dbo].[RedEnvelope]([treasureChestId] ASC, [cookie] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_redEnvelope_treasureChestId_Status]
    ON [dbo].[RedEnvelope]([treasureChestId] ASC, [status] ASC)
    INCLUDE([Amount]);


GO
CREATE NONCLUSTERED INDEX [ix_redEnvelope_mobilePhoneNumer]
    ON [dbo].[RedEnvelope]([mobilePhoneNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_RedEnvelope_rank]
    ON [dbo].[RedEnvelope]([isOverflow] ASC, [isExecuted] ASC)
    INCLUDE([Amount], [mobilePhoneNumber]);


GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[GetRedEnvelope]
   ON  [dbo].[RedEnvelope]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[CustomerInfo] SET [executedRedEnvelopeAmount]= ISNULL([executedRedEnvelopeAmount],0)+b.[Amount]
	FROM INSERTED b 
	INNER JOIN DELETED c on b.[redEnvelopeId]=c.[redEnvelopeId]
	WHERE b.[mobilePhoneNumber]=[dbo].[CustomerInfo].[mobilePhoneNumber] AND b.[isExecuted]=1 AND c.[isExecuted]=0 AND b.[status]=1
    -- Insert statements for trigger here

	--号码纠错
	UPDATE [dbo].[CustomerInfo] SET [executedRedEnvelopeAmount]= ISNULL([executedRedEnvelopeAmount],0)+b.[Amount]
	FROM INSERTED b 
	INNER JOIN DELETED c on b.[redEnvelopeId]=c.[redEnvelopeId]
	WHERE b.[mobilePhoneNumber]=[dbo].[CustomerInfo].[mobilePhoneNumber] AND b.[isExecuted]=1 AND c.[isExecuted]=1 AND b.[IsChange]=1 AND ISNULL(c.[isChange],0)=0

	UPDATE [dbo].[CustomerInfo] SET [executedRedEnvelopeAmount]= ISNULL([executedRedEnvelopeAmount],0)-b.[Amount]
	FROM INSERTED b 
	INNER JOIN DELETED c on b.[redEnvelopeId]=c.[redEnvelopeId]
	WHERE c.[mobilePhoneNumber]=[dbo].[CustomerInfo].[mobilePhoneNumber] AND b.[isExecuted]=1 AND c.[isExecuted]=1 AND b.[IsChange]=1 AND ISNULL(c.[isChange],0)=0

  ----记录下 
        --   INSERT INTO [dbo].[T_OperateLog]
        --   ([SYSTEMUSER]
        --   ,[USER]
        --   ,[CreateTime])
        --VALUES
        --   (system_user
        --   ,user
        --   ,getdate())
END


GO
DISABLE TRIGGER [dbo].[GetRedEnvelope]
    ON [dbo].[RedEnvelope];


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[UnLockBox] 
   ON [dbo].[RedEnvelope]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here

	--DECLARE @mobilePhone nvarchar(20),@amount float
	UPDATE [dbo].[CustomerInfo] SET [notExecutedRedEnvelopeAmount] =ISNULL([notExecutedRedEnvelopeAmount],0)+b.[Amount] 
	FROM INSERTED b 
	WHERE b.[mobilePhoneNumber]=[dbo].[CustomerInfo].[mobilePhoneNumber] AND b.[status]=1 AND b.[isOverflow]=0 AND b.[isExecuted]=0

	UPDATE [dbo].[CustomerInfo] SET [executedRedEnvelopeAmount]=ISNULL([executedRedEnvelopeAmount],0)+b.[Amount]
	FROM INSERTED b
	WHERE b.[mobilePhoneNumber]=[dbo].[CustomerInfo].[mobilePhoneNumber] AND b.[status]=1 AND b.[isOverflow]=0 AND b.[isExecuted]=1
END

GO
DISABLE TRIGGER [dbo].[UnLockBox]
    ON [dbo].[RedEnvelope];

