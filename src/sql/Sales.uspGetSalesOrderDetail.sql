USE [AdventureWorks2017]
GO

/****** Object:  StoredProcedure [Sales].[uspGetSalesOrderDetail]    Script Date: 3/4/2020 11:55:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Sales].[uspGetSalesOrderDetail]
(
	@salesOrderId INT
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		p.[Name]
	   ,p.ProductNumber
	   ,sod.OrderQty
	   ,sod.UnitPrice
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

