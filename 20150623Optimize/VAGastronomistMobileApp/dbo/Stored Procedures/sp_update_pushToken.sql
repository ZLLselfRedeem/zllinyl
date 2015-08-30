CREATE procedure [dbo].[sp_update_pushToken]
as
begin
	declare @n int =0
	declare @id int =0
	declare @customSendTime datetime
	declare cur cursor for	
select id,customSendTime from VAGastronomistNotification.dbo.CustomPushRecord where 
	customSendTime>'2014-9-20' and isSent=0 and sendCount<1 order by id ;
	open cur
	fetch next from cur into @id,@customSendTime; 
	while(@@FETCH_STATUS=0)
	begin		
						
			set @n+=1;	
			update VAGastronomistNotification.dbo.CustomPushRecord
			set customSendTime = DATEADD(MI,@n,customSendTime)
			where id=@id;				
			print cast(@n as nvarchar)
			+','+cast(@id as nvarchar)
			+','+cast(@customSendTime as nvarchar)+','+cast(DATEADD(MI,@n,@customSendTime) as nvarchar)			
						
		fetch next from cur into @id,@customSendTime; 
	end	
	close cur
	deallocate cur
end

