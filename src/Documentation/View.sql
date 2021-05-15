CREATE VIEW [DocumentsView] AS
SELECT 
	t02.Name AS SenderName,
	t02.LastName AS SenderLastName,
	t03.Name AS AddresseeName,
	t03.LastName AS AddresseeLastName,
	t01.Type,
	ISNULL(t04.InternalFileId, t05.ExternalFileId) AS Consecutive,
	t01.CreatedAt,
	t01.CreatedBy
FROM 
	[dbo].Documents AS t01 INNER JOIN
	[dbo].People AS t02 ON t01.SenderId = t02.PersonId INNER JOIN
	[dbo].People AS t03 ON t01.AddresseeId = t03.PersonId INNER JOIN
	[dbo].InternalFile AS t04 ON t01.InternalFileId = t04.InternalFileId INNER JOIN
	[dbo].ExternalFile AS t05 ON t01.ExternalFileId = t05.ExternalFileId
