CREATE TABLE [Users].[UserOpenAnswers] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]      BIT             NOT NULL,
    [CreationDate]   DATETIME        NOT NULL,
    [DeletionDate]   DATETIME        NULL,
    [UserId]         INT             NOT NULL,
    [TestId]         INT             NOT NULL,
    [OpenQuestionId] INT             NOT NULL,
    [Content]        NVARCHAR (4000) NULL,
    [Points]         INT             NULL,
    [Comment]        NVARCHAR (1000) NULL,
    CONSTRAINT [PK_UsersOpenAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersOpenAnswers_OpenQuestions] FOREIGN KEY ([OpenQuestionId]) REFERENCES [Courses].[OpenQuestions] ([Id]),
    CONSTRAINT [FK_UsersOpenAnswers_Tests] FOREIGN KEY ([TestId]) REFERENCES [Courses].[Tests] ([Id]),
    CONSTRAINT [FK_UsersOpenAnswers_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);

