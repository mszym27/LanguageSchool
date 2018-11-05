CREATE TABLE [ContactInfo].[ContactRequests] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [ModificationDate] DATETIME        NULL,
    [PhoneNumber]      VARCHAR (15)    NULL,
    [EmailAdress]      VARCHAR (255)   NULL,
    [Comment]          NVARCHAR (1000) NULL,
    [Result]           NVARCHAR (1000) NULL,
    [IsAwaiting]       BIT             NOT NULL,
    CONSTRAINT [PK_ContactRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
);

