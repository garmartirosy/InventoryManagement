CREATE PROCEDURE CreateInventory(
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Amount INT
)
AS SET NOCOUNT ON
INSERT INTO Inventory (ProductId, Amount)
SELECT @ProductId, @Amount
WHERE EXISTS (SELECT 1 FROM Product WHERE Id = @ProductId AND UserId = @UserId)
