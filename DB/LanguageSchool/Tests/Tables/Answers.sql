CREATE TABLE [Tests].[Answers] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [CreationDate]     DATETIME       CONSTRAINT [DF_AnswersCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]        BIT            NOT NULL,
    [DeletionDate]     DATETIME       NULL,
    [ClosedQuestionId] INT            NOT NULL,
    [Answer]           NVARCHAR (250) NOT NULL,
    [IsCorrect]        BIT            NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Answers_ClosedQuestions] FOREIGN KEY ([ClosedQuestionId]) REFERENCES [Tests].[ClosedQuestions] ([Id])
);



