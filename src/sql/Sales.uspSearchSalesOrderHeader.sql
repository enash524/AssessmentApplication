USE [AdventureWorks2017]
GO

/****** Object:  StoredProcedure [Sales].[uspSearchSalesOrderHeader]    Script Date: 3/4/2020 11:55:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Sales].[uspSearchSalesOrderHeader]
(
	@orderDateStart DATETIME = NULL
   ,@orderDateEnd	DATETIME = NULL
   ,@dueDateStart	DATETIME = NULL
   ,@dueDateEnd		DATETIME = NULL
   ,@shipDateStart	DATETIME = NULL
   ,@shipDateEnd	DATETIME = NULL
   ,@customerName	NVARCHAR(128) = NULL
   ,@limit			INT = 10
   ,@offset			INT = 0
   ,@sortBy			NVARCHAR(100) = 'SalesOrderID'
   ,@sortDirection	NVARCHAR(4) = 'ASC'
   ,@recordCount	INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	IF @limit < 1
	BEGIN
		SET @limit = 10
	END

	IF @offset < 0
	BEGIN
		SET @offset = 0
	END

	DECLARE @TempSalesOrderHeaderTable TABLE
	(
		SalesOrderID		INT NOT NULL
	   ,BusinessEntityID	INT NOT NULL
	   ,Title				NVARCHAR(8) NULL
	   ,FirstName			NVARCHAR(50) NOT NULL
	   ,MiddleName			NVARCHAR(50) NULL
	   ,LastName			NVARCHAR(50) NOT NULL
	   ,Suffix				NVARCHAR(10) NULL
	   ,AccountNumber		NVARCHAR(15) NULL
	   ,AddressID			INT NOT NULL
	   ,AddressLine1		NVARCHAR(60) NOT NULL
	   ,AddressLine2		NVARCHAR(60) NULL
	   ,City				NVARCHAR(30) NOT NULL
	   ,StateProvinceCode	NCHAR(3) NOT NULL
	   ,PostalCode			NVARCHAR(15) NOT NULL
	   ,ShipMethodID		INT NOT NULL
	   ,[Name]				NVARCHAR(50) NOT NULL
	   ,SubTotal			MONEY NOT NULL
	   ,TaxAmt				MONEY NOT NULL
	   ,Freight				MONEY NOT NULL
	   ,TotalDue			MONEY NOT NULL
	   ,OrderDate			DATETIME NOT NULL
	   ,DueDate				DATETIME NOT NULL
	   ,ShipDate			DATETIME NULL
	   ,FullName			NVARCHAR(168) NOT NULL
	   ,SortBy				SQL_VARIANT NULL
	)

	INSERT INTO @TempSalesOrderHeaderTable
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
		,sm.[Name]
		,soh.SubTotal
		,soh.TaxAmt
		,soh.Freight
		,soh.TotalDue
		,soh.OrderDate
		,soh.DueDate
		,soh.ShipDate
		,LTRIM(RTRIM(REPLACE(CONCAT(p.Title, ' ', p.FirstName, ' ', p.MiddleName, ' ', p.LastName, ' ', p.Suffix), '  ', ' ')))
		,NULL
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

	SELECT
		@recordCount = COUNT(1)
	FROM
		@TempSalesOrderHeaderTable
/*
	SELECT *
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
		   ,sm.[Name]
		   ,soh.SubTotal
		   ,soh.TaxAmt
		   ,soh.Freight
		   ,soh.TotalDue
		   ,soh.OrderDate
		   ,soh.DueDate
		   ,soh.ShipDate
		   ,LTRIM(RTRIM(REPLACE(CONCAT(p.Title, ' ', p.FirstName, ' ', p.MiddleName, ' ', p.LastName, ' ', p.Suffix), '  ', ' '))) 'FullName'
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
		AND ((@orderDateEnd IS NULL) OR (b.OrderDate >= @orderDateEnd))
		AND ((@dueDateStart IS NULL) OR (@dueDateStart <= b.DueDate))
		AND ((@dueDateEnd IS NULL) OR (b.DueDate >= @dueDateEnd))
		AND ((@shipDateStart IS NULL) OR (@shipDateStart <= b.ShipDate))
		AND ((@shipDateEnd IS NULL) OR (b.ShipDate >= @shipDateEnd))
		AND ((@customerName IS NULL) OR (b.FullName LIKE '%' + @customerName + '%'))
*/

END
GO

