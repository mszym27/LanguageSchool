CREATE TABLE [Users].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT            NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    [DeletionDate] DATETIME       NULL,
    [Login]        NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (100) NOT NULL,
    [RoleId]       INT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Users].[Roles] ([Id])
);

