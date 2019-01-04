DECLARE @CourseId INT = (SELECT Id FROM [Courses].[Courses] WHERE Name = N'Intensywny kurs angielskiego')
	,@TeacherId INT = (SELECT Id FROM [Users].[Users] WHERE [Login] = N'BL\T_LL_0001')
	,@GroupId INT

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa pierwsza'
    ,1)

SET @GroupId = SCOPE_IDENTITY()

INSERT INTO [Users].[UsersGroups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[UserId]
    ,[GroupId])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@TeacherId
    ,@GroupId)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,6
    ,'8:00'
    ,'16:00'
    ,1)

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa druga'
    ,1)

SET @GroupId = SCOPE_IDENTITY()

INSERT INTO [Users].[UsersGroups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[UserId]
    ,[GroupId])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@TeacherId
    ,@GroupId)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,7
    ,'8:00'
    ,'16:00'
    ,1)

SET @CourseId = (SELECT Id FROM [Courses].[Courses] WHERE Name = N'Kurs przygotowujący do matury (I)')

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa pierwsza'
    ,1)

SET @GroupId = SCOPE_IDENTITY()

INSERT INTO [Users].[UsersGroups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[UserId]
    ,[GroupId])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@TeacherId
    ,@GroupId)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,1
    ,'16:00'
    ,'18:00'
    ,1)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,2
    ,'16:00'
    ,'18:00'
    ,1)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,3
    ,'16:00'
    ,'18:00'
    ,1)

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa druga'
    ,1)

SET @GroupId = SCOPE_IDENTITY()

INSERT INTO [Users].[UsersGroups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[UserId]
    ,[GroupId])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@TeacherId
    ,@GroupId)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,4
    ,'16:00'
    ,'20:00'
    ,1)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,5
    ,'18:00'
    ,'20:00'
    ,1)

SET @CourseId = (SELECT Id FROM [Courses].[Courses] WHERE Name = N'Intensywny kurs przygotowujący do matury')

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa pierwsza'
    ,1)

SET @GroupId = SCOPE_IDENTITY()

INSERT INTO [Users].[UsersGroups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[UserId]
    ,[GroupId])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@TeacherId
    ,@GroupId)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,6
    ,'16:00'
    ,'20:00'
    ,1)

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa druga'
    ,1)

INSERT INTO [Courses].[Groups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[CourseId]
    ,[Name]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,0
    ,@CourseId
    ,N'Grupa trzecia'
    ,1)

SET @GroupId = SCOPE_IDENTITY()

INSERT INTO [Users].[UsersGroups]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[UserId]
    ,[GroupId])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@TeacherId
    ,@GroupId)

INSERT INTO [Courses].[GroupTimes]
    ([IsDeleted]
    ,[CreationDate]
    ,[DeletionDate]
    ,[GroupId]
    ,[DayOfWeekId]
    ,[StartTime]
    ,[EndTime]
    ,[IsActive])
VALUES
    (0
    ,GETDATE()
    ,NULL
    ,@GroupId
    ,7
    ,'16:00'
    ,'20:00'
    ,1)