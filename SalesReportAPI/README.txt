You need create two views at sqlserver in database Adventure works



USE [AdventureWorks2022]
GO

/****** Object:  View [Sales].[SalesRegion]    Script Date: 3/5/2024 22:20:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Sales].[SalesRegion]
AS
SELECT DISTINCT 
  p.Name AS ProductName,
  pc.Name AS ProductCategory,
  SUM(soh.SubTotal) OVER(PARTITION BY p.Name) AS TotalSales,
  SUM(soh.SubTotal) OVER(PARTITION BY st.CountryRegionCode) AS TotalSalesInRegion,
  (SUM(soh.SubTotal) OVER(PARTITION BY st.CountryRegionCode) / 
   NULLIF(SUM(soh.SubTotal) OVER(), 0)) * 100.0 AS PercentageOfTotalSalesInRegion,
  (SUM(soh.SubTotal) OVER(PARTITION BY st.CountryRegionCode, pc.Name) / 
   NULLIF(SUM(soh.SubTotal) OVER(PARTITION BY st.CountryRegionCode), 0)) * 100.0 AS PercentageOfTotalCategorySalesInRegion
FROM 
  Sales.SalesOrderHeader soh
INNER JOIN Sales.SalesTerritory st ON soh.TerritoryID = st.TerritoryID
INNER JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID
INNER JOIN Production.Product p ON sod.ProductID = p.ProductID
INNER JOIN Production.ProductSubcategory psc ON p.ProductSubcategoryID = psc.ProductSubcategoryID
INNER JOIN Production.ProductCategory pc ON psc.ProductCategoryID = pc.ProductCategoryID
GO

CREATE VIEW [Sales].[SalesReport] as

SELECT distinct Sales.SalesOrderDetail.SalesOrderID AS OrderID,
	   Sales.SalesOrderHeader.OrderDate,	
	   Sales.SalesOrderHeader.CustomerID, 
	   Sales.SalesOrderDetail.ProductID, 
	   Production.Product.Name AS ProductName, 
	   max(Production.ProductCategory.Name) AS CategoryName, 
       Sales.SalesOrderDetail.UnitPrice, 
	   Sales.ShoppingCartItem.Quantity, 
	   Sales.SalesOrderDetail.LineTotal AS TotalPrice, 
	   Sales.SalesOrderHeader.SalesPersonID, 
	   CONCAT(Person.FirstName,' ', Person.LastName) AS SalesPersonName,
	   Person.Address.AddressLine1 AS ShippingAddress, 
	   Person.Address.City, 
       Sales.SalesOrderHeader.BillToAddressID

FROM     
Sales.SalesOrderHeader INNER JOIN
Sales.SalesOrderDetail ON Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderDetail.SalesOrderID INNER JOIN
                  Sales.SalesPerson ON Sales.SalesOrderHeader.SalesPersonID = Sales.SalesPerson.BusinessEntityID INNER JOIN
                  Production.Product ON Sales.SalesOrderDetail.ProductID = Production.Product.ProductID INNER JOIN
                  Sales.ShoppingCartItem ON Production.Product.ProductID = Sales.ShoppingCartItem.ProductID INNER JOIN
                  Person.Person ON Sales.SalesPerson.BusinessEntityID = Person.Person.BusinessEntityID INNER JOIN
                  Person.Address ON Sales.SalesOrderHeader.BillToAddressID = Person.Address.AddressID AND 
				  Sales.SalesOrderHeader.ShipToAddressID = Person.Address.AddressID AND 
                  Sales.SalesOrderHeader.BillToAddressID = Person.Address.AddressID AND 
				  Sales.SalesOrderHeader.ShipToAddressID = Person.Address.AddressID AND 
				  Sales.SalesOrderHeader.BillToAddressID = Person.Address.AddressID AND 
                  Sales.SalesOrderHeader.ShipToAddressID = Person.Address.AddressID AND 
				  Sales.SalesOrderHeader.BillToAddressID = Person.Address.AddressID AND 
                  Sales.SalesOrderHeader.ShipToAddressID = Person.Address.AddressID CROSS JOIN
                  Production.ProductCategory
Group by Sales.SalesOrderDetail.SalesOrderID,
	   Sales.SalesOrderHeader.OrderDate,	
	   Sales.SalesOrderHeader.CustomerID, 
	   Sales.SalesOrderDetail.ProductID, 
	   Production.Product.Name,
       Sales.SalesOrderDetail.UnitPrice, 
	   Sales.ShoppingCartItem.Quantity, 
	   Sales.SalesOrderDetail.LineTotal, 
	   Sales.SalesOrderHeader.SalesPersonID, 
	   Person.Person.FirstName, 
	   Person.Person.LastName,
	   Person.Address.AddressLine1, 
	   Person.Address.City, 
       Sales.SalesOrderHeader.BillToAddressID
GO

