BEGIN TRAN

INSERT INTO [Materials].[Materials]
	([CreationDate]
    ,[IsDeleted]
    ,[LessonSubjectId]
    ,[Name]
    ,[File]
    ,[Comment])
VALUES
	(GETDATE()
    ,0
    ,15
    ,N'czasy_podstawowe'
    ,(SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\marcinszymko\Downloads\present-times-repeat.pdf', SINGLE_BLOB) AS pdf)
    ,N'Powtórzenie podstawowych czasów teraźniejszych. Proszę o ponowne przerobienie materiałów przed następnym spotkaniem.')

COMMIT