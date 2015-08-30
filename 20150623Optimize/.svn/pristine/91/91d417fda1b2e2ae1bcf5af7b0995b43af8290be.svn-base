CREATE VIEW dbo.PreOrder19dianV
AS
SELECT     dbo.PreOrder19dian.preOrder19dianId, dbo.PreOrder19dian.menuId, dbo.PreOrder19dian.companyId, dbo.PreOrder19dian.shopId, dbo.PreOrder19dian.customerId, 
                      dbo.PreOrder19dian.orderInJson, dbo.PreOrder19dian.status, dbo.PreOrder19dian.preOrderSum, dbo.PreOrder19dian.preOrderServerSum, 
                      dbo.PreOrder19dian.preOrderTime, dbo.PreOrder19dian.isPaid, dbo.PreOrder19dian.prePaidSum, dbo.PreOrder19dian.prePayTime, 
                      dbo.PreOrder19dian.viewallocCommission, dbo.PreOrder19dian.transactionFee, dbo.PreOrder19dian.viewallocNeedsToPayToShop, 
                      dbo.PreOrder19dian.viewallocPaidToShopSum, dbo.PreOrder19dian.viewallocTransactionCompleted, dbo.PreOrder19dian.isApproved, 
                      dbo.PreOrder19dian.verifiedSaving, dbo.PreOrder19dian.isShopConfirmed, dbo.PreOrder19dian.invoiceTitle, dbo.PreOrder19dian.sundryJson, 
                      dbo.PreOrder19dian.refundMoneySum, dbo.PreOrder19dian.discount, dbo.PreOrder19dian.refundMoneyClosedSum, dbo.PreOrder19dian.deskNumber, 
                      dbo.PreOrder19dian.refundRedEnvelope, dbo.PreOrder19dian.appType, dbo.PreOrder19dian.appBuild, dbo.PreOrder19dian.expireTime, dbo.CustomerInfo.UserName, 
                      dbo.CustomerInfo.mobilePhoneNumber, dbo.ShopInfo.shopName, dbo.ShopInfo.cityID, dbo.PreOrder19dian.isEvaluation, 
                      dbo.PreorderEvaluation.PreorderEvaluationId, dbo.PreorderEvaluation.EvaluationValue, dbo.PreorderEvaluation.EvaluationContent, 
                      dbo.PreorderEvaluation.EvaluationTime, dbo.PreorderEvaluation.EvaluationLevel, dbo.PreOrder19dian.customerUUID
FROM         dbo.PreOrder19dian INNER JOIN
                      dbo.ShopInfo ON dbo.PreOrder19dian.shopId = dbo.ShopInfo.shopID INNER JOIN
                      dbo.CustomerInfo ON dbo.PreOrder19dian.customerId = dbo.CustomerInfo.CustomerID LEFT OUTER JOIN
                      dbo.PreorderEvaluation ON dbo.PreOrder19dian.preOrder19dianId = dbo.PreorderEvaluation.PreOrder19dianId

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[28] 2[33] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PreOrder19dian"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 278
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 19
         End
         Begin Table = "ShopInfo"
            Begin Extent = 
               Top = 204
               Left = 314
               Bottom = 421
               Right = 553
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "CustomerInfo"
            Begin Extent = 
               Top = 178
               Left = 610
               Bottom = 409
               Right = 872
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PreorderEvaluation"
            Begin Extent = 
               Top = 103
               Left = 951
               Bottom = 222
               Right = 1142
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3390
         Alias = 900
         Table = 2220
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'PreOrder19dianV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'PreOrder19dianV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'PreOrder19dianV';

