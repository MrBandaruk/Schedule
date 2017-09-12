CREATE TABLE [dbo].[ClientProfiles] (
    [Id]      NVARCHAR (128) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [Surname] NVARCHAR (MAX) NULL,
    [Age]     INT            NOT NULL,
    CONSTRAINT [PK_dbo.ClientProfiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ClientProfiles_dbo.AspNetUsers_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[ClientProfiles]([Id] ASC);

