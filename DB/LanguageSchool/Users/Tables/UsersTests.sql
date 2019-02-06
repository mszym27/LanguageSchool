CREATE TABLE [Users].[UsersTests] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [CreationDate] DATETIME CONSTRAINT [DF_UsersTestsCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    [DeletionDate] DATETIME NULL,
    [Points]       INT      NOT NULL,
    [IsMarked]     BIT      NOT NULL,
    [MarkId]       INT      NOT NULL,
    [UserId]       INT      NOT NULL,
    [TestId]       INT      NOT NULL,
    [GroupId]      INT      NOT NULL,
    CONSTRAINT [PK_UsersTests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersTests_DictionaryItems] FOREIGN KEY ([MarkId]) REFERENCES [Administration].[DictionaryItems] ([Id]),
    CONSTRAINT [FK_UsersTests_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id]),
    CONSTRAINT [FK_UsersTests_Tests] FOREIGN KEY ([TestId]) REFERENCES [Tests].[Tests] ([Id]),
    CONSTRAINT [FK_UsersTests_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);



















