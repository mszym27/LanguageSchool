CREATE TABLE [Tests].[OpenQuestions] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]       BIT             NOT NULL,
    [CreationDate]    DATETIME        NOT NULL,
    [DeletionDate]    DATETIME        NULL,
    [LessonSubjectId] INT             NOT NULL,
    [Contents]        NVARCHAR (1000) NOT NULL,
    [Points]          INT             NOT NULL,
    CONSTRAINT [PK_OpenQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OpenQuestions_LessonSubjects] FOREIGN KEY ([LessonSubjectId]) REFERENCES [Courses].[LessonSubjects] ([Id])
);





