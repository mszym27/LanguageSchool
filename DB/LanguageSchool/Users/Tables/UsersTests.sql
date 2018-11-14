CREATE TABLE [Users].[UsersTests] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [DeletionDate] DATETIME NULL,
    [UserId]       INT      NOT NULL,
    [TestId]       INT      NOT NULL,
    [Points]       INT      NULL,
    [IsMarked]     BIT      NOT NULL,
    CONSTRAINT [PK_UsersTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersTests_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id]),
    CONSTRAINT [FK_UsersTests_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);









