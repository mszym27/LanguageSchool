CREATE TABLE [Administration].[Classrooms] (
    [Id]            INT          NOT NULL,
    [Identificator] VARCHAR (10) NOT NULL,
    [NumberOfSeats] INT          NOT NULL,
    [IsDeleted]     BIT          NOT NULL,
    [DeletionDate]  BIT          NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY CLUSTERED ([Id] ASC)
);



