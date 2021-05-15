-- =============================================
-- Author:		<demorales13@outlook.com>
-- Create date: <15-05-2021>
-- Description:	<Inserción de datos en la tabla documentos>
-- =============================================
CREATE PROCEDURE document_insert 
	@SenderId int,
	@AddresseeId int,
	@Type nvarchar(2),
	@InternalFileId int,
	@ExternalFileId int,
	@CreatedBy varchar,
	@CreatedAt datetime,
	@UpdatedBy varchar,
	@UpdatedAt datetime
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO [dbo].[Documents]
           (Type,
           InternalFileId,
           ExternalFileId,
           SenderId,
           AddresseeId,
           CreatedBy,
           CreatedAt,
           UpdatedBy,
           UpdatedAt)
     VALUES
           (@Type,
            @InternalFileId,
            @ExternalFileId,
            @SenderId,
            @AddresseeId,
            @CreatedBy,
            @CreatedAt,
            @UpdatedBy,
            @UpdatedAt)
END
GO
