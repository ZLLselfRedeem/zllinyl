CREATE TABLE [dbo].[UserLottery] (
    [id]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [shopId]             INT           NOT NULL,
    [startTime]          NVARCHAR (20) NOT NULL,
    [endTime]            NVARCHAR (20) NOT NULL,
    [lastLotteryDate]    DATETIME      NULL,
    [workdayPrizeNum]    INT           NOT NULL,
    [currentPrizeNum]    INT           NOT NULL,
    [weekendPrizeNum]    INT           NOT NULL,
    [highestLotteryRate] FLOAT (53)    NOT NULL,
    [lowestLotteryRate]  FLOAT (53)    NOT NULL,
    [decreaseRate]       FLOAT (53)    NOT NULL,
    [status]             TINYINT       NOT NULL,
    CONSTRAINT [PK_UserLottery] PRIMARY KEY CLUSTERED ([id] ASC)
);

