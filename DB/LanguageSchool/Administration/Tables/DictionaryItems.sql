CREATE TABLE [Administration].[DictionaryItems] (
    [Id]            INT            NOT NULL,
    [PLName]        NVARCHAR (50)  NOT NULL,
    [ENName]        NVARCHAR (50)  NOT NULL,
    [PLDescription] NVARCHAR (250) NULL,
    [ENDescription] NVARCHAR (250) NULL,
    CONSTRAINT [PK_DictionaryItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);







