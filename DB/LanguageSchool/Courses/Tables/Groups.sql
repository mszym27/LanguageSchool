CREATE TABLE [Courses].[Groups] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT            NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    [DeletionDate] DATETIME       NULL,
    [CourseId]     INT            NOT NULL,
    [Name]         NVARCHAR (150) NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [StartDate]    DATE           NOT NULL,
    [EndDate]      DATE           NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);



