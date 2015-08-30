CREATE TABLE [dbo].[ReserveInfo] (
    [ReserveID]        INT           IDENTITY (1, 1) NOT NULL,
    [TableID]          INT           NULL,
    [ReserveName]      NVARCHAR (50) NULL,
    [ReservePhone]     NVARCHAR (50) NULL,
    [ReserveDate]      DATETIME      NULL,
    [DinnerTimeID]     INT           NULL,
    [PeopleNumber]     SMALLINT      NULL,
    [ReserveStatus]    SMALLINT      NULL,
    [OrderID]          INT           NULL,
    [EmployeeUserName] NVARCHAR (50) NULL,
    [ReserveStartTime] DATETIME      NULL,
    [ReserveEndTime]   DATETIME      NULL,
    CONSTRAINT [PK_ReserveInfo] PRIMARY KEY CLUSTERED ([ReserveID] ASC)
);

