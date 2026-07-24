CREATE PROCEDURE UpdateStockTransaction(
	@StockTransactionId INT,
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Amount INT,
	@Description NVARCHAR(MAX) = NULL
)
AS SET NOCOUNT ON
IF EXISTS (SELECT 1 FROM Product WHERE Id = @ProductId AND UserId = @UserId)
UPDATE st
SET ProductId = @ProductId,
	Amount = @Amount,
	Description = @Description
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.Id = @StockTransactionId AND p.UserId = @UserId
