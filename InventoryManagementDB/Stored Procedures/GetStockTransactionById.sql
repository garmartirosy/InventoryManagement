CREATE PROCEDURE GetStockTransactionById(
	@StockTransactionId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT st.Id, st.ProductId, st.Amount, st.Description, p.Name AS ProductName
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.Id = @StockTransactionId AND p.UserId = @UserId
