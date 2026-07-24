CREATE PROCEDURE UpdateSupplier(
	@SupplierId INT,
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
UPDATE Supplier
SET Name = @Name
WHERE Id = @SupplierId AND UserId = @UserId
