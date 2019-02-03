CREATE TABLE [Courses].[LessonSubjects] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate] DATETIME        CONSTRAINT [DF_LessonSubjectsCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]    BIT             NOT NULL,
    [DeletionDate] DATETIME        NULL,
    [IsActive]     BIT             NOT NULL,
    [Name]         NVARCHAR (150)  NOT NULL,
    [Description]  NVARCHAR (4000) NULL,
    [GroupId]      INT             NOT NULL,
    CONSTRAINT [PK_LessonSubjects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LessonSubjects_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id])
);





