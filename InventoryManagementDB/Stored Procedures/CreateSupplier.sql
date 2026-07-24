CREATE PROCEDURE CreateSupplier(
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
INSERT INTO Supplier (UserId, Name)
VALUES (@UserId, @Name)
