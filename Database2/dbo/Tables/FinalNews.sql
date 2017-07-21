CREATE TABLE [dbo].[FinalNews] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [ShortTitle]   NVARCHAR (60)  NOT NULL,
    [FullTitle]    NVARCHAR (256) NOT NULL,
    [ShortArticle] NVARCHAR (410) NOT NULL,
    [FullArticle]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_FinalNews] PRIMARY KEY CLUSTERED ([Id] ASC)
);

