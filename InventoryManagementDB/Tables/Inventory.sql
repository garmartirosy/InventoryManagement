CREATE TABLE Inventory
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    ProductId INT NOT NULL,
    Amount INT NOT NULL DEFAULT 0 CHECK (Amount >= 0),

    CONSTRAINT FK_Inventory_Product_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
