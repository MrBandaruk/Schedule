CREATE TABLE [dbo].[FinalEvent] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (50)  NOT NULL,
    [Additional] NVARCHAR (300) NOT NULL,
    [StartDate]  SMALLDATETIME  NOT NULL,
    [EndDate]    SMALLDATETIME  NOT NULL,
    [Color]      NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_FinalEvent] PRIMARY KEY CLUSTERED ([Id] ASC)
);

