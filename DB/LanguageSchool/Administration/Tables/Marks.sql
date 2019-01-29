CREATE TABLE [Administration].[Marks] (
    [Id]     INT           NOT NULL,
    [PLName] NVARCHAR (30) NOT NULL,
    [ENName] NVARCHAR (30) NULL,
    CONSTRAINT [PK_Marks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

