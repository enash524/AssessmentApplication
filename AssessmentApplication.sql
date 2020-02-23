USE [AdventureWorks2012]
GO
/****** Object:  StoredProcedure [Sales].[uspGetSalesOrderDetail]    Script Date: 3/18/2017 11:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Nash
-- Create date: March 11, 2017
-- Description:	Search Sales.SalesOrderDetail based on order id
-- =============================================
CREATE PROCEDURE [Sales].[uspGetSalesOrderDetail]
(
	@salesOrderId	INT
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		p.Name
		,p.ProductNumber
		,sod.OrderQty
		,SOD.UnitPrice
		,sod.UnitPriceDiscount
		,sod.LineTotal
	FROM
		Sales.SalesOrderDetail sod
	JOIN
		Sales.SpecialOfferProduct sop ON sop.ProductID = sod.ProductID AND sop.SpecialOfferID = sod.SpecialOfferID
	JOIN
		Production.Product p ON p.ProductID = sop.ProductID
	WHERE
		sod.SalesOrderID = @salesOrderId
END

GO
/****** Object:  StoredProcedure [Sales].[uspSearchSalesOrderHeader]    Script Date: 3/18/2017 11:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Nash
-- Create date: March 11, 2017
-- Description:	Search Sales.SalesOrderHeader based on provided criteria
-- =============================================
CREATE PROCEDURE [Sales].[uspSearchSalesOrderHeader]
(
	@orderDateStart	DATETIME = NULL,
	@orderDateEnd	DATETIME = NULL,
	@dueDateStart	DATETIME = NULL,
	@dueDateEnd		DATETIME = NULL,
	@shipDateStart	DATETIME = NULL,
	@shipDateEnd	DATETIME = NULL,
	@customerName	NVARCHAR(128) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		*
	FROM
	(
		SELECT
			soh.SalesOrderID
			,p.BusinessEntityID
			,p.Title
			,p.FirstName
			,p.MiddleName
			,p.LastName
			,p.Suffix
			,soh.AccountNumber
			,a.AddressID
			,a.AddressLine1
			,a.AddressLine2
			,a.City
			,sp.StateProvinceCode
			,a.PostalCode
			,sm.ShipMethodID
			,sm.Name
			,soh.SubTotal
			,soh.TaxAmt
			,soh.Freight
			,soh.TotalDue
			,soh.OrderDate
			,soh.DueDate
			,soh.ShipDate
			,LTRIM(RTRIM(REPLACE(CONCAT(p.Title,' ',p.FirstName,' ',p.MiddleName,' ',p.LastName,' ',p.Suffix), '  ', ' '))) 'FullName'
		FROM
			Sales.SalesOrderHeader soh
		JOIN
			Sales.Customer c ON c.CustomerID = soh.CustomerID
		JOIN
			Person.Person p ON p.BusinessEntityID = c.PersonID
		JOIN
			Person.[Address] a ON a.AddressID = soh.ShipToAddressID
		JOIN
			Person.StateProvince sp ON sp.StateProvinceID = a.StateProvinceID
		JOIN
			Purchasing.ShipMethod sm ON sm.ShipMethodID = soh.ShipMethodID
	) b
	WHERE
		((@orderDateStart IS NULL) OR (@orderDateStart <= b.OrderDate))
		AND ((@orderDateEnd IS NULL) OR (b.OrderDate <= @orderDateEnd))
		AND ((@dueDateStart IS NULL) OR (@dueDateStart <= b.DueDate))
		AND ((@dueDateEnd IS NULL) OR (b.DueDate <= @dueDateEnd))
		AND ((@shipDateStart IS NULL) OR (@shipDateStart <= b.ShipDate))
		AND ((@shipDateEnd IS NULL) OR (b.ShipDate <= @shipDateEnd))
		AND ((@customerName IS NULL) OR (b.FullName LIKE '%'+ @customerName + '%'))
END

GO
