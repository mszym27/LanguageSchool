DECLARE @CourseId INT
	,@TestId INT
	,@LessonSubjectId INT
	,@QuestionId INT

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
	,[StartDate]
	,[NumberOfHours]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Kurs angielskiego dla małoletnich'
    ,N'Skierowany do dzieci w wieku 6 - 10 lat, oferuje możliwość nauki przez zabawę. Jego celem wspieranie efektów i uzupełnianie szkolnych lekcji pobieranych w cyklu normalnej edukacji. W jego ramach udostępniany jest szereg atrakcyjnych pomocy naukowych oraz możliwość tzw. ,,nauki w ruchu"'
    ,DATEADD(DAY, 7,GETDATE())
	,'Dwa spotkania w godziach południowych, w co drugi weekend'
	,1)

SET @CourseId = SCOPE_IDENTITY()

INSERT INTO [Courses].[LessonSubjects]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[Description]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,N'Nazwy zwierząt'
    ,NULL
    ,1)

SET @LessonSubjectId = SCOPE_IDENTITY()

INSERT INTO [Tests].[Tests]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[Comment]
    ,[NumberOfQuestions]
    ,[NumberOfOpenQuestions]
    ,[NumberOfClosedQuestions]
    ,[Points]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,N'Przykładowy test dotyczący nazw zwierząt'
    ,NULL
    ,3
    ,0
    ,3
    ,3
    ,1)
	
SET @TestId = SCOPE_IDENTITY()

INSERT INTO [Courses].[EntryTests]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[TestId]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,@TestId
    ,1)

INSERT INTO [Tests].[TestsLessonSubjects]
    ([TestId]
    ,[LessonSubjectId])
VALUES
    (@TestId
    ,@LessonSubjectId)

INSERT INTO [Tests].[ClosedQuestions]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[LessonSubjectId]
    ,[Contents]
    ,[NumberOfPossibleAnswers]
    ,[IsMultichoice]
    ,[Points])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@LessonSubjectId
    ,N'Wskaż angielskie nazwy dużych kotów'
    ,4
    ,1
    ,1)
	
SET @QuestionId = SCOPE_IDENTITY()

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Octopus'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (1
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Jaguar'
    ,1)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Housecat'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Lion'
    ,1)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Camel'
    ,0)

INSERT INTO [Tests].[ClosedQuestions]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[LessonSubjectId]
    ,[Contents]
    ,[NumberOfPossibleAnswers]
    ,[IsMultichoice]
    ,[Points])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@LessonSubjectId
    ,N'Przetłumacz ''Ośmiornica'' na angielski'
    ,3
    ,0
    ,1)
	
SET @QuestionId = SCOPE_IDENTITY()

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Octopus'
    ,1)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (1
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Cat'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Camel'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (1
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Dog'
    ,0)

INSERT INTO [Tests].[ClosedQuestions]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[LessonSubjectId]
    ,[Contents]
    ,[NumberOfPossibleAnswers]
    ,[IsMultichoice]
    ,[Points])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@LessonSubjectId
    ,N'Przetłumacz ''Camel'' na polski'
    ,3
    ,0
    ,1)
	
SET @QuestionId = SCOPE_IDENTITY()

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Kot'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Pies'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Wielbłąd'
    ,1)

GO

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
    ,StartDate
	,NumberOfHours
	,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Intensywny kurs angielskiego'
    ,N'Stworzony z myślą o osobach ze słabą praktyczną znajomością języka. Zorientowany jest na szybkie uzupełnienie ich zdolności poprzez bezpośrednie konwersacje.'
    ,DATEADD(DAY, 7, GETDATE())
	,N'Dwa ośmiogodzinne spotkania w sobotę i niedzielę'
	,0)

GO

DECLARE @CourseId INT
	,@TestId INT
	,@LessonSubjectId1 INT
	,@LessonSubjectId2 INT
	,@QuestionId INT

INSERT INTO [Courses].[Courses]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[Name]
    ,[Description]
    ,StartDate
	,NumberOfHours
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,N'Advanced english (B1)'
    ,N'Aimed at working professionals, offers an opportunity to deepen their knowledge and practice english in conversations. Structured in weekly meetings organized on a flexible schedule, offering opportunity to easily combine work and learning process.'
     ,DATEADD(DAY, 7, GETDATE())
	,N'Every wednesday evening'
	,1)

SET @CourseId = SCOPE_IDENTITY()

INSERT INTO [Courses].[LessonSubjects]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[Description]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,N'Past Simple'
    ,NULL
    ,1)

SET @LessonSubjectId1 = SCOPE_IDENTITY()

INSERT INTO [Courses].[LessonSubjects]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[Description]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,N'Past Continuous'
    ,NULL
    ,1)

SET @LessonSubjectId2 = SCOPE_IDENTITY()

INSERT INTO [Tests].[Tests]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[Comment]
    ,[NumberOfQuestions]
    ,[NumberOfOpenQuestions]
    ,[NumberOfClosedQuestions]
    ,[Points]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,N'Past tenses - sample test'
    ,NULL
    ,2
    ,0
    ,2
    ,2
    ,1)
	
SET @TestId = SCOPE_IDENTITY()

INSERT INTO [Courses].[EntryTests]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[TestId]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@CourseId
    ,@TestId
    ,1)

INSERT INTO [Tests].[TestsLessonSubjects]
    ([TestId]
    ,[LessonSubjectId])
VALUES
    (@TestId
    ,@LessonSubjectId1)

INSERT INTO [Tests].[TestsLessonSubjects]
    ([TestId]
    ,[LessonSubjectId])
VALUES
    (@TestId
    ,@LessonSubjectId2)

INSERT INTO [Tests].[ClosedQuestions]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[LessonSubjectId]
    ,[Contents]
    ,[NumberOfPossibleAnswers]
    ,[IsMultichoice]
    ,[Points])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@LessonSubjectId1
    ,N'Translate ''Piotr ją zobaczył '' to english'
    ,3
    ,0
    ,1)
	
SET @QuestionId = SCOPE_IDENTITY()

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Piotr saw her'
    ,1)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Piotr watched her'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Piotr did see her'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'Piotr see her'
    ,0)

INSERT INTO [Tests].[ClosedQuestions]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[LessonSubjectId]
    ,[Contents]
    ,[NumberOfPossibleAnswers]
    ,[IsMultichoice]
    ,[Points])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@LessonSubjectId2
    ,N'Translate ''Ona była obserwowana przez Piotra'' to english'
    ,2
    ,0
    ,1)
	
SET @QuestionId = SCOPE_IDENTITY()

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'She was being watched by Piotr'
    ,1)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'She was watched by Piotr'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'She has been watched by Piotr'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (1
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'She is being watching by Piotr'
    ,0)

INSERT INTO [Tests].[Answers]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[ClosedQuestionId]
    ,[Answer]
    ,[IsCorrect])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@QuestionId
    ,N'She is watching by Piotr'
    ,0)