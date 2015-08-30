CREATE TABLE [dbo].[FreeToReturnToSend] (
    [id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [description]     NVARCHAR (300) NULL,
    [descriptionType] INT            NULL,
    [shopId]          INT            NULL,
    CONSTRAINT [PK_FreeToReturnToSend] PRIMARY KEY CLUSTERED ([id] ASC)
);

