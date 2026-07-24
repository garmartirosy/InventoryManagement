CREATE PROCEDURE DeleteSupplier(
	@SupplierId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE FROM Supplier
WHERE Id = @SupplierId AND UserId = @UserId
