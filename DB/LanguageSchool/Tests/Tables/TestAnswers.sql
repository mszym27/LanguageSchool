CREATE TABLE [Tests].[TestAnswers] (
    [Id]                   INT IDENTITY (1, 1) NOT NULL,
    [TestClosedQuestionId] INT NOT NULL,
    [AnswerId]             INT NOT NULL,
    CONSTRAINT [PK_TestAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestAnswers_Answers] FOREIGN KEY ([AnswerId]) REFERENCES [Tests].[Answers] ([Id]),
    CONSTRAINT [FK_TestAnswers_Tests] FOREIGN KEY ([TestClosedQuestionId]) REFERENCES [Tests].[TestClosedQuestions] ([Id])
);



