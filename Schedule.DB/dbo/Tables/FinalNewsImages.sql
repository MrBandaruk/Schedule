CREATE TABLE [dbo].[FinalNewsImages] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [ImageItem] VARBINARY (MAX) NOT NULL,
    [NewsId]    INT             NOT NULL,
    CONSTRAINT [PK_FinalNewsImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FinalNewsImages_FinalNews] FOREIGN KEY ([NewsId]) REFERENCES [dbo].[FinalNews] ([Id])
);

