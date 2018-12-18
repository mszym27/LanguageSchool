CREATE TABLE [Administration].[LanguageProficencies] (
    [Id]            INT           NOT NULL,
    [Name]          VARCHAR (2)   NOT NULL,
    [PLDescription] NVARCHAR (50) NOT NULL,
    [ENDescription] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LanguageProficencies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

