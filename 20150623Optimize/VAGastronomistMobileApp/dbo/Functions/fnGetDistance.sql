
-- =============================================
-- Author:wangc	
-- Create date:20140325
-- Description:	创建经纬度距离计算函数
-- =============================================
CREATE FUNCTION [dbo].[fnGetDistance] 
--LatBegin 开始经度 
--LngBegin 开始维度 
--LatEnd 中心点经度 
--LngEnd 中心点纬度
(@LatBegin REAL, @LngBegin REAL, @LatEnd REAL, @LngEnd REAL) 
RETURNS FLOAT 
AS 
BEGIN 
--距离(千米) 
DECLARE @Distance REAL 
DECLARE @EARTH_RADIUS REAL 
SET @EARTH_RADIUS = 6378.137 
DECLARE @RadLatBegin REAL, 
@RadLatEnd REAL, 
@RadLatDiff REAL, 
@RadLngDiff REAL 
SET @RadLatBegin = @LatBegin *PI()/ 180.0 
SET @RadLatEnd = @LatEnd *PI()/ 180.0 
SET @RadLatDiff = @RadLatBegin - @RadLatEnd 
SET @RadLngDiff = @LngBegin *PI()/ 180.0 - @LngEnd *PI()/ 180.0 

SET @Distance = 2 *ASIN( 
SQRT( 
POWER(SIN(@RadLatDiff / 2), 2)+COS(@RadLatBegin)*COS(@RadLatEnd) 
*POWER(SIN(@RadLngDiff / 2), 2) 
) 
) 
SET @Distance = @Distance * @EARTH_RADIUS 
RETURN @Distance 
END 

