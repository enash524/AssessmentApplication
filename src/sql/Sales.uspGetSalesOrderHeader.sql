USE [AdventureWorks2017]
GO

/****** Object:  StoredProcedure [Sales].[uspGetSalesOrderHeader]    Script Date: 10/7/2021 10:51:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [Sales].[uspGetSalesOrderHeader]
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

	IF OBJECT_ID('tempdb..#tempSalesOrderHeaderTable') IS NOT NULL
	BEGIN
		DROP TABLE #tempSalesOrderHeaderTable
	END

	CREATE TABLE #tempSalesOrderHeaderTable
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
	   ,FullAddress			NVARCHAR(175) NOT NULL
	   ,SortBy				SQL_VARIANT NULL
	)

	INSERT INTO #tempSalesOrderHeaderTable
		SELECT
			*
		   ,CASE WHEN @sortBy = 'BusinessEntityID' THEN CAST(BusinessEntityID AS sql_variant)
				 WHEN @sortBy = 'Title' THEN CAST(Title AS sql_variant)
				 WHEN @sortBy = 'FirstName' THEN CAST(FirstName AS sql_variant)
				 WHEN @sortBy = 'MiddleName' THEN CAST(MiddleName AS sql_variant)
				 WHEN @sortBy = 'LastName' THEN CAST(LastName AS sql_variant)
				 WHEN @sortBy = 'Suffix' THEN CAST(Suffix AS sql_variant)
				 WHEN @sortBy = 'AccountNumber' THEN CAST(AccountNumber AS sql_variant)
				 WHEN @sortBy = 'AddressLine1' THEN CAST(AddressLine1 AS sql_variant)
				 WHEN @sortBy = 'AddressLine2' THEN CAST(AddressLine2 AS sql_variant)
				 WHEN @sortBy = 'City' THEN CAST(City AS sql_variant)
				 WHEN @sortBy = 'StateProvinceCode' THEN CAST(StateProvinceCode AS sql_variant)
				 WHEN @sortBy = 'PostalCode' THEN CAST(PostalCode AS sql_variant)
				 WHEN @sortBy = 'ShipMethod' THEN CAST([Name] AS sql_variant)
				 WHEN @sortBy = 'SubTotal' THEN CAST(SubTotal AS sql_variant)
				 WHEN @sortBy = 'TaxAmt' THEN CAST(TaxAmt AS sql_variant)
				 WHEN @sortBy = 'Freight' THEN CAST(Freight AS sql_variant)
				 WHEN @sortBy = 'TotalDue' THEN CAST(TotalDue AS sql_variant)
				 WHEN @sortBy = 'OrderDate' THEN CAST(OrderDate AS sql_variant)
				 WHEN @sortBy = 'DueDate' THEN CAST(DueDate AS sql_variant)
				 WHEN @sortBy = 'ShipDate' THEN CAST(ShipDate AS sql_variant)
				 WHEN @sortBy = 'FullName' THEN CAST(FullName AS sql_variant)
				 WHEN @sortBy = 'Address' THEN CAST(FullAddress AS sql_variant)
				 ELSE CAST(SalesOrderID AS sql_variant)
			END 'SortBy'
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
			   ,CONCAT_WS(' ', Title, FirstName, MiddleName, LastName, Suffix) AS 'FullName'
			   ,CONCAT_WS(' ', StateProvinceCode, City, PostalCode, AddressLine1, AddressLine2) AS 'FullAddress'
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
		) AS HeaderInfo

	IF @orderDateStart IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			OrderDate < @orderDateStart
	END

	IF @orderDateEnd IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			OrderDate > @orderDateEnd
	END

	IF @dueDateStart IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			DueDate < @dueDateStart
	END

	IF @dueDateEnd IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			DueDate > @dueDateEnd
	END

	IF @shipDateStart IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			ShipDate IS NULL
			OR ShipDate < @shipDateStart
	END

	IF @shipDateEnd IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			ShipDate IS NULL
			OR ShipDate > @shipDateEnd
	END

	IF @customerName IS NOT NULL
	BEGIN
		DELETE FROM
			#tempSalesOrderHeaderTable
		WHERE
			FullName NOT LIKE '%' + @customerName + '%'
	END

	SELECT
		@recordCount = COUNT(1)
	FROM
		#tempSalesOrderHeaderTable

	SELECT
		*
	FROM
		#tempSalesOrderHeaderTable
	ORDER BY
		CASE WHEN @sortDirection = 'DESC' THEN SortBy END DESC
	   ,CASE WHEN @sortDirection = 'ASC' THEN SortBy END ASC
	OFFSET @offset ROWS
	FETCH NEXT @limit ROWS ONLY

	IF OBJECT_ID('tempdb..#tempSalesOrderHeaderTable') IS NOT NULL
	BEGIN
		DROP TABLE #tempSalesOrderHeaderTable
	END
END
GO

