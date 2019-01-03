CREATE TABLE [Administration].[Classroms] (
    [Id]            INT          NOT NULL,
    [Identificator] VARCHAR (10) NOT NULL,
    [NumberOfSeats] INT          NOT NULL,
    CONSTRAINT [PK_Classroms] PRIMARY KEY CLUSTERED ([Id] ASC)
);

