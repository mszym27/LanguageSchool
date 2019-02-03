CREATE TABLE [Tests].[Tests] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]            DATETIME        CONSTRAINT [DF_TestsCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]               BIT             NOT NULL,
    [DeletionDate]            DATETIME        NULL,
    [Name]                    NVARCHAR (50)   NOT NULL,
    [Comment]                 NVARCHAR (1000) NULL,
    [NumberOfQuestions]       INT             NOT NULL,
    [NumberOfOpenQuestions]   INT             NOT NULL,
    [NumberOfClosedQuestions] INT             NOT NULL,
    [Points]                  INT             NOT NULL,
    [IsActive]                BIT             NOT NULL,
    [GroupId]                 INT             NOT NULL,
    CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tests_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id])
);













