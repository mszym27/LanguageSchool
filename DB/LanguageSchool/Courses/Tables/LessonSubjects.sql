CREATE TABLE [Courses].[LessonSubjects] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT             NOT NULL,
    [CreationDate] DATETIME        NOT NULL,
    [DeletionDate] DATETIME        NULL,
    [CourseId]     INT             NOT NULL,
    [Name]         NVARCHAR (150)  NOT NULL,
    [Description]  NVARCHAR (4000) NULL,
    [IsActive]     BIT             NOT NULL,
    CONSTRAINT [PK_LessonSubjects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LessonSubjects_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);

