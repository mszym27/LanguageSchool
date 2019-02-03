CREATE TABLE [Courses].[Courses] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]         DATETIME        CONSTRAINT [DF_CoursesCreationDate] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]            BIT             NOT NULL,
    [DeletionDate]         DATETIME        NULL,
    [IsActive]             BIT             NOT NULL,
    [Name]                 NVARCHAR (150)  NOT NULL,
    [Description]          NVARCHAR (4000) NULL,
    [StartDate]            DATE            NOT NULL,
    [EndDate]              DATE            NOT NULL,
    [NumberOfHours]        NVARCHAR (200)  NOT NULL,
    [LanguageProficencyId] INT             NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_LanguageProficencies] FOREIGN KEY ([LanguageProficencyId]) REFERENCES [Administration].[DictionaryItems] ([Id])
);















