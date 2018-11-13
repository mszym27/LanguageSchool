CREATE TABLE [Tests].[Answers] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]        BIT            NOT NULL,
    [CreationDate]     DATETIME       NOT NULL,
    [DeletionDate]     DATETIME       NULL,
    [ClosedQuestionId] INT            NOT NULL,
    [Answer]           NVARCHAR (250) NOT NULL,
    [IsCorrect]        BIT            NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Answers_ClosedQuestions] FOREIGN KEY ([ClosedQuestionId]) REFERENCES [Tests].[ClosedQuestions] ([Id])
);

