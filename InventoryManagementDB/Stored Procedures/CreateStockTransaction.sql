CREATE PROCEDURE CreateStockTransaction(
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Amount INT,
	@Description NVARCHAR(MAX) = NULL
)
AS SET NOCOUNT ON
INSERT INTO StockTransaction (ProductId, Amount, Description)
SELECT @ProductId, @Amount, @Description
WHERE EXISTS (SELECT 1 FROM Product WHERE Id = @ProductId AND UserId = @UserId)
