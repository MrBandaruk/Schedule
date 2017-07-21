CREATE TABLE [dbo].[NewsImage] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [ImageItem] VARBINARY (MAX) NOT NULL,
    [NewsId]    INT             NOT NULL,
    CONSTRAINT [PK_NewsImage1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsImage_NewsV] FOREIGN KEY ([NewsId]) REFERENCES [dbo].[NewsV] ([Id])
);









