﻿CREATE TABLE [Courses].[GroupTimes] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME CONSTRAINT [DF_GroupTimesCreationDate] DEFAULT (getdate()) NOT NULL,
    [DeletionDate] DATETIME NULL,
    [GroupId]      INT      NOT NULL,
    [StartTime]    INT      NOT NULL,
    [EndTime]      INT      NOT NULL,
    [DayOfWeekId]  INT      NOT NULL,
    CONSTRAINT [PK_GroupTimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupTimes_DictionaryItems] FOREIGN KEY ([DayOfWeekId]) REFERENCES [Administration].[DictionaryItems] ([Id]),
    CONSTRAINT [FK_GroupTimes_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id])
);







