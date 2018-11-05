CREATE TABLE [Courses].[Materials] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT              NOT NULL,
    [CreationDate] DATETIME         NOT NULL,
    [DeletionDate] DATETIME         NULL,
    [CourseId]     INT              NOT NULL,
    [Name]         NVARCHAR (50)    NOT NULL,
    [File]         VARBINARY (4000) NULL,
    [Comment]      NVARCHAR (1000)  NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Materials_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);

