CREATE TABLE [Tests].[TestOpenQuestions] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [DeletionDate] DATETIME NULL,
    [TestId]       INT      NOT NULL,
    [QuestionId]   INT      NOT NULL,
    CONSTRAINT [PK_TestOpenQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TestOpenQuestions_OpenQuestions] FOREIGN KEY ([QuestionId]) REFERENCES [Tests].[OpenQuestions] ([Id]),
    CONSTRAINT [FK_TestOpenQuestions_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id])
);



