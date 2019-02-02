CREATE TABLE [ContactInfo].[UserData] (
    [UserId]             INT		     NOT NULL,
    [IsDeleted]          BIT             NOT NULL,
    [CreationDate]       DATETIME        NOT NULL,
    [DeletionDate]       DATETIME        NULL,
    [Name]               NVARCHAR (25)   NULL,
    [Surname]            NVARCHAR (50)   NULL,
    [City]               NVARCHAR (150)  NULL,
    [Street]             NVARCHAR (150)  NULL,
    [HouseNumber]        NVARCHAR (50)   NULL,
    [HomeNumber]         NVARCHAR (50)   NULL,
    [PublicPhoneNumber]  VARCHAR (15)    NULL,
    [PrivatePhoneNumber] VARCHAR (15)    NULL,
    [EmailAdress]        VARCHAR (255)   NULL,
    [Comment]            NVARCHAR (1000) NULL,
    CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserData_UserLoginData] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id])
);

