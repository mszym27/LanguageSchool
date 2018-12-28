CREATE PROCEDURE [ContactInfo].[GetContactInfoList]
	 @CreationDateFrom DATETIME
	,@CreationDateTo DATETIME
	,@PrefferedHoursFrom INT
	,@PrefferedHoursTo INT
	,@FullName NVARCHAR(80)
	,@City NVARCHAR(150)
	,@Street NVARCHAR(250)
	,@PhoneNumber VARCHAR(15)
	,@EmailAdress VARCHAR(255)
	,@ShowUserData BIT
	,@ShowContactRequests BIT
	,@SortColumn VARCHAR(50)
	,@SortDirection VARCHAR(4)
	,@PageIndex INT
	,@PageSize INT
AS
BEGIN
DECLARE @Query NVARCHAR(MAX)
	,@QueryParameters NVARCHAR(MAX)

SET @CreationDateTo += '23:59:59'

SET @City = LTRIM(RTRIM(@City))
SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber))
SET @EmailAdress = LTRIM(RTRIM(@EmailAdress))
SET @FullName = LTRIM(RTRIM(@FullName))
SET @Street = LTRIM(RTRIM(@Street))

SELECT @ShowContactRequests = 0
WHERE (@FullName IS NOT NULL AND @FullName != '')
	OR (@Street IS NOT NULL AND @Street != '')
	OR (@City IS NOT NULL AND @City != '')

SET @Query = N''

SET @Query += N'
SELECT temp.[Id]
	,temp.[CreationDate]
	,temp.FullName
	,temp.[City]
	,temp.Street
	,temp.[PhoneNumber]
	,temp.[EmailAdress]
	,temp.[Comment]
	,temp.IsContactRequest
	,COUNT(1) OVER() AS TotalRowCount
FROM ( '

IF(@ShowUserData = 1)
BEGIN
	SET @Query += N'
	SELECT [UserData].[Id]
		,[UserData].[CreationDate]
		,[UserData].[Name] + '' '' + [UserData].[Surname] AS FullName
		,[UserData].[City]
		,ISNULL([UserData].[Street] + '' '','''') +
			ISNULL([UserData].[HouseNumber] + '' '','''') +
			ISNULL([UserData].[HomeNumber] + '' '','''') AS Street
		,[UserData].[PublicPhoneNumber] AS [PhoneNumber]
		,[UserData].[EmailAdress]
		,[UserData].[Comment]
		,0 AS IsContactRequest
	FROM [LanguageSchool].[ContactInfo].[UserData]
	WHERE [UserData].[IsDeleted] = 0
		AND [UserData].[CreationDate] >= @CreationDateFrom
		AND [UserData].[CreationDate] <= @CreationDateTo '
	
	IF(@City IS NOT NULL AND @City != '')
		SET @Query += N'
		AND [UserData].[City] LIKE ''%'' + @City + ''%'' '
	
	IF(@PhoneNumber IS NOT NULL AND @PhoneNumber != '')
		SET @Query += N'
		AND [UserData].[PublicPhoneNumber] LIKE ''%'' + @PhoneNumber + ''%'' '
	
	IF(@EmailAdress IS NOT NULL AND @EmailAdress != '')
		SET @Query += N'
		AND [UserData].[EmailAdress] LIKE ''%'' + @EmailAdress + ''%'' '
END

IF(@ShowUserData = 1 AND @ShowContactRequests = 1)
	SET @Query += N'
	UNION ALL '
	
IF(@ShowContactRequests = 1)
BEGIN
	SET @Query += N'
	SELECT [ContactRequests].[Id]
		,[ContactRequests].[CreationDate]
		,'''' AS FullName
		,'''' AS City
		,'''' AS Street
		,[ContactRequests].[PhoneNumber]
		,[ContactRequests].[EmailAdress]
		,[ContactRequests].[Comment]
		,1 AS IsContactRequest
	FROM [ContactInfo].[ContactRequests]
	WHERE [ContactRequests].[IsAwaiting] = 1
		AND [ContactRequests].[CreationDate] >= @CreationDateFrom
		AND [ContactRequests].[CreationDate] <= @CreationDateTo
		AND [ContactRequests].[PreferredHoursFrom] >= @PreferredHoursFrom
		AND [ContactRequests].[PreferredHoursTo] <= @PreferredHoursTo '
	
	IF(@PhoneNumber IS NOT NULL AND @PhoneNumber != '')
		SET @Query += N'
		AND [ContactRequests].[PhoneNumber] LIKE ''%'' + @PhoneNumber + ''%'' '
	
	IF(@EmailAdress IS NOT NULL AND @EmailAdress != '')
		SET @Query += N'
		AND [ContactRequests].[EmailAdress] LIKE ''%'' + @EmailAdress + ''%'' '
END

SET @Query += N'
) temp
WHERE 1 = 1 '
	
IF(@FullName IS NOT NULL AND @FullName != '')
	SET @Query += N'
	AND [temp].[FullName] LIKE ''%'' + @FullName + ''%'' '
	
IF(@Street IS NOT NULL AND @Street != '')
	SET @Query += N'
	AND [temp].[Street] LIKE ''%'' + @Street + ''%'' '

SET @Query += N'
ORDER BY ' + @SortColumn + ' ' + @SortDirection + N'
OFFSET (@PageIndex - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY '

SET @QueryParameters = N'
	 @CreationDateFrom DATETIME
	,@CreationDateTo DATETIME
	,@FullName NVARCHAR(80)
	,@City NVARCHAR(150)
	,@Street NVARCHAR(250)
	,@PhoneNumber VARCHAR(15)
	,@EmailAdress VARCHAR(255)
	,@PageIndex INT
	,@PageSize INT '

EXECUTE sp_executesql @Query, @QueryParameters
	,@CreationDateFrom
	,@CreationDateTo
	,@FullName
	,@City
	,@Street
	,@PhoneNumber
	,@EmailAdress
	,@PageIndex
	,@PageSize

--SELECT temp.[Id]
--	,temp.[CreationDate]
--	,temp.FullName
--	,temp.[City]
--	,temp.Street
--	,temp.[PhoneNumber]
--	,temp.[EmailAdress]
--	,temp.[Comment]
--	,temp.IsContactRequest
--	,COUNT(1) OVER() AS TotalRowCount
--FROM (
--	SELECT [UserData].[Id]
--		,[UserData].[CreationDate]
--		,[UserData].[Name] + ' ' + [UserData].[Surname] AS FullName
--		,[UserData].[City]
--		,ISNULL([UserData].[Street] + ' ','') +
--			ISNULL([UserData].[HouseNumber] + ' ','') +
--			ISNULL([UserData].[HomeNumber] + ' ','') AS Street
--		,[UserData].[PublicPhoneNumber] AS [PhoneNumber]
--		,[UserData].[EmailAdress]
--		,[UserData].[Comment]
--		,0 AS IsContactRequest
--	FROM [LanguageSchool].[ContactInfo].[UserData]
--	WHERE [UserData].[IsDeleted] = 0

--	UNION ALL

--	SELECT [ContactRequests].[Id]
--		,[ContactRequests].[CreationDate]
--		,'' AS FullName
--		,'' AS City
--		,'' AS Street
--		,[ContactRequests].[PhoneNumber]
--		,[ContactRequests].[EmailAdress]
--		,[ContactRequests].[Comment]
--		,1 AS IsContactRequest
--	FROM [ContactInfo].[ContactRequests]
--	WHERE [ContactRequests].[IsAwaiting] = 1
--) temp
END