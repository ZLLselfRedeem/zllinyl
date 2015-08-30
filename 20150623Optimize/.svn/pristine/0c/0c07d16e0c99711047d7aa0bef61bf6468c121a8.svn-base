CREATE VIEW [dbo].[CouponGetDetailV]
AS
SELECT     dbo.Coupon.CouponName, dbo.PreOrder19dian.PreOrderSum, dbo.Coupon.ShopId, dbo.CouponGetDetail.CouponGetDetailID, 
                      dbo.CouponGetDetail.CouponDetailNumber, dbo.CouponGetDetail.GetTime, dbo.CouponGetDetail.ValidityEnd, dbo.CouponGetDetail.RequirementMoney, 
                      dbo.CouponGetDetail.DeductibleAmount, dbo.CouponGetDetail.[State], dbo.CouponGetDetail.MobilePhoneNumber, dbo.CouponGetDetail.CouponId, 
                      dbo.CouponGetDetail.UseTime, dbo.CouponGetDetail.PreOrder19DianId, dbo.CouponGetDetail.IsCorrected, dbo.CouponGetDetail.CorrectTime, 
                      dbo.CouponGetDetail.SharePreOrder19DianId, dbo.CouponGetDetail.OriginalNumber
FROM         dbo.PreOrder19dian INNER JOIN
                      dbo.CouponGetDetail ON dbo.PreOrder19dian.preOrder19dianId = dbo.CouponGetDetail.PreOrder19DianId INNER JOIN
                      dbo.Coupon ON dbo.CouponGetDetail.CouponId = dbo.Coupon.CouponId