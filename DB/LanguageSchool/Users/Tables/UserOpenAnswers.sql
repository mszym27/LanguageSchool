﻿CREATE TABLE [Users].[UserOpenAnswers] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]   DATETIME        CONSTRAINT [DF_UserOpenAnswersCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT            NOT NULL,
    [DeletionDate]   DATETIME        NULL,
    [IsMarked]       BIT             NOT NULL,
    [Content]        NVARCHAR (4000) NULL,
    [Points]         INT             NOT NULL,
    [Comment]        NVARCHAR (1000) NULL,
    [UserId]         INT             NOT NULL,
    [UserTestId]     INT             NOT NULL,
    [OpenQuestionId] INT             NOT NULL,
    CONSTRAINT [PK_UsersOpenAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserOpenAnswers_UsersTests] FOREIGN KEY ([UserTestId]) REFERENCES [Users].[UsersTests] ([Id]),
    CONSTRAINT [FK_UsersOpenAnswers_OpenQuestions] FOREIGN KEY ([OpenQuestionId]) REFERENCES [Tests].[OpenQuestions] ([Id]),
    CONSTRAINT [FK_UsersOpenAnswers_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);













