CREATE TABLE [Users].[UserClosedAnswers] (
    [Id]                   INT IDENTITY (1, 1) NOT NULL,
    [UserId]               INT NOT NULL,
    [TestId]               INT NOT NULL,
    [TestClosedQuestionId] INT NOT NULL,
    [AnswerId]             INT NOT NULL,
    [IsCorrect]            BIT NOT NULL,
    [UserTestId]           INT NOT NULL,
    CONSTRAINT [PK_UserClosedAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClosedAnswers_Answers] FOREIGN KEY ([AnswerId]) REFERENCES [Tests].[Answers] ([Id]),
    CONSTRAINT [FK_UserClosedAnswers_TestClosedQuestions] FOREIGN KEY ([TestClosedQuestionId]) REFERENCES [Tests].[TestClosedQuestions] ([Id]),
    CONSTRAINT [FK_UserClosedAnswers_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id]),
    CONSTRAINT [FK_UserClosedAnswers_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id]),
    CONSTRAINT [FK_UserClosedAnswers_UsersTests] FOREIGN KEY ([UserTestId]) REFERENCES [Users].[UsersTests] ([Id])
);





