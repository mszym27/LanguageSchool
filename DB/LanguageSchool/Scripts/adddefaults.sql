SELECT 'ALTER TABLE ' + SCHEMA_NAME(schema_id) + '.' + t.name +
	' ADD CONSTRAINT DF_' + t.name + 
	'CreationDate DEFAULT GETDATE() FOR CreationDate'
FROM sys.tables AS t
INNER JOIN sys.columns c 
	ON t.OBJECT_ID = c.OBJECT_ID
WHERE c.name LIKE 'CreationDate'