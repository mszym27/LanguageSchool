CREATE TABLE [Users].[Messages] (
    [Id]       INT             NOT NULL,
    [Header]   NVARCHAR (50)   NOT NULL,
    [Contents] NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

