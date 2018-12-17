CREATE TABLE [Courses].[CourseTimes] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [DeletionDate] DATETIME NULL,
    [CourseId]     INT      NOT NULL,
    [DayOfWeek]    INT      NOT NULL,
    [StartTime]    TIME (7) NOT NULL,
    [EndTime]      TIME (7) NULL,
    [IsActive]     BIT      NOT NULL,
    CONSTRAINT [PK_CourseTimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CourseTimes_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);

