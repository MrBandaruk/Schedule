CREATE TABLE [dbo].[Event] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (50)  NOT NULL,
    [Additional] NVARCHAR (300) NOT NULL,
    [StartDate]  SMALLDATETIME  NOT NULL,
    [EndDate]    SMALLDATETIME  NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC)
);

