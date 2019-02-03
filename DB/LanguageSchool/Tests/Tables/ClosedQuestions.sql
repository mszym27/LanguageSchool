CREATE TABLE [Tests].[ClosedQuestions] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [CreationDate]            DATETIME       CONSTRAINT [DF_ClosedQuestionsCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]               BIT            NOT NULL,
    [DeletionDate]            DATETIME       NULL,
    [Contents]                NVARCHAR (250) NOT NULL,
    [NumberOfPossibleAnswers] INT            NOT NULL,
    [IsMultichoice]           BIT            NOT NULL,
    [Points]                  INT            NOT NULL,
    [LessonSubjectId]         INT            NOT NULL,
    CONSTRAINT [PK_ClosedQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedQuestions_LessonSubjects] FOREIGN KEY ([LessonSubjectId]) REFERENCES [Courses].[LessonSubjects] ([Id])
);









