CREATE TABLE [Courses].[GroupTimes] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [DeletionDate] DATETIME NULL,
    [GroupId]      INT      NOT NULL,
    [DayOfWeekId]  INT      NOT NULL,
    [StartTime]    INT      NOT NULL,
    [EndTime]      INT      NOT NULL,
    [IsActive]     BIT      NOT NULL,
    CONSTRAINT [PK_GroupTimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupTimes_DaysOfWeek] FOREIGN KEY ([DayOfWeekId]) REFERENCES [Administration].[DaysOfWeek] ([Id]),
    CONSTRAINT [FK_GroupTimes_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id])
);



