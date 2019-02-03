CREATE TABLE [Administration].[DictionaryItems] (
    [Id]             INT            NOT NULL,
    [PLName]         NVARCHAR (30)  NOT NULL,
    [ENName]         NVARCHAR (30)  NOT NULL,
    [PLDescription]  NVARCHAR (250) NULL,
    [ENDDescription] NVARCHAR (250) NULL,
    CONSTRAINT [PK_DictionaryItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);





