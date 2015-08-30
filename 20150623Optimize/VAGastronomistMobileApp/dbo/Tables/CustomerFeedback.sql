CREATE TABLE [dbo].[CustomerFeedback] (
    [id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [customerId]   BIGINT         NULL,
    [feedbackMsg]  NVARCHAR (MAX) NULL,
    [feedbackTime] DATETIME       NULL,
    CONSTRAINT [PK_CustomerFeedback] PRIMARY KEY CLUSTERED ([id] ASC)
);

