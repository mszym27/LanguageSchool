INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[LanguageProficencyId]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'IT language for professionals'
    ,N'Aimed at working proffesionals, offers an opportunity to learn and practice IT-specific terms. Structured in short, intensive lessons. Perfect for busy managers and key IT staff aimming to deepen their knowledge of the field language.'
    ,'2019-01-06'
	,'Six meetings - two hours every sunday evening'
	,6
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
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs przygotowujący do matury (I)'
    ,N'Przygotowany z myślą o uczniach dopiero zaczynających naukę języka. Powstał z myślą o uzupełnieniu lekcji pobieranych w trakcie normalnej nauki. Umożliwia pratykowanie języka oraz pogłębienie wiedzy w mniejszych grupach. Kontynuowany w kolejnych semestrach.'
    ,'2019-01-05'
	,'8 - 12 w soboty'
	,1
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
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs przygotowujący do matury (II)'
    ,N'Kontynuacja wcześniejszego kursu. Osoby które zapisują się na ten bez przejścia poprzedniego będą musiały przejść test ze znajomości języka.'
    ,'2019-01-05'
	,'2019-02-26'
	,2
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
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs przygotowujący do matury (III)'
    ,N'Kulminacja linii kursów przygotowujących oferowanych przez naszą szkołę. W ostatnim ich części uczniowie koncentrują się na rozwiązywaniu arkuszy maturalnych i przeprowadzaniu testów języka. Osoby zapisujące się od razu na ten kurs będą musiały uzyskać odpowiednią ocenę na wejściowym sprawdzianie.'
    ,'2019-05-02'
	,'10 - 14 w soboty'
	,3
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
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Intensywny kurs przygotowujący do matury'
    ,N'Skierowany do osób pragnących nabrać praktycznego doświadczenia w przechodzeniu testów maturalnych. Odbywa się pod okiem faktycznych egzaminatorów, celuje w jak najwierniejsze odwzorowanie warunków maturalnych.'
    ,'2019-05-03'
	,'Dwie godziny wieczorem w każdą niedzielę'
	,4
	,1)