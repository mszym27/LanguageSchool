﻿CREATE TABLE [Courses].[Tests] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]               BIT             NOT NULL,
    [CreationDate]            DATETIME        NOT NULL,
    [DeletionDate]            DATETIME        NULL,
    [CourseId]                INT             NOT NULL,
    [Name]                    NVARCHAR (50)   NOT NULL,
    [Comment]                 NVARCHAR (1000) NULL,
    [NumberOfQuestions]       INT             NOT NULL,
    [NumberOfOpenQuestions]   INT             NOT NULL,
    [NumberOfClosedQuestions] INT             NOT NULL,
    [Points]                  INT             NOT NULL,
    [IsAvaibleAsEntry]        BIT             NOT NULL,
    CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tests_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);



