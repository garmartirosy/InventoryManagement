
CREATE TABLE ProductCategory
(
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	UserId NVARCHAR(450) NOT NULL,
	Name VARCHAR(100) NOT NULL,

	CONSTRAINT FK_ProductCategory_AspNetUsers_UserId 
		FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
)
GO

CREATE TABLE Supplier
(
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	UserId NVARCHAR(450) NOT NULL,
	Name VARCHAR(100) NOT NULL,

	CONSTRAINT FK_Supplier_AspNetUsers_UserId 
		FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)

)
GO

CREATE TABLE Product
(
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	UserId NVARCHAR(450) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Price DECIMAL(10,2) NOT NULL,
	CategoryId INT NULL,

	CONSTRAINT FK_Product_Category_CategoryId FOREIGN KEY (CategoryId) 
		REFERENCES ProductCategory(Id),
	CONSTRAINT FK_Product_AspNetUsers_UserId FOREIGN KEY (UserId) 
		REFERENCES AspNetUsers(Id)

)
GO

CREATE TABLE Inventory
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    ProductId INT NOT NULL,
    Amount INT NOT NULL DEFAULT 0 CHECK (Amount >= 0),

    CONSTRAINT FK_Inventory_Product_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
GO

CREATE TABLE StockTransaction
(
	
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  ProductId INT NOT NULL,
  Amount INT NOT NULL,
  Description NVARCHAR(MAX),
 
  CONSTRAINT FK_StockTransaction_Product_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id)

)
GO

CREATE PROCEDURE CreateProduct(
	@UserId NVARCHAR(450),
	@Name VARCHAR(100),
	@Price DECIMAL(10,2),
	@CategoryId INT = NULL
)
AS SET NOCOUNT ON
IF @CategoryId IS NULL OR EXISTS (SELECT 1 FROM ProductCategory WHERE Id = @CategoryId AND UserId = @UserId)
INSERT INTO Product (UserId, Name, Price, CategoryId)
VALUES (@UserId, @Name, @Price, @CategoryId)
GO

CREATE PROCEDURE CreateProductCategory(
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
INSERT INTO ProductCategory (UserId, Name)
VALUES (@UserId, @Name)
GO

CREATE PROCEDURE CreateInventory(
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Amount INT
)
AS SET NOCOUNT ON
INSERT INTO Inventory (ProductId, Amount)
SELECT @ProductId, @Amount
WHERE EXISTS (SELECT 1 FROM Product WHERE Id = @ProductId AND UserId = @UserId)
GO

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
GO

CREATE PROCEDURE CreateSupplier(
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
INSERT INTO Supplier (UserId, Name)
VALUES (@UserId, @Name)
GO

CREATE PROCEDURE DeleteInventory(
	@InventoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE i
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.Id = @InventoryId AND p.UserId = @UserId
GO

CREATE PROCEDURE DeleteProduct(
	@ProductId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE st
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.ProductId = @ProductId AND p.UserId = @UserId

DELETE i
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.ProductId = @ProductId AND p.UserId = @UserId

DELETE FROM Product
WHERE Id = @ProductId AND UserId = @UserId
GO

CREATE PROCEDURE DeleteProductCategory(
	@ProductCategoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
UPDATE Product
SET CategoryId = NULL
WHERE CategoryId = @ProductCategoryId AND UserId = @UserId

DELETE FROM ProductCategory
WHERE Id = @ProductCategoryId AND UserId = @UserId
GO

CREATE PROCEDURE DeleteStockTransaction(
	@StockTransactionId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE st
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.Id = @StockTransactionId AND p.UserId = @UserId
GO

CREATE PROCEDURE DeleteSupplier(
	@SupplierId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
DELETE FROM Supplier
WHERE Id = @SupplierId AND UserId = @UserId
GO

CREATE PROCEDURE GetInventories(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT i.Id, i.ProductId, i.Amount, p.Name AS ProductName
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE p.UserId = @UserId
ORDER BY p.Name
GO

CREATE PROCEDURE GetInventoryById(
	@InventoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT i.Id, i.ProductId, i.Amount, p.Name AS ProductName
FROM Inventory i
INNER JOIN Product p ON i.ProductId = p.Id
WHERE i.Id = @InventoryId AND p.UserId = @UserId
GO

CREATE PROCEDURE GetProductById(
	@ProductId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT p.Id, p.UserId, p.Name, p.Price, p.CategoryId, ISNULL(c.Name, '') AS CategoryName
FROM Product p
LEFT JOIN ProductCategory c ON p.CategoryId = c.Id
WHERE p.Id = @ProductId AND p.UserId = @UserId
GO

CREATE PROCEDURE GetProductCategories(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM ProductCategory
WHERE UserId = @UserId
ORDER BY Name
GO

CREATE PROCEDURE GetProductCategoryById(
	@ProductCategoryId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM ProductCategory
WHERE Id = @ProductCategoryId AND UserId = @UserId
GO

CREATE PROCEDURE GetProducts(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT p.Id, p.UserId, p.Name, p.Price, p.CategoryId, ISNULL(c.Name, '') AS CategoryName
FROM Product p
LEFT JOIN ProductCategory c ON p.CategoryId = c.Id
WHERE p.UserId = @UserId
ORDER BY p.Name
GO

CREATE PROCEDURE GetStockTransactionById(
	@StockTransactionId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT st.Id, st.ProductId, st.Amount, st.Description, p.Name AS ProductName
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE st.Id = @StockTransactionId AND p.UserId = @UserId
GO

CREATE PROCEDURE GetStockTransactions(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT st.Id, st.ProductId, st.Amount, st.Description, p.Name AS ProductName
FROM StockTransaction st
INNER JOIN Product p ON st.ProductId = p.Id
WHERE p.UserId = @UserId
ORDER BY st.Id DESC
GO

CREATE PROCEDURE GetSupplierById(
	@SupplierId INT,
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM Supplier
WHERE Id = @SupplierId AND UserId = @UserId
GO

CREATE PROCEDURE GetSuppliers(
	@UserId NVARCHAR(450)
)
AS SET NOCOUNT ON
SELECT Id, UserId, Name
FROM Supplier
WHERE UserId = @UserId
ORDER BY Name
GO

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
GO

CREATE PROCEDURE UpdateProduct(
	@ProductId INT,
	@UserId NVARCHAR(450),
	@Name VARCHAR(100),
	@Price DECIMAL(10,2),
	@CategoryId INT = NULL
)
AS SET NOCOUNT ON
IF @CategoryId IS NULL OR EXISTS (SELECT 1 FROM ProductCategory WHERE Id = @CategoryId AND UserId = @UserId)
UPDATE Product
SET Name = @Name,
	Price = @Price,
	CategoryId = @CategoryId
WHERE Id = @ProductId AND UserId = @UserId
GO

CREATE PROCEDURE UpdateProductCategory(
	@ProductCategoryId INT,
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
UPDATE ProductCategory
SET Name = @Name
WHERE Id = @ProductCategoryId AND UserId = @UserId
GO

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
GO

CREATE PROCEDURE UpdateSupplier(
	@SupplierId INT,
	@UserId NVARCHAR(450),
	@Name VARCHAR(100)
)
AS SET NOCOUNT ON
UPDATE Supplier
SET Name = @Name
WHERE Id = @SupplierId AND UserId = @UserId
GO
