CREATE TABLE [dbo].[CustomerComplaint] (
    [id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [preOrder19dianId] BIGINT         NULL,
    [complaintMsg]     NVARCHAR (MAX) NULL,
    [complaintTime]    DATETIME       NULL,
    CONSTRAINT [PK_CustomerComplaint] PRIMARY KEY CLUSTERED ([id] ASC)
);

