CREATE TABLE [Courses].[OpenQuestions] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT             NOT NULL,
    [CreationDate] DATETIME        NOT NULL,
    [DeletionDate] DATETIME        NULL,
    [TestId]       INT             NOT NULL,
    [Contents]     NVARCHAR (1000) NOT NULL,
    [Points]       INT             NOT NULL,
    CONSTRAINT [PK_OpenQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OpenQuestions_Tests] FOREIGN KEY ([TestId]) REFERENCES [Courses].[Courses] ([Id])
);



