CREATE PROCEDURE DeleteStockTransaction(
	@StockTransactionId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE st
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.Id = @StockTransactionId AND p.UserId = @UserId
