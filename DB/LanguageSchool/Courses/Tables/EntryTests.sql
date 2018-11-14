CREATE TABLE [Courses].[EntryTests] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [DeletionDate] DATETIME NULL,
    [CourseId]     INT      NOT NULL,
    [TestId]       INT      NOT NULL,
    [IsActive]     BIT      NOT NULL,
    CONSTRAINT [PK_EntryTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntryTests_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id]),
    CONSTRAINT [FK_EntryTests_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id])
);

