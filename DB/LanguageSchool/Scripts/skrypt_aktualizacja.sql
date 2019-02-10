DECLARE @now DATE = GETDATE()

UPDATE [Courses].[Courses] SET IsActive = 0 WHERE [StartDate] = @now

DELETE FROM [Materials].[Materials] WHERE CAST([DeletionDate] AS DATE) = DATEADD(DAY, -31, @now)