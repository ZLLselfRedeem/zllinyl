CREATE VIEW dbo.CouponV
AS
SELECT     dbo.Coupon.CouponId, dbo.Coupon.CouponName, dbo.Coupon.ValidityPeriod, dbo.Coupon.StartDate, dbo.Coupon.EndDate, dbo.Coupon.SheetNumber, 
                      dbo.Coupon.SendCount, dbo.Coupon.ShopId, dbo.Coupon.RequirementMoney, dbo.Coupon.SortOrder, dbo.Coupon.DeductibleAmount, dbo.Coupon.State, 
                      dbo.Coupon.CreatedBy, dbo.Coupon.CreateTime, dbo.Coupon.LastUpdatedBy, dbo.Coupon.LastUpdatedTime, dbo.Coupon.Remark, dbo.ShopInfo.shopName, 
                      dbo.ShopInfo.cityID, dbo.ShopInfo.countyID, dbo.ShopInfo.companyID, dbo.Coupon.RefuseReason, dbo.City.cityName, dbo.Coupon.AuditEmployee, 
                      dbo.Coupon.AuditTime,dbo.ShopInfo.ShopStatus,dbo.ShopInfo.IsHandle
FROM         dbo.Coupon INNER JOIN
                      dbo.ShopInfo ON dbo.Coupon.ShopId = dbo.ShopInfo.shopID INNER JOIN
                      dbo.City ON dbo.ShopInfo.cityID = dbo.City.cityID

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Coupon"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 16
         End
         Begin Table = "ShopInfo"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 125
               Right = 497
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "City"
            Begin Extent = 
               Top = 6
               Left = 535
               Bottom = 114
               Right = 712
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
      Begin ColumnWidths = 24
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CouponV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CouponV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CouponV';

