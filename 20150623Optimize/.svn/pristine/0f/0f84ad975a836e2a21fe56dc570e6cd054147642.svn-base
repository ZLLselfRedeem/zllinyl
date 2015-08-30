CREATE TABLE [dbo].[MealSchedule] (
    [MealScheduleID] INT      IDENTITY (1, 1) NOT NULL,
    [MealID]         INT      NOT NULL,
    [ValidFrom]      DATETIME NOT NULL,
    [ValidTo]        DATETIME NOT NULL,
    [SoldCount]      INT      NOT NULL,
    [DinnerTime]     DATETIME NOT NULL,
    [IsActive]       INT      NOT NULL,
    [DinnerType]     INT      NOT NULL,
    [TotalCount]     INT      NOT NULL,
    CONSTRAINT [PK_MEALSCHEDULE] PRIMARY KEY CLUSTERED ([MealScheduleID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'套餐排期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MealSchedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排期编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MealSchedule', @level2type = N'COLUMN', @level2name = N'MealScheduleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'套餐编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MealSchedule', @level2type = N'COLUMN', @level2name = N'MealID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期起始', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MealSchedule', @level2type = N'COLUMN', @level2name = N'ValidFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期截止', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MealSchedule', @level2type = N'COLUMN', @level2name = N'ValidTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可购份数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MealSchedule', @level2type = N'COLUMN', @level2name = N'SoldCount';

