CREATE TABLE [Users].[UsersMessages] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [IsDeleted]       BIT      NOT NULL,
    [DeletionDate]    DATETIME NULL,
    [UserId]          INT      NOT NULL,
    [MessageId]       INT      NOT NULL,
    [HasBeenReceived] BIT      NOT NULL,
    [ReceivedDate]    DATETIME NULL,
    CONSTRAINT [PK_UsersMessages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersMessages_Messages] FOREIGN KEY ([MessageId]) REFERENCES [Administration].[Messages] ([Id]),
    CONSTRAINT [FK_UsersMessages_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);





