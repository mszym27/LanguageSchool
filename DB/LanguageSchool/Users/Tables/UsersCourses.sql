CREATE TABLE [Users].[UsersCourses] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [DeletionDate] DATETIME NULL,
    [UserId]       INT      NOT NULL,
    [CourseId]     INT      NOT NULL,
    CONSTRAINT [PK_UsersCourses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersCourses_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id]),
    CONSTRAINT [FK_UsersCourses_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);

