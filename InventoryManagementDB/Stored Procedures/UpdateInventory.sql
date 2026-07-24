CREATE PROCEDURE UpdateInventory(
	@InventoryId INT,
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Amount INT
)
AS SET NOCOUNT ON
IF EXISTS (SELECT 1 FROM Product WHERE Id = @ProductId AND UserId = @UserId)
UPDATE i
SET ProductId = @ProductId,
	Amount = @Amount
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.Id = @InventoryId AND p.UserId = @UserId
