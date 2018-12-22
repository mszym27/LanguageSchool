CREATE TABLE [Administration].[Messages] (
    [Id]            INT             NOT NULL,
    [Header]        NVARCHAR (50)   NOT NULL,
    [Contents]      NVARCHAR (4000) NOT NULL,
    [MessageTypeId] INT             NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Messages_MessageTypes] FOREIGN KEY ([MessageTypeId]) REFERENCES [Administration].[MessageTypes] ([Id])
);

