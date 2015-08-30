CREATE TABLE [dbo].[RoleAuthority] (
    [RoleAuthorityID]     INT IDENTITY (1, 1) NOT NULL,
    [RoleID]              INT NULL,
    [AuthorityID]         INT NULL,
    [RoleAuthorityStatus] INT NULL,
    CONSTRAINT [PK_RoleAuthority] PRIMARY KEY CLUSTERED ([RoleAuthorityID] ASC)
);

