CREATE TABLE [Courses].[ClosedQuestions] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]               BIT            NOT NULL,
    [CreationDate]            DATETIME       NOT NULL,
    [DeletionDate]            DATETIME       NULL,
    [TestId]                  INT            NOT NULL,
    [Contents]                NVARCHAR (250) NOT NULL,
    [NumberOfPossibleAnswers] INT            NOT NULL,
    [IsMultichoice]           BIT            NOT NULL,
    [Points]                  INT            NOT NULL,
    CONSTRAINT [PK_ClosedQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedQuestions_Tests] FOREIGN KEY ([TestId]) REFERENCES [Courses].[Courses] ([Id])
);



