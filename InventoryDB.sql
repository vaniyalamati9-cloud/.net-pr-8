CREATE DATABASE InventoryDB;
GO

USE InventoryDB;
GO

CREATE TABLE Product
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL
);
GO

INSERT INTO Product (ProductName, Category, Price, Quantity)
VALUES
('Laptop', 'Electronics', 55000, 10),
('Keyboard', 'Accessories', 1200, 25),
('Mouse', 'Accessories', 700, 40),
('Monitor', 'Electronics', 15000, 15);
GO