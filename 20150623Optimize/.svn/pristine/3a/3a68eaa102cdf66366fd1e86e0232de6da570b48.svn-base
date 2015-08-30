CREATE TABLE [dbo].[CustomerInfo] (
    [CustomerID]                        BIGINT         IDENTITY (1, 1) NOT NULL,
    [CustomerRankID]                    INT            NULL,
    [UserName]                          NVARCHAR (100) NULL,
    [Password]                          NVARCHAR (500) NULL,
    [RegisterDate]                      DATETIME       NULL,
    [CustomerFirstName]                 NVARCHAR (50)  NULL,
    [CustomerLastName]                  NVARCHAR (50)  NULL,
    [CustomerSex]                       INT            NULL,
    [CustomerBirthday]                  DATETIME       NULL,
    [mobilePhoneNumber]                 NVARCHAR (50)  NULL,
    [CustomerAddress]                   NVARCHAR (500) NULL,
    [customerEmail]                     NVARCHAR (500) NULL,
    [CustomerStatus]                    INT            NULL,
    [cookie]                            NVARCHAR (100) NULL,
    [securityQuestion]                  NVARCHAR (50)  NULL,
    [securityAnswer]                    NVARCHAR (50)  NULL,
    [localAlarm]                        BIT            NULL,
    [localAlarmHour]                    INT            NULL,
    [localAlarmMinute]                  INT            NULL,
    [receivePushForFavoriteRestaurants] BIT            NULL,
    [isVIP]                             BIT            NULL,
    [vipExpireDate]                     DATETIME       NULL,
    [verificationCode]                  NVARCHAR (50)  NULL,
    [verificationCodeTime]              DATETIME       NULL,
    [verificationTime]                  DATETIME       NULL,
    [money19dianRemained]               FLOAT (53)     NULL,
    [loginRewardTime]                   DATETIME       NULL,
    [registerRewardTime]                DATETIME       NULL,
    [verifyMobileRewardTime]            DATETIME       NULL,
    [continuousLoginNumber]             INT            NULL,
    [eCardNumber]                       NVARCHAR (50)  NULL,
    [wechatId]                          NVARCHAR (100) NULL,
    [titleName]                         NVARCHAR (100) NULL,
    [registerCityId]                    INT            NULL,
    [preOrderTotalAmount]               FLOAT (53)     NULL,
    [preOrderTotalQuantity]             INT            NULL,
    [currentPlatformVipGrade]           INT            NULL,
    [personalImgInfo]                   NVARCHAR (MAX) NULL,
    [isVCSendByVoice]                   BIT            NULL,
    [defaultPayment]                    INT            NULL,
    [executedRedEnvelopeAmount]         FLOAT (53)     NULL,
    [notExecutedRedEnvelopeAmount]      FLOAT (53)     NULL,
    [Picture]                           NVARCHAR (256) NULL,
    [IsUpdatePicture]                   BIT            NULL,
    [verificationCodeMobile]            NVARCHAR (50)  NULL,
    [currentCityId]                     INT            NULL,
    [verificationCodeErrCnt]            INT            NULL,
    CONSTRAINT [PK_CustomerInfo] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [cookie]
    ON [dbo].[CustomerInfo]([cookie] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_mobilePhoneNumber]
    ON [dbo].[CustomerInfo]([mobilePhoneNumber] ASC)
    INCLUDE([CustomerID], [UserName]);

