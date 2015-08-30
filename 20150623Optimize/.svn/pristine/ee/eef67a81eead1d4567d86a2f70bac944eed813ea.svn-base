


CREATE PROC [dbo].[AddShopTag](@TagId int, @Name nvarchar(50)) 
AS 
BEGIN
   DECLARE @mTagNode hierarchyid, @lc hierarchyid
   SELECT @mTagNode = TagNode 
   FROM dbo.ShopTag
   WHERE TagId = @TagId
   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
   BEGIN TRANSACTION
      SELECT @lc = max(TagNode) 
      FROM dbo.ShopTag
      WHERE TagNode.GetAncestor(1) =@mTagNode ;

      INSERT dbo.ShopTag (TagNode, Name)
      VALUES(ISNULL(@mTagNode.GetDescendant(@lc, NULL),hierarchyid::GetRoot()), @Name)

	  SELECT @@IDENTITY;
   COMMIT
END ;



