function SearchFormModel() {
	this.customerName = null;
	this.dateOptions = {
		showWeeks: false,
		startingDay: 0
	};
	this.datepickerFormat = "M/d/yyyy";
	this.dueDate = new DateModel();
	this.orderDate = new DateModel();
	this.salesOrders = null;
	this.searchButtonClicked = false;
	this.shipDate = new DateModel();
}

SearchFormModel.prototype.resetValues = function() {
	this.customerName = null;
	this.dueDate.resetValues();
	this.orderDate.resetValues();
	this.salesOrders = null;
	this.searchButtonClicked = false;
	this.shipDate.resetValues();
};