CREATE TABLE [Tests].[OpenQuestions] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]    DATETIME        CONSTRAINT [DF_OpenQuestionsCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT             NOT NULL,
    [DeletionDate]    DATETIME        NULL,
    [Contents]        NVARCHAR (1000) NOT NULL,
    [Points]          INT             NOT NULL,
    [LessonSubjectId] INT             NOT NULL,
    CONSTRAINT [PK_OpenQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OpenQuestions_LessonSubjects] FOREIGN KEY ([LessonSubjectId]) REFERENCES [Courses].[LessonSubjects] ([Id])
);







