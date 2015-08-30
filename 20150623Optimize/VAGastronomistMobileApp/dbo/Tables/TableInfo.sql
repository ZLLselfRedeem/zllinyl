CREATE TABLE [dbo].[TableInfo] (
    [TableID]          INT           IDENTITY (1, 1) NOT NULL,
    [TableName]        NVARCHAR (50) NULL,
    [TableSeats]       SMALLINT      NULL,
    [TableStatus]      SMALLINT      NULL,
    [tablePrinter]     NVARCHAR (50) NULL,
    [tableAreaID]      INT           NULL,
    [interfaceTableno] CHAR (6)      NULL
);


GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
CREATE trigger [dbo].[TableStatusChange]
on [dbo].[TableInfo]
for update
as
if update(TableStatus) 
begin
delete from  TableMonitor where ChangeTableName='TableInfo'
insert into TableMonitor (ChangeTag,ChangeTime,ChangeMethod,ChangeTableName) values('true',getdate(),'','TableInfo')
end