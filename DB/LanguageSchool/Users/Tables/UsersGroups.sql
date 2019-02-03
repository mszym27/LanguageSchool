CREATE TABLE [Users].[UsersGroups] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME CONSTRAINT [DF_UsersGroupsCreationDate] DEFAULT (getdate()) NOT NULL,
    [DeletionDate] DATETIME NULL,
    [UserId]       INT      NOT NULL,
    [GroupId]      INT      NOT NULL,
    CONSTRAINT [PK_UsersGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersGroups_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id]),
    CONSTRAINT [FK_UsersGroups_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);



