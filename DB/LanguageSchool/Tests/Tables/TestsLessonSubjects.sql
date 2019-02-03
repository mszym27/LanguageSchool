CREATE TABLE [Tests].[TestsLessonSubjects] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]       BIT      NOT NULL,
    [DeletionDate]    DATETIME NULL,
    [TestId]          INT      NOT NULL,
    [LessonSubjectId] INT      NOT NULL,
    CONSTRAINT [PK_TestsLessonSubjects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestsLessonSubjects_LessonSubjects] FOREIGN KEY ([LessonSubjectId]) REFERENCES [Courses].[LessonSubjects] ([Id]),
    CONSTRAINT [FK_TestsLessonSubjects_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id])
);



