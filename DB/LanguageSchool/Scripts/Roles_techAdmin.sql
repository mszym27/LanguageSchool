IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 1)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (1 ,'Administrator')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 2)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (2 ,'Sekretariat')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 3)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (3 ,'Nauczyciel')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 4)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (4 ,'Student')

GO

DECLARE @Now DATETIME = GETDATE()

IF NOT EXISTS(SELECT 1 FROM [Users].[Users] WHERE RoleId = 1)
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