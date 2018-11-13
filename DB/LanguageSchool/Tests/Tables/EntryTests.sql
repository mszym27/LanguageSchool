CREATE TABLE [Tests].[EntryTests] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]         BIT             NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    [DeletionDate]      DATETIME        NULL,
    [CourseId]          INT             NOT NULL,
    [Name]              NVARCHAR (50)   NOT NULL,
    [Comment]           NVARCHAR (1000) NULL,
    [NumberOfQuestions] INT             NOT NULL,
    [Points]            INT             NOT NULL,
    [IsActive]          BIT             NOT NULL,
    CONSTRAINT [PK_EntryTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntryTests_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);

