CREATE TABLE [dbo].[NewsOld] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (256) NOT NULL,
    [Body]  NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_News1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

