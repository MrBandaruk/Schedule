CREATE TABLE [dbo].[News] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Title]     NVARCHAR (60)  NOT NULL,
    [ImageLink] NVARCHAR (MAX) NOT NULL,
    [Body]      NVARCHAR (410) NOT NULL,
    CONSTRAINT [PK_NewsNew] PRIMARY KEY CLUSTERED ([Id] ASC)
);



