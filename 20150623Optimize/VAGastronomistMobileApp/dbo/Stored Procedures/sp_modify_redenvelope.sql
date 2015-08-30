

CREATE procedure [dbo].[sp_modify_redenvelope]
as
begin
declare @mobilePhoneNumber nvarchar(20)
declare @count int
declare @amount float

	set @amount=0;
	declare cur cursor for
select d.mobilePhoneNumber,COUNT(1),r.Amount from RedEnvelope r inner join RedEnvelopeDetails d on
r.redEnvelopeId=d.redEnvelopeId 
inner join CustomerInfo c on d.mobilePhoneNumber=c.mobilePhoneNumber
 where activityId=73
and isnull(r.mobilePhoneNumber,'')!=''
and c.RegisterDate<'2014-11-24'
group by d.mobilePhoneNumber,r.Amount
having COUNT(d.mobilePhoneNumber)>1;

	open cur
	fetch next from cur into @mobilePhoneNumber,@count,@amount; 
	while(@@FETCH_STATUS=0)
	begin
		--删除的detail
		delete from RedEnvelopeDetails where mobilePhoneNumber=@mobilePhoneNumber and treasureChestId=11255
and Id not in (select MIN(Id) from RedEnvelopeDetails where mobilePhoneNumber=@mobilePhoneNumber and treasureChestId=11255);

--select Id from RedEnvelopeDetails where mobilePhoneNumber=@mobilePhoneNumber and treasureChestId=11255
--and Id not in (select MIN(Id) from RedEnvelopeDetails where mobilePhoneNumber=@mobilePhoneNumber and treasureChestId=11255);

--print substring(@all,0,len(@all))

		--更新用户信息
		update CustomerInfo set notExecutedRedEnvelopeAmount=notExecutedRedEnvelopeAmount-@amount*(@count-1)
		where mobilePhoneNumber=@mobilePhoneNumber;
		
		--select notExecutedRedEnvelopeAmount-@amount*(@count-1)
		--from CustomerInfo
		--where mobilePhoneNumber=@mobilePhoneNumber;
		
		fetch next from cur into @mobilePhoneNumber,@count,@amount;
	end	
	close cur
	deallocate cur	
end

