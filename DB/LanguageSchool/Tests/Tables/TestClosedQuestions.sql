CREATE TABLE [Tests].[TestClosedQuestions] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [DeletionDate] DATETIME NULL,
    [TestId]       INT      NOT NULL,
    [QuestionId]   INT      NOT NULL,
    CONSTRAINT [PK_TestClosedQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestClosedQuestions_ClosedQuestions] FOREIGN KEY ([QuestionId]) REFERENCES [Tests].[ClosedQuestions] ([Id]),
    CONSTRAINT [FK_TestClosedQuestions_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id])
);



