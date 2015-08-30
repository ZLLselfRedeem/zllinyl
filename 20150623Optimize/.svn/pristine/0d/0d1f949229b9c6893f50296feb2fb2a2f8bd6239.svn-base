CREATE TABLE [dbo].[QueueInfo] (
    [CallNumberID]  INT           IDENTITY (1, 1) NOT NULL,
    [TableID]       INT           NULL,
    [Password]      NVARCHAR (50) NULL,
    [QueuePhone]    NVARCHAR (50) NULL,
    [CreationTime]  DATETIME      NULL,
    [PeopleNumber]  SMALLINT      NULL,
    [QueueStatus]   SMALLINT      NULL,
    [OrderID]       INT           NULL,
    [DisplayName]   VARCHAR (50)  NULL,
    [WaitingListID] INT           NULL,
    [CallTime]      DATETIME      NULL,
    [IsActive]      BIT           NULL,
    CONSTRAINT [PK_QueueID] PRIMARY KEY CLUSTERED ([CallNumberID] ASC)
);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE trigger [dbo].[QueueInsert]
on [dbo].[QueueInfo]
for insert
as
begin
--DECLARE @DisplayNumber INT 
--declare @waitingLineID INT
--select @DisplayNumber=DisplayNumber,@waitingLineID=waitingLineID from inserted
--update WaitingLine set DisplayNumber=@DisplayNumber where waitingLineID=@waitingLineID

delete from TableMonitor where ChangeTableName='QueueInfo'
insert into TableMonitor (ChangeTag,ChangeTime,ChangeMethod,ChangeTableName) values('true',getdate(),'','QueueInfo')
end