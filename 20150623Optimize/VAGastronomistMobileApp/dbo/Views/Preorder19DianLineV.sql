CREATE VIEW [dbo].[Preorder19DianLineV]
	AS SELECT     dbo.Preorder19DianLine.*, dbo.PreOrder19dian.Status, dbo.PreOrder19dian.IsPaid, dbo.PreOrder19dian.PreOrderTime
FROM         dbo.PreOrder19dian INNER JOIN
                      dbo.Preorder19DianLine ON dbo.PreOrder19dian.preOrder19dianId = dbo.Preorder19DianLine.Preorder19DianId
