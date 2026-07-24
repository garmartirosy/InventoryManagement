CREATE TABLE StockTransaction
(
	
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  ProductId INT NOT NULL,
  Amount INT NOT NULL,
  Description NVARCHAR(MAX),
 
  CONSTRAINT FK_StockTransaction_Product_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id)

)
