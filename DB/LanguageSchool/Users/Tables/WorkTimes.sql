CREATE TABLE [Users].[WorkTimes] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [DeletionDate] DATETIME NULL,
    [UserId]       INT      NOT NULL,
    [DayOfWeekId]  INT      NOT NULL,
    [StartTime]    TIME (7) NOT NULL,
    [EndTime]      TIME (7) NULL,
    [IsActive]     BIT      NOT NULL,
    CONSTRAINT [PK_WorkTimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkTimes_DaysOfWeek] FOREIGN KEY ([DayOfWeekId]) REFERENCES [Administration].[DaysOfWeek] ([Id]),
    CONSTRAINT [FK_WorkTimes_Groups] FOREIGN KEY ([UserId]) REFERENCES [Courses].[Groups] ([Id])
);

