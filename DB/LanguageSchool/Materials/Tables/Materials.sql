CREATE TABLE [Materials].[Materials] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]    DATETIME        CONSTRAINT [DF_MaterialsCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT             NOT NULL,
    [DeletionDate]    DATETIME        NULL,
    [LessonSubjectId] INT             NOT NULL,
    [Name]            NVARCHAR (50)   NOT NULL,
    [File]            VARBINARY (MAX) NOT NULL,
    [Comment]         NVARCHAR (1000) NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Materials_LessonSubjects] FOREIGN KEY ([LessonSubjectId]) REFERENCES [Courses].[LessonSubjects] ([Id])
);





