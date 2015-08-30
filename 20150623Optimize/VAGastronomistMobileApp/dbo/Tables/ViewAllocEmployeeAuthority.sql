CREATE TABLE [dbo].[ViewAllocEmployeeAuthority] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [employeeId]      INT NOT NULL,
    [shopAuthorityId] INT NOT NULL,
    [status]          BIT DEFAULT ((1)) NOT NULL
);

