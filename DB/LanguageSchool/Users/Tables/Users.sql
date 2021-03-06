﻿CREATE TABLE [Users].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT            NOT NULL,
    [CreationDate] DATETIME       CONSTRAINT [DF_UsersCreationDate] DEFAULT (getdate()) NOT NULL,
    [DeletionDate] DATETIME       NULL,
    [Login]        NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (100) NOT NULL,
    [RoleId]       INT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_DictionaryItems] FOREIGN KEY ([RoleId]) REFERENCES [Administration].[DictionaryItems] ([Id])
);





