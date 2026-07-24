CREATE PROCEDURE GetInventoryById(
	@InventoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT i.Id, i.ProductId, i.Amount, p.Name AS ProductName
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.Id = @InventoryId AND p.UserId = @UserId
