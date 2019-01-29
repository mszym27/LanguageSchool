IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 1)
	INSERT INTO [Users].[Roles] ([Id],[ENName],[PLName]) VALUES (1,'Admin','Administrator')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 2)
	INSERT INTO [Users].[Roles] ([Id],[ENName],[PLName]) VALUES (2,'Secretary','Sekretariat')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 3)
	INSERT INTO [Users].[Roles] ([Id],[ENName],[PLName]) VALUES (3,'Teacher','Nauczyciel')

IF NOT EXISTS(SELECT 1 FROM [Users].[Roles] WHERE Id = 4)
	INSERT INTO [Users].[Roles] ([Id],[ENName],[PLName]) VALUES (4,'Student','Student')

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

IF NOT EXISTS(SELECT 1 FROM [Administration].[Marks] WHERE Id = 1)
	INSERT INTO [Administration].[Marks]
		([Id]
		,[PLName]
		,[ENName])
	VALUES
		(1
		,N'niedostateczny'
		,NULL)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Marks] WHERE Id = 2)
	INSERT INTO [Administration].[Marks]
		([Id]
		,[PLName]
		,[ENName])
	VALUES
		(2
		,N'dopuszczający'
		,NULL)
		
IF NOT EXISTS(SELECT 1 FROM [Administration].[Marks] WHERE Id = 3)
	INSERT INTO [Administration].[Marks]
		([Id]
		,[PLName]
		,[ENName])
	VALUES
		(3
		,N'dostateczny'
		,NULL)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Marks] WHERE Id = 4)
	INSERT INTO [Administration].[Marks]
		([Id]
		,[PLName]
		,[ENName])
	VALUES
		(4
		,N'dobry'
		,NULL)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Marks] WHERE Id = 5)
	INSERT INTO [Administration].[Marks]
		([Id]
		,[PLName]
		,[ENName])
	VALUES
		(5
		,N'bardzo dobry'
		,NULL)

IF NOT EXISTS(SELECT 1 FROM [Administration].[Marks] WHERE Id = 6)
	INSERT INTO [Administration].[Marks]
		([Id]
		,[PLName]
		,[ENName])
	VALUES
		(6
		,N'celujący'
		,NULL)

GO

IF NOT EXISTS (SELECT 1 FROM Administration.DaysOfWeek WHERE Id = 1)
	INSERT INTO Administration.DaysOfWeek 
		(Id
		,PLName
		,ENName)
	VALUES 
		(1
		,N'Poniedziałek'
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
		,N'Środa'
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
		,N'Piątek'
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
		,N'Do uczestników kursu')

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 4)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(4
		,N'Do wszystkich użytkowników o roli')

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 5)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(5
		,N'Do wszystkich')

IF NOT EXISTS(SELECT 1 FROM [Administration].[MessageTypes] WHERE Id = 6)
	INSERT INTO [Administration].[MessageTypes]
		([Id]
		,[Name])
	VALUES
		(6
		,N'Wiadomość powitalna dla studentów')

GO

IF NOT EXISTS(SELECT 1 FROM [Administration].[Messages] WHERE [MessageTypeId] = 6)
INSERT [Administration].[Messages] ([CreationDate], [Header], [Contents], [MessageTypeId], [UserId], [GroupId], [CourseId], [RoleId], [IsSystem]) 
VALUES (GETDATE(), N'Witaj w szkole!', N'Miło nam Cię powitać w naszej szkole.
W lewym górnym rogu tej strony zobaczysz logo. Od momentu w którym odczytałeś ten komunikat po kliknięciu na nie zostaniesz przeniesiony na podgląd swojego planu zajęć.
Jeśli w przyszłości otrzymasz nowe komunikaty to po zalogowaniu automatycznie zobaczysz listę swoich wiadomości. Rozkład zajęć stanie się dla Ciebie z powrotem dostępny po tym jak zapoznasz się z treścią tych które otrzymałeś.
Cały masz też możliwość zobaczenia historii komunikatów - jest ona dostępna po kliknięciu na przypisany do Ciebie login.', 6, NULL, NULL, NULL, 4, 1)

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
		,'BL\S_DL_00211'
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
		,'S_MS_0001'
		,'012'
		,4)

GO

UPDATE [Users].[Users] SET [Password] = N'uBmnT9Q+rFZkSOwOBRNFOQ==' WHERE [Login] = N'techAdmin'
UPDATE [Users].[Users] SET [Password] = N'tnp38KG17HWhMRQcRf16tQ==' WHERE [Login] = N'BL\S_DL'
UPDATE [Users].[Users] SET [Password] = N'yMWW2EWUj09FY7Wd005AtQ==' WHERE [Login] = N'BL\T_LL_0001'
UPDATE [Users].[Users] SET [Password] = N'W0AYXVFZdcO+2d32/7eIyw==' WHERE [Login] = N'S_MS_0001'

GO

DECLARE @userId INT = (SELECT Id FROM [Users].[Users] WHERE [Login] = N'S_MS_0001')

IF NOT EXISTS(SELECT Id FROM [ContactInfo].[UserData] WHERE UserId = @userId)
	INSERT INTO [ContactInfo].[UserData]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[UserId]
		,[Name]
		,[Surname]
		,[City]
		,[Street]
		,[HouseNumber]
		,[HomeNumber]
		,[PublicPhoneNumber]
		,[PrivatePhoneNumber]
		,[EmailAdress]
		,[Comment])
	VALUES
		(0
		,GETDATE()
		,NULL
		,@userId
		,N'Mikołaj'
		,N'Szumigaj'
		,NULL
		,NULL
		,NULL
		,NULL
		,'990-354-123'
		,NULL
		,NULL
		,N'Domyślnie obecny w systemie student')

SET @userId = (SELECT Id FROM [Users].[Users] WHERE [Login] = N'BL\T_LL_0001')

IF NOT EXISTS(SELECT Id FROM [ContactInfo].[UserData] WHERE UserId = @userId)
	INSERT INTO [ContactInfo].[UserData]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[UserId]
		,[Name]
		,[Surname]
		,[City]
		,[Street]
		,[HouseNumber]
		,[HomeNumber]
		,[PublicPhoneNumber]
		,[PrivatePhoneNumber]
		,[EmailAdress]
		,[Comment])
	VALUES
		(0
		,GETDATE()
		,NULL
		,@userId
		,N'Łukasz'
		,N'Łączka'
		,NULL
		,NULL
		,NULL
		,NULL
		,'990-354-123'
		,NULL
		,NULL
		,N'Domyślnie obecny w systemie nauczyciel')

SET @userId = (SELECT Id FROM [Users].[Users] WHERE [Login] = N'BL\S_DL')

IF NOT EXISTS(SELECT Id FROM [ContactInfo].[UserData] WHERE UserId = @userId)
	INSERT INTO [ContactInfo].[UserData]
		([IsDeleted]
		,[CreationDate]
		,[DeletionDate]
		,[UserId]
		,[Name]
		,[Surname]
		,[City]
		,[Street]
		,[HouseNumber]
		,[HomeNumber]
		,[PublicPhoneNumber]
		,[PrivatePhoneNumber]
		,[EmailAdress]
		,[Comment])
	VALUES
		(0
		,GETDATE()
		,NULL
		,@userId
		,N'Dominik'
		,N'Lasocki'
		,NULL
		,NULL
		,NULL
		,NULL
		,'990-354-123'
		,NULL
		,NULL
		,N'Domyślnie obecny w systemie członek sekretariatu')

GO

UPDATE [Tests].[ClosedQuestions] SET [LessonSubjectId] = NULL -- TODO

GO