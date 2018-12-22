﻿IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 1)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (1 ,'Administrator')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 2)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (2 ,'Secretary')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 3)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (3 ,'Teacher')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 4)
	INSERT INTO [Users].[Roles] ([Id],[Name]) VALUES (4 ,'Student')

GO

IF NOT EXISTS(SELECT 1 FROM [Administration].[LanguageProficencies] WHERE Id = 1)
	INSERT INTO [Administration].[LanguageProficencies]
		([Id]
		,[Name]
		,[PLDescription]
		,[ENDescription])
	VALUES
		(1
		,'A1'
		,'początkujący'
		,'beginner')

IF NOT EXISTS(SELECT 1 FROM [Administration].[LanguageProficencies] WHERE Id = 2)
	INSERT INTO [Administration].[LanguageProficencies]
		([Id]
		,[Name]
		,[PLDescription]
		,[ENDescription])
	VALUES
		(2
		,'A2'
		,'niższy średnio zaawansowany'
		,'elementary/pre-intermediate')

IF NOT EXISTS(SELECT 1 FROM [Administration].[LanguageProficencies] WHERE Id = 3)
	INSERT INTO [Administration].[LanguageProficencies]
		([Id]
		,[Name]
		,[PLDescription]
		,[ENDescription])
	VALUES
		(3
		,'B1'
		,'średnio zaawansowany'
		,'intermediate')

IF NOT EXISTS(SELECT 1 FROM [Administration].[LanguageProficencies] WHERE Id = 4)
	INSERT INTO [Administration].[LanguageProficencies]
		([Id]
		,[Name]
		,[PLDescription]
		,[ENDescription])
	VALUES
		(4
		,'B2'
		,'wyższy średnio zaawansowany'
		,'upper/post-intermediate')

IF NOT EXISTS(SELECT 1 FROM [Administration].[LanguageProficencies] WHERE Id = 5)
	INSERT INTO [Administration].[LanguageProficencies]
		([Id]
		,[Name]
		,[PLDescription]
		,[ENDescription])
	VALUES
		(5
		,'C1'
		,'zaawansowany'
		,'advanced')

IF NOT EXISTS(SELECT 1 FROM [Administration].[LanguageProficencies] WHERE Id = 6)
	INSERT INTO [Administration].[LanguageProficencies]
		([Id]
		,[Name]
		,[PLDescription]
		,[ENDescription])
	VALUES
		(6
		,'C2'
		,'profesjonalny'
		,'nearly native-speaker level')

GO

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 1)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(1
		,'Poniedziałek'
		,'Monday')

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 2)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(2
		,'Wtorek'
		,'Tuesday')

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 3)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(3
		,'Środa'
		,'Wednesday')

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 4)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(4
		,'Czwartek'
		,'Thursday')

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 5)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(5
		,'Piątek'
		,'Friday')

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 6)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(6
		,'Sobota'
		,'Saturday')

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 7)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(7
		,'Niedziela'
		,'Sunday')

GO

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 1)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(1
		,N'Do konkretnego użytkownika')

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 2)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(2
		,N'Do członków grupy')

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 3)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(3
		,N'Do wszystkich uczestników kursu')

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 4)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(4
		,N'Do wszystkich')

GO

DECLARE @Now DATETIME = GETDATE()

IF NOT EXISTS(SELECT 1 FROM [Administration].[Holidays] WHERE [Date] = '2018-12-24')
	INSERT INTO [Administration].[Holidays]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[Name]
		,[Date]
		,[IsActive])
	VALUES
		(0
		,@Now
		,NULL
		,'Wigilia'
		,'2018-12-24'
		,1)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Holidays] WHERE [Date] = '2018-12-25')
	INSERT INTO [Administration].[Holidays]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[Name]
		,[Date]
		,[IsActive])
	VALUES
		(0
		,@Now
		,NULL
		,'Pierwszy dzień świąt'
		,'2018-12-25'
		,1)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Holidays] WHERE [Date] = '2018-12-26')
	INSERT INTO [Administration].[Holidays]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[Name]
		,[Date]
		,[IsActive])
	VALUES
		(0
		,@Now
		,NULL
		,'Drugi dzień świąt'
		,'2018-12-26'
		,1)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Holidays] WHERE [Date] = '2019-01-01')
	INSERT INTO [Administration].[Holidays]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[Name]
		,[Date]
		,[IsActive])
	VALUES
		(0
		,@Now
		,NULL
		,'Nowy rok'
		,'2019-01-01'
		,1)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Holidays] WHERE [Date] = '2019-01-06')
	INSERT INTO [Administration].[Holidays]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[Name]
		,[Date]
		,[IsActive])
	VALUES
		(0
		,@Now
		,NULL
		,'Trzech króli'
		,'2019-01-06'
		,1)

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