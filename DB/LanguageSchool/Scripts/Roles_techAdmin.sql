INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (1 ,'Administrator')
INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (2 ,'Sekretariat')
INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (3 ,'Nauczyciel')
INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (4 ,'Student')

GO

DECLARE @Now DATETIME = GETDATE()

INSERT INTO [Users].[Users]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Login]
    ,[Password]
    ,[RoleId])
VALUES
    (0
    ,@Now
    ,NULL
    ,'techAdmin'
    ,'123' -- do zmiany przez uzytkownika
    ,1)