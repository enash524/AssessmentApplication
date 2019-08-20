function SalesOrderHeader(salesOrderId, accountNumber, freight, subTotal, taxAmt, totalDue, person, shipMethod, shipToAddress) {
	this.salesOrderId = salesOrderId;
	this.accountNumber = accountNumber;
	this.freight = freight;
	this.subTotal = subTotal;
	this.taxAmt = taxAmt;
	this.totalDue = totalDue;
	this.person = person;
	this.shipMethod = shipMethod;
	this.shipToAddress = shipToAddress;
}