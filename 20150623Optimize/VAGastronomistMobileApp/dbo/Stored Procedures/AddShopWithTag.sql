



CREATE PROC [dbo].[AddShopWithTag](@TagId int, @ShopId int) 
AS 
BEGIN
   DECLARE @mTagNode hierarchyid
   SELECT @mTagNode = TagNode 
   FROM dbo.ShopTag
   WHERE TagId = @TagId
   IF @mTagNode IS NOT NULL
   BEGIN
	   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	   BEGIN TRANSACTION   

		  INSERT dbo.ShopWithTag (TagNode, ShopId)
		  VALUES(@mTagNode, @ShopId)
	   COMMIT
   END
END ;




