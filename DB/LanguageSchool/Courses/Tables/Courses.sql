CREATE TABLE [Courses].[Courses] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT             NOT NULL,
    [CreationDate] DATETIME        NOT NULL,
    [DeletionDate] DATETIME        NULL,
    [Name]         NVARCHAR (150)  NOT NULL,
    [Description]  NVARCHAR (4000) NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

