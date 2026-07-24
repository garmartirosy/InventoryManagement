CREATE PROCEDURE DeleteInventory(
	@InventoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE i
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.Id = @InventoryId AND p.UserId = @UserId
