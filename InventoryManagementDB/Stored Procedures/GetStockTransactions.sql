CREATE PROCEDURE GetStockTransactions(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT st.Id, st.ProductId, st.Amount, st.Description, p.Name AS ProductName
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE p.UserId = @UserId
ORDER BY st.Id DESC
