



CREATE procedure [dbo].[sp_package_pushToken](@appType int,@all nvarchar(max) out )
as
begin
declare @pushToken nvarchar(500)
declare @n int
declare @note nvarchar(1)

if @appType=1
begin
	set @note='*';
end
else
begin
	set @note=',';
end
set @n=0;
	set @all='';
	declare cur cursor for
	select pushToken from tempPushToken2 where appType=@appType
	open cur
	fetch next from cur into @pushToken; 
	while(@@FETCH_STATUS=0)
	begin
		if len(@all)+LEN(@pushToken)>3900
		begin			
			insert into VAGastronomistNotification.dbo.CustomPushRecord
			(isLocked,pushToken,addTime,customSendTime,isSent,sendCount,appType,message,customType,customValue)
			select 0,substring(@all,0,len(@all)),
			GETDATE(),
			DATEADD(SECOND,@n*30,GETDATE()),
			0,0,@appType,'春节都吃腻了？给你准备了诱人小点解腻！来看推荐>>',
			'11','-999'
			print substring(@all,0,len(@all))
			set @n+=1;
			set @all ='';
			set @all =@all+@pushToken+@note;
		end		
		else
		begin
			set @all =@all+@pushToken+@note;
		end
		fetch next from cur into @pushToken;
	end	
	close cur
	deallocate cur
	insert into VAGastronomistNotification.dbo.CustomPushRecord
			(isLocked,pushToken,addTime,customSendTime,isSent,sendCount,appType,message,customType,customValue)
			select 0,substring(@all,0,len(@all)),
			GETDATE(),DATEADD(SECOND,@n*30,GETDATE()),0,0,@appType,'春节都吃腻了？给你准备了诱人小点解腻！来看推荐>>',
			'11','-999'
	print substring(@all,0,len(@all))
	set @n+=1;
	select @n
end

