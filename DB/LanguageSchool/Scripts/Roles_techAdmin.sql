IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 1)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (1 ,'Administrator')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 2)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (2 ,'Secretary')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 3)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (3 ,'Teacher')

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
		,'BL\techAdmin'
		,'123'
		,1)

IF NOT EXISTS(SELECT 1 FROM [Users].[Users] WHERE RoleId = 2)
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
		,'BL\S_DL'
		,'456'
		,2)

IF NOT EXISTS(SELECT 1 FROM [Users].[Users] WHERE RoleId = 3)
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
		,'BL\T_LL_0001'
		,'789'
		,3)

IF NOT EXISTS(SELECT 1 FROM [Users].[Users] WHERE RoleId = 4)
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
		,'BL\S_MS_0001'
		,'012'
		,4)