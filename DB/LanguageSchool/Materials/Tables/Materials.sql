CREATE TABLE [Materials].[Materials] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [IsDeleted]       BIT              NOT NULL,
    [CreationDate]    DATETIME         NOT NULL,
    [DeletionDate]    DATETIME         NULL,
    [LessonSubjectId] INT              NOT NULL,
    [Name]            NVARCHAR (50)    NOT NULL,
    [File]            VARBINARY (4000) NULL,
    [Comment]         NVARCHAR (1000)  NULL,
    [IsActive]        BIT              NOT NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Materials_LessonSubjects] FOREIGN KEY ([LessonSubjectId]) REFERENCES [Courses].[LessonSubjects] ([Id])
);

