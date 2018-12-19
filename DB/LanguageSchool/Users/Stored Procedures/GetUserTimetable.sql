CREATE PROCEDURE [Users].[GetUserTimetable]
@UserId INT
,@DayId INT
AS
BEGIN
	DECLARE @WorkingHours TABLE ([Hour] INT)
	
	DECLARE @i INT = 8

	WHILE (@i <= 20)
	BEGIN
		INSERT INTO @WorkingHours SELECT @i

		SET @i += 1
	END

	DECLARE @CurrentDate DATETIME = GETDATE(),
		@StartOfWeek DATE,
		@DayDate DATE

	SET @StartOfWeek = @CurrentDate - DATEPART(dw, @CurrentDate) + 1

	SET @DayDate = DATEADD(DAY, @DayId, @StartOfWeek)

	IF(@DayDate NOT IN (SELECT Date FROM [Administration].[Holidays]))
		SELECT 
			WH.Hour,
			ScheduledCourse.[CourseId],
			ScheduledCourse.Name
		FROM @WorkingHours WH
		JOIN [Administration].[DaysOfWeek] DOW
			ON DOW.Id = @DayId
		OUTER APPLY (
			SELECT temp.[CourseId],
				temp.Name
			FROM (
				SELECT C.Id AS [CourseId],
					C.Name,
					DATEPART(HOUR, CT.[StartTime]) CourseStart,
					DATEPART(HOUR, CT.[EndTime]) CourseEnd
				FROM [Courses].[CourseTimes] CT
				JOIN [Courses].[Courses] C
					ON C.Id = CT.CourseId
					AND C.IsDeleted = 0
				JOIN [Users].[UsersCourses] UC
					ON UC.CourseId = C.Id
					AND UC.IsDeleted = 0
				WHERE UC.UserId = @UserId
			) temp
				WHERE (CourseStart <= WH.Hour) AND (WH.Hour < CourseEnd)
		) ScheduledCourse
	ELSE
		SELECT 
			WH.Hour,
			NULL,
			H.Name
		FROM @WorkingHours WH
		JOIN [Administration].[Holidays] H
			ON H.Date = @DayDate

--	DECLARE @UserId INT = 3

--	DECLARE @WorkingHours TABLE ([Hour] INT)
	
--	DECLARE @i INT = 8

--	WHILE (@i <= 20)
--	BEGIN
--		INSERT INTO @WorkingHours SELECT @i

--		SET @i += 1
--	END

--	DECLARE @CurrentDate DATETIME = GETDATE(),
--		@StartOfWeek DATE

--	SET @StartOfWeek = @CurrentDate - DATEPART(dw, @CurrentDate) + 1

--	DECLARE @CurrentWeek TABLE (WeekDay INT, DayDate DATE)

--	INSERT INTO @CurrentWeek 
--		(WeekDay, 
--		 DayDate)
--	SELECT 
--		[Id], 
--		DATEADD(DAY, [Id], @StartOfWeek)
--	FROM [Administration].[DaysOfWeek]
	
--	DECLARE @CurrentWeekTimeTable TABLE (
--		MondayCourseId INT,
--		MondayCourseName VARCHAR(150),
--		TuesdayCourseId INT,
--		TuesdayCourseName VARCHAR(150),
--		WednesdayCourseId INT,
--		WednesdayCourseName VARCHAR(150),
--		ThursdayCourseId INT,
--		ThursCourseName VARCHAR(150),
--		FridayCourseId INT,
--		FridayCourseName VARCHAR(150),
--		SaturdayCourseId INT,
--		SaturdayCourseName VARCHAR(150),
--		SundayCourseId INT,
--		SundayCourseName VARCHAR(150)
--	)

--	;WITH DayTimeTable AS (
--		SELECT
--			DOW.Id AS DayId,
--			WH.Hour,
--			ScheduledCourse.[CourseId],
--			ScheduledCourse.Name -- AS Monday
--		FROM @WorkingHours WH
--		JOIN [Administration].[DaysOfWeek] DOW
--			ON 1 = 1
--		OUTER APPLY (
--			SELECT temp.[CourseId],
--				temp.Name
--			FROM (
--				SELECT C.Id AS [CourseId],
--					C.Name,
--					CT.DayOfWeekId,
--					DATEPART(HOUR, CT.[StartTime]) CourseStart,
--					DATEPART(HOUR, CT.[EndTime]) CourseEnd
--				FROM [Courses].[CourseTimes] CT
--				JOIN [Courses].[Courses] C
--					ON C.Id = CT.CourseId
--					AND C.IsDeleted = 0
--				JOIN [Users].[UsersCourses] UC
--					ON UC.CourseId = C.Id
--					AND UC.IsDeleted = 0
--				WHERE UC.UserId = @UserId
--			) temp
--				WHERE (CourseStart <= WH.Hour) AND (WH.Hour < CourseEnd)
--					AND DayOfWeekId = DOW.Id
--		) ScheduledCourse
--	)
--	SELECT 
--		DayTimeTable.CourseId,
--		DayTimeTable.Name
--	FROM DayTimeTable
--	JOIN @CurrentWeek CW
--		ON CW.WeekDay = DayTimeTable.DayId
--		AND DayTimeTable.DayId = 3
--	WHERE CW.DayDate NOT IN (
--		SELECT Date FROM [Administration].[Holidays]
--	)



--	--ELSE
--	--	SELECT 
--	--		WH.Hour,
--	--		NULL,
--	--		H.Name
--	--	FROM @WorkingHours WH
--	--	JOIN [Administration].[Holidays] H
--	--		ON H.Date = @DayDate
----END
END