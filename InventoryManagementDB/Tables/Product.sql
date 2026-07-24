CREATE TABLE Product
(
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	UserId NVARCHAR(450) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Price DECIMAL(10,2) NOT NULL,
	CategoryId INT NULL,
	SupplierId INT NULL,

	CONSTRAINT FK_Product_Category_CategoryId FOREIGN KEY (CategoryId)
		REFERENCES ProductCategory(Id),
	CONSTRAINT FK_Product_Supplier_SupplierId FOREIGN KEY (SupplierId)
		REFERENCES Supplier(Id),
	CONSTRAINT FK_Product_AspNetUsers_UserId FOREIGN KEY (UserId)
		REFERENCES AspNetUsers(Id)

)
