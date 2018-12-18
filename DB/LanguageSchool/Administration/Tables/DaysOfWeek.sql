CREATE TABLE [Administration].[DaysOfWeek] (
    [Id]     INT           NOT NULL,
    [PLName] NVARCHAR (30) NOT NULL,
    [ENName] NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_DaysOfWeek] PRIMARY KEY CLUSTERED ([Id] ASC)
);

