-- role

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (1001, N'Secretary', N'Sekretariat')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (1002, N'Teacher', N'Nauczyciel')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (1003, N'Student', N'Student')

-- typy wiadomości

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2001, N'', N'Do konkretnego użytkownika')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2002, N'', N'Do członków grupy')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2003, N'', N'Do uczestników kursu')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2004, N'', N'Do wszystkich użytkowników o roli')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2005, N'', N'Do wszystkich')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2006, N'', N'Wiadomość powitalna dla studentów')

-- oceny

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3001, '', N'niedostateczny')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3002, '', N'dopuszczający')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3003, '', N'dostateczny')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3004, '', N'dobry')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3005, '', N'bardzo dobry')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3006, '', N'celujący')

-- poziomy zaawansowania kursów

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4001, N'A1', N'A1', N'początkujący', N'beginner')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4002, N'A2', N'A2', N'niższy srednio zaawansowany', N'elementary/pre-intermediate')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4003, N'B1', N'B1', N'średnio zaawansowany', N'intermediate')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4004, N'B2', N'B2', N'wyższy srednio zaawansowany', N'upper/post-intermediate')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4005, N'C1', N'C1', N'zaawansowany', N'advanced')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4006, N'C2', N'C2', N'profesjonalny', N'nearly native-speaker level')

-- dni tygodnia

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5001, N'Monday', N'Poniedziałek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5002, N'Tuesday', N'Wtorek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5003, N'Wednesday', N'Środa')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5004, N'Thursday', N'Czwartek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5005, N'Friday', N'Piątek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5006, N'Saturday', N'Sobota')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5007, N'Sunday', N'Niedziela')

GO

-- wiadomość powitalna

INSERT [Administration].[Messages] ([CreationDate], [Header], [Contents], [MessageTypeId], [UserId], [GroupId], [CourseId], [RoleId], [IsSystem]) 
VALUES (GETDATE(), N'Witaj w szkole!', N'Miło nam Cię powitać w naszej szkole.<br><br>W lewym górnym rogu tej strony zobaczysz logo. Od momentu w którym odczytałeś ten komunikat po kliknięciu na nie zostaniesz przeniesiony na podgląd swojego planu zajęć.<br>Jeśli w przyszłości otrzymasz nowe komunikaty to po zalogowaniu automatycznie zobaczysz listę swoich wiadomości. Rozkład zajęć stanie się dla Ciebie z powrotem dostępny po tym jak zapoznasz się z treścią tych które otrzymałeś.<br><br>Cały masz też możliwość zobaczenia historii komunikatów - jest ona dostępna po kliknięciu na przypisany do Ciebie login.', 2006, NULL, NULL, NULL, 1003, 1)

GO

-- przykładowi użytkownicy

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
	,'BL\S_DL_00211'
	,N'tnp38KG17HWhMRQcRf16tQ==' -- 456
	,1001) -- Sekretariat

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
	,'BL\T_PP_00738'
	,N'yMWW2EWUj09FY7Wd005AtQ==' -- 789
	,1002) -- Nauczyciel

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
	,'S_HC_04636'
	,N'W0AYXVFZdcO+2d32/7eIyw==' -- 012
	,1003) -- Student

GO

DECLARE @userId INT = (SELECT Id FROM [Users].[Users] WHERE [Login] = N'S_HC_04636')

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
	,N'Hubert'
	,N'Cieślar'
	,N'Łódź'
	,N'Bednarskiego'
	,N'82'
	,N'21'
	,'990-354-123'
	,NULL
	,N'hubert.cies2@poczta.onet.pl'
	,N'Domyślnie obecny w systemie student')

SET @userId = (SELECT Id FROM [Users].[Users] WHERE [Login] = N'BL\T_PP_00738')

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
	,N'Przemysława'
	,N'Pająk'
	,NULL
	,NULL
	,NULL
	,NULL
	,'990-356-223'
	,'990-314-157'
	,N'przemyslawa.pajak33@gmail.com.pl'
	,N'Domyślnie obecny w systemie nauczyciel')

GO

-- kursy

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[LanguageProficencyId]
	,EndDate
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'IT language for professionals'
    ,N'Aimed at working proffesionals, offers an opportunity to learn and practice IT-specific terms. Structured in short, intensive lessons. Perfect for busy managers and key IT staff aimming to deepen their knowledge of the field language.'
    ,'2019-01-06'
	,'Six meetings - two hours every sunday evening'
	,4006
	,'2019-01-10'
	,1)

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[LanguageProficencyId]
	,EndDate
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs przygotowujący do matury (I)'
    ,N'Przygotowany z myślą o uczniach dopiero zaczynających naukę języka. Powstał z myślą o uzupełnieniu lekcji pobieranych w trakcie normalnej nauki. Umożliwia pratykowanie języka oraz pogłębienie wiedzy w mniejszych grupach. Kontynuowany w kolejnych semestrach.'
    ,'2019-01-05'
	,'8 - 12 w soboty'
	,4001
	,'2019-02-16'
	,1)

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[LanguageProficencyId]
	,EndDate
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs przygotowujący do matury (II)'
    ,N'Kontynuacja wcześniejszego kursu. Osoby które zapisują się na ten bez przejścia poprzedniego będą musiały przejść test ze znajomości języka.'
	,'2019-02-23'
	,'8 - 12 w soboty'
	,4002
	,'2019-03-30'
	,1)

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[LanguageProficencyId]
	,EndDate
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs przygotowujący do matury (III)'
    ,N'Kulminacja linii kursów przygotowujących oferowanych przez naszą szkołę. W ostatnim ich części uczniowie koncentrują się na rozwiązywaniu arkuszy maturalnych i przeprowadzaniu testów języka. Osoby zapisujące się od razu na ten kurs będą musiały uzyskać odpowiednią ocenę na wejściowym sprawdzianie.'
    ,'2019-04-02'
	,'10 - 14 w soboty'
	,4003
	,'2019-05-04'
	,1)

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[LanguageProficencyId]
	,EndDate
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Intensywny kurs przygotowujący do matury'
    ,N'Skierowany do osób pragnących nabrać praktycznego doświadczenia w przechodzeniu testów maturalnych. Odbywa się pod okiem faktycznych egzaminatorów, celuje w jak najwierniejsze odwzorowanie warunków maturalnych.'
    ,'2019-04-02'
	,'Dwie godziny wieczorem w każdą sobotę i niedzielę'
	,4004
	,'2019-05-04'
	,1)