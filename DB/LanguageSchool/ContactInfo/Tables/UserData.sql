CREATE TABLE [ContactInfo].[UserData] (
    [UserId]             INT             NOT NULL,
    [IsDeleted]          BIT             NOT NULL,
    [CreationDate]       DATETIME        CONSTRAINT [DF_UserDataCreationDate] DEFAULT (getdate()) NOT NULL,
    [DeletionDate]       DATETIME        NULL,
    [Name]               NVARCHAR (25)   NOT NULL,
    [Surname]            NVARCHAR (50)   NOT NULL,
    [City]               NVARCHAR (150)  NULL,
    [Street]             NVARCHAR (150)  NULL,
    [HouseNumber]        NVARCHAR (50)   NULL,
    [HomeNumber]         NVARCHAR (50)   NULL,
    [PublicPhoneNumber]  VARCHAR (15)    NULL,
    [PrivatePhoneNumber] VARCHAR (15)    NULL,
    [EmailAdress]        VARCHAR (255)   NULL,
    [Comment]            NVARCHAR (1000) NULL,
    CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserData_Users] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);



