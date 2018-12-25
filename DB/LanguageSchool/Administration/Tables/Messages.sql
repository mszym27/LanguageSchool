CREATE TABLE [Administration].[Messages] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]  DATETIME        NOT NULL,
    [Header]        NVARCHAR (50)   NOT NULL,
    [Contents]      NVARCHAR (4000) NOT NULL,
    [MessageTypeId] INT             NOT NULL,
    [UserId]        INT             NULL,
    [GroupId]       INT             NULL,
    [CourseId]      INT             NULL,
    [RoleId]        INT             NULL,
    [IsSystem]      BIT             NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Messages_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id]),
    CONSTRAINT [FK_Messages_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Courses].[Groups] ([Id]),
    CONSTRAINT [FK_Messages_MessageTypes] FOREIGN KEY ([MessageTypeId]) REFERENCES [Administration].[MessageTypes] ([Id]),
    CONSTRAINT [FK_Messages_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Users].[Roles] ([Id]),
    CONSTRAINT [FK_Messages_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);







