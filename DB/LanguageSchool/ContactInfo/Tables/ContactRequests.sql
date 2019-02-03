CREATE TABLE [ContactInfo].[ContactRequests] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [CreationDate]       DATETIME        CONSTRAINT [DF_ContactRequestsCreationDate] DEFAULT (getdate()) NOT NULL,
    [ModificationDate]   DATETIME        NULL,
    [Name]               VARCHAR (25)    NOT NULL,
    [Surname]            NVARCHAR (50)   NULL,
    [PhoneNumber]        VARCHAR (15)    NOT NULL,
    [EmailAdress]        VARCHAR (255)   NULL,
    [CourseId]           INT             NOT NULL,
    [PreferredHoursFrom] INT             NOT NULL,
    [PreferredHoursTo]   INT             NOT NULL,
    [Comment]            NVARCHAR (1000) NULL,
    [IsAwaiting]         BIT             NOT NULL,
    CONSTRAINT [PK_ContactRequests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContactRequests_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses].[Courses] ([Id])
);




























GO




