USE [AdventureWorks2012]
GO

select *
from
(
SELECT soh.SalesOrderID, p.Title, p.FirstName, p.MiddleName, p.LastName, p.Suffix, 
	soh.AccountNumber, a.AddressLine1, a.AddressLine2, a.City,
	sp.StateProvinceCode, a.PostalCode, sm.Name, soh.SubTotal, soh.TaxAmt,
	soh.Freight, soh.TotalDue,
	soh.OrderDate, soh.DueDate, soh.ShipDate,
	--LTRIM(RTRIM(ISNULL(p.title, '') + ISNULL(' ' + p.FirstName, '') + ISNULL(' ' + p.MiddleName, '') + ISNULL(' ' + p.LastName, '') + ISNULL(' ' + p.Suffix, ''))) 'FullName'
	LTRIM(RTRIM(REPLACE(CONCAT(p.Title,' ',p.FirstName,' ',p.MiddleName,' ',p.LastName,' ',p.Suffix), '  ', ' '))) 'FullName'
FROM Sales.SalesOrderHeader soh
JOIN Sales.Customer c ON c.CustomerID = soh.CustomerID
JOIN Person.Person p ON p.BusinessEntityID = c.PersonID
JOIN Person.[Address] a ON a.AddressID = soh.ShipToAddressID
JOIN Person.StateProvince sp ON sp.StateProvinceID = a.StateProvinceID
JOIN Purchasing.ShipMethod sm ON sm.ShipMethodID = soh.ShipMethodID
) a
--where a.FullName like '%gio%'
--where contains(FullName, 'gio')

select *
from Sales.SalesOrderDetail sod

select *
from sales.SpecialOfferProduct

select *
from Production.Product

select p.Name, p.ProductNumber, sod.OrderQty, SOD.UnitPrice, sod.UnitPriceDiscount, sod.LineTotal
from Sales.SalesOrderDetail sod
join Sales.SpecialOfferProduct sop on sop.ProductID = sod.ProductID and sop.SpecialOfferID = sod.SpecialOfferID
join Production.Product p on p.ProductID = sop.ProductID

exec Sales.uspGetSalesOrderDetail 43659

exec Sales.uspSearchSalesOrderHeader
