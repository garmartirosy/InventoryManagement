CREATE PROCEDURE GetSupplierById(
	@SupplierId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM Supplier
WHERE Id = @SupplierId AND UserId = @UserId
