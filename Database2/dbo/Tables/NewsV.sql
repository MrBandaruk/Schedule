CREATE TABLE [dbo].[NewsV] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (60)  NOT NULL,
    [Body]  NVARCHAR (410) NOT NULL,
    CONSTRAINT [PK_NewsV1] PRIMARY KEY CLUSTERED ([Id] ASC)
);





