CREATE TABLE [dbo].[MainNews] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [ShortTitle]   NVARCHAR (60)   NOT NULL,
    [FullTitle]    NVARCHAR (256)  NOT NULL,
    [Image]        VARBINARY (MAX) NOT NULL,
    [ShortArticle] NVARCHAR (410)  NOT NULL,
    [FullArticle]  NVARCHAR (MAX)  NOT NULL,
    [Date]         DATETIME        NOT NULL,
    [Author]       NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_News2] PRIMARY KEY CLUSTERED ([Id] ASC)
);

