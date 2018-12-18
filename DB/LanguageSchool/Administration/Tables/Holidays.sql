CREATE TABLE [Administration].[Holidays] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IsDeleted]    BIT            NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    [DeletionDate] DATETIME       NULL,
    [Name]         NVARCHAR (150) NOT NULL,
    [Date]         DATE           NOT NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_Holidays] PRIMARY KEY CLUSTERED ([Id] ASC)
);

