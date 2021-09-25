USE [AdventureWorks2017]
GO

/****** Object:  StoredProcedure [Sales].[uspGetSalesOrderHeader]    Script Date: 3/13/2020 4:49:02 PM ******/
DROP PROCEDURE [Sales].[uspGetSalesOrderHeader]
GO

/****** Object:  StoredProcedure [Sales].[uspGetSalesOrderHeader]    Script Date: 3/13/2020 4:49:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Sales].[uspGetSalesOrderHeader]
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

	DECLARE @tempSalesOrderHeaderTable TABLE
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
	   ,ShipMethodName		NVARCHAR(50) NOT NULL
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

	INSERT INTO @tempSalesOrderHeaderTable
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
			,CASE WHEN @sortBy = 'BusinessEntityID' THEN CAST(p.BusinessEntityID AS sql_variant)
				  WHEN @sortBy = 'Title' THEN CAST(p.Title AS sql_variant)
				  WHEN @sortBy = 'FirstName' THEN CAST(p.FirstName AS sql_variant)
				  WHEN @sortBy = 'MiddleName' THEN CAST(p.MiddleName AS sql_variant)
				  WHEN @sortBy = 'LastName' THEN CAST(p.LastName AS sql_variant)
				  WHEN @sortBy = 'Suffix' THEN CAST(p.Suffix AS sql_variant)
				  WHEN @sortBy = 'AccountNumber' THEN CAST(soh.AccountNumber AS sql_variant)
				  WHEN @sortBy = 'AddressLine1' THEN CAST(a.AddressLine1 AS sql_variant)
				  WHEN @sortBy = 'AddressLine2' THEN CAST(a.AddressLine2 AS sql_variant)
				  WHEN @sortBy = 'City' THEN CAST(a.City AS sql_variant)
				  WHEN @sortBy = 'StateProvinceCode' THEN CAST(sp.StateProvinceCode AS sql_variant)
				  WHEN @sortBy = 'PostalCode' THEN CAST(a.PostalCode AS sql_variant)
				  WHEN @sortBy = 'ShipMethod' THEN CAST(sm.[Name] AS sql_variant)
				  WHEN @sortBy = 'SubTotal' THEN CAST(soh.SubTotal AS sql_variant)
				  WHEN @sortBy = 'TaxAmt' THEN CAST(soh.TaxAmt AS sql_variant)
				  WHEN @sortBy = 'Freight' THEN CAST(soh.Freight AS sql_variant)
				  WHEN @sortBy = 'TotalDue' THEN CAST(soh.TotalDue AS sql_variant)
				  WHEN @sortBy = 'OrderDate' THEN CAST(soh.OrderDate AS sql_variant)
				  WHEN @sortBy = 'DueDate' THEN CAST(soh.DueDate AS sql_variant)
				  WHEN @sortBy = 'ShipDate' THEN CAST(soh.ShipDate AS sql_variant)
				  ELSE CAST(soh.SalesOrderID AS sql_variant)
			 END
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

	IF @orderDateStart IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			OrderDate > @orderDateStart
	END

	IF @orderDateEnd IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			@orderDateEnd < OrderDate
	END

	IF @dueDateStart IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			DueDate > @dueDateStart
	END

	IF @dueDateEnd IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			@dueDateEnd < DueDate
	END

	IF @shipDateStart IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			ShipDate IS NULL
			OR ShipDate > @shipDateStart
	END

	IF @shipDateEnd IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			ShipDate IS NULL
			OR @shipDateEnd < ShipDate
	END

	IF @customerName IS NOT NULL
	BEGIN
		DELETE FROM
			@tempSalesOrderHeaderTable
		WHERE
			FullName NOT LIKE '%' + @customerName + '%'
	END

	SELECT
		@recordCount = COUNT(1)
	FROM
		@tempSalesOrderHeaderTable

	SELECT
		*
	FROM
		@tempSalesOrderHeaderTable
	ORDER BY
		CASE WHEN @sortDirection = 'DESC' THEN SortBy END DESC
	   ,CASE WHEN @sortDirection = 'ASC' THEN SortBy END ASC
	OFFSET @offset ROWS
	FETCH NEXT @limit ROWS ONLY
END
GO

