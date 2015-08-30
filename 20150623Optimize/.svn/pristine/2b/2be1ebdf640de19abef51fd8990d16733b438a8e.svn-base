

CREATE proc [dbo].[sp_updateShopCount](@childrenTagId xml,@shopCount int)
as
begin
declare @tagId int
declare cur cursor for select tagId from shopTag where tagId IN (
  select d.x.value('./id[1]','int') from @childrenTagId.nodes('/*') as d(x));
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
   BEGIN TRANSACTION
open cur
fetch next from cur into @tagId;
while(@@FETCH_STATUS=0)
begin
   update ShopTag set ShopCount=ISNULL(ShopCount,0)+@shopCount where TagId in (@tagId);
DECLARE @level2 hierarchyid
	SELECT @level2=TagNode FROM ShopTag WHERE TagId=@tagId
	update ShopTag set ShopCount=ISNULL(ShopCount,0)+@shopCount
	where @level2.IsDescendantOf(TagNode)=1	and TagLevel=1 and Enable=1;
	
	fetch next from cur into @tagId;
end
close cur
deallocate cur
 COMMIT
end

