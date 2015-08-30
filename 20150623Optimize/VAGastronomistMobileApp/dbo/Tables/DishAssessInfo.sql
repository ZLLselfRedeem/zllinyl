CREATE TABLE [dbo].[DishAssessInfo] (
    [AssessmentID]  INT            IDENTITY (1, 1) NOT NULL,
    [OrderID]       INT            NULL,
    [DishID]        INT            NULL,
    [AssessContent] NVARCHAR (MAX) NULL,
    [AssessScore]   SMALLINT       NULL,
    [AssessStatus]  SMALLINT       NULL
);

